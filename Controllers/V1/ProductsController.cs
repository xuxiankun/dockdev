using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eCommerceApiProducts.Models;
using System.Net;
using System.Threading;
using MediatR;

namespace eCommerceApiProducts.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]" )]
    [ApiVersion("1.0")]
  
    public class ProductsController : ControllerBase
    {
        private readonly ProductsDbContext _context;

        private readonly IMediator _mediator;

        private readonly SomeRepository _repository;

        public ProductsController(ProductsDbContext context, SomeRepository repository, IMediator mediator)
        {
            _context = context;
            _repository = repository;
            _mediator = mediator;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var query = new GetAllProductsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        private async Task SomeBackgroundThingAsync(CancellationToken token)
        {
            var result = await _repository.GetValueAsync(2);
            await Task.Delay(10000, token);
            //throw new InvalidOperationException();
            Console.WriteLine($"The number is {result}");
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var query = new GetProductByIdQuery(id);
            var result = await _mediator.Send(query);
            //var product = await _context.Products.FindAsync(id);
            return result != null ? Ok(result) : NotFound();
             
        }

        [HttpGet("hostname")]
        public async Task<string> GetHostname(CancellationToken token)
        {
            string hostName = Dns.GetHostName();
            await SomeBackgroundThingAsync( token);
            return hostName;
        }


        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
