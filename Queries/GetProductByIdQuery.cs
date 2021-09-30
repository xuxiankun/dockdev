using eCommerceApiProducts.Models;
using MediatR;

namespace eCommerceApiProducts
{
    public class GetProductByIdQuery:IRequest<Product>
    {
        public int Id { get;}

        public GetProductByIdQuery(int id)
        {
            Id = id;

        }
    }
}