using RubTechFocus.DAL;
using RubTechFocus.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubTechFocus.BAL
{
    public class MentorBAL
    {
        MentorDAL objmentorDAL = new MentorDAL();
        public List<MentorDTO> GetMentor()
        {
            return objmentorDAL.GetMentor();
        }

        public MentorDTO AddMentor(MentorDTO add)
        {
            return objmentorDAL.AddMentor(add);
        }
        public MentorDTO UpdateMentor(MentorDTO update)
        {
            return objmentorDAL.UpdateMentor(update);
        }
        public void DeleteMentor(int id)
        {
            objmentorDAL.DeleteMentor(id);
        }
    }
}
