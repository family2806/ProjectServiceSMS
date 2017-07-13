using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMSProcess.ServiceVietinbank;
using LibData.Models.Vietinbank;
using System.Xml.Serialization;
using System.IO;
using System.Web;
using LibData;
using LibData.Provider;
using LibData.Bussiness;
using System.Threading;

namespace SMSProcess.Bussiness
{
    class ModelCallService
    {
        public int NOSMS { get; set; }
        public string SMSLIST { get; set; }
    }
    class ConnectServiceVietinbank
    {
        private static readonly ILog logger = LogManager.GetLogger("ConnectServiceVietinbank");
        public void CallServiceOPT(object obj)
        {
            string tranid = "";
            try
            {

                ModelCallService input = (ModelCallService)obj;
                VB2B_SMSSoapClient client = new VB2B_SMSSoapClient();
                SMSRQ model = new SMSRQ()
                {
                    HEADER = new HEADERRQ()
                    {
                        SOURCE = "HUNGTHINH",
                        DEST = "VIETINBANK",
                        TRANSID = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(0,9999).ToString("0000"),
                        TRANSTIME = DateTime.Now.ToString("yyyyMMddHHmmss"),
                        USER = ConfigType.HungThinhUser,
                        PWD = ConfigType.HungThinhPassword
                    },
                    DATA = new DATARQ()
                    {
                        NOSMS = input.NOSMS.ToString(),
                        SMSLIST = input.SMSLIST
                    }
                };
                tranid = model.HEADER.TRANSID;
                string xmlreq = ConvertXML.ModelToXMLString<SMSRQ>(model);
                LogCallVTBProvider provider = new LogCallVTBProvider();
                LogCallVTB logModel = new LogCallVTB()
                {
                    Request = xmlreq,
                };
                xmlreq = HttpUtility.HtmlEncode(xmlreq);
                //logger.Info("Start CallServiceOPT");
                logModel.Response = HttpUtility.HtmlDecode(client.ReceiveMTResOTP(xmlreq));
                //logger.Info("End CallServiceOPT");
                provider.Insert(logModel);
                logger.Info(string.Format("CallVTB: {0} | {1}", logModel.Request, logModel.Response));
            }
            catch(Exception ex)
            {
                logger.Error("tranid : "+ tranid, ex);
                Thread.Sleep(60000);
                CallServiceOPT(obj);
            }
        }

        public void CallServiceBDSD(object obj)
        {
            string tranid = "";
            try
            {
                ModelCallService input = (ModelCallService)obj;
                VB2B_SMSSoapClient client = new VB2B_SMSSoapClient();

                SMSRQ model = new SMSRQ()
                {
                    HEADER = new HEADERRQ()
                    {
                        SOURCE = "HUNGTHINH",
                        DEST = "VIETINBANK",
                        TRANSID = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(0, 9999).ToString("0000"),
                        TRANSTIME = DateTime.Now.ToString("yyyyMMddHHmmss"),
                        USER = ConfigType.HungThinhUser,
                        PWD = ConfigType.HungThinhPassword
                    },
                    DATA = new DATARQ()
                    {
                        NOSMS = input.NOSMS.ToString(),
                        SMSLIST = input.SMSLIST
                    }
                };
                tranid = model.HEADER.TRANSID;
                string xmlreq = ConvertXML.ModelToXMLString<SMSRQ>(model);
                LogCallVTBProvider provider = new LogCallVTBProvider();
                LogCallVTB logModel = new LogCallVTB()
                {
                    Request = xmlreq,
                };
                xmlreq = HttpUtility.HtmlEncode(xmlreq);
                //logger.Info("Start CallServiceBDSD");
                logModel.Response = HttpUtility.HtmlDecode(client.ReceiveMTResBDSD(xmlreq));
                //logger.Info("End CallServiceBDSD");
                provider.Insert(logModel);
                logger.Info(string.Format("CallVTB: {0} | {1}", logModel.Request, logModel.Response));
            }
            catch (Exception ex)
            {
                logger.Error("tranid : " + tranid, ex);
                logger.Error(ex);
                Thread.Sleep(60000);
                CallServiceBDSD(obj);
            }
        }
    }
}
