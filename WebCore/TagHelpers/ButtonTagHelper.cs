using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.TagHelpers
{
    [HtmlTargetElement("button", Attributes = "bs-button-color", TagStructure = TagStructure.NormalOrSelfClosing)]
    //[HtmlTargetElement("a", Attributes = "bs-button-color", ParentTag = "form")]
    public class MyPersonalButtonTagHelper : TagHelper
    {
        //[HtmlAttributeName("colore")]
        public string BsButtonColor { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Logica: accesso al db,             
            output.Attributes.SetAttribute("class", $"btn btn-{BsButtonColor}");           
            output.Content.AppendHtml("<h1>CIAO SONO IL TAG HELPER</h1>");
        }
    }

    [HtmlTargetElement("input", Attributes = "[type = 'checkbox']", TagStructure = TagStructure.NormalOrSelfClosing)]
    //[HtmlTargetElement("a", Attributes = "bs-button-color", ParentTag = "form")]
    public class MyPersonal2ButtonTagHelper : TagHelper
    {
        //[HtmlAttributeName("colore")]
        public string BsButtonColor { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.SetAttribute("class", "btn btn-warning");
            output.Content.SetContent("<h1>SONO IL TAG HELPER 2</h1>");
        }
    }
}
