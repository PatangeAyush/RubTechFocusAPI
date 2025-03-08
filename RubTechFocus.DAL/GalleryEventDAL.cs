using RubTechFocus.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace RubTechFocus.DAL
{
    public class GalleryEventDAL : DALBASE
    {
        //GalleryEventDTO objgalleryEventDTO = new GalleryEventDTO();

        string exception = "";
        public List<GalleryEventDTO> GetGallery()
        {
            GalleryEventDTO objgalleryEventDTO = new GalleryEventDTO();
            try
            {
                List<GalleryEventDTO> galleryList = new List<GalleryEventDTO>();

                using (command = db.GetStoredProcCommand("SP_Gallery"))
                {
                    db.AddInParameter(command, "@Action", DbType.String, "GetGallery");

                    using (IDataReader reader = db.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            int eventId = Convert.ToInt32(reader["ID"]);

                            var existingEvent = galleryList.FirstOrDefault(x => x.ID == eventId);

                            if (existingEvent == null)
                            {
                                existingEvent = new GalleryEventDTO()
                                {
                                    ID = eventId,
                                    Title = reader["Title"].ToString(),
                                    ImagePaths = new List<string>(),
                                    //BigImagePaths = new List<string>()
                                };

                                galleryList.Add(existingEvent);
                            }

                            string ImagePaths = reader["ImagePath"].ToString();
                            existingEvent.ImagePaths.Add(ImagePaths);

                            //string BigImagePath = reader["BigImagePath"].ToString();
                            //existingEvent.BigImagePaths.Add(BigImagePath);
                        }
                    }
                }

                return galleryList;
            }
            catch (Exception ex)
            {
                errorLog("GalleryEventDTO - GetGallery", ex.ToString());
                throw;
            }
        }

        public GalleryEventDTO GetGalleryEvents()
        {
            GalleryEventDTO objgalleryEventDTO = new GalleryEventDTO();
            try
            {
                objgalleryEventDTO.GalleryEventList = new List<GalleryEventDTO.GalleryEventEntity>();
                using (command = db.GetStoredProcCommand("SP_Gallery"))
                {
                    db.AddInParameter(command, "@Action", DbType.String, "slctGalleryEvents");

                }
                IDataReader Reader = db.ExecuteReader(command);
                if (Reader == null)
                {
                    objgalleryEventDTO.Code = (int)Errorcode.ErrorType.DATANOTFOUND;
                    objgalleryEventDTO.Message = "DATANOTFOUND";
                }
                else
                {
                    while (Reader.Read())
                    {
                        objgalleryEventDTO.GalleryEventList.Add(new GalleryEventDTO.GalleryEventEntity
                        {

                            Title = Convert.ToString(Reader["Title"]),

                        });
                    }
                    objgalleryEventDTO.Code = (int)Errorcode.ErrorType.SUCESS;
                    objgalleryEventDTO.Message = "Success";

                }
                return objgalleryEventDTO;
            }
            catch (Exception ex)
            {
                errorLog("GalleryEventDTO" + "GetGallery", ex.ToString());
                throw ex;
            }
        }

        public GalleryEventDTO AddEvent(GalleryEventDTO add)
        {

            string exception = "";

            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_Gallery"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "AddEvent");
                    db.AddInParameter(command, "@Title", DbType.String, add.Title);

                    // Execute Scalar to get Event ID
                    int eventId = Convert.ToInt32(db.ExecuteScalar(command));

                    if (eventId > 0 && add.ImagePaths != null && add.ImagePaths.Count > 0)
                    {
                        // Reuse the same DbCommand instance for inserting images
                        DbCommand cmdImage = db.GetStoredProcCommand("SP_Gallery");
                        db.AddInParameter(cmdImage, "@Action", DbType.String, "AddEventImageforMultipleImg");
                        db.AddInParameter(cmdImage, "@EventId", DbType.Int32, eventId);
                        db.AddInParameter(cmdImage, "@ImagePath", DbType.String, add.ImagePaths);

                        foreach (string imagePath in add.ImagePaths)
                        {
                            db.SetParameterValue(cmdImage, "@ImagePath", imagePath);
                            db.ExecuteNonQuery(cmdImage);
                        }
                    }

                    add.Code = (int)Errorcode.ErrorType.SUCESS;
                    add.Message = "Event added successfully";
                }
            }
            catch (Exception ex)
            {
                exception = ex.ToString();
                add.Code = (int)Errorcode.ErrorType.ERROR;
                add.Message = "Error occurred while adding event";
                DALBASE res = new DALBASE();
                res.errorLog("Method Name: AddEvent", exception);
            }

            return add;
        }

        //public GalleryEventDTO AddEvent(GalleryEventDTO add)
        //{
        //    int eventId = 0;

        //    try
        //    {
        //        using (DbCommand command = db.GetStoredProcCommand("SP_Gallery"))
        //        {
        //            db.AddInParameter(command, "@action", DbType.String, "AddEvent");
        //            db.AddInParameter(command, "@Title", DbType.String, add.Title);

        //            // ExecuteScalar to get Event ID
        //            object objEventId = db.ExecuteScalar(command);
        //            if (objEventId != null && objEventId != DBNull.Value)
        //            {
        //                eventId = Convert.ToInt32(objEventId);
        //            }
        //        }
        //        //if (eventId > 0 && add.SmallImagePaths != null && add.SmallImagePaths.Count > 0)
        //        //{
        //        //    foreach (string smallimagePath in add.SmallImagePaths)
        //        //    {
        //        //        foreach (string bigimagepath in add.BigImagePaths)
        //        //        {
        //        //            using (DbCommand cmdImage = db.GetStoredProcCommand("SP_Gallery"))
        //        //            {
        //        //                db.AddInParameter(cmdImage, "@action", DbType.String, "AddEventImageforMultipleImg");
        //        //                db.AddInParameter(cmdImage, "@Id", DbType.Int32, eventId);
        //        //                db.AddInParameter(cmdImage, "@BigImagePath", DbType.String, bigimagepath);
        //        //                db.AddInParameter(cmdImage, "@SmallImagePath", DbType.String, smallimagePath);

        //        //                db.ExecuteNonQuery(cmdImage);
        //        //            }
        //        //        }

        //        //    }
        //        //}

        //        if (eventId > 0 && add.SmallImagePaths != null && add.BigImagePaths != null &&
        //            add.SmallImagePaths.Count == add.BigImagePaths.Count)
        //        {
        //            for (int i = 0; i < add.SmallImagePaths.Count; i++)  // Loop one-by-one
        //            {
        //                using (DbCommand cmdImage = db.GetStoredProcCommand("SP_Gallery"))
        //                {
        //                    db.AddInParameter(cmdImage, "@action", DbType.String, "AddEventImageforMultipleImg");
        //                    db.AddInParameter(cmdImage, "@Id", DbType.Int32, eventId);
        //                    db.AddInParameter(cmdImage, "@BigImagePath", DbType.String, add.BigImagePaths[i]);
        //                    db.AddInParameter(cmdImage, "@SmallImagePath", DbType.String, add.SmallImagePaths[i]);

        //                    db.ExecuteNonQuery(cmdImage);
        //                }
        //            }
        //        }


        //        add.Code = (int)Errorcode.ErrorType.SUCESS;
        //        add.Message = "Event added successfully";
        //    }
        //    catch (Exception ex)
        //    {
        //        exception = ex.ToString();
        //        add.Code = (int)Errorcode.ErrorType.ERROR;
        //        add.Message = "Error occurred while adding event";
        //        DALBASE res = new DALBASE();
        //        res.errorLog("Method Name: AddEvent", exception);
        //    }

        //    return add;
        //}

        public void AddEventImage(int id, string ImagePath)
        {
            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_Gallery"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "AddEventImage");
                    db.AddInParameter(command, "@Id", DbType.Int32, id);
                    db.AddInParameter(command, "@ImagePath", DbType.String, ImagePath);
                    //db.AddInParameter(command, "@BigImagePath", DbType.String, BigImagePath);
                    db.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                exception = ex.ToString();
                throw;
            }
        }

        public void RenameEvent(int id, string EventName)
        {
            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_Gallery"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "RenameEvent");
                    db.AddInParameter(command, "@id", DbType.Int32, id);
                    db.AddInParameter(command, "@Title", DbType.String, EventName);

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
            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_Gallery"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "DeleteEvent");
                    db.AddInParameter(command, "@Id", DbType.Int32, id);

                    db.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                DALBASE res = new DALBASE();
                res.errorLog("Method Name: DeleteEvent", ex.ToString());
            }
        }

        public void DeleteEventImage(int id)
        {
            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_Gallery"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "DeleteEventImage");
                    db.AddInParameter(command, "@Id", DbType.Int32, id);
                    db.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                DALBASE res = new DALBASE();
                res.errorLog("Method Name : Delete Event Image", ex.ToString());
            }

        }

    }
}
