using RubTechFocus.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RubTechFocus.DTO.GalleryReportDTO;

namespace RubTechFocus.DAL
{
    public class GalleryReportDAL : DALBASE
    {
        GalleryReportDTO objGalleryReportDTO = new GalleryReportDTO();

        string Exception = "";

        //public List<GalleryReportDTO> GetReports()
        //{
        //    GalleryReportDTO objGalleryReportDTO = new GalleryReportDTO();

        //    objGalleryReportDTO.GalleryReportList = new List<GalleryReportEntity>();
        //    try
        //    {
        //        using (DbCommand command = db.GetSqlStringCommand("SP_GalleryReport"))
        //        {

        //            db.AddInParameter(command, "@action", DbType.String, "GetReport");

        //            using (IDataReader reader = db.ExecuteReader(command))
        //            {
        //                while (reader.Read())
        //                {
        //                    int reportID = Convert.ToInt32(reader["ID"]);

        //                    var existingReport = objGalleryReportDTO.GalleryReportList.FirstOrDefault(x => x.ID == reportID);

        //                    if (existingReport == null)
        //                    {
        //                        GalleryReportEntity user = new GalleryReportEntity
        //                        {
        //                            ID = Convert.ToInt16(reader["ID"]),
        //                            ReportName = reader["ReportName"].ToString(),
        //                            ImagePath = reader["ImagePath"].ToString()
        //                        };

        //                        objGalleryReportDTO.GalleryReportList.Add(user);
        //                    }
        //                }
        //            }

        //            if (objGalleryReportDTO.GalleryReportList.Count > 0)
        //            {
        //                objGalleryReportDTO.Messege = "Report Fetched Successfully";
        //                objGalleryReportDTO.Code = 200;
        //            }
        //            else
        //            {
        //                objGalleryReportDTO.Code = 500;
        //                objGalleryReportDTO.Messege = "Error In Fetching data";
        //            }
        //        }
        //        return objGalleryReportDTO;
        //    }
        //    catch (Exception ex) 
        //    {
        //        objGalleryReportDTO.Code = (int)Errorcode.ErrorType.ERROR;
        //        objGalleryReportDTO.Messege = "Error occurred while retrieving Reports.";

        //        DALBASE res = new DALBASE();
        //        res.errorLog("Method Name: GetReports", ex.ToString());
        //    }
        //}

        //public GalleryReportDTO GetReportss()
        //{
        //    GalleryReportDTO objGalleryReportDTO = new GalleryReportDTO();
        //    objGalleryReportDTO.GalleryReportList = new List<GalleryReportEntity>();

        //    try
        //    {
        //        using (DbCommand command = db.GetSqlStringCommand("SP_GalleryReport"))
        //        {
        //            db.AddInParameter(command, "@action", DbType.String, "GetReport");

        //            using (IDataReader reader = db.ExecuteReader(command))
        //            {

        //                while (reader.Read())
        //                {
        //                    // Check for DBNull values to prevent runtime exceptions
        //                    int reportID = Convert.ToInt16(reader["ID"]);
        //                    var existingReport = objGalleryReportDTO.GalleryReportList.FirstOrDefault(x => x.ID == reportID);

        //                    if (existingReport == null)
        //                    {
        //                        GalleryReportEntity report = new GalleryReportEntity
        //                        {
        //                            ID = reportID,
        //                            ReportName = reader["ReportName"]?.ToString(),
        //                            ImagePath = reader["ImagePath"]?.ToString()
        //                        };

        //                        objGalleryReportDTO.GalleryReportList.Add(report);
        //                    }
        //                }
        //            }

        //            if (objGalleryReportDTO.GalleryReportList.Count > 0)
        //            {
        //                objGalleryReportDTO.Messege = "Report Fetched Successfully";
        //                objGalleryReportDTO.Code = 200;
        //            }
        //            else
        //            {
        //                objGalleryReportDTO.Code = 500;
        //                objGalleryReportDTO.Messege = "Error In Fetching data";
        //            }
        //        }
        //        return objGalleryReportDTO;
        //    }
        //    catch (Exception ex)
        //    {
        //        objGalleryReportDTO.Code = (int)Errorcode.ErrorType.ERROR;
        //        objGalleryReportDTO.Messege = "Error occurred while retrieving Reports.";

        //        DALBASE res = new DALBASE();
        //        res.errorLog("Method Name: GetReports", ex.ToString());

        //        return objGalleryReportDTO;
        //    }
        //}

        public List<GalleryReportDTO> GetReports()
        {
            try
            {
                List<GalleryReportDTO> reportList = new List<GalleryReportDTO>();

                using (DbCommand command = db.GetStoredProcCommand("SP_GalleryReport"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "GetReport");

                    using (IDataReader reader = db.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            int reportID = Convert.ToInt32(reader["ReportId"]);
                            int GalleryReportImageID = Convert.ToInt32(reader["ReportImageID"]);

                            var existingReport = reportList.FirstOrDefault(x => x.ID == reportID);

                            if (existingReport == null)
                            {
                                existingReport = new GalleryReportDTO()
                                {
                                    ID = reportID,
                                    ReportName = reader["ReportName"]?.ToString(),
                                    ImagePaths = new List<string>()
                                };

                                reportList.Add(existingReport);
                            }

                            string imagePath = reader["ImagePath"]?.ToString();
                            if (!string.IsNullOrEmpty(imagePath))
                            {
                                existingReport.ImagePaths.Add($"{GalleryReportImageID}|{imagePath}");
                            }
                        }
                    }
                }

                return reportList;
            }
            catch (Exception ex)
            {
                errorLog("GalleryReportDTO - GetReports", ex.ToString());
                throw;
            }
        }

        public GalleryReportDTO AddGalleryReport(GalleryReportDTO add)
        {
            int reportID = 0;
            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_GalleryReport"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "AddGalleryReport");
                    db.AddInParameter(command, "@ReportName", DbType.String, add.ReportName);

                    object objReportId = db.ExecuteScalar(command);

                    if (objReportId != null && objReportId != DBNull.Value)
                    {
                        reportID = Convert.ToInt32(objReportId);
                    }

                    if (reportID > 0 && add.ImagePaths != null && add.ImagePaths.Count > 0)
                    {
                        foreach (string imagepath in add.ImagePaths) 
                        {
                            using (DbCommand command1 = db.GetStoredProcCommand("SP_GalleryReport"))
                            {
                                db.AddInParameter(command1, "@action", DbType.String, "AddGalleryReportImages");
                                db.AddInParameter(command1, "@Id", DbType.Int32, reportID);
                                db.AddInParameter(command1, "@ImagePath", DbType.String, imagepath);

                                db.ExecuteNonQuery(command1);
                            }
                        }
                    }
                }
                add.Messege = "Gallery Report Added Successfully";
                add.Code = (int)Errorcode.ErrorType.SUCESS;
            }
            catch (Exception ex)
            {
                Exception = ex.ToString();
                add.Code = (int)Errorcode.ErrorType.ERROR;
                add.Messege = "Error occurred while adding event";
                DALBASE res = new DALBASE();
                res.errorLog("Method Name: AddEvent", Exception);
            }
            return add;
        }

        public void AddReportSingleImage(int id, string imagePath)
        {
            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_GalleryReport"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "AddReportImage");
                    db.AddInParameter(command, "@Id", DbType.Int32, id);
                    db.AddInParameter(command, "@ImagePath", DbType.String, imagePath);
                    //db.AddInParameter(command, "@BigImagePath", DbType.String, BigImagePath);
                    db.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                Exception = ex.ToString();
                throw;
            }
        }
        public void RenameReport(int id, string reportName)
        {
            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_GalleryReport"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "RenameReport");
                    db.AddInParameter(command, "@Id", DbType.Int32, id);
                    db.AddInParameter(command, "@ReportName", DbType.String, reportName);

                    db.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                DALBASE res = new DALBASE();
                res.errorLog("Method Name: RenameReport", ex.ToString());
                throw;
            }
        }

        public void DeleteReport(int id)
        {
            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_GalleryReport"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "DeleteReport");
                    db.AddInParameter(command, "@Id", DbType.Int32, id);

                    db.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                DALBASE res = new DALBASE();
                res.errorLog("Method Name: DeleteReport", ex.ToString());
            }
        }

        public void DeleteReportImage(string imagePath)
        {
            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_GalleryReport"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "DeleteReportImage");
                    db.AddInParameter(command, "@ImagePath", DbType.String, imagePath);

                    db.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                DALBASE res = new DALBASE();
                res.errorLog("Method Name: DeleteReportImage", ex.ToString());
            }
        }

    }
}

