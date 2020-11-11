using System.Collections.Generic;
using NetCoreMentoring.Core.Models;

namespace NetCoreMentoring.Core.Services.Contracts
{
    public interface ICategoryService
    {
        public IEnumerable<Category> GetCategories();
    }
}