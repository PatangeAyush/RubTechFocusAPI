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
    public class MagzineDAL:DALBASE
    {
        string Exception;
        public List<AdvertisingDTO> GetMagzine()
        {
            using (command = db.GetStoredProcCommand("SP_Magzine"))
            {
                db.AddInParameter(command, "@action", DbType.String, "GetMagzine");

                List<AdvertisingDTO> advertisingsList = new List<AdvertisingDTO>();

                IDataReader reader = db.ExecuteReader(command);

                while (reader.Read())
                {
                    advertisingsList.Add(new AdvertisingDTO()
                    {

                        Id = Convert.ToInt16(reader["ID"]),
                        ImagePath = reader["ImagePath"].ToString()
                    });
                }
                return advertisingsList;
            }
        }

        public AdvertisingDTO AddMagzine(AdvertisingDTO add)
        {
            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_Magzine"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "AddMagzine");
                    db.AddInParameter(command, "@imagepath", DbType.String, add.ImagePath);

                    db.ExecuteNonQuery(command);
                }
                return add;
            }
            catch (Exception ex)
            {
                Exception = ex.ToString();
                throw;
            }

        }

        public void DeleteMagzine(int id)
        {
            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_Magzine"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "DeleteMagzine");
                    db.AddInParameter(command, "@id", DbType.Int32, id);

                    db.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex) { Exception = ex.Message; throw; }
        }
    }
}

