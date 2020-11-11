using System.Collections.Generic;
using NetCoreMentoring.Core.Models;

namespace NetCoreMentoring.Core.Services.Contracts
{
    public interface IProductService
    {
        public IEnumerable<Product> GetProducts();

        public Product GetProduct(int id);

        public Product Update(Product product);

        public Product Create(Product product);
    }
}