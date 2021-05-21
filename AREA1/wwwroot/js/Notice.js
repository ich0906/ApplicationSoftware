function goPage(page) {
    $("[name=page]").val(page);

    $("#__form__").attr("action", "/Notice/SelectPageListNotice");
    $("#__form__").submit();
}
function goDetail(bbs_id) {
    var input = "<input type='hidden' name='bbs_id' />";
    $("#__form__").append(input);

    $("[name=bbs_id]").val(bbs_id);

    $("#__form__").attr("action", "/Notice/SelectNotice");
    $("#__form__").submit();
}


$(function () {
    $(".btnCon").find("button").click(function () {
        $("#__form__").attr("action", "/Notice/InsertFormNotice");
        $("#__form__").submit();
    });

    $("[name=selectSubj]").change(function () {
        $("#__form__").attr("action", "/Notice/SelectPageListNotice");
        $("#__form__").submit();
    });

    $("[name=selectYearhakgi]").change(function () {
        $("[name=selectSubj]").trigger('change');
    });
});