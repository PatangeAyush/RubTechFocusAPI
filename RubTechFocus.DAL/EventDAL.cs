using RubTechFocus.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubTechFocus.DAL
{
    public class EventDAL:DALBASE
    {
        string exception = "";

        EventDTO objEventDTO = new EventDTO();

        //public List<EventDTO> GetEvents()
        //{
        //    EventDTO eventDTO = new EventDTO();
        //    try
        //    {
        //        EventList = new List<EventDTO.EventsEntity>();
        //        using (DbCommand command = db.GetStoredProcCommand("SP_Events"))
        //        {
        //            db.AddInParameter(command, "@action", DbType.String, "GetEvent");

        //            using (IDataReader reader = db.ExecuteReader(command))
        //            {
        //                while (reader.Read())
        //                {
        //                    int eventID = Convert.ToInt32(reader["Id"]);

        //                    var existingEvent = EventList.FirstOrDefault(x => x.ID == eventID);

        //                    if (existingEvent != null) 
        //                    {
        //                        existingEvent = new EventDTO()
        //                        {
        //                            ID = eventID,
        //                            EventTitle = reader["EventTitle"].ToString(),
        //                            SubTitle = reader["SubTitle"].ToString(),
        //                            Paragraph = reader["Paragraph"].ToString(),
        //                            ImagePaths = new List<string>()
        //                        };
        //                        EventList.Add(existingEvent);
        //                    }
        //                    string imagePath = reader["ImagePath"].ToString();
        //                    existingEvent.ImagePaths.Add(imagePath);
        //                }
        //            }
        //        }
        //        return EventList;
        //    }
        //    catch (Exception ex) 
        //    {
        //        errorLog("EventDTO - GetEvents", ex.ToString());
        //        exception = ex.Message;
        //        throw;              
        //    }

        //}

        public List<EventDTO> GetEvents()
        {
            try
            {
                List<EventDTO> eventList = new List<EventDTO>();

                using (DbCommand command = db.GetStoredProcCommand("SP_Events"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "GetEvent");

                    using (IDataReader reader = db.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            int eventID = Convert.ToInt32(reader["EventID"]);
                            int eventImageID = Convert.ToInt32(reader["EventImageID"]);

                            // Pehle se exist karta hai ya nahi check karein
                            var existingEvent = eventList.FirstOrDefault(x => x.ID == eventID);

                            if (existingEvent == null)
                            {
                                existingEvent = new EventDTO()
                                {
                                    ID = eventID,
                                    EventTitle = reader["EventTitle"].ToString(),
                                    SubTitle = reader["SubTitle"].ToString(),
                                    Paragraph = reader["Paragraph"].ToString(),
                                    ImagePaths = new List<string>(),
                                    
                                };

                                eventList.Add(existingEvent);
                            }

                            // ImagePath add karein
                            string imagePath = reader["ImagePath"].ToString();
                            existingEvent.ImagePaths.Add($"{eventImageID}|{imagePath}");
                        }
                    }
                }

                return eventList;
            }
            catch (Exception ex)
            {
                errorLog("EventDTO - GetEvents", ex.ToString());
                throw;
            }
        }

        public List<EventDTO> GetEventsByTitle(string EventTitle)
        {
            try
            {
                List<EventDTO> eventList = new List<EventDTO>();

                using (DbCommand command = db.GetStoredProcCommand("SP_Events"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "GetEventByEventTitle"); // ✅ Correct action name
                    db.AddInParameter(command, "@EventTitle", DbType.String, EventTitle);

                    using (IDataReader reader = db.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            int eventID = Convert.ToInt32(reader["EventID"]);
                            int eventImageID = reader["EventImageID"] != DBNull.Value ? Convert.ToInt32(reader["EventImageID"]) : 0;
                            string imagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : "";

                            // Pehle check karo ki eventList me ye event already exist karta hai ya nahi
                            var existingEvent = eventList.FirstOrDefault(e => e.ID == eventID);

                            if (existingEvent == null)
                            {
                                // Naya event create karo agar exist nahi karta
                                existingEvent = new EventDTO()
                                {
                                    ID = eventID,
                                    EventTitle = reader["EventTitle"].ToString(),
                                    SubTitle = reader["SubTitle"].ToString(),
                                    Paragraph = reader["Paragraph"].ToString(),
                                    ImagePaths = new List<string>(), // Empty list initially
                                    URL = reader["URL"].ToString()
                                };
                                eventList.Add(existingEvent);
                            }

                            // ImagePath add karo agar available hai
                            if (!string.IsNullOrEmpty(imagePath))
                            {
                                existingEvent.ImagePaths.Add($"{eventImageID}|{imagePath}");
                            }
                        }
                    }
                }

                return eventList;
            }
            catch (Exception ex)
            {
                errorLog("EventDTO - GetEventsByTitle", ex.ToString());
                throw;
            }
        }


        public EventDTO AddEvent(EventDTO.EventsEntity add, List<string> imagePaths)
        {
            int eventId = 0;
            try
            {
                objEventDTO.EventsList = new List<EventDTO.EventsEntity>();

                using (DbCommand command = db.GetStoredProcCommand("SP_Events"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "AddEvent");
                    db.AddInParameter(command, "@EventTitle", DbType.String, add.EventTitle);
                    db.AddInParameter(command, "@SubTitle", DbType.String, add.SubTitle);
                    db.AddInParameter(command, "@Paragraph", DbType.String, add.Paragraph);
                    //db.AddInParameter(command, "@ImagePath", DbType.String, add.ImagePath);


                    object objEventId = db.ExecuteScalar(command);
                    if (objEventId != null && objEventId != DBNull.Value)
                    {
                        eventId = Convert.ToInt32(objEventId);
                    }
                }
                if (eventId > 0)
                {
                    foreach (string imagePath in imagePaths)
                    {
                        using (DbCommand cmdImage = db.GetStoredProcCommand("SP_Events"))
                        {
                            db.AddInParameter(cmdImage, "@action", DbType.String, "AddEventImageforMultipleImg");
                            db.AddInParameter(cmdImage, "@EventId", DbType.Int32, eventId);

                            db.AddInParameter(cmdImage, "@ImagePath", DbType.String, imagePath);

                            db.ExecuteNonQuery(cmdImage);
                        }
                    }
                }

                objEventDTO.Code = (int)Errorcode.ErrorType.SUCESS;
                objEventDTO.Message = "Event added successfully";
            }
            catch (Exception ex)
            {
                exception = ex.ToString();
                objEventDTO.Code = (int)Errorcode.ErrorType.ERROR;
                objEventDTO.Message = "Error occurred while adding event";
                DALBASE res = new DALBASE();
                res.errorLog("Method Name: AddEvent", exception);
            }
            return objEventDTO;
        }

        public void UpdateEvent(EventDTO.EventsEntity eventDTO)
        {
            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_Events"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "UpdateEvent");
                    db.AddInParameter(command, "@Id", DbType.Int32, eventDTO.ID);
                    db.AddInParameter(command, "@EventTitle", DbType.String, eventDTO.EventTitle);
                    db.AddInParameter(command, "@SubTitle", DbType.String, eventDTO.SubTitle);
                    db.AddInParameter(command, "@Paragraph", DbType.String, eventDTO.Paragraph);
                    db.AddInParameter(command, "@EventImageId", DbType.String, eventDTO.EventImageId);
                    db.AddInParameter(command, "@ImagePath", DbType.String, eventDTO.Imagepath);

                    db.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                exception = ex.ToString();
                throw;
            }
        }

        public void DeleteEvent(int id)
        {
            using (DbCommand command = db.GetStoredProcCommand("SP_Events"))
            {
                try
                {
                    db.AddInParameter(command, "@action", DbType.String, "DeleteEvent");
                    db.AddInParameter(command, "@Id", DbType.Int32, id);

                    db.ExecuteNonQuery(command);
                }
                catch (Exception ex)
                {
                    exception = ex.ToString();
                    throw;
                }
            }
        }

    }
}
