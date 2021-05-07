using AREA1.Data;
using AREA1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using Tool;
using System;
using System.IO;
using System.Web;


/*namespace FileUpload.Controllers
{
    // Home으로 시작하는 Controller	
    public class HomeController : Controller
    {
        // 요청 URL - Home/Index	
        public ActionResult Index()
        {
            // /View/Home/Index.cshtml를 렌더링한다.	
            return View();
        }
        // 요청 URL - Home/FileUpload	
        // 요청 Parameter에 name이 file인 요소를 받는다.	
        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            try
            {
                // file에 업로드하고 그 파일 사이즈가 0byte 이상이면	
                if (file != null && file.ContentLength > 0)
                {
                    // file이름 취득	
                    string filename = Path.GetFileName(file.FileName);
                    // 저장할 경로 설정	
                    string savepath = Path.Combine(@"c:\filestream\uploads\", filename);
                    // 파일 저장	
                    file.SaveAs(savepath);
                    // 메시지 설정	
                    ViewBag.Message = "File Uploaded Successfully.";
                }
                else
                {
                    // 메시지 설정	
                    ViewBag.Message = "File upload failed";
                }
            }
            catch
            {
                // 메시지 설정	
                ViewBag.Message = "File upload failed";
            }
            // /View/Home/Index.cshtml를 렌더링한다.	
            return View("Index");
        }
    }
}*/
