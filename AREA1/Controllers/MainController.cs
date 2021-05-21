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

namespace AREA1.Controllers {
    [LoginActionFilter]
    public class MainController : Controller {
        private readonly ILogger<MainController> _logger;
        private readonly AppSoftDbContext _context;
        private readonly CommonDao _commonDao;
        private readonly CodeMngTool _codeMngTool;
        public MainController(ILogger<MainController> logger, AppSoftDbContext context) {
            _logger = logger;
            _context = context;
            _commonDao = new CommonDao(context);
            _codeMngTool = new CodeMngTool(context);
        }
        public IActionResult Main() {
            // TIME_SLOT_TABLE에서 DAY1만 채워져 있다면 1연속 수업,
            // DAY2만 채워져 있다면 3연속 수업, 둘 다 채워져 있다면 일반적.
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");

            if (userInfo == null) {
                return RedirectToAction("/Login", new { alertLogin = 2 });
            }

            ViewData["name"] = userInfo.name;
            ViewData["user_id"] = userInfo.user_id;

            ViewData["Title"] = HttpContext.Session.GetString("_Key");

            ViewData["Today"] = DateTime.Now.ToString().Split(' ')[0];

            ViewBag.userinfo = userInfo;

            string sql = "SELECT COUNT(*) AS TAKES_CNT "
                + "FROM OP_TAKES "
                + "WHERE ID=" + userInfo.user_id;

            int takes_cnt = 0;
            takes_cnt = Convert.ToInt32(_commonDao.SelectOne(sql)["TAKES_CNT"]);

            if (userInfo.author.Equals("1000")) {
                sql = "SELECT DISTINCT A.YEAR,A.SEMESTER FROM OP_TEACHES A "
                    + "JOIN OP_USER B ON A.ID = B.USER_ID "
                    + "JOIN OP_SECTION C on A.SEC_ID = C.SEC_ID and A.COURSE_ID = C.COURSE_ID and A.SEMESTER = C.SEMESTER and A.YEAR = C.YEAR "
                    + "JOIN OP_COURSE D ON C.COURSE_ID = D.COURSE_ID "
                    + "JOIN OP_TIME_SLOT E on C.TIME_SLOT_ID = E.TIME_SLOT_ID "
                    + "WHERE ID=" + userInfo.user_id;
            } else {
                sql = "SELECT DISTINCT A.YEAR,A.SEMESTER FROM OP_TAKES A "
                    + "JOIN OP_USER B ON A.ID = B.USER_ID "
                    + "JOIN OP_SECTION C on A.SEC_ID = C.SEC_ID and A.COURSE_ID = C.COURSE_ID and A.SEMESTER = C.SEMESTER and A.YEAR = C.YEAR "
                    + "JOIN OP_COURSE D ON C.COURSE_ID = D.COURSE_ID "
                    + "JOIN OP_TIME_SLOT E on C.TIME_SLOT_ID = E.TIME_SLOT_ID "
                    + "WHERE ID=" + userInfo.user_id;
            }

            var yearHakgiList = _commonDao.SelectList(sql);
            ViewBag.yearHakgiList = yearHakgiList;
            ViewBag.yearHakgiCount = yearHakgiList.Count;

            bool isSelected = false;
            if (Request.HasFormContentType && !Request.Form["year"].ToString().Equals("")) {
                for(int i = 0; i < yearHakgiList.Count; ++i) {
                    if(Request.Form["year"]==yearHakgiList[i]["YEAR"] && Request.Form["hakgi"] == yearHakgiList[i]["SEMESTER"]) {
                        ViewBag.selectedIndex = i;
                        isSelected = true;
                    }
                }
            }
            if (!isSelected) ViewBag.selectedIndex = 0;

                if (takes_cnt > 0) {
                sql = "SELECT TITLE,NAME,DAY1,DAY2,PERIOD1,PERIOD2,BUILDING,ROOM_NUMBER,USER_ID,ACDMC_NO,A.ID,A.YEAR,A.SEMESTER, "
                    + "(SELECT OP_ADVISOR.I_ID FROM OP_USER JOIN OP_ADVISOR ON OP_USER.USER_ID=OP_ADVISOR.I_ID WHERE OP_ADVISOR.S_ID=A.ID) AS ADVISOR_ID,"
                    + "(SELECT NAME FROM OP_USER JOIN OP_ADVISOR ON OP_USER.USER_ID=OP_ADVISOR.I_ID WHERE OP_ADVISOR.S_ID=A.ID) AS ADVISOR, "
                    + "(SELECT EMAIL FROM OP_USER JOIN OP_ADVISOR ON OP_USER.USER_ID=OP_ADVISOR.I_ID WHERE OP_ADVISOR.S_ID=A.ID) AS AD_EMAIL,"
                    + "(SELECT PHONE FROM OP_USER JOIN OP_ADVISOR ON OP_USER.USER_ID=OP_ADVISOR.I_ID WHERE OP_ADVISOR.S_ID=A.ID) AS AD_PHONE "
                   + "FROM OP_TAKES A "
                   + "JOIN OP_TEACHES B "
                   + "ON A.SEC_ID=B.SEC_ID AND A.COURSE_ID=B.COURSE_ID AND A.SEMESTER=B.SEMESTER AND A.YEAR=B.YEAR "
                   + "JOIN OP_SECTION C "
                   + "ON A.SEC_ID=C.SEC_ID AND A.COURSE_ID=C.COURSE_ID AND A.SEMESTER=C.SEMESTER AND A.YEAR=C.YEAR "
                   + "JOIN OP_COURSE D ON A.COURSE_ID=D.COURSE_ID "
                   + "JOIN OP_USER E ON E.USER_ID=B.ID "
                   + "JOIN OP_TIME_SLOT F on C.TIME_SLOT_ID = F.TIME_SLOT_ID ";
                if(Request.HasFormContentType && !Request.Form["year"].ToString().Equals("")) sql+= "WHERE A.ID=" + userInfo.user_id + " AND A.YEAR=" + Request.Form["year"] + " AND A.SEMESTER=" + Request.Form["hakgi"];
                else sql+="WHERE A.ID=" + userInfo.user_id + " AND A.YEAR=" + yearHakgiList[0]["YEAR"] + " AND A.SEMESTER=" + yearHakgiList[0]["SEMESTER"];

                var resultList = _commonDao.SelectList(sql);
                ViewBag.ResultList = resultList;
                ViewBag.ResultCount = resultList.Count;
            }

            sql = "SELECT COUNT(*) AS TEACHES_CNT "
                + "FROM OP_TEACHES "
                + "WHERE ID=" + userInfo.user_id;

            int teaches_cnt = 0;
            teaches_cnt = Convert.ToInt32(_commonDao.SelectOne(sql)["TEACHES_CNT"]);

            if (teaches_cnt > 0) {
                sql = "SELECT TITLE,ACDMC_NO,DAY1,DAY2,PERIOD1,PERIOD2,BUILDING,ROOM_NUMBER,NAME,ID FROM OP_TEACHES A "
                    + "JOIN OP_USER B ON A.ID = B.USER_ID "
                    + "JOIN OP_SECTION C on A.SEC_ID = C.SEC_ID and A.COURSE_ID = C.COURSE_ID and A.SEMESTER = C.SEMESTER and A.YEAR = C.YEAR "
                    + "JOIN OP_COURSE D ON C.COURSE_ID = D.COURSE_ID "
                    + "JOIN OP_TIME_SLOT E on C.TIME_SLOT_ID = E.TIME_SLOT_ID ";
                if (Request.HasFormContentType && !Request.Form["year"].ToString().Equals("")) sql += "WHERE A.ID=" + userInfo.user_id + " AND A.YEAR=" + Request.Form["year"] + " AND A.SEMESTER=" + Request.Form["hakgi"];
                else sql += "WHERE A.ID=" + userInfo.user_id + " AND A.YEAR=" + yearHakgiList[0]["YEAR"] + " AND A.SEMESTER=" + yearHakgiList[0]["SEMESTER"];
                
                var resultList2 = _commonDao.SelectList(sql);
                ViewBag.ResultListTeacher = resultList2;
                ViewBag.TeachesCount = resultList2.Count;
            }

            return View("/Views/Main.cshtml");
        }

        public List<Dictionary<string, List<Dictionary<string, string>>>> YearhakgiAtnlcSbjectList() {
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");
            Dictionary<string, string> param = new Dictionary<string, string>();

            // 학기 리스트
            List<Dictionary<string, string>> yearhakgiList = null;
            // 학기 정보 리스트
            List<Dictionary<string, List<Dictionary<string, string>>>> resultList = new List<Dictionary<string, List<Dictionary<string, string>>>>();


            param["USER_ID"] = userInfo.user_id;


            if (userInfo.author.Equals(_codeMngTool.getCode("AUTHOR", "PROFESSOR"))) {
                string sql = "";

                // 학기명
                sql = "SELECT YEAR || '년도 ' || SEMESTER || '학기' AS LABEL"
                    + " ,YEAR || ',' || SEMESTER AS YEAR_HAKGI "
                    + ", YEAR, SEMESTER "
                    + "FROM OP_TEACHES "
                    + $"WHERE ID = '{userInfo.user_id}' "
                    + "GROUP BY YEAR, SEMESTER "
                    + "ORDER BY YEAR DESC, SEMESTER DESC";

                yearhakgiList = _commonDao.SelectList(sql);


                for (int i = 0; i < yearhakgiList.Count; i++) {
                    // 과목명
                    sql = "SELECT B.ACDMC_NO, A.SEMESTER, A.YEAR, D.TITLE || ' (' || B.ACDMC_NO || ') - ' || C.NAME AS LABEL" +
                        ",  A.YEAR || ',' || A.SEMESTER AS YEAR_HAKGI"
                               + " FROM OP_TEACHES A"
                               + " JOIN OP_SECTION B"
                               + " ON A.COURSE_ID = B.COURSE_ID AND A.SEC_ID = B.SEC_ID AND A.SEMESTER = B.SEMESTER AND A.YEAR = B.YEAR"
                               + " JOIN OP_USER C"
                               + " ON A.ID = C.USER_ID"
                               + " JOIN OP_COURSE D"
                               + " ON A.COURSE_ID = D.COURSE_ID"
                               + $" WHERE A.ID = '{userInfo.user_id}'"
                               + " AND A.semester = '" + yearhakgiList[i]["SEMESTER"] + "'"
                               + " AND A.YEAR = '" + yearhakgiList[i]["YEAR"] + "'"
                               + " ORDER BY A.COURSE_ID";


                    // 학기 당 과목리스트
                    List<Dictionary<string, string>> subjList = null;

                    subjList = _commonDao.SelectList(sql);
                    subjList.Add(yearhakgiList[i]);

                    // 반환 리스트
                    Dictionary<string, List<Dictionary<string, string>>> result = new Dictionary<string, List<Dictionary<string, string>>>();

                    result["subjList"] = subjList;

                    resultList.Add(result);
                }
            } else if (userInfo.author.Equals(_codeMngTool.getCode("AUTHOR", "STUDENT"))) {
                string sql = "";

                // 학기명
                sql = "SELECT YEAR || '년도 ' || SEMESTER || '학기' AS LABEL"
                    + " ,YEAR || ',' || SEMESTER AS YEAR_HAKGI "
                    + ", YEAR, SEMESTER "
                    + "FROM OP_TAKES "
                    + $"WHERE ID = '{userInfo.user_id}' "
                    + "GROUP BY YEAR, SEMESTER "
                    + "ORDER BY YEAR DESC, SEMESTER DESC";

                yearhakgiList = _commonDao.SelectList(sql);

                for (int i = 0; i < yearhakgiList.Count; i++) {
                    // 과목명
                    sql = "SELECT B.ACDMC_NO, A.SEMESTER, A.YEAR, D.TITLE || ' (' || B.ACDMC_NO || ') - ' || (SELECT BB.NAME FROM OP_TEACHES AA JOIN OP_USER BB ON AA.ID=BB.USER_ID WHERE COURSE_ID=D.COURSE_ID ) AS LABEL" +
                        ",  A.YEAR || ',' || A.SEMESTER AS YEAR_HAKGI"
                               + " FROM OP_TAKES A"
                               + " JOIN OP_SECTION B"
                               + " ON A.COURSE_ID = B.COURSE_ID AND A.SEC_ID = B.SEC_ID AND A.SEMESTER = B.SEMESTER AND A.YEAR = B.YEAR"
                               + " JOIN OP_USER C"
                               + " ON A.ID = C.USER_ID"
                               + " JOIN OP_COURSE D"
                               + " ON A.COURSE_ID = D.COURSE_ID"
                               + $" WHERE A.ID = '{userInfo.user_id}'"
                               + " AND A.semester = '" + yearhakgiList[i]["SEMESTER"] + "'"
                               + " AND A.YEAR = '" + yearhakgiList[i]["YEAR"] + "'"
                               + " ORDER BY A.COURSE_ID";


                    // 학기 당 과목리스트
                    List<Dictionary<string, string>> subjList = null;

                    subjList = _commonDao.SelectList(sql);
                    subjList.Add(yearhakgiList[i]);

                    // 반환 리스트
                    Dictionary<string, List<Dictionary<string, string>>> result = new Dictionary<string, List<Dictionary<string, string>>>();

                    result["subjList"] = subjList;

                    resultList.Add(result);
                }
            }
            return resultList;
        }
    }
}
