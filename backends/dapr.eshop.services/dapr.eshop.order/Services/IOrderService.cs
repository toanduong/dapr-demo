namespace dapr.eshop.order.Services
{
    public interface IOrderService
    {
        Task<string> GetConfigs();
    }
}
