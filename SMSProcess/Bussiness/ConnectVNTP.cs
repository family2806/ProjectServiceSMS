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
    public class ConnectVNTP
    {
        private static string username = "vpmedia-sms";
        private static string password = "cb3101b79d39b6ed1c7e3ba9a086e3e9";

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static int SendSMS(string phone, string content)
        {
            try
            {
                content = ConvertVietChar.convertToUnSign2(content);
                bool isUnicode = ConvertVietChar.ContainsUnicodeCharacter(content);
                ServiceVNTP.BrandNameWSClient client = new ServiceVNTP.BrandNameWSClient();
                long result = client.uploadSMS(username, password, "VietinBank", phone, "0", "0", content);
                client.Close();
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
