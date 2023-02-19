namespace CoordsTelegram.Domain.Options
{
    public class RabbitMQConfiguration
    {
        public static string SectionName => "MTRabbit";

        public string Username { get; set; }
        public string Password { get; set; }
        public string Uri { get; set; }
    }
}
