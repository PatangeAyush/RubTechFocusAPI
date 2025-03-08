using RubTechFocus.DTO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubTechFocus.DAL
{
    public class TechnicalAbstractDAL:DALBASE
    {
        //public List<TechnicalAbstractDTO> GetTechnicalAbstracts()
        //{
        //    List<TechnicalAbstractDTO> list = new List<TechnicalAbstractDTO>();

        //    using (DbCommand command = db.GetStoredProcCommand("SP_TechnicalAbstract"))
        //    {
        //        db.AddInParameter(command, "@action", DbType.String, "GetTechnicalAbstract");
        //        using (IDataReader reader = db.ExecuteReader(command))
        //        {
        //            while (reader.Read())
        //            {
        //                list.Add(new TechnicalAbstractDTO
        //                {
        //                    Id = Convert.ToInt32(reader["Id"]),
        //                    Name = reader["Name"].ToString(),
        //                    Absract_ImagePath = reader["Absract_ImagePath"].ToString(),
        //                    Author_ImagePath = reader["Author_ImagePath"].ToString(),
        //                    Author_Name = reader["Author_Name"].ToString(),
        //                    Author_Description = reader["Author_Description"].ToString(),
        //                    Abstract_Paragraph = reader["Abstract_Paragraph"].ToString()
        //                });
        //            }
        //        }
        //    }
        //    return list;
        //}

        //public TechnicalAbstractDTO AddTechnicalAbstract(TechnicalAbstractDTO model)
        //{
        //    try
        //    {
        //        using (DbCommand command = db.GetStoredProcCommand("SP_TechnicalAbstract"))
        //        {
        //            db.AddInParameter(command, "@action", DbType.String, "AddTechnicalAbstract");
        //            db.AddInParameter(command, "@Name", DbType.String, model.Name);
        //            db.AddInParameter(command, "@Absract_ImagePath", DbType.String, model.Absract_ImagePath);
        //            db.AddInParameter(command, "@Author_ImagePath", DbType.String, model.Author_ImagePath);
        //            db.AddInParameter(command, "@Author_Name", DbType.String, model.Author_Name);
        //            db.AddInParameter(command, "@Author_Description", DbType.String, model.Author_Description);
        //            db.AddInParameter(command, "@Abstract_Paragraph", DbType.String, model.Abstract_Paragraph);
        //            db.ExecuteNonQuery(command);
        //        }
        //        model.Message = "Technical Abstract Added Successfully";
        //        model.Code = 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        model.Message = "Error occurred while adding Technical Abstract";
        //        model.Code = 0;
        //    }
        //    return model;
        //}

        string Exception; 
        public List<TechnicalAbstractDTO> GetTechnicalAbstracts()
        {

            TechnicalAbstractDTO obj = new TechnicalAbstractDTO();
            List<TechnicalAbstractDTO> list = new List<TechnicalAbstractDTO>();

            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_TechnicalAbstract"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "GetTechnicalAbstract");

                    using (IDataReader reader = db.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {

                      
                            list.Add(new TechnicalAbstractDTO
                            {
                                Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0,
                                Name = reader["Name"]?.ToString(),
                                Absract_ImagePath = reader["Absract_ImagePath"]?.ToString(),
                                Author_ImagePath = reader["Author_ImagePath"]?.ToString(),
                                Author_Name = reader["Author_Name"]?.ToString(),
                                Author_Description = reader["Author_Description"]?.ToString(),
                                Abstract_Paragraph = reader["Abstract_Paragraph"]?.ToString(),
                                //Created_Date = reader["Created_Date"] != DBNull.Value ? Convert.ToDateTime(reader["Created_Date"]) : (DateTime?)null,
                                //Updated_Date = reader["Updated_Date"] != DBNull.Value ? Convert.ToDateTime(reader["Updated_Date"]) : (DateTime?)null
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               Exception = ex.ToString();
                throw;
                //Console.WriteLine($"Error in GetTechnicalAbstracts: {ex.Message}");
            }
            return list;
        }

        public TechnicalAbstractDTO AddTechnicalAbstract(TechnicalAbstractDTO model)
        {
            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_TechnicalAbstract"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "AddTechnicalAbstract");
                    db.AddInParameter(command, "@Name", DbType.String, model.Name);
                    db.AddInParameter(command, "@Absract_ImagePath", DbType.String, model.Absract_ImagePath ?? (object)DBNull.Value);
                    db.AddInParameter(command, "@Author_ImagePath", DbType.String, model.Author_ImagePath ?? (object)DBNull.Value);
                    db.AddInParameter(command, "@Author_Name", DbType.String, model.Author_Name ?? (object)DBNull.Value);
                    db.AddInParameter(command, "@Author_Description", DbType.String, model.Author_Description ?? (object)DBNull.Value);
                    db.AddInParameter(command, "@Abstract_Paragraph", DbType.String, model.Abstract_Paragraph ?? (object)DBNull.Value);

                    db.ExecuteNonQuery(command);
                }

                model.Message = "Technical Abstract Added Successfully";
                model.Code = 1;
            }
            catch (Exception ex)
            {
                model.Message = "Error occurred while adding Technical Abstract";
                model.Code = 0;
                Console.WriteLine($"Error in AddTechnicalAbstract: {ex.Message}");
            }
            return model;
        }

        public TechnicalAbstractDTO UpdateTechnicalAbstract(TechnicalAbstractDTO model)
        {
            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_TechnicalAbstract"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "UpdateTechnicalAbstract");
                    db.AddInParameter(command, "@Id", DbType.Int32, model.Id);
                    db.AddInParameter(command, "@Name", DbType.String, model.Name);

                    // Agar naye images aaye toh update ho, nahi toh purana hi rahe
                    db.AddInParameter(command, "@Absract_ImagePath", DbType.String,
                        string.IsNullOrEmpty(model.Absract_ImagePath) ? (object)DBNull.Value : model.Absract_ImagePath);
                    db.AddInParameter(command, "@Author_ImagePath", DbType.String,
                        string.IsNullOrEmpty(model.Author_ImagePath) ? (object)DBNull.Value : model.Author_ImagePath);

                    db.AddInParameter(command, "@Author_Name", DbType.String, model.Author_Name);
                    db.AddInParameter(command, "@Author_Description", DbType.String, model.Author_Description);
                    db.AddInParameter(command, "@Abstract_Paragraph", DbType.String, model.Abstract_Paragraph);

                    db.ExecuteNonQuery(command);
                }

                model.Message = "Technical Abstract Updated Successfully";
                model.Code = 1;
            }
            catch (Exception ex)
            {
                model.Message = "Error occurred while updating Technical Abstract";
                model.Code = 0;
                Console.WriteLine($"Error in UpdateTechnicalAbstract: {ex.Message}");
            }
            return model;
        }

        public TechnicalAbstractDTO DeleteTechnicalAbstract(int id)
        {
            TechnicalAbstractDTO model = new TechnicalAbstractDTO();
            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_TechnicalAbstract"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "DeleteTechnicalAbstract");
                    db.AddInParameter(command, "@Id", DbType.Int32, id);

                    db.ExecuteNonQuery(command);
                }

                model.Message = "Technical Abstract Deleted Successfully";
                model.Code = 1;
            }
            catch (Exception ex)
            {
                model.Message = "Error occurred while deleting Technical Abstract";
                model.Code = 0;
                Console.WriteLine($"Error in DeleteTechnicalAbstract: {ex.Message}");
            }
            return model;
        }


    }
}
