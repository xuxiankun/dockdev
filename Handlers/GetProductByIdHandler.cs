using System.Threading;
using System.Threading.Tasks;
using eCommerceApiProducts.Models;
using MediatR;

namespace eCommerceApiProducts
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
         private readonly ProductsDbContext _context;
        public GetProductByIdHandler(ProductsDbContext context)
        {
            _context = context;
        }

        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Products.FindAsync(request.Id);
        }
    }
}