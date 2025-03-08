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
    public class MenuBarController : Controller
    {
        MenuBarBAL menuBAL = new MenuBarBAL();

        public JsonResult GetMenuBar()
        {
            var data = menuBAL.GetMenuBar();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddMenuBar(MenuBarDTO menu)
        {
            menuBAL.AddMenuBar(menu);
            return Json(new { Code = 200, Message = "Menu Added Successfully" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddSubMenuBar(MenuBarDTO menu)
        {
            menuBAL.AddSubMenuBar(menu);
            return Json(new { Code = 200, Message = "SubMenu Added Successfully" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateMenuBar(MenuBarDTO menu)
        {
            if (menu.MenuID == 0 && menu.SubMenuID == 0)
            {
                return Json(new { Code = 400, Message = "Invalid Parameters" }, JsonRequestBehavior.AllowGet);
            }

            menuBAL.UpdateMenuBar(menu);
            return Json(new { Code = 200, Message = "Menu Updated Successfully" }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult DeleteMenuBar(int? menuID, int? subMenuID = null)
        {
            menuBAL.DeleteMenuBar(menuID, subMenuID);
            return Json(new { Code = 200, Message = "Menu Deleted Successfully" }, JsonRequestBehavior.AllowGet);
        }
    }
}