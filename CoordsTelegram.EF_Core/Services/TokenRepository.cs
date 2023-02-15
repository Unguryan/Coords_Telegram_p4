using CoordsTelegram.App.Repositories;
using CoordsTelegram.Domain.ViewModels;
using CoordsTelegram.EF_Core.Context;
using CoordsTelegram.EF_Core.Dbo;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CoordsTelegram.Domain.Models;

namespace CoordsTelegram.EF_Core.Services
{
    public class TokenRepository : ITokenRepository
    {
        private readonly TokenContext _context;
        private readonly IMapper _mapper;

        public TokenRepository(TokenContext tokenContext, IMapper mapper)
        {
            _context = tokenContext;
            _mapper = mapper;
        }

        public async Task<bool> AddTokenAsync(AddTokenViewModel request, DateTime expired)
        {
            var addedEntity = await _context.Tokens.AddAsync(
                new TokenDbo()
                {
                    Key = request.Key,
                    Expired = expired,
                    ChatId = request.ChatId,
                    FullName = request.FullName,
                    PhoneNumber = request.PhoneNumber,
                    UserName = request.UserName
                });
            var res = await _context.SaveChangesAsync();

            return res > 0;
        }

        public async Task<TokenInfoViewModel> GetTokenInfoAsync(string key)
        {
            var entity = await _context.Tokens.FirstOrDefaultAsync(x => x.Key == key);
            return _mapper.Map<TokenInfoViewModel>(entity);
        }

        public async Task<List<TokenInfoViewModel>> GetTokensAsync()
        {
            var result = new List<TokenInfoViewModel>();
            await _context.Tokens.ForEachAsync(t => result.Add(_mapper.Map<TokenInfoViewModel>(t)));
            return result;
        }

        public async Task<bool> RemoveByKeyAsync(string key)
        {
            var entity = await _context.Tokens.FirstOrDefaultAsync(x => x.Key == key);

            if (entity == null)
            {
                return false;
            }

            _context.Remove(entity);
            var res = await _context.SaveChangesAsync();

            return res > 0;
        }

        public async Task<int> RemoveRangeByKeyAsync(List<string> keys)
        {
            var entityToRemove = new List<TokenDbo>();

            foreach (var key in keys)
            {
                var entity = await _context.Tokens.FirstOrDefaultAsync(x => x.Key == key);

                if (entity == null)
                {
                    continue;
                }

                entityToRemove.Add(entity);
            }

            _context.RemoveRange(entityToRemove);
            var res = await _context.SaveChangesAsync();

            return res;
        }
    }
}
