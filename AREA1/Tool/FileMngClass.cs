using AREA1.Data;
using AREA1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using System.Net;
using Microsoft.AspNetCore.Http;
/*
namespace AREA1.Tool
{
    public class FileMngClass
    {
        public void Uploads()
        {
            string fileName = "";

            foreach (IFormFile file in Request.Form.Files)
            {
                fileName = file.FileName;

                if (file.Length > 0)
                {
                    string filePath = Path.Combine(@"C:\filestream\uploads", file.FileName);

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
    }
}
*/
