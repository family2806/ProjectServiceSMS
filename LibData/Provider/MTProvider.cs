using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibData.Provider
{
    public class MTProvider
    {
        private DBSMSGatewayDataContext db;
        private const int MAX_TAKE = 6000;
        private static ILog logger = LogManager.GetLogger("MTProvider");

        public MTProvider()
        {
            db = new DBSMSGatewayDataContext();
        }

        public List<MT> GetAllByStatusAndProcessingCode(int status, string ProcessingCode)
        {
            try
            {
                List<MT> list;
                if(ProcessingCode.Equals(ConfigType.QueueService_ProcessingCode_OTP))
                {
                    list = db.MTs.Where(m => m.Status == status && m.ProcessingCode.Equals(ProcessingCode)).ToList();
                }
                else
                {
                    list = db.MTs.Where(m => m.Status == status && !m.ProcessingCode.Equals(ConfigType.QueueService_ProcessingCode_OTP)).ToList();
                }
                if (list.Count() > MAX_TAKE)
                {
                    list = db.MTs.Where(m => m.Status == status).Take(MAX_TAKE).ToList();
                }
                return list;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return new List<MT>();
            }
        }

        public bool Insert(MT model)
        {
            try
            {
                if (db.CheckSMSID(model.SMSID) == 0)
                {
                    var modeltelco = new SubTelcoProvider().getSubTelcoByDest(model.Receiver);
                    int IdTelco = 0;
                    if (modeltelco != null)
                    {
                        IdTelco = modeltelco.IdTelco.Value;
                    }
                    model.IdTelco = IdTelco;
                    db.MTs.InsertOnSubmit(model);
                    db.SubmitChanges();
                }
                else
                {
                    db.ExecuteCommand("UPDATE MT SET Status = {0} WHERE SMSID = {1}", ConfigType.MT_STATUS_NOT_NOTIFY, model.SMSID);
                }
                return true;
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                return false;
            }
        }

        public bool ChangeStatus(MT model)
        {
            try
            {
                db.ExecuteCommand("UPDATE MT SET Status = {0} WHERE ID = {1}",model.Status,model.Id);
                return true;
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                return false;
            }
        }
       
    }
}
