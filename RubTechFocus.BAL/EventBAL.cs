using RubTechFocus.DAL;
using RubTechFocus.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubTechFocus.BAL
{
    public class EventBAL
    {
        EventDAL eventDAL = new EventDAL();

        public List<EventDTO> GetEvents()
        {
            return eventDAL.GetEvents();
        }
        public List<EventDTO> GetEventsByTitle(string EventTitle)
        {
            return eventDAL.GetEventsByTitle(EventTitle);
        }
        public EventDTO AddEvent(EventDTO.EventsEntity add, List<string> imagePaths)
        {
            return eventDAL.AddEvent(add,imagePaths);
        }

        public void UpdateEvent(EventDTO.EventsEntity update)
        {
            eventDAL.UpdateEvent(update);
        }

        public void DeleteEvent(int id)
        {
            eventDAL.DeleteEvent(id);
        }
    }
}
