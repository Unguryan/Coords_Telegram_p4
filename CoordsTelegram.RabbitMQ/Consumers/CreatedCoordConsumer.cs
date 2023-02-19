using AutoMapper;
using Coord.Domain.Events;
using CoordsTelegram.App.Commands.CreatedCoord;
using MassTransit;
using MediatR;

namespace CoordsTelegram.RabbitMQ.Consumers
{
    public class CreatedCoordConsumer : IConsumer<CreatedCoordEvent>
    {
        private readonly IMediator _mediator;

        public CreatedCoordConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<CreatedCoordEvent> context)
        {
            if (context.Message.IsCreated)
            {
                await _mediator.Send(new CreatedCoordCommand(context.Message.Data));
            }
        }
    }
}
