namespace CoordsTelegram.Domain.ViewModels
{
    public record TokenInfoViewModel(string Key, string ChatId, string PhoneNumber, string FullName, string UserName, DateTime Expired)
    {
        public bool IsExpired => DateTime.Now > Expired;   
    }
}
