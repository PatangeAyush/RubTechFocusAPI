using RubTechFocus.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace RubTechFocus.DAL
{
    public class ReportDAL:DALBASE
    {
        ReportDTO objreportDTO = new ReportDTO();

        string Exception;
        public List<ReportDTO> GetReport()
        {
            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_Report"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "GetReport");

                    List<ReportDTO> reportList = new List<ReportDTO>();

                    IDataReader reader = db.ExecuteReader(command);

                    while (reader.Read())
                    {
                        reportList.Add(new ReportDTO()
                        {

                            ID = Convert.ToInt32(reader["ID"]),
                            ReportName = reader["ReportName"].ToString(),
                            ReportSubParagraph = reader["ReportSubParagraph"].ToString(),
                            ReportImage = reader["ReportImage"].ToString(),
                            Report_Description = reader["Report_Description"].ToString(),
                        });
                    }
                    return reportList;
                }
            }
            catch (Exception ex) 
            {
                Exception = ex.Message;
                errorLog("GalleryReportDTO - GetReports", ex.ToString());
                throw;
            }
            
        }
        public ReportDTO GetReportByID(int id)
        {
            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_Report"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "GetReportByID");
                    db.AddInParameter(command, "@Id", DbType.Int16, id);

                    IDataReader reader = db.ExecuteReader(command);

                    if (reader.Read())  // ✅ Sirf ek record return hoga, toh `while` ki zaroorat nahi
                    {
                        return new ReportDTO
                        {
                            ID = Convert.ToInt32(reader["Id"]),
                            ReportName = reader["ReportName"].ToString(),
                            ReportImage = reader["ReportImage"].ToString(),
                            Report_Description = reader["Report_Description"].ToString(),
                            ReportSubParagraph = reader["ReportSubParagraph"].ToString(),
                        };
                    }
                    else
                    {
                        return null; // ✅ Agar koi data na mile toh `null` return karna hoga
                    }
                }
            }
            catch (Exception ex)
            {
                Exception = ex.Message; // ✅ Global exception variable se bachne ke liye log karo
                throw new Exception("Error fetching report by ID", ex);
            }
        }

        public ReportDTO AddReport(ReportDTO add) 
        {
            try 
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_Report"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "AddReport");
                    db.AddInParameter(command, "@ReportName", DbType.String, add.ReportName);
                    db.AddInParameter(command, "@ReportSubParagraph", DbType.String, add.ReportSubParagraph);
                    db.AddInParameter(command, "@ReportImage", DbType.String, add.ReportImage);
                    db.AddInParameter(command, "@Report_Description", DbType.String, add.Report_Description);

                    db.ExecuteNonQuery(command);

                }
            }
            catch (Exception ex)
            {
                Exception = ex.Message;
                throw;
            }
            return add;
           
        }

        public ReportDTO UpdateReport(ReportDTO report)
        {
            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_Report"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "UpdateReport");
                    db.AddInParameter(command, "@Id", DbType.Int32, report.ID);
                    db.AddInParameter(command, "@ReportName", DbType.String, report.ReportName);
                    db.AddInParameter(command, "@ReportSubParagraph", DbType.String, report.ReportSubParagraph);
                    db.AddInParameter(command, "@ReportImage", DbType.String, string.IsNullOrEmpty(report.ReportImage) ? (object)DBNull.Value : report.ReportImage);
                    db.AddInParameter(command, "@Report_Description", DbType.String, report.Report_Description);
                    db.ExecuteNonQuery(command);
                    
                    return report;
                }
            }
            catch (Exception ex)
            {
                errorLog("ReportDAL - UpdateReport", ex.ToString());
                return report;
            }
        }

        public void DeleteReport(int id)
        {
            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_Report"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "DeleteReport");
                    db.AddInParameter(command, "@Id", DbType.Int32, id);
                   
                    db.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                errorLog("ReportDAL - UpdateReport", ex.ToString());
                Exception = ex.Message;
                throw;
            }
        }
    }
}
