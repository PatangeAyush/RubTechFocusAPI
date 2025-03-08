using Newtonsoft.Json;
using RubTechFocus.BAL;
using RubTechFocus.DAL;
using RubTechFocus.DTO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RubTechFocusAPI.Controllers
{
    public class MentorController : Controller
    {
        MentorBAL objmentorBAL = new MentorBAL();

        string Exception;
        string Response;

        public JsonResult GetMentor()
        {
            try
            {
                var data = objmentorBAL.GetMentor();
                Response = JsonConvert.SerializeObject(data);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Exception = ex.Message;
                throw;
            }
            finally
            {
                RequestResponseLog res = new RequestResponseLog();
                res.RequestResponseLogs("GetMentor", "MentorDAL", "Request", Response, Exception);
            }
        }

        public JsonResult AddMentor(MentorDTO add)
        {
            try
            {
                var httpRequest = HttpContext.Request;
                var name = httpRequest["Name"];
                var position = httpRequest["Position"];

                var filePath = "";

                if (string.IsNullOrEmpty(name))
                {
                    return Json(new { Code = 400, Message = "Event Title is required" }, JsonRequestBehavior.AllowGet);
                }



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

                MentorDTO mentorDTO = new MentorDTO()
                {
                    Name = name,
                    Position = position,
                    ImagePath = filePath 
                };

                

                var data = objmentorBAL.AddMentor(mentorDTO);
                Response = JsonConvert.SerializeObject(data);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Exception = ex.Message;
                throw;
            }
            finally
            {
                RequestResponseLog res = new RequestResponseLog();
                res.RequestResponseLogs("GetMentor", "MentorDAL", "Request", Response, Exception);
            }
        }

        public JsonResult UpdateMentor(MentorDTO udpate)
        {
            try 
            {
                var httpRequest = HttpContext.Request;
                var Id = Convert.ToInt16(httpRequest["ID"]);
                var name = httpRequest["Name"];
                var position = httpRequest["Position"];

                var filePath = "";

                if (string.IsNullOrEmpty(name))
                {
                    return Json(new { Code = 400, Message = "Event Title is required" }, JsonRequestBehavior.AllowGet);
                }



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

                MentorDTO mentorDTO = new MentorDTO()
                {
                    ID = Id,
                    Name = name,
                    Position = position,
                    ImagePath = filePath
                };

                var data = objmentorBAL.UpdateMentor(mentorDTO);
                Response = JsonConvert.SerializeObject(data);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Exception = ex.Message;
                throw;
            }
            finally
            {
                RequestResponseLog res = new RequestResponseLog();
                res.RequestResponseLogs("UpdateMentor", "MentorDAL", "Request", Response, Exception);
            }
        }

        public JsonResult DeleteMentor(int id)
        {
            try 
            {
                objmentorBAL.DeleteMentor(id);
                return Json(new { Code = 200, Messege = "Event Deleted Successfully" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Exception = ex.Message;
                throw;
            }
            finally
            {
                RequestResponseLog res = new RequestResponseLog();
                res.RequestResponseLogs("GetMentor", "MentorDAL", "Request", Response, Exception);
            }
        }
    }
}