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
    public class ScreInqireController : Controller {
        private readonly ILogger<ScreInqireController> _logger;
        private readonly AppSoftDbContext _context;
        private readonly CommonDao _commonDao;
        public ScreInqireController(ILogger<ScreInqireController> logger, AppSoftDbContext context) {
            _logger = logger;
            _context = context;
            _commonDao = new CommonDao(context);
        }
        public IActionResult ScreInqire() {
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");

            ViewData["user_id"] = userInfo.user_id;
            ViewData["name"] = userInfo.name;
            ViewData["Title"] = HttpContext.Session.GetString("_Key");
            ViewData["phone"] = userInfo.phone;
            ViewData["author"]=userInfo.author;

            string sql = "";

            //지도교수 정보
            sql = "SELECT C.EMAIL AS I_EMAIL, A.PHONE AS S_PHONE, C.NAME AS I_NAME, I_ID "
                + "FROM OP_USER A JOIN OP_ADVISOR B "
                + "ON A.USER_ID=B.S_ID "
                + "JOIN OP_USER C ON B.I_ID=C.USER_ID "
                + "WHERE S_ID='" + userInfo.user_id + "'";
            var advisorList = _commonDao.SelectOne(sql);
            ViewBag.advisorList = advisorList;

            sql = "SELECT DISTINCT YEAR,SEMESTER "
                + "FROM OP_TAKES A JOIN OP_COURSE B "
                + "ON A.COURSE_ID=B.COURSE_ID "
                + "WHERE ID='" + userInfo.user_id + "' ORDER BY A.YEAR, A.SEMESTER";
            var yearHakgiList = _commonDao.SelectList(sql);


            sql = "SELECT * "
                + "FROM OP_TAKES A JOIN OP_COURSE B "
                + "ON A.COURSE_ID=B.COURSE_ID "
                + "JOIN OP_SECTION OS on A.SEC_ID = OS.SEC_ID and A.COURSE_ID = OS.COURSE_ID and A.SEMESTER = OS.SEMESTER and A.YEAR = OS.YEAR "
                + "WHERE ID='" + userInfo.user_id + "' ORDER BY A.YEAR, A.SEMESTER";
            var gradeList = _commonDao.SelectList(sql);

            for (int i = 0; i < yearHakgiList.Count; ++i) {
                double sum = 0.0;
                double grade = 0.0, gradeF = 0.0;
                double softGrade = 0.0, softGradeF = 0.0;
                double notSoftGrade = 0.0, notSoftGradeF = 0.0;
                int sbjSum = 0, sbjSumF = 0;
                int softSum = 0, softSumF = 0, notSoftSum = 0, notSoftSumF = 0;

                for (int j = 0; j < gradeList.Count; ++j) {
                    if (yearHakgiList[i]["YEAR"] == gradeList[j]["YEAR"] && yearHakgiList[i]["SEMESTER"] == gradeList[j]["SEMESTER"]) {
                        if (gradeList[j]["GRADE"] != "-") { sbjSumF += Convert.ToInt32(gradeList[j]["CREDITS"]); }
                        if (getGrade(gradeList[j]["GRADE"]) != 0.0) {
                            sum += Convert.ToDouble(gradeList[j]["CREDITS"]);
                        }
                        gradeF += getGrade(gradeList[j]["GRADE"]) * Convert.ToInt32(gradeList[j]["CREDITS"]);
                        if (gradeList[j]["GRADE"] != "F") { sbjSum += Convert.ToInt32(gradeList[j]["CREDITS"]); grade += (getGrade(gradeList[j]["GRADE"]) * Convert.ToInt32(gradeList[j]["CREDITS"])); }

                        if (gradeList[j]["DEPT_NAME"] == "소프트웨어학부" && gradeList[j]["GRADE"] != "-") {
                            softSumF += Convert.ToInt32(gradeList[j]["CREDITS"]);
                            softGradeF += getGrade(gradeList[j]["GRADE"]) * Convert.ToInt32(gradeList[j]["CREDITS"]);
                            if (gradeList[j]["GRADE"] != "F") { softSum += Convert.ToInt32(gradeList[j]["CREDITS"]); softGrade += getGrade(gradeList[j]["GRADE"]) * Convert.ToInt32(gradeList[j]["CREDITS"]); }

                        } else if (gradeList[j]["DEPT_NAME"] != "소프트웨어학부" && gradeList[j]["GRADE"] != "-") {
                            notSoftSumF += Convert.ToInt32(gradeList[j]["CREDITS"]);
                            notSoftGradeF += getGrade(gradeList[j]["GRADE"]) * Convert.ToInt32(gradeList[j]["CREDITS"]);
                            if (gradeList[j]["GRADE"] != "F") { notSoftSum += Convert.ToInt32(gradeList[j]["CREDITS"]); notSoftGrade += getGrade(gradeList[j]["GRADE"]) * Convert.ToInt32(gradeList[j]["CREDITS"]); }
                        }
                    }
                }
                yearHakgiList[i]["SUM"] = sum.ToString();

                if (sbjSumF == 0) yearHakgiList[i]["AVG_GRADE_F"] = string.Format("{0:0.0#}", gradeF);
                else yearHakgiList[i]["AVG_GRADE_F"] = string.Format("{0:0.0#}", gradeF / sbjSumF);

                if (sbjSum == 0) yearHakgiList[i]["AVG_GRADE"] = string.Format("{0:0.0#}", grade);
                else yearHakgiList[i]["AVG_GRADE"] = string.Format("{0:0.0#}", grade / sbjSum);

                if (softSumF == 0) yearHakgiList[i]["SOFT_GRADE_F"] = string.Format("{0:0.0#}", softGradeF);
                else yearHakgiList[i]["SOFT_GRADE_F"] = string.Format("{0:0.0#}", softGradeF / softSumF);

                if (softSum == 0) yearHakgiList[i]["SOFT_GRADE"] = string.Format("{0:0.0#}", softGrade);
                else yearHakgiList[i]["SOFT_GRADE"] = string.Format("{0:0.0#}", softGrade / softSum);

                if (notSoftSumF == 0) yearHakgiList[i]["NOT_SOFT_GRADE_F"] = string.Format("{0:0.0#}", notSoftGradeF);
                else yearHakgiList[i]["NOT_SOFT_GRADE_F"] = string.Format("{0:0.0#}", notSoftGradeF / notSoftSumF);

                if (notSoftSumF == 0) yearHakgiList[i]["NOT_SOFT_GRADE"] = string.Format("{0:0.0#}", notSoftGrade);
                else yearHakgiList[i]["NOT_SOFT_GRADE"] = string.Format("{0:0.0#}", notSoftGrade / notSoftSum);
            }

            ViewBag.yearHakgiList = yearHakgiList;
            ViewBag.yearHakgiCount = yearHakgiList.Count;
            ViewBag.gradeList = gradeList;
            ViewBag.gradeCount = gradeList.Count;

            return View("/Views/LrnResult/ScreInqire.cshtml");
        }

        public double getGrade(string grade) {
            switch (grade) {
                case "A+":
                    return 4.5;
                case "A":
                    return 4.0;
                case "B+":
                    return 3.5;
                case "B":
                    return 3.0;
                case "C+":
                    return 2.5;
                case "C":
                    return 2.0;
                case "D+":
                    return 1.5;
                case "D":
                    return 1.0;
                default:
                    return 0.0;
            }
        }

    }
}


