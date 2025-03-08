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
    public class ContactDAL:DALBASE
    {
        ContactDTO objcontactDTO = new ContactDTO();

        string Exception = "";
        //public List<ContactDTO> GetContacts()
        //{
        //    using (DbCommand command = db.GetStoredProcCommand("SP_Contact"))
        //    {
        //        db.AddInParameter(command, "@action", DbType.String, "GetContacts");

        //        List<ContactDTO> contactList = new List<ContactDTO>();

        //        using (IDataReader reader = db.ExecuteReader(command))
        //        {
        //            while (reader.Read())
        //            {
        //                contactList.Add(new ContactDTO
        //                {
        //                    Id = Convert.ToInt32(reader["Id"]),
        //                    Contact = reader["Contact"].ToString(),
        //                    Alternative_Contact = reader["Alternative_Contact"].ToString(),
        //                    Email = reader["Email"].ToString(),
        //                    Address = reader["Address"].ToString(),
        //                    Created_Date = Convert.ToDateTime(reader["Created_Date"]),
        //                    Updated_Date = Convert.ToDateTime(reader["Updated_Date"]) 
        //                });
        //            }
        //        }
        //        return contactList;
        //    }
        //}

        public List<ContactDTO> GetContacts()
        {
            using (DbCommand command = db.GetStoredProcCommand("SP_Contact"))
            {
                db.AddInParameter(command, "@action", DbType.String, "GetContacts");

                List<ContactDTO> contactList = new List<ContactDTO>();

                using (IDataReader reader = db.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        contactList.Add(new ContactDTO
                        {
                            Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0,
                            Contact = reader["Contact"] != DBNull.Value ? reader["Contact"].ToString() : string.Empty,
                            Alternative_Contact = reader["Alternative_Contact"] != DBNull.Value ? reader["Alternative_Contact"].ToString() : string.Empty,
                            Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : string.Empty,
                            Address = reader["Address"] != DBNull.Value ? reader["Address"].ToString() : string.Empty,
                            Created_Date = reader["Created_Date"] != DBNull.Value ? Convert.ToDateTime(reader["Created_Date"]) : (DateTime?)null,
                            Updated_Date = reader["Updated_Date"] != DBNull.Value ? Convert.ToDateTime(reader["Updated_Date"]) : (DateTime?)null
                        });
                    }
                }
                return contactList;
            }
        }

        public void AddContact(ContactDTO add)
        {
            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_Contact"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "AddContact");
                    db.AddInParameter(command, "@Contact", DbType.String, add.Contact);
                    db.AddInParameter(command, "@alternativeContact", DbType.String, add.Alternative_Contact);
                    db.AddInParameter(command, "@email", DbType.String, add.Email);
                    db.AddInParameter(command, "@address", DbType.String, add.Address);

                    db.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                Exception = ex.ToString();
                add.Code = (int)Errorcode.ErrorType.ERROR;
                add.Messege = "Error occurred while adding event";
                DALBASE res = new DALBASE();
                res.errorLog("Method Name: AddEvent", Exception);
            }
        }

        public ContactDTO UpdateContact(ContactDTO update)
        {
            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_Contact"))
                {
                   
                    db.AddInParameter(command, "@action", DbType.String, "UpdateContact");
                    db.AddInParameter(command, "@Id", DbType.Int32, update.Id);
                    db.AddInParameter(command, "@Contact", DbType.String, update.Contact);
                    db.AddInParameter(command, "@alternativeContact", DbType.String, update.Alternative_Contact);
                    db.AddInParameter(command, "@email", DbType.String, update.Email);
                    db.AddInParameter(command, "@address", DbType.String, update.Address);

                    db.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                Exception = ex.ToString();
                update.Code = (int)Errorcode.ErrorType.ERROR;
                update.Messege = "Error occurred while adding event";
                DALBASE res = new DALBASE();
                res.errorLog("Method Name: AddEvent", Exception);
            }
            return update;
        }

        public void DeleteContact(int id)
        {
            try 
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_Contact"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "DeleteContact");
                    db.AddInParameter(command, "@Id", DbType.Int32,id);
                    db.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                Exception = ex.ToString();
                DALBASE res = new DALBASE();
                res.errorLog("Method Name: AddEvent", Exception);
                throw;
                //update.Code = (int)Errorcode.ErrorType.ERROR;
                //update.Messege = "Error occurred while adding event";
              
            }
        }
    }

}
