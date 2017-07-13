using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibData.Provider
{
    public class MTQueueReportProvider
    {
        private DBSMSGatewayDataContext db;
        private const int MAX_TAKE = 6000;
        private static ILog logger = LogManager.GetLogger("MTQueueReportProvider");

        public MTQueueReportProvider()
        {
            db = new DBSMSGatewayDataContext();
        }


        public List<MTQueueReport> GetAllByStatusAndProcessingCode(int status, string ProcessingCode)
        {
            try
            {
                List<MTQueueReport> list;
                if(ProcessingCode.Equals(ConfigType.QueueService_ProcessingCode_OTP))
                {
                    list = db.MTQueueReports.Where(m => m.Status == status && m.ProcessingCode.Equals(ProcessingCode)).ToList();
                }
                else
                {
                    list = db.MTQueueReports.Where(m => m.Status == status && !m.ProcessingCode.Equals(ConfigType.QueueService_ProcessingCode_OTP)).ToList();
                }
                if (list.Count() > MAX_TAKE)
                {
                    list = db.MTQueueReports.Where(m => m.Status == status).Take(MAX_TAKE).ToList();
                }
                return list;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return new List<MTQueueReport>();
            }
        }

        public bool Insert(MTQueueReport model)
        {
            try
            {
                if (db.CheckSMSID(model.SMSID) == 0)
                {
                    db.MTQueueReports.InsertOnSubmit(model);
                    db.SubmitChanges();
                }
                else
                {
                    db.ExecuteCommand("UPDATE MTQueueReport SET Status = {0} WHERE SMSID = {1}", ConfigType.MT_STATUS_NOT_NOTIFY, model.SMSID);
                }
                return true;
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                return false;
            }
        }

        public bool ChangeStatus(MTQueueReport model)
        {
            try
            {
                db.ExecuteCommand("UPDATE MTQueueReport SET Status = {0} WHERE ID = {1}", model.Status,model.Id);
                return true;
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                return false;
            }
        }
        public void DeleteById(int id)
        {
            try
            {
                db.ExecuteCommand("DELETE FROM MTQueueReport WHERE Id={0}", id);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

    }
}
