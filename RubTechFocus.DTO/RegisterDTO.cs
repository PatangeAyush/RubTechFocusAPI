using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubTechFocus.DTO
{
    public class RegisterDTO
    {
        public int Code { get; set; }

        public string Message { get; set; }
        public string Retval { get; set; }

        public class RegisterEntity
        {
            public string action { get; set; }
            public int Id { get; set; }
            public string name { get; set; }
            public string company { get; set; }
            public string email { get; set; }
            public string phoneNumber { get; set; }
            public string magazine { get; set; }
            public string duration { get; set; }
            public string Address { get; set; }
            public string City_State { get; set; }
            public string Zip_Postal { get; set; }
            public string Country { get; set; }
            public string Payment_Info { get; set; }
            public string Promotional_Preference { get; set; }
            public string IRMRA_Membership { get; set; }
            public string User_Registration_Date { get; set; }
            public string Updated_Date { get; set; }
        }
        public List<RegisterDTO.RegisterEntity> RegisterList { get; set; }
    }
}
