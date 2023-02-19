namespace CoordsTelegram.Domain.Options
{
    public class RabbitMQOptions
    {
        public static string SectionName => "MTRabbitOptions";

        public string CreateCoordQueue { get; set; }
        public string CreatedCoordQueue { get; set; }
    }
}
