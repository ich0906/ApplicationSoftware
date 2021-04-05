using AREA1.Data;
using AREA1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace AREA1.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly AppSoftDbContext _context;
        private readonly CommonDao _commonDao;

        public HomeController(ILogger<HomeController> logger, AppSoftDbContext context) {
            _logger = logger;
            _context = context;
            _commonDao = new CommonDao(context);
        }

        public IActionResult Index() {
            using var transaction = _context.Database.BeginTransaction();
            string query = "SELECT 'ABC' AS ABC, 'BCD' AS BCD FROM DUAL";

            // 쿼리 결과가 딕셔너리로 반환됨
            // SelectOne은 단 한건의 결과만 반환됨, 나머지는 날라감
            Dictionary<string, string> result = _commonDao.SelectOne(query);

            // SelectList는 Dictionary의 리스트로 반환됨, 쿼리 결과가 여러줄이 나올 때 사용 가능
            //List< Dictionary<string, string>> resultList = _commonDao.SelectList(query, new DataSet());


            // 컬럼 이름만 집어넣고 바로 사용 가능함
            ViewData["Title"] = result["BCD"];

            transaction.Commit();

            return View();
        }

        public IActionResult InsertData() {
            using var transaction = _context.Database.BeginTransaction();


            string query = "INSERT INTO PERSONS VALUES(" + Request.Form["person_id"] + ","
                                                          + "'" + Request.Form["last_name"] + "',"
                                                          + "'" + Request.Form["first_name"] + "',"
                                                          + "'" + Request.Form["address"] + "',"
                                                          + "'" + Request.Form["city"]
                                                          + "')";


            _commonDao.Insert(query);

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
