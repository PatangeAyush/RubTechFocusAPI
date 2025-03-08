using RubTechFocus.DAL;
using RubTechFocus.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubTechFocus.BAL
{
    public class MessegeBAL
    {
        MessegeDAL messegeDAL = new MessegeDAL();

        public List<MessegeDTO> GetMessege()
        {
            return messegeDAL.GetMessege();
        }

        public MessegeDTO AddMessege(MessegeDTO messege)
        {
            return messegeDAL.AddMessege(messege);
        }

        public MessegeDTO UpdateMessege(MessegeDTO messege)
        {
            return messegeDAL.UpdateMessege(messege);
        }

        public void DeleteMessege(int id)
        {
            messegeDAL.DeleteMessege(id);
        }
    }
}
