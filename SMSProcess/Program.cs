using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using log4net;

namespace SMSProcess
{
    class Program
    {
        private static readonly ILog logger = LogManager.GetLogger("Program");
        static void Main(string[] args)
        {
            try
            {
                logger.Info("Start...");
                ThreadSendSMS sms = new ThreadSendSMS();
                Thread thSendSMS = new Thread(sms.SendSMSNormal);
                thSendSMS.Start();
                Thread thSendOTP = new Thread(sms.SendSMSOTP);
                thSendOTP.Start();
                Thread thCallVTB = new Thread(new ThreadCallVietinbank().AutoCallVietinbank);
                thCallVTB.Start();
                Thread thCallVTBOTP = new Thread(new ThreadCallVietinbank().AutoCallVietinbankOTP);
                thCallVTBOTP.Start();
                //===========================
                Thread thCallMTError = new Thread(new ThreadCallVietinbank().AutoInsertMTError);
                thCallMTError.Start();
                Thread thCallMTErrorOTP = new Thread(new ThreadCallVietinbank().AutoInsertMTErrorkOTP);
                thCallMTErrorOTP.Start();
                //=============================
                logger.Info("Start done.");
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                Thread.Sleep(1000);
                Main(args);
            }
            
        }

    }
}
