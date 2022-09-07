using dapr.eshop.order.Models;

namespace dapr.eshop.order.ViewModels
{
    public class OrderViewModel
    {
        public string StoreId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<OrderItemViewModel> OrderItems { get; set; }

        public Address Address { get; set; }
    }
}
