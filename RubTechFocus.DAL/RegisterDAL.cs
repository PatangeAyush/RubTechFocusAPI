using RubTechFocus.DTO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RubTechFocus.DTO.RegisterDTO;

namespace RubTechFocus.DAL
{
    public class RegisterDAL:DALBASE
    {
        RegisterDTO response = new RegisterDTO();
        public RegisterDTO GetRegisteredUsers()
        {
            RegisterDTO response = new RegisterDTO();
            response.RegisterList = new List<RegisterEntity>();

            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_UserRegister"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "GetUser");

                    using (IDataReader reader = db.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {

                            RegisterEntity user = new RegisterEntity
                            {
                                Id = Convert.ToInt32(reader["ID"]),
                                name = reader["FullName"].ToString(),
                                company = reader["CompanyName"].ToString(),
                                email = reader["Email"].ToString(),
                                phoneNumber = reader["PhoneNumber"].ToString(),
                                magazine = reader["Magzine_Subscription"].ToString(),
                                duration = reader["Subscription_Duration"].ToString(),
                                Address = reader["Address"].ToString(),

                                City_State = reader["City_State"].ToString(),
                                Zip_Postal = reader["Zip_Postal"].ToString(),
                                Country = reader["Country"].ToString(),
                                Payment_Info = reader["Payment_Info"].ToString(),
                                Promotional_Preference = reader["Promotional_Preference"].ToString(),
                                IRMRA_Membership = reader["IRMRA_Membership"].ToString(),
                                User_Registration_Date = reader["User_Registration_Date"].ToString(),
                                Updated_Date = reader["Updated_Date"].ToString()
                            };

                            response.RegisterList.Add(user);
                        }
                    }
                }

                if (response.RegisterList.Count > 0)
                {
                    response.Code = (int)Errorcode.ErrorType.SUCESS;
                    response.Message = "Users retrieved successfully.";
                }

                else
                {
                    response.Code = 0;
                    response.Message = "No users found.";
                }
            }
            catch (Exception ex)
            {
                response.Code = (int)Errorcode.ErrorType.ERROR;
                response.Message = "Error occurred while retrieving users.";

                DALBASE res = new DALBASE();
                res.errorLog("Method Name: GetRegisteredUsers", ex.ToString());
            }

            return response;
        }

        public RegisterDTO AddUser(RegisterDTO.RegisterEntity add)
        {
            string exception = "";
            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_UserRegister"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "InsertUser");
                    db.AddInParameter(command, "@FullName", DbType.String, add.name);
                    db.AddInParameter(command, "@CompanyName", DbType.String, add.company);
                    db.AddInParameter(command, "@Email", DbType.String, add.email);
                    db.AddInParameter(command, "@PhoneNumber", DbType.String, add.phoneNumber);
                    db.AddInParameter(command, "@Magzine_Subscription", DbType.String, add.magazine);
                    db.AddInParameter(command, "@Subscription_Duration", DbType.String, add.duration);
                    db.AddInParameter(command, "@Address", DbType.String, add.Address);

                    db.AddInParameter(command, "@City_State", DbType.String, add.City_State);
                    db.AddInParameter(command, "@Zip_Postal", DbType.String, add.Zip_Postal);
                    db.AddInParameter(command, "@Country", DbType.String, add.Country);
                    db.AddInParameter(command, "@Payment_Info", DbType.String, add.Payment_Info);
                    db.AddInParameter(command, "@Promotional_Preference", DbType.String, add.Promotional_Preference);
                    db.AddInParameter(command, "@IRMRA_Membership", DbType.String, add.IRMRA_Membership);

                    db.ExecuteNonQuery(command);
                }

                response.Code = (int)Errorcode.ErrorType.SUCESS;
                response.Message = "User registered successfully";
            }
            catch (Exception ex)
            {
                exception = ex.ToString();
                response.Code = (int)Errorcode.ErrorType.ERROR;
                response.Message = "Error occurred while adding user";
                DALBASE res = new DALBASE();
                res.errorLog("Method Name: AddUser", exception);
            }

            return response;
        }

        public void UpdateUser(RegisterDTO.RegisterEntity update)
        {
            try 
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_UserRegister"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "UpdateUser");
                    db.AddInParameter(command, "@ID", DbType.Int32, update.Id);
                    db.AddInParameter(command, "@FullName", DbType.String, update.name);
                    db.AddInParameter(command, "@CompanyName", DbType.String, update.company);
                    db.AddInParameter(command, "@Email", DbType.String, update.email);
                    db.AddInParameter(command, "@PhoneNumber", DbType.String, update.phoneNumber);
                    db.AddInParameter(command, "@Magzine_Subscription", DbType.String, update.magazine);
                    db.AddInParameter(command, "@Subscription_Duration", DbType.String, update.duration);
                    db.AddInParameter(command, "@Address", DbType.String, update.Address);
                    db.AddInParameter(command, "@City_State", DbType.String, update.City_State);
                    db.AddInParameter(command, "@Zip_Postal", DbType.String, update.Zip_Postal);
                    db.AddInParameter(command, "@Country", DbType.String, update.Country);
                    db.AddInParameter(command, "@Payment_Info", DbType.String, update.Payment_Info);
                    db.AddInParameter(command, "@Promotional_Preference", DbType.String, update.Promotional_Preference);
                    db.AddInParameter(command, "@IRMRA_Membership", DbType.String, update.IRMRA_Membership);
                    db.AddInParameter(command, "@Updated_Date", DbType.DateTime, DateTime.Now);

                    db.ExecuteNonQuery(command);
                }
                response.Code = (int)Errorcode.ErrorType.SUCESS;
                response.Message = "User registered successfully";

            }
            catch (Exception ex)
            {
                DALBASE res = new DALBASE();
                res.errorLog("Method Name: DeleteEvent", ex.ToString());
            }
        }

        public void DeleteRegistredUser(int id)
        {
            try 
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_UserRegister"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "DeleteUser");
                    db.AddInParameter(command, "@Id", DbType.String, id);

                    db.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                DALBASE res = new DALBASE();
                res.errorLog("Method Name: DeleteEvent", ex.ToString());
            }
        }

    }
}
