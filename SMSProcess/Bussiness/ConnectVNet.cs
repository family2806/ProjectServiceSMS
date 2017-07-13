using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using SMSProcess.ServiceVNet;

namespace SMSProcess.Bussiness
{
    public class ConnectVNet
    {
        private static string username = "hungthinh";
        private static string password = "h1T234#h";
        private static string src = "VietinBank";
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static int SendSMS(string phone, string content)
        {
            try
            {
                content = ConvertVietChar.convertToUnSign2(content);
                smsapiClient client = new smsapiClient();
                long result = client.Sent(username, password, src, phone, content);
                client.Close();
                // 0 success
                return (int)result;
            }
            catch(Exception ex)
            {
                log.Error(ex);
                return -100;
            }
        }

    }
}
