using RubTechFocus.DAL;
using RubTechFocus.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubTechFocus.BAL
{
    public class GalleryReportBAL
    {
        GalleryReportDAL objgalleryreportDAL = new GalleryReportDAL();

        public List<GalleryReportDTO> GetReports()
        { 
            return objgalleryreportDAL.GetReports();
        }

        public GalleryReportDTO AddGalleryReport(GalleryReportDTO add)
        {
            return objgalleryreportDAL.AddGalleryReport(add);
        }
        public void AddReportSingleImage(int id, string ImagePath)
        {
            objgalleryreportDAL.AddReportSingleImage(id, ImagePath);
        }
        public void RenameReport(int id, string reportName)
        {
            objgalleryreportDAL.RenameReport(id, reportName);
        }

        public void DeleteReport(int id)
        {
            objgalleryreportDAL.DeleteReport(id);
        }

        public void DeleteReportImage(string ImagePath)
        {
            objgalleryreportDAL.DeleteReportImage(ImagePath);
        }
    }
}
