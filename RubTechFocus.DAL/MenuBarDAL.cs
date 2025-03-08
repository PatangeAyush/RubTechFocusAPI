using RubTechFocus.DTO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubTechFocus.DAL
{
    public class MenuBarDAL:DALBASE
    {
        MenuBarDTO response = new MenuBarDTO();

        public List<MenuBarDTO> GetMenuBar()
        {
            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_MenuBar"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "GetMenuBar");

                    List<MenuBarDTO> menuList = new List<MenuBarDTO>();
                    IDataReader reader = db.ExecuteReader(command);

                    while (reader.Read())
                    {
                        menuList.Add(new MenuBarDTO()
                        {
                            MenuID = Convert.ToInt32(reader["MenuID"]),
                            MenuName = reader["Menu_Name"].ToString(),
                            SubMenuID = reader["SubMenuID"] != DBNull.Value ? Convert.ToInt32(reader["SubMenuID"]) : 0,
                            SubMenu = reader["Sub_Menu"].ToString()
                        });
                    }
                    return menuList;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AddMenuBar(MenuBarDTO menu)
        {
            using (DbCommand command = db.GetStoredProcCommand("SP_MenuBar"))
            {
                db.AddInParameter(command, "@action", DbType.String, "AddMenuBar");
                db.AddInParameter(command, "@name", DbType.String, menu.MenuName);
                db.ExecuteNonQuery(command);
            }
        }

        public void AddSubMenuBar(MenuBarDTO menu)
        {
            using (DbCommand command = db.GetStoredProcCommand("SP_MenuBar"))
            {
                db.AddInParameter(command, "@action", DbType.String, "AddSubMenubar");
                db.AddInParameter(command, "@MenuID", DbType.Int32, menu.MenuID);
                db.AddInParameter(command, "@submenu", DbType.String, menu.SubMenu);
                db.ExecuteNonQuery(command);
            }
        }

        public void UpdateMenuBar(MenuBarDTO menu)
        {
            using (DbCommand command = db.GetStoredProcCommand("SP_MenuBar"))
            {
                db.AddInParameter(command, "@action", DbType.String, "UpdateMenuBar");
                db.AddInParameter(command, "@MenuID", DbType.Int32, menu.MenuID);
                db.AddInParameter(command, "@SubMenuID", DbType.Int32, menu.SubMenuID == 0 ? (object)DBNull.Value : menu.SubMenuID);
                db.AddInParameter(command, "@name", DbType.String, string.IsNullOrEmpty(menu.MenuName) ? (object)DBNull.Value : menu.MenuName);
                db.AddInParameter(command, "@submenu", DbType.String, string.IsNullOrEmpty(menu.SubMenu) ? (object)DBNull.Value : menu.SubMenu);

                db.ExecuteNonQuery(command);
            }
        }


        public void DeleteMenuBar(int? menuID = null, int? subMenuID = null)
        {
            using (DbCommand command = db.GetStoredProcCommand("SP_MenuBar"))
            {
                db.AddInParameter(command, "@action", DbType.String, "DeleteMenuBar");

                if (menuID.HasValue)
                {
                    db.AddInParameter(command, "@MenuID", DbType.Int32, menuID);
                }
                if (subMenuID.HasValue)
                    db.AddInParameter(command, "@SubMenuID", DbType.Int32, subMenuID);
                db.ExecuteNonQuery(command);
            }
        }

    }
}

