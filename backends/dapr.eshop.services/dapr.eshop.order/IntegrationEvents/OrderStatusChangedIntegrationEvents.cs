using dapr.eshop.order.Models;

namespace dapr.eshop.order.IntegrationEvents;

public record ShipmentIntegrationEvent(
    Guid OrderId,
    Address Address
    );

public record OrderPaidIntegrationEvent(
    Guid OrderId,
    string Description);

public record OrderPaymentFailureIntegrationEvent(
    Guid OrderId,
    string Description);

public record OrderShippingIntegrationEvent(
    Guid OrderId,
    string Description);

public record OrderCompletedIntegrationEvent(
    Guid OrderId,
    string Description);