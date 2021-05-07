using AREA1.Data;
using AREA1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace AREA1.Controllers.Lrn_Sport.Lrn_Sttus_Inqire
{
    public class Lrn_Sttus_Inqire : Controller {
        private readonly ILogger<Lrn_Sttus_Inqire> _logger;
        private readonly AppSoftDbContext _context;
        private readonly CommonDao _commonDao;

        public Lrn_Sttus_Inqire(ILogger<Lrn_Sttus_Inqire> logger, AppSoftDbContext context) {
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

            // SelectList는 Dictionary의 리스트로 반환됨, 쿼리 결과가 여러줄이 나올 때 사용 가능
            //List< Dictionary<string, string>> resultList = _commonDao.SelectList(query, new DataSet());


            // 컬럼 이름만 집어넣고 바로 사용 가능함
            //ViewData["Title"] = result["BCD"];

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

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
