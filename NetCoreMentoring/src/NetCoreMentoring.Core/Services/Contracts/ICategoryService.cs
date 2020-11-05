using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using NetCoreMentoring.Core.Models;

namespace NetCoreMentoring.Core.Services.Contracts
{
    public interface ICategoryService
    {
        public IEnumerable<Category> GetCategories();

        public Category GetCategory(int categoryId);

        public byte[] GetPicture(int categoryId);

        public void UpdatePicture(int categoryId, IFormFile newPicture);
    }
}