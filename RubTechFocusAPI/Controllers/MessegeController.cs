using RubTechFocus.BAL;
using RubTechFocus.DAL;
using RubTechFocus.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RubTechFocusAPI.Controllers
{
    public class MessegeController : Controller
    {
        MessegeBAL messegeBAL = new MessegeBAL();

        public JsonResult GetMessege()
        {
            var data = messegeBAL.GetMessege();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddMessege(MessegeDTO messege)
        {
            var httpRequest = HttpContext.Request;

            var MessegeFrom = httpRequest["MessegeFrom"];
            var Name = httpRequest["Name"];
            var Position = httpRequest["Position"];
            var Paragraph = httpRequest["Paragraph"];

            var filePath = "";

            //if (string.IsNullOrEmpty(ImagePath))
            //{
            //    return Json(new { Code = 400, Message = "Event Title is required" }, JsonRequestBehavior.AllowGet);
            //}



            if (httpRequest.Files.Count > 0)
            {
                var file = httpRequest.Files[0];

                if (file != null && file.ContentLength > 0) // Ensure valid file
                {
                    var uniquename = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var relativePath = Path.Combine("Content/Uploads", uniquename); // Relative path
                    var fullPath = Path.Combine(Server.MapPath("~/" + relativePath));

                    // Ensure the file is saved properly
                    file.InputStream.Position = 0;
                    file.SaveAs(fullPath);

                    filePath = relativePath; // Store only relative path
                }
            }
            else
            {
                return Json(new { Code = 400, Message = "No images uploaded" }, JsonRequestBehavior.AllowGet);
            }

            MessegeDTO messegeDTO = new MessegeDTO()
            {
                MessegeFrom = MessegeFrom,
                Name = Name, Position = Position, Paragraph = Paragraph,
                ImagePath = filePath
            };
            var data = messegeBAL.AddMessege(messegeDTO);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateMessege(MessegeDTO messege)
        {
            try
            {
                var httpRequest = HttpContext.Request;

                var Id = Convert.ToInt32(httpRequest["Id"]);
                var MessegeFrom = httpRequest["MessegeFrom"];
                var Name = httpRequest["Name"];
                var Position = httpRequest["Position"];
                var Paragraph = httpRequest["Paragraph"];

                string filePath = null; // Null initialize kiya hai blank nahi

                // Check if any file is uploaded
                if (httpRequest.Files.Count > 0)
                {
                    var file = httpRequest.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                        var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                        var relativePath = Path.Combine("Content/Uploads", uniqueName);
                        var fullPath = Path.Combine(Server.MapPath("~/" + relativePath));

                        file.InputStream.Position = 0;
                        file.SaveAs(fullPath);

                        filePath = relativePath; // Store new image path
                    }
                }

                MessegeDTO messegeDTO = new MessegeDTO()
                {
                    Id = Id,
                    MessegeFrom = MessegeFrom,
                    Name = Name,
                    Position = Position,
                    Paragraph = Paragraph,
                    ImagePath = filePath // Agar koi naya image nahi hai toh null jayega
                };

                var data = messegeBAL.UpdateMessege(messegeDTO);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult DeleteMessege(int id)
        {
            messegeBAL.DeleteMessege(id);
            return Json(new { Code = 200, Message = "Messege deleted successfully" }, JsonRequestBehavior.AllowGet);
        }
    }
}