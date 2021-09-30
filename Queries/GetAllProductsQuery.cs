using System.Collections.Generic;
using eCommerceApiProducts.Models;
using MediatR;

namespace eCommerceApiProducts
{
    public class GetAllProductsQuery :IRequest<List<Product>>
    {

    }
}
