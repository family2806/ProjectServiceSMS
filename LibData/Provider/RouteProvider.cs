using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibData.Provider
{
    public  class RouteProvider
    {
        private DBSMSGatewayDataContext db;
        private static ILog logger = LogManager.GetLogger("RouteProvider");
        public RouteProvider()
        {
            db = new DBSMSGatewayDataContext();
        }
        //public Route getRoute(int telco, int IsBackUp)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception)
        //    {

        //    }

        //}
    }
}
