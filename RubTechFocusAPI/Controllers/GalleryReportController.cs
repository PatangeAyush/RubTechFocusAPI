using Newtonsoft.Json;
using RubTechFocus.BAL;
using RubTechFocus.DAL;
using RubTechFocus.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace RubTechFocusAPI.Controllers
{
    public class GalleryReportController : Controller
    {
       GalleryReportBAL objgalleryreporttBAL = new GalleryReportBAL();

        string Response;
        string Exception;
        string Request1;

        public JsonResult GetGalleryReport()
        {
            try
            {
                var result = objgalleryreporttBAL.GetReports();
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
                res.RequestResponseLogs("Method Name: GetGallery", "Class Name: GalleryReportBAL", Request1, Response, Exception);
            }
        }

        //public JsonResult AddGalleryReport(GalleryReportDTO add)
        //{
        //    try 
        //    {
        //        var httpRequest = HttpContext.Request;

        //        var reportname = httpRequest["ReportName"];

        //        if (string.IsNullOrEmpty(reportname))
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

        //        GalleryReportDTO eventDTO = new GalleryReportDTO
        //        {
        //            ReportName = reportname,
        //            ImagePaths = imagePaths
        //        };

        //        var result = objgalleryreporttBAL.AddGalleryReport(eventDTO);

        //        Request1 = JsonConvert.SerializeObject(result);


        //        return Json(result);
        //        //var result = objgalleryeventBAL.AddGalleryReport(add);
        //        //Response = JsonConvert.SerializeObject(result);
        //        //return Json(result, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Exception = ex.ToString();
        //        throw;
        //    }
        //    finally
        //    {
        //        RequestResponseLog res = new RequestResponseLog();
        //        res.RequestResponseLogs("Method Name: AddGalleryReport", "Class Name: GalleryEventBAL", Request1, Response, Exception);
        //    }
        //}

        public JsonResult AddGalleryReport(GalleryReportDTO add)
        {
            try
            {
                var httpRequest = HttpContext.Request;
                var reportname = httpRequest["ReportName"];

                if (string.IsNullOrEmpty(reportname))
                {
                    return Json(new { Code = 400, Message = "Report Name is required" }, JsonRequestBehavior.AllowGet);
                }

                List<string> imagePaths = new List<string>();

                if (httpRequest.Files.Count > 0)
                {
                    for (int i = 0; i < httpRequest.Files.Count; i++) // Loop through uploaded files
                    {
                        var file = httpRequest.Files[i];

                        if (file != null && file.ContentLength > 0) // Ensure valid file
                        {
                            var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                            var relativePath = Path.Combine("Content", "Uploads", uniqueName); // ✅ Relative Path
                            var fullPath = Server.MapPath("~/" + relativePath); // Absolute Path for saving only

                            file.SaveAs(fullPath); // Save file
                            imagePaths.Add(relativePath); // ✅ Store relative path (No absolute path)
                        }
                    }
                }
                else
                {
                    return Json(new { Code = 400, Message = "No images uploaded" }, JsonRequestBehavior.AllowGet);
                }

                GalleryReportDTO eventDTO = new GalleryReportDTO
                {
                    ReportName = reportname,
                    ImagePaths = imagePaths
                };

                var result = objgalleryreporttBAL.AddGalleryReport(eventDTO);
                Request1 = JsonConvert.SerializeObject(result);

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
                res.RequestResponseLogs("Method Name: AddGalleryReport", "Class Name: GalleryReportBAL", Request1, Response, Exception);
            }
        }

        public JsonResult AddGalleryReportSingleImage(int id, string imagePath)
        {
            try
            {
                var httpRequest = HttpContext.Request;
                var uniquename = "";
                var relativePath = ""; // ✅ Store relative path only

                if (httpRequest.Files.Count > 0)
                {
                    var file = httpRequest.Files[0];

                    if (file != null && file.ContentLength > 0) // Ensure valid file
                    {
                        uniquename = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                        relativePath = Path.Combine("Content", "Uploads", uniquename); // ✅ Relative Path
                        var fullPath = Server.MapPath("~/" + relativePath); // Absolute Path for saving only

                        file.SaveAs(fullPath); // Save file to server

                        // ✅ Now, store only relative path
                        imagePath = relativePath;
                    }
                }

                objgalleryreporttBAL.AddReportSingleImage(id, imagePath);

                return Json(new { Code = 200, Message = "Gallery Report Image Added Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Exception = ex.ToString();
                throw;
            }
            finally
            {
                RequestResponseLog res = new RequestResponseLog();
                res.RequestResponseLogs("Method Name: AddGalleryReportSingleImage", "Class Name: GalleryReportBAL", Request1, Response, Exception);
            }
        }

        public JsonResult RenameGalleryReport(int Id, string reportName)
        {
            try 
            {
                objgalleryreporttBAL.RenameReport(Id, reportName);
                return Json(new { Code = 400, Messege = "Report Renamed Successfully" });
            }
            catch (Exception ex)
            {
                Exception = ex.ToString();
                throw;
            }
            finally
            {
                RequestResponseLog res = new RequestResponseLog();
                res.RequestResponseLogs("Method Name: RenameGalleryReport", "Class Name: GalleryReportBAL", Request1, Response, Exception);
            }
        }

        public JsonResult DeleteGalleryReport(int Id) 
        {
            try 
            {
                objgalleryreporttBAL.DeleteReport(Id);
                return Json(new { code = 400, Messege = "Report Deleted Successfully" });
            }
            catch (Exception ex) 
            {
                Exception = ex.ToString();
                throw;
            }
            finally 
            {
                RequestResponseLog res = new RequestResponseLog();
                res.RequestResponseLogs("Method Name: DeleteGalleryReport", "Class Name: GalleryReportBAL", Request1, Response, Exception);
            }
        }

        public JsonResult DeleteGalleryReportImage(string imagePath)
        {
            try 
            {
                objgalleryreporttBAL.DeleteReportImage(imagePath);
                return Json(new { code = 400, Messege = "Gallery Report Image Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Exception = ex.ToString();
                throw;
            }
            finally 
            {
                RequestResponseLog res = new RequestResponseLog();
                res.RequestResponseLogs("Method Name: DeleteGalleryReportImage", "Class Name: GalleryReportBAL", Request1, Response, Exception);
            }
        }
    }
}