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
    public class MessegeDAL:DALBASE
    {
        string Exception;

        public List<MessegeDTO> GetMessege()
        {
            using (command = db.GetStoredProcCommand("SP_Messege"))
            {
                db.AddInParameter(command, "@action", DbType.String, "GetMessege");

                List<MessegeDTO> messegeList = new List<MessegeDTO>();
                IDataReader reader = db.ExecuteReader(command);

                while (reader.Read())
                {
                    messegeList.Add(new MessegeDTO()
                    {
                        Id = Convert.ToInt32(reader["ID"]),
                        MessegeFrom = reader["MessegeFrom"].ToString(),
                        Name = reader["Name"].ToString(),
                        Position = reader["Position"].ToString(),
                        Paragraph = reader["Paragraph"].ToString(),
                        ImagePath = reader["ImagePath"].ToString()
                    });
                }
                return messegeList;
            }
        }

        public MessegeDTO AddMessege(MessegeDTO messege)
        {
            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_Messege"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "AddMessege");
                    db.AddInParameter(command, "@MessegeFrom", DbType.String, messege.MessegeFrom);
                    db.AddInParameter(command, "@Name", DbType.String, messege.Name);
                    db.AddInParameter(command, "@Position", DbType.String, messege.Position);
                    db.AddInParameter(command, "@Paragraph", DbType.String, messege.Paragraph);
                    db.AddInParameter(command, "@ImagePath", DbType.String, messege.ImagePath);
                    db.ExecuteNonQuery(command);
                }
                return messege;
            }
            catch (Exception ex)
            {
                Exception = ex.ToString();
                throw;
            }
        }

        public MessegeDTO UpdateMessege(MessegeDTO messege)
        {
            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_Messege"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "UpdateMessege");
                    db.AddInParameter(command, "@Id", DbType.Int32, messege.Id);
                    db.AddInParameter(command, "@MessegeFrom", DbType.String, messege.MessegeFrom);
                    db.AddInParameter(command, "@Name", DbType.String, messege.Name);
                    db.AddInParameter(command, "@Position", DbType.String, messege.Position);
                    db.AddInParameter(command, "@ImagePath", DbType.String, messege.ImagePath);
                    db.ExecuteNonQuery(command);
                }
                return messege;
            }
            catch (Exception ex)
            {
                Exception = ex.ToString();
                throw;
            }
        }

        public void DeleteMessege(int id)
        {
            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_Messege"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "DeleteMessege");
                    db.AddInParameter(command, "@Id", DbType.Int32, id);
                    db.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                Exception = ex.ToString();
                throw;
            }
        }   
    }
}
