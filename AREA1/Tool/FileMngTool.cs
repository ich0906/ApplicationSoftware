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

namespace Tool {
    public class FileMngTool {
        private readonly AppSoftDbContext _context;
        private readonly CommonDao _commonDao;

        public FileMngTool(AppSoftDbContext context) {
            _context = context;
            _commonDao = new CommonDao(context);
        }


        public string Uploads(IFormCollection param) {
            string fileName = "";
            string fileEXTSN = "";
            string docId = getRandomString();
            DateTime timeNow = DateTime.Now;


            foreach (IFormFile file in param.Files) {
                fileName = file.FileName;
                string[] result = fileName.Split(new string[] { "." }, System.StringSplitOptions.None);
                //파일명 해쉬
                string sSourceData = fileName + DateTime.Now;
                byte[] tmpSource;
                byte[] tmpHash;
                tmpSource = UnicodeEncoding.Unicode.GetBytes(sSourceData);
                tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);

                string fileHash = BitConverter.ToString(tmpHash);//해쉬
                fileEXTSN = result[1];//확장자
                string fileNameSplit = result[0];//확장자뗀이름
                string FileTimeNow = timeNow.ToString("yyyy-MM-dd HH:mm:ss");//업로드시각

                //속성마다 값 넣어주기
                Dictionary<string, string> files = new Dictionary<string, string>();
                files.Add("file_id", fileHash);
                files.Add("file_name", fileNameSplit);
                files.Add("file_extsn", fileEXTSN);
                files.Add("upload_time", FileTimeNow);
                files.Add("doc_id", docId);

                //INSERT 쿼리
                using var transaction = _context.Database.BeginTransaction();

                string query = "INSERT INTO OP_FILE VALUES(@file_id:VARCHAR,"
                                                   + "@file_name:VARCHAR,"
                                                   + "@file_extsn:VARCHAR,"
                                                   + "@upload_time:VARCHAR,"
                                                   + "@doc_id:VARCHAR"
                                                   + ")";
                _commonDao.Insert(query, files);

                transaction.Commit();

                //폴더에 파일쓰기
                if (file.Length > 0) {
                    string folder = System.IO.Directory.GetCurrentDirectory() + @"\wwwroot\upload";
                    string filePath = Path.Combine(folder, fileHash);

                    using (Stream fileStream = new FileStream(filePath, FileMode.Create)) {
                        file.CopyTo(fileStream);

                        fileStream.Flush();
                    }
                }
            }
            return docId;
            //Response.WriteAsync("<script language=\"javascript\">alert('" + fileName + " is uploaded!');</script>");
            //Response.WriteAsync("<script language=\"javascript\">window.location=\"Main\"</script>");

        }
        public void Downloads(IFormCollection param) {
            var fileData = new Dictionary<string, string>();
            //HTML input값과 동일한 해쉬값을 가진 튜플에서 파일네임과 확장자 가져오기
            using var transaction = _context.Database.BeginTransaction();

            string query = "SELECT FILE_ID, FILE_EXTSN, FILE_NAME FROM OP_FILE WHERE FILE_ID = @file_id:VARCHAR";

            fileData = _commonDao.SelectOne(query, param);

            transaction.Commit();

            //확장자 붙여서 원래 이름 만들어주기
            string fileID = "";
            string fileName = "";
            string tmpName = "";
            string tmpEXTSN = "";

            foreach (KeyValuePair<string, string> keyValues in fileData) {
                if (fileData.TryGetValue("FILE_ID", out string id))
                    fileID = id;
                if (fileData.TryGetValue("FILE_EXTSN", out string extsn))
                    tmpEXTSN = extsn;
                if (fileData.TryGetValue("FILE_NAME", out string name))
                    tmpName = name;
            }
            fileName = tmpName + "." + tmpEXTSN;

            //Response.WriteAsync("<script language=\"javascript\">alert('" + fileName + " is downloaded!');</script>");
            //Response.WriteAsync("<script language=\"javascript\">window.location=\"Main\"</script>");

            //파일 다운로드(카피)
            string sourcePath = @"c:\filestream\uploads";
            string targetPath = @"c:\filestream\downloads";

            string sourceFile = System.IO.Path.Combine(sourcePath, fileID);
            string destFile = System.IO.Path.Combine(targetPath, fileName);

            System.IO.Directory.CreateDirectory(targetPath);
            System.IO.File.Copy(sourceFile, destFile, true);

        }

        public string getRandomString() {
            DateTime dt = DateTime.Now;
            return dt.ToString("MMddhhmmss");
        }
    }
}