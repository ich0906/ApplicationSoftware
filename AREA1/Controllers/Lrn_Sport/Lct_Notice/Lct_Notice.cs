using AREA1.Data;
using AREA1.Filters;
using AREA1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace AREA1.Controllers.Lrn_Sport.Lct_Notice {
    [LoginActionFilter]
    public class Lct_Notice : Controller {
        private readonly ILogger<Lct_Notice> _logger;
        private readonly AppSoftDbContext _context;
        private readonly CommonDao _commonDao;

        public Lct_Notice(ILogger<Lct_Notice> logger, AppSoftDbContext context) {
            _logger = logger;
            _context = context;
            _commonDao = new CommonDao(context);
        }

        public IActionResult SelectPageListNotice() {
            string sql = "SELECT COUNT(*) AS BBS_CNT"                                                                                                      
                      + "FROM OP_BBS A"
                      + "JOIN OP_USER B"
                      + "ON A.REGISTER = B.USER_ID"
                      + "JOIN OP_FILE C"
                      + "ON A.DOC_ID = C.DOC_ID AND C.SNO = 1"
                      + "WHERE BBS_CODE = " //+ CodeMngTool.getCode("BBS", "NOTICE") 
                      + "AND LCTRE_SE = @LCTRE_SE:VARCHAR";

            int bbsCnt = Convert.ToInt32(_commonDao.SelectOne(sql, Request.Form)["BBS_CNT"]);

            if (bbsCnt > 0) {

                sql = "SELECT A.SJ"                                                                                                                 // 게시글 제목
                                + ", (SELECT DECODE(COUNT(*), 0, 'F', 'Y') FROM OP_FILE WHERE DOC_ID = A.DOC_ID) AS FILE_AT"                        // 파일 여부
                                + ", (SELECT DECODE(COUNT(*), 0, '', FILE_ID) FROM OP_FILE WHERE DOC_ID = A.DOC_ID AND SNO = 1) AS FILE_ID"         // 첫번째 첨부파일 ID
                                + ", B.NAME AS REGISTER"                                                                                            // 작성자명
                                + ", A.REGIST_DT"                                                                                                   // 작성일
                                + ", A.RDCNT"                                                                                                       // 조회수
                          + "FROM OP_BBS A"
                          + "JOIN OP_USER B"
                          + "ON A.REGISTER = B.USER_ID"
<<<<<<< HEAD
=======
                          + "JOIN OP_FILE C"
                          + "ON A.DOC_ID = C.DOC_ID AND C.SNO = 1"
>>>>>>> 2fe69837d42967dd9f18e14aa989536a153510c2
                          + "WHERE BBS_CODE = " //+ CodeMngTool.getCode("BBS", "NOTICE") 
                          + "AND LCTRE_SE = @LCTRE_SE:VARCHAR";

                var resultList = _commonDao.SelectList(sql, Request.Form);
                ViewBag.ResultList = resultList;
            }

            ViewBag.ResultCnt = bbsCnt;

            return View("BoardListStdPage");
        }

        public IActionResult SelectNotice() {
            return View();
        }

        public IActionResult InsertNotice() {
            return View();
        }

        public IActionResult UpdateNotice() {
            return View();
        }
        public IActionResult DeleteNotice() {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
