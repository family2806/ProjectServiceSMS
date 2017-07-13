using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LibData.Models
{
    [XmlRoot(ElementName = "HEADER")]
    public class HEADERREQ
    {
        [XmlElement(ElementName = "SOURCE")]
        public string SOURCE { get; set; }
        [XmlElement(ElementName = "DEST")]
        public string DEST { get; set; }
        [XmlElement(ElementName = "TRANSID")]
        public string TRANSID { get; set; }
        [XmlElement(ElementName = "TRANSTIME")]
        public string TRANSTIME { get; set; }
        [XmlElement(ElementName = "USER")]
        public string USER { get; set; }
        [XmlElement(ElementName = "PWD")]
        public string PWD { get; set; }
    }

    [XmlRoot(ElementName = "SMS")]
    public class SMSREQ
    {
        [XmlElement(ElementName = "SMSID")]
        public string SMSID { get; set; }
        [XmlElement(ElementName = "PROCESSINGCODE")]
        public string PROCESSINGCODE { get; set; }
        [XmlElement(ElementName = "PRIORITY")]
        public int PRIORITY { get; set; }
        [XmlElement(ElementName = "RECEIVER")]
        public string RECEIVER { get; set; }
        [XmlElement(ElementName = "CONTENT")]
        public string CONTENT { get; set; }
    }

    [XmlRoot(ElementName = "DATA")]
    public class DATAREQ
    {
        [XmlElement(ElementName = "SMS")]
        public List<SMSREQ> SMS { get; set; }
    }

    [XmlRoot(ElementName = "SMSRQ")]
    public class SMSRQ
    {
        [XmlElement(ElementName = "HEADER")]
        public HEADERREQ HEADER { get; set; }
        [XmlElement(ElementName = "DATA")]
        public DATAREQ DATA { get; set; }
    }

}
