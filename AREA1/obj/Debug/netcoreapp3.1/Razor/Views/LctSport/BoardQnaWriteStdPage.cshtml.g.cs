#pragma checksum "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "59546c245645c2eb161c8a724283d0cefc292075"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_LctSport_BoardQnaWriteStdPage), @"mvc.1.0.view", @"/Views/LctSport/BoardQnaWriteStdPage.cshtml")]
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
#line 1 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\_ViewImports.cshtml"
using AREA1;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\_ViewImports.cshtml"
using AREA1.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"59546c245645c2eb161c8a724283d0cefc292075", @"/Views/LctSport/BoardQnaWriteStdPage.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a1f71929804859653ddd0cc5f8b5c7bd41d3ccfb", @"/Views/_ViewImports.cshtml")]
    public class Views_LctSport_BoardQnaWriteStdPage : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/javascript"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/smarteditor/HuskyEZCreator.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("charset", new global::Microsoft.AspNetCore.Html.HtmlString("utf-8"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/smarteditor/smarteditor_basic.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
Write(Html.Partial("~/Views/Shared/LctSportHeader.cshtml"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n<!-- content -->\r\n<div id=\"appModule\" class=\"card card-body mb-4 content\">\r\n    <div><p class=\"contenttitle\">");
#nullable restore
#line 6 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
                            Write(ViewData["pageNm"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p></div>\r\n    <div class=\"board_write_form\">\r\n        <dl>\r\n            <dt>\r\n                제목\r\n            </dt>\r\n            <dd><input type=\"text\" id=\"title\" v-model=\"title\" class=\"es\" style=\"width: 100%;\"></dd>\r\n        </dl>\r\n");
#nullable restore
#line 14 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
         if (ViewData["pageNm"].Equals("강의 공지사항")) {
        

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
        <dl>
            <dt>
                최우선공지여부
            </dt>
            <dd>
                <input type=""radio"" id=""two"" value=""N"" v-model=""othbcAt"">
                <label for=""two"">Y</label>
                <input type=""radio"" id=""one"" value=""Y"" v-model=""othbcAt"">
                <label for=""one"">N</label>
            </dd>
        </dl>
        ");
#nullable restore
#line 27 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
               
        } else {
        

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
        <dl>
            <dt>
                공개여부
            </dt>
            <dd>
                <input type=""radio"" id=""one"" value=""Y"" v-model=""othbcAt"">
                <label for=""one"">공개</label>
                <input type=""radio"" id=""two"" value=""N"" v-model=""othbcAt"">
                <label for=""two"">비공개</label>
            </dd>
        </dl>
        ");
#nullable restore
#line 41 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
               
        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        <dl>
            <dt>
                내용
            </dt>
            <dd>
                <textarea id=""content"" class=""es"" style=""display: none;""></textarea>
            </dd>
        </dl>
        <dl>
            <dt>
                파일 업로드
            </dt>
            <dd>
                <file-upload ref=""attach"" multiple=""true"" :storage-id=""storageId"" :attach-id=""atchFileId"" params=""{ 'masterNo' : '5' }"" ");
            WriteLiteral("@upload-complete=\"uploadComplete\"></file-upload>\r\n            </dd>\r\n        </dl>\r\n    </div>\r\n    <div class=\'btnCon\'>\r\n");
#nullable restore
#line 61 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
         if (ViewData["command"].Equals("UPDATE")) {

#line default
#line hidden
#nullable disable
            WriteLiteral("<button type=\"button\" class=\"btn2 btn-darkpurple\" ");
            WriteLiteral("@click=\"saveData()\">수정</button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\r\n");
#nullable restore
#line 63 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
} else {

#line default
#line hidden
#nullable disable
            WriteLiteral("<button type=\"button\" class=\"btn2 btn-darkpurple\" ");
            WriteLiteral("@click=\"saveData()\">등록</button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\r\n");
#nullable restore
#line 65 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("        <button type=\"button\" class=\"btn2 btn-gray\" ");
            WriteLiteral(@"@click=""goList()"">목록</button>
    </div>
</div>
<!-- content -->
<script>

                let appModule = new Vue({
                    el: '#appModule',
                    data: {
                        pageInit: false,
                        selectYearhakgi: null,
                        selectSubj: null,
                        selectChangeYn: 'Y',
                        masterNo: '5',
                        searchCondition: 'ALL',
                        searchKeyword: '',
                        boardNo: '',
");
#nullable restore
#line 83 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
                         if (ViewData["command"].Equals("UPDATE")) {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            ");
            WriteLiteral(" title: \'");
#nullable restore
#line 84 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
                                  Write(ViewBag.result["TITLE"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\',\r\n                            ");
            WriteLiteral(" content: \'");
#nullable restore
#line 85 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
                                    Write(Html.Raw(@ViewBag.result["CONTENTS"]));

#line default
#line hidden
#nullable disable
            WriteLiteral("\',\r\n                            ");
            WriteLiteral(" atchFileId: \'");
#nullable restore
#line 86 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
                                       Write(ViewBag.result["DOC_ID"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\',\r\n");
#nullable restore
#line 87 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
                        } else {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            ");
            WriteLiteral(" title: null,\r\n                            ");
            WriteLiteral(" content: null,\r\n                            ");
            WriteLiteral(" atchFileId: null,\r\n");
#nullable restore
#line 91 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        othbcAt: \'Y\',\r\n                        preOthbcAt: \'Y\',\r\n                        storageId: \'1000\',\r\n                        cmd: \'insert\',\r\n                        sortOrdr: null,\r\n                        upperNo: null,\r\n");
#nullable restore
#line 98 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
                         if (ViewData["command"].Equals("UPDATE")) {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            ");
            WriteLiteral("bbs_id: \'");
#nullable restore
#line 99 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
                                  Write(ViewBag.result["BBS_ID"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\'\r\n");
#nullable restore
#line 100 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    },
                    mounted: function () {
                        let self = this;

                        appEvents.$on('eventSubjChange', function (param) {

                            self.selectYearhakgi = param.selectYearhakgi;
                            self.selectSubj = param.selectSubj;

                            if (!self.pageInit) {

                                self.pageInit = true;


                            } else {

                                self.searchCondition = 'ALL';
                                self.searchKeyword = '';

                                linkUrl('BoardQnaListStdPage.do', self.$data);
                            }
                        });
                    },
                    methods: {
                        saveData: function () {
                            this.$refs.attach.upload();
                        },
                        uploadComplete: function (attachId) {
                       ");
            WriteLiteral(@"     this.atchFileId = attachId;


                            oEditors.getById[""content""].exec(""UPDATE_CONTENTS_FIELD"", []);


                            this.content = $('#content').val();
                            this.content = this.content.replace(/(?:\r\n|\r|\n)/g, '<br/>');
                            if (this.title == null) {
                                alert('제목은 반드시 입력해주세요');
                                $(""#title"").focus();
                                return false;
                            }

                            axios.post('");
#nullable restore
#line 143 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
                                   Write(ViewBag.Insert);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
                                {
                                    'selectSubj': this.$data.selectSubj,
                                    'selectChangeYn': this.$data.selectChangeYn,
                                    'title': this.$data.title,
                                    'othbcAt': this.$data.othbcAt,
                                    'content': this.$data.content,
                                    'atchFileId': this.$data.atchFileId,
                                    'storageId': this.$data.storageId,
                                    'cmd': this.$data.cmd,
");
#nullable restore
#line 153 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
                                      if (ViewData["command"].Equals("UPDATE")) {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        ");
            WriteLiteral("\'bbs_id\': this.$data.bbs_id\r\n");
#nullable restore
#line 155 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                 })\r\n                                .then(function (response) {\r\n");
#nullable restore
#line 158 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
                                     if(ViewData["command"].Equals("UPDATE")) {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        ");
            WriteLiteral("alert(\"수정되었습니다.\");\r\n                                        ");
            WriteLiteral("goDetail(\'");
#nullable restore
#line 160 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
                                               Write(ViewBag.result["BBS_ID"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\');\r\n");
#nullable restore
#line 161 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
                                    } else {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        ");
            WriteLiteral("alert(\"등록되었습니다.\");\r\n                                        ");
            WriteLiteral("linkUrl(\'");
#nullable restore
#line 163 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
                                              Write(ViewBag.SelectPageList);

#line default
#line hidden
#nullable disable
            WriteLiteral("\', this.$data);\r\n");
#nullable restore
#line 164 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                });\r\n                        },\r\n\r\n                        getData: function (param) {\r\n                            axios.post(\'");
#nullable restore
#line 169 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
                                   Write(ViewBag.Select);

#line default
#line hidden
#nullable disable
            WriteLiteral("\', param)\r\n                                .then(function (response) {\r\n                                }.bind(this));\r\n                        },\r\n                        goList: function () {\r\n                            linkUrl(\'");
#nullable restore
#line 174 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
                                Write(ViewBag.SelectPageList);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"', this.$data);
                        }
                    }
                });

</script>

<script type=""text/javascript"">
                //SmartEditor :항상 Textarea보다 아래에 이 스크립트가 있어야 합니다.
                var oEditors = [];
                var oContents = new Array(1);
                oContents[0] = ""content"";

                $(function () {
");
#nullable restore
#line 188 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
                     if(ViewData["command"].Equals("UPDATE")) {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        ");
            WriteLiteral("$(\"#title\").val(\'");
#nullable restore
#line 189 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
                                      Write(ViewBag.result["TITLE"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\');\r\n                        ");
            WriteLiteral("$(\"#content\").html(\'");
#nullable restore
#line 190 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
                                         Write(Html.Raw(@ViewBag.result["CONTENTS"]));

#line default
#line hidden
#nullable disable
            WriteLiteral("\');\r\n");
#nullable restore
#line 191 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    $(\".btnCon\").find(\"button\").eq(1).click(function () {\r\n                        $(\"#__form__\").attr(\"action\", \"");
#nullable restore
#line 194 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
                                                  Write(ViewBag.SelectPageList);

#line default
#line hidden
#nullable disable
            WriteLiteral("\");\r\n                        $(\"#__form__\").submit();\r\n                    });\r\n\r\n                    $(\"[name=selectSubj]\").change(function () {\r\n                        $(\"#__form__\").attr(\"action\", \"");
#nullable restore
#line 199 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
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

                function goDetail(bbs_id) {
                    var input = ""<input type='hidden' name='bbs_id' />"";
                    $(""#__form__"").append(input);

                    $(""[name=bbs_id]"").val(bbs_id);

                    $(""#__form__"").attr(""action"", """);
#nullable restore
#line 214 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
                                              Write(ViewBag.Select);

#line default
#line hidden
#nullable disable
            WriteLiteral("\");\r\n                    $(\"#__form__\").submit();\r\n                }\r\n</script>\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "59546c245645c2eb161c8a724283d0cefc29207522027", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "59546c245645c2eb161c8a724283d0cefc29207523233", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n\r\n");
#nullable restore
#line 224 "C:\Users\djfwk\source\repos\ApplicationSoftware\AREA1\Views\LctSport\BoardQnaWriteStdPage.cshtml"
Write(Html.Partial("~/Views/Shared/LctSportFooter.cshtml"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
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
