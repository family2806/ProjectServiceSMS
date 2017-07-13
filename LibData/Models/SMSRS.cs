/* 
 Licensed under the Apache License, Version 2.0

 http://www.apache.org/licenses/LICENSE-2.0
 */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace LibData.Models
{
    [XmlRoot(ElementName = "HEADER")]
    public class HEADERRES
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
    public class ERRORRES
    {
        [XmlElement(ElementName = "ERRCODE")]
        public int ERRCODE { get; set; }
        [XmlElement(ElementName = "ERRDESC")]
        public string ERRDESC { get; set; }
    }

    [XmlRoot(ElementName = "DATA")]
    public class DATARES
    {
        [XmlElement(ElementName = "ERROR")]
        public ERRORRES ERROR { get; set; }
    }

    [XmlRoot(ElementName = "SMSRS")]
    public class SMSRS
    {
        [XmlElement(ElementName = "HEADER")]
        public HEADERRES HEADER { get; set; }
        [XmlElement(ElementName = "DATA")]
        public DATARES DATA { get; set; }
    }

}
