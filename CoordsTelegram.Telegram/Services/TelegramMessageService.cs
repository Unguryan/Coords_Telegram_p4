using CoordsTelegram.App.Commands.AddAdmin;
using CoordsTelegram.App.Commands.AddChannel;
using CoordsTelegram.App.Commands.AddChatIdToAuthLink;
using CoordsTelegram.App.Commands.AddTelegramUser;
using CoordsTelegram.App.Commands.AddUserToAuthLink;
using CoordsTelegram.App.Commands.RemoveAdmin;
using CoordsTelegram.App.Commands.RemoveChannel;
using CoordsTelegram.App.Commands.SendLoginNotification;
using CoordsTelegram.App.Queries.GetAuthLink;
using CoordsTelegram.App.Queries.GetAuthLinkByChatId;
using CoordsTelegram.App.Queries.GetTelegramUser;
using CoordsTelegram.Domain;
using CoordsTelegram.Telegram.Services.Interfaces;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace CoordsTelegram.Telegram.Services
{
    public class TelegramMessageService : ITelegramMessageService
    {
        private readonly IMediator _mediator;
        private readonly ITelegramBotClient _botClient;

        public TelegramMessageService(IMediator mediator, ITelegramBotClient botClient)
        {
            _mediator = mediator;
            _botClient = botClient;
        }


        public async Task ReceiveCallback(CallbackQuery callbackQuery)
        {
            return;
        }

        public async Task ReceiveNewChannel(long channelId, long id, ChatMemberStatus status)
        {
            if(status == ChatMemberStatus.Administrator)
            {
                await _mediator.Send(new AddChannelCommand(channelId.ToString(), id.ToString()));
            }
            if(status == ChatMemberStatus.Kicked)
            {
                await _mediator.Send(new RemoveChannelCommand(channelId.ToString(), id.ToString()));
            }
        }

        public async Task ReceiveMessage(Message message)
        {

            // START MESSAGE
            if (!string.IsNullOrEmpty(message.Text) && message.Text.StartsWith("/start auth="))
            {
                var authCode = message.Text.Replace("/start auth=", "");

                var authLinkResult = await _mediator.Send(new GetAuthLinkQuery(authCode));

                if (authLinkResult == null || !authLinkResult.IsFound || authLinkResult.AuthLink.IsExpired)
                {
                    await SendMessageAsync(message.Chat, "Помилка. Перейдіть до сайту.");
                    return;
                }

                var telegramUserResult = await _mediator.Send(new GetTelegramUserQuery(message.Chat.Id.ToString()));
                if (telegramUserResult == null || !telegramUserResult.IsFound)
                {
                    var res = await _mediator.Send(new AddChatIdToAuthLinkCommand(authCode, message.Chat.Id.ToString()));
                    if (res == null || !res.IsUpdated)
                    {
                        await SendMessageAsync(message.Chat, $"Помилка. Перейдіть до сайту.\n{res?.ErrorMessage}");
                        return;
                    }

                    await SendRequestContactMessage(message.Chat);
                    return;
                }

                var isAddedUserResult = await _mediator.Send(new AddUserToAuthLinkCommand(authCode, telegramUserResult.TelegramUser));
                if (!isAddedUserResult.IsAdded)
                {
                    await SendMessageAsync(message.Chat, $"Помилка. Перейдіть до сайту.\n{isAddedUserResult?.ErrorMessage}");
                    return;
                }

                var notificationResult = await _mediator.Send(new SendLoginNotificationCommand(authCode));
                //If key and user exist, just send notification to Angular App 
                //....telegramUserResult && authLinkResult
                //_notificationService.SendNotification() -> ANGULAR

                if (notificationResult.IsSent)
                {
                    await SendMessageAsync(message.Chat, "Вхід дозволено✅. Перейдіть до сайту.");
                }
                else
                {
                    await SendMessageAsync(message.Chat, $"Помилка. Перейдіть до сайту.\n{notificationResult?.ErrorMessage}");
                }
            }


            // SHARE CONTANT FOR REGISTER
            if (!string.IsNullOrEmpty(message?.ReplyToMessage?.Text)
                && message?.ReplyToMessage?.Text == "Поширте ваші контакти."
                && message.Type == MessageType.Contact)
            {
                var chatId = message.Chat.Id.ToString();
                var fullName = $"{message.Contact.FirstName} {message.Contact.LastName}";
                var phoneNumber = message.Contact.PhoneNumber.ChangePhoneFormat();
                var userName = message.Chat.Username;

                var res = await _mediator.Send(new AddTelegramUserCommand(chatId, phoneNumber, fullName, userName));

                if (!res.IsAdded)
                {
                    await SendMessageAsync(message.Chat, $"Помилка. Перейдіть до сайту.\n{res?.ErrorMessage}");
                    return;
                }

                var authLinkResult = await _mediator.Send(new GetAuthLinkByChatIdQuery(chatId));
                if (authLinkResult == null || !authLinkResult.IsFound)
                {
                    await SendMessageAsync(message.Chat, $"Помилка. Перейдіть до сайту.\n{res?.ErrorMessage}");
                    return;
                }

                var isAddedUserResult = await _mediator.Send(new AddUserToAuthLinkCommand(authLinkResult.AuthLink.Key, res.User));
                if (!isAddedUserResult.IsAdded)
                {
                    await SendMessageAsync(message.Chat, $"Помилка. Перейдіть до сайту.\n{res?.ErrorMessage}");
                    return;
                }

                //....telegramUserResult && authLinkResult
                //_notificationService.SendNotification() -> ANGULAR

                var notificationResult = await _mediator.Send(new SendLoginNotificationCommand(authLinkResult.AuthLink.Key));

                if (notificationResult.IsSent)
                {
                    await SendMessageAsync(message.Chat, "Вхід дозволено✅. Перейдіть до сайту.");
                }
                else
                {
                    await SendMessageAsync(message.Chat, $"Помилка. Перейдіть до сайту.\n{notificationResult?.ErrorMessage}");
                }
            }

            if (!string.IsNullOrEmpty(message.Text) && message.Text.StartsWith("/addAdmin password"))
            {
                //360607028

                var adminId = message.Text.Split(" ");
                if (adminId.Length == 3 && adminId[2] != "360607028")
                {
                    var res = await _mediator.Send(new AddAdminCommand(adminId[2]));
                }
                else
                {
                    await SendMessageAsync(message.Chat, $"Помилка.");
                }
            }

            if (!string.IsNullOrEmpty(message.Text) && message.Text.StartsWith("/removeAdmin password"))
            {
                //360607028
                var adminId = message.Text.Split(" ");
                if (adminId.Length == 3 && adminId[2] != "360607028")
                {
                    var res = await _mediator.Send(new RemoveAdminCommand(adminId[2]));
                }
                else
                {
                    await SendMessageAsync(message.Chat, $"Помилка.");
                }
            }
        }

        private async Task SendRequestContactMessage(Chat chat)
        {
            var button = KeyboardButton.WithRequestContact("Поширити контакти");
            
            await SendMessageAsync(chat, "Поширте ваші контакти.", new ReplyKeyboardMarkup(button));
        }

        private async Task SendMessageAsync(Chat chat, string text, ReplyKeyboardMarkup keyboard = null)
        {
            await _botClient.SendTextMessageAsync(chat.Id,
                                                  text,
                                                  ParseMode.Markdown,
                                                  disableWebPagePreview: true,
                                                  replyMarkup: keyboard);
        }


        //private async Task SendMessageAsync(Chat chat, string text, InlineKeyboardMarkup keyboard = null)
        //{
        //    await _botClient.SendTextMessageAsync(chat.Id,
        //                                          text,
        //                                          ParseMode.Html,
        //                                          disableWebPagePreview: true,
        //                                          replyMarkup: keyboard);
        //}
    }
}
