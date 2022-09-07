using dapr.eshop.order.Models;

namespace dapr.eshop.order.Infrastructure
{
    public interface IOrderRepository
    {
        Order GetOrderById(Guid orderId);
        IEnumerable<Order> GetAllOrders();
        Task<int> CreateAsync(Order entity);
        Task<int> UpdateAsync(Order entity);
    }
}
