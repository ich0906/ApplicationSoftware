using AREA1.Data;
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



namespace AREA1.Controllers
{
    public class FileMngToolController : Controller
    {
        private readonly ILogger<MainController> _logger;
        private readonly AppSoftDbContext _context;
        private readonly CommonDao _commonDao;
        public FileMngToolController(ILogger<MainController> logger, AppSoftDbContext context)
        {
            _logger = logger;
            _context = context;
            _commonDao = new CommonDao(context);
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
                    string filePath = Path.Combine(@"C:\filestream\uploads", fileHash);

                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);

                        fileStream.Flush();
                    }
                }
            }

            Response.WriteAsync("<script language=\"javascript\">alert('" + fileName + "이 저장되었습니다!');</script>");
            Response.WriteAsync("<script language=\"javascript\">window.location=\"Main\"</script>");

        }
        public void Downloads()
        {
            /*Dictionary<string, string> fileNameTemp = new Dictionary<string, string>();
            List<Dictionary<string,string>> fileNames = new List<Dictionary<string,string>>();

            string filePath = Path.GetFullPath(@"uploads");
            if(System.IO.Directory.Exists(filePath))
            {
                System.IO.DirectoryInfo uploadDir = new System.IO.DirectoryInfo(filePath);
                foreach(var item in uploadDir.GetFiles())
        {
                    string itemName = "";
                    itemName = item.Name.ToString();
                    fileNameTemp.Add("file_id", itemName);
                    fileNames.Add(fileNameTemp);
        }
            }*/

            string Path = @"C:\filestream\uploads";
            string fileName = "";

            if (System.IO.Directory.Exists(Path))
            {
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Path);

                foreach (var item in di.GetFiles())
                {
                    fileName = item.Name;

    }            
}
        }
    }
}
   