using dapr.eshop.order.Infrastructure;
using dapr.eshop.order.IntegrationEvents;
using dapr.eshop.order.Models;
using dapr.eshop.order.ViewModels;
using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace dapr.eshop.order.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private const string _pubSub = "eshop.pubsub";
        private const string _shipmentTopic = "shipments";
        private const string _orderTopic = "orders";
        private readonly IOrderRepository _orderRepository;
        private readonly DaprClient _daprClient;

        public OrderController(ILogger<OrderController> logger, 
                                IOrderRepository orderRepository,
                                DaprClient daprClient)
        {
            _logger = logger;
            _orderRepository = orderRepository;
            _daprClient = daprClient;
        }

        [HttpGet()]
        public async Task<IActionResult> GetOrders()
        {
            var orders = _orderRepository.GetAllOrders();
            await Task.CompletedTask;

            return Ok(orders);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrder(Guid orderId)
        {
            var order = _orderRepository.GetOrderById(orderId);
            await Task.CompletedTask;

            return Ok(order);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateOrder(OrderViewModel order)
        {
            var model = Order.Create(order.StoreId, 
                                    order.FirstName, 
                                    order.LastName, 
                                    order.OrderItems.Select(i => new OrderItem(i.ProductId, i.UnitPrice, i.Quantity)));
            await _orderRepository.CreateAsync(model);

            // publish a message to shipment service
            var payload = JsonConvert.SerializeObject(new ShipmentIntegrationEvent(model.OrderId, order.Address));
            await _daprClient.PublishEventAsync(_pubSub, _shipmentTopic, payload);

            return new OkObjectResult(model.OrderId);
        }

        [Topic(_pubSub, _orderTopic)]
        [Route("{orderId}/shipping")]
        [HttpPut()]
        public async Task<IActionResult> ShippingOrderAsync(Guid orderId)
        {
            var model = _orderRepository.GetOrderById(orderId);
            if (model == null)
                return new BadRequestObjectResult("Order was not found");

            var order = Order.OrderShipping(model);
            await _orderRepository.UpdateAsync(order);

            return new OkResult();
        }
    }
}