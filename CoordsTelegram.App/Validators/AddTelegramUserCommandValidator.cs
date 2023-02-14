using CoordsTelegram.App.Commands.AddTelegramUser;
using CoordsTelegram.Domain.Errors;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CoordsTelegram.App.Validators
{
    public class AddTelegramUserCommandValidator : AbstractValidator<AddTelegramUserCommand>
    {
        public AddTelegramUserCommandValidator()
        {
            RuleFor(x => x.ChatId)
                .NotEmpty().WithMessage(AddTelegramUserCommandErrors.ChatIdRequired);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .NotNull().WithMessage(AddTelegramUserCommandErrors.PhoneNumberRequired)
                .Length(13).WithMessage(AddTelegramUserCommandErrors.PhoneNumberLength)
                .Matches(new Regex(@"\d{3}-\d{3}-\d{2}-\d{2}$")).WithMessage(AddTelegramUserCommandErrors.PhoneNumberInvalid);
        }
    }
}
