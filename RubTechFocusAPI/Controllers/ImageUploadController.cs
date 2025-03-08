using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RubTechFocusAPI.Controllers
{
    public class ImageUploadController : Controller
    {
        // GET: ImageUpload
        public JsonResult SaveFile(string FileSavePath, string FileName)
        {
            string retval, fileName, actualFileName;
            string fileExtention = FileName.Substring(FileName.LastIndexOf('.') + 1);
            retval = fileName = actualFileName = string.Empty;
            bool flag = false;
            if (Request.Files.Count != 0)
            {
                var file = Request.Files[0];
                actualFileName = file.FileName;
                fileName = FileName;
                int size = file.ContentLength;
                try
                {
                    string imgName = Server.MapPath("~/Content/Uploads" + FileSavePath + "/" + fileName);
                    file.SaveAs(imgName);
                    retval = FileName;
                    flag = true;
                }
                catch (Exception ex)
                {
                    retval = "false";
                    throw ex;
                }
            }
            // chk if file extention is png,jpg,jpeg image
            else if (fileExtention == "png" || fileExtention == "Png" || fileExtention == "PNG" || fileExtention == "jpg" || fileExtention == "Jpg" || fileExtention == "JPG" || fileExtention == "jpeg" || fileExtention == "Jpeg" || fileExtention == "JPEG" || fileExtention == "pdf" || fileExtention == "Pdf" || fileExtention == "PDF" || fileExtention == "mp4" || fileExtention == "gif" || fileExtention == "GIF" || fileExtention == "Gif")
            {
                retval = fileName;
            }
            else //for RESUME
            {
                retval = "";
            }
            return Json(retval);
        }
    }
}
