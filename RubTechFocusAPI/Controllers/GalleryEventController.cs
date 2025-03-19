using Microsoft.Ajax.Utilities;
using RubTechFocus.BAL;
using RubTechFocus.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using RubTechFocus.DTO;
using System.IO;


namespace RubTechFocusAPI.Controllers
{
    public class GalleryEventController : Controller
    {
        GalleryEventBAL galEventBalObj = new GalleryEventBAL();

        string Response = "";
        string Request1 = "";
        string Exception = "";

        public JsonResult GetGallery()
        {
            try
            {
                var result = galEventBalObj.GetGallery();
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
                res.RequestResponseLogs("Method Name: GetGallery", "Class Name: GalleryEventBAL", Request1, Response, Exception);
            }
        }

        public JsonResult GetGalleryEvents()
        {
            try
            {
                var result = galEventBalObj.GetGalleryEvents();
                Response = JsonConvert.SerializeObject(result);
                return Json(result);
            }
            catch (Exception ex)
            { Exception = ex.ToString(); throw; }
            finally
            {
                RequestResponseLog res = new RequestResponseLog();
                res.RequestResponseLogs("Method Name: GetGalleryEvents", "Class Name: GalleryEventBAL", Request1, Response, Exception);
            }
        }

        public JsonResult AddEvent(GalleryEventDTO add)
        {
            try
            {
                var httpRequest = HttpContext.Request;
                var eventTitle = httpRequest["Title"];

                if (string.IsNullOrEmpty(eventTitle))
                {
                    return Json(new { Code = 400, Message = "Event Title is required" }, JsonRequestBehavior.AllowGet);
                }

                List<string> imagePaths = new List<string>();

                if (httpRequest.Files.Count > 0)
                {
                    for (int i = 0; i < httpRequest.Files.Count; i++)
                    {
                        var file = httpRequest.Files[i];

                        if (file != null && file.ContentLength > 0)
                        {
                            var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                            var relativePath = Path.Combine("Content/Uploads", uniqueName); // ✅ Relative Path
                            var fullPath = Server.MapPath("~/" + relativePath); // Absolute path for saving only

                            file.SaveAs(fullPath); // Save File
                            imagePaths.Add(relativePath); // ✅ Store only relative path (Fixed slashes)
                        }
                    }
                }
                else
                {
                    return Json(new { Code = 400, Message = "No images uploaded" }, JsonRequestBehavior.AllowGet);
                }

                Request1 = JsonConvert.SerializeObject(add);

                GalleryEventDTO eventDTO = new GalleryEventDTO
                {
                    Title = eventTitle,
                    ImagePaths = imagePaths,
                };

                var result = galEventBalObj.AddEvent(eventDTO);
                Request1 = JsonConvert.SerializeObject(result);

                return Json(result);
            }
            catch (Exception ex)
            {
                Exception = ex.ToString();
                throw;
            }
            finally
            {
                RequestResponseLog res = new RequestResponseLog();
                res.RequestResponseLogs("Method Name: AddEvent", "Class Name: GalleryEventBAL", Request1, Response, Exception);
            }
        }

        //public JsonResult AddEvent(GalleryEventDTO add)
        //{
        //    try
        //    {
        //        var httpRequest = HttpContext.Request;
        //        var eventTitle = httpRequest["Title"];

        //        if (string.IsNullOrEmpty(eventTitle))
        //        {
        //            return Json(new { Code = 400, Message = "Event Title is required" }, JsonRequestBehavior.AllowGet);
        //        }

        //        List<string> smallImagePaths = new List<string>();
        //        List<string> bigImagePaths = new List<string>();

        //        // Ensure the DTO contains small and big image counts
        //        if (add.SmallImageCount == 0 || add.BigImageCount == 0)
        //        {
        //            return Json(new { Code = 400, Message = "Please specify small and big image counts in the request" }, JsonRequestBehavior.AllowGet);
        //        }

        //        if (httpRequest.Files.Count > 0)
        //        {
        //            int smallImageCount = 0;
        //            int bigImageCount = 0;

        //            for (int i = 0; i < httpRequest.Files.Count; i++)
        //            {
        //                var file = httpRequest.Files[i];
        //                var uniquename = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        //                var filePath = Path.Combine(Server.MapPath("~/Content/Uploads"), uniquename);

        //                file.SaveAs(filePath);

        //                // Distribute images to small and big image lists based on the count
        //                if (smallImageCount < add.SmallImageCount)
        //                {
        //                    smallImagePaths.Add(filePath);
        //                    smallImageCount++;
        //                }
        //                else if (bigImageCount < add.BigImageCount)
        //                {
        //                    bigImagePaths.Add(filePath);
        //                    bigImageCount++;
        //                }
        //                else
        //                {
        //                    return Json(new { Code = 400, Message = "Uploaded images exceed the specified counts" }, JsonRequestBehavior.AllowGet);
        //                }
        //            }

        //            // Check if both lists have the same count (For 1:1 relationship)
        //            if (smallImagePaths.Count != bigImagePaths.Count)
        //            {
        //                return Json(new { Code = 400, Message = "Mismatch in small and big images count!" }, JsonRequestBehavior.AllowGet);
        //            }
        //        }
        //        else
        //        {
        //            return Json(new { Code = 400, Message = "No images uploaded" }, JsonRequestBehavior.AllowGet);
        //        }

        //        // Serialize the request DTO
        //        Request1 = JsonConvert.SerializeObject(add);

        //        // Create event DTO with the paths
        //        GalleryEventDTO eventDTO = new GalleryEventDTO
        //        {
        //            Title = eventTitle,
        //            SmallImagePaths = smallImagePaths,
        //            BigImagePaths = bigImagePaths
        //        };

        //        // Call business logic to add event
        //        var result = galEventBalObj.AddEvent(eventDTO);

        //        Request1 = JsonConvert.SerializeObject(result);

        //        return Json(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        Exception = ex.ToString();
        //        return Json(new { Code = 500, Message = "Internal Server Error", Error = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //    finally
        //    {
        //        RequestResponseLog res = new RequestResponseLog();
        //        res.RequestResponseLogs("Method Name: AddEvent", "Class Name: GalleryEventBAL", Request1, Response, Exception);
        //    }
        //}

        public JsonResult AddEventImage(int id, string ImagePath)
        {
            try
            {
                var httpRequest = HttpContext.Request;

                // Save Image with GUID-based filename
                if (httpRequest.Files.Count > 0)
                {
                    var imageFile = httpRequest.Files[0]; // Pehla file le raha hai
                    if (imageFile != null && imageFile.ContentLength > 0)
                    {
                        var uniqueImageName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
                        var relativePath = Path.Combine("Content/Uploads", uniqueImageName); // ✅ Relative Path
                        var fullPath = Server.MapPath("~/" + relativePath); // Absolute path for saving only

                        imageFile.SaveAs(fullPath); // Save File
                        ImagePath = relativePath; // ✅ Store only relative path (Fixed slashes)
                    }
                }

                // Call business logic to add the event image
                galEventBalObj.AddEventImage(id, ImagePath);

                return Json(new { Code = 200, Message = "Image Added Successfully To The Event" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string Exception = ex.ToString();
                return Json(new { Code = 500, Message = "Internal Server Error", Error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            finally
            {
                RequestResponseLog res = new RequestResponseLog();
                res.RequestResponseLogs("Method Name: AddEventImage", "Class Name: GalleryEventBAL", Request1, Response, Exception);
            }
        }


        //public JsonResult AddEventImage(int id, string SmallImagePath, string BigImagePath)
        //{
        //    try
        //    {
        //        var httpRequest = HttpContext.Request;

        //        galEventBalObj.AddEventImage(id, SmallImagePath, BigImagePath);
        //        return Json(new { Code = 200, Messege = "Image Added Successfully To The Event" }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Exception = ex.ToString(); throw;
        //    }
        //    finally
        //    {
        //        RequestResponseLog res = new RequestResponseLog();
        //        res.RequestResponseLogs("Method Name: AddEventImage", "Class Name: GalleryEventBAL", Request1, Response, Exception);
        //    }
        //}

        //public JsonResult AddEventImage(int id, string SmallImagePath, string BigImagePath)
        //{
        //    try
        //    {
        //        var httpRequest = HttpContext.Request;

        //        // Save Small Image with GUID-based filename
        //        if (httpRequest.Files.Count > 0)
        //        {
        //            var smallImageFile = httpRequest.Files["SmallImagePath"];
        //            if (smallImageFile != null && smallImageFile.ContentLength > 0)
        //            {
        //                var uniqueSmallImageName = $"{Guid.NewGuid()}{Path.GetExtension(smallImageFile.FileName)}";
        //                var smallImagePath = Path.Combine(Server.MapPath("~/Content/Uploads"), uniqueSmallImageName);
        //                smallImageFile.SaveAs(smallImagePath);
        //                SmallImagePath = smallImagePath; // Update the SmallImagePath variable to new saved path
        //            }
        //        }

        //        // Save Big Image with GUID-based filename
        //        if (httpRequest.Files.Count > 1)
        //        {
        //            var bigImageFile = httpRequest.Files["BigImagepath"];
        //            if (bigImageFile != null && bigImageFile.ContentLength > 0)
        //            {
        //                var uniqueBigImageName = $"{Guid.NewGuid()}{Path.GetExtension(bigImageFile.FileName)}";
        //                var bigImagePath = Path.Combine(Server.MapPath("~/Content/Uploads"), uniqueBigImageName);
        //                bigImageFile.SaveAs(bigImagePath);
        //                BigImagePath = bigImagePath; // Update the BigImagePath variable to new saved path
        //            }
        //        }

        //        // Call business logic to add the event image with the GUID-based file paths
        //        galEventBalObj.AddEventImage(id, SmallImagePath, BigImagePath);

        //        return Json(new { Code = 200, Message = "Image Added Successfully To The Event" }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Exception = ex.ToString();
        //        return Json(new { Code = 500, Message = "Internal Server Error", Error = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //    finally
        //    {
        //        RequestResponseLog res = new RequestResponseLog();
        //        res.RequestResponseLogs("Method Name: AddEventImage", "Class Name: GalleryEventBAL", Request1, Response, Exception);
        //    }
        //}

        public JsonResult RenameEvent(int id, string EventName)
        {
            try
            {
                galEventBalObj.RenameEvent(id, EventName);
                return Json(new { Code = 200, Messege = "Rename Of Event Is Done Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Exception = ex.ToString(); throw;
            }
            finally
            {
                RequestResponseLog res = new RequestResponseLog();
                res.RequestResponseLogs("Method Name: AddEventImage", "Class Name: GalleryEventBAL", Request1, Response, Exception);
            }
        }

        public JsonResult DeleteEvent(int id)
        {
            try
            {
                galEventBalObj.DeleteEvent(id);
                return Json(new { success = true, message = "Event deleted successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                DALBASE res = new DALBASE();
                res.errorLog("Method Name: DeleteEvent", ex.ToString());

                return Json(new { success = false, message = "Error occurred while deleting event." }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteEventImage(int id)
        {
            try
            {
                galEventBalObj.DeleteEventImage(id);
                return Json(new { success = true, Messege = "Event Image Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                DALBASE res = new DALBASE();
                res.errorLog("Method Name : DeleteEventImage", ex.ToString());
                return Json(new { success = false, message = "Error occurred while deleting event Image." }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}