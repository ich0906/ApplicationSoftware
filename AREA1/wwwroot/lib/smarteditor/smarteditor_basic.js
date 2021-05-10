/**
 * Description: SmartEditor display
 * Version : 1.0.0.
 * Date    : 2019.05.27
 * Writer  : haengho.kang
 */

// SmartEditor Script Start
//var oEditors = [];

function setEditor(n) {
	nhn.husky.EZCreator.createInIFrame({
		oAppRef: oEditors,
		elPlaceHolder: oContents[n],
		sSkinURI: "/lib/smarteditor/SmartEditor2Skin.html",	
		htParams : {
			bUseToolbar : true,				// 툴바 사용 여부 (true:사용/ false:사용하지 않음)
			bUseVerticalResizer : true,		// 입력창 크기 조절바 사용 여부 (true:사용/ false:사용하지 않음)
			bUseModeChanger : true,			// 모드 탭(Editor | HTML | TEXT) 사용 여부 (true:사용/ false:사용하지 않음)
			bSkipXssFilter : true,	
			I18N_LOCALE : "ko_KR"
		},
		fCreator: "createSEditor2"
	});
	
}	

$(function(){
	for (var i = 0; i < oContents.length; i++) {
	    setEditor(i);
	}
});

/**
 * 이미지 업로드 이후 Editor에 이미지 나오게 처리함
 * haengho.kang 2019.04.18
 * @param filepath
 * @returns
 */
function setImagePasteHTML(filepath,id){
    var sHTML = '<img src="'+filepath+'">';
    oEditors.getById[id].exec("PASTE_HTML", [sHTML]);
}