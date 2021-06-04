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
        public IActionResult StandInqire() {
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");

            ViewData["user_id"] = userInfo.user_id;
            ViewData["name"] = userInfo.name;
            ViewData["Title"] = HttpContext.Session.GetString("_Key");
            string query_year = "SELECT COUNT(DISTINCT YEAR) AS CNTTT FROM OP_TAKES WHERE" + $" ID = '{userInfo.user_id}' ORDER BY YEAR";
            int count_year = Convert.ToInt32(_commonDao.SelectOne(query_year)["CNTTT"]);

            ViewData["takesYear"] = count_year;
            List<string> yArr = new List<string>();
            string query_years = "SELECT DISTINCT YEAR FROM OP_TAKES WHERE" + $" ID = '{userInfo.user_id}' ORDER BY YEAR";
            List<Dictionary<string, string>> takesYears = _commonDao.SelectList(query_years);
            for (int i = 0; i < takesYears.Count; i++) {
                var tmpresult = new Dictionary<string, string>()
                        {
                              {"YEAR",takesYears[i]["YEAR"]},
                        };
                if (tmpresult.TryGetValue("YEAR", out string tmpYear)) {
                    string tmp;
                    tmp = tmpYear;
                    yArr.Add(tmp);
                }
            }

            List<string> sArr = new List<string>();
            sArr.Add("1");
            sArr.Add("2");
            int count_sem = 2;
            string grdTostr = "";
            string MyRank = "";
            string sumTostr = "";
            string allgrd = "";
            string perc = "";
            string t2 = "";
            string query_userID = "SELECT USER_ID FROM OP_USER WHERE AUTHOR = '2000'";
            List<Dictionary<string, string>> userID_set = _commonDao.SelectList(query_userID);
            string[] userID_arr = new string[userID_set.Count];
            for (int i = 0; i < userID_set.Count; i++) {
                var IDresult = new Dictionary<string, string>()
                        {
                              {"USER_ID",userID_set[i]["USER_ID"]},
                        };
                if (IDresult.TryGetValue("USER_ID", out string tmpID)) {
                    string tmp1 = tmpID;
                    userID_arr[i] = tmp1;
                }
            }
            DateTime date = DateTime.Now;
            string datetime = date.ToString("yyyy.MM");
            string[] yearhakgi = datetime.Split(new string[] { "." }, System.StringSplitOptions.None);
            ViewData["year"] = yearhakgi[0];
            string cur_month = yearhakgi[1];
            int cur_hakgi = Convert.ToInt32(cur_month);
            ViewData["hakgi"] = cur_month;
            if (cur_hakgi > 2 && cur_hakgi < 9)
                ViewData["hakgi"] = "1";
            else
                ViewData["hakgi"] = "2";

            //포문 스타트
            for (int y = 0; y < yArr.Count; y++) {
                for (int j = 0; j < sArr.Count; j++) {
                    //수강학생

                    string query1 = "SELECT ID FROM OP_TAKES WHERE YEAR =" + $"'{yArr[y]}' AND SEMESTER ='{sArr[j]}' GROUP BY ID";
                    List<Dictionary<string, string>> userID_List = _commonDao.SelectList(query1);
                    for (int i = 0; i < userID_List.Count; i++) {
                        var tmplist = new Dictionary<string, string>()
                        {
                                {"ID",userID_List[i]["ID"]}
                         };
                        if (tmplist.TryGetValue("ID", out string tmpID)) {
                            string temp1 = "";
                            temp1 = tmpID;
                        }
                    }
                    int count_students = userID_List.Count;
                    ViewData["countstudents" + y + j] = count_students;

                    //학생 수 포문 돌리기
                    for (int z = 0; z < userID_arr.Length; z++) {
                        using var transaction = _context.Database.BeginTransaction();
                        string query = "SELECT B.CREDITS, OT.GRADE " +
                            "FROM OP_SECTION A JOIN OP_COURSE B " +
                            "ON A.COURSE_ID = B.COURSE_ID " +
                            "RIGHT JOIN OP_TAKES OT " +
                            "on A.SEC_ID = OT.SEC_ID " +
                            "and A.COURSE_ID = OT.COURSE_ID " +
                            "and A.SEMESTER = OT.SEMESTER " +
                           "and A.YEAR = OT.YEAR "
                           + $"WHERE OT.ID = '{userID_arr[z]}' and OT.YEAR = '{yArr[y]}' and OT.SEMESTER = '{sArr[j]}'";

                        List<Dictionary<string, string>> semResult = _commonDao.SelectList(query);
                        transaction.Commit();

                        string queryX = "SELECT COUNT(*) AS CNT FROM OP_TAKES WHERE ID = " + $"'{userID_arr[z]}'and YEAR = '{yArr[y]}' and SEMESTER = {sArr[j]} ";

                        int sum = 0;
                        double sum_grd = 0;
                        string tmp = "";
                        int count = Convert.ToInt32(_commonDao.SelectOne(queryX)["CNT"]);
                        List<string> credList = new List<string>();
                        List<string> grdList = new List<string>();

                        //딕셔너리 안 매핑된 밸류 꺼내기
                        for (int i = 0; i < semResult.Count; i++) {
                            var tmpresult = new Dictionary<string, string>()
                        {
                              {"CREDITS",semResult[i]["CREDITS"]},
                              {"GRADE",semResult[i]["GRADE"]}
                        };
                            if (tmpresult.TryGetValue("CREDITS", out string tmpCredit)) {
                                tmp = tmpCredit;
                                if (tmp.Equals("")) continue;
                                credList.Add(tmp);
                            }
                            if (tmpresult.TryGetValue("GRADE", out string tmpGrd)) {
                                tmp = tmpGrd;
                                if (tmp.Equals("")) continue;
                                grdList.Add(tmp);
                            }
                        }
                        //총 신청학점 
                        for (int i = 0; i < credList.Count; i++) {
                            string watchlist = credList[i];
                            int tmpCredit = Convert.ToInt32(credList[i]);
                            sum += tmpCredit;
                        }
                        // 학점 구하기
                        for (int i = 0; i < grdList.Count; i++) {
                            if (grdList[i] == "A+") {
                                grdList[i] = "4.5";
                                double cvtgrd = Convert.ToDouble(grdList[i]);
                                double cvtCredit = Convert.ToDouble(credList[i]);
                                double totalscore = cvtgrd * cvtCredit;
                                sum_grd += totalscore;
                            }
                            if (grdList[i] == "A") {
                                grdList[i] = "4.0";
                                double cvtgrd = Convert.ToDouble(grdList[i]);
                                double cvtCredit = Convert.ToDouble(credList[i]);
                                double totalscore = cvtgrd * cvtCredit;
                                sum_grd += totalscore;
                            }
                            if (grdList[i] == "B+") {
                                grdList[i] = "3.5";
                                double cvtgrd = Convert.ToDouble(grdList[i]);
                                double cvtCredit = Convert.ToDouble(credList[i]);
                                double totalscore = cvtgrd * cvtCredit;
                                sum_grd += totalscore;
                            }
                            if (grdList[i] == "B") {
                                grdList[i] = "3.0";
                                double cvtgrd = Convert.ToDouble(grdList[i]);
                                double cvtCredit = Convert.ToDouble(credList[i]);
                                double totalscore = cvtgrd * cvtCredit;
                                sum_grd += totalscore;
                            }
                            if (grdList[i] == "C+") {
                                grdList[i] = "2.5";
                                double cvtgrd = Convert.ToDouble(grdList[i]);
                                double cvtCredit = Convert.ToDouble(credList[i]);
                                double totalscore = cvtgrd * cvtCredit;
                                sum_grd += totalscore;
                            }
                            if (grdList[i] == "C") {
                                grdList[i] = "2.0";
                                double cvtgrd = Convert.ToDouble(grdList[i]);
                                double cvtCredit = Convert.ToDouble(credList[i]);
                                double totalscore = cvtgrd * cvtCredit;
                                sum_grd += totalscore;
                            }
                            if (grdList[i] == "D+") {
                                grdList[i] = "1.5";
                                double cvtgrd = Convert.ToDouble(grdList[i]);
                                double cvtCredit = Convert.ToDouble(credList[i]);
                                double totalscore = cvtgrd * cvtCredit;
                                sum_grd += totalscore;
                            }
                            if (grdList[i] == "D") {
                                grdList[i] = "1.0";
                                double cvtgrd = Convert.ToDouble(grdList[i]);
                                double cvtCredit = Convert.ToDouble(credList[i]);
                                double totalscore = cvtgrd * cvtCredit;
                                sum_grd += totalscore;
                            }
                            if (grdList[i] == "F") {
                                grdList[i] = "0.0";
                                double cvtgrd = Convert.ToDouble(grdList[i]);
                                double cvtCredit = Convert.ToDouble(credList[i]);
                                double totalscore = cvtgrd * cvtCredit;
                                sum_grd += totalscore;
                            }


                            double grd = sum_grd / sum;
                            sumTostr = sum.ToString();
                            allgrd = sum_grd.ToString();
                            grdTostr = grd.ToString("N2");

                            double t1 = 0.0; //임시로 평점평균 저장

                            if (grd >= 3.8) {
                                t1 = ((grd - 1.0) * (40.0 / 3.5)) + 60.0;
                                t2 = " ";
                            }
                            if (1.5 < grd || grd < 3.8) {
                                t1 = ((grd - 1) * 10.0) + 64.0;
                                t2 = " ";
                            }
                            if (grd <= 1.5) {
                                t2 = "Y";
                            }
                            perc = t1.ToString("N2");
                            if (userInfo.user_id == userID_arr[z]) {
                                ViewData["allcred" + y + j] = sumTostr;
                                ViewData["allgrd" + y + j] = allgrd;
                                ViewData["grd" + y + j] = grdTostr;
                                ViewData["per" + y + j] = perc;
                                ViewData["probation" + y + j] = t2;
                            }

                        }

                        string[] arr = new string[sArr.Count];
                        arr[j] = grdTostr;

                        using var transaction2 = _context.Database.BeginTransaction();
                        string query2 = "INSERT INTO OP_TEMP VALUES" + $"('{userID_arr[z]}'," +
                            $"'{yArr[y]}'," +
                            $"'{sArr[j]}'," +
                            $"'{sumTostr}'," +
                            $"'{allgrd}'," +
                            $"'{grdTostr}'," +
                            $"'{perc}'," +
                            $"'{count_students}'," +
                            $"'{t2}')";
                        _commonDao.Insert(query2);
                        transaction2.Commit();
                        t2 = " ";
                        using var transaction4 = _context.Database.BeginTransaction();
                        string queryDeleteRow = "DELETE FROM OP_TEMP WHERE GRADE is null";
                        _commonDao.Delete(queryDeleteRow);
                        transaction4.Commit();
                    }
                    //00학기의 성적만 들어옴.
                    string queryRank = "SELECT ROWNUM, A.* FROM (SELECT DISTINCT * FROM OP_TEMP ORDER BY GRADE DESC) A";
                    List<Dictionary<string, string>> rank_set = _commonDao.SelectList(queryRank);
                    for (int i = 0; i < rank_set.Count; i++) {
                        string[] rank_arr = new string[rank_set.Count];
                        string[] id_arr = new string[rank_set.Count];
                        string[] grade_arr = new string[rank_set.Count];
                        var Rankresult = new Dictionary<string, string>()
                        {
                              {"ROWNUM",rank_set[i]["ROWNUM"]},
                              {"ID",rank_set[i]["ID"]}
                        };
                        if (Rankresult.TryGetValue("ID", out string tmpID)) {
                            string tmp1 = tmpID;
                            id_arr[i] = tmp1;
                        }

                        if (Rankresult.TryGetValue("ROWNUM", out string tmprank)) {
                            string tmp1 = tmprank;
                            rank_arr[i] = tmp1;
                        }
                        if (userInfo.user_id == id_arr[i]) {
                            if (y == 0 && j == 0) {
                                MyRank = rank_arr[i];
                                ViewData["rank00"] = MyRank;
                            } else if (y == 0 && j == 1) {
                                MyRank = rank_arr[i];
                                ViewData["rank01"] = MyRank;
                            } else if (y == 1 && j == 0) {
                                MyRank = rank_arr[i];
                                ViewData["rank10"] = MyRank;
                            }
                            break;
                        }

                    }
                    //op_temp 비우기
                    using var transaction3 = _context.Database.BeginTransaction();
                    string query3 = "DELETE FROM OP_TEMP";
                    _commonDao.Delete(query3);
                    transaction3.Commit();
                    grdTostr = "";

                }//for(s)

            }//for(y)



            return View("/Views/LrnResult/StandInqire.cshtml");
        }
        public List<JsonResult> PreRankList() {
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");

            ViewData["user_id"] = userInfo.user_id;
            ViewData["name"] = userInfo.name;
            ViewData["Title"] = HttpContext.Session.GetString("_Key");
            var ranklist = new List<JsonResult>();//리턴할 리스트
            List<Dictionary<string, string>> rank_set = new List<Dictionary<string, string>>();
            List<string> yArr = new List<string>();
            string query_years = "SELECT DISTINCT YEAR FROM OP_TAKES WHERE" + $" ID = '{userInfo.user_id}' ORDER BY YEAR";
            List<Dictionary<string, string>> takesYears = _commonDao.SelectList(query_years);
            for (int i = 0; i < takesYears.Count; i++) {
                var tmpresult = new Dictionary<string, string>()
                        {
                              {"YEAR",takesYears[i]["YEAR"]},
                        };
                if (tmpresult.TryGetValue("YEAR", out string tmpYear)) {
                    string tmp;
                    tmp = tmpYear;
                    yArr.Add(tmp);
                }
            }
            List<string> sArr = new List<string>();
            sArr.Add("1");
            sArr.Add("2");
            int count_sem = 2;
            string grdTostr = "";
            string MyRank = "";
            string sumTostr = "";
            string allgrd = "";
            string perc = "";
            string t2 = "";
            string query_userID = "SELECT USER_ID FROM OP_USER WHERE AUTHOR = '2000'";
            List<Dictionary<string, string>> userID_set = _commonDao.SelectList(query_userID);
            string[] userID_arr = new string[userID_set.Count];
            for (int i = 0; i < userID_set.Count; i++) {
                var IDresult = new Dictionary<string, string>()
                        {
                              {"USER_ID",userID_set[i]["USER_ID"]},
                        };
                if (IDresult.TryGetValue("USER_ID", out string tmpID)) {
                    string tmp1 = tmpID;
                    userID_arr[i] = tmp1;
                }
            }


            //포문 스타트
            for (int y = 0; y < yArr.Count; y++) {
                for (int j = 0; j < sArr.Count; j++) {
                    int nograde = 0;
                    //수강학생
                    string query1 = "SELECT ID FROM OP_TAKES WHERE YEAR =" + $"'{yArr[y]}' AND SEMESTER ='{sArr[j]}' GROUP BY ID";
                    List<Dictionary<string, string>> userID_List = _commonDao.SelectList(query1);
                    for (int i = 0; i < userID_List.Count; i++) {
                        var tmplist = new Dictionary<string, string>()
                        {
                                {"ID",userID_List[i]["ID"]}
                         };
                        if (tmplist.TryGetValue("ID", out string tmpID)) {
                            string temp1 = "";
                            temp1 = tmpID;
                        }
                    }
                    int count_students = userID_List.Count;
                    ViewData["countstudents" + y + j] = count_students;

                    //학생 수 포문 돌리기
                    for (int z = 0; z < userID_arr.Length; z++) {
                        using var transaction = _context.Database.BeginTransaction();
                        string query = "SELECT B.CREDITS, OT.GRADE " +
                            "FROM OP_SECTION A JOIN OP_COURSE B " +
                            "ON A.COURSE_ID = B.COURSE_ID " +
                            "RIGHT JOIN OP_TAKES OT " +
                            "on A.SEC_ID = OT.SEC_ID " +
                            "and A.COURSE_ID = OT.COURSE_ID " +
                            "and A.SEMESTER = OT.SEMESTER " +
                           "and A.YEAR = OT.YEAR "
                           + $"WHERE OT.ID = '{userID_arr[z]}' and OT.YEAR = '{yArr[y]}' and OT.SEMESTER = '{sArr[j]}'";

                        List<Dictionary<string, string>> semResult = _commonDao.SelectList(query);
                        transaction.Commit();

                        string queryX = "SELECT COUNT(*) AS CNT FROM OP_TAKES WHERE ID = " + $"'{userID_arr[z]}'and YEAR = '{yArr[y]}' and SEMESTER = {sArr[j]} ";

                        int sum = 0;
                        double sum_grd = 0;
                        string tmp = "";
                        int count = Convert.ToInt32(_commonDao.SelectOne(queryX)["CNT"]);
                        List<string> credList = new List<string>();
                        List<string> grdList = new List<string>();

                        //딕셔너리 안 매핑된 밸류 꺼내기
                        for (int i = 0; i < semResult.Count; i++) {
                            var tmpresult = new Dictionary<string, string>()
                        {
                              {"CREDITS",semResult[i]["CREDITS"]},
                              {"GRADE",semResult[i]["GRADE"]}
                        };
                            if (tmpresult.TryGetValue("CREDITS", out string tmpCredit)) {
                                tmp = tmpCredit;
                                if (tmp.Equals("")) continue;
                                credList.Add(tmp);
                            }
                            if (tmpresult.TryGetValue("GRADE", out string tmpGrd)) {
                                tmp = tmpGrd;
                                if (tmp.Equals("")) continue;
                                grdList.Add(tmp);
                            }
                        }
                        //총 신청학점 
                        //신청학점
                        for (int i = 0; i < credList.Count; i++) {
                            int tmpCredit = Convert.ToInt32(credList[i]);
                            sum += tmpCredit;
                        }
                        // 학점 구하기
                        for (int i = 0; i < grdList.Count; i++) {
                            if (grdList[i] == "A+") {
                                grdList[i] = "4.5";
                                double cvtgrd = Convert.ToDouble(grdList[i]);
                                double cvtCredit = Convert.ToDouble(credList[i]);
                                double totalscore = cvtgrd * cvtCredit;
                                sum_grd += totalscore;
                            }
                            if (grdList[i] == "A") {
                                grdList[i] = "4.0";
                                double cvtgrd = Convert.ToDouble(grdList[i]);
                                double cvtCredit = Convert.ToDouble(credList[i]);
                                double totalscore = cvtgrd * cvtCredit;
                                sum_grd += totalscore;
                            }
                            if (grdList[i] == "B+") {
                                grdList[i] = "3.5";
                                double cvtgrd = Convert.ToDouble(grdList[i]);
                                double cvtCredit = Convert.ToDouble(credList[i]);
                                double totalscore = cvtgrd * cvtCredit;
                                sum_grd += totalscore;
                            }
                            if (grdList[i] == "B") {
                                grdList[i] = "3.0";
                                double cvtgrd = Convert.ToDouble(grdList[i]);
                                double cvtCredit = Convert.ToDouble(credList[i]);
                                double totalscore = cvtgrd * cvtCredit;
                                sum_grd += totalscore;
                            }
                            if (grdList[i] == "C+") {
                                grdList[i] = "2.5";
                                double cvtgrd = Convert.ToDouble(grdList[i]);
                                double cvtCredit = Convert.ToDouble(credList[i]);
                                double totalscore = cvtgrd * cvtCredit;
                                sum_grd += totalscore;
                            }
                            if (grdList[i] == "C") {
                                grdList[i] = "2.0";
                                double cvtgrd = Convert.ToDouble(grdList[i]);
                                double cvtCredit = Convert.ToDouble(credList[i]);
                                double totalscore = cvtgrd * cvtCredit;
                                sum_grd += totalscore;
                            }
                            if (grdList[i] == "D+") {
                                grdList[i] = "1.5";
                                double cvtgrd = Convert.ToDouble(grdList[i]);
                                double cvtCredit = Convert.ToDouble(credList[i]);
                                double totalscore = cvtgrd * cvtCredit;
                                sum_grd += totalscore;
                            }
                            if (grdList[i] == "D") {
                                grdList[i] = "1.0";
                                double cvtgrd = Convert.ToDouble(grdList[i]);
                                double cvtCredit = Convert.ToDouble(credList[i]);
                                double totalscore = cvtgrd * cvtCredit;
                                sum_grd += totalscore;
                            }
                            if (grdList[i] == "F") {
                                grdList[i] = "0.0";
                                double cvtgrd = Convert.ToDouble(grdList[i]);
                                double cvtCredit = Convert.ToDouble(credList[i]);
                                double totalscore = cvtgrd * cvtCredit;
                                sum_grd += totalscore;
                            }
                            if (grdList[i] == "-") {
                                nograde = 1;
                            }

                            double grd = 0.00;
                            grd = sum_grd / sum;
                            sumTostr = sum.ToString();
                            allgrd = sum_grd.ToString();
                            grdTostr = grd.ToString("N2");

                            double t1 = 0.0; //임시로 평점평균 저장

                            if (grd >= 3.8) {
                                t1 = ((grd - 1.0) * (40.0 / 3.5)) + 60.0;
                                t2 = " ";
                            }
                            if (1.5 < grd || grd < 3.8) {
                                t1 = ((grd - 1) * 10.0) + 64.0;
                                t2 = " ";
                            }
                            if (grd <= 1.5) {
                                t2 = "Y";
                            }
                            perc = t1.ToString("N2");
                            if (userInfo.user_id == userID_arr[z]) {
                                ViewData["allcred" + y + j] = sumTostr;
                                ViewData["allgrd" + y + j] = allgrd;
                                ViewData["grd" + y + j] = grdTostr;
                                ViewData["per" + y + j] = perc;
                                ViewData["probation" + y + j] = t2;
                            }

                        }

                        string[] arr = new string[sArr.Count];
                        arr[j] = grdTostr;

                        using var transaction2 = _context.Database.BeginTransaction();
                        string query2 = "INSERT INTO OP_TEMP VALUES" + $"('{userID_arr[z]}'," +
                            $"'{yArr[y]}'," +
                            $"'{sArr[j]}'," +
                            $"'{sumTostr}'," +
                            $"'{allgrd}'," +
                            $"'{grdTostr}'," +
                            $"'{perc}'," +
                            $"'{count_students}'," +
                            $"'{t2}')";
                        _commonDao.Insert(query2);
                        transaction2.Commit();
                        t2 = " ";
                        using var transaction4 = _context.Database.BeginTransaction();
                        string queryDeleteRow = "DELETE FROM OP_TEMP WHERE GRADE is null";
                        _commonDao.Delete(queryDeleteRow);
                        transaction4.Commit();
                    }
                    //00학기의 성적만 들어옴.
                    string queryRank = "SELECT ROWNUM, A.* FROM (SELECT DISTINCT * FROM OP_TEMP ORDER BY GRADE DESC) A";
                    rank_set = _commonDao.SelectList(queryRank);
                    string[] id_arr = new string[rank_set.Count];
                    string[] grade_arr = new string[rank_set.Count];
                    for (int i = 0; i < rank_set.Count; i++) {
                        string[] rank_arr = new string[rank_set.Count];

                        var Rankresult = new Dictionary<string, string>()
                        {
                              {"ROWNUM",rank_set[i]["ROWNUM"]},
                              {"ID",rank_set[i]["ID"]},
                            {"GRADE",rank_set[i]["GRADE"]}
                        };
                        if (Rankresult.TryGetValue("ID", out string tmpID)) {
                            string tmp1 = tmpID;
                            id_arr[i] = tmp1;
                        }

                        if (Rankresult.TryGetValue("ROWNUM", out string tmprank)) {
                            string tmp1 = tmprank;
                            rank_arr[i] = tmp1;
                        }
                        if (Rankresult.TryGetValue("GRADE", out string tmpgrade)) {
                            string tmp1 = tmpgrade;
                            grade_arr[i] = tmp1;
                        }

                    }
                    for (int i = 0; i < rank_set.Count; ++i) {
                        var ranktemp = new Dictionary<string, string>()
                              {
                                  {"ROWNUM",rank_set[i]["ROWNUM"]},
                                  {"ID",rank_set[i]["ID"]},
                                  {"YEAR",rank_set[i]["YEAR"]},
                                  {"SEMESTER",rank_set[i]["SEMESTER"]},
                                  {"CREDIT",rank_set[i]["CREDIT"]},
                                  {"TOTALSCORE",rank_set[i]["TOTALSCORE"]},
                                  {"GRADE",rank_set[i]["GRADE"]},
                                  {"PERCENT",rank_set[i]["PERCENT"]},
                                  {"TS",rank_set[i]["TS"]},
                                  {"PROBATION",rank_set[i]["PROBATION"]}
                              };
                        if (userInfo.user_id == id_arr[i])
                            if (nograde != 1)
                                ranklist.Add(Json(ranktemp));
                    }
                    using var transaction3 = _context.Database.BeginTransaction();
                    string query3 = "DELETE FROM OP_TEMP";
                    _commonDao.Delete(query3);
                    transaction3.Commit();
                    grdTostr = "";
                }

                //op_temp 비우기
                using var transaction5 = _context.Database.BeginTransaction();
                string query5 = "DELETE FROM OP_TEMP";
                _commonDao.Delete(query5);
                transaction5.Commit();
                grdTostr = "";
            }//for(s)
            return ranklist;
        }//for(y)
    }
}
