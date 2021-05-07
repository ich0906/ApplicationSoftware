using AREA1.Data;
using AREA1.Filters;
using AREA1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tool;

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
        public IActionResult Main() {
            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(HttpContext.Session, "userInfo");

            if (userInfo == null) {
                return RedirectToAction("/Login", new { alertLogin = 2 });
            }

            ViewData["name"] = userInfo.name;
            ViewData["user_id"] = userInfo.user_id;

            ViewData["Title"] = HttpContext.Session.GetString("_Key");

<<<<<<< HEAD
            return View("/Views/Main.cshtml");
        }


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

                string query = "INSERT INTO FILES VALUES(@file_id:VARCHAR,"
                                                   + "@file_extsn:VARCHAR,"
                                                   + "@upload_time:VARCHAR,"
                                                   + "@file_name:VARCHAR"
                                                   + ")";
                _commonDao.Insert(query, files);

                transaction.Commit();
              

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
=======
>>>>>>> e86fc866fe0a1c108cad686d4adf46865d16babc

        }

        public void Downloads()
        {

        }
    }
}
