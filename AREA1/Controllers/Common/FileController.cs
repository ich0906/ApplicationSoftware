using AREA1.Data;
using AREA1.Filters;
using AREA1.Models;
using AREA1.Tool;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Tool;

namespace AREA1.Controllers.Common {
    public class FileController : Controller {
        private readonly FileMngTool _fileMngTool;
        private readonly AppSoftDbContext _context;
        private readonly CommonDao _commonDao;

        public FileController(AppSoftDbContext context) {
            _context = context;
            _fileMngTool = new FileMngTool(context);
            _commonDao = new CommonDao(context);
        }

        public string UserUploadFile() {
            string docId = _fileMngTool.Uploads(Request.Form);
            return docId;
        }

        public ActionResult DownloadDocument() {
            string sourcePath = System.IO.Directory.GetCurrentDirectory() + @"\wwwroot\upload";

            string query = "SELECT FILE_ID, FILE_EXTSN, FILE_NAME FROM OP_FILE WHERE FILE_ID = '" + Request.Form["file_id"] + "'";
            var fileData = _commonDao.SelectOne(query);

            string filePath = System.IO.Path.Combine(sourcePath,fileData["FILE_ID"]);
            string fileName = fileData["FILE_NAME"]+"."+fileData["FILE_EXTSN"];

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(fileBytes, "application/force-download", fileName);
        }
    }
}
