using CoordsTelegram.App.Repositories;
using CoordsTelegram.EF_Core.Context;
using CoordsTelegram.EF_Core.Dbo;
using Microsoft.EntityFrameworkCore;

namespace CoordsTelegram.EF_Core.Services
{
    internal class TelegramChatRepository : ITelegramChatRepository
    {
        private readonly TelegramChatContext _context;

        public TelegramChatRepository(TelegramChatContext context)
        {
            _context = context;
        }

        public async Task<bool> IsUserAdmin(string idUser)
        {
            var user = await _context.Admins.FirstOrDefaultAsync(x => x.Key == idUser);
            return user != null;
        }

        public async Task<bool> AddAdmin(string idChat)
        {
            if(_context.Admins.Any(x => x.Key == idChat))
            {
                return false;
            }

            await _context.Admins.AddAsync(new AdminDbo() { Key = idChat});
            var res = await _context.SaveChangesAsync();

            return res > 0;
        }

        public async Task<bool> AddChannel(string idChannel)
        {
            if (_context.Channels.Any(x => x.Key == idChannel))
            {
                return false;
            }

            await _context.Channels.AddAsync(new ChannelDbo() { Key = idChannel});
            var res = await _context.SaveChangesAsync();

            return res > 0;
        }

        public async Task<List<string>> GetChannels()
        {
            return await _context.Channels.Select(x => x.Key).ToListAsync();
        }

        

        public async Task<bool> RemoveAdmin(string idChat)
        {
            var item = await _context.Admins.FirstOrDefaultAsync(x => x.Key == idChat);
            if (item == null)
            {
                return false;
            }

            _context.Remove(item);
            var res = await _context.SaveChangesAsync();

            return res > 0;
        }

        public async Task<bool> RemoveChannel(string idChannel)
        {
            var item = await _context.Channels.FirstOrDefaultAsync(x => x.Key == idChannel);
            if (item == null)
            {
                return false;
            }

            _context.Remove(item);
            var res = await _context.SaveChangesAsync();

            return res > 0;
        }
    }
}
