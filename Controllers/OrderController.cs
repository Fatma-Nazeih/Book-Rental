using BookOrder.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookOrder.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private static int _orderCounter = 1;

        [HttpPost]
        public IActionResult CreateOrder([FromBody] Order order)
        {
            order.OrderId = _orderCounter++;
            order.OrderDate = DateTime.UtcNow;
            InMemoryDatabase.Orders.Add(order);
            return CreatedAtAction(nameof(GetOrdersByUser), new { userId = order.UserId }, order);
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetOrdersByUser(int userId)
        {
            var userOrders = InMemoryDatabase.Orders.Where(o => o.UserId == userId).ToList();
            return Ok(userOrders);
        }
    }


}
