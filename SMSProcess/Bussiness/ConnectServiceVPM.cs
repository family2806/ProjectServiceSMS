using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using SMSProcess.ServiceVPM;

namespace SMSProcess.Bussiness
{
    public class ConnectServiceVPM
    {
        private static readonly ILog logger = LogManager.GetLogger("ConnectServiceVPM");
        private const string USER_NAME = "tuananh";
        private const string PASSWORD = "abc@123#";
        private static readonly List<string> listPhone = new List<string>()
        {
            "84987281090",
        };
        public static int SendSMS(string src, string dest, string msgbody)
        {
            try
            {
                if(listPhone.Contains(dest))
                {
                    string requestid = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(0, 999999).ToString("000000");
                    GatewaySoapClient client = new GatewaySoapClient();
                    return client.SendMT(src, dest, msgbody, requestid, USER_NAME, PASSWORD);
                }
                return -202;
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                return -101;
            }
        }
    }
}
