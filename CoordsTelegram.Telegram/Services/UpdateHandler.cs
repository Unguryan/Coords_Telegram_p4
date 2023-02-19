using CoordsTelegram.Telegram.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace CoordsTelegram.Telegram.Services;

public class UpdateHandler 
{
    private readonly ITelegramBotClient _botClient;
    private readonly ILogger<UpdateHandler> _logger;
    private readonly ITelegramMessageService _telegramMessageService;

    public UpdateHandler(ITelegramBotClient botClient, ILogger<UpdateHandler> logger, ITelegramMessageService telegramMessageParser)
    {
        _botClient = botClient;
        _logger = logger;
        _telegramMessageService = telegramMessageParser;
    }

    public async Task HandleUpdateAsync(Update update, CancellationToken cancellationToken)
    {
        var handler = update switch
        {
            // UpdateType.Unknown:
            // UpdateType.ChannelPost:
            // UpdateType.EditedChannelPost:
            // UpdateType.ShippingQuery:
            // UpdateType.PreCheckoutQuery:
            // UpdateType.Poll:
            { Message: { } message } => BotOnMessageReceived(message, cancellationToken),
            { CallbackQuery: { } callbackQuery } => BotOnCallbackQueryReceived(callbackQuery, cancellationToken),
            { MyChatMember: { } myChatMemberQuery } => BotOnMyChatMemberQueryReceived(myChatMemberQuery, cancellationToken),
            //{ EditedMessage: { } message } => BotOnMessageReceived(message, cancellationToken),
            //{ InlineQuery: { } inlineQuery } => BotOnInlineQueryReceived(inlineQuery, cancellationToken),
            //{ ChosenInlineResult: { } chosenInlineResult } => BotOnChosenInlineResultReceived(chosenInlineResult, cancellationToken),
            _ => UnknownUpdateHandlerAsync(update, cancellationToken)
        };

        await handler;
    }

    private async Task BotOnMyChatMemberQueryReceived(ChatMemberUpdated myChatMemberQuery, CancellationToken cancellationToken)
    {
        var me = await _botClient.GetMeAsync();
        //-1001896971722
        if(myChatMemberQuery.Chat.Type == ChatType.Channel && myChatMemberQuery.NewChatMember.User.Id == me.Id)
        {
            var channelId = myChatMemberQuery.Chat.Id;
            await _telegramMessageService.ReceiveNewChannel(channelId, myChatMemberQuery.From.Id, myChatMemberQuery.NewChatMember.Status);
        }
        //throw new NotImplementedException();
    }

    private async Task BotOnMessageReceived(Message message, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Receive message type: {MessageType}", message.Type);
        //if (message.Text is not { } messageText)
        //    return;

        await _telegramMessageService.ReceiveMessage(message);
    }

    private async Task BotOnCallbackQueryReceived(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received inline keyboard callback from: {CallbackQueryId}", callbackQuery.Id);

        await _telegramMessageService.ReceiveCallback(callbackQuery);
    }


    private Task UnknownUpdateHandlerAsync(Update update, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Unknown update type: {UpdateType}", update.Type);
        return Task.CompletedTask;
    }

    public async Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        _logger.LogInformation("HandleError: {ErrorMessage}", ErrorMessage);

        // Cooldown in case of network connection error
        if (exception is RequestException)
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
    }
}