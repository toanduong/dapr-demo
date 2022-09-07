using System.Text.Json.Serialization;

namespace dapr.eshop.order.Models;

public class OrderItem
{
    public Guid OrderItemId { get; private set; }

    public int ProductId { get; private set; }

    public decimal UnitPrice { get; private set; }

    public int Quantity { get; private set; }

    public OrderItem(int productId, decimal unitPrice, int quantity)
    {
        OrderItemId = Guid.NewGuid();
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
}
