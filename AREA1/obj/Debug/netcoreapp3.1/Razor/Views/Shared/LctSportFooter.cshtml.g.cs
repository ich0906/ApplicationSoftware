#pragma checksum "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\Shared\LctSportFooter.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c465f08cfee85012d319e771771283a739507806"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_LctSportFooter), @"mvc.1.0.view", @"/Views/Shared/LctSportFooter.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c465f08cfee85012d319e771771283a739507806", @"/Views/Shared/LctSportFooter.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dbabdacff04c265f312ce9c282b57090f72e3e37", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_LctSportFooter : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"</section>

<script>

    let appSelectSubj = new Vue({
        el: '#appSelectSubj',
        data: {
            yearhakgiList: [],
            yearhakgis: [],
            subjList: [],
            selectYearhakgi: null,
            selectSubj: null,
            selectedYearhakgi: '");
#nullable restore
#line 13 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\Shared\LctSportFooter.cshtml"
                           Write(ViewBag.YEAR_HAKGI);

#line default
#line hidden
#nullable disable
            WriteLiteral("\',\r\n            selectedSubj: \'");
#nullable restore
#line 14 "C:\Users\smart\Desktop\homework\응소실\ApplicationSoftware\AREA1\Views\Shared\LctSportFooter.cshtml"
                      Write(ViewBag.ACDMC_NO);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"'
        },
        mounted: function () {
            this.getYearhakgiAtnlcSbjectList(this.selectedYearhakgi, this.selectedSubj);
        },
        methods: {
            getYearhakgiAtnlcSbjectList: function (selectedYearhakgi, selectedSubj) {
                axios.post('/Main/YearhakgiAtnlcSbjectList', {})
                    .then(function (response) {
                        this.yearhakgiList = response.data;

                        let selectedIndex = 0;
                        for (let i = 0; i < this.yearhakgiList.length; i++) {
                            if (this.yearhakgiList[i][""subjList""][this.yearhakgiList[i][""subjList""][""length""] - 1]['YEAR_HAKGI'] === selectedYearhakgi) {
                                selectedIndex = i;
                                break;
                            }
                        }

                        this.selectYearhakgi = selectedIndex;

                        this.onYearhakgiChange(selectedSubj);
                    }.bind(t");
            WriteLiteral(@"his));
            },
            onYearhakgiChange: function (selectedSubj) {
                let selectedIndex = 0;

                for (let yearhakgi in this.yearhakgiList) {
                    if (selectedSubj != undefined)
                        this.yearhakgis.push(this.yearhakgiList[yearhakgi]['subjList'].pop(this.yearhakgiList[yearhakgi]['subjList'].length - 1));

                    if (this.yearhakgiList[yearhakgi] === this.yearhakgiList[this.selectYearhakgi] || (selectedSubj == undefined && yearhakgi == this.yearhakgiList[this.selectYearhakgi])) {
                        this.subjList = this.yearhakgiList[yearhakgi]['subjList'];
                        this.selectYearhakgi = yearhakgi;

                        for (let j = 0; j < this.subjList.length; j++) {
                            if (this.subjList[j][""ACDMC_NO""] === selectedSubj) {
                                selectedIndex = j;
                                break;
                            }
                      ");
            WriteLiteral(@"  }
                    }
                }

                this.selectSubj = this.subjList[selectedIndex];
                this.onSubjChange();
            },
            onSubjChange: function () {

                setLctrumInfo('', '' + this.selectSubj[""YEAR""] + ',' + this.selectSubj[""SEMESTER""], this.selectSubj[""ACDMC_NO""]);

                appEvents.$emit('eventSubjChange', {
                    selectYearhakgi: this.selectSubj[""YEAR""] + ',' + this.selectSubj[""SEMESTER""],
                    selectSubj: this.selectSubj[""ACDMC_NO""],
                    selectChangeYn: 'Y',
                    subjNm: this.selectSubj[""LABEL""],
                    yearhakgi: this.selectYearhakgi,
                    subj: this.selectSubj
                });
            }
        }
    });
</script>");
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
