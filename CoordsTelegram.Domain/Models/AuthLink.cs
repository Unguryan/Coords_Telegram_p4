namespace CoordsTelegram.Domain.Models
{
    public class AuthLink
    {
        public string Key { get; set; }

        public string ChatId { get; set; }

        public string PhoneNumber { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }

        public DateTime Expired { get; set; }

        public bool IsExpired => DateTime.Now > Expired;
    }
}
