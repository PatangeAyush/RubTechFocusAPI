using RubTechFocus.DAL;
using RubTechFocus.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubTechFocus.BAL
{
    public class ReportBAL
    {
        ReportDAL objreportDAL = new ReportDAL();

        public List<ReportDTO> GetReport()
        {
            return objreportDAL.GetReport();
        }
        public ReportDTO GetReportByID(int id)
        {
            return objreportDAL.GetReportByID(id);
        }
        public ReportDTO AddReport(ReportDTO add)
        {
            return objreportDAL.AddReport(add);
        }

        public ReportDTO UpdateReport(ReportDTO report)
        {
            return objreportDAL.UpdateReport(report);
        }

        public void DeleteReport(int id)
        {
            objreportDAL.DeleteReport(id);
        }
    }
}
