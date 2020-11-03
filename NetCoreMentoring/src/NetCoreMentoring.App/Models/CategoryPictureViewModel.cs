using Microsoft.AspNetCore.Http;

namespace NetCoreMentoring.App.Models
{
    public class CategoryPictureViewModel
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public byte[] Picture { get; set; }

        public IFormFile FormFile { get; set; }
    }
}
