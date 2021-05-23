using AREA1.Data;
using AREA1.Filters;
using AREA1.Models;
using AREA1.Tool;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Tool;

namespace AREA1.Controllers.Lrn_Sport.Lct_Notice {
    [LoginActionFilter]
    public class StandInqireController : Controller {
        private readonly ILogger<StandInqireController> _logger;
        private readonly AppSoftDbContext _context;
        private readonly CommonDao _commonDao;
        private readonly CodeMngTool _codeMngTool;

        public StandInqireController(ILogger<StandInqireController> logger, AppSoftDbContext context) {
            _logger = logger;
            _context = context;
            _commonDao = new CommonDao(context);
            _codeMngTool = new CodeMngTool(context);
        }

        /*
         * 강의 공지사항 리스트 페이지
         * 작성자 : 김정원
         * 기능 : 공지사항 리스트 페이지 호출
         * */
        public IActionResult SelectPageListNotice() {
            Dictionary<string, string> param = new Dictionary<string, string>();

            // User 정보 파싱
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");
            ViewData["name"] = userInfo.name;                                       // 이름
            ViewData["user_id"] = userInfo.user_id;                                 // 유저 ID(학번)
            ViewData["fs_at"] = userInfo.author.Equals(_codeMngTool.getCode("AUTHOR", "PROFESSOR")) ? "Y" : "N";         // 교수 여부

            string sql = "";

            // 공지사항 개수 체크
            sql = "SELECT COUNT(*) AS BBS_CNT "
                      + "FROM OP_BBS A "
                      + "JOIN OP_USER B "
                      + "ON A.REGISTER = B.USER_ID "
                      /*+ "JOIN OP_FILE C "
                      + "ON A.DOC_ID = C.DOC_ID AND C.FILE_NUM = 1 "*/
                      + "WHERE BBS_CODE = '" + _codeMngTool.getCode("BBS", "NOTICE") + "' " 
                      + "AND ACDMC_NO = @selectedSubj:VARCHAR";


            int bbsCnt = 0;
            // Form이 존재하지 않으면 오류가 나기 때문에 분기해주어야한다.
            if (Request.HasFormContentType && !Request.Form["selectedSubj"].ToString().Equals("")) {

                bbsCnt = Convert.ToInt32(_commonDao.SelectOne(sql, Request.Form)["BBS_CNT"]);

                ViewBag.YEAR_HAKGI = Request.Form["selectedYearhakgi"];
                ViewBag.ACDMC_NO = Request.Form["selectedSubj"];

            // Form이 없거나 과목을 선택하지 않고 공지사항 페이지에 넘어오는 경우
            } else {
                // 디폴트 과목을 선택함
                string sql2 = "SELECT B.ACDMC_NO AS selectedSubj, YEAR || ',' || SEMESTER AS YEAR_HAKGI"
                    + $" FROM OP_TEACHES A NATURAL JOIN OP_SECTION B WHERE A.ID = '{userInfo.user_id}' AND semester='1' AND YEAR = '2021' AND ROWNUM = 1 ORDER BY COURSE_ID";

                param = _commonDao.SelectOne(sql2);
                param.Add("selectedSubj", param["SELECTEDSUBJ"]);

                bbsCnt = Convert.ToInt32(_commonDao.SelectOne(sql, param)["BBS_CNT"]);

                ViewBag.YEAR_HAKGI = param["YEAR_HAKGI"];
                ViewBag.ACDMC_NO = param["selectedSubj"];
            }


            // 만약 조회된 공지사항이 있으면 값을 가져온다.
            if (bbsCnt > 0) {
                sql = "SELECT A.TITLE"                                                                                                                     // 게시글 제목
                                   /* + ", (SELECT DECODE(COUNT(*), 0, 'N', 'Y') FROM OP_FILE WHERE DOC_ID = A.DOC_ID) AS FILE_AT"                            // 파일 여부
                                    + ", (SELECT DECODE(COUNT(*), 0, '', FILE_ID) FROM OP_FILE WHERE DOC_ID = A.DOC_ID AND SNO = 1) AS FILE_ID"             // 첫번째 첨부파일 ID*/
                                    + ", B.NAME AS REGISTER"                                                                                                // 작성자명
                                    + ", A.REGIST_DT"                                                                                                       // 작성일
                                    + ", A.RDCNT "                                                                                                          // 조회수
                              + "FROM OP_BBS A "
                              + "JOIN OP_USER B "
                              + "ON A.REGISTER = B.USER_ID "
                              + "WHERE BBS_CODE = '" + _codeMngTool.getCode("BBS", "NOTICE") + "' "
                              + "AND ACDMC_NO = @selectedSubj:VARCHAR";

                // Form이 존재하지 않으면 오류가 나기 때문에 분기해주어야한다.
                if (Request.HasFormContentType) {
                    var resultList = _commonDao.SelectList(sql, Request.Form);
                    ViewBag.ResultList = resultList;
                // Form이 없거나 과목을 선택하지 않고 공지사항 페이지에 넘어오는 경우
                } else {
                    var resultList = _commonDao.SelectList(sql, param);
                    ViewBag.ResultList = resultList;
                }
            }

            ViewBag.ResultCnt = bbsCnt;

            return View("/Views/LctSport/BoardListStdPage.cshtml");
        }

        public IActionResult SelectNotice() {
            return View();
        }

        /*
         * 강의 공지사항 작성 페이지
         * 작성자 : 김정원
         * 기능 : 공지사항 작성 페이지 호출
         * */
        public IActionResult InsertFormNotice() {
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");
            ViewData["name"] = userInfo.name;
            ViewData["user_id"] = userInfo.user_id;
            ViewBag.ACDMC_NO = Request.Form["selectedSubj"];
            ViewBag.YEAR_HAKGI = Request.Form["selectedYearhakgi"];

            if (!userInfo.author.Equals(_codeMngTool.getCode("AUTHOR", "PROFESSOR"))) {
                Response.WriteAsync("<script language=\"javascript\">alert('잘못된 권한입니다.');</script>");
                Response.WriteAsync("<script language=\"javascript\">window.location=\"Main\"</script>");
            }

            return View("/Views/LctSport/BoardQnaWriteStdPage.cshtml");
        }


        /*
         * 강의 공지사항 작성
         * 작성자 : 김정원
         * 기능 : 공지사항 작성 로직
         * */
        [HttpPost]
        public string InsertNotice([FromBody]Notice notice) {
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");
            Dictionary<string, string> param = new Dictionary<string, string>();

            // Notice 데이터 파싱
            param.Add("SelectSubj", notice.SelectSubj);
            param.Add("SelectChangeYn", notice.SelectChangeYn);
            param.Add("Title", notice.Title);
            param.Add("OthbcAt", notice.OthbcAt);
            param.Add("Content", notice.Content);
            param.Add("AtchFileId", notice.SelectSubj);
            param.Add("StorageId", notice.SelectSubj);
            param.Add("Cmd", notice.SelectSubj);
            param.Add("user_id", userInfo.user_id);

            string query = "";

            query = "INSERT INTO OP_BBS " +
                    "VALUES(NOTICE_SEQ.NEXTVAL" +
                    ", @SelectSubj:VARCHAR" +
                    ", '1000'" +
                    ", @Title:VARCHAR" +
                    ", TO_CHAR(SYSDATE, 'yyyy/mm/dd')" +
                    ", 0" +
                    ", @Content:VARCHAR" +
                    ", @user_id:VARCHAR" +
                    ", ''" +
                    ", @OthbcAt:VARCHAR)";

            //cud 처리할 때는 트랜잭션 시작해주어야함
            using var transaction = _context.Database.BeginTransaction();

            _commonDao.Insert(query, param);

            transaction.Commit();

            return "ok";
        }

        public IActionResult UpdateNotice() {
            return View();
        }
        public IActionResult DeleteNotice() {
            return View();
        }

        // 공지사항 전용 모델 public으로 선언해야 매개변수로 사용가능함
        public class Notice {
            public string SelectSubj { get; set; }
            public string SelectChangeYn { get; set; }
            public string Title { get; set; }
            public string OthbcAt { get; set; }
            public string Content { get; set; }
            public string AtchFileId { get; set; }
            public string StorageId { get; set; }
            public string Cmd { get; set; }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
