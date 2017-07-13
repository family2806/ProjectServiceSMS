using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Net;
using System.IO;
using SMSProcess.Models;

namespace SMSProcess.Bussiness
{
    public class ConnectSOUTHtelecom
    {

        private static string URL = "http://api-02.worldsms.vn/webapi/sendSMS";
        private static string URLBackUP = "http://api-01.worldsms.vn/webapi/sendSMS";

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
     
        public static string SendSMSBrandName(string phone, string content, string brandname)
        {
            try
            {
                JsonSOUTHtelecom model = new JsonSOUTHtelecom
                {
                    from = brandname,
                    to = phone,
                    text = content,
                };
                var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string body = javaScriptSerializer.Serialize(model);
                string jsonResult = ConnectURLByPostBrandName(URL, body);
                List<string> Result = jsonResult.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                return Result[1].Replace("}", "");
            }
            catch (Exception ex1)
            {
                log.Error(ex1);
                try
                {
                    content = ConvertVietChar.convertToUnSign2(content);
                    JsonSOUTHtelecom model = new JsonSOUTHtelecom
                    {
                        from = brandname,
                        to = phone,
                        text = content,
                    };
                    var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string body = javaScriptSerializer.Serialize(model);
                    string jsonResult = ConnectURLByPostBrandName(URLBackUP, body);
                    List<string> Result = jsonResult.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    return Result[1].Replace("}", "");
                }
                catch (Exception ex2)
                {
                    log.Error(ex2);
                    return "error";
                }
            }
        }
      
        public static string ConnectURLByPostBrandName(string url, string body)
        {
            try
            {
                string resut = "";
                string contentType = "application/json";
                HttpWebRequest request = WebRequest.CreateHttp(url);
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                request.Method = "POST";
                byte[] bytesBody = Encoding.UTF8.GetBytes(body);
                request.ContentLength = bytesBody.Length;
                request.ContentType = contentType;
                request.Headers["Authorization"] = "Basic aHVuZ3RoaW5oOnVLeHY1Qms3S3Q=";
                request.Accept = "Accept=application/json";
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(bytesBody, 0, bytesBody.Length);
                dataStream.Close();
                StreamReader reader;
                WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);
                resut = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
                return resut;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return "";
            }
        }
    }
}
