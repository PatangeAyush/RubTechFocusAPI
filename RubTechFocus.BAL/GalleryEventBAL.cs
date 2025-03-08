using RubTechFocus.DAL;
using RubTechFocus.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubTechFocus.BAL
{
    public class GalleryEventBAL
    {
        GalleryEventDAL galEventDalobj = new GalleryEventDAL();
        //GalleryEventDTO galEventDTO = new GalleryEventDTO();
        public List<GalleryEventDTO> GetGallery()
        {
            return galEventDalobj.GetGallery(); 
        }

        public GalleryEventDTO GetGalleryEvents()
        {
            return galEventDalobj.GetGalleryEvents();
        }

        public GalleryEventDTO AddEvent(GalleryEventDTO add)
        {
            return galEventDalobj.AddEvent(add);
        }

        public void AddEventImage(int id, string ImagePath)
        {
            galEventDalobj.AddEventImage(id, ImagePath);
        }

        public void RenameEvent(int id, string EventName)
        {
            galEventDalobj.RenameEvent(id, EventName);
        }

        public void DeleteEvent(int id)
        {
            galEventDalobj.DeleteEvent(id);
        }

        public void DeleteEventImage(int id)
        {
            galEventDalobj.DeleteEventImage(id);
        }
    }
}
