#pragma checksum "C:\git\controllogix\ControlLogix\ControlLogix\Views\Libraries\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "664a3ecbc2e8d47827809c7e123d5487174bbcab"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Libraries_Details), @"mvc.1.0.view", @"/Views/Libraries/Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Libraries/Details.cshtml", typeof(AspNetCore.Views_Libraries_Details))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\git\controllogix\ControlLogix\ControlLogix\Views\_ViewImports.cshtml"
using ControlLogix;

#line default
#line hidden
#line 2 "C:\git\controllogix\ControlLogix\ControlLogix\Views\_ViewImports.cshtml"
using ControlLogix.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"664a3ecbc2e8d47827809c7e123d5487174bbcab", @"/Views/Libraries/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"83fc6d3906db3fba2e364fe6c124f549fee48a22", @"/Views/_ViewImports.cshtml")]
    public class Views_Libraries_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ControlLogix.Models.Library>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(36, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\git\controllogix\ControlLogix\ControlLogix\Views\Libraries\Details.cshtml"
  
    ViewData["Title"] = "Library Details";

#line default
#line hidden
            BeginContext(89, 52, true);
            WriteLiteral("\r\n<br />\r\n<br />\r\n\r\n<h5>Library Details</h5>\r\n\r\n<h1>");
            EndContext();
            BeginContext(142, 40, false);
#line 12 "C:\git\controllogix\ControlLogix\ControlLogix\Views\Libraries\Details.cshtml"
Write(Html.DisplayFor(modelItem => Model.Name));

#line default
#line hidden
            EndContext();
            BeginContext(182, 15, true);
            WriteLiteral("</h1>\r\n\r\n\r\n<h4>");
            EndContext();
            BeginContext(198, 47, false);
#line 15 "C:\git\controllogix\ControlLogix\ControlLogix\Views\Libraries\Details.cshtml"
Write(Html.DisplayFor(modelItem => Model.Description));

#line default
#line hidden
            EndContext();
            BeginContext(245, 111, true);
            WriteLiteral("</h4>\r\n\r\n<br />\r\n<br />\r\n\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(357, 46, false);
#line 24 "C:\git\controllogix\ControlLogix\ControlLogix\Views\Libraries\Details.cshtml"
           Write(Html.DisplayNameFor(model => model.PrimaryKey));

#line default
#line hidden
            EndContext();
            BeginContext(403, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(459, 40, false);
#line 27 "C:\git\controllogix\ControlLogix\ControlLogix\Views\Libraries\Details.cshtml"
           Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
            EndContext();
            BeginContext(499, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(555, 47, false);
#line 30 "C:\git\controllogix\ControlLogix\ControlLogix\Views\Libraries\Details.cshtml"
           Write(Html.DisplayNameFor(model => model.Description));

#line default
#line hidden
            EndContext();
            BeginContext(602, 21, true);
            WriteLiteral("\r\n            </th>\r\n");
            EndContext();
            BeginContext(650, 42, true);
            WriteLiteral("        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
            EndContext();
#line 36 "C:\git\controllogix\ControlLogix\ControlLogix\Views\Libraries\Details.cshtml"
         foreach (var item in Model.ControlBlocks.Values.ToArray())
        {

#line default
#line hidden
            BeginContext(772, 60, true);
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(833, 45, false);
#line 40 "C:\git\controllogix\ControlLogix\ControlLogix\Views\Libraries\Details.cshtml"
               Write(Html.DisplayFor(modelItem => item.PrimaryKey));

#line default
#line hidden
            EndContext();
            BeginContext(878, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(946, 39, false);
#line 43 "C:\git\controllogix\ControlLogix\ControlLogix\Views\Libraries\Details.cshtml"
               Write(Html.DisplayFor(modelItem => item.Name));

#line default
#line hidden
            EndContext();
            BeginContext(985, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(1053, 46, false);
#line 46 "C:\git\controllogix\ControlLogix\ControlLogix\Views\Libraries\Details.cshtml"
               Write(Html.DisplayFor(modelItem => item.Description));

#line default
#line hidden
            EndContext();
            BeginContext(1099, 25, true);
            WriteLiteral("\r\n                </td>\r\n");
            EndContext();
            BeginContext(1274, 22, true);
            WriteLiteral(">\r\n            </tr>\r\n");
            EndContext();
#line 52 "C:\git\controllogix\ControlLogix\ControlLogix\Views\Libraries\Details.cshtml"
        }

#line default
#line hidden
            BeginContext(1307, 24, true);
            WriteLiteral("    </tbody>\r\n</table>\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ControlLogix.Models.Library> Html { get; private set; }
    }
}
#pragma warning restore 1591
