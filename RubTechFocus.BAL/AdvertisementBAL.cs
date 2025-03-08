using RubTechFocus.DAL;
using RubTechFocus.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubTechFocus.BAL
{
    public class AdvertisementBAL
    {
        AdvertisementDAL objadvertisingDAL = new AdvertisementDAL();
        public List<AdvertisingDTO> GetAdvertisement()
        {
            return objadvertisingDAL.GetAdvertisement();
        }
        public AdvertisingDTO AddAdvertisement(AdvertisingDTO add)
        {
            return objadvertisingDAL.AddAdvertisement(add);
        }
        public void DeleteAdvertisement(int id)
        {
            objadvertisingDAL.DeleteAdvertisement(id);
        }
    }
}
