using Newtonsoft.Json;
using RubTechFocus.BAL;
using RubTechFocus.DAL;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RubTechFocusAPI.Controllers
{
    public class NewsLetterController : Controller
    {
        NewsLetterBAL objNewsLetterBAL = new NewsLetterBAL();

        string Exception = "";
        string Response = "";
        public JsonResult GetNewsLetter()
        {
            try
            {
                var data = objNewsLetterBAL.GetNewsLetter();
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

        public JsonResult AddNewsLetter(string email)
        {
            try 
            {
                objNewsLetterBAL.AddNewsLetter(email);
                return Json(new { Code = 200, Messege = "News Letter Added Successfully"},JsonRequestBehavior.AllowGet);
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