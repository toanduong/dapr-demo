using dapr.eshop.order.Common;
using System.Text.Json.Serialization;

namespace dapr.eshop.order.Models;

public class OrderTracking
{
    [JsonPropertyName("orderId")]
    public Guid OrderId { get; private set; }

    [JsonPropertyName("state")]
    public OrderState State { get; private set; }

    [JsonPropertyName("updatedDate")]
    public DateTime UpdatedDate { get; private set; }

    private OrderTracking(Guid orderId, OrderState state)
    {
        OrderId = orderId;
        State = state;
    }

    public static OrderTracking AddTracking(Guid orderId, OrderState state)
    {
        var orderTracking = new OrderTracking(orderId, state);
        return orderTracking;
    }
}