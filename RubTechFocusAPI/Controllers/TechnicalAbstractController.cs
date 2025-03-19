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
    public class TechnicalAbstractController : Controller
    {
       TechnicalAbstractBAL objTechnicalAbstractBAL = new TechnicalAbstractBAL();

        string request = string.Empty;
        string response = string.Empty;
        string exception = string.Empty;
        public JsonResult GetTechnicalAbstracts()
        {
            string response = string.Empty;
            string exception = string.Empty;
            try
            {
                var result = objTechnicalAbstractBAL.GetTechnicalAbstracts();
                response = JsonConvert.SerializeObject(result);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                exception = ex.ToString();
                throw;
            }
            finally
            {
                RequestResponseLog res = new RequestResponseLog();
                res.RequestResponseLogs("Method Name: GetTechnicalAbstracts", "Class Name: TechnicalAbstractBAL", string.Empty, response, exception);
            }
        }

        public JsonResult AddTechnicalAbstract()
        {
            try
            {
                var httpRequest = HttpContext.Request;
                var name = httpRequest["Name"];
                var Authorname = httpRequest["Author_Name"];
                var AuthorDescription = httpRequest["Author_Description"];
                var AbstractParagraph = httpRequest["Abstract_Paragraph"];

                if (string.IsNullOrEmpty(name))
                {
                    return Json(new { Code = 400, Message = "Abstract Name is required" }, JsonRequestBehavior.AllowGet);
                }

                List<string> imagePaths = new List<string>();

                if (httpRequest.Files.Count > 0)
                {
                    foreach (string fileKey in httpRequest.Files)
                    {
                        var file = httpRequest.Files[fileKey];

                        if (file != null && file.ContentLength > 0)
                        {
                            var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                            var relativePath = $"Content\\Uploads\\{uniqueName}"; // ✅ Double backslash format
                            var fullPath = Server.MapPath("~/" + relativePath.Replace("\\", "/")); // ✅ Convert to valid server path

                            file.SaveAs(fullPath); // Save file to server
                            imagePaths.Add(relativePath); // ✅ Store only relative path
                        }
                    }
                }
                else
                {
                    return Json(new { Code = 400, Message = "No images uploaded" }, JsonRequestBehavior.AllowGet);
                }

                TechnicalAbstractDTO dto = new TechnicalAbstractDTO
                {
                    Name = name,
                    Absract_ImagePath = imagePaths.Count > 0 ? imagePaths[0] : null, // First image for Abstract
                    Author_ImagePath = imagePaths.Count > 1 ? imagePaths[1] : null,
                    Author_Name = Authorname,
                    Author_Description = AuthorDescription,
                    Abstract_Paragraph = AbstractParagraph,
                };

                var result = objTechnicalAbstractBAL.AddTechnicalAbstract(dto);
                request = JsonConvert.SerializeObject(dto);
                response = JsonConvert.SerializeObject(result);
                return Json(result);
            }
            catch (Exception ex)
            {
                exception = ex.ToString();
                throw;
            }
            finally
            {
                RequestResponseLog res = new RequestResponseLog();
                res.RequestResponseLogs("Method Name: AddTechnicalAbstract", "Class Name: TechnicalAbstractBAL", request, response, exception);
            }
        }



        public JsonResult UpdateTechnicalAbstract(TechnicalAbstractDTO model)
        {
            try
            {
                var httpRequest = HttpContext.Request;

                if (string.IsNullOrEmpty(model.Name))
                {
                    return Json(new { Code = 400, Message = "Abstract Name is required" }, JsonRequestBehavior.AllowGet);
                }

                List<string> imagePaths = new List<string>();

                if (httpRequest.Files.Count > 0)
                {
                    foreach (string fileKey in httpRequest.Files)
                    {
                        var file = httpRequest.Files[fileKey];

                        if (file != null && file.ContentLength > 0)
                        {
                            var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                            var relativePath = $"Content\\Uploads\\{uniqueName}"; // ✅ Double backslash format
                            var fullPath = Server.MapPath("~/" + relativePath.Replace("\\", "/")); // ✅ Convert to valid server path

                            file.SaveAs(fullPath); // Save new image
                            imagePaths.Add(relativePath); // ✅ Store only relative path
                        }
                    }
                }

                // Assign the image paths to the model
                model.Absract_ImagePath = imagePaths.Count > 0 ? imagePaths[0] : model.Absract_ImagePath;  // First image for Abstract
                model.Author_ImagePath = imagePaths.Count > 1 ? imagePaths[1] : model.Author_ImagePath;

                var result = objTechnicalAbstractBAL.UpdateTechnicalAbstract(model);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Message = "Internal Server Error", Error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }



        //public JsonResult UpdateTechnicalAbstract(TechnicalAbstractDTO model)
        //{
        //    try
        //    {
        //        var httpRequest = HttpContext.Request;
        //        var id = Convert.ToInt32(httpRequest["Id"]);
        //        var name = httpRequest["Name"];
        //        var Authorname = httpRequest["Author_Name"];
        //        var AuthorDescription = httpRequest["Author_Description"];
        //        var AbstractParagraph = httpRequest["Abstract_Paragraph"];

        //        if (string.IsNullOrEmpty(name))
        //        {
        //            return Json(new { Code = 400, Message = "Abstract Name is required" }, JsonRequestBehavior.AllowGet);
        //        }

        //        List<string> imagePaths = new List<string>();

        //        if (httpRequest.Files.Count > 0)
        //        {
        //            foreach (string fileKey in httpRequest.Files)
        //            {
        //                var file = httpRequest.Files[fileKey];
        //                var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        //                var filePath = Path.Combine(HttpContext.Server.MapPath("~/Content/Uploads"), uniqueName);
        //                file.SaveAs(filePath);
        //                imagePaths.Add(filePath);
        //            }
        //        }
        //        else
        //        {
        //            return Json(new { Code = 400, Message = "No images uploaded" }, JsonRequestBehavior.AllowGet);
        //        }

        //        TechnicalAbstractDTO dto = new TechnicalAbstractDTO
        //        {
        //            Id = id,
        //            Name = name,
        //            Absract_ImagePath = imagePaths.Count > 0 ? imagePaths[0] : null, // First image for Abstract
        //            Author_ImagePath = imagePaths.Count > 1 ? imagePaths[1] : null,
        //            Author_Name = Authorname,
        //            Author_Description = AuthorDescription,
        //            Abstract_Paragraph = AbstractParagraph,

        //        };

        //        var result = objTechnicalAbstractBAL.UpdateTechnicalAbstract(dto);
        //        response = JsonConvert.SerializeObject(result);
        //        return Json(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        exception = ex.ToString();
        //        throw;
        //    }
        //    finally
        //    {
        //        RequestResponseLog res = new RequestResponseLog();
        //        res.RequestResponseLogs("Method Name: UpdateTechnicalAbstract", "Class Name: TechnicalAbstractBAL", request, response, exception);
        //    }
        //}

        public JsonResult DeleteTechnicalAbstract(int id)
        {
            try 
            {
                var result = objTechnicalAbstractBAL.DeleteTechnicalAbstract(id); 
                response = JsonConvert.SerializeObject(result);
                return Json(result);
            }
            catch (Exception ex)
            {
                exception = ex.ToString();
                throw;
            }
            finally
            {
                RequestResponseLog res = new RequestResponseLog();
                res.RequestResponseLogs("Method Name: DeleteTechnicalAbstract", "Class Name: TechnicalAbstractBAL", request, response, exception);
            }
        }
    }
}