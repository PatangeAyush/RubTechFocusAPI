using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubTechFocus.DTO
{
    public class MenuBarDTO
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public int SubMenuID { get; set; }
        public string SubMenu { get; set; }
        public string MenuURL { get; set; }
        public string SubMenuURL { get; set; }
    }
}
