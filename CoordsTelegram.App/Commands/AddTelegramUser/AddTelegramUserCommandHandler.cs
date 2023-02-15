using AutoMapper;
using CoordsTelegram.App.Commands.AddChatIdToAuthLink;
using CoordsTelegram.App.Services;
using CoordsTelegram.Domain.ViewModels;
using FluentValidation;
using MediatR;

namespace CoordsTelegram.App.Commands.AddTelegramUser
{
    public class AddTelegramUserCommandHandler : IRequestHandler<AddTelegramUserCommand, AddTelegramUserCommandResult>
    {
        private readonly ITelegramUserService _telegramUserService;
        private readonly IValidator<AddTelegramUserCommand> _validator;
        private readonly IMapper _mapper;

        public AddTelegramUserCommandHandler(ITelegramUserService telegramUserService, 
                                             IValidator<AddTelegramUserCommand> validator,
                                             IMapper mapper)
        {
            _telegramUserService = telegramUserService;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<AddTelegramUserCommandResult> Handle(AddTelegramUserCommand request, CancellationToken cancellationToken)
        {
            var validateRes = await _validator.ValidateAsync(request);

            if (!validateRes.IsValid)
            {
                var errorsStr = string.Empty;
                validateRes.Errors.ForEach(e => errorsStr += $"{e}\n");
                return new AddTelegramUserCommandResult(false, null, errorsStr);
            }

            var result = await _telegramUserService.AddTelegramUserAsync(_mapper.Map<CreateTelegramUserViewModel>(request));

            return new AddTelegramUserCommandResult(result.IsAdded, result.User);
        }
    }
}
