using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreMentoring.App.ViewComponents
{
    public class Breadcrumbs : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string url)
        {
            var pathComponents = new List<string>() {"Home"};

            pathComponents.AddRange(GetPathComponents(url));

            return View("Default", pathComponents);

        }

        private IEnumerable<string> GetPathComponents(string url)
        {
            return url.StartsWith('/')
                ? url.Substring(1).Split('/')
                : url.Split('/');
        }
    }
}