using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NetCoreMentoring.App.Infrastructure
{
    [HtmlTargetElement(Attributes = "category-image-id")]
    public class CategoryImageIdTagHelper : TagHelper
    {
        public int CategoryImageId { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.RemoveAll("bold");
            output.Attributes.SetAttribute("href", $"/images/{CategoryImageId}");
            output.Content.SetContent("picture");
        }
    }
}