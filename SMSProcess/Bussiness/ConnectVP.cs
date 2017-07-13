using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMSProcess.ServiceVP;
using log4net;

namespace SMSProcess.Bussiness
{
   public  class ConnectVP
    {
        private static string username = "hungthinh";
        private static string password = "awd62ma5";
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static int SendSMS(string phone, string content)
        {
            try
            {
                
                string requestid = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(0, 999999).ToString("000000");

                GatewaySoapClient client = new GatewaySoapClient();

                int result = client.SendMT("VietinBank", phone, content, requestid, username, password);

                client.Close();
                // 0 success
                return (int)result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return -100;
            }
        }
    }
}
