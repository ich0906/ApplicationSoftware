#pragma checksum "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8648224908926be6d99a734dc42bc878725bd206"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_LctSport_BoardListStdPage), @"mvc.1.0.view", @"/Views/LctSport/BoardListStdPage.cshtml")]
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
#line 1 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\_ViewImports.cshtml"
using AREA1;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\_ViewImports.cshtml"
using AREA1.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8648224908926be6d99a734dc42bc878725bd206", @"/Views/LctSport/BoardListStdPage.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dbabdacff04c265f312ce9c282b57090f72e3e37", @"/Views/_ViewImports.cshtml")]
    public class Views_LctSport_BoardListStdPage : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "ALL", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "TLE", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "CNT", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "AUT", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("aform"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
Write(Html.Partial("~/Views/Shared/LctSportHeader.cshtml"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<!-- content -->\r\n<div id=\"appModule\" class=\"card card-body mb-4 content\">\r\n    <div><p class=\"contenttitle\">");
#nullable restore
#line 4 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
                            Write(ViewData["pageNm"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p></div>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8648224908926be6d99a734dc42bc878725bd2065594", async() => {
                WriteLiteral("\r\n        <div class=\"con_search\">\r\n            <ul>\r\n                <li>\r\n                    <select class=\"form-control form-control-sm\">\r\n                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8648224908926be6d99a734dc42bc878725bd2066029", async() => {
                    WriteLiteral("전체");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8648224908926be6d99a734dc42bc878725bd2067272", async() => {
                    WriteLiteral("제목");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8648224908926be6d99a734dc42bc878725bd2068515", async() => {
                    WriteLiteral("내용");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8648224908926be6d99a734dc42bc878725bd2069758", async() => {
                    WriteLiteral("작성자");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                    </select> <input type=""text"" maxlength=""50"" placeholder=""검색어를 입력하세요"">
                </li>
                <li>
                    <button type=""button"" class=""btn2  btn-gray"">검색</button>
                </li>
            </ul>
        </div>
    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 22 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
     if (ViewData["fs_at"].Equals("Y") || ViewData["pageNm"].Equals("강의 묻고답하기")) {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"btnCon\"><button type=\"button\" class=\"btn2 btn-gray\">글쓰기</button></div>\r\n");
#nullable restore
#line 24 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("    <table class=\"AType\">\r\n        <colgroup>\r\n            <col class=\"wid10pro  wid65px\">\r\n");
#nullable restore
#line 28 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
             if (ViewData["pageNm"].Equals("강의 묻고답하기"))
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <col class=\"wid10pro  wid65px\">\r\n");
#nullable restore
#line 31 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            <col>
            <col width=""10%"" class=""disnone"">
            <col width=""12%"" class=""disnone"">
            <col width=""10%"" class=""disnone"">
            <col width=""10%"" class=""disnone"">
        </colgroup>
        <thead>
            <tr>
                <th>번호</th>
");
#nullable restore
#line 41 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
                 if (ViewData["pageNm"].Equals("강의 묻고답하기"))
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <th>공개 여부</th>\r\n");
#nullable restore
#line 44 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <th>제목</th>
                <th class=""disnone"">파일</th>
                <th class=""disnone"">작성자</th>
                <th class=""disnone"">작성일</th>
                <th class=""disnone"">조회수</th>
            </tr>
        </thead>
        <tbody>
            <script language=""JAVASCRIPT"">
                function test() {
                    alert(""비공개글입니다."");
                }
            </script>
");
#nullable restore
#line 58 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
             if (ViewBag.ResultCnt > 0) {
                for (int i = 0; i < ViewBag.ResultList.Count; i++) {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td><!----> <span>");
#nullable restore
#line 61 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
                              Write( ViewBag.ResultCnt - 10 * (Convert.ToInt32(ViewBag.param["page"])- 1) - @i );

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></td>\r\n");
#nullable restore
#line 62 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
                 if (ViewData["pageNm"].Equals("강의 묻고답하기"))
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td><!----> <span>");
#nullable restore
#line 64 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
                                 Write(ViewBag.ResultList[i]["OTHBC"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></td>\r\n");
#nullable restore
#line 65 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 66 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
                 if (ViewData["pageNm"].Equals("강의 묻고답하기") && ViewBag.ResultList[i]["OTHBC"].Equals("비공개") && !ViewData["name"].Equals(ViewBag.ResultList[i]["REGISTER"])) {

#line default
#line hidden
#nullable disable
            WriteLiteral(" <td class=\"lft\" style=\"cursor: pointer;\" onclick=\"test()\">");
#nullable restore
#line 66 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
                                                                                                                                                                                                                                  Write(ViewBag.ResultList[i]["TITLE"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>");
#nullable restore
#line 66 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
                                                                                                                                                                                                                                                                           }
                else {

#line default
#line hidden
#nullable disable
            WriteLiteral(" <td class=\"lft\" style=\"cursor: pointer;\"");
            BeginWriteAttribute("onclick", " onclick=\"", 2910, "\"", 2962, 3);
            WriteAttributeValue("", 2920, "goDetail(", 2920, 9, true);
#nullable restore
#line 67 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
WriteAttributeValue("", 2929, ViewBag.ResultList[i]["BBS_ID"], 2929, 32, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2961, ")", 2961, 1, true);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 67 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
                                                                                                                Write(ViewBag.ResultList[i]["TITLE"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>");
#nullable restore
#line 67 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
                                                                                                                                                         }

#line default
#line hidden
#nullable disable
            WriteLiteral("                <td class=\"disnone\"><!----></td>\r\n                <td class=\"disnone\">");
#nullable restore
#line 69 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
                               Write(ViewBag.ResultList[i]["REGISTER"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td class=\"disnone\">");
#nullable restore
#line 70 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
                               Write(ViewBag.ResultList[i]["REGIST_DT"].ToString().Split(' ')[0]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td class=\"disnone\">");
#nullable restore
#line 71 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
                               Write(ViewBag.ResultList[i]["RDCNT"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n");
#nullable restore
#line 73 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
                }
            } else {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td colspan=\"6\" class=\"disnone\">글이 없습니다.</td>\r\n                </tr>\r\n");
#nullable restore
#line 78 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n    </table>\r\n\r\n");
#nullable restore
#line 82 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
      
        int k = 1;
    

#line default
#line hidden
#nullable disable
            WriteLiteral("    <ul class=\"paging typeA\">\r\n        <!---->\r\n        <li style=\"cursor:pointer\"");
            BeginWriteAttribute("class", " class=\"", 3645, "\"", 3895, 3);
            WriteAttributeValue("", 3653, "go-prev", 3653, 7, true);
            WriteAttributeValue("\r\n                                           ", 3660, new Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_attribute_value_writer) => {
                PushWriter(__razor_attribute_value_writer);
#nullable restore
#line 88 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
                                            if (ViewBag.param["page"].Equals("1")) {

#line default
#line hidden
#nullable disable
                WriteLiteral("                                                ");
                WriteLiteral("disabled\r\n");
#nullable restore
#line 90 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
                                            }

#line default
#line hidden
#nullable disable
                PopWriter();
            }
            ), 3705, 150, false);
            WriteAttributeValue("                                        ", 3855, "", 3895, 40, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n            <a tabindex=\"-1\">Prev</a>\r\n        </li>\r\n");
#nullable restore
#line 94 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
         for (int j = ViewBag.ResultCnt; j > 0; j -= 10, k++) {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <li");
            BeginWriteAttribute("onclick", " onclick=\"", 4033, "\"", 4054, 3);
            WriteAttributeValue("", 4043, "goPage(", 4043, 7, true);
#nullable restore
#line 95 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
WriteAttributeValue("", 4050, k, 4050, 2, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 4052, ");", 4052, 2, true);
            EndWriteAttribute();
            WriteLiteral(" style=\"cursor:pointer\"");
            BeginWriteAttribute("class", " class=", 4078, "", 4235, 1);
            WriteAttributeValue("", 4085, new Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_attribute_value_writer) => {
                PushWriter(__razor_attribute_value_writer);
#nullable restore
#line 95 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
                                                                    if (ViewBag.param["page"].Equals(k.ToString())) {

#line default
#line hidden
#nullable disable
                WriteLiteral("                                    ");
                WriteLiteral("\" active\"\r\n");
#nullable restore
#line 97 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
                } else {

#line default
#line hidden
#nullable disable
                WriteLiteral(" ");
                WriteLiteral(" \"\"\r\n");
#nullable restore
#line 98 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
                }

#line default
#line hidden
#nullable disable
                PopWriter();
            }
            ), 4085, 150, false);
            EndWriteAttribute();
            WriteLiteral(">\r\n                <a tabindex=\"0\">");
#nullable restore
#line 99 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
                           Write(k);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n            </li>\r\n");
#nullable restore
#line 101 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("        <li style=\"cursor:pointer\"");
            BeginWriteAttribute("class", " class=\"", 4342, "\"", 4591, 3);
            WriteAttributeValue("", 4350, "go-next", 4350, 7, true);
            WriteAttributeValue("\r\n                                        ", 4357, new Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_attribute_value_writer) => {
                PushWriter(__razor_attribute_value_writer);
#nullable restore
#line 103 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
                                         if (ViewBag.param["page"].Equals((k-1).ToString())) {

#line default
#line hidden
#nullable disable
                WriteLiteral("                                            ");
                WriteLiteral("disabled\r\n");
#nullable restore
#line 105 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
                                        }

#line default
#line hidden
#nullable disable
                PopWriter();
            }
            ), 4399, 155, false);
            WriteAttributeValue("                                     ", 4554, "", 4591, 37, true);
            EndWriteAttribute();
            WriteLiteral(@">
            <a tabindex=""-1"">Next</a>
        </li>
        <!---->
    </ul>
</div>
<!-- content -->
<script type=""text/javascript"">
            $(function () {
                $("".go-prev"").click(function () {
                    var page = $(""[name=page]"").val() * 1 - 1;
                    if (!$(this).hasClass('disabled'))
                        goPage(page);
                });

                $("".go-next"").click(function () {
                    var page = $(""[name=page]"").val() * 1 + 1;
                    if (!$(this).hasClass('disabled'))
                        goPage(page);
                });
            });
            function goPage(page) {
                $(""[name=page]"").val(page);

                $(""#__form__"").attr(""action"", """);
#nullable restore
#line 130 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
                                          Write(ViewBag.SelectPageList);

#line default
#line hidden
#nullable disable
            WriteLiteral(@""");
                $(""#__form__"").submit();
            }
            function goDetail(bbs_id) {
                var input = ""<input type='hidden' name='bbs_id' />"";
                $(""#__form__"").append(input);

                $(""[name=bbs_id]"").val(bbs_id);

                $(""#__form__"").attr(""action"", """);
#nullable restore
#line 139 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
                                          Write(ViewBag.Select);

#line default
#line hidden
#nullable disable
            WriteLiteral("\");\r\n                $(\"#__form__\").submit();\r\n            }\r\n\r\n\r\n            $(function () {\r\n                $(\".btnCon\").find(\"button\").click(function () {\r\n                    $(\"#__form__\").attr(\"action\", \"");
#nullable restore
#line 146 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
                                              Write(ViewBag.InsertForm);

#line default
#line hidden
#nullable disable
            WriteLiteral("\");\r\n                    $(\"#__form__\").submit();\r\n                });\r\n\r\n                $(\"[name=selectSubj]\").change(function () {\r\n                    $(\"#__form__\").attr(\"action\", \"");
#nullable restore
#line 151 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
                                              Write(ViewBag.SelectPageList);

#line default
#line hidden
#nullable disable
            WriteLiteral(@""");
                    $(""#__form__"").submit();
                });

                $(""[name=selectYearhakgi]"").change(function () {
                    $(""[name=selectSubj]"").trigger('change');
                });
            });
</script>
    </div>
</div>


");
#nullable restore
#line 164 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\LctSport\BoardListStdPage.cshtml"
Write(Html.Partial("~/Views/Shared/LctSportFooter.cshtml"));

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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
