using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubTechFocus.DTO
{
    public class AboutCompanyDTO
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public int Id { get; set; }
        public string CompanyAddress { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
