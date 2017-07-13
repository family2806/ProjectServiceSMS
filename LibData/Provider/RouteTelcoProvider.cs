using LibData.Bussiness;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibData.Provider
{
    public  class RouteTelcoProvider
    {
        private DBSMSGatewayDataContext db;
        private static ILog logger = LogManager.GetLogger("RouteTelcoProvider");
        public RouteTelcoProvider()
        {
            db = new DBSMSGatewayDataContext();
        }
        public  int getRoute(int idTelco,bool IsBackUp)
        {
            try
            {
                var route_telco = db.Route_Telcos.Where(m => m.IdTelco == idTelco && m.Route.HasValue).ToList();
                if (IsBackUp)
                {
                    if(route_telco.Count(m => m.Ratio == 0) > 1)
                    {
                        //Create Model Ramdom
                        string dataBackUp = "";
                        var route_telco_backup = route_telco.Where(m => m.Ratio == 0).ToList();
                        foreach (var item in route_telco_backup)
                        {
                            dataBackUp += item.Route + ":" + item.Ratio + ",";
                        }
                        dataBackUp += "0:0";
                        return Convert.ToInt32(Ramdom(dataBackUp));
                    }
                    else
                    {
                        return route_telco.First(m => m.Ratio == 0).Route.Value;
                    }
                }
                //Create Model Ramdom
                string data = "";
                foreach(var item in route_telco)
                {
                    data += item.Route + ":" + item.Ratio + ",";
                }
                data += "0:0";
                return Convert.ToInt32(Ramdom(data));
            }
            catch (Exception)
            {
                return -101;
            }
        }
        private  string Ramdom(string data)
        {

            var srcs = data.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            List<ProportionValue> listsrc = new List<ProportionValue>();
            foreach (var item in srcs)
            {
                ProportionValue model = new ProportionValue();
                if (!string.IsNullOrEmpty(item))
                {
                    var srcandratio = item.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    model.Proportion = Convert.ToDouble(srcandratio[1]);
                    model.Value = srcandratio[0];
                    listsrc.Add(model);
                }
            }
            return ChooseByRandom(listsrc);
        }
        private  string ChooseByRandom(
           List<ProportionValue> collection)
        {
            Random random = new Random();
            var rnd = random.NextDouble();

            foreach (var item in collection)
            {
                if (rnd < item.Proportion)
                    return item.Value;
                rnd -= item.Proportion;
            }
            throw new InvalidOperationException(
                //The proportions in the collection do not add up to 1.
                "");
        }

    }
}
