using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;
        public UserService(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public async Task<User> GetUser()
        {
            var userId = _dbContext.Orders
                .GroupBy(order => order.UserId)
                .OrderByDescending(group => group.Count())
                .Select(group => group.Key)
                .FirstOrDefault();

            return await _dbContext.Users.Where(user => user.Id == userId).FirstAsync();
        }

        public async Task<List<User>> GetUsers()
        {
            return await _dbContext.Users.Where(user => user.Status == Enums.UserStatus.Inactive)
                .ToListAsync();

        }
    }
}
