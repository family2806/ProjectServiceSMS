﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SMSProcess.ServiceVNTP {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://sms.mc.vasc.com/", ConfigurationName="ServiceVNTP.BrandNameWS")]
    public interface BrandNameWS {
        
        // CODEGEN: Generating message contract since element name username from namespace  is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://sms.mc.vasc.com/BrandNameWS/uploadSMSRequest", ReplyAction="http://sms.mc.vasc.com/BrandNameWS/uploadSMSResponse")]
        SMSProcess.ServiceVNTP.uploadSMSResponse uploadSMS(SMSProcess.ServiceVNTP.uploadSMSRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://sms.mc.vasc.com/BrandNameWS/uploadSMSRequest", ReplyAction="http://sms.mc.vasc.com/BrandNameWS/uploadSMSResponse")]
        System.Threading.Tasks.Task<SMSProcess.ServiceVNTP.uploadSMSResponse> uploadSMSAsync(SMSProcess.ServiceVNTP.uploadSMSRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class uploadSMSRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="uploadSMS", Namespace="http://sms.mc.vasc.com/", Order=0)]
        public SMSProcess.ServiceVNTP.uploadSMSRequestBody Body;
        
        public uploadSMSRequest() {
        }
        
        public uploadSMSRequest(SMSProcess.ServiceVNTP.uploadSMSRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class uploadSMSRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string username;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string password;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string serviceId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string userId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string contentType;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string serviceKind;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string infor;
        
        public uploadSMSRequestBody() {
        }
        
        public uploadSMSRequestBody(string username, string password, string serviceId, string userId, string contentType, string serviceKind, string infor) {
            this.username = username;
            this.password = password;
            this.serviceId = serviceId;
            this.userId = userId;
            this.contentType = contentType;
            this.serviceKind = serviceKind;
            this.infor = infor;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class uploadSMSResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="uploadSMSResponse", Namespace="http://sms.mc.vasc.com/", Order=0)]
        public SMSProcess.ServiceVNTP.uploadSMSResponseBody Body;
        
        public uploadSMSResponse() {
        }
        
        public uploadSMSResponse(SMSProcess.ServiceVNTP.uploadSMSResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class uploadSMSResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public int @return;
        
        public uploadSMSResponseBody() {
        }
        
        public uploadSMSResponseBody(int @return) {
            this.@return = @return;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface BrandNameWSChannel : SMSProcess.ServiceVNTP.BrandNameWS, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BrandNameWSClient : System.ServiceModel.ClientBase<SMSProcess.ServiceVNTP.BrandNameWS>, SMSProcess.ServiceVNTP.BrandNameWS {
        
        public BrandNameWSClient() {
        }
        
        public BrandNameWSClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BrandNameWSClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BrandNameWSClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BrandNameWSClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        SMSProcess.ServiceVNTP.uploadSMSResponse SMSProcess.ServiceVNTP.BrandNameWS.uploadSMS(SMSProcess.ServiceVNTP.uploadSMSRequest request) {
            return base.Channel.uploadSMS(request);
        }
        
        public int uploadSMS(string username, string password, string serviceId, string userId, string contentType, string serviceKind, string infor) {
            SMSProcess.ServiceVNTP.uploadSMSRequest inValue = new SMSProcess.ServiceVNTP.uploadSMSRequest();
            inValue.Body = new SMSProcess.ServiceVNTP.uploadSMSRequestBody();
            inValue.Body.username = username;
            inValue.Body.password = password;
            inValue.Body.serviceId = serviceId;
            inValue.Body.userId = userId;
            inValue.Body.contentType = contentType;
            inValue.Body.serviceKind = serviceKind;
            inValue.Body.infor = infor;
            SMSProcess.ServiceVNTP.uploadSMSResponse retVal = ((SMSProcess.ServiceVNTP.BrandNameWS)(this)).uploadSMS(inValue);
            return retVal.Body.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<SMSProcess.ServiceVNTP.uploadSMSResponse> SMSProcess.ServiceVNTP.BrandNameWS.uploadSMSAsync(SMSProcess.ServiceVNTP.uploadSMSRequest request) {
            return base.Channel.uploadSMSAsync(request);
        }
        
        public System.Threading.Tasks.Task<SMSProcess.ServiceVNTP.uploadSMSResponse> uploadSMSAsync(string username, string password, string serviceId, string userId, string contentType, string serviceKind, string infor) {
            SMSProcess.ServiceVNTP.uploadSMSRequest inValue = new SMSProcess.ServiceVNTP.uploadSMSRequest();
            inValue.Body = new SMSProcess.ServiceVNTP.uploadSMSRequestBody();
            inValue.Body.username = username;
            inValue.Body.password = password;
            inValue.Body.serviceId = serviceId;
            inValue.Body.userId = userId;
            inValue.Body.contentType = contentType;
            inValue.Body.serviceKind = serviceKind;
            inValue.Body.infor = infor;
            return ((SMSProcess.ServiceVNTP.BrandNameWS)(this)).uploadSMSAsync(inValue);
        }
    }
}
