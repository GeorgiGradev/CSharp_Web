using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MyFirstAspNetCoreApp.TagHelpers
{
    [HtmlTargetElement("table", Attributes = "bootstrap-table")]
    public class BootstrapTableTagHelper :TagHelper
    {
       
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Add(new TagHelperAttribute("class", "table table-striped table-hover table sm"));
        }
    }
}
