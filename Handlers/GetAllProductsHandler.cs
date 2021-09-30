using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using eCommerceApiProducts.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApiProducts
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<Product>>
    {
        private readonly ProductsDbContext _context;

        public GetAllProductsHandler(ProductsDbContext context)
        {
            _context = context;

        }
        public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Products.ToListAsync();
        }
    }
}