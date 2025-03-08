using RubTechFocus.BAL;
using RubTechFocus.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RubTechFocusAPI.Controllers
{
    public class BannerController : Controller
    {
        BannerBAL objBannerBAL = new BannerBAL();

        public JsonResult GetBanner()
        {
            var data = objBannerBAL.GetBanner();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddBanner()
        {
            try
            {
                var httpRequest = HttpContext.Request;
                string filePath = "";

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

                        filePath = relativePath; // Save relative path
                    }
                }
                else
                {
                    return Json(new { Code = 400, Message = "No images uploaded" }, JsonRequestBehavior.AllowGet);
                }

                BannerDTO bannerDTO = new BannerDTO() { ImagePath = filePath };
                var data = objBannerBAL.AddBanner(bannerDTO);

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UpdateBanner()
        {
            try
            {
                var httpRequest = HttpContext.Request;

                int id = Convert.ToInt32(httpRequest["Id"]);
                string filePath = null;

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

                BannerDTO bannerDTO = new BannerDTO() { Id = id, ImagePath = filePath };
                objBannerBAL.UpdateBanner(bannerDTO);

                return Json(new { Code = 200, Message = "Banner updated successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteBanner(int id)
        {
            try
            {
                objBannerBAL.DeleteBanner(id);
                return Json(new { Code = 200, Message = "Banner deleted successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}