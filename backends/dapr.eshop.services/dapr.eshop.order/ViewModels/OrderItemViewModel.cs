using System.Text.Json.Serialization;

namespace dapr.eshop.order.ViewModels
{
    public class OrderItemViewModel
    {
        public int ProductId { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }
    }
}
