#pragma checksum "C:\Users\jaeha\source\repos\ApplicationSoftware\AREA1\Views\Shared\LctSportHeader.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ae17bbda79e22a46ab1db45557e17ce3b4d01f40"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_LctSportHeader), @"mvc.1.0.view", @"/Views/Shared/LctSportHeader.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ae17bbda79e22a46ab1db45557e17ce3b4d01f40", @"/Views/Shared/LctSportHeader.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dbabdacff04c265f312ce9c282b57090f72e3e37", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_LctSportHeader : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("__form__"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("display: none;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("v-for", new global::Microsoft.AspNetCore.Html.HtmlString("option in yearhakgiList"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute(":value", new global::Microsoft.AspNetCore.Html.HtmlString("option"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("v-for", new global::Microsoft.AspNetCore.Html.HtmlString("option in subjList"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral(@"<script>
    let appEvents = new Vue({});
</script>

<script type=""text/javascript"">

    function setLctrumInfo(grcode, yearhakgi, subj) {
        $('#__form__').find('input[name=""selectedGrcode""]').val(grcode);
        $('#__form__').find('input[name=""selectedYearhakgi""]').val(yearhakgi);
        $('#__form__').find('input[name=""selectedSubj""]').val(subj);
    }

    function linkWinOpen(url) {
        window.open(url);
    }

    function linkUrl(url, param, target) {
        if ('#' === url || '' === url) {
            return;
        }

        let loader = Vue.$loading.show();

        if (param !== undefined) {
            if (typeof param === 'object') {
                var $form = $('#__form__');
                var $inputBox;

                for (var key in param) {
                    if (typeof param[key] === 'string' || typeof param[key] === 'number') {
                        $inputBox = $form.find(':input[name=""' + key + '""]');

                        if ($inpu");
            WriteLiteral(@"tBox.length > 0) {
                            $inputBox.val(param[key]);
                        } else {
                            $('<input>').attr({
                                type: 'hidden',
                                name: key,
                                value: param[key]
                            }).appendTo($form);
                        }
                    }
                }
            }
        }

        if (target === undefined) {
            target = '';

        } else {
            loader && loader.hide();
        }

        $('#__form__').attr('target', target).attr('action', url).submit();
    }

    function linkOpenTalkPopup(yearhakgi, subj, subjNm) {
        let param = {
            'selectYearhakgi': yearhakgi,
            'selectSubj': subj,
            'subjNm': subjNm
        };

        //window.open('', 'talkPop', 'scrollbars=yes,width=345,height=495,resizable=no');
        window.open('', 'talkPop', 'scrollbars=yes,width=500,h");
            WriteLiteral("eight=495,resizable=yes\');\r\n\r\n        linkUrl(\'/std/lis/sport/LrnTalkStdPopupPage.do\', param, \'talkPop\');\r\n    }\r\n    function pageReload() {\r\n        location.reload();\r\n    }\r\n\r\n</script>\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ae17bbda79e22a46ab1db45557e17ce3b4d01f407786", async() => {
                WriteLiteral("\r\n    <input type=\"hidden\" name=\"selectedGrcode\" />\r\n    <input type=\"hidden\" name=\"selectedYearhakgi\" />\r\n    <input type=\"hidden\" name=\"selectedSubj\" />\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"

<script type=""text/javascript"">
    $(function () {
        $("".btnCon"").find(""button"").click(function () {
            $(""#__form__"").attr(""action"", ""/Notice/InsertFormNotice"");
            $(""#__form__"").submit();
        });

        $(""[name=selectSubj]"").change(function () {
            $(""#__form__"").attr(""action"", ""/Notice/SelectPageListNotice"");
            $(""#__form__"").submit();
        });
    });
</script>

<section>

    <div id=""appHeaderSubj"" class=""subjectnameba"">
        <div class=""container"">
            <div class=""row mg0"" style=""height: 37px"">
                <div class=""col-md-10 subtitle"" style=""cursor: pointer;"" ");
            WriteLiteral("@click=\"goLctrumMain()\"><span class=\"subjectname\">{{ subjNm }}</span> <span class=\"subjecttime\">{{ lctrumSchdul }}</span></div>\r\n                <div class=\"col-md-2 subtap\">\r\n                    <a href=\"#\" ");
            WriteLiteral(@"@click=""appHeaderSubj.goOpenTalkWin(selectYearhakgi, selectSubj, subjNm);""><img src=""/images/lctSport/btntap.png""></a>
                </div>
            </div>
        </div>
    </div>

    <script>

        let appHeaderSubj = new Vue({
            el: '#appHeaderSubj',
            data: {
                subjNm: null,
                lctrumSchdul: null,
                selectGrcode: null,
                selectYearhakgi: null,
                selectSubj: null
            },
            mounted: function () {
                let self = this;

                appEvents.$on('eventSubjChange', function (param) {
                    self.selectYearhakgi = param.selectYearhakgi;
                    self.selectSubj = param.selectSubj;
                    self.subjNm = param.subjNm;
                });
            },
            methods: {
                goLctrumMain: function () {

                    linkUrl('/std/lis/evltn/LctrumHomeStdPage.do', this.$data);

                },");
            WriteLiteral(@"
                goOpenTalkWin: function (yearhakgi, subj, subjNm) {
                    linkOpenTalkPopup(yearhakgi, subj, subjNm);
                }
            }
        });

    </script>

    <div class=""py-4 bg-light"">
        <div class=""container "">

            <div class=""card card-body selectsemester"">
                <!-- style=""border:1px solid #585858;"" -->

                <div id=""appSelectSubj"" class=""row"">
                    <div class=""col-md-5"">
                        <div class=""row"">
                            <div class=""col-3 aC"">수강학기</div>
                            <div class=""col-9"">
                                <select name=""selectYearhakgi"" v-model=""selectYearhakgi"" ");
            WriteLiteral("@change=\"onYearhakgiChange()\" class=\"form-control form-control-sm\">\r\n                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ae17bbda79e22a46ab1db45557e17ce3b4d01f4012500", async() => {
                WriteLiteral("\r\n                                        {{ option.label }}\r\n                                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class=""col-md-7"">
                        <div class=""row"">
                            <div class=""col-3 aC"">과목명</div>
                            <div class=""col-9"">
                                <select name=""selectSubj"" v-model=""selectSubj"" ");
            WriteLiteral("@change=\"onSubjChange()\" class=\"form-control form-control-sm\" style=\"width:100%\">\r\n                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ae17bbda79e22a46ab1db45557e17ce3b4d01f4014305", async() => {
                WriteLiteral("\r\n                                        {{ option.LABEL }}\r\n                                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                </select>\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n\r\n            </div>\r\n");
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
