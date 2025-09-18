using ApplicationCore.Contracts;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace OrderAPI.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService service)
        {

            _orderService = service;
            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAll()
        {
            await _orderService.GetAllAsync();
            return Ok();
        }


        [HttpPost]
        public async Task<ActionResult<Order>> Create([FromBody] Order order)
        {
            Order created = await _orderService.CreateAsync(order);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Order>> GetById(int id)
        {
            Order o = await _orderService.GetAsync(id);

            if (o == null) { return NotFound(); }

            return Ok(o);

        }

        [HttpGet("byCustomer/{customerId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetByCustomerId(string customerId)
        {
            await _orderService.GetByCustomerIdAsync(customerId);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Order order)
        {
            await _orderService.UpdateAsync(id, order);
            return NoContent();
        }

    }
}
