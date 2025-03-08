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
    public class AdvertisementDAL:DALBASE
    {
        string Exception;
        public List<AdvertisingDTO> GetAdvertisement()
        {
            using (command = db.GetStoredProcCommand("SP_Advertisement"))
            {
                db.AddInParameter(command, "@action", DbType.String, "GetAdvertisment");

                List<AdvertisingDTO> advertisingsList = new List<AdvertisingDTO>();

                IDataReader reader = db.ExecuteReader(command);

                while (reader.Read()) 
                {
                    advertisingsList.Add(new AdvertisingDTO() { 

                        Id = Convert.ToInt16(reader["ID"]),
                        ImagePath = reader["ImagePath"].ToString()
                    });
                }
                return advertisingsList;
            }
        }

        public AdvertisingDTO AddAdvertisement(AdvertisingDTO add)
        {
            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_Advertisement"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "AddAdvertisement");
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

        public void DeleteAdvertisement(int id)
        {
            try 
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_Advertisement"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "DeleteAdvertisement");
                    db.AddInParameter(command, "@id", DbType.Int32, id);

                    db.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex){ Exception = ex.Message; throw; }
        }
    }
}
