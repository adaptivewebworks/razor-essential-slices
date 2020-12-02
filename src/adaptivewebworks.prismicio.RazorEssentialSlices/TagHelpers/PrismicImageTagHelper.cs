using System.Linq;
using Microsoft.AspNetCore.Razor.TagHelpers;
using prismic;

namespace adaptivewebworks.prismicio.RazorEssentialSlices.TagHelpers
{
    [HtmlTargetElement("prismic-image")]
    public class PrismicImageTagHelper : TagHelper
    {
        public WithFragments Document { get; set; }
        public string Field { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Document == null || !Document.Fragments.Any())
            {
                output.SuppressOutput();
                return;
            }

            var image = Document.GetImage(Field);

            if (image == null || !image.TryGetView("main", out var imageView))
            {
                output.SuppressOutput();
                return;
            }
            
            output.TagName = "img";
            output.Attributes.Add("src", imageView.Url);
            output.Attributes.Add("alt", imageView.Alt);
            // TODO copyright
            output.TagMode = TagMode.SelfClosing;
        }
    }
}