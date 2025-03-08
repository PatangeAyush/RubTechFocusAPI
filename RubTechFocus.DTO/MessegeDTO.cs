using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubTechFocus.DTO
{
    public class MessegeDTO
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public int Id { get; set; }
        public string MessegeFrom { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Paragraph { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
