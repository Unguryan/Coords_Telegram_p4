using CoordsTelegram.App.Commands.AddChatIdToAuthLink;
using CoordsTelegram.Domain.Errors;
using FluentValidation;

namespace CoordsTelegram.App.Validators
{
    public class AddChatIdToAuthLinkCommandValidator : AbstractValidator<AddChatIdToAuthLinkCommand>
    {
        public AddChatIdToAuthLinkCommandValidator()
        {
            RuleFor(x => x.ChatId)
                .NotEmpty().WithMessage(AddChatIdToAuthLinkCommandErrors.ChatIdRequired);

            RuleFor(x => x.Key)
                 .NotEmpty().WithMessage(AddChatIdToAuthLinkCommandErrors.KeyRequired);
        }
    }
}
