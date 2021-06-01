using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using AREA1.Data;
using AREA1.Filters;
using AREA1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using Tool;
using AREA1.Tool;

namespace AREA1.Controllers.Lrn_Sport.Lct_Calendar {
    public class CalendarController : Controller {
        private readonly ILogger<CalendarController> _logger;
        private readonly AppSoftDbContext _context;
        private readonly CommonDao _commonDao;
        private readonly CodeMngTool _codeMngTool;

        public CalendarController(ILogger<CalendarController> logger, AppSoftDbContext context) {
            _logger = logger;
            _context = context;
            _commonDao = new CommonDao(context);
            _codeMngTool = new CodeMngTool(context);
        }

        public IActionResult ShowCalendar() {
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");

            if (userInfo == null) {
                return RedirectToAction("/Login", new { alertLogin = 2 });
            }

            DateTime nowDt = DateTime.Now;
            if (nowDt.DayOfWeek == DayOfWeek.Monday)
                ViewBag.DayOfWeek = 0;
            else if (nowDt.DayOfWeek == DayOfWeek.Thursday)
                ViewBag.DayOfWeek = 1;
            else if (nowDt.DayOfWeek == DayOfWeek.Wednesday)
                ViewBag.DayOfWeek = 2;
            else if (nowDt.DayOfWeek == DayOfWeek.Thursday)
                ViewBag.DayOfWeek = 3;
            else if (nowDt.DayOfWeek == DayOfWeek.Friday)
                ViewBag.DayOfWeek = 4;
            else if (nowDt.DayOfWeek == DayOfWeek.Saturday)
                ViewBag.DayOfWeek = 5;

            ViewData["name"] = userInfo.name;
            ViewData["user_id"] = userInfo.user_id;

            ViewData["Today"] = DateTime.Now.ToString().Split(' ')[0];

            ViewBag.userinfo = userInfo;

            string sql = "SELECT COUNT(*) AS TAKES_CNT "
                + "FROM OP_TAKES "
                + "WHERE ID=" + userInfo.user_id;

            int takes_cnt = 0;
            takes_cnt = Convert.ToInt32(_commonDao.SelectOne(sql)["TAKES_CNT"]);

            if (userInfo.author.Equals("1000")) {
                sql = "SELECT DISTINCT A.YEAR FROM OP_TEACHES A "
                    + "JOIN OP_USER B ON A.ID = B.USER_ID "
                    + "JOIN OP_SECTION C on A.SEC_ID = C.SEC_ID and A.COURSE_ID = C.COURSE_ID and A.SEMESTER = C.SEMESTER and A.YEAR = C.YEAR "
                    + "JOIN OP_COURSE D ON C.COURSE_ID = D.COURSE_ID "
                    + "JOIN OP_TIME_SLOT E on C.TIME_SLOT_ID = E.TIME_SLOT_ID "
                    + "WHERE ID=" + userInfo.user_id+" ORDER BY A.YEAR DESC";
            } else {
                sql = "SELECT DISTINCT A.YEAR FROM OP_TAKES A "
                    + "JOIN OP_USER B ON A.ID = B.USER_ID "
                    + "JOIN OP_SECTION C on A.SEC_ID = C.SEC_ID and A.COURSE_ID = C.COURSE_ID and A.SEMESTER = C.SEMESTER and A.YEAR = C.YEAR "
                    + "JOIN OP_COURSE D ON C.COURSE_ID = D.COURSE_ID "
                    + "JOIN OP_TIME_SLOT E on C.TIME_SLOT_ID = E.TIME_SLOT_ID "
                    + "WHERE ID=" + userInfo.user_id+ " ORDER BY A.YEAR DESC";
            }

            var yearList = _commonDao.SelectList(sql);
            ViewBag.yearList = yearList;
            ViewBag.yearCount = yearList.Count;

            if (userInfo.author.Equals("1000")) {
                sql = "SELECT DISTINCT A.SEMESTER FROM OP_TEACHES A "
                    + "JOIN OP_USER B ON A.ID = B.USER_ID "
                    + "JOIN OP_SECTION C on A.SEC_ID = C.SEC_ID and A.COURSE_ID = C.COURSE_ID and A.SEMESTER = C.SEMESTER and A.YEAR = C.YEAR "
                    + "JOIN OP_COURSE D ON C.COURSE_ID = D.COURSE_ID "
                    + "JOIN OP_TIME_SLOT E on C.TIME_SLOT_ID = E.TIME_SLOT_ID "
                    + "WHERE ID=" + userInfo.user_id+ " ORDER BY A.SEMESTER";
            } else {
                sql = "SELECT DISTINCT A.SEMESTER FROM OP_TAKES A "
                    + "JOIN OP_USER B ON A.ID = B.USER_ID "
                    + "JOIN OP_SECTION C on A.SEC_ID = C.SEC_ID and A.COURSE_ID = C.COURSE_ID and A.SEMESTER = C.SEMESTER and A.YEAR = C.YEAR "
                    + "JOIN OP_COURSE D ON C.COURSE_ID = D.COURSE_ID "
                    + "JOIN OP_TIME_SLOT E on C.TIME_SLOT_ID = E.TIME_SLOT_ID "
                    + "WHERE ID=" + userInfo.user_id+ " ORDER BY A.SEMESTER";
            }

            var hakgiList = _commonDao.SelectList(sql);
            ViewBag.hakgiList = hakgiList;
            ViewBag.hakgiCount = hakgiList.Count;

            bool isSelected = false;
            if (Request.HasFormContentType && !Request.Form["year"].ToString().Equals("")) {
                for (int i = 0; i < yearList.Count; ++i) {
                    if (Request.Form["year"] == yearList[i]["YEAR"]) {
                        ViewBag.selectedYear = Request.Form["year"];
                        ViewBag.selectedYearIndex = i;
                        isSelected = true;
                    }
                }
            }
            if (!isSelected) {
                ViewBag.selectedYear = yearList[0]["YEAR"];
                ViewBag.selectedYearIndex = 0;
            }

            isSelected = false;
            if (Request.HasFormContentType && !Request.Form["hakgi"].ToString().Equals("")) {
                for (int i = 0; i < hakgiList.Count; ++i) {
                    if (Request.Form["hakgi"] == hakgiList[i]["SEMESTER"]) {
                        ViewBag.selectedHakgi = Request.Form["hakgi"];
                        ViewBag.selectedHakgiIndex = i;
                        isSelected = true;
                    }
                }
            }
            if (!isSelected) {
                ViewBag.selectedHakgi = hakgiList[0]["SEMESTER"];
                ViewBag.selectedHakgiIndex = 0;
            }

            if (takes_cnt > 0 && userInfo.author.Equals("2000")) {
                sql = "SELECT TITLE,NAME,DAY1,DAY2,PERIOD1,PERIOD2,BUILDING,ROOM_NUMBER,USER_ID,ACDMC_NO,A.ID,A.YEAR,A.SEMESTER, "
                    + "(SELECT OP_ADVISOR.I_ID FROM OP_USER JOIN OP_ADVISOR ON OP_USER.USER_ID=OP_ADVISOR.I_ID WHERE OP_ADVISOR.S_ID=A.ID) AS ADVISOR_ID, "
                    + "(SELECT NAME FROM OP_USER JOIN OP_ADVISOR ON OP_USER.USER_ID=OP_ADVISOR.I_ID WHERE OP_ADVISOR.S_ID=A.ID) AS ADVISOR, "
                    + "(SELECT EMAIL FROM OP_USER JOIN OP_ADVISOR ON OP_USER.USER_ID=OP_ADVISOR.I_ID WHERE OP_ADVISOR.S_ID=A.ID) AS AD_EMAIL, "
                    + "(SELECT PHONE FROM OP_USER JOIN OP_ADVISOR ON OP_USER.USER_ID=OP_ADVISOR.I_ID WHERE OP_ADVISOR.S_ID=A.ID) AS AD_PHONE "
                   + "FROM OP_TAKES A "
                   + "JOIN OP_TEACHES B "
                   + "ON A.SEC_ID=B.SEC_ID AND A.COURSE_ID=B.COURSE_ID AND A.SEMESTER=B.SEMESTER AND A.YEAR=B.YEAR "
                   + "JOIN OP_SECTION C "
                   + "ON A.SEC_ID=C.SEC_ID AND A.COURSE_ID=C.COURSE_ID AND A.SEMESTER=C.SEMESTER AND A.YEAR=C.YEAR "
                   + "JOIN OP_COURSE D ON A.COURSE_ID=D.COURSE_ID "
                   + "JOIN OP_USER E ON E.USER_ID=B.ID "
                   + "JOIN OP_TIME_SLOT F on C.TIME_SLOT_ID = F.TIME_SLOT_ID ";
                if (Request.HasFormContentType && !Request.Form["year"].ToString().Equals("")) sql += "WHERE A.ID=" + userInfo.user_id + " AND A.YEAR=" + Request.Form["year"] + " AND A.SEMESTER=" + Request.Form["hakgi"];
                else sql += "WHERE A.ID=" + userInfo.user_id + " AND A.YEAR=" + yearList[0]["YEAR"] + " AND A.SEMESTER=" + hakgiList[0]["SEMESTER"];

                var resultList = _commonDao.SelectList(sql);
                ViewBag.ResultList = resultList;
                ViewBag.ResultCount = resultList.Count;
            }

            sql = "SELECT COUNT(*) AS TEACHES_CNT "
                + "FROM OP_TEACHES "
                + "WHERE ID=" + userInfo.user_id;

            int teaches_cnt = 0;
            teaches_cnt = Convert.ToInt32(_commonDao.SelectOne(sql)["TEACHES_CNT"]);

            if (teaches_cnt > 0 && userInfo.author.Equals("1000")) {
                sql = "SELECT TITLE,ACDMC_NO,DAY1,DAY2,PERIOD1,PERIOD2,BUILDING,ROOM_NUMBER,NAME,ID,A.YEAR,A.SEMESTER FROM OP_TEACHES A "
                    + "JOIN OP_USER B ON A.ID = B.USER_ID "
                    + "JOIN OP_SECTION C on A.SEC_ID = C.SEC_ID and A.COURSE_ID = C.COURSE_ID and A.SEMESTER = C.SEMESTER and A.YEAR = C.YEAR "
                    + "JOIN OP_COURSE D ON C.COURSE_ID = D.COURSE_ID "
                    + "JOIN OP_TIME_SLOT E on C.TIME_SLOT_ID = E.TIME_SLOT_ID ";
                if (Request.HasFormContentType && !Request.Form["year"].ToString().Equals("")) sql += "WHERE A.ID=" + userInfo.user_id + " AND A.YEAR=" + Request.Form["year"] + " AND A.SEMESTER=" + Request.Form["hakgi"];
                else sql += "WHERE A.ID=" + userInfo.user_id + " AND A.YEAR=" + yearList[0]["YEAR"] + " AND A.SEMESTER=" + hakgiList[0]["SEMESTER"];

                var resultList2 = _commonDao.SelectList(sql);
                ViewBag.ResultListTeacher = resultList2;
                ViewBag.TeachesCount = resultList2.Count;
            }

            return View("/Views/LctCalendar/LctCalendar.cshtml");
        }
    }
}
