using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace LibData.Provider
{
    public class SubTelcoProvider
    {
        private DBSMSGatewayDataContext db;
        private static ILog logger = LogManager.GetLogger("SubTelcoProvider");
        public SubTelcoProvider()
        {
            db = new DBSMSGatewayDataContext();
        }

        public SubTelco getSubTelcoByDest(string dest)
        {
            try
            {
                if(dest.StartsWith("0"))
                {
                    dest = "84" + dest.Substring(1);
                }
                else if(!dest.StartsWith("84"))
                {
                    dest = "84" + dest;
                }
                foreach (SubTelco item in db.SubTelcos)
                {
                    if (dest.StartsWith(item.StartPhone))
                        return item;
                }
                return null;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return null;
            }
        }

    }
}
