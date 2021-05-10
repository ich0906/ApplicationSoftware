using AREA1.Data;
using AREA1.Filters;
using AREA1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Tool;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Text;
using System;
using System.Security.Cryptography;



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
            ViewData["name"] = userInfo.name;
            ViewData["user_id"] = userInfo.user_id;
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

            if(userInfo.author.Equals("1000")) {
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
