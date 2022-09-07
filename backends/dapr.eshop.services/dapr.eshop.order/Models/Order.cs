using dapr.eshop.order.Common;

namespace dapr.eshop.order.Models;

public class Order
{
    public Guid OrderId { get; private set; }

    public string StoreId { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public List<OrderItem> OrderItems { get; private set; }

    public OrderSummary OrderSummary { get; private set; }

    public List<OrderTracking> OrderTrackings { get; private set; }

    public DateTime CreatedDate { get; private set; }

    private Order(string storeId, string firstName, string lastName)
    {
        OrderId = Guid.NewGuid();
        StoreId = storeId;
        FirstName = firstName;
        LastName = lastName;
        CreatedDate = new DateTime();
    }

    public static Order Create(string storeId, string firstName, string lastName, IEnumerable<OrderItem> items)
    {
        var order = new Order(storeId, firstName, lastName);

        order.OrderItems = items.ToList();
        order.OrderSummary = OrderSummary.OrderCreated(order.OrderId, order.OrderItems.Sum(o => o.Quantity * o.UnitPrice));

        order.OrderTrackings = new List<OrderTracking>
        {
            OrderTracking.AddTracking(order.OrderId, OrderState.Pending)
        };

        return order;
    }

    public static Order OrderPaymentFailed(Order order)
    {
        order.OrderSummary = OrderSummary.OrderPaymentFailed(order.OrderSummary);
        order.OrderTrackings.Add(OrderTracking.AddTracking(order.OrderId, OrderState.PaymentFailed));

        return order;
    }

    public static Order OrderPaid(Order order)
    {
        order.OrderSummary = OrderSummary.OrderPaid(order.OrderSummary);
        order.OrderTrackings.Add(OrderTracking.AddTracking(order.OrderId, OrderState.Paid));

        return order;
    }

    public static Order OrderShipping(Order order)
    {
        order.OrderSummary = OrderSummary.OrderShipping(order.OrderSummary);
        order.OrderTrackings.Add(OrderTracking.AddTracking(order.OrderId, OrderState.Shipping));

        return order;
    }

    public Order OrderCompleted(Order order)
    {
        order.OrderSummary = OrderSummary.OrderComplete(order.OrderSummary);
        order.OrderTrackings.Add(OrderTracking.AddTracking(order.OrderId, OrderState.Completed));

        return order;
    }
}