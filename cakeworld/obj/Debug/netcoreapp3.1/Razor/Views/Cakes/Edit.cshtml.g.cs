#pragma checksum "C:\Users\Acer\Desktop\softwareproject\cakeworld\Views\Cakes\Edit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c0fcf5fb52c0364bc2beb9a3c3ecbf9aa1c3bb00"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Cakes_Edit), @"mvc.1.0.view", @"/Views/Cakes/Edit.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c0fcf5fb52c0364bc2beb9a3c3ecbf9aa1c3bb00", @"/Views/Cakes/Edit.cshtml")]
    public class Views_Cakes_Edit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<cakeworld.Models.Cake>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Acer\Desktop\softwareproject\cakeworld\Views\Cakes\Edit.cshtml"
  
    ViewData["Title"] = "Edit";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<h1>Edit</h1>

<h4>Cake</h4>
<hr />
<div class=""row"">
    <div class=""col-md-4"">
        <form asp-action=""Edit"">
            <div asp-validation-summary=""ModelOnly"" class=""text-danger""></div>
            <input type=""hidden"" asp-for=""Id"" />
            <div class=""form-group"">
                <label asp-for=""Name"" class=""control-label""></label>
                <input asp-for=""Name"" class=""form-control"" />
                <span asp-validation-for=""Name"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""Descrption"" class=""control-label""></label>
                <input asp-for=""Descrption"" class=""form-control"" />
                <span asp-validation-for=""Descrption"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""Price"" class=""control-label""></label>
                <input asp-for=""Price"" class=""form-control"" />
                <span asp-validation-for=""Pr");
            WriteLiteral(@"ice"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <input type=""submit"" value=""Save"" class=""btn btn-primary"" />
            </div>
        </form>
    </div>
</div>

<div>
    <a asp-action=""Index"">Back to List</a>
</div>

");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n");
#nullable restore
#line 43 "C:\Users\Acer\Desktop\softwareproject\cakeworld\Views\Cakes\Edit.cshtml"
      await Html.RenderPartialAsync("_ValidationScriptsPartial");

#line default
#line hidden
#nullable disable
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<cakeworld.Models.Cake> Html { get; private set; }
    }
}
#pragma warning restore 1591
