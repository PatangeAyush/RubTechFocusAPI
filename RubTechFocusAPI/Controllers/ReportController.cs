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
    public class ReportController : Controller
    {

        ReportBAL objreportBAL = new ReportBAL();

        string Exception;
        string Response;
        string Request1;

        public JsonResult GetReport()
        {
            try 
            {
                var data = objreportBAL.GetReport();
                Response = JsonConvert.SerializeObject(data);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Exception = ex.ToString();
                throw;
            }
            finally
            {
                RequestResponseLog res = new RequestResponseLog();
                res.RequestResponseLogs("Method Name: GetReport", "Class Name: GalleryReportBAL", Request1, Response, Exception);
            }
        }
        public JsonResult GetReportByID(int id)
        {
            try
            {
                var data = objreportBAL.GetReportByID(id);
                Response = JsonConvert.SerializeObject(data);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Exception = ex.ToString();
                throw;
            }
            finally
            {
                RequestResponseLog res = new RequestResponseLog();
                res.RequestResponseLogs("Method Name: GetReport", "Class Name: GalleryReportBAL", Request1, Response, Exception);
            }
        }

        public JsonResult AddReport(ReportDTO add)
        {
            try
            {
                var httpRequest = HttpContext.Request;
                var reportname = httpRequest["ReportName"];
                var ReportSubParagraph = httpRequest["ReportSubParagraph"];
                var Report_Description = httpRequest["Report_Description"];

                if (string.IsNullOrEmpty(reportname))
                {
                    return Json(new { Code = 400, Message = "Report Name is required" }, JsonRequestBehavior.AllowGet);
                }

                string imagePath = null;  // Initialize imagePath variable

                if (httpRequest.Files.Count > 0)
                {
                    var file = httpRequest.Files[0]; // Accessing only the first file

                    if (file != null && file.ContentLength > 0) // Ensure the file is valid
                    {
                        var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                        var relativePath = $"Content\\Uploads\\{uniqueName}"; // ✅ Double backslash format
                        var fullPath = Server.MapPath("~/" + relativePath.Replace("\\", "/")); // ✅ Convert to valid server path

                        file.SaveAs(fullPath); // Save file to server
                        imagePath = relativePath; // ✅ Store only relative path (double backslash)
                    }
                }
                else
                {
                    return Json(new { Code = 400, Message = "No image uploaded" }, JsonRequestBehavior.AllowGet);
                }

                // Create DTO for the report
                ReportDTO eventDTO = new ReportDTO
                {
                    ReportName = reportname,
                    ReportSubParagraph = ReportSubParagraph,
                    Report_Description = Report_Description,
                    ReportImage = imagePath  // ✅ Stored as "Content\\Uploads\\filename.jpg"
                };

                var data = objreportBAL.AddReport(eventDTO);

                return Json(new { Code = 200, Message = "Report Added Successfully", Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Exception = ex.ToString();
                throw;
            }
            finally
            {
                RequestResponseLog res = new RequestResponseLog();
                res.RequestResponseLogs("Method Name: AddReport", "Class Name: GalleryReportBAL", Request1, Response, Exception);
            }
        }

        public JsonResult UpdateReport(ReportDTO update)
        {
            try
            {
                var httpRequest = HttpContext.Request;
                int id = Convert.ToInt32(httpRequest["ID"]);
                var reportname = httpRequest["ReportName"];
                var ReportSubParagraph = httpRequest["ReportSubParagraph"];
                var Report_Description = httpRequest["Report_Description"];
                var oldImagePath = httpRequest["OldImage"];
                string newImagePath = oldImagePath; // Default to old image

                if (string.IsNullOrEmpty(reportname))
                {
                    return Json(new { Code = 400, Message = "Report Name is required" }, JsonRequestBehavior.AllowGet);
                }

                if (httpRequest.Files.Count > 0)
                {
                    var file = httpRequest.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                        var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                        var relativePath = $"Content\\Uploads\\{uniqueName}"; // ✅ Double backslash format
                        var fullPath = Server.MapPath("~/" + relativePath.Replace("\\", "/")); // ✅ Convert to valid server path

                        file.SaveAs(fullPath); // Save new image
                        newImagePath = relativePath; // ✅ Store only relative path
                    }
                }

                ReportDTO reportDTO = new ReportDTO
                {
                    ID = id,
                    ReportName = reportname,
                    ReportSubParagraph = ReportSubParagraph,
                    Report_Description = Report_Description,
                    ReportImage = newImagePath // ✅ Stored as "Content\\Uploads\\filename.jpg"
                };

                var data = objreportBAL.UpdateReport(reportDTO);

                return Json(new { Code = 200, Message = "Report Updated Successfully", Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Exception = ex.ToString();
                throw;
            }
            finally
            {
                RequestResponseLog res = new RequestResponseLog();
                res.RequestResponseLogs("Method Name: UpdateReport", "Class Name: GalleryReportBAL", Request1, Response, Exception);
            }
        }

        public JsonResult DeleteReport(int id)
        {
            try 
            {
                objreportBAL.DeleteReport(id);
                return Json(new { Code = 200, Messege = "Report Deleted Successfully" }, JsonRequestBehavior.AllowGet); 
            }
            catch (Exception ex)
            {
                Exception = ex.ToString();
                throw;
            }
            finally
            {
                RequestResponseLog res = new RequestResponseLog();
                res.RequestResponseLogs("Method Name: UpdateReport", "Class Name: GalleryReportBAL", Request1, Response, Exception);
            }
        }

    }
}