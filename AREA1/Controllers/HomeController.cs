using AREA1.Data;
using AREA1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using Tool;

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
            //Dictionary<string, string> result = _commonDao.SelectOne(query, Request.Form);

            string query2 = "SELECT * FROM PERSONS";
            List<Dictionary<string, string>> resultList = _commonDao.SelectList(query2);

            // SelectList는 Dictionary의 리스트로 반환됨, 쿼리 결과가 여러줄이 나올 때 사용 가능
            //List< Dictionary<string, string>> resultList = _commonDao.SelectList(query, new DataSet());


            // 컬럼 이름만 집어넣고 바로 사용 가능함

            ViewData["Title"] = HttpContext.Session.GetString("_Key");

            transaction.Commit();

            return View();
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
        public IActionResult UpdateData()
        {
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
        public IActionResult DeleteData()
        {
            using var transaction = _context.Database.BeginTransaction();


            string query = "DELETE FROM PERSONS WHERE PERSON_ID = @person_id:NUMBER";

            _commonDao.Delete(query, Request.Form);


            transaction.Commit();

            return Redirect("Index");
        }
        public List<JsonResult> SelectData()
        {
            using var transaction = _context.Database.BeginTransaction();

            string query = "SELECT * FROM PERSONS";
            List<Dictionary<string, string>> resultList = _commonDao.SelectList(query);

            var persons = new List<JsonResult>();
            for (int i = 0; i < resultList.Count; ++i)
            {
                var person = new Dictionary<string, string>()
                {
                    {"PERSON_ID",resultList[i]["PERSON_ID"]},
                    {"LAST_NAME",resultList[i]["LAST_NAME"]},
                    {"FIRST_NAME",resultList[i]["FIRST_NAME"]},
                    {"ADDRESS",resultList[i]["ADDRESS"]},
                    {"CITY",resultList[i]["CITY"]}
                };
                persons.Add(Json(person));
            }
            return persons;
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
