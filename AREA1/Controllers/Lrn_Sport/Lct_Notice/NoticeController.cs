using AREA1.Data;
using AREA1.Filters;
using AREA1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Tool;

namespace AREA1.Controllers.Lrn_Sport.Lct_Notice {
    [LoginActionFilter]
    public class NoticeController : Controller {
        private readonly ILogger<NoticeController> _logger;
        private readonly AppSoftDbContext _context;
        private readonly CommonDao _commonDao;

        public NoticeController(ILogger<NoticeController> logger, AppSoftDbContext context) {
            _logger = logger;
            _context = context;
            _commonDao = new CommonDao(context);
        }

        /*
         * 강의 공지사항 리스트 페이지
         * 작성자 : 김정원
         * */
        public IActionResult SelectPageListNotice() {
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");
            ViewData["name"] = userInfo.name;
            ViewData["user_id"] = userInfo.user_id;
            ViewData["fs_at"] = userInfo.author.Equals("1000") ? "Y" : "N";

            string sql = "";


            int bbsCnt = 0;
            if (Request.HasFormContentType) {
                if (!Request.Form["selectedSubj"].ToString().Equals("")) {
                    // 공지사항 개수 체크
                    sql = "SELECT COUNT(*) AS BBS_CNT "
                      + "FROM OP_BBS A "
                      + "JOIN OP_USER B "
                      + "ON A.REGISTER = B.USER_ID "
                      + "JOIN OP_FILE C "
                      + "ON A.DOC_ID = C.DOC_ID AND C.FILE_NUM = 1 "
                      + "WHERE BBS_CODE = 1000 " //+ CodeMngTool.getCode("BBS", "NOTICE") 
                      + "AND ACDMC_NO = @selectedSubj:VARCHAR";
                    bbsCnt = Convert.ToInt32(_commonDao.SelectOne(sql, Request.Form)["BBS_CNT"]);

                    ViewBag.ACDMC_NO = Request.Form["selectedSubj"];
                }
            } else {
                Dictionary<string, string> param = new Dictionary<string, string>();

                sql = $"SELECT B.ACDMC_NO FROM OP_TEACHES A NATURAL JOIN OP_SECTION B WHERE A.ID = '{userInfo.user_id}' AND semester='1' AND YEAR = '2021' AND ROWNUM = 1 ORDER BY COURSE_ID";

                param = _commonDao.SelectOne(sql);

                sql = "SELECT COUNT(*) AS BBS_CNT "
                      + "FROM OP_BBS A "
                      + "JOIN OP_USER B "
                      + "ON A.REGISTER = B.USER_ID "
                      + "JOIN OP_FILE C "
                      + "ON A.DOC_ID = C.DOC_ID AND C.FILE_NUM = 1 "
                      + "WHERE BBS_CODE = 1000 " //+ CodeMngTool.getCode("BBS", "NOTICE") 
                      + "AND ACDMC_NO = @ACDMC_NO:VARCHAR";

                bbsCnt = Convert.ToInt32(_commonDao.SelectOne(sql, param)["BBS_CNT"]);
                ViewBag.ACDMC_NO = param["ACDMC_NO"];
            }



            if (bbsCnt > 0) {
                sql = "SELECT A.SJ"                                                                                                                 // 게시글 제목
                                + ", (SELECT DECODE(COUNT(*), 0, 'N', 'Y') FROM OP_FILE WHERE DOC_ID = A.DOC_ID) AS FILE_AT"                        // 파일 여부
                                + ", (SELECT DECODE(COUNT(*), 0, '', FILE_ID) FROM OP_FILE WHERE DOC_ID = A.DOC_ID AND SNO = 1) AS FILE_ID"         // 첫번째 첨부파일 ID
                                + ", B.NAME AS REGISTER"                                                                                            // 작성자명
                                + ", A.REGIST_DT"                                                                                                   // 작성일
                                + ", A.RDCNT"                                                                                                       // 조회수
                          + "FROM OP_BBS A"
                          + "JOIN OP_USER B"
                          + "ON A.REGISTER = B.USER_ID"
                          + "WHERE BBS_CODE = 1000" //+ CodeMngTool.getCode("BBS", "NOTICE") 
                          + "AND LCTRE_SE = @LCTRE_SE:VARCHAR";

                var resultList = _commonDao.SelectList(sql, Request.Form);
                ViewBag.ResultList = resultList;
            }

            ViewBag.ResultCnt = bbsCnt;

            return View("/Views/LctSport/BoardListStdPage.cshtml");
        }

        public IActionResult SelectNotice() {
            return View();
        }

        public IActionResult InsertFormNotice() {
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");
            ViewData["name"] = userInfo.name;
            ViewData["user_id"] = userInfo.user_id;
            ViewBag.ACDMC_NO = Request.Form["selectedSubj"];

            //if(userInfo.author.Equals())

            return View("/Views/LctSport/BoardQnaWriteStdPage.cshtml");
        } 

        [HttpPost]
        public void InsertNotice([FromBody]Notice notice) {
            Object o = Response;

            return;
        }

        public IActionResult UpdateNotice() {
            return View();
        }
        public IActionResult DeleteNotice() {
            return View();
        }

        public class Notice {
            private string title { get; set; }
            private string content { get; set; }
            private string atchFileId { get; set; }
            private string boardNo { get; set; }
            private string cmd { get; set; }
            private string masterNo { get; set; }
            private string othbcAt { get; set; }
            private string pageInit { get; set; }
            private string preOthbcAt { get; set; }
            private string searchCondition { get; set; }
            private string selectChangeYn { get; set; }
            private string searchKeyword { get; set; }
            private string selectSubj { get; set; }
            private string sortOrdr { get; set; }
            private string storageId { get; set; }
            private string upperNo { get; set; }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
