using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubTechFocus.DTO
{
    public class GalleryReportDTO
    {
        public int Code { get; set; }
        public string Messege { get; set; }
        public string Retval { get; set; }
        public List<string> ImagePaths { get; set; }
        public int ID { get; set; }
        public string ReportName { get; set; }
        public int GalleryReportImageID { get; set; }
        //public int ID { get; set; }

        public class GalleryReportEntity
        {
            public int ID { get; set; }
            public string ReportName { get; set; }
            public string ImagePath { get; set; }

        }

        public List<GalleryReportDTO.GalleryReportEntity> GalleryReportList { get; set; }
    }
}
