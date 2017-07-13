using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace LibData.Provider
{
    public class QueueServiceProvider
    {
        private DBSMSGatewayDataContext db;
        private static ILog logger = LogManager.GetLogger("QueueServiceProvider");
        private const int MAX_TAKE = 3000;
        public QueueServiceProvider()
        {
            db = new DBSMSGatewayDataContext();
        }

        public List<QueueService> GetAll()
        {
            try
            {
                return db.QueueServices.ToList();
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                return null;
            }
        }

        public List<QueueService> GetByPriority(int Priority)
        {
            try
            {
                List<QueueService> result = new List<QueueService>();
                if(Priority == ConfigType.QueueService_Priority_OTP)
                {
                    result = db.QueueServices.Where(m => m.Priority == Priority).ToList();
                }
                else
                {
                    result = db.QueueServices.Where(m => m.Priority != ConfigType.QueueService_Priority_OTP).ToList();
                }
                if(result.Count > MAX_TAKE)
                {
                    return result.Take(MAX_TAKE).ToList();
                }
                else
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return new List<QueueService>();
            }
        }

        public bool Insert(QueueService model)
        {
            try
            {
                //if (db.CheckSMSID(model.SMSID) == 0)
                //{
                db.QueueServices.InsertOnSubmit(model);
                db.SubmitChanges();
                //}
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
                db.ExecuteCommand("DELETE FROM QueueService WHERE Id={0}", id);
            }
            catch(Exception ex)
            {
                logger.Error(ex);
            }
        }

    }
}
