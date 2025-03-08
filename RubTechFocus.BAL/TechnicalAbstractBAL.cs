using RubTechFocus.DAL;
using RubTechFocus.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubTechFocus.BAL
{
    public class TechnicalAbstractBAL
    {
        TechnicalAbstractDAL objTechnicalAbstract = new TechnicalAbstractDAL();

        public List<TechnicalAbstractDTO> GetTechnicalAbstracts()
        {
            return objTechnicalAbstract.GetTechnicalAbstracts();
        }

        public TechnicalAbstractDTO AddTechnicalAbstract(TechnicalAbstractDTO model)
        {
            return objTechnicalAbstract.AddTechnicalAbstract(model);
        }
        public TechnicalAbstractDTO UpdateTechnicalAbstract(TechnicalAbstractDTO model)
        {
            return objTechnicalAbstract.UpdateTechnicalAbstract(model);
        }
        public TechnicalAbstractDTO DeleteTechnicalAbstract(int id)
        {
            return objTechnicalAbstract.DeleteTechnicalAbstract(id);
        }
    }
}
