using System.Threading.Tasks;

namespace eCommerceApiProducts
{
    public class SomeRepository
    {
        public async Task<int> GetValueAsync(int numberToAdd)
        {
            return await Task.FromResult( numberToAdd *2);
        }
    }
}