using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using log4net;
using LibData;
using LibData.Provider;
using LibData.Models;
using LibData.Bussiness;
using SMSProcess.Bussiness;

namespace SMSProcess
{
    class ThreadCallVietinbank
    {
        private static readonly ILog logger = LogManager.GetLogger("ThreadCallVietinbank");
        private const int NUMBER_TAKE = 100;
        public void AutoCallVietinbank()
        {
            try
            {
                ThreadPool.SetMaxThreads(100, int.MaxValue);
                while(true)
                {
                    MTQueueReportProvider provider = new MTQueueReportProvider();
                    List<MTQueueReport> list = provider.GetAllByStatusAndProcessingCode(ConfigType.MT_STATUS_NOT_NOTIFY,"");
                    if(list.Count > 0)
                    {
                        List<ManualResetEvent> handles = new List<ManualResetEvent>();
                        while (list.Count > 0)
                        {
                            ManualResetEvent handle = new ManualResetEvent(false);
                            SendRequestVietinbank srv = new SendRequestVietinbank(handle);
                            handles.Add(handle);
                            List<MTQueueReport> tmp = new List<MTQueueReport>();
                            if(list.Count > NUMBER_TAKE)
                            {
                                tmp.AddRange(list.Take(NUMBER_TAKE).ToList());
                                list.RemoveRange(0, NUMBER_TAKE);
                            }
                            else
                            {
                                tmp.AddRange(list);
                                list.RemoveRange(0, list.Count);
                            }
                            ThreadPool.QueueUserWorkItem(srv.ThreadSendRequest, tmp);
                        }
                        WaitHandle.WaitAll(handles.ToArray());
                    }
                    Thread.Sleep(100);
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                Thread.Sleep(1000);
                AutoCallVietinbank();
            }
        }

        public void AutoCallVietinbankOTP()
        {
            try
            {
                ThreadPool.SetMaxThreads(100, int.MaxValue);
                while (true)
                {
                    MTQueueReportProvider provider = new MTQueueReportProvider();
                    List<MTQueueReport> list = provider.GetAllByStatusAndProcessingCode(ConfigType.MT_STATUS_NOT_NOTIFY,ConfigType.QueueService_ProcessingCode_OTP);
                    if (list.Count > 0)
                    {
                        List<ManualResetEvent> handles = new List<ManualResetEvent>();
                        while (list.Count > 0)
                        {
                            ManualResetEvent handle = new ManualResetEvent(false);
                            SendRequestVietinbank srv = new SendRequestVietinbank(handle);
                            handles.Add(handle);
                            List<MTQueueReport> tmp = new List<MTQueueReport>();
                            if (list.Count > NUMBER_TAKE)
                            {
                                tmp.AddRange(list.Take(NUMBER_TAKE).ToList());
                                list.RemoveRange(0, NUMBER_TAKE);
                            }
                            else
                            {
                                tmp.AddRange(list);
                                list.RemoveRange(0, list.Count);
                            }
                            ThreadPool.QueueUserWorkItem(srv.ThreadSendRequestOTP, tmp);
                        }
                        WaitHandle.WaitAll(handles.ToArray());
                    }
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                Thread.Sleep(1000);
                AutoCallVietinbankOTP();
            }
        }
        public void AutoInsertMTError()
        {
            try
            {
                ThreadPool.SetMaxThreads(100, int.MaxValue);
                while (true)
                {
                    MTQueueReportProvider provider = new MTQueueReportProvider();
                    List<MTQueueReport> list = provider.GetAllByStatusAndProcessingCode(ConfigType.MT_STATUS_NOT_NOTIFY_ERROR, "");
                    if (list.Count > 0)
                    {
                        List<ManualResetEvent> handles = new List<ManualResetEvent>();
                        while (list.Count > 0)
                        {
                            ManualResetEvent handle = new ManualResetEvent(false);
                            SendRequestVietinbank srv = new SendRequestVietinbank(handle);
                            handles.Add(handle);
                            List<MTQueueReport> tmp = new List<MTQueueReport>();
                            if (list.Count > NUMBER_TAKE)
                            {
                                tmp.AddRange(list.Take(NUMBER_TAKE).ToList());
                                list.RemoveRange(0, NUMBER_TAKE);
                            }
                            else
                            {
                                tmp.AddRange(list);
                                list.RemoveRange(0, list.Count);
                            }
                            ThreadPool.QueueUserWorkItem(srv.ThreadInsertRequestERROR, tmp);
                        }
                        WaitHandle.WaitAll(handles.ToArray());
                    }
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                Thread.Sleep(1000);
                AutoCallVietinbank();
            }
        }
        public void AutoInsertMTErrorkOTP()
        {
            try
            {
                ThreadPool.SetMaxThreads(100, int.MaxValue);
                while (true)
                {
                    MTQueueReportProvider provider = new MTQueueReportProvider();
                    List<MTQueueReport> list = provider.GetAllByStatusAndProcessingCode(ConfigType.MT_STATUS_NOT_NOTIFY_ERROR, ConfigType.QueueService_ProcessingCode_OTP);
                    if (list.Count > 0)
                    {
                        List<ManualResetEvent> handles = new List<ManualResetEvent>();
                        while (list.Count > 0)
                        {
                            ManualResetEvent handle = new ManualResetEvent(false);
                            SendRequestVietinbank srv = new SendRequestVietinbank(handle);
                            handles.Add(handle);
                            List<MTQueueReport> tmp = new List<MTQueueReport>();
                            if (list.Count > NUMBER_TAKE)
                            {
                                tmp.AddRange(list.Take(NUMBER_TAKE).ToList());
                                list.RemoveRange(0, NUMBER_TAKE);
                            }
                            else
                            {
                                tmp.AddRange(list);
                                list.RemoveRange(0, list.Count);
                            }
                            ThreadPool.QueueUserWorkItem(srv.ThreadInsertRequestERROR, tmp);
                        }
                        WaitHandle.WaitAll(handles.ToArray());
                    }
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                Thread.Sleep(1000);
                AutoCallVietinbank();
            }
        }
    }

    public class SendRequestVietinbank
    {
        private static readonly ILog logger = LogManager.GetLogger("SendRequestVietinbank");
        private ManualResetEvent _doneEvent;
        public SendRequestVietinbank(ManualResetEvent doneEvent)
        {
            _doneEvent = doneEvent;
        }
        public void ThreadInsertRequestERROR(object o)
        {
            try
            {
                MTQueueReportProvider providerMTR = new MTQueueReportProvider();
                MTProvider providerMT = new MTProvider();
                List<MTQueueReport> list = (List <MTQueueReport> )o;
                //string listSMSID = "";
                foreach (MTQueueReport item in list)
                {
                    //logger.Info("ThreadSendRequest - " + item.ProcessingCode);
                    //listSMSID += item.SMSID + "|";
                    item.Status = ConfigType.MT_STATUS_NOT_NOTIFY_ERROR;
                    MT mt = new MT()
                    {
                        Content = item.Content,
                        DateCreate = item.DateCreate,
                        Dest = item.Dest,
                        Password = item.Password,
                        Priority = item.Priority,
                        ProcessingCode = item.ProcessingCode,
                        Receiver = item.Receiver,
                        Source = item.Source,
                        Status = item.Status,
                        TimeSend = item.TimeSend,
                        TransID = item.TransID,
                        TransTime = item.TransTime,
                        User = item.User,
                        Result = item.Result,
                        SMSID = item.SMSID,
                        RouteName = item.RouteName
                    };
                    providerMT.Insert(mt);
                    providerMTR.DeleteById(item.Id);
                }
                //if (!string.IsNullOrEmpty(listSMSID))
                //{
                //    listSMSID = listSMSID.Substring(0, listSMSID.Length - 1);
                //    ConnectServiceVietinbank csv = new ConnectServiceVietinbank();
                //    Thread callService = new Thread(csv.CallServiceBDSD);
                //    ModelCallService model = new ModelCallService()
                //    {
                //        NOSMS = list.Count,
                //        SMSLIST = listSMSID
                //    };
                //    callService.Start(model);
                //}
                _doneEvent.Set();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                _doneEvent.Set();
            }
        }
        public void ThreadSendRequest(object o)
        {
            try
            {
                MTQueueReportProvider providerMTR = new MTQueueReportProvider();
                MTProvider providerMT = new MTProvider();
                List<MTQueueReport> list = (List<MTQueueReport>)o;
                string listSMSID = "";
                foreach (MTQueueReport item in list)
                {
                    //logger.Info("ThreadSendRequest - " + item.ProcessingCode);
                    listSMSID += item.SMSID + "|";
                    item.Status = ConfigType.MT_STATUS_NOTIFY_OK;
                    MT mt = new MT()
                    {
                        Content = item.Content,
                        DateCreate = item.DateCreate,
                        Dest = item.Dest,
                        Password = item.Password,
                        Priority = item.Priority,
                        ProcessingCode = item.ProcessingCode,
                        Receiver = item.Receiver,
                        Source = item.Source,
                        Status = item.Status,
                        TimeSend = item.TimeSend,
                        TransID = item.TransID,
                        TransTime = item.TransTime,
                        User = item.User,
                        Result = item.Result,
                        SMSID = item.SMSID,
                        RouteName = item.RouteName
                    };
                    providerMT.Insert(mt);
                    providerMTR.DeleteById(item.Id);
                }
                if (!string.IsNullOrEmpty(listSMSID))
                {
                    listSMSID = listSMSID.Substring(0, listSMSID.Length - 1);
                    ConnectServiceVietinbank csv = new ConnectServiceVietinbank();
                    Thread callService = new Thread(csv.CallServiceBDSD);
                    ModelCallService model = new ModelCallService()
                    {
                        NOSMS = list.Count,
                        SMSLIST = listSMSID
                    };
                    callService.Start(model);
                }
                _doneEvent.Set();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                _doneEvent.Set();
            }
        }
        public void ThreadSendRequestOTP(object o)
        {
            try
            {

                MTQueueReportProvider providerMTR = new MTQueueReportProvider();
                MTProvider providerMT = new MTProvider();
                List<MTQueueReport> list = (List<MTQueueReport>)o;
                string listSMSID = "";
                foreach (MTQueueReport item in list)
                {
                    
                    listSMSID += item.SMSID + "|";
                    item.Status = ConfigType.MT_STATUS_NOTIFY_OK;
                    MT mt = new MT()
                    {
                        Content = item.Content,
                        DateCreate = item.DateCreate,
                        Dest = item.Dest,
                        Password = item.Password,
                        Priority = item.Priority,
                        ProcessingCode = item.ProcessingCode,
                        Receiver = item.Receiver,
                        Source = item.Source,
                        Status = item.Status,
                        TimeSend = item.TimeSend,
                        TransID = item.TransID,
                        TransTime = item.TransTime,
                        User = item.User,
                        Result = item.Result,
                        SMSID = item.SMSID,
                        RouteName = item.RouteName
                    };
                    providerMT.Insert(mt);
                    providerMTR.DeleteById(item.Id);
                }
                if (!string.IsNullOrEmpty(listSMSID))
                {
                    listSMSID = listSMSID.Substring(0, listSMSID.Length - 1);
                    ConnectServiceVietinbank csv = new ConnectServiceVietinbank();
                    Thread callService = new Thread(csv.CallServiceOPT);
                    ModelCallService model = new ModelCallService()
                    {
                        NOSMS = list.Count,
                        SMSLIST = listSMSID
                    };
                    callService.Start(model);
                }
                _doneEvent.Set();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                _doneEvent.Set();
            }
        }
    }
}
