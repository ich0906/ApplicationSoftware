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

namespace AREA1.Controllers {
    [LoginActionFilter]
    public class MainController : Controller {
        private readonly ILogger<MainController> _logger;
        private readonly AppSoftDbContext _context;
        private readonly CommonDao _commonDao;
        public MainController(ILogger<MainController> logger, AppSoftDbContext context) {
            _logger = logger;
            _context = context;
            _commonDao = new CommonDao(context);
        }
        public IActionResult Main() {
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");

            if (userInfo == null) {
                return RedirectToAction("/Login", new { alertLogin = 2 });
            }

            ViewData["name"] = userInfo.name;
            ViewData["user_id"] = userInfo.user_id;

            ViewData["Title"] = HttpContext.Session.GetString("_Key");

            string sql = "SELECT COUNT(*) AS TAKES_CNT"
                + " FROM OP_TAKES"
                + " WHERE ID=" + userInfo.user_id;

            int takes_cnt = 0;
            takes_cnt = Convert.ToInt32(_commonDao.SelectOne(sql)["TAKES_CNT"]);

            if (takes_cnt > 0) {
                sql = "SELECT TITLE,NAME,DAY1,DAY2,PERIOD1,PERIOD2,BUILDING,ROOM_NUMBER,"
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

                var resultList = _commonDao.SelectList(sql);
                ViewBag.ResultList = resultList;
                ViewBag.ResultCount = resultList.Count;
            }

            return View("/Views/Main.cshtml");
        }

        public List<Dictionary<string, List<Dictionary<string, string>>>> YearhakgiAtnlcSbjectList() {
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");
            Dictionary<string, string> param = new Dictionary<string, string>();
            // 학기 당 과목리스트
            List<Dictionary<string, string>> subjList = null;
            // 학기 정보 리스트
            List<Dictionary<string, List<Dictionary<string, string>>>> resultList = new List<Dictionary<string, List<Dictionary<string, string>>>>();

            param["USER_ID"] = userInfo.user_id;

            if (userInfo.author.Equals("1000")) {
                string sql = "SELECT B.ACDMC_NO, A.SEMESTER, A.YEAR, D.TITLE || ' (' || B.ACDMC_NO || ') - ' || C.NAME AS LABEL"
                           + " FROM OP_TEACHES A"
                           + " JOIN OP_SECTION B"
                           + " ON A.COURSE_ID = B.COURSE_ID AND A.SEC_ID = B.SEC_ID AND A.SEMESTER = B.SEMESTER AND A.YEAR = B.YEAR"
                           + " JOIN OP_USER C"
                           + " ON A.ID = C.USER_ID"
                           + " JOIN OP_COURSE D"
                           + " ON A.COURSE_ID = D.COURSE_ID"
                           + $" WHERE A.ID = '{userInfo.user_id}'"
                           + " AND A.semester = '1'"
                           + " AND A.YEAR = '2021'"
                           + " ORDER BY A.COURSE_ID";


                Dictionary<string, List<Dictionary<string, string>>> result = new Dictionary<string, List<Dictionary<string, string>>>();

                subjList = _commonDao.SelectList(sql);
                result["subjList"] = subjList;

                resultList.Add(result);
            }
            return resultList;
        }
    }
}
