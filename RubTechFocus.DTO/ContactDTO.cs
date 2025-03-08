using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubTechFocus.DTO
{
    public class ContactDTO
    {
        public int Code { get; set; }
        public string Messege { get; set; }
        public int Id { get; set; }
        public string Contact { get; set; }
        public string Alternative_Contact { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime? Created_Date { get; set; }
        public DateTime? Updated_Date { get; set; }
    }
}
