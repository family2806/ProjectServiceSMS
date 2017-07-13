using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace LibData.Models.Vietinbank
{
    [XmlRoot(ElementName = "HEADER")]
    public class HEADERRS
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

    [XmlRoot(ElementName = "ERROR")]
    public class ERRORRS
    {
        [XmlElement(ElementName = "ERRCODE")]
        public string ERRCODE { get; set; }
        [XmlElement(ElementName = "ERRDESC")]
        public string ERRDESC { get; set; }
    }

    [XmlRoot(ElementName = "DATA")]
    public class DATARS
    {
        [XmlElement(ElementName = "ERROR")]
        public ERRORRS ERROR { get; set; }
    }

    [XmlRoot(ElementName = "SMSRS")]
    public class SMSRSRS
    {
        [XmlElement(ElementName = "HEADER")]
        public HEADERRS HEADER { get; set; }
        [XmlElement(ElementName = "DATA")]
        public DATARS DATA { get; set; }
    }
}
