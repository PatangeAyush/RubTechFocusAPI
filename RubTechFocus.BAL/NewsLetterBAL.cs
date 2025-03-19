using RubTechFocus.DAL;
using RubTechFocus.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubTechFocus.BAL
{
    public class NewsLetterBAL
    {
        NewsLetterDAL objNewsLetterDAL = new NewsLetterDAL();
        public List<NewsLetterDTO> GetNewsLetter()
        {
            return objNewsLetterDAL.GetNewsLetter();
        }

        public void AddNewsLetter(string email)
        {
            objNewsLetterDAL.AddNewsLetter(email);
        }
    }
}
