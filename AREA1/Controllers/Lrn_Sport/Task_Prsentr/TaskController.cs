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

namespace AREA1.Controllers.Lrn_Sport.Task_Prsentr {
    [LoginActionFilter]
    public class TaskController : Controller {
        private readonly ILogger<TaskController> _logger;
        private readonly AppSoftDbContext _context;
        private readonly CommonDao _commonDao;
        private readonly CodeMngTool _codeMngTool;

        public TaskController(ILogger<TaskController> logger, AppSoftDbContext context) {
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
        public IActionResult SelectPageListTask() {
            Dictionary<string, string> param = new Dictionary<string, string>();

            // User 정보 파싱
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");
            ViewData["name"] = userInfo.name;                                       // 이름
            ViewData["user_id"] = userInfo.user_id;                                 // 유저 ID(학번)
            ViewData["fs_at"] = userInfo.author.Equals(_codeMngTool.getCode("AUTHOR", "PROFESSOR")) ? "Y" : "N";         // 교수 여부
            ViewData["pageNm"] = "과제 제출";

            string sql = "";

            // 공지사항 개수 체크
            sql = "SELECT COUNT(*) AS TASK_CNT "
                      + "FROM OP_TASK A "
                      + "JOIN OP_USER B "
                      + "ON A.REGISTER = B.USER_ID "
                      /*+ "JOIN OP_FILE C "
                      + "ON A.DOC_ID = C.DOC_ID AND C.FILE_NUM = 1 "*/
                      + "WHERE ACDMC_NO = @selectedSubj:VARCHAR";


            int bbsCnt = 0;
            // Form이 존재하지 않으면 오류가 나기 때문에 분기해주어야한다.
            if (Request.HasFormContentType && !Request.Form["selectedSubj"].ToString().Equals("")) {
                bbsCnt = Convert.ToInt32(_commonDao.SelectOne(sql, Request.Form)["TASK_CNT"]);

                ViewBag.YEAR_HAKGI = Request.Form["selectedYearhakgi"];
                ViewBag.ACDMC_NO = Request.Form["selectedSubj"];

                // Form이 없거나 과목을 선택하지 않고 공지사항 페이지에 넘어오는 경우
            } else {
                // 디폴트 과목을 선택함
                string sql2 = "SELECT B.ACDMC_NO AS selectedSubj, YEAR || ',' || SEMESTER AS YEAR_HAKGI"
                    + $" FROM OP_TEACHES A NATURAL JOIN OP_SECTION B WHERE A.ID = '{userInfo.user_id}' AND semester='1' AND YEAR = '2021' AND ROWNUM = 1 ORDER BY COURSE_ID";

                param = _commonDao.SelectOne(sql2);
                param.Add("selectedSubj", param["SELECTEDSUBJ"]);

                bbsCnt = Convert.ToInt32(_commonDao.SelectOne(sql, param)["TASK_CNT"]);

                ViewBag.YEAR_HAKGI = param["YEAR_HAKGI"];
                ViewBag.ACDMC_NO = param["selectedSubj"];
            }


            // 만약 조회된 공지사항이 있으면 값을 가져온다.
            if (bbsCnt > 0) {
                sql = "SELECT *                                                                                               "
                    + "FROM(SELECT ROWNUM AS RNUM, AA.*                                                                       "
                    + "      FROM(SELECT A.TITLE,                                                                             "
                    + "                   B.NAME AS REGISTER,                                                                 "
                    + "                   A.TASK_SEQ,                                                                         "
                    + "                   TO_CHAR(A.BEGIN_TMLMT, 'YYYY-MM-DD hh24:mi:ss') AS BEGIN_TMLMT,                     "
                    + "                   TO_CHAR(A.END_TMLMT, 'YYYY-MM-DD hh24:mi:ss')  AS END_TMLMT,                        "
                    + "                   TO_CHAR(A.BEGIN_ADIT_TMLMT, 'YYYY-MM-DD hh24:mi:ss')  AS BEGIN_ADIT_TMLMT,          "
                    + "                   TO_CHAR(A.END_ADIT_TMLMT, 'YYYY-MM-DD hh24:mi:ss')   AS END_ADIT_TMLMT,             "
                    + "                   CASE WHEN A.BEGIN_TMLMT <= SYSDATE THEN CASE WHEN A.END_TMLMT >= SYSDATE THEN  'Y' ELSE 'N' END ELSE 'N' END AS B1,    "
                    + "                   CASE WHEN A.BEGIN_ADIT_TMLMT <= SYSDATE THEN CASE WHEN A.END_ADIT_TMLMT >= SYSDATE THEN  'Y' ELSE 'N' END ELSE 'N' END AS B2    "
                    + "            FROM OP_TASK A                                                                             "
                    + "                     JOIN OP_USER B                                                                    "
                    + "                          ON A.REGISTER = B.USER_ID                                                    "
                    + "            WHERE ACDMC_NO = @selectedSubj:VARCHAR                                                     "
                    + "            ORDER BY TASK_SEQ DESC) AA) AAA WHERE 1 = 1                                                ";

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
            param.Add("page", "1");

            ViewBag.ResultCnt = bbsCnt;
            ViewBag.param = param;
            ViewBag.SelectPageList = "/Task/SelectPageListTask";
            ViewBag.Select = "/Task/SelectTask";
            ViewBag.InsertForm = "/Task/InsertFormTask";

            return View("/Views/LctSport/Task/BoardListTaskPage.cshtml");
        }

        public IActionResult SelectTask() {
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");
            Dictionary<string, string> param = new Dictionary<string, string>();

            ViewData["name"] = userInfo.name;
            ViewData["user_id"] = userInfo.user_id;
            ViewData["pageNm"] = "과제 제출";
            ViewData["fs_at"] = userInfo.author.Equals(_codeMngTool.getCode("AUTHOR", "PROFESSOR")) ? "Y" : "N";
            ViewBag.ACDMC_NO = Request.Form["selectedSubj"];
            ViewBag.YEAR_HAKGI = Request.Form["selectedYearhakgi"];


            param.Add("page", Request.Form["page"]);

            string sql = "SELECT A.ACDMC_NO AS SelectSubj"
                       + ", A.TITLE"
                       + ", A.CONTENT"
                       + ", A.PRESENT_FORM"
                       + ", A.FILE_CPCTY_LMT"
                       + ", TO_CHAR(A.BEGIN_TMLMT, 'YYYY-MM-DD hh24:mi:ss') AS BEGIN_TMLMT "
                       + ", TO_CHAR(A.END_TMLMT, 'YYYY-MM-DD hh24:mi:ss')  AS END_TMLMT "
                       + ", TO_CHAR(A.BEGIN_ADIT_TMLMT, 'YYYY-MM-DD hh24:mi:ss')  AS BEGIN_ADIT_TMLMT "
                       + ", TO_CHAR(A.END_ADIT_TMLMT, 'YYYY-MM-DD hh24:mi:ss')   AS END_ADIT_TMLMT "
                       + ", B.NAME "
                       + ", A.TASK_SEQ "
                       + "FROM OP_TASK A "
                       + "JOIN OP_USER B "
                       + "ON A.REGISTER = B.USER_ID "
                       + "AND ACDMC_NO = @selectedSubj:VARCHAR "
                       + "WHERE TASK_SEQ = @task_id:VARCHAR";

            var result = _commonDao.SelectOne(sql, Request.Form);

            ViewBag.result = result;
            ViewBag.param = param;
            ViewBag.Select = "/Task/SelectTask";
            ViewBag.SelectPageList = "/Task/SelectPageListTask";
            ViewBag.UpdateForm = "/Task/UpdateFormTask";
            ViewBag.Delete = "/Task/DeleteTask";

            return View("/Views/LctSport/Task/TaskViewPage.cshtml");
        }

        /*
         * 강의 공지사항 작성 페이지
         * 작성자 : 김정원
         * 기능 : 공지사항 작성 페이지 호출
         * */
        public IActionResult InsertFormTask() {
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");
            ViewData["name"] = userInfo.name;
            ViewData["user_id"] = userInfo.user_id;
            ViewData["fs_at"] = userInfo.author.Equals(_codeMngTool.getCode("AUTHOR", "PROFESSOR")) ? "Y" : "N";         // 교수 여부
            ViewData["pageNm"] = "과제 제출";
            ViewData["command"] = "INSERT";

            Dictionary<string, string> param = new Dictionary<string, string>();

            ViewBag.ACDMC_NO = Request.Form["selectedSubj"];
            ViewBag.YEAR_HAKGI = Request.Form["selectedYearhakgi"];

            if (!userInfo.author.Equals(_codeMngTool.getCode("AUTHOR", "PROFESSOR"))) {
                Response.WriteAsync("<script language=\"javascript\">alert('잘못된 권한입니다.');</script>");
                Response.WriteAsync("<script language=\"javascript\">window.location=\"Main\"</script>");
            }

            param.Add("page", Request.Form["page"]);
            ViewBag.param = param;

            ViewBag.SelectPageList = "/Task/SelectPageListTask";
            ViewBag.Select = "/Task/SelectTask";
            ViewBag.InsertForm = "/Task/InsertFormTask";
            ViewBag.Insert = "/Task/InsertTask";


            return View("/Views/LctSport/Task/TaskInsertStdPage.cshtml");
        }


        /*
         * 강의 공지사항 작성
         * 작성자 : 김정원
         * 기능 : 공지사항 작성 로직
         * */
        [HttpPost]
        public string InsertTask([FromBody] Task Task) {
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");
            Dictionary<string, string> param = new Dictionary<string, string>();

            // Task 데이터 파싱
            param.Add("SelectSubj", Task.SelectSubj);
            param.Add("Title", Task.Title);
            param.Add("Content", Task.Content);
            param.Add("Sdate", Task.Sdate);
            param.Add("Edate", Task.Edate);
            param.Add("Adit_sdate", Task.Adit_sdate);
            param.Add("Adit_edate", Task.Adit_edate);
            param.Add("AtchFileId", Task.AtchFileId);
            param.Add("PresentForm", Task.PresentForm);
            param.Add("FileCpctyLlmt", Task.FileCpctyLlmt);
            param.Add("user_id", userInfo.user_id);
            string query = "";

            query = "INSERT INTO OP_TASK " +
                    "VALUES(Task_SEQ.NEXTVAL" +
                    ", @SelectSubj:VARCHAR" +
                    ", @Title:VARCHAR" +
                    ", TO_DATE(@Sdate:VARCHAR, 'YYYYMMDDHH24MI')" +
                    ", TO_DATE(@Edate:VARCHAR, 'YYYYMMDDHH24MI')" +
                    ", TO_DATE(@Adit_sdate:VARCHAR, 'YYYYMMDDHH24MI')" +
                    ", TO_DATE(@Adit_edate:VARCHAR, 'YYYYMMDDHH24MI')" +
                    ", '1'" +
                    ", @Content:VARCHAR" +
                    ", @PresentForm:VARCHAR" +
                    ", ''" +
                    ", @FileCpctyLlmt:VARCHAR" +
                    ", @user_id:VARCHAR)";

            //cud 처리할 때는 트랜잭션 시작해주어야함
            using var transaction = _context.Database.BeginTransaction();

            _commonDao.Insert(query, param);

            transaction.Commit();

            return "ok";
        }
        public IActionResult UpdateFormTask() {
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");
            ViewData["name"] = userInfo.name;
            ViewData["user_id"] = userInfo.user_id;
            ViewData["pageNm"] = "과제 제출";
            ViewData["command"] = "UPDATE";

            Dictionary<string, string> param = new Dictionary<string, string>();

            ViewBag.ACDMC_NO = Request.Form["selectedSubj"];
            ViewBag.YEAR_HAKGI = Request.Form["selectedYearhakgi"];

            if (!userInfo.author.Equals(_codeMngTool.getCode("AUTHOR", "PROFESSOR"))) {
                Response.WriteAsync("<script language=\"javascript\">alert('잘못된 권한입니다.');</script>");
                Response.WriteAsync("<script language=\"javascript\">window.location=\"Main\"</script>");
            }

            param.Add("page", Request.Form["page"]);

            string sql = "SELECT A.ACDMC_NO AS SelectSubj"
                      + ", A.TITLE"
                      + ", A.CONTENT"
                      + ", A.PRESENT_FORM"
                      + ", A.FILE_CPCTY_LMT"
                      + ", TO_CHAR(A.BEGIN_TMLMT, 'MONTH DD, YYYY hh24:mi:ss', 'NLS_DATE_LANGUAGE=ENGLISH') AS SDATE "
                      + ", TO_CHAR(A.BEGIN_TMLMT, 'hh24') AS STIMEHOUR "
                      + ", TO_CHAR(A.BEGIN_TMLMT, 'mi') AS STIMEMIN "
                      + ", TO_CHAR(A.END_TMLMT, 'MONTH DD, YYYY hh24:mi:ss', 'NLS_DATE_LANGUAGE=ENGLISH') AS EDATE "
                      + ", TO_CHAR(A.END_TMLMT, 'hh24') AS ETIMEHOUR "
                      + ", TO_CHAR(A.END_TMLMT, 'mi') AS ETIMEMIN "
                      + ", TO_CHAR(A.BEGIN_ADIT_TMLMT, 'MONTH DD, YYYY hh24:mi:ss', 'NLS_DATE_LANGUAGE=ENGLISH')  AS ADIT_SDATE "
                      + ", TO_CHAR(A.BEGIN_ADIT_TMLMT, 'hh24')  AS ADIT_STIMEHOURE "
                      + ", TO_CHAR(A.BEGIN_ADIT_TMLMT, 'mi')  AS ADIT_STIMEMIN "
                      + ", TO_CHAR(A.END_ADIT_TMLMT, 'MONTH DD, YYYY hh24:mi:ss', 'NLS_DATE_LANGUAGE=ENGLISH')   AS ADIT_EDATE "
                      + ", TO_CHAR(A.END_ADIT_TMLMT, 'hh24')   AS ADIT_ETIMEHOUR "
                      + ", TO_CHAR(A.END_ADIT_TMLMT, 'mi')   AS ADIT_ETIMEMIN "
                      + ", B.NAME "
                      + ", A.TASK_SEQ "
                      + "FROM OP_TASK A "
                      + "JOIN OP_USER B "
                      + "ON A.REGISTER = B.USER_ID "
                      + "AND ACDMC_NO = @selectedSubj:VARCHAR "
                      + "WHERE TASK_SEQ = @task_id:VARCHAR";

            var result = _commonDao.SelectOne(sql, Request.Form);

            ViewBag.result = result;
            ViewBag.param = param;

            ViewBag.SelectPageList = "/Task/SelectPageListTask";
            ViewBag.Select = "/Task/SelectTask";
            ViewBag.InsertForm = "/Task/InsertFormTask";
            ViewBag.Insert = "/Task/UpdateTask";

            return View("/Views/LctSport/Task/TaskInsertStdPage.cshtml");
        }

        public string UpdateTask([FromBody] Task Task) {
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");
            Dictionary<string, string> param = new Dictionary<string, string>();

            // Task 데이터 파싱
            param.Add("SelectSubj", Task.SelectSubj);
            param.Add("Title", Task.Title);
            param.Add("Content", Task.Content);
            param.Add("Sdate", Task.Sdate);
            param.Add("Edate", Task.Edate);
            param.Add("Adit_sdate", Task.Adit_sdate);
            param.Add("Adit_edate", Task.Adit_edate);
            param.Add("AtchFileId", Task.AtchFileId);
            param.Add("PresentForm", Task.PresentForm);
            param.Add("FileCpctyLlmt", Task.FileCpctyLlmt);
            param.Add("TaskId", Task.TaskId);
            param.Add("user_id", userInfo.user_id);
            string query = "";

            query = "UPDATE OP_TASK SET " +
                    "TITLE = @Title:VARCHAR" +
                    ", BEGIN_TMLMT = TO_DATE(@Sdate:VARCHAR, 'YYYYMMDDHH24MI')" +
                    ", END_TMLMT = TO_DATE(@Edate:VARCHAR, 'YYYYMMDDHH24MI')" +
                    ", BEGIN_ADIT_TMLMT = TO_DATE(@Adit_sdate:VARCHAR, 'YYYYMMDDHH24MI')" +
                    ", END_ADIT_TMLMT = TO_DATE(@Adit_edate:VARCHAR, 'YYYYMMDDHH24MI')" +
                    ", CONTENT = @Content:VARCHAR" +
                    ", PRESENT_FORM = @PresentForm:VARCHAR" +
                    ", FILE_CPCTY_LMT = @FileCpctyLlmt:VARCHAR" +
                    " WHERE TASK_SEQ = @TaskId:NUMBER";

            //cud 처리할 때는 트랜잭션 시작해주어야함
            using var transaction = _context.Database.BeginTransaction();

            _commonDao.Update(query, param);

            transaction.Commit();

            return "ok";
        }
        public string DeleteTask() {
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");

            if (!userInfo.author.Equals(_codeMngTool.getCode("AUTHOR", "PROFESSOR"))) {
                Response.WriteAsync("<script language=\"javascript\">alert('잘못된 권한입니다.');</script>");
                Response.WriteAsync("<script language=\"javascript\">window.location=\"/Task/SelectPageListTask\"</script>");
            }

            string query = "";

            query = "DELETE FROM OP_TASK WHERE TASK_SEQ = @task_id:NUMBER";

            //cud 처리할 때는 트랜잭션 시작해주어야함
            using var transaction = _context.Database.BeginTransaction();

            string resultCode = "ok";

            if (_commonDao.Delete(query, Request.Form) == 0) {
                resultCode = "false";
            }

            transaction.Commit();

            return resultCode;
        }

        // 과제 전용 모델 public으로 선언해야 매개변수로 사용가능함
        public class Task {
            public string SelectSubj { get; set; }              // 학정번호
            public string Title { get; set; }                   // 제목
            public string Content { get; set; }                 // 내용
            public string Sdate { get; set; }                   // 내용
            public string Edate { get; set; }                   // 내용
            public string Adit_sdate { get; set; }              // 내용
            public string Adit_edate { get; set; }              // 내용
            public string AtchFileId { get; set; }              // 첨부파일 ID
            public string PresentForm { get; set; }              // 첨부파일 ID
            public string FileCpctyLlmt { get; set; }              // 첨부파일 ID
            public string TaskId { get; set; }              // 첨부파일 ID
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
