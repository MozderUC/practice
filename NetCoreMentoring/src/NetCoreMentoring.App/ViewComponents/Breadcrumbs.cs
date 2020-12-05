using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreMentoring.App.Models;

namespace NetCoreMentoring.App.ViewComponents
{
    public class Breadcrumbs : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string url)
        {
            return View("Default", GetBreadcrumbs(url));
        }

        private List<BreadcrumbViewModel> GetBreadcrumbs(string url)
        {
            var pathComponents = url.StartsWith('/')
                ? url.Substring(1).Split('/').ToList()
                : url.Split('/').ToList();

            var result = new List<BreadcrumbViewModel>()
            {
                new BreadcrumbViewModel() {ControllerName = "Home", NavigationName = "Home"},
                new BreadcrumbViewModel() {ControllerName = pathComponents.ElementAtOrDefault(0), NavigationName = pathComponents.ElementAtOrDefault(0)},
                new BreadcrumbViewModel() {ControllerName = pathComponents.ElementAtOrDefault(0), NavigationName = pathComponents.ElementAtOrDefault(1)}
            };

            return result;
        }
    }
}