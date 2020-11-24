using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using NetCoreMentoring.Core.Models;
using NetCoreMentoring.Core.Utilities.ResultFlow;

namespace NetCoreMentoring.Core.Services.Contracts
{
    public interface ICategoryService
    {
        public Result<IEnumerable<Category>> GetCategories();

        public Result<Category> GetCategory(int categoryId);

        public Result<byte[]> GetPicture(int categoryId);

        public Result UpdatePicture(int categoryId, IFormFile newPicture);
    }
}