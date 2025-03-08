using RubTechFocus.DAL;
using RubTechFocus.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubTechFocus.BAL
{
    public class MenuBarBAL
    {
        MenuBarDAL menuDAL = new MenuBarDAL();

        public List<MenuBarDTO> GetMenuBar()
        {
            return menuDAL.GetMenuBar();
        }
        public void AddMenuBar(MenuBarDTO menu)
        {
            menuDAL.AddMenuBar(menu);
        }

        public void AddSubMenuBar(MenuBarDTO menu)
        {
            menuDAL.AddSubMenuBar(menu);
        }

        public void UpdateMenuBar(MenuBarDTO menu)
        {
            menuDAL.UpdateMenuBar(menu);
        }

        public void DeleteMenuBar(int? menuID, int? subMenuID = null)
        {
            menuDAL.DeleteMenuBar(menuID, subMenuID);
        }
    }
}
