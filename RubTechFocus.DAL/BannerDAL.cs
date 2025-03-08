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
    public class BannerDAL : DALBASE
    {

        string Exception;

        public List<BannerDTO> GetBanner()
        {
            using (command = db.GetStoredProcCommand("SP_Banner"))
            {
                db.AddInParameter(command, "@action", DbType.String, "GetBanner");

                List<BannerDTO> bannerList = new List<BannerDTO>();
                IDataReader reader = db.ExecuteReader(command);

                while (reader.Read())
                {
                    bannerList.Add(new BannerDTO()
                    {
                        Id = Convert.ToInt32(reader["ID"]),
                        ImagePath = reader["ImagePath"].ToString()
                    });
                }
                return bannerList;
            }
        }

        public BannerDTO AddBanner(BannerDTO banner)
        {
            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_Banner"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "AddBanner");
                    db.AddInParameter(command, "@ImagePath", DbType.String, banner.ImagePath);

                    db.ExecuteNonQuery(command);
                }
                return banner;
            }
            catch (Exception ex)
            {
                Exception = ex.ToString();
                throw;
            }
        }

        public void UpdateBanner(BannerDTO banner)
        {
            using (DbCommand command = db.GetStoredProcCommand("SP_Banner"))
            {
                db.AddInParameter(command, "@action", DbType.String, "UpdateBanner");
                db.AddInParameter(command, "@Id", DbType.Int32, banner.Id);
                db.AddInParameter(command, "@ImagePath", DbType.String, banner.ImagePath);

                db.ExecuteNonQuery(command);
            }
        }

        public void DeleteBanner(int id)
        {
            using (DbCommand command = db.GetStoredProcCommand("SP_Banner"))
            {
                db.AddInParameter(command, "@action", DbType.String, "DeleteBanner");
                db.AddInParameter(command, "@Id", DbType.Int32, id);

                db.ExecuteNonQuery(command);
            }
        }
    }


}
