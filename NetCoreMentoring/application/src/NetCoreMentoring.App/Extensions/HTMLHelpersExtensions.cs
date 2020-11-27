using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using TagBuilder = System.Web.Mvc.TagBuilder;

namespace NetCoreMentoring.App.Extensions
{
    public static class HTMLHelpersExtensions
    {
        public static HtmlString CategoryImageLink(this IHtmlHelper html, int categoryImageId)
        {
            var a = new TagBuilder("a");
            a.SetInnerText("picture");
            a.MergeAttribute("href",$"images/{categoryImageId}");
            return new HtmlString(a.ToString());
        }
    }
}
