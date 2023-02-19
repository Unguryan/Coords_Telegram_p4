using CoordsTelegram.Domain.Options;
using CoordsTelegram.RabbitMQ.Consumers;
using MassTransit;

namespace CoordsTelegram.RabbitMQ.Extensions
{
    public static class RabbitMQExtensions
    {
        public static void AddReceiveEndpointCreatedCoord(this IRabbitMqBusFactoryConfigurator cfg,
                                                               IBusRegistrationContext context,
                                                               RabbitMQOptions rabbitSection)
        {
            cfg.ReceiveEndpoint(rabbitSection.CreatedCoordQueue, config =>
            {
                config.ConfigureConsumeTopology = false;
                config.Bind($"{rabbitSection.CreatedCoordQueue}_Exchange");
                //config.Bind<CreatedCoordEvent>();
                config.ConfigureConsumer<CreatedCoordConsumer>(context);
            });
        }
    }
}
