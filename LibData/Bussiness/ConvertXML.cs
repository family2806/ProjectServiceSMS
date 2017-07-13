using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using log4net;
using System.IO;

namespace LibData.Bussiness
{
    public class ConvertXML
    {
        private static ILog logger = LogManager.GetLogger("ConvertXML");
        public static string ModelToXMLString<T>(T model)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                StringWriter sw = new StringWriter();
                xs.Serialize(sw, model);
                return sw.ToString();
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                return null;
            }
        }

        public static T XMLToModel<T>(string xml)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                StringReader reader = new StringReader(xml);
                T model = (T) xs.Deserialize(reader);
                return model;
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                return default(T);
            }
        }

    }
}
