using System.Collections.Generic;

namespace NetCoreMentoring.App.Models
{
    public class ModifyProductViewModel
    {
        public ProductViewModel Product { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
