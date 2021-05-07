using AREA1.Data;
using AREA1.Filters;
using AREA1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using Tool;
using System.IO;
using System.Web;
using System.Text;
using System;
using System.Security.Cryptography;

namespace AREA1.Controllers {
    [LoginActionFilter]
    public class MainController : Controller {
        private readonly ILogger<MainController> _logger;
        private readonly AppSoftDbContext _context;
        private readonly CommonDao _commonDao;
        public MainController(ILogger<MainController> logger, AppSoftDbContext context) {
            _logger = logger;
            _context = context;
            _commonDao = new CommonDao(context);
        }
        public IActionResult Main()
        {
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");

            ViewData["name"] = userInfo.name;
            ViewData["user_id"] = userInfo.user_id;

            ViewData["Title"] = HttpContext.Session.GetString("_Key");

            return View("/Views/Main.cshtml");
        }

        /*public IActionResult Upload()
            {
            string fileName = "";
         
            foreach (IFormFile file in Request.Form.Files)
            {
                fileName = file.FileName;
                string[] result = fileName.Split(new string[] { "." }, System.StringSplitOptions.None);
                string sSourceData = fileName + DateTime.Now;
                byte[] tmpSource;
                byte[] tmpHash;
                tmpSource = ASCIIEncoding.ASCII.GetBytes(sSourceData);
                tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
                string str = Encoding.Default.GetString(tmpHash);
                return Content(str);

            }


            return RedirectToAction("Main", new { fileName = fileName });
        }*/


        public void Uploads()
        {
            string fileName = "";
            string fileEXTSN = "";
            DateTime timeNow = DateTime.Now;


            foreach (IFormFile file in Request.Form.Files)
            {
                fileName = file.FileName;
                string[] result = fileName.Split(new string[] { "." }, System.StringSplitOptions.None);
                string sSourceData = fileName + DateTime.Now;
                byte[] tmpSource;
                byte[] tmpHash;
                tmpSource = UnicodeEncoding.Unicode.GetBytes(sSourceData);
                tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);

                string fileHash = BitConverter.ToString(tmpHash);
                fileEXTSN = result[1];
                string fileNameSplit = result[0];
                string FileTimeNow = timeNow.ToString("yyyyMMddHHmmss");

                Dictionary<string, string> files = new Dictionary<string, string>();
                files.Add("file_id", fileHash);
                files.Add("file_extsn", fileEXTSN);
                files.Add("upload_time", FileTimeNow);
                files.Add("file_name", fileNameSplit);



                using var transaction = _context.Database.BeginTransaction();



                /*string query = "INSERT INTO FILES VALUES(" + fileHash + ", "
                                                           +fileEXTSN + ", "
                                                           +FileTimeNow + ", "
                                                           +fileName
                                                           + ")";

                _commonDao.Insert(query);*/

                string query = "INSERT INTO FILES VALUES(@file_id:VARCHAR,"
                                                   + "@file_extsn:VARCHAR,"
                                                   + "@upload_time:VARCHAR,"
                                                   + "@file_name:VARCHAR"
                                                   + ")";
                _commonDao.Insert(query, files);

                transaction.Commit();
              

                /* List<Dictionary<string, string>> resultList = _commonDao.SelectList(query);

                var uploadslist = new List<JsonResult>();
                for (int i = 0; i < resultList.Count; ++i)
                {
                    var upload_dic = new Dictionary<string, string>()
                {
                    {"FILE_ID",resultList[i]["FILE_ID"]},
                    {"FILE_EXTSN",resultList[i]["FILE_EXTSN"]},
                    {"UPLOADS_TIME",resultList[i]["UPLOADS_TIME"]},
                    {"FILE_NAME",resultList[i]["FILE_NAME"]},
                };
                    uploadslist.Add(Json(upload_dic));
                }*/

               if (file.Length > 0)    
                {
                    string filePath = Path.Combine(@"C:\filestream\uploads", FileTimeNow);

                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);

                        fileStream.Flush();
                    }
                }
            }

            Response.WriteAsync("<script language=\"javascript\">alert('" + fileName + "이 저장되었습니다!');</script>");
            Response.WriteAsync("<script language=\"javascript\">window.location=\"Main\"</script>");

            return View("/Views/Main.cshtml");
        }

    }
}
