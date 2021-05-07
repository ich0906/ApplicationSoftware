using AREA1.Data;
using AREA1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Tool;

namespace AREA1.Login.Controllers {
    public class LoginController : Controller {
        private readonly ILogger<LoginController> _logger;
        private readonly AppSoftDbContext _context;
        private readonly CommonDao _commonDao;

        public LoginController(ILogger<LoginController> logger, AppSoftDbContext context) {
            _logger = logger;
            _context = context;
            _commonDao = new CommonDao(context);
        }
     

        public IActionResult Login() {
            string alertMsg = "";

            switch(Convert.ToInt32(Request.Query["alertLogin"])) {
                case 1:
                    alertMsg = "로그인 정보가 틀렸습니다.";
                    break;

                case 2:
                    alertMsg = "로그인 정보가 만료되었습니다.";
                    break;
            }

            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");

            if (userInfo != null) {
                return Redirect("/Main");
            }

            ViewData["AlertMsg"] = alertMsg;

            return View();
        }

        public IActionResult DoLogin() {
            if (!Request.Form["login_id"].Equals(""))
            {
                string query = "SELECT USER_ID" +
                                    ", NAME" +
                                    ", AUTHOR" +
                                    ", BIRTHDAY" +
                                    ", PHONE" +
                                    ", EMAIL" +
                                " FROM OP_USER" +
                                " WHERE USER_ID = @login_id:CHAR" +
                                    " AND PASSWORD = @login_pass:VARCHAR";

                // 쿼리 결과가 딕셔너리로 반환됨
                // SelectOne은 단 한건의 결과만 반환됨, 나머지는 날라감
                Dictionary<string, string> result = _commonDao.SelectOne(query, Request.Form);

                if (!result["USER_ID"].Equals(""))
                {
                    UserModel userInfo = new UserModel();
                    userInfo.user_id = result["USER_ID"];
                    userInfo.name = result["NAME"];
                    userInfo.author = result["AUTHOR"];
                    userInfo.birthday = result["BIRTHDAY"];
                    userInfo.phone = result["PHONE"];
                    userInfo.email = result["EMAIL"];

                    SessionExtensionTool.SetObject(HttpContext.Session, "userInfo", userInfo);

                    return Redirect("/Main/Main");
                }
            }


            return RedirectToAction("Login", new { alertLogin = 1 });
        }

        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
