using RubTechFocus.DAL;
using RubTechFocus.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubTechFocus.BAL
{
    public class AboutCompanyBAL
    {
        AboutCompanyDAL aboutCompanyDAL = new AboutCompanyDAL();

        public List<AboutCompanyDTO> GetAboutCompany()
        {
            return aboutCompanyDAL.GetAboutCompany();
        }

        public AboutCompanyDTO AddCompanyAddress(AboutCompanyDTO aboutCompany)
        {
            return aboutCompanyDAL.AddCompanyAddress(aboutCompany);
        }

        public AboutCompanyDTO UpdateCompanyAddress(AboutCompanyDTO aboutCompany)
        {
            return aboutCompanyDAL.UpdateCompanyAddress(aboutCompany);
        }
    }
}
