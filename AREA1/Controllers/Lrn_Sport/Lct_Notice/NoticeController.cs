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

namespace AREA1.Controllers.Lrn_Sport.Lct_Notice
{
    [LoginActionFilter]
    public class NoticeController : Controller {
        private readonly ILogger<NoticeController> _logger;
        private readonly AppSoftDbContext _context;
        private readonly CommonDao _commonDao;
        private readonly CodeMngTool _codeMngTool;
        private readonly FileMngTool _fileMngTool;
        public NoticeController(ILogger<NoticeController> logger, AppSoftDbContext context) {
            _logger = logger;
            _context = context;
            _commonDao = new CommonDao(context);
            _codeMngTool = new CodeMngTool(context);
            _fileMngTool = new FileMngTool(context);
        }

        /*
         * 강의 공지사항 리스트 페이지
         * 작성자 : 김정원
         * 기능 : 공지사항 리스트 페이지 호출
         * */
        public IActionResult SelectPageListNotice()
        {
            Dictionary<string, string> param = new Dictionary<string, string>();

            // User 정보 파싱
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");
            ViewData["name"] = userInfo.name;                                       // 이름
            ViewData["user_id"] = userInfo.user_id;                                 // 유저 ID(학번)
            ViewData["fs_at"] = userInfo.author.Equals(_codeMngTool.getCode("AUTHOR", "PROFESSOR")) ? "Y" : "N";         // 교수 여부
            ViewData["pageNm"] = "강의 공지사항";

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
            if (Request.HasFormContentType && !Request.Form["selectedSubj"].ToString().Equals(""))
            {
                param.Add("page", Request.Form["page"]);

                bbsCnt = Convert.ToInt32(_commonDao.SelectOne(sql, Request.Form)["BBS_CNT"]);

                ViewBag.YEAR_HAKGI = Request.Form["selectedYearhakgi"];
                ViewBag.ACDMC_NO = Request.Form["selectedSubj"];

                // Form이 없거나 과목을 선택하지 않고 공지사항 페이지에 넘어오는 경우
            }
            else
            {
                // 디폴트 과목을 선택함
                string sql2 = userInfo.author.Equals(_codeMngTool.getCode("AUTHOR", "PROFESSOR"))
                    ? "SELECT * FROM (SELECT ROWNUM, AA.* FROM (SELECT B.ACDMC_NO AS selectedSubj, YEAR || ',' || SEMESTER AS YEAR_HAKGI"
                    + $" FROM OP_TEACHES A NATURAL JOIN OP_SECTION B WHERE A.ID = '{userInfo.user_id}' ORDER BY YEAR DESC, SEMESTER DESC, COURSE_ID) AA) AAA WHERE ROWNUM = 1"
                    : "SELECT * FROM (SELECT ROWNUM, AA.* FROM (SELECT B.ACDMC_NO AS selectedSubj, YEAR || ',' || SEMESTER AS YEAR_HAKGI"
                    + $" FROM OP_TAKES A NATURAL JOIN OP_SECTION B WHERE A.ID = '{userInfo.user_id}' ORDER BY YEAR DESC, SEMESTER DESC, COURSE_ID) AA) AAA WHERE ROWNUM = 1";

                param = _commonDao.SelectOne(sql2);
                param.Add("selectedSubj", param["SELECTEDSUBJ"]);
                param.Add("page", "1");

                bbsCnt = Convert.ToInt32(_commonDao.SelectOne(sql, param)["BBS_CNT"]);

                ViewBag.YEAR_HAKGI = param["YEAR_HAKGI"];
                ViewBag.ACDMC_NO = param["selectedSubj"];
            }


            // 만약 조회된 공지사항이 있으면 값을 가져온다.
            if (bbsCnt > 0)
            {
                sql = "SELECT *                                                                         "
                    + "FROM(SELECT ROWNUM AS RNUM, TITLE, REGISTER, REGIST_DT, RDCNT, BBS_ID                                  "
                    + "      FROM(SELECT A.TITLE,                                                                             "
                    + "                   B.NAME AS REGISTER,                                                                 "
                    + "                   A.REGIST_DT,                                                                        "
                    + "                   A.RDCNT,                                                                            "
                    + "                   A.BBS_ID                                                                            "
                    + "            FROM OP_BBS A                                                                              "
                    + "                     JOIN OP_USER B                                                                    "
                    + "                          ON A.REGISTER = B.USER_ID                                                    "
                    + "            WHERE BBS_CODE = '1000'                                                                    "
                    + "              AND ACDMC_NO = @selectedSubj:VARCHAR                                                     "
                    + "            ORDER BY BBS_ID DESC, REGIST_DT DESC) AA) AAA WHERE 1 = 1                                  "
                    + (param.ContainsKey("page") ? "AND RNUM > " + (Convert.ToInt32(param["page"]) - 1) + " * 10 " : "AND RNUM > 0 ")
                    + (param.ContainsKey("page") ? "AND RNUM <= " + param["page"] + "0" : "AND RNUM <= 10");

                // Form이 존재하지 않으면 오류가 나기 때문에 분기해주어야한다.
                if (Request.HasFormContentType)
                {
                    var resultList = _commonDao.SelectList(sql, Request.Form);
                    ViewBag.ResultList = resultList;
                    // Form이 없거나 과목을 선택하지 않고 공지사항 페이지에 넘어오는 경우
                }
                else
                {
                    var resultList = _commonDao.SelectList(sql, param);
                    ViewBag.ResultList = resultList;
                }

            }

            ViewBag.ResultCnt = bbsCnt;
            ViewBag.param = param;
            ViewBag.SelectPageList = "/Notice/SelectPageListNotice";
            ViewBag.Select = "/Notice/SelectNotice";
            ViewBag.InsertForm = "/Notice/InsertFormNotice";

            return View("/Views/LctSport/BoardListStdPage.cshtml");
        }

        public IActionResult SelectNotice()
        {
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");
            Dictionary<string, string> param = new Dictionary<string, string>();

            ViewData["name"] = userInfo.name;
            ViewData["user_id"] = userInfo.user_id;
            ViewData["pageNm"] = "강의 공지사항";
            ViewData["fs_at"] = userInfo.author.Equals(_codeMngTool.getCode("AUTHOR", "PROFESSOR")) ? "Y" : "N";
            ViewBag.ACDMC_NO = Request.Form["selectedSubj"];
            ViewBag.YEAR_HAKGI = Request.Form["selectedYearhakgi"];


            // 조회수 증가 쿼리
            string sql = "UPDATE OP_BBS SET RDCNT = (SELECT RDCNT+1 AS RDCNT FROM OP_BBS WHERE BBS_ID = @BBS_ID:VARCHAR) WHERE BBS_ID = @BBS_ID:VARCHAR";

            //cud 처리할 때는 트랜잭션 시작해주어야함
            using var transaction = _context.Database.BeginTransaction();

            _commonDao.Update(sql, Request.Form);

            transaction.Commit();

            param.Add("page", Request.Form["page"]);

            sql = "SELECT * FROM ("
                       + "SELECT A.ACDMC_NO AS SelectSubj"
                       + ", A.TITLE"
                       + ", A.OTHBC_AT"
                       + ", A.CONTENTS"
                       + ", A.REGIST_DT "
                       + ", B.NAME "
                       + ", A.BBS_ID "
                       + ", A.RDCNT "
                       + ", DOC_ID "
                       + ", LEAD(BBS_ID) OVER(ORDER BY BBS_ID) AS NEXT_ID "
                       + ", LEAD(TITLE) OVER(ORDER BY BBS_ID) AS NEXT_TITLE "
                       + ", LAG(BBS_ID) OVER(ORDER BY BBS_ID) AS PREV_ID "
                       + ", LAG(TITLE) OVER(ORDER BY BBS_ID) AS PREV_TITLE "
                       + "FROM OP_BBS A "
                       + "JOIN OP_USER B "
                       + "ON A.REGISTER = B.USER_ID "
                       + "WHERE 1=1 "
                       + "AND ACDMC_NO = @selectedSubj:VARCHAR "
                       + "AND BBS_CODE = '" + _codeMngTool.getCode("BBS", "NOTICE") + "' "
                       + "ORDER BY BBS_ID DESC)"
                       + "WHERE BBS_ID = @BBS_ID:VARCHAR";

            var result = _commonDao.SelectOne(sql, Request.Form);

            ViewBag.result = result;
            ViewBag.param = param;
            ViewBag.Select = "/Notice/SelectNotice";
            ViewBag.SelectPageList = "/Notice/SelectPageListNotice";
            ViewBag.UpdateForm = "/Notice/UpdateFormNotice";
            ViewBag.Delete = "/Notice/DeleteNotice";

            int fcount = 0;
            //첨부파일 읽어오기
            if (result["DOC_ID"] != "")
            {
                sql = "SELECT FILE_NAME,FILE_EXTSN,FILE_ID FROM OP_FILE A JOIN OP_BBS B ON A.DOC_ID=B.DOC_ID"
               + $" WHERE A.DOC_ID='{result["DOC_ID"]}'";

                var fileList = _commonDao.SelectList(sql);
                fcount = fileList.Count;
                ViewBag.fileList = fileList;
            }

            ViewBag.fileCount = fcount;

            return View("/Views/LctSport/BoardViewStdPage.cshtml");
        }

        /*
         * 강의 공지사항 작성 페이지
         * 작성자 : 김정원
         * 기능 : 공지사항 작성 페이지 호출
         * */
        public IActionResult InsertFormNotice()
        {
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");
            ViewData["name"] = userInfo.name;
            ViewData["user_id"] = userInfo.user_id;
            ViewData["pageNm"] = "강의 공지사항";
            ViewData["command"] = "INSERT";

            Dictionary<string, string> param = new Dictionary<string, string>();

            ViewBag.ACDMC_NO = Request.Form["selectedSubj"];
            ViewBag.YEAR_HAKGI = Request.Form["selectedYearhakgi"];

            if (!userInfo.author.Equals(_codeMngTool.getCode("AUTHOR", "PROFESSOR")))
            {
                Response.WriteAsync("<script language=\"javascript\">alert('잘못된 권한입니다.');</script>");
                Response.WriteAsync("<script language=\"javascript\">window.location=\"Main\"</script>");
            }

            param.Add("page", Request.Form["page"]);
            ViewBag.param = param;

            ViewBag.SelectPageList = "/Notice/SelectPageListNotice";
            ViewBag.Select = "/Notice/SelectNotice";
            ViewBag.InsertForm = "/Notice/InsertFormNotice";
            ViewBag.Insert = "/Notice/InsertNotice";


            return View("/Views/LctSport/BoardQnaWriteStdPage.cshtml");
        }


        /*
         * 강의 공지사항 작성
         * 작성자 : 김정원
         * 기능 : 공지사항 작성 로직
         * */
        [HttpPost]
        public string InsertNotice([FromBody] Notice notice)
        {
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");
            Dictionary<string, string> param = new Dictionary<string, string>();

            // Notice 데이터 파싱
            param.Add("SelectSubj", notice.SelectSubj);
            param.Add("Title", notice.Title);
            param.Add("OthbcAt", notice.OthbcAt);
            param.Add("Content", notice.Content);
            param.Add("AtchFileId", notice.AtchFileId);
            param.Add("user_id", userInfo.user_id);

            string query = "";

            query = "INSERT INTO OP_BBS " +
                    "VALUES(NOTICE_SEQ.NEXTVAL" +
                    ", @SelectSubj:VARCHAR" +
                    ", '1000'" +
                    ", @Title:VARCHAR" +
                    ", TO_CHAR(SYSDATE, 'yyyy/mm/dd hh:mi')" +
                    ", 0" +
                    ", @Content:VARCHAR" +
                    ", @user_id:VARCHAR" +
                    ", ''" +
                    ", @OthbcAt:VARCHAR" +
                    ", NULL" +
                    ", NULL" +
                    ", NULL)";

            //cud 처리할 때는 트랜잭션 시작해주어야함
            using var transaction = _context.Database.BeginTransaction();

            _commonDao.Insert(query, param);

            transaction.Commit();

            return "ok";
        }
        public IActionResult UpdateFormNotice()
        {
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");
            ViewData["name"] = userInfo.name;
            ViewData["user_id"] = userInfo.user_id;
            ViewData["pageNm"] = "강의 공지사항";
            ViewData["command"] = "UPDATE";

            Dictionary<string, string> param = new Dictionary<string, string>();

            ViewBag.ACDMC_NO = Request.Form["selectedSubj"];
            ViewBag.YEAR_HAKGI = Request.Form["selectedYearhakgi"];

            if (!userInfo.author.Equals(_codeMngTool.getCode("AUTHOR", "PROFESSOR")))
            {
                Response.WriteAsync("<script language=\"javascript\">alert('잘못된 권한입니다.');</script>");
                Response.WriteAsync("<script language=\"javascript\">window.location=\"Main\"</script>");
            }

            param.Add("page", Request.Form["page"]);

            string sql = "SELECT * FROM ("
                        + "SELECT A.ACDMC_NO AS SelectSubj"
                        + ", A.TITLE"
                        + ", A.OTHBC_AT"
                        + ", A.CONTENTS"
                        + ", A.REGIST_DT "
                        + ", B.NAME "
                        + ", A.BBS_ID "
                        + ", A.RDCNT "
                        + ", A.DOC_ID "
                        + ", LEAD(BBS_ID) OVER(ORDER BY BBS_ID) AS NEXT_ID "
                        + ", LEAD(TITLE) OVER(ORDER BY BBS_ID) AS NEXT_TITLE "
                        + ", LAG(BBS_ID) OVER(ORDER BY BBS_ID) AS PREV_ID "
                        + ", LAG(TITLE) OVER(ORDER BY BBS_ID) AS PREV_TITLE "
                        + "FROM OP_BBS A "
                        + "JOIN OP_USER B "
                        + "ON A.REGISTER = B.USER_ID "
                        + "WHERE 1=1 "
                        + "AND ACDMC_NO = @selectedSubj:VARCHAR "
                        + "AND BBS_CODE = '" + _codeMngTool.getCode("BBS", "NOTICE") + "' "
                        + "ORDER BY BBS_ID DESC)"
                        + "WHERE BBS_ID = @BBS_ID:VARCHAR";

            var result = _commonDao.SelectOne(sql, Request.Form);

            ViewBag.result = result;
            ViewBag.param = param;

            ViewBag.SelectPageList = "/Notice/SelectPageListNotice";
            ViewBag.Select = "/Notice/SelectNotice";
            ViewBag.InsertForm = "/Notice/InsertFormNotice";
            ViewBag.Insert = "/Notice/UpdateNotice";

            return View("/Views/LctSport/BoardQnaWriteStdPage.cshtml");
        }

        public string UpdateNotice([FromBody] Notice notice)
        {
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");
            Dictionary<string, string> param = new Dictionary<string, string>();

            // Notice 데이터 파싱
            param.Add("SelectSubj", notice.SelectSubj);
            param.Add("Title", notice.Title);
            param.Add("OthbcAt", notice.OthbcAt);
            param.Add("Content", notice.Content);
            param.Add("bbs_id", notice.Bbs_id);
            param.Add("AtchFileId", notice.AtchFileId);
            param.Add("user_id", userInfo.user_id);
            string query = "";

            query = "UPDATE OP_BBS SET " +
                    "TITLE = @Title:VARCHAR" +
                    ", UPDATE_DT = TO_CHAR(SYSDATE, 'yyyy/mm/dd hh:mi')" +
                    ", CONTENTS = @Content:VARCHAR" +
                    ", UPDUSR = @user_id:VARCHAR" +
                    ", DOC_ID = @AtchFileId:VARCHAR" +
                    ", OTHBC_AT = @OthbcAt:VARCHAR WHERE BBS_ID = @bbs_id:NUMBER";

            //cud 처리할 때는 트랜잭션 시작해주어야함
            using var transaction = _context.Database.BeginTransaction();

            _commonDao.Update(query, param);

            transaction.Commit();

            return "ok";
        }
        public string DeleteNotice()
        {
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");

            if (!userInfo.author.Equals(_codeMngTool.getCode("AUTHOR", "PROFESSOR")))
            {
                Response.WriteAsync("<script language=\"javascript\">alert('잘못된 권한입니다.');</script>");
                Response.WriteAsync("<script language=\"javascript\">window.location=\"/Notice/SelectPageListNotice\"</script>");
            }

            string query = "";

            query = "SELECT FILE_ID, A.DOC_ID FROM OP_FILE A "
                + "JOIN OP_BBS B "
                + "ON A.DOC_ID=B.DOC_ID "
                + "AND BBS_ID='" + Request.Form["bbs_id"] + "'";
            var removeFiles = _commonDao.SelectList(query);
            for (int i = 0; i < removeFiles.Count; ++i)
            {
                _fileMngTool.removeFile(removeFiles[i]["DOC_ID"]);
            }

            query = "DELETE FROM OP_BBS WHERE BBS_ID = @bbs_id:NUMBER";

            //cud 처리할 때는 트랜잭션 시작해주어야함
            using var transaction = _context.Database.BeginTransaction();

            string resultCode = "ok";

            if (_commonDao.Delete(query, Request.Form) == 0)
            {
                resultCode = "false";
            }

            transaction.Commit();

            return resultCode;
        }

        // 공지사항 전용 모델 public으로 선언해야 매개변수로 사용가능함
        public class Notice
        {
            public string SelectSubj { get; set; }          // 학정번호
            public string Title { get; set; }               // 제목
            public string OthbcAt { get; set; }             // 공개여부(중요여부)
            public string Content { get; set; }             // 내용
            public string AtchFileId { get; set; }          // 첨부파일 ID
            public string Bbs_id { get; set; }              // 게시판 ID
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}