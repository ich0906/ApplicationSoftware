using AREA1.Data;
using AREA1.Filters;
using AREA1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Tool;

namespace AREA1.Controllers {
    [LoginActionFilter]
    public class StandInqireController : Controller {
        private readonly ILogger<StandInqireController> _logger;
        private readonly AppSoftDbContext _context;
        private readonly CommonDao _commonDao;
        public StandInqireController(ILogger<StandInqireController> logger, AppSoftDbContext context) {
            _logger = logger;
            _context = context;
            _commonDao = new CommonDao(context);
        }
        public IActionResult SelectPageListStandInqire() {
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");
            ViewData["name"] = userInfo.name;                                       // 이름
            ViewData["user_id"] = userInfo.user_id;                                 // 유저 ID(학번)
            ViewBag.userInfo = userInfo;

            string query = "";

            // 최신 학기에 수강한 과목의 목록
            query = "SELECT A.*                                                                                     "
                    + "FROM OP_TAKES A                                                                              "
                    + "         JOIN(SELECT Distinct(YEAR) AS YEAR, SEMESTER                                        "
                    + "               FROM(                                                                         "
                    + "                        SELECT FIRST_VALUE(YEAR) over(ORDER BY YEAR DESC)         AS YEAR,   "
                    + "                               FIRST_VALUE(SEMESTER) over(ORDER BY SEMESTER DESC) AS SEMESTER"
                    + "                        FROM OP_SECTION)) B                                                  "
                    + "              ON A.YEAR = B.YEAR AND A.SEMESTER = B.SEMESTER                                 "
                    + $"WHERE A.ID = '{userInfo.user_id}'";

            var recentlyTakeList = _commonDao.SelectList(query);
            ViewBag.RecentlyGradeAt = "N";

            // 최신 연도, 학기
            query = "SELECT Distinct(YEAR) AS YEAR, SEMESTER                                        "
                    + "FROM(                                                                        "
                    + "        SELECT FIRST_VALUE(YEAR) over(ORDER BY YEAR DESC)         AS YEAR,   "
                    + "               FIRST_VALUE(SEMESTER) over(ORDER BY SEMESTER DESC) AS SEMESTER"
                    + "        FROM OP_SECTION)";

            var recentYearHakgi = _commonDao.SelectOne(query);

            ViewBag.RecentYear = recentYearHakgi["YEAR"];
            ViewBag.RecentHakgi = recentYearHakgi["SEMESTER"];

            for (int i = 0; i<recentlyTakeList.Count; i++) {
                var recentlyTake = recentlyTakeList[i];
                
                // 수강한 과목을 가져와서 성적이 입력되어있지 않거나 -처리 되어 있지 않은 경우 성적이 입력 된 것임
                if(!recentlyTake["GRADE"].Equals("") && !recentlyTake["GRADE"].Equals("-")) {
                    ViewBag.RecentlyGradeAt = "Y";
                    break;
                }
            }

            query = "SELECT AAAA.*                                                                                                         "
                    + "FROM(SELECT AAA.*,                                                                                                    "
                    + "             TRUNC(CASE                                                                                               "
                    + "                       WHEN AVG_JUMSU <= 4.5 AND AVG_JUMSU >= 3.8 THEN((AVG_JUMSU - 1) * (40 / 3.5)) + 60             "
                    + "                       ELSE((AVG_JUMSU - 1) * 10) + 64 END, 2)                                          AS PERCENTAGE,"
                    + "             RANK() OVER(PARTITION BY AAA.YEAR, AAA.SEMESTER ORDER BY AAA.AVG_JUMSU DESC) || ' / ' ||                 "
                    + "             (SELECT COUNT(DISTINCT ID) FROM OP_TAKES WHERE YEAR = AAA.YEAR AND SEMESTER = AAA.SEMESTER) AS RANK      "
                    + "      FROM(                                                                                                           "
                    + "               SELECT AA.YEAR,                                                                                        "
                    + "                      AA.SEMESTER,                                                                                    "
                    + "                      AA.ID,                                                                                          "
                    + "                      AA.TOTAL_CREDITS,                                                                               "
                    + "                      AA.TOTAL_GRADE_JUMSU,                                                                           "
                    + "                      TRUNC(AA.TOTAL_GRADE_JUMSU / AA.TOTAL_CREDITS, 2) AS AVG_JUMSU                                  "
                    + "               FROM(                                                                                                  "
                    + "                        SELECT A.YEAR,                                                                                "
                    + "                               A.SEMESTER,                                                                            "
                    + "                               A.ID,                                                                                  "
                    + "                               SUM(C.CREDITS)                  AS TOTAL_CREDITS,                                      "
                    + "                               SUM(CASE                                                                               "
                    + "                                       WHEN GRADE = 'A+' THEN 4.5                                                     "
                    + "                                       WHEN GRADE = 'A' THEN 4                                                        "
                    + "                                       WHEN GRADE = 'B+' THEN 3.5                                                     "
                    + "                                       WHEN GRADE = 'B' THEN 3                                                        "
                    + "                                       WHEN GRADE = 'C+' THEN 2.5                                                     "
                    + "                                       WHEN GRADE = 'C' THEN 2                                                        "
                    + "                                       WHEN GRADE = 'D+' THEN 1.5                                                     "
                    + "                                       WHEN GRADE = 'D' THEN 1                                                        "
                    + "                                       ELSE 0 END * C.CREDITS) AS TOTAL_GRADE_JUMSU                                   "
                    + "                        FROM OP_TAKES A                                                                               "
                    + "                                 JOIN(                                                                                "
                    + "                            SELECT YEAR, SEMESTER                                                                     "
                    + "                            FROM OP_TAKES                                                                             "
                    + "                            WHERE ID = '2017203062') B                                                                "
                    + "                                      ON A.YEAR = B.YEAR AND A.SEMESTER = B.SEMESTER                                  "
                    + "                                 JOIN OP_COURSE C                                                                     "
                    + "                                      ON A.COURSE_ID = C.COURSE_ID                                                    "
                    + "                        GROUP BY A.ID, A.YEAR, A.SEMESTER                                                             "
                    + "                        ORDER BY A.YEAR DESC, A.SEMESTER DESC) AA) AAA                                                "
                    + "     ) AAAA                                                                                                           "
                    +$"WHERE AAAA.ID = '{userInfo.user_id}' ORDER BY YEAR DESC, SEMESTER DESC                                                ";

            var rankList = _commonDao.SelectList(query);

            ViewBag.RankList = rankList;

            return View("/Views/LrnResult/SelectPageListStandInqire.cshtml");
        }
    }
}
