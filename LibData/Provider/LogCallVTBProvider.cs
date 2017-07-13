using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace LibData.Provider
{
    public class LogCallVTBProvider
    {
        private DBSMSGatewayDataContext db;
        private static ILog logger = LogManager.GetLogger("LogCallVTBProvider");
        public LogCallVTBProvider()
        {
            db = new DBSMSGatewayDataContext();
        }

        public bool Insert(LogCallVTB model)
        {
            try
            {
                model.CreateDate = DateTime.Now;
                db.LogCallVTBs.InsertOnSubmit(model);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return false;
            }
        }
    }
}
