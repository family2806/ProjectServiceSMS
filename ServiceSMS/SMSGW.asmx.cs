using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using LibData.Models;
using System.Xml.Serialization;
using System.IO;
using log4net;
using LibData;
using LibData.Provider;
using LibData.Bussiness;

namespace ServiceSMS
{
    /// <summary>
    /// Summary description for SMSGW
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SMSGW : System.Web.Services.WebService
    {
        private static ILog logger = LogManager.GetLogger("SMSGW");
        [WebMethod]
        public string SendMT(string xmlreq)
        {
            logger.Info("xmlreq befor:" + xmlreq);
            xmlreq = Server.HtmlDecode(xmlreq);
            logger.Info("xmlreq after:" + xmlreq);
            SMSRS response = new SMSRS();
            try
            {
                SMSRQ request = ConvertXML.XMLToModel<SMSRQ>(xmlreq);
                response.HEADER = new HEADERRES()
                {
                    DEST = request.HEADER.DEST,
                    PWD = request.HEADER.PWD,
                    SOURCE = request.HEADER.SOURCE,
                    TRANSID = request.HEADER.TRANSID,
                    TRANSTIME = request.HEADER.TRANSTIME,
                    USER = request.HEADER.USER,
                };
                if (ValidateUser.CheckUser(request.HEADER.USER, request.HEADER.PWD))
                {
                    DateTime transTime = DateTime.ParseExact(request.HEADER.TRANSTIME, "yyyyMMddHHmmss", null);
                    QueueServiceProvider provider = new QueueServiceProvider();
                    EncryptAndDecrypt ead = new EncryptAndDecrypt();
                    foreach (var item in request.DATA.SMS)
                    {
                        QueueService model = new QueueService()
                        {
                            Content = item.CONTENT,
                            Dest = request.HEADER.DEST,
                            Password = request.HEADER.PWD,
                            Priority = item.PRIORITY,
                            ProcessingCode = item.PROCESSINGCODE,
                            Receiver = item.RECEIVER,
                            Source = request.HEADER.SOURCE,
                            TransID = request.HEADER.TRANSID,
                            TransTime = transTime,
                            DateCreate = DateTime.Now,
                            User = request.HEADER.USER,
                            SMSID = item.SMSID
                        };
                        provider.Insert(model);
                    }
                    response.DATA = new DATARES()
                    {
                        ERROR = new ERRORRES()
                        {
                            ERRCODE = ConfigType.RS_SUCCESS,
                            ERRDESC = ConfigType.RS_SUCCESS_MESS
                        }
                    };
                }
                else
                {
                    response.DATA = new DATARES()
                    {
                        ERROR = new ERRORRES()
                        {
                            ERRCODE = ConfigType.RS_PASSWORD_FAIL,
                            ERRDESC = ConfigType.RS_PASSWORD_FAIL_MESS
                        }
                    };
                }
            }
            catch(Exception ex)
            {
                response.DATA = new DATARES()
                {
                    ERROR = new ERRORRES()
                    {
                        ERRCODE = ConfigType.RS_SYSTEM_ERROR,
                        ERRDESC = ConfigType.RS_SYSTEM_ERROR_MESS
                    }
                };
                logger.Error(ex);
            }
            string responseXML = ConvertXML.ModelToXMLString<SMSRS>(response);
            logger.Info("Response: " + responseXML);
            //ResultModel result = new ResultModel()
            //{
            //    xmlres = Server.UrlEncode(responseXML),
            //};
            //return result;
            return Server.HtmlEncode(responseXML);
        }
    }
}
