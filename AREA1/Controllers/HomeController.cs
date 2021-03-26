using AREA1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AREA1.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }

        public IActionResult Index() {
            string DBConnString = "User Id=APPSOFTDEV; Password=appsoftdevpass; Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SID=XE)))";

            OracleConnection conn = null;

            conn = new OracleConnection(DBConnString);

            conn.Open();

            string sql = "select 'abc' from dual";

            OracleDataAdapter adapter = new OracleDataAdapter(sql, conn);

            DataSet ds = new DataSet();

            adapter.Fill(ds, "test");

            ViewData["Title"] = ds.Tables["test"].Rows[0][0].ToString();


            return View();
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
