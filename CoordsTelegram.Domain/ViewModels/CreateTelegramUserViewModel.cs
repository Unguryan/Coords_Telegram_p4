namespace CoordsTelegram.Domain.ViewModels
{
    public record CreateTelegramUserViewModel(string ChatId, string PhoneNumber, string FullName, string UserName);
}
