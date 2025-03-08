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
    public class MentorDAL:DALBASE
    {
        string Exception;

        public List<MentorDTO> GetMentor()
        {
            try 
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_Mentor"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "GetMentors");

                    List<MentorDTO> mentorList = new List<MentorDTO>();

                    IDataReader reader = db.ExecuteReader(command);

                    while (reader.Read()) 
                    {
                        mentorList.Add(new MentorDTO() {
                            
                            ID = Convert.ToInt32(reader["ID"]),
                            Name = reader["Mentor_Name"].ToString(),
                            Position = reader["Mentor_Position"].ToString(),
                            ImagePath = reader["ImagePath"].ToString()
                        });
                    }
                    return mentorList;
                }
            }
            catch (Exception ex)
            {
                Exception = ex.Message;
                throw;
            }
        }

        public MentorDTO AddMentor(MentorDTO add)
        {
            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_Mentor"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "AddMentor");

                    db.AddInParameter(command, "@Name", DbType.String, add.Name);
                    db.AddInParameter(command, "@Position", DbType.String, add.Position);
                    db.AddInParameter(command, "@ImagePath", DbType.String, add.ImagePath);

                    db.ExecuteNonQuery(command);

                    add.Messege = "Mentor Added Successfully";
                    add.Code = 200;
                    return add;
                }
            }
            catch (Exception ex) 
            {
                Exception = ex.Message;
                errorLog("Add Mento", Exception);
                throw;
            }
        }

        public MentorDTO UpdateMentor(MentorDTO update)
        {
            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_Mentor"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "UpdateMentor");

                    db.AddInParameter(command, "@Id", DbType.String, update.ID);
                    db.AddInParameter(command, "@Name", DbType.String, update.Name);
                    db.AddInParameter(command, "@Position", DbType.String, update.Position);
                    db.AddInParameter(command, "@ImagePath", DbType.String, update.ImagePath);

                    db.ExecuteNonQuery(command);

                    update.Messege = "Mentor Updated Successfully";
                    update.Code = 200;
                    return update;
                }
            }
            catch (Exception ex)
            {
                Exception = ex.Message;
                throw;
            }
        }

        public void DeleteMentor(int id)
        {
            try 
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_Mentor"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "DeleteMentor");
                    db.AddInParameter(command, "@Id", DbType.Int64, id);

                    db.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                Exception = ex.Message;
                throw;
            }
        }
    }
}
