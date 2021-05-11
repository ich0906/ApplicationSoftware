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
    public class MainController : Controller
    {
        private readonly ILogger<MainController> _logger;
        private readonly AppSoftDbContext _context;
        private readonly CommonDao _commonDao;
        public MainController(ILogger<MainController> logger, AppSoftDbContext context)
        {
            _logger = logger;
            _context = context;
            _commonDao = new CommonDao(context);
        }
        public IActionResult Main()
        {
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");

            if (userInfo == null)
            {
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

            if (takes_cnt > 0) {
                sql = "SELECT TITLE,NAME,DAY1,DAY2,PERIOD1,PERIOD2,BUILDING,ROOM_NUMBER,USER_ID,ACDMC_NO,"
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

                var resultList = _commonDao.SelectList(sql);
                ViewBag.ResultList = resultList;
                ViewBag.ResultCount = resultList.Count;
            }

            sql = "SELECT COUNT(*) AS TEACHES_CNT "
                + "FROM OP_TEACHES "
                + "WHERE ID=" + userInfo.user_id;

            int teaches_cnt = 0;
            teaches_cnt= Convert.ToInt32(_commonDao.SelectOne(sql)["TEACHES_CNT"]);

            if (teaches_cnt > 0) {
                sql = "SELECT TITLE,ACDMC_NO,DAY1,DAY2,PERIOD1,PERIOD2,BUILDING,ROOM_NUMBER FROM OP_TEACHES A "
                    + "JOIN OP_USER B ON A.ID = B.USER_ID "
                    + "JOIN OP_SECTION C on A.SEC_ID = C.SEC_ID and A.COURSE_ID = C.COURSE_ID and A.SEMESTER = C.SEMESTER and A.YEAR = C.YEAR "
                    + "JOIN OP_COURSE D ON C.COURSE_ID = D.COURSE_ID "
                    + "JOIN OP_TIME_SLOT E on C.TIME_SLOT_ID = E.TIME_SLOT_ID ";

                var resultList2 = _commonDao.SelectList(sql);
                ViewBag.ResultListTeacher = resultList2;
                ViewBag.TeachesCount = resultList2.Count;
            }

            return View("/Views/Main.cshtml");
        }
    }
}
