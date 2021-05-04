using AREA1.Data;
using AREA1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Tool;

namespace AREA1.Controllers {
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

            ViewData["name"] = userInfo.name;
            ViewData["user_id"] = userInfo.user_id;

            ViewData["Title"] = HttpContext.Session.GetString("_Key");

            return View("/Views/Main.cshtml");
        }

        public void Upload() {
            string fileName = "";

            foreach (IFormFile file in Request.Form.Files) {
                fileName = file.FileName;
                
                if(file.Length > 0) {
                    string filePath = Path.Combine(@"C:\Users\kih03\source\repos\AREA1\ApplicationSoftware\AREA1\Upload\", file.FileName);

                    using (Stream fileStream = new FileStream(filePath, FileMode.Create)) {
                        file.CopyTo(fileStream);
                        
                        fileStream.Flush();
                }
                }
            }

            Response.WriteAsync("<script language=\"javascript\">alert('" + fileName + "이 저장되었습니다!');</script>");
            Response.WriteAsync("<script language=\"javascript\">window.location=\"Main\"</script>");

        }

    }
}
