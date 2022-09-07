using dapr.eshop.order.Common;
using System.Text.Json.Serialization;

namespace dapr.eshop.order.Models;

public class OrderSummary
{
    [JsonPropertyName("orderId")]
    public Guid OrderId { get; private set; }

    [JsonPropertyName("orderDate")]
    public DateTime OrderDate { get; private set; }

    public OrderState State { get; private set; }

    [JsonPropertyName("orderCompletedDate")]
    public DateTime? OrderCompletedDate { get; private set; }

    [JsonPropertyName("orderTotal")]
    public decimal OrderTotal { get; private set; }

    private OrderSummary(Guid orderId, OrderState state, decimal orderTotal)
    {
        OrderId = orderId;
        State = state;
        OrderTotal = orderTotal;
    }

    public static OrderSummary OrderCreated(Guid orderId, decimal orderTotal)
    {
        var orderSummary = new OrderSummary(orderId, OrderState.Pending, orderTotal);

        return orderSummary;
    }

    public static OrderSummary OrderPaymentFailed(OrderSummary orderSummary)
    {
        orderSummary.State = OrderState.PaymentFailed;

        return orderSummary;
    }

    public static OrderSummary OrderShipping(OrderSummary orderSummary)
    {
        orderSummary.State = OrderState.Shipping;

        return orderSummary;
    }

    public static OrderSummary OrderPaid(OrderSummary orderSummary)
    {
        orderSummary.State = OrderState.Paid;

        return orderSummary;
    }

    public static OrderSummary OrderComplete(OrderSummary orderSummary)
    {
        orderSummary.State = OrderState.Completed;

        return orderSummary;
    }
}