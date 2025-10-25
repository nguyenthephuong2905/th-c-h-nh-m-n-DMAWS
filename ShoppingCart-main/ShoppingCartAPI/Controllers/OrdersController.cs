using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCartAPI.Data;
using ShoppingCartAPI.Models;

namespace ShoppingCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderSystemContext _context;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(OrderSystemContext context, ILogger<OrdersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderTbl>>> GetOrders()
        {
            try
            {
                var orders = await _context.OrderTbl
                    .OrderByDescending(o => o.CreatedDate)
                    .ToListAsync();
                
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting orders");
                return StatusCode(500, "An error occurred while retrieving orders");
            }
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderTbl>> GetOrder(int id)
        {
            try
            {
                var order = await _context.OrderTbl.FindAsync(id);

                if (order == null)
                {
                    return NotFound(new { message = $"Order with ID {id} not found" });
                }

                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting order {OrderId}", id);
                return StatusCode(500, "An error occurred while retrieving the order");
            }
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<OrderTbl>> CreateOrder([FromBody] CreateOrderDto orderDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var order = new OrderTbl
                {
                    ItemCode = orderDto.ItemCode,
                    ItemName = orderDto.ItemName,
                    ItemQty = orderDto.ItemQty,
                    OrderDelivery = orderDto.OrderDelivery,
                    OrderAddress = orderDto.OrderAddress,
                    PhoneNumber = orderDto.PhoneNumber,
                    CreatedDate = DateTime.Now
                };

                _context.OrderTbl.Add(order);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Order created successfully with ID: {OrderId}", order.OrderId);

                return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating order");
                return StatusCode(500, "An error occurred while creating the order");
            }
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderDto orderDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var order = await _context.OrderTbl.FindAsync(id);

                if (order == null)
                {
                    return NotFound(new { message = $"Order with ID {id} not found" });
                }

                // Only update delivery time and address as per requirements
                order.OrderDelivery = orderDto.OrderDelivery;
                order.OrderAddress = orderDto.OrderAddress;
                order.UpdatedDate = DateTime.Now;

                _context.Entry(order).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                _logger.LogInformation("Order {OrderId} updated successfully", id);

                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating order {OrderId}", id);
                return StatusCode(500, "An error occurred while updating the order");
            }
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                var order = await _context.OrderTbl.FindAsync(id);
                
                if (order == null)
                {
                    return NotFound(new { message = $"Order with ID {id} not found" });
                }

                _context.OrderTbl.Remove(order);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Order {OrderId} deleted successfully", id);

                return Ok(new { message = "Order deleted successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting order {OrderId}", id);
                return StatusCode(500, "An error occurred while deleting the order");
            }
        }

        // GET: api/Orders/stats
        [HttpGet("stats")]
        public async Task<ActionResult> GetOrderStatistics()
        {
            try
            {
                var stats = await _context.OrderTbl
                    .GroupBy(o => new { o.CreatedDate.Year, o.CreatedDate.Month })
                    .Select(g => new
                    {
                        Month = g.Key.Month,
                        Year = g.Key.Year,
                        TotalOrders = g.Count(),
                        TotalItems = g.Sum(o => o.ItemQty)
                    })
                    .OrderBy(s => s.Year)
                    .ThenBy(s => s.Month)
                    .ToListAsync();

                return Ok(stats);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting order statistics");
                return StatusCode(500, "An error occurred while retrieving statistics");
            }
        }
    }
}

