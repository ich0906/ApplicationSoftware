using AREA1.Data;
using AREA1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace AREA1.Controllers.Lrn_Sport.Lct_Gnrlz
{
    public class LctGnrlzController : Controller {
        private readonly ILogger<LctGnrlzController> _logger;
        private readonly AppSoftDbContext _context;
        private readonly CommonDao _commonDao;

        public LctGnrlzController(ILogger<LctGnrlzController> logger, AppSoftDbContext context) {
            _logger = logger;
            _context = context;
            _commonDao = new CommonDao(context);
        }

        public IActionResult LctGnrlz() {

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
