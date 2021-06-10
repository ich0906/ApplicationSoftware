using AREA1.Data;
using AREA1.Filters;
using AREA1.Models;
using AREA1.Tool;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
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
         * 강의 과제 리스트 페이지
         * 작성자 : 김정원
         * 기능 : 과제 리스트 페이지 호출
         * */
        public IActionResult SelectPageListTask() {
            Dictionary<string, string> param = new Dictionary<string, string>();

            // User 정보 파싱
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");
            ViewData["name"] = userInfo.name;                                       // 이름
            ViewData["user_id"] = userInfo.user_id;                                 // 유저 ID(학번)
            ViewData["fs_at"] = userInfo.author.Equals(_codeMngTool.getCode("AUTHOR", "PROFESSOR")) ? "Y" : "N";         // 교수 여부
            ViewData["pageNm"] = "과제 제출";
            ViewData["author"] = userInfo.author;

            string sql = "";

            // 과제 개수 체크
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

                // Form이 없거나 과목을 선택하지 않고 과제 페이지에 넘어오는 경우
            } else {
                // 디폴트 과목을 선택함
                string sql2 = userInfo.author.Equals(_codeMngTool.getCode("AUTHOR", "PROFESSOR"))
                    ? "SELECT * FROM (SELECT ROWNUM, AA.* FROM (SELECT B.ACDMC_NO AS selectedSubj, YEAR || ',' || SEMESTER AS YEAR_HAKGI"
                    + $" FROM OP_TEACHES A NATURAL JOIN OP_SECTION B WHERE A.ID = '{userInfo.user_id}' ORDER BY YEAR DESC, SEMESTER DESC, COURSE_ID) AA) AAA WHERE ROWNUM = 1"
                    : "SELECT * FROM (SELECT ROWNUM, AA.* FROM (SELECT B.ACDMC_NO AS selectedSubj, YEAR || ',' || SEMESTER AS YEAR_HAKGI"
                    + $" FROM OP_TAKES A NATURAL JOIN OP_SECTION B WHERE A.ID = '{userInfo.user_id}' ORDER BY YEAR DESC, SEMESTER DESC, COURSE_ID) AA) AAA WHERE ROWNUM = 1";

                param = _commonDao.SelectOne(sql2);
                param.Add("selectedSubj", param["SELECTEDSUBJ"]);

                bbsCnt = Convert.ToInt32(_commonDao.SelectOne(sql, param)["TASK_CNT"]);

                ViewBag.YEAR_HAKGI = param["YEAR_HAKGI"];
                ViewBag.ACDMC_NO = param["selectedSubj"];
            }


            // 만약 조회된 과제가 있으면 값을 가져온다.
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
                    // Form이 없거나 과목을 선택하지 않고 과제 페이지에 넘어오는 경우
                } else {
                    var resultList = _commonDao.SelectList(sql, param);
                    ViewBag.ResultList = resultList;
                }

                for(int i = 0; i< ViewBag.ResultList.Count; i++) {
                    if(ViewData["fs_at"].Equals("Y")) {
                        sql = "SELECT("
                            + "SELECT COUNT(*) AS PRSENTR_CNT FROM OP_TASK_PRSENTR  WHERE TASK_SEQ = '81') || ' / ' || "
                            + "(SELECT COUNT(*) TOTAL_TAKES FROM OP_TAKES A "
                            + "JOIN OP_SECTION B ON A.SEC_ID = B.SEC_ID and A.COURSE_ID = B.COURSE_ID and A.SEMESTER = B.SEMESTER and A.YEAR = B.YEAR "
                            + "WHERE B.ACDMC_NO = 'H030-3-9876-01') AS PRSENTR_AT "
                            + "FROM DUAL ";
                    } else {
                        sql = $"SELECT PRSENTR_AT FROM OP_TASK_PRSENTR WHERE REGISTER = '{userInfo.user_id}'";
                    }

                    if (Request.HasFormContentType) {
                        ViewBag.ResultList[i]["PRSENTR_AT"] = _commonDao.SelectOne(sql, Request.Form)["PRSENTR_AT"].Equals("") ? "미제출" : _commonDao.SelectOne(sql, Request.Form)["PRSENTR_AT"];
                    } else {
                        ViewBag.ResultList[i]["PRSENTR_AT"] = _commonDao.SelectOne(sql, param)["PRSENTR_AT"].Equals("") ? "미제출" : _commonDao.SelectOne(sql, param)["PRSENTR_AT"];
                    }
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
            ViewData["author"] = userInfo.author;
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
                       + ", CASE WHEN A.BEGIN_TMLMT <= SYSDATE THEN CASE WHEN A.END_TMLMT >= SYSDATE THEN  'Y' ELSE 'N' END ELSE 'N' END AS B1 "
                       + ", CASE WHEN A.BEGIN_ADIT_TMLMT <= SYSDATE THEN CASE WHEN A.END_ADIT_TMLMT >= SYSDATE THEN  'Y' ELSE 'N' END ELSE 'N' END AS B2 "
                       + ", B.NAME "
                       + ", A.TASK_SEQ "
                       + ", A.DOC_ID "
                       + "FROM OP_TASK A "
                       + "JOIN OP_USER B "
                       + "ON A.REGISTER = B.USER_ID "
                       + "AND ACDMC_NO = @selectedSubj:VARCHAR "
                       + "WHERE TASK_SEQ = @task_id:VARCHAR";

            var result = _commonDao.SelectOne(sql, Request.Form);

            //첨부파일 읽어오기
            if (result["DOC_ID"] != "") {
                sql = "SELECT FILE_NAME,FILE_EXTSN,FILE_ID FROM OP_FILE A JOIN OP_TASK B ON A.DOC_ID=B.DOC_ID"
               + $" WHERE A.DOC_ID='{result["DOC_ID"]}'";

                int fcount = 0;
                var fileList = _commonDao.SelectList(sql);
                fcount = fileList.Count;
                ViewBag.fileList = fileList;
                ViewBag.fileCount = fcount;
            }

            
            // 학생 관련 부분
            if (ViewData["fs_at"].Equals("N")) {
                sql = $"SELECT TASK_SEQ, REGISTER, DOC_ID, TITLE, CONTENT, PRSENTR_AT, REGIST_DT FROM OP_TASK_PRSENTR WHERE REGISTER = '{userInfo.user_id}' AND TASK_SEQ = @task_id:VARCHAR";
            } else {
                sql = $"SELECT TASK_SEQ, REGISTER, DOC_ID, TITLE, CONTENT, PRSENTR_AT, REGIST_DT FROM OP_TASK_PRSENTR WHERE TASK_SEQ = @task_id:VARCHAR ORDER BY REGISTER ASC";
            }

            // 학생들이 과제 제출한 부분
            var resultPrsentList = _commonDao.SelectList(sql, Request.Form);

            // 학생 과제 수정 시 기존 내용을 유지하면서 수정 폼을 띄우기 위한 구분값을 설정한다.
            // 해당 if문이 참일 경우는 학생일 경우를 가정하고 실행시키기 때문에 배열의 0번 인덱스에 해당 학생의 과제정보가 있을 것이라고 판단한다.
            if (Request.Form.ContainsKey("prsentr_at") && Request.Form["prsentr_at"] == "N") {
                resultPrsentList[0]["PRSENTR_AT"] = "N";
            }

            // 학생이 업로드한 파일 리스트를 담는 리스트
            List<List<Dictionary<string, string>>> studentFileList = new List<List<Dictionary<string, string>>>();

            for (int i = 0; i < resultPrsentList.Count; i++) {
                var resultPrsentr = resultPrsentList[i];
                //첨부파일 읽어오기
                if (resultPrsentr["DOC_ID"] != "") {
                    sql = "SELECT FILE_NAME,FILE_EXTSN,FILE_ID FROM OP_FILE A JOIN OP_TASK_PRSENTR B ON A.DOC_ID=B.DOC_ID"
                   + $" WHERE A.DOC_ID='{resultPrsentr["DOC_ID"]}'";

                    int fcount2 = 0;
                    var fileList2 = _commonDao.SelectList(sql);
                    fcount2 = fileList2.Count;

                    studentFileList.Add(fileList2);
                    resultPrsentr["fileCount"] = fcount2.ToString();
                }
            }

            ViewBag.resultPrsentList = resultPrsentList;
            ViewBag.studentFileList = studentFileList;
            ViewBag.result = result;
            ViewBag.param = param;
            ViewBag.Select = "/Task/SelectTask";
            ViewBag.SelectPageList = "/Task/SelectPageListTask";
            ViewBag.UpdateForm = "/Task/UpdateFormTask";
            ViewBag.Delete = "/Task/DeleteTask";
            ViewBag.Prsentr = "/Task/PrsentrTask";


            return View("/Views/LctSport/Task/TaskViewPage.cshtml");
        }

        /*
         * 강의 과제 작성 페이지
         * 작성자 : 김정원
         * 기능 : 과제 작성 페이지 호출
         * */
        public IActionResult InsertFormTask() {
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");
            ViewData["name"] = userInfo.name;
            ViewData["user_id"] = userInfo.user_id;
            ViewData["fs_at"] = userInfo.author.Equals(_codeMngTool.getCode("AUTHOR", "PROFESSOR")) ? "Y" : "N";         // 교수 여부
            ViewData["pageNm"] = "과제 제출";
            ViewData["author"] = userInfo.author;
            ViewData["command"] = "INSERT";

            Dictionary<string, string> param = new Dictionary<string, string>();

            ViewBag.ACDMC_NO = Request.Form["selectedSubj"];
            ViewBag.YEAR_HAKGI = Request.Form["selectedYearhakgi"];

            if (!userInfo.author.Equals(_codeMngTool.getCode("AUTHOR", "PROFESSOR"))) {
                Response.WriteAsync("<script language=\"javascript\">alert('Invalid Author!!');</script>");
                Response.WriteAsync("<script language=\"javascript\">window.location=\"/Main/Main\"</script>");
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
         * 강의 과제 작성
         * 작성자 : 김정원
         * 기능 : 과제 작성 로직
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
                    ", @AtchFileId:VARCHAR" +
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
            ViewData["author"] = userInfo.author;

            Dictionary<string, string> param = new Dictionary<string, string>();

            ViewBag.ACDMC_NO = Request.Form["selectedSubj"];
            ViewBag.YEAR_HAKGI = Request.Form["selectedYearhakgi"];

            if (!userInfo.author.Equals(_codeMngTool.getCode("AUTHOR", "PROFESSOR"))) {
                Response.WriteAsync("<script language=\"javascript\">alert('Invalid Author!!');</script>");
                Response.WriteAsync("<script language=\"javascript\">window.location=\"/Main/Main\"</script>");
            }

            param.Add("page", Request.Form["page"]);

            string sql = "SELECT A.ACDMC_NO AS SelectSubj"
                      + ", A.TITLE"
                      + ", A.CONTENT"
                      + ", A.PRESENT_FORM"
                      + ", A.FILE_CPCTY_LMT"
                      + ", A.DOC_ID "
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
                    ", DOC_ID = @AtchFileId:VARCHAR" +
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

            //cud 처리할 때는 트랜잭션 시작해주어야함
            using var transaction = _context.Database.BeginTransaction();
            string resultCode = "ok";

            string query = "";

            if (userInfo.author.Equals(_codeMngTool.getCode("AUTHOR", "PROFESSOR"))) {
                if (!userInfo.author.Equals(_codeMngTool.getCode("AUTHOR", "PROFESSOR"))) {
                    Response.WriteAsync("<script language=\"javascript\">alert('Invalid Author!!');</script>");
                    Response.WriteAsync("<script language=\"javascript\">window.location=\"/Task/SelectPageListTask\"</script>");
                }

                query = $"DELETE FROM OP_TASK_PRSENTR WHERE TASK_SEQ = @task_id:NUMBER";
                // 과제에 딸려 있는 제출 내역들을 먼저 삭제하고 
                if (_commonDao.Delete(query, Request.Form) <= 0) {
                    resultCode = "false";
                }
                
                // 과제를 삭제한다.
                query = "DELETE FROM OP_TASK WHERE TASK_SEQ = @task_id:NUMBER";
            } else {
                query = $"DELETE FROM OP_TASK_PRSENTR WHERE REGISTER = '{userInfo.user_id}' AND TASK_SEQ = @task_id:NUMBER";
            }


            if (_commonDao.Delete(query, Request.Form) <= 0) {
                resultCode = "false";
            }

            if(resultCode.Equals("false")) transaction.Rollback();
            else transaction.Commit();

            return resultCode;
        }

        public string PrsentrTask() {
            string resultCode = "ok";

            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");

            string query = "";

            query = $"SELECT COUNT(*) AS CNT FROM OP_TAKES A NATURAL JOIN OP_SECTION B WHERE A.ID = '{userInfo.user_id}' AND ACDMC_NO = '" + Request.Form["selectedSubj"] + "'";

            // 해당 강의를 수강하는 학생이 아니라면 
            if (!_commonDao.SelectOne(query)["CNT"].Equals("1")) {
                resultCode = "false";
            }

            query = "INSERT INTO OP_TASK_PRSENTR VALUES(" +
                    "@task_id:VARCHAR " +
                    $", '{userInfo.user_id}' " +
                    ", @doc_id:VARCHAR " +
                    ", @prsentr_title:VARCHAR " +
                    ", @prsentr_content:VARCHAR " +
                    ", 'Y'" +
                    ", SYSDATE)";

            //cud 처리할 때는 트랜잭션 시작해주어야함
            using var transaction = _context.Database.BeginTransaction();

            try {
                if (_commonDao.Insert(query, Request.Form) == 0) {
                    resultCode = "false";
                }
                // 한번 제출한 과제는 insert 시 에러가 나기 때문에 catch 문으로 받고 업데이트문으로 바꾸어준다.
            } catch (OracleException e) {
                query = "UPDATE OP_TASK_PRSENTR SET " +
                   "DOC_ID =  @doc_id:VARCHAR " +
                   ", TITLE = @prsentr_title:VARCHAR " +
                   ", CONTENT = @prsentr_content:VARCHAR " +
                   " WHERE TASK_SEQ = @task_id:VARCHAR" +
                  $" AND REGISTER = '{userInfo.user_id}'";

                if (_commonDao.Insert(query, Request.Form) == 0) {
                    resultCode = "false";
                }
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
