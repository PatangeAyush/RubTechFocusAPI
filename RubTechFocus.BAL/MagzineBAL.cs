using RubTechFocus.DAL;
using RubTechFocus.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubTechFocus.BAL
{
    public class MagzineBAL
    {
        MagzineDAL objadvertisingDAL = new MagzineDAL();
        public List<AdvertisingDTO> GetMagzine()
        {
            return objadvertisingDAL.GetMagzine();
        }
        public AdvertisingDTO AddMagzine(AdvertisingDTO add)
        {
            return objadvertisingDAL.AddMagzine(add);
        }
        public void DeleteMagzine(int id)
        {
            objadvertisingDAL.DeleteMagzine(id);
        }
    }
}
