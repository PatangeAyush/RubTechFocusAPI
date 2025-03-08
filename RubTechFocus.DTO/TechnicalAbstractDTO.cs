using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubTechFocus.DTO
{
    public class TechnicalAbstractDTO
    {
        public string Message { get; set; }
        public int Code { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Absract_ImagePath { get; set; }
        public string Author_ImagePath { get; set; }
        public string Author_Name { get; set; }
        public string Author_Description { get; set; }
        public string Abstract_Paragraph { get; set; }
    }
}
