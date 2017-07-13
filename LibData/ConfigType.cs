using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibData
{
    public class ConfigType
    {
        public const int RS_SUCCESS = 0;
        public const string RS_SUCCESS_MESS = "Success";
        public const int RS_SYSTEM_ERROR = 3;
        public const string RS_SYSTEM_ERROR_MESS = "System Error";
        public const int RS_PASSWORD_FAIL = 1;
        public const string RS_PASSWORD_FAIL_MESS = "User or password fail";
        public const int RS_PHONE_FAIL = 2;
        public const string RS_PHONE_FAIL_MESS = "Phone number fail";

        public const int QueueService_Priority_OTP = 1;
        public const string QueueService_ProcessingCode_OTP = "620000";

        public const int MT_STATUS_NOT_NOTIFY = 0;
        
        public const int MT_STATUS_NOTIFY_OK = 1;

        public const int MT_STATUS_NOT_NOTIFY_ERROR = 2;

        public const string VietinbankUser = "vietinbank";
        public const string VietinbankPassword = "vietinbank@2016";
        public const string HungThinhKey = "VIETINBANKHUNGTHINH68668";
        public const string HungThinhIV = "97041568";
        public const string HungThinhUser = "hungthinh";
        public const string HungThinhPassword = "HungThinh@2016";

        //Route
        public const int ROUTE_VNET = 1;
        public const int ROUTE_SOUTHtelecom = 2;
       // public const int ROUTE_VP = 3;
        public const int ROUTE_VNTP = 3;
    }
 
}
