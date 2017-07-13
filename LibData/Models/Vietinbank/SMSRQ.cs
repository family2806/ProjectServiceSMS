using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace LibData.Models.Vietinbank
{
    [XmlRoot(ElementName = "HEADER")]
    public class HEADERRQ
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

    [XmlRoot(ElementName = "DATA")]
    public class DATARQ
    {
        [XmlElement(ElementName = "NOSMS")]
        public string NOSMS { get; set; }
        [XmlElement(ElementName = "SMSLIST")]
        public string SMSLIST { get; set; }
    }

    [XmlRoot(ElementName = "SMSRQ")]
    public class SMSRQ
    {
        [XmlElement(ElementName = "HEADER")]
        public HEADERRQ HEADER { get; set; }
        [XmlElement(ElementName = "DATA")]
        public DATARQ DATA { get; set; }
    }
}
