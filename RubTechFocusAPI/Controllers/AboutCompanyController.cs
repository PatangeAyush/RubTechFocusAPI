using Newtonsoft.Json;
using RubTechFocus.BAL;
using RubTechFocus.DAL;
using RubTechFocus.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RubTechFocusAPI.Controllers
{
    public class AboutCompanyController : Controller
    {
        AboutCompanyBAL aboutCompanyBAL = new AboutCompanyBAL();

        string Response = "";
        string Exception;

        public JsonResult GetAboutCompany()
        {
            try
            {
                var data = aboutCompanyBAL.GetAboutCompany();
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
                res.RequestResponseLogs("Method Name: GetAboutCompany", "Class Name: AboutCompanyBAL", "Request1", Response, Exception);
            }
        }

        public JsonResult AddCompanyAddress(AboutCompanyDTO aboutCompany)
        {
            try
            {
                var data = aboutCompanyBAL.AddCompanyAddress(aboutCompany);
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
                res.RequestResponseLogs("Method Name: AddCompanyAddress", "Class Name: AboutCompanyBAL", "Request1", Response, Exception);
            }
        }

        public JsonResult UpdateCompanyAddress(AboutCompanyDTO aboutCompany)
        {
            try
            {
                var data = aboutCompanyBAL.UpdateCompanyAddress(aboutCompany);
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
                res.RequestResponseLogs("Method Name: UpdateCompanyAddress", "Class Name: AboutCompanyBAL", "Request1", Response, Exception);
            }
        }
    }
}