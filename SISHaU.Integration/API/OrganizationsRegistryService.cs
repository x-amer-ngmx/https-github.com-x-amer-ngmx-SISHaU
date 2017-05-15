//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Integration.Base;
using Integration.OrganizationsRegistry;
using Integration.OrganizationsRegistryBase;


namespace Integration.OrganizationsRegistryService
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/organizations-registry-service/", ConfigurationName="RegOrgPortsType")]
    public interface RegOrgPortsType
    {
        
        // CODEGEN: Контракт генерации сообщений с операцией importSubsidiary не является ни RPC, ни упакованным документом.
        [System.ServiceModel.OperationContractAttribute(Action="urn:importSubsidiary", ReplyAction="*")]
        [System.ServiceModel.FaultContractAttribute(typeof(Fault), Action="urn:importSubsidiary", Name="Fault", Namespace="http://dom.gosuslugi.ru/schema/integration/base/")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(BaseType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForeignBranchImportType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(HeaderType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SubsidiaryImportType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SubsidiaryType))]
        importSubsidiaryResponseMessage importSubsidiary(importSubsidiaryRequestMessage request);
        
        // CODEGEN: Контракт генерации сообщений с операцией importForeignBranch не является ни RPC, ни упакованным документом.
        [System.ServiceModel.OperationContractAttribute(Action="urn:importForeignBranch", ReplyAction="*")]
        [System.ServiceModel.FaultContractAttribute(typeof(Fault), Action="urn:importForeignBranch", Name="Fault", Namespace="http://dom.gosuslugi.ru/schema/integration/base/")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(BaseType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForeignBranchImportType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(HeaderType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SubsidiaryImportType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SubsidiaryType))]
        importForeignBranchResponseMessage importForeignBranch(importForeignBranchRequestMessage request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public class importSubsidiaryRequestMessage
    {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/base/")]
        public RequestHeader RequestHeader;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="importSubsidiaryRequest", Namespace="http://dom.gosuslugi.ru/schema/integration/organizations-registry/", Order=0)]
        public importSubsidiaryRequest importSubsidiaryRequest1;
        
        public importSubsidiaryRequestMessage()
        {
        }
        
        public importSubsidiaryRequestMessage(RequestHeader RequestHeader, importSubsidiaryRequest importSubsidiaryRequest1)
        {
            this.RequestHeader = RequestHeader;
            this.importSubsidiaryRequest1 = importSubsidiaryRequest1;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public class importSubsidiaryResponseMessage
    {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/base/")]
        public ResultHeader ResultHeader;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/base/", Order=0)]
        public ImportResult ImportResult;
        
        public importSubsidiaryResponseMessage()
        {
        }
        
        public importSubsidiaryResponseMessage(ResultHeader ResultHeader, ImportResult ImportResult)
        {
            this.ResultHeader = ResultHeader;
            this.ImportResult = ImportResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public class importForeignBranchRequestMessage
    {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/base/")]
        public RequestHeader RequestHeader;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="importForeignBranchRequest", Namespace="http://dom.gosuslugi.ru/schema/integration/organizations-registry/", Order=0)]
        public importForeignBranchRequest importForeignBranchRequest1;
        
        public importForeignBranchRequestMessage()
        {
        }
        
        public importForeignBranchRequestMessage(RequestHeader RequestHeader, importForeignBranchRequest importForeignBranchRequest1)
        {
            this.RequestHeader = RequestHeader;
            this.importForeignBranchRequest1 = importForeignBranchRequest1;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public class importForeignBranchResponseMessage
    {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/base/")]
        public ResultHeader ResultHeader;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/base/", Order=0)]
        public ImportResult ImportResult;
        
        public importForeignBranchResponseMessage()
        {
        }
        
        public importForeignBranchResponseMessage(ResultHeader ResultHeader, ImportResult ImportResult)
        {
            this.ResultHeader = ResultHeader;
            this.ImportResult = ImportResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface RegOrgPortsTypeChannel : RegOrgPortsType, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public class RegOrgPortsTypeClient : System.ServiceModel.ClientBase<RegOrgPortsType>, RegOrgPortsType
    {
        
        public RegOrgPortsTypeClient()
        {
        }
        
        public RegOrgPortsTypeClient(string endpointConfigurationName) : 
                base(endpointConfigurationName)
        {
        }
        
        public RegOrgPortsTypeClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress)
        {
        }
        
        public RegOrgPortsTypeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress)
        {
        }
        
        public RegOrgPortsTypeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        importSubsidiaryResponseMessage RegOrgPortsType.importSubsidiary(importSubsidiaryRequestMessage request)
        {
            return base.Channel.importSubsidiary(request);
        }
        
        public ResultHeader importSubsidiary(RequestHeader RequestHeader, importSubsidiaryRequest importSubsidiaryRequest1, out ImportResult ImportResult)
        {
            importSubsidiaryRequestMessage inValue = new importSubsidiaryRequestMessage();
            inValue.RequestHeader = RequestHeader;
            inValue.importSubsidiaryRequest1 = importSubsidiaryRequest1;
            importSubsidiaryResponseMessage retVal = ((RegOrgPortsType)(this)).importSubsidiary(inValue);
            ImportResult = retVal.ImportResult;
            return retVal.ResultHeader;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        importForeignBranchResponseMessage RegOrgPortsType.importForeignBranch(importForeignBranchRequestMessage request)
        {
            return base.Channel.importForeignBranch(request);
        }
        
        public ResultHeader importForeignBranch(RequestHeader RequestHeader, importForeignBranchRequest importForeignBranchRequest1, out ImportResult ImportResult)
        {
            importForeignBranchRequestMessage inValue = new importForeignBranchRequestMessage();
            inValue.RequestHeader = RequestHeader;
            inValue.importForeignBranchRequest1 = importForeignBranchRequest1;
            importForeignBranchResponseMessage retVal = ((RegOrgPortsType)(this)).importForeignBranch(inValue);
            ImportResult = retVal.ImportResult;
            return retVal.ResultHeader;
        }
    }
}
