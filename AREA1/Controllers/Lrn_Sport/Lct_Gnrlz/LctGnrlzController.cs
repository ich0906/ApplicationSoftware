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

namespace AREA1.Controllers.Lrn_Sport.Lct_Gnrlz {
    public class LctGnrlzController : Controller {
        private readonly ILogger<LctGnrlzController> _logger;
        private readonly AppSoftDbContext _context;
        private readonly CommonDao _commonDao;
        private readonly CodeMngTool _codeMngTool;

        public LctGnrlzController(ILogger<LctGnrlzController> logger, AppSoftDbContext context) {
            _logger = logger;
            _context = context;
            _commonDao = new CommonDao(context);
            _codeMngTool = new CodeMngTool(context);
        }

        public IActionResult LctGnrlz() {
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");

            if (userInfo == null) {
                return RedirectToAction("/Login", new { alertLogin = 2 });
            }

            ViewData["name"] = userInfo.name;
            ViewData["user_id"] = userInfo.user_id;

            ViewData["Title"] = HttpContext.Session.GetString("_Key");

            ViewBag.userinfo = userInfo;

            Dictionary<string, string> param = new Dictionary<string, string>();

            string sql = "";
            string acdmc_no = "";

            //강의 개수 체크
            sql = "SELECT COUNT(*) AS TAKES_CNT "
                 + "FROM OP_TAKES "
                 + "WHERE ID=" + userInfo.user_id;
            

            int takesCnt = 0;
            // Form이 존재하지 않으면 오류가 나기 때문에 분기해주어야한다.
            if (Request.HasFormContentType && !Request.Form["selectedSubj"].ToString().Equals("")) {
                acdmc_no = Request.Form["selectedSubj"];

                if (userInfo.author.Equals("1000")) {
                    sql = "SELECT BUILDING,ROOM_NUMBER,ACDMC_NO,TITLE,NAME,DAY1,DAY2,PERIOD1,PERIOD2,A.YEAR,A.SEMESTER,C.ID " +
                       "FROM OP_SECTION A JOIN OP_COURSE B ON A.COURSE_ID=B.COURSE_ID " +
                       "JOIN OP_TEACHES C ON A.SEC_ID=C.SEC_ID and A.COURSE_ID=C.COURSE_ID and A.SEMESTER=C.SEMESTER and A.YEAR=C.YEAR " +
                       "JOIN OP_USER D on C.ID=D.USER_ID " +
                       "JOIN OP_TIME_SLOT E on A.TIME_SLOT_ID=E.TIME_SLOT_ID " +
                       "WHERE ID=" + userInfo.user_id+" AND ACDMC_NO='"+acdmc_no+"'";
                } else {
                    sql = "SELECT BUILDING,ROOM_NUMBER,ACDMC_NO,TITLE,NAME,DAY1,DAY2,PERIOD1,PERIOD2,A.YEAR,A.SEMESTER,C.ID " +
                        "FROM OP_SECTION A JOIN OP_COURSE B ON A.COURSE_ID=B.COURSE_ID " +
                        "JOIN OP_TAKES C ON A.SEC_ID=C.SEC_ID and A.COURSE_ID=C.COURSE_ID and A.SEMESTER=C.SEMESTER and A.YEAR=C.YEAR " +
                        "JOIN OP_USER D on C.ID=D.USER_ID " +
                        "JOIN OP_TIME_SLOT E on A.TIME_SLOT_ID=E.TIME_SLOT_ID " +
                        "WHERE ID=" + userInfo.user_id+" AND ACDMC_NO='"+acdmc_no+"'";
                }    

                param = _commonDao.SelectOne(sql);
                param.Add("page", "1");
                ViewBag.param = param;
                ViewBag.selectedSubj = param;
                ViewBag.ResultList = param;
                ViewBag.YEAR_HAKGI = param["YEAR"] + "," + param["SEMESTER"];
                ViewBag.ACDMC_NO = param["ACDMC_NO"];
                // Form이 없거나 과목을 선택하지 않고 페이지에 넘어오는 경우
            }else{
                // 디폴트 과목을 선택함
                if (userInfo.author.Equals("1000")) {
                    int teachesCnt = 0;
                    sql = "SELECT COUNT(*) AS TEACHES_CNT FROM OP_TEACHES WHERE ID=" + userInfo.user_id;
                    teachesCnt = Convert.ToInt32(_commonDao.SelectOne(sql)["TEACHES_CNT"]);
                    if (teachesCnt == 0) {
                        return Redirect("/Views/Main.cshtml");
                    }

                    sql = "SELECT BUILDING,ROOM_NUMBER,ACDMC_NO,TITLE,NAME,DAY1,DAY2,PERIOD1,PERIOD2,A.YEAR YEAR,A.SEMESTER SEMESTER,C.ID " +
                        "FROM OP_SECTION A JOIN OP_COURSE B ON A.COURSE_ID=B.COURSE_ID " +
                        "JOIN OP_TEACHES C ON A.SEC_ID=C.SEC_ID and A.COURSE_ID=C.COURSE_ID and A.SEMESTER=C.SEMESTER and A.YEAR=C.YEAR " +
                        "JOIN OP_USER D on C.ID=D.USER_ID " +
                        "JOIN OP_TIME_SLOT E on A.TIME_SLOT_ID=E.TIME_SLOT_ID "+
                        "WHERE ID="+userInfo.user_id;
                } else {
                    takesCnt = Convert.ToInt32(_commonDao.SelectOne(sql, param)["TAKES_CNT"]);
                    if (takesCnt == 0) {
                        return Redirect("/Views/Main.cshtml");
                    }

                    sql = "SELECT BUILDING,ROOM_NUMBER,ACDMC_NO,TITLE,NAME,DAY1,DAY2,PERIOD1,PERIOD2,A.YEAR,A.SEMESTER,C.ID " +
                        "FROM OP_SECTION A JOIN OP_COURSE B ON A.COURSE_ID=B.COURSE_ID " +
                        "JOIN OP_TAKES C ON A.SEC_ID = C.SEC_ID and A.COURSE_ID = C.COURSE_ID and A.SEMESTER = C.SEMESTER and A.YEAR = C.YEAR " +
                        "JOIN OP_USER D on C.ID=D.USER_ID " +
                        "JOIN OP_TIME_SLOT E on A.TIME_SLOT_ID=E.TIME_SLOT_ID "+
                        "WHERE ID="+userInfo.user_id;
                }

                param = _commonDao.SelectOne(sql);
                param.Add("page", "1");
                ViewBag.param = param;
                ViewBag.selectedSubj = param;
                ViewBag.ResultList = param;
                ViewBag.YEAR_HAKGI = param["YEAR"] + "," + param["SEMESTER"];
                ViewBag.ACDMC_NO = param["ACDMC_NO"];
            }

            return View("/Views/LctGnrlz/LctGnrlz.cshtml");
        }

        public IActionResult InsertData() {
            using var transaction = _context.Database.BeginTransaction();


            string query = "INSERT INTO PERSONS VALUES(@person_id:NUMBER,"
                                                    + "@last_name:VARCHAR,"
                                                    + "@first_name:VARCHAR,"
                                                    + "@address:VARCHAR,"
                                                    + "@city:VARCHAR"
                                                    + ")";


            _commonDao.Insert(query, Request.Form);

            transaction.Commit();

            return Redirect("Index");
        }
        public IActionResult UpdateData() {
            using var transaction = _context.Database.BeginTransaction();


            string query = "UPDATE PERSONS SET PERSON_ID = @person_id:NUMBER, "
                                                          + "LAST_NAME = @last_name:VARCHAR, "
                                                          + "FIRST_NAME = @first_name:VARCHAR, "
                                                          + "ADDRESS = @address:VARCHAR, "
                                                          + "CITY = @city:VARCHAR";


            _commonDao.Update(query, Request.Form);

            transaction.Commit();

            return Redirect("Index");
        }
        public IActionResult DeleteData() {
            using var transaction = _context.Database.BeginTransaction();


            string query = "DELETE FROM PERSONS WHERE PERSON_ID = @person_id:NUMBER";

            _commonDao.Delete(query, Request.Form);


            transaction.Commit();

            return Redirect("Index");
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
