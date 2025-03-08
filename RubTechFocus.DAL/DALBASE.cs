using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace RubTechFocus.DAL
{
    public class DALBASE
    {

        public Database db = null;
        public DbCommand command = null;


        public DALBASE()
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            db = factory.Create("Constr");

        }
        public void errorLog(string MethodName, string Message)
        {
            using (command = db.GetStoredProcCommand("sp_tblErrorLog"))
            {
                db.AddInParameter(command, "@MethodName", DbType.String, MethodName);
                db.AddInParameter(command, "@ErrorMsg", DbType.String, Message);

                try
                {
                    IDataReader reader = db.ExecuteReader(command);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        private bool disposed = false;

        public void Disposed()
        {
            Dispose(true);
            GC.SuppressFinalize(this);


        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {

                }

                if (db != null)
                    db = null;
                disposed = true;
            }
        }

        ~DALBASE()
        {

            Dispose(false);
        }
    }
}

