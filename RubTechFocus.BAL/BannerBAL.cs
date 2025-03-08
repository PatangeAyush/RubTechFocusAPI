using RubTechFocus.DAL;
using RubTechFocus.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubTechFocus.BAL
{
    public class BannerBAL
    {
        BannerDAL objBannerDAL = new BannerDAL();

        public List<BannerDTO> GetBanner()
        {
            return objBannerDAL.GetBanner();
        }

        public BannerDTO AddBanner(BannerDTO banner)
        {
            return objBannerDAL.AddBanner(banner);
        }

        public void UpdateBanner(BannerDTO banner)
        {
            objBannerDAL.UpdateBanner(banner);
        }

        public void DeleteBanner(int id)
        {
            objBannerDAL.DeleteBanner(id);
        }
    }

}
