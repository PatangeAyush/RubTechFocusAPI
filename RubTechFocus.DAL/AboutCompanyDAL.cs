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
    public class AboutCompanyDAL:DALBASE
    {
        string Exception;

        public List<AboutCompanyDTO> GetAboutCompany()
        {
            using (command = db.GetStoredProcCommand("SP_AboutCompany"))
            {
                db.AddInParameter(command, "@action", DbType.String, "GetAboutCompany");

                List<AboutCompanyDTO> companyList = new List<AboutCompanyDTO>();
                IDataReader reader = db.ExecuteReader(command);

                while (reader.Read())
                {
                    companyList.Add(new AboutCompanyDTO()
                    {
                        CompanyAddress = reader["CompanyAddress"].ToString()
                    });
                }
                return companyList;
            }
        }

        public AboutCompanyDTO AddCompanyAddress(AboutCompanyDTO aboutCompany)
        {
            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_AboutCompany"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "AddCompanyAddress");
                    db.AddInParameter(command, "@companyAddress", DbType.String, aboutCompany.CompanyAddress);
                    db.ExecuteNonQuery(command);
                }
                return aboutCompany;
            }
            catch (Exception ex)
            {
                Exception = ex.ToString();
                throw;
            }
        }

        public AboutCompanyDTO UpdateCompanyAddress(AboutCompanyDTO aboutCompany)
        {
            try
            {
                using (DbCommand command = db.GetStoredProcCommand("SP_AboutCompany"))
                {
                    db.AddInParameter(command, "@action", DbType.String, "UpdateCompany");
                    db.AddInParameter(command, "@companyAddress", DbType.String, aboutCompany.CompanyAddress);
                    db.ExecuteNonQuery(command);
                }
                return aboutCompany;
            }
            catch (Exception ex)
            {
                Exception = ex.ToString();
                throw;
            }
        }
    }
}
