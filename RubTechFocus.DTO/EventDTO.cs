using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubTechFocus.DTO
{
    public class EventDTO
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Retval { get; set; }
        public string EventTitle { get; set; }
        public string SubTitle { get; set; }
        public string Paragraph { get; set; }
        public List<string> ImagePaths { get; set; }
        public int ID { get; set; }
        public string URL { get; set; }
        public class EventsEntity
        {
            public int ID { get; set; }
            public string EventTitle { get; set; }
            public string SubTitle { get; set; }
            public string Paragraph { get; set; }
            public int EventImageId { get; set; }
            public List<string> ImagePaths { get; set; }
            public string Imagepath { get; set; }
        }

        public List<EventDTO.EventsEntity> EventsList { get; set; }

    }
}
