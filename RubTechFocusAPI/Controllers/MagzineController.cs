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
    public class MagzineController : Controller
    {
        MagzineBAL objadvertisingBAL = new MagzineBAL();

        string Response = "";
        string Exception;
        public JsonResult GetMagzine()
        {
            try
            {
                var data = objadvertisingBAL.GetMagzine();
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
                res.RequestResponseLogs("Method Name: GetAdvertisement", "Class Name: AdvertisementBAL", "Request1", Response, Exception);
            }
        }

        public JsonResult AddMagzine(AdvertisingDTO add)
        {
            try
            {
                var httpRequest = HttpContext.Request;

                //var ImagePath = httpRequest["ImagePath"];

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

                AdvertisingDTO advertisingDTO = new AdvertisingDTO()
                {
                    ImagePath = filePath
                };

                var data = objadvertisingBAL.AddMagzine(advertisingDTO);
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
                res.RequestResponseLogs("Method Name: AddMagzine", "Class Name: MagzineBAL", "Request1", Response, Exception);
            }
        }

        public JsonResult DeleteMagzine(int id)
        {
            try
            {
                objadvertisingBAL.DeleteMagzine(id);
                return Json(new { Code = 200, Messege = "Magzine Deleted Succsessfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Exception = ex.ToString();
                throw;
            }
            finally
            {
                RequestResponseLog res = new RequestResponseLog();
                res.RequestResponseLogs("Method Name: DeleteMagzine", "Class Name: MagzineBAL", "Request1", Response, Exception);
            }
        }
    }
}