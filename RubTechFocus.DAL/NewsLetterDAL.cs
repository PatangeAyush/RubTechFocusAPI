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
    public class NewsLetterDAL:DALBASE
    {
        //public List<NewsLetterDTO> GetNewsLetter()
        //{
        //    using (DbCommand command = db.GetStoredProcCommand("SP_NewsLetter"))
        //    {
        //        db.AddInParameter(command,"@action", DbType.String, "GetNewsLetter");

        //        List<NewsLetterDTO> newsLetterList = new List<NewsLetterDTO>();

        //        using (IDataReader reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                newsLetterList.Add(new NewsLetterDTO()
        //                {
        //                    Email = reader["Email"].ToString(),
        //                    DateTime = Convert.ToDateTime(reader["DateTime"])
        //                });
        //            }
        //            return newsLetterList;
        //        }


        //    }
        //}

        public List<NewsLetterDTO> GetNewsLetter()
        {
            List<NewsLetterDTO> newsLetterList = new List<NewsLetterDTO>();

            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_NewsLetter"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "GetNewsLetter");

                    using (IDataReader reader = db.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            var newsletter = new NewsLetterDTO()
                            {
                                Email = reader["Email"].ToString(),
                              
                                DateTime = Convert.ToDateTime(reader["Created_Date"])
                            };

                            newsLetterList.Add(newsletter);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                // LogException(ex);  // Implement your logging mechanism here
                throw; // Rethrow or handle appropriately
            }

            return newsLetterList;
        }

        public void AddNewsLetter(string email) 
        {
            try 
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_NewsLetter"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "AddNewsLetter");
                    db.AddInParameter(command, "@email", DbType.String, email);

                    db.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                string Exception = ex.Message;
                throw ex;
            }
        }
    }
}
