using AutoMapper;
using CoordsTelegram.App.Services;
using MediatR;

namespace CoordsTelegram.App.Commands.CreateAuthLink
{
    public class CreateAuthLinkCommandHandler : IRequestHandler<CreateAuthLinkCommand, CreateAuthLinkCommandResult>
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public CreateAuthLinkCommandHandler(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<CreateAuthLinkCommandResult> Handle(CreateAuthLinkCommand request, CancellationToken cancellationToken)
        {
            var result = await _authService.CreateAuthLinkAsync(cancellationToken);

            return _mapper.Map<CreateAuthLinkCommandResult>(result);
        }
    }
}
