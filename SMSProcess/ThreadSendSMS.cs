using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using log4net;
using LibData;
using LibData.Provider;
using LibData.Bussiness;
using SMSProcess.Bussiness;
using System.Text.RegularExpressions;

namespace SMSProcess
{
    class ThreadSendSMS
    {
        private static readonly ILog logger = LogManager.GetLogger("ThreadSendSMS");
        private static Regex digitsOnly = new Regex(@"[^\d]");
        private const int NUMBER_TAKE = 50;
        public void SendSMSNormal()
        {
            try
            {
                ThreadPool.SetMaxThreads(100, int.MaxValue);
                while (true)
                {
                    QueueServiceProvider provider = new QueueServiceProvider();
                    List<QueueService> listSend = provider.GetByPriority(-1);
                    if (listSend.Count > 0)
                    {
                        List<ManualResetEvent> handles = new List<ManualResetEvent>();
                        while (listSend.Count > 0)
                        {
                            ManualResetEvent handle = new ManualResetEvent(false);
                            CSendSMS srv = new CSendSMS(handle);
                            handles.Add(handle);
                            List<QueueService> tmp = new List<QueueService>();
                            if (listSend.Count > NUMBER_TAKE)
                            {
                                tmp.AddRange(listSend.Take(NUMBER_TAKE).ToList());
                                listSend.RemoveRange(0, NUMBER_TAKE);
                            }
                            else
                            {
                                tmp.AddRange(listSend);
                                listSend.RemoveRange(0, listSend.Count);
                            }
                            ThreadPool.QueueUserWorkItem(srv.SendSMSFormList, tmp);
                        }
                        WaitHandle.WaitAll(handles.ToArray());
                    }
                    Thread.Sleep(10);
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                Thread.Sleep(1000);
                SendSMSNormal();
            }
        }

        public void SendSMSOTP()
        {
            try
            {
                ThreadPool.SetMaxThreads(100, int.MaxValue);
                while (true)
                {
                    QueueServiceProvider provider = new QueueServiceProvider();
                    List<QueueService> listSend = provider.GetByPriority(ConfigType.QueueService_Priority_OTP);
                    if(listSend.Count > 0)
                    {
                        List<ManualResetEvent> handles = new List<ManualResetEvent>();
                        while (listSend.Count > 0)
                        {
                            ManualResetEvent handle = new ManualResetEvent(false);
                            CSendSMS srv = new CSendSMS(handle);
                            handles.Add(handle);
                            List<QueueService> tmp = new List<QueueService>();
                            if (listSend.Count > NUMBER_TAKE)
                            {
                                tmp.AddRange(listSend.Take(NUMBER_TAKE).ToList());
                                listSend.RemoveRange(0, NUMBER_TAKE);
                            }
                            else
                            {
                                tmp.AddRange(listSend);
                                listSend.RemoveRange(0, listSend.Count);
                            }
                            ThreadPool.QueueUserWorkItem(srv.SendSMSFormList, tmp);
                        }
                        WaitHandle.WaitAll(handles.ToArray());
                    }
                    Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                Thread.Sleep(1000);
                SendSMSOTP();
            }
        }
    }

    public class CSendSMS
    {
        private static readonly ILog logger = LogManager.GetLogger("CLSendSMS");
        private ManualResetEvent _doneEvent;

        public CSendSMS(ManualResetEvent doneEvent)
        {
            _doneEvent = doneEvent;
        }
        public void SendSMSFormList(object obj)
        {
            try
            {
                List<QueueService> listSend = (List<QueueService>)obj;
                MTQueueReportProvider providerMTR = new MTQueueReportProvider();
                MTProvider providerMT = new MTProvider();
                QueueServiceProvider qsProvider = new QueueServiceProvider();
                EncryptAndDecrypt ead = new EncryptAndDecrypt();
                SubTelcoProvider stProvider = new SubTelcoProvider();
                RouteTelcoProvider routeProvider = new RouteTelcoProvider();
                while (listSend != null && listSend.Count > 0)
                {
                    //string str_result = "0:NONE_ROUTE";
                    QueueService model = listSend[0];
                    //if(model.Receiver.Contains("904993309") || model.Receiver.Contains("988018028"))
                    //{
                    //    str_result = SendSMS(model, false, ead, stProvider, routeProvider);
                    //}
                    string str_result = SendSMS(model, false, ead, stProvider, routeProvider);
                    var results = str_result.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    int result = Convert.ToInt32(results[0]);
                    if (result == 501 || result== 502 || result == 503 ||result == 555)
                    {
                        //FAIL
                        MT mt = new MT()
                        {
                            Content = model.Content,
                            DateCreate = model.DateCreate,
                            Dest = model.Dest,
                            Password = model.Password,
                            Priority = model.Priority,
                            ProcessingCode = model.ProcessingCode,
                            Receiver = model.Receiver,
                            Source = model.Source,
                            Status = ConfigType.MT_STATUS_NOT_NOTIFY_ERROR,
                            TimeSend = DateTime.Now,
                            TransID = model.TransID,
                            TransTime = model.TransTime,
                            User = model.User,
                            Result = result,
                            SMSID = model.SMSID,
                            RouteName = results[1]
                        };
                        providerMT.Insert(mt);
                    }
                    else
                    {
                        //SUCCESS
                        MTQueueReport mt = new MTQueueReport()
                        {
                            Content = model.Content,
                            DateCreate = model.DateCreate,
                            Dest = model.Dest,
                            Password = model.Password,
                            Priority = model.Priority,
                            ProcessingCode = model.ProcessingCode,
                            Receiver = model.Receiver,
                            Source = model.Source,
                            Status = ConfigType.MT_STATUS_NOT_NOTIFY,
                            TimeSend = DateTime.Now,
                            TransID = model.TransID,
                            TransTime = model.TransTime,
                            User = model.User,
                            Result = result,
                            SMSID = model.SMSID,
                            RouteName = results[1]
                        };
                        providerMTR.Insert(mt);
                    }
                    logger.Info("SendSMS ["+ results[1] + "] : " + model.Receiver + " | " + model.Content +" | "+ result);
                    listSend.RemoveAt(0);
                    qsProvider.DeleteById(model.Id);

                    Thread.Sleep(10);
                }
                _doneEvent.Set();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                _doneEvent.Set();
                Thread.Sleep(1000);
                //SendSMSFormList(listSend);
            }
        }
        public string SendSMS(object obj, bool IsBackup, EncryptAndDecrypt ead, SubTelcoProvider stProvider, RouteTelcoProvider routeProvider)
        {
            try
            {
                QueueService model = (QueueService)obj;
                string content = model.Content;
                if (model.ProcessingCode.Equals(ConfigType.QueueService_ProcessingCode_OTP))
                {
                    content = ead.Decrypt(content);
                }
                var modeltelco = stProvider.getSubTelcoByDest(model.Receiver);
                if (modeltelco == null)
                {
                    return 503 + ":NONE_TELCO";
                }
                else
                {
                    // CHECK TELCO
                    Telco telco = modeltelco.Telco;

                    // GET ROUTE
                    int route = routeProvider.getRoute(telco.Id, IsBackup);
                    int result = 0;
                    string code_Route = "";
                    if (route == ConfigType.ROUTE_VNET)
                    {
                        result = ConnectVNet.SendSMS(validatePhone(model.Receiver), content);
                        if (result == 0)
                        {
                            code_Route = "VNET";
                            //string log = string.Format("SendSMS: {0} -> {1} | {2}", model.Receiver, model.Content, code_Route);
                            //logger.Info(log + " - " + model.ProcessingCode);
                            return result + ":" + code_Route;
                        }
                        else if (!IsBackup)
                        {
                            return SendSMS(obj, true, ead, stProvider, routeProvider);
                        }
                        else
                        {
                            return 501 + ":" + code_Route;
                        }
                    }
                    else if (route == ConfigType.ROUTE_VNTP)
                    {
                        result = ConnectVNTP.SendSMS(validatePhone(model.Receiver), content);
                        if (result == 0)
                        {
                            code_Route = "VNTP";
                            return result + ":" + code_Route;
                        }
                        else if (!IsBackup)
                        {
                            return SendSMS(obj, true, ead, stProvider, routeProvider);
                        }
                        else
                        {
                            return 501 + ":" + code_Route;
                        }
                    }
                    else if (route == ConfigType.ROUTE_SOUTHtelecom)
                    {
                        string str_result = ConnectSOUTHtelecom.SendSMSBrandName(validatePhone(model.Receiver), content, "VietinBank");

                        if (result == 0)
                        {
                            code_Route = "SOUTHtelecom";
                            return result + ":" + code_Route;
                        }
                        else if (!IsBackup)
                        {
                            return SendSMS(obj, true, ead, stProvider, routeProvider);
                        }
                        else
                        {
                            return 501 + ":" + code_Route;
                        }
                    }
                    else
                    {
                        code_Route = "NONE_ROUTE";
                        //string log = string.Format("SendSMS: {0} -> {1} | {2}", model.Receiver, model.Content, code_Route);
                        //logger.Info(log + " - " + model.ProcessingCode);
                        return 502 + ":" + code_Route;
                    }
                }

            }
            catch(Exception ex)
            {
                logger.Error(ex);
                return 555 + ":SendSMS_EX_ERROR";
                
            }
            
            
        }
        public string validatePhone(string phone)
        {
            if (phone.StartsWith("0"))
            {
                return "84" + phone.Substring(1);
            }
            else if (!phone.StartsWith("84"))
            {
                return "84" + phone;
            }
            return phone;
        }
    }
}
