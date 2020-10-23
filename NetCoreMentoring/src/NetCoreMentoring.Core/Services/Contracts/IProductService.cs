using System;
using System.Collections.Generic;
using System.Text;
using NetCoreMentoring.Core.Models;

namespace NetCoreMentoring.Core.Services.Contracts
{
    public interface IProductService
    {
        public IEnumerable<Products> GetProducts();
    }
}
