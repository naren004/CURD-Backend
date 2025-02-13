using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using WebApiMongoDbDemo.Data;
using WebApiMongoDbDemo.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiMongoDbDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMongoCollection<Customer> _customers;

        public CustomerController(MongoDbService mongoDbService)
        {
            _customers = mongoDbService.Database.GetCollection<Customer>("Customers");
        }

        // GET: api/customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Get()
        {
            var customers = await _customers.Find(Builders<Customer>.Filter.Empty).ToListAsync();
            return Ok(customers);
        }

        // GET: api/customer/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetById(string id)
        {
            var customer = await _customers.Find(x => x.Id == id).FirstOrDefaultAsync();
            return customer is not null ? Ok(customer) : NotFound(new { message = "Customer not found" });
        }

        // POST: api/customer
        [HttpPost]
        public async Task<ActionResult<Customer>> Post([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest(new { message = "Invalid customer data" });
            }

            await _customers.InsertOneAsync(customer);
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }

        // PUT: api/customer
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Customer customer)
        {
            if (customer == null || string.IsNullOrEmpty(customer.Id))
            {
                return BadRequest(new { message = "Invalid customer data" });
            }

            var filter = Builders<Customer>.Filter.Eq(x => x.Id, customer.Id);
            var result = await _customers.ReplaceOneAsync(filter, customer);

            return result.ModifiedCount > 0 ? Ok(new { message = "Customer updated successfully" }) : NotFound(new { message = "Customer not found" });
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await _customers.DeleteOneAsync(x => x.Id == id);
            return result.DeletedCount > 0 ? Ok() : NotFound();
        }

    }
}
