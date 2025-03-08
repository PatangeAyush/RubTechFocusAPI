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
    public class RegisterController : Controller
    {

        RegisterBAL registerBAL = new RegisterBAL();

        string response = "";
        string Exception = "";
        string Request1 = "";
        string Response = "";
        

        public JsonResult GetRegisteredUsers()
        {
            try 
            {
                var result = registerBAL.GetRegisteredUsers();
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
                res.RequestResponseLogs("Method Name: GetRegisteredUsers", "Class Name: RegisterBAL", Request1, Response, Exception);
            }
        }

        public JsonResult AddUser(RegisterDTO.RegisterEntity add)
        {
            try 
            {
               var result = registerBAL.AddUser(add);
                response = JsonConvert.SerializeObject(result);
                return Json(response);
            }
            catch (Exception ex) 
            {
                Exception = ex.ToString();
                throw;
            }
            finally 
            {
                RequestResponseLog res = new RequestResponseLog();
                res.RequestResponseLogs("Method Name: AddUser", "Class Name: RegisterBAL", Request1, Response, Exception);
            }
        }

        public JsonResult UpdateUser(RegisterDTO.RegisterEntity update)
        {
            try
            {
                registerBAL.UpdateUser(update);
                return Json(new { success = true, messege = "User Updated Successfully."}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) 
            {
                Exception = ex.ToString(); 
                throw;
            }
            finally 
            {
                RequestResponseLog res = new RequestResponseLog();
                res.RequestResponseLogs("Method Name: UpdateUser", "Class Name: RegisterBAL", Request1, Response, Exception);
            }
        }

        public JsonResult DeleteUser(int id)
        {
            try 
            {
                registerBAL.DeleteRegistredUser(id);
                return Json(new { success = true, message = "User deleted successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) 
            {
                Exception = ex.ToString() ;
                throw;
            }
            finally 
            {
                RequestResponseLog res = new RequestResponseLog();
                res.RequestResponseLogs("Method Name: GetRegisteredUsers", "Class Name: RegisterBAL", Request1, Response, Exception);
            }
        }

    }
}
