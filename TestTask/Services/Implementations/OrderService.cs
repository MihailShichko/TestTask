using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _dbContext;
        public OrderService(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public async Task<Order> GetOrder()
        {
            return await _dbContext.Orders.OrderByDescending(order => order.Price * order.Quantity)
                .FirstAsync();
        }

        public async Task<List<Order>> GetOrders()
        {
            return await _dbContext.Orders.Where(order => order.Quantity > 10)
                .ToListAsync();
        }
    }
}
