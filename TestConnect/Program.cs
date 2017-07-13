using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LibData.Provider;
using TestConnect.ServiceVNET;
using SMSProcess.Bussiness;

namespace TestConnect
{
    class Program
    {
        private static string username = "hungthinh";
        private static string password = "h1T234#h";
        private static string src = "VietinBank";
        static void Main(string[] args)
        {
            try
            {
                //RouteTelcoProvider routeProvider = new RouteTelcoProvider();
                //var model= routeProvider.getRoute(1, false);
                Console.WriteLine("Run Test...");
                Console.WriteLine("Send...");
                //SMSProcess.ServiceVNet.smsapiClient client = new SMSProcess.ServiceVNet.smsapiClient();
                // long result = client.Sent(username, password, src, "84924841036", "From Feb 7, 2017, your account numbers will be changed to 2313111. Thank you for your trust and support for VietinBank");
                //  client.Close();
                //string result = ConnectSOUTHtelecom.SendSMSBrandName("84924841036", "So du tai khoan cua ban la 12.213.123 VND", "VietinBank");
                int result = ConnectVNTP.SendSMS("841297327617", "From Feb 7, 2017, your account numbers will be changed to 2313111. Thank you for your trust and support for VietinBank");
                Console.WriteLine("result: " + result);
            }
            catch (Exception ex)
            {

            }
            Console.ReadLine();

        }
    }
}
