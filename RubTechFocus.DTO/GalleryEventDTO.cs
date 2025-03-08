using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubTechFocus.DTO
{
    public class GalleryEventDTO
    {
        public int Code { get; set; }

        public string Message { get; set; }
        public string Retval { get; set; }
        public string Title { get; set; }
        public List<string> ImagePaths { get; set; }

        //public List<string> BigImagePaths { get; set; }
        public int ID { get; set; }

        //public int SmallImageCount { get; set; }
        //public int BigImageCount { get; set; }
        public class GalleryEventEntity
        {
            public string ID { get; set; }
            public string Title { get; set; }
            public string ImagePaths { get; set; }

            //public string BigImagePaths { get; set; }
            public string EventId { get; set; }
            public string EventName { get; set; }
        }
        public List<GalleryEventDTO.GalleryEventEntity> GalleryEventList { get; set; }

    }
}
