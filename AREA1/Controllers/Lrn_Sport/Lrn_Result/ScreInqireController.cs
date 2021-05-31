using AREA1.Data;
using AREA1.Filters;
using AREA1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Tool;

namespace AREA1.Controllers.Lrn_Result.SCRE_INQIRE
{
    [LoginActionFilter]
    public class ScreInqireController : Controller
    {
        private readonly ILogger<ScreInqireController> _logger;
        private readonly AppSoftDbContext _context;
        private readonly CommonDao _commonDao;
        public ScreInqireController(ILogger<ScreInqireController> logger, AppSoftDbContext context)
        {
            _logger = logger;
            _context = context;
            _commonDao = new CommonDao(context);
        }
        public IActionResult ScreInqire()
        {
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");

            ViewData["user_id"] = userInfo.user_id;
            ViewData["name"] = userInfo.name;
            ViewData["Title"] = HttpContext.Session.GetString("_Key");

            List<string> yArr = new List<string>();
            yArr.Add("2020");
            yArr.Add("2021");
            List<string> sArr = new List<string>();
            sArr.Add("1");
            sArr.Add("2");
            int Count_sem = 2;
            int totalcred = 0;
            string all_sum_grd = "0.0"; // 전학년 평점*학점
            double allsumGrade = 0.0;
            string query3 = "SELECT I_ID FROM OP_ADVISOR " + $"WHERE S_ID = '{userInfo.user_id}'";
            ViewData["advisor"] = Convert.ToString(_commonDao.SelectOne(query3)["I_ID"]);

            for (int y = 0; y < yArr.Count; y++)
            {
                for (int j = 0; j < Count_sem; j++)
                {
                    if (y == 1)
                        Count_sem = 1;
                    int sum = 0;
                    double sum_grd = 0;
                    string tmp = "";
                    List<string> credList = new List<string>();
                    List<string> grdList = new List<string>();
                    List<string> anList = new List<string>();
                    List<string> titleList = new List<string>();
                    List<string> deptList = new List<string>();

                    //표테이블4
                    string query = "SELECT A.ACDMC_NO ,B.TITLE, B.DEPT_NAME, B.CREDITS, OT.GRADE " +
                        "FROM OP_SECTION A JOIN OP_COURSE B " +
                        "ON A.COURSE_ID = B.COURSE_ID " +
                        "RIGHT JOIN OP_TAKES OT " +
                        "on A.SEC_ID = OT.SEC_ID " +
                        "and A.COURSE_ID = OT.COURSE_ID " +
                        "and A.SEMESTER = OT.SEMESTER " +
                       "and A.YEAR = OT.YEAR "
                       + $"WHERE OT.ID = '{userInfo.user_id}' and OT.YEAR = '{yArr[y]}' and OT.SEMESTER = '{sArr[j]}'";
                    List<Dictionary<string, string>> subjList = _commonDao.SelectList(query);

                    //학정번호, 과목명, 학부명, 학점, 평점 리스트에 넣음
                    for (int i = 0; i < subjList.Count; i++)
                    {
                        var tmpsubj = new Dictionary<string, string>()
                        {
                        {"ACDMC_NO",subjList[i]["ACDMC_NO"]},
                        {"TITLE",subjList[i]["TITLE"]},
                        {"DEPT_NAME",subjList[i]["DEPT_NAME"]},
                        {"CREDITS",subjList[i]["CREDITS"]},
                        {"GRADE",subjList[i]["GRADE"]}
                        };
                        if (tmpsubj.TryGetValue("ACDMC_NO", out string tmpAN))
                        {
                            string tmptmp = tmpAN;
                            anList.Add(tmptmp);
                        }
                        if (tmpsubj.TryGetValue("TITLE", out string tmptitle))
                        {
                            string tmptmp = tmptitle;
                            titleList.Add(tmptmp);
                        }
                        if (tmpsubj.TryGetValue("DEPT_NAME", out string tmpdept))
                        {
                            string tmptmp = tmpdept;
                            deptList.Add(tmptmp);
                        }
                        if (tmpsubj.TryGetValue("CREDITS", out string tmpCredit))
                        {
                            tmp = tmpCredit;
                            credList.Add(tmp);
                        }
                        if (tmpsubj.TryGetValue("GRADE", out string tmpGrd))
                        {
                            tmp = tmpGrd;
                            grdList.Add(tmp);
                        }

                        ViewData["credit" + y + j + i] = credList[i];
                        ViewData["an" + y + j + i] = anList[i];
                        ViewData["title" + y + j + i] = titleList[i];
                        ViewData["dept" + y + j + i] = deptList[i];
                        ViewData["grade" + y + j + i] = grdList[i];
                    }

                    //총점계산
                    for (int i = 0; i < credList.Count; i++)
                    {
                        int tmpCredit = Convert.ToInt32(credList[i]);
                        sum += tmpCredit;
                    }
                    //전학년 총점계산
                    totalcred += sum;
                    ViewData["tc"] = totalcred;
                    //character 당 평점할당 
                    int count = 0; // 포문 입장 전 카운트 초기화 (초기선언 포함)
                    int mulcountcredit = 0; //초기화
                    for (int i = 0; i < grdList.Count; i++)
                    {
                        if (grdList[i] == "A+")
                        {
                            grdList[i] = "4.5";
                            double cvtgrd = Convert.ToDouble(grdList[i]);
                            double cvtCredit = Convert.ToDouble(credList[i]);
                            double totalscore = cvtgrd * cvtCredit;
                            sum_grd += totalscore;
                        }
                        if (grdList[i] == "A")
                        {
                            grdList[i] = "4.0";
                            double cvtgrd = Convert.ToDouble(grdList[i]);
                            double cvtCredit = Convert.ToDouble(credList[i]);
                            double totalscore = cvtgrd * cvtCredit;
                            sum_grd += totalscore;
                        }
                        if (grdList[i] == "B+")
                        {
                            grdList[i] = "3.5";
                            double cvtgrd = Convert.ToDouble(grdList[i]);
                            double cvtCredit = Convert.ToDouble(credList[i]);
                            double totalscore = cvtgrd * cvtCredit;
                            sum_grd += totalscore;
                        }
                        if (grdList[i] == "B")
                        {
                            grdList[i] = "3.0";
                            double cvtgrd = Convert.ToDouble(grdList[i]);
                            double cvtCredit = Convert.ToDouble(credList[i]);
                            double totalscore = cvtgrd * cvtCredit;
                            sum_grd += totalscore;
                        }
                        if (grdList[i] == "C+")
                        {
                            grdList[i] = "2.5";
                            double cvtgrd = Convert.ToDouble(grdList[i]);
                            double cvtCredit = Convert.ToDouble(credList[i]);
                            double totalscore = cvtgrd * cvtCredit;
                            sum_grd += totalscore;
                        }
                        if (grdList[i] == "C")
                        {
                            grdList[i] = "2.0";
                            double cvtgrd = Convert.ToDouble(grdList[i]);
                            double cvtCredit = Convert.ToDouble(credList[i]);
                            double totalscore = cvtgrd * cvtCredit;
                            sum_grd += totalscore;
                        }
                        if (grdList[i] == "D+")
                        {
                            grdList[i] = "1.5";
                            double cvtgrd = Convert.ToDouble(grdList[i]);
                            double cvtCredit = Convert.ToDouble(credList[i]);
                            double totalscore = cvtgrd * cvtCredit;
                            sum_grd += totalscore;
                        }
                        if (grdList[i] == "D")
                        {
                            grdList[i] = "1.0";
                            double cvtgrd = Convert.ToDouble(grdList[i]);
                            double cvtCredit = Convert.ToDouble(credList[i]);
                            double totalscore = cvtgrd * cvtCredit;
                            sum_grd += totalscore;
                        }

                        if (grdList[i] == "F")
                        {
                            grdList[i] = "0.0";
                            double cvtgrd = Convert.ToDouble(grdList[i]);
                            double cvtCredit = Convert.ToDouble(credList[i]);
                            double totalscore = cvtgrd * cvtCredit;
                            sum_grd += totalscore;//학기별 총 평점*학점
                            //F가 몇개 들어오는 센 다음에 전체sum(크레딧)에서 뺀다음 총평점에 뺀 크레딧을 나눠준다.
                            count++;
                            int credToInt = Convert.ToInt32(credList[i]);
                            mulcountcredit = count * credToInt;
                        }

                        double mulcountcredit_doub = Convert.ToDouble(mulcountcredit);
                        double grd = sum_grd / sum;
                        double grd_noF = sum_grd / (sum - mulcountcredit_doub);
                        string grd_noF_str = grd_noF.ToString("N2");
                        string sumTostr = sum.ToString();
                        string allgrd = sum_grd.ToString();
                        string grdTostr = grd.ToString("N2");
                        ViewData["allcred" + y + j] = sumTostr; //학기당신청학점
                        ViewData["ag" + y + j] = sum_grd; //학기당총점
                        ViewData["grd" + y + j] = grdTostr; //학기당평점
                        all_sum_grd = allgrd;
                        ViewData["grd_noF" + y + j] = grd_noF_str;
                    }
                    //00번 학기에 총점을 allgrd에 넣었음. allgrd = allgrd00
                    //00번 학기 종료->01학기 시작
                    double temp = Convert.ToDouble(all_sum_grd);
                    allsumGrade += temp;
                    double temp2 = allsumGrade / totalcred;
                    string temp3 = temp2.ToString("N2");
                    double temp4 = allsumGrade / (totalcred - mulcountcredit);
                    string temp5 = temp4.ToString("N2");
                    ViewData["ag"] = temp3;
                    ViewData["agnf"] = temp5;
                }
            }
            //for문 종료

            return View("/Views/LrnResult/ScreInqire.cshtml");
        }

    }
}


