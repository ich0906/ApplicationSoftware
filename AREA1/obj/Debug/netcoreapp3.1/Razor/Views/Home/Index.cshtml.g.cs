#pragma checksum "C:\Users\jaeha\source\repos\ApplicationSoftware\AREA1\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b64b8052f86ca30d18f4cace77b833560bfd604c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
#line 1 "C:\Users\jaeha\source\repos\ApplicationSoftware\AREA1\Views\_ViewImports.cshtml"
using AREA1;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\jaeha\source\repos\ApplicationSoftware\AREA1\Views\_ViewImports.cshtml"
using AREA1.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b64b8052f86ca30d18f4cace77b833560bfd604c", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dbabdacff04c265f312ce9c282b57090f72e3e37", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("aform"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\jaeha\source\repos\ApplicationSoftware\AREA1\Views\Home\Index.cshtml"
  
    Layout = "_Layout";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"text-center\">\r\n    <h1 class=\"display-4\">Hello ");
#nullable restore
#line 6 "C:\Users\jaeha\source\repos\ApplicationSoftware\AREA1\Views\Home\Index.cshtml"
                           Write(ViewData["UserId"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n    <p>Learn about <a href=\"https://docs.microsoft.com/aspnet/core\">building Web apps with ASP.NET Core</a>.</p>\r\n\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b64b8052f86ca30d18f4cace77b833560bfd604c4174", async() => {
                WriteLiteral(@"
        group_nm  : <input name=""group_nm"" /><br>
        code_nm  : <input name=""code_nm"" /><br>
        <!--
        FirstName : <input name=""first_name"" /><br>
        Address   : <input name=""address"" /><br>
        City      : <input name=""city"" /><br>
        -->
    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"

    <button id=""btn_insert"">Insert Button</button>
    <button id=""btn_update"">Update Button</button>
    <button id=""btn_delete"">Delete Button</button>
    <button id=""btn_select"">Select Button</button>

    <br>
    <div id=""rslt_select"">
    </div>
</div>

<script type=""text/javascript"">
    $(function () {
        $(""#btn_insert"").click(function () {

            $(""#aform"").attr(""action"", ""/Home/InsertData"");
            $(""#aform"").attr(""method"", ""POST"");
            $(""#aform"").submit();

        });
    });

    $(""#btn_select"").click(function () {
        $(""#aform"").attr(""action"", ""/Home/SelectData"");
        $(""#aform"").attr(""method"", ""POST"");

        $(""#aform"").ajaxForm({
            success: function (data) {
                var table = '<table id=""rslt_table"">' +
                    '<tr>' +
                    '<td>group_id</td>' +
                    '<td>code_id</td>' +
                    //'<td>FIRST_NAME</td>' +
                    //'<td>ADDRESS</td>'");
            WriteLiteral(@" +
                    //'<td>CITY</td>' +
                    '</tr>' +
                    '<tbody></tbody>' +
                    '</table> ';
                $(""#rslt_select"").append(table);
                for (let i = 0; i < data.length; ++i) {
                    $(""#rslt_table > tbody:last"").append(""<tr><td>""
                        + data[i].value['group_id'] + ""</td><td>"" +
                        //data[i].value['code_id'] + ""</td><td>"" +
                        //data[i].value['FIRST_NAME'] + ""</td><td>"" +
                        //data[i].value['ADDRESS'] + ""</td><td>"" +
                        data[i].value['code_id'] + ""</td></tr>"");
                }
            }
        });

        $('#aform').submit();
    });
</script>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
