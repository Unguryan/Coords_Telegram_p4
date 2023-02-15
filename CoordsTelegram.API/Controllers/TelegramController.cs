using AutoMapper;
using CoordsTelegram.App.Commands.CreateAuthLink;
using CoordsTelegram.App.Queries.GetAuthLink;
using CoordsTelegram.App.Queries.GetTokenInfo;
using CoordsTelegram.Domain.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoordsTelegram.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TelegramController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TelegramController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("auth_link")]
        public async Task<CreateAuthLinkViewModel> GetAuthLink()
        {
            var result = await _mediator.Send(new CreateAuthLinkCommand());

            return _mapper.Map<CreateAuthLinkViewModel>(result);
        }

        [HttpGet]
        [Route("auth_link/{key}")]
        public async Task<AuthLinkExpiredViewModel> GetAuthLinkExpired(string key)
        {
            var result = await _mediator.Send(new GetAuthLinkQuery(key));

            return _mapper.Map<AuthLinkExpiredViewModel>(result.AuthLink);
        }

        [HttpGet]
        [Route("token/{token}")]
        public async Task<GetTokenInfoViewModel> GetTokenInfo(string token)
        {
            var result = await _mediator.Send(new GetTokenInfoQuery(token));

            return _mapper.Map<GetTokenInfoViewModel>(result);
        }
    }
}
