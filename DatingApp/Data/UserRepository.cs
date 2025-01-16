using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApp.DTO;
using DatingApp.Entities;
using DatingApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Data
{
    public class UserRepository(DataContext dataContext, IMapper mapper) : IUserRepository
    {
        public async Task<MemberDto?> GetMemberAsync(string username)
        {
            return await dataContext.Users
                .Where(x => x.userName == username)
                .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await dataContext.Users
                .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<AppUser?> GetUserByIdAsync(int id)
        {
            return await dataContext.Users.FindAsync(id);
        }

        public Task<AppUser?> GetUserByUserNameAsync(string userName)
        {
            return dataContext.Users
                .Include(p => p.Photos)
                .SingleOrDefaultAsync(x => x.userName == userName);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await dataContext.Users
                .Include(p => p.Photos)
                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await dataContext.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            dataContext.Entry(user).State = EntityState.Modified;
        }
    }
}
