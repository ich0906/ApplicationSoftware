using AREA1.Data;
using AREA1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
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
          
            // 쿼리 결과가 datatable로 반환됨
            // datatable은 쿼리 결과로 나오는 테이블을 c#객체로 구현한 것이라고 생각하면 됨
            DataTable result = _commonDao.selectOne(query, new DataSet());


            // 행과 열을 지정해서 조회할수도 있고
            ViewData["Title"] = result.Rows[0]["BCD"].ToString();
            // 열만 선택해서 조회할수도 있음
            ViewData["Title"] = result.Columns["BCD"].ToString();

            transaction.Commit();


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
