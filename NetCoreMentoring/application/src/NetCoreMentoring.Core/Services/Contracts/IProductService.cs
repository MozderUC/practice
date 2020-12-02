using System.Collections.Generic;
using NetCoreMentoring.Core.Models;
using NetCoreMentoring.Core.Utilities.ResultFlow;

namespace NetCoreMentoring.Core.Services.Contracts
{
    public interface IProductService
    {
        public Result<IEnumerable<Product>> GetProducts();

        public Result<Product> GetProduct(int id);

        public Result<ProductAndCategories> GetProductWithCategories(int id);

        public Result Update(Product product);

        public Result Create(Product product);
    }
}