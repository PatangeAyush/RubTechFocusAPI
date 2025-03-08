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
    public class ContactController : Controller
    {       
       ContactBAL objcontactBAL = new ContactBAL();

        string response = "";
        string Exception = "";
        string Request1 = "";
        public JsonResult GetContact()
        {
            try 
            {
                var result = objcontactBAL.GetContacts();
                response = JsonConvert.SerializeObject(result);
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
                res.RequestResponseLogs("Method Name: GetContact", "Class Name: ContactBAL", Request1, response, Exception);
            }
        }

        public JsonResult AddContact(ContactDTO add)
        {
            string requestData = JsonConvert.SerializeObject(add); 
            string responseData = "";
            try 
            {
                //objcontactBAL.AddContact(add);
                //return Json();
                objcontactBAL.AddContact(add);
                responseData = JsonConvert.SerializeObject(new { success = true, message = "Contact added successfully" });
                return Json(new { success = true, message = "Contact added successfully" });
            }
            catch (Exception ex)
            {
                Exception = ex.ToString();
                throw;
            }
            finally 
            {
                RequestResponseLog res = new RequestResponseLog();
                res.RequestResponseLogs("Method Name: AddContact", "Class Name: ContactBAL", Request1, response, Exception);
            }
        }

        public JsonResult UpdateContact(ContactDTO update)
        {
            try
            {
                var result = objcontactBAL.UpdateContact(update);
                response = JsonConvert.SerializeObject(result);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Exception = ex.ToString();
                throw;
            }
            finally
            {
                update.Messege = "Contact Updated Successfully";
                RequestResponseLog res = new RequestResponseLog();
                res.RequestResponseLogs("Method Name: UpdateContact", "Class Name: ContactBAL", Request1, response, Exception);
            }
        }

        public JsonResult DeleteContact(int id)
        {
            string requestData = JsonConvert.SerializeObject(id);
            string responseData = "";
            try 
            {
                objcontactBAL.DeleteContact(id);
                responseData = JsonConvert.SerializeObject(new { success = true, message = "Contact Deleted successfully" });
                return Json(new { success = true, message = "Contact added successfully" });

            }
            catch (Exception ex)
            {
                Exception = ex.ToString();
                throw;
            }
            finally
            {
                RequestResponseLog res = new RequestResponseLog();
                res.RequestResponseLogs("Method Name: AddContact", "Class Name: ContactBAL", Request1, response, Exception);
            }
        }
    }
}