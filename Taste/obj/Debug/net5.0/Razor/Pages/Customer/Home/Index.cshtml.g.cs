#pragma checksum "C:\Users\Dad S. Wonkulah Jr\source\repos\Taste\Taste\Pages\Customer\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e01cb099e3c2e6237cccc623b4c5188dde023350"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(Taste.Pages.Customer.Home.Pages_Customer_Home_Index), @"mvc.1.0.razor-page", @"/Pages/Customer/Home/Index.cshtml")]
namespace Taste.Pages.Customer.Home
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
#line 1 "C:\Users\Dad S. Wonkulah Jr\source\repos\Taste\Taste\Pages\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Dad S. Wonkulah Jr\source\repos\Taste\Taste\Pages\_ViewImports.cshtml"
using Taste;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e01cb099e3c2e6237cccc623b4c5188dde023350", @"/Pages/Customer/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0e81d51dcf919c7ec22041d8ef9f2658229c4add", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Customer_Home_Index : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 8 "C:\Users\Dad S. Wonkulah Jr\source\repos\Taste\Taste\Pages\Customer\Home\Index.cshtml"
 foreach (var category in Model.CustomerHomeVM.ListOfCategorieItems)
{
    if (Model.CustomerHomeVM.ListOfMenuItems.Where(c => c.CategoryId == category.Id).Count() > 0)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"row container pb-3 backgroundWhite\">\r\n            <div class=\"col-12\">\r\n                <div class=\"row\">\r\n                    <h2 class=\"text-success pl-1\">");
#nullable restore
#line 15 "C:\Users\Dad S. Wonkulah Jr\source\repos\Taste\Taste\Pages\Customer\Home\Index.cshtml"
                                             Write(category.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n                </div>\r\n            </div>\r\n        </div>\r\n");
#nullable restore
#line 19 "C:\Users\Dad S. Wonkulah Jr\source\repos\Taste\Taste\Pages\Customer\Home\Index.cshtml"
    }

}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Taste.Pages.Customer.Home.IndexModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Taste.Pages.Customer.Home.IndexModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Taste.Pages.Customer.Home.IndexModel>)PageContext?.ViewData;
        public Taste.Pages.Customer.Home.IndexModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
