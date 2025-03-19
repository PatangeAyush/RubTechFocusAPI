using Newtonsoft.Json;
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
    public class EventController : Controller
    {

        EventBAL objeventBAL = new EventBAL();

        string Response = "";
        string Request1 = "";
        string Exception = "";

        public JsonResult GetEvents()
        {
            try
            {
                var result = objeventBAL.GetEvents();
                Response = JsonConvert.SerializeObject(result);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Exception = ex.ToString();
                throw;
            }
            finally
            {
                RequestResponseLog res = new RequestResponseLog();
                res.RequestResponseLogs("Method Name: GetEvents", "Class Name: EventBAL", Request1, Response, Exception);
            }
        }

        public JsonResult GetEventsByTitle(string EventTitle)
        {
            try
            {
                var result = objeventBAL.GetEventsByTitle(EventTitle);
                Response = JsonConvert.SerializeObject(result);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Exception = ex.ToString();
                throw;
            }
            finally
            {
                RequestResponseLog res = new RequestResponseLog();
                res.RequestResponseLogs("Method Name: GetEvents", "Class Name: EventBAL", Request1, Response, Exception);
            }
        }
            
        //public JsonResult AddEvent(EventDTO.EventsEntity add)
        //{
        //    try
        //    {
        //        var httpRequest = HttpContext.Request;
        //        var eventTitle = httpRequest["EventTitle"];
        //        var subTitle = httpRequest["SubTitle"];
        //        var paragraph = httpRequest["Paragraph"];


        //        if (string.IsNullOrEmpty(eventTitle))
        //        {
        //            return Json(new { Code = 400, Messege = "Event Title is required" }, JsonRequestBehavior.AllowGet);
        //        }

        //        List<string> imagePaths = new List<string>();

        //        if (httpRequest.Files.Count > 0)
        //        {
        //            foreach (string fileKey in httpRequest.Files)
        //            {
        //                var file = httpRequest.Files[fileKey];
        //                var uniquename = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        //                var filePath = Path.Combine(Server.MapPath("~/Content/Uploads"), uniquename);

        //                file.SaveAs(filePath);

        //                imagePaths.Add(filePath);
        //            }
        //        }
        //        else
        //        {
        //            return Json(new { Code = 400, Message = "No images uploaded" }, JsonRequestBehavior.AllowGet);
        //        }

        //        EventDTO.EventsEntity eventEntity = new EventDTO.EventsEntity
        //        {
        //            EventTitle = eventTitle,
        //            SubTitle = subTitle,
        //            Paragraph = paragraph
        //        };

        //        var result = objeventBAL.AddEvent(eventEntity,imagePaths);
        //        Response = JsonConvert.SerializeObject(result);
        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Exception = ex.ToString();
        //        throw;
        //    }
        //    finally
        //    {
        //        RequestResponseLog res = new RequestResponseLog();
        //        res.RequestResponseLogs("Method Name: AddEvent", "Class Name: EventBAL", Request1, Response, Exception);
        //    }
        //}

        public JsonResult AddEvent(EventDTO.EventsEntity add)
        {
            try
            {
                var httpRequest = HttpContext.Request;
                List<string> imagePaths = new List<string>();

                // Check if request contains files (FormData)
                if (httpRequest.Files.Count > 0)
                {
                    for (int i = 0; i < httpRequest.Files.Count; i++)
                    {
                        var file = httpRequest.Files[i];
                        var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                        var relativePath = Path.Combine("Content/Uploads", uniqueName); // Relative Path
                        var fullPath = Path.Combine(Server.MapPath("~/" + relativePath)); // Absolute Path

                        file.SaveAs(fullPath); // Save File
                        imagePaths.Add(relativePath);  // ✅ Only store relative path
                    }
                }
                // If request is coming from Raw JSON
                else if (add != null && add.ImagePaths != null && add.ImagePaths.Count > 0)
                {
                    foreach (var imagePath in add.ImagePaths)
                    {
                        var extension = Path.GetExtension(imagePath) ?? ".jpg";
                        var uniqueName = $"{Guid.NewGuid()}{extension}";
                        var relativePath = $"Content/Uploads/{uniqueName}";  // ✅ Relative path

                        var absolutePath = Server.MapPath("~/" + relativePath);

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Copy(imagePath, absolutePath, true);
                        }

                        imagePaths.Add(relativePath);  // ✅ Only store relative path
                    }
                }

                // Validation for required fields
                if (add == null || string.IsNullOrEmpty(add.EventTitle))
                {
                    return Json(new { Code = 400, Message = "Event Title is required" }, JsonRequestBehavior.AllowGet);
                }

                // Call BAL method
                var result = objeventBAL.AddEvent(add, imagePaths);
                Response = JsonConvert.SerializeObject(result);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Exception = ex.ToString();
                throw;
            }
            finally
            {
                RequestResponseLog res = new RequestResponseLog();
                res.RequestResponseLogs("Method Name: AddEvent", "Class Name: EventBAL", Request1, Response, Exception);
            }
        }

        public JsonResult UpdateEvent(EventDTO.EventsEntity update)
        {
            try
            {
                var httpRequest = HttpContext.Request;

                var ID = Convert.ToInt16(httpRequest["id"]);
                var EventTitle = httpRequest["EventTitle"];
                var SubTitle = httpRequest["SubTitle"];
                var Paragraph = httpRequest["Paragraph"];
                var EventImageId = Convert.ToInt16(httpRequest["EventImageId"]);

                var uniquename = "";
                var relativePath = "";
                if (httpRequest.Files.Count > 0)
                {
                    var file = httpRequest.Files[0];

                    if (file != null && file.ContentLength > 0) // Ensure valid file
                    {
                        uniquename = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                        relativePath = $"~/Content/Uploads/{uniquename}";  // Store relative path
                        var absolutePath = Server.MapPath(relativePath); // Convert to absolute path for saving

                        // Ensure the file is saved properly
                        file.InputStream.Position = 0;
                        file.SaveAs(absolutePath);
                    }
                }

                EventDTO.EventsEntity eventDTO = new EventDTO.EventsEntity
                {
                    ID = ID,
                    EventTitle = EventTitle,
                    SubTitle = SubTitle,
                    Paragraph = Paragraph,
                    EventImageId = EventImageId,
                    Imagepath = relativePath // Storing relative path
                };

                objeventBAL.UpdateEvent(eventDTO);
                return Json(new { Code = 200, Message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Exception = ex.ToString();
                throw;
            }
            finally
            {
                RequestResponseLog res = new RequestResponseLog();
                res.RequestResponseLogs("Method Name: UpdateEvent", "Class Name: EventBAL", Request1, Response, Exception);
            }
        }

        public JsonResult DeleteEvent(int id)
        {
            try
            {
                objeventBAL.DeleteEvent(id);
                return Json(new { Code = 200, Messege = "Event Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Exception = ex.ToString();
                throw;
            }
            finally
            {
                RequestResponseLog res = new RequestResponseLog();
                res.RequestResponseLogs("Method Name: DeleteEvent", "Class Name: EventBAL", Request1, Response, Exception);
            }
        }
    }
}