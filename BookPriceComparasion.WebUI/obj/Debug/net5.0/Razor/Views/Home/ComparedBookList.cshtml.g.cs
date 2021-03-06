#pragma checksum "H:\GithubProjects\BookPriceComparasion\BookPriceComparasion.WebUI\Views\Home\ComparedBookList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0d3b177d9e6f33de05b8b7305d2d45f650a45750"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_ComparedBookList), @"mvc.1.0.view", @"/Views/Home/ComparedBookList.cshtml")]
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
#nullable restore
#line 1 "H:\GithubProjects\BookPriceComparasion\BookPriceComparasion.WebUI\Views\_ViewImports.cshtml"
using BookPriceComparasion.WebUI;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "H:\GithubProjects\BookPriceComparasion\BookPriceComparasion.WebUI\Views\_ViewImports.cshtml"
using BookPriceComparasion.WebUI.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0d3b177d9e6f33de05b8b7305d2d45f650a45750", @"/Views/Home/ComparedBookList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"44bd5f47c684f6bd5cb41cae65f251a47a0190f7", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_ComparedBookList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Book>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "H:\GithubProjects\BookPriceComparasion\BookPriceComparasion.WebUI\Views\Home\ComparedBookList.cshtml"
  
    ViewData["Title"] = "ComparedBookList";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            DefineSection("Styles", async() => {
                WriteLiteral("\r\n    <style>\r\n        span[class=\'badge\'] {\r\n            background-color: cornflowerblue;\r\n            color: white;\r\n        }\r\n    </style>\r\n");
            }
            );
            WriteLiteral("\r\n\r\n\r\n");
#nullable restore
#line 18 "H:\GithubProjects\BookPriceComparasion\BookPriceComparasion.WebUI\Views\Home\ComparedBookList.cshtml"
 if (Model.Count > 0)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"text-center\">\r\n        <img");
            BeginWriteAttribute("src", " src=\"", 314, "\"", 335, 1);
#nullable restore
#line 21 "H:\GithubProjects\BookPriceComparasion\BookPriceComparasion.WebUI\Views\Home\ComparedBookList.cshtml"
WriteAttributeValue("", 320, Model[0].Image, 320, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" alt=\"Alternate Text\" style=\"width: 150px; height: 250px; object-fit: cover;\" />\r\n\r\n\r\n        <p>\r\n            <h4>\r\n                <span class=\"badge\"> Yazar: ");
#nullable restore
#line 26 "H:\GithubProjects\BookPriceComparasion\BookPriceComparasion.WebUI\Views\Home\ComparedBookList.cshtml"
                                       Write(Model[0].Author);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </span>\r\n            </h4>\r\n        </p>\r\n\r\n\r\n        <p>\r\n            <h4>\r\n                <span class=\"badge\"> Yay??nevi: ");
#nullable restore
#line 33 "H:\GithubProjects\BookPriceComparasion\BookPriceComparasion.WebUI\Views\Home\ComparedBookList.cshtml"
                                          Write(Model[0].Publisher);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </span>\r\n            </h4>\r\n        </p>\r\n    </div>\r\n");
            WriteLiteral("    <div class=\"row\" style=\" background-color: lavender;width:80%;margin-left:10%;\">\r\n\r\n");
#nullable restore
#line 42 "H:\GithubProjects\BookPriceComparasion\BookPriceComparasion.WebUI\Views\Home\ComparedBookList.cshtml"
         foreach (var book in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"col-md-4\">\r\n                <img");
            BeginWriteAttribute("src", " src=\"", 911, "\"", 934, 1);
#nullable restore
#line 45 "H:\GithubProjects\BookPriceComparasion\BookPriceComparasion.WebUI\Views\Home\ComparedBookList.cshtml"
WriteAttributeValue("", 917, book.WebsiteLogo, 917, 17, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" alt=\"Website Logo\" style=\"max-width: 200px;max-height:150px;\" />\r\n            </div>\r\n");
            WriteLiteral("            <div class=\"col-md-3\">\r\n\r\n                <h3>\r\n                    <span class=\"badge badge-pill badge-secondary\">\r\n                        ");
#nullable restore
#line 52 "H:\GithubProjects\BookPriceComparasion\BookPriceComparasion.WebUI\Views\Home\ComparedBookList.cshtml"
                    Write(book.Price.Replace("TL", "").Replace("???", "").Trim());

#line default
#line hidden
#nullable disable
            WriteLiteral(" <i class=\'fa fa-try\' aria-hidden=\'true\'></i>\r\n                    </span>\r\n                </h3>\r\n            </div>\r\n");
            WriteLiteral("            <div class=\"col-md-3\">\r\n                <a");
            BeginWriteAttribute("href", " href=\"", 1409, "\"", 1431, 1);
#nullable restore
#line 59 "H:\GithubProjects\BookPriceComparasion\BookPriceComparasion.WebUI\Views\Home\ComparedBookList.cshtml"
WriteAttributeValue("", 1416, book.DetailUrl, 1416, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-primary\" target=\"_blank\"> Siteye Git </a>\r\n            </div>\r\n");
#nullable restore
#line 62 "H:\GithubProjects\BookPriceComparasion\BookPriceComparasion.WebUI\Views\Home\ComparedBookList.cshtml"
             if (Model[0].Price == book.Price)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"col-md-2\">\r\n                    <h5> <span class=\"badge badge-success\"> En ucuz  </span> </h5>\r\n                </div>\r\n");
#nullable restore
#line 67 "H:\GithubProjects\BookPriceComparasion\BookPriceComparasion.WebUI\Views\Home\ComparedBookList.cshtml"
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 67 "H:\GithubProjects\BookPriceComparasion\BookPriceComparasion.WebUI\Views\Home\ComparedBookList.cshtml"
             
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n");
#nullable restore
#line 71 "H:\GithubProjects\BookPriceComparasion\BookPriceComparasion.WebUI\Views\Home\ComparedBookList.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Book>> Html { get; private set; }
    }
}
#pragma warning restore 1591
