using dapr.eshop.order.Models;

namespace dapr.eshop.order.Infrastructure
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _dbContext;
        public OrderRepository(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Order GetOrderById(Guid orderId)
        {
            return _dbContext.Orders.FirstOrDefault(o => o.OrderId == orderId);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _dbContext.Orders.OrderByDescending(o => o.CreatedDate).AsEnumerable();
        }

        public async Task<int> CreateAsync(Order entity)
        {
            _dbContext.Orders.Add(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(Order entity)
        {
            _dbContext.Orders.Update(entity);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
