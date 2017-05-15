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
using Integration.NsiBase;


namespace Integration.Nsi
{
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/nsi/")]
    public class importBaseDecisionMSPType
    {
        
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/base/", Order=0)]
        public string TransportGUID { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string ElementGuid { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string DecisionName { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public nsiRef DecisionType { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public bool IsAppliedToSubsidiaries { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public bool IsAppliedToRefundOfCharges { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/nsi/")]
    public class ImportCapitalRepairWorkType
    {
        
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/base/", Order=0)]
        public string TransportGUID { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string ElementGuid { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string ServiceName { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public nsiRef WorkGroupRef { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/nsi/")]
    public class ImportOrganizationWorkType
    {
        
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/base/", Order=0)]
        public string TransportGUID { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string ElementGuid { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string WorkName { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public nsiRef ServiceTypeRef { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("RequiredServiceRef", Order=4)]
        public nsiRef[] RequiredServiceRef { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("OKEI", typeof(string), Namespace="http://dom.gosuslugi.ru/schema/integration/base/", Order=5)]
        public string OKEI { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("StringDimensionUnit", typeof(string), Order=6)]
        public string StringDimensionUnit { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("ImportOrganizationWork", Order=7)]
        public ImportOrganizationWorkType[] ImportOrganizationWork { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://dom.gosuslugi.ru/schema/integration/nsi/")]
    public class importAdditionalServicesRequest : BaseType
    {
        
        public importAdditionalServicesRequest()
        {
            this.version = "10.0.1.2";
        }
        
        [System.Xml.Serialization.XmlElementAttribute("ImportAdditionalServiceType", Order=0)]
        public importAdditionalServicesRequestImportAdditionalServiceType[] ImportAdditionalServiceType { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("RecoverAdditionalServiceType", Order=1)]
        public importAdditionalServicesRequestRecoverAdditionalServiceType[] RecoverAdditionalServiceType { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("DeleteAdditionalServiceType", Order=2)]
        public importAdditionalServicesRequestDeleteAdditionalServiceType[] DeleteAdditionalServiceType { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute(Form=System.Xml.Schema.XmlSchemaForm.Qualified, Namespace="http://dom.gosuslugi.ru/schema/integration/base/")]
        public string version { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://dom.gosuslugi.ru/schema/integration/nsi/")]
    public class importAdditionalServicesRequestImportAdditionalServiceType
    {
        
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/base/", Order=0)]
        public string TransportGUID { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string ElementGuid { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string AdditionalServiceTypeName { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("OKEI", typeof(string), Namespace="http://dom.gosuslugi.ru/schema/integration/base/", Order=3)]
        public string OKEI { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("StringDimensionUnit", typeof(string), Order=4)]
        public string StringDimensionUnit { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://dom.gosuslugi.ru/schema/integration/nsi/")]
    public class importAdditionalServicesRequestRecoverAdditionalServiceType
    {
        
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/base/", Order=0)]
        public string TransportGUID { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string ElementGuid { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://dom.gosuslugi.ru/schema/integration/nsi/")]
    public class importAdditionalServicesRequestDeleteAdditionalServiceType
    {
        
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/base/", Order=0)]
        public string TransportGUID { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string ElementGuid { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://dom.gosuslugi.ru/schema/integration/nsi/")]
    public class importMunicipalServicesRequest : BaseType
    {
        
        public importMunicipalServicesRequest()
        {
            this.version = "11.0.0.4";
        }
        
        [System.Xml.Serialization.XmlElementAttribute("ImportMainMunicipalService", Order=0)]
        public importMunicipalServicesRequestImportMainMunicipalService[] ImportMainMunicipalService { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("RecoverMainMunicipalService", Order=1)]
        public importMunicipalServicesRequestRecoverMainMunicipalService[] RecoverMainMunicipalService { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("DeleteMainMunicipalService", Order=2)]
        public importMunicipalServicesRequestDeleteMainMunicipalService[] DeleteMainMunicipalService { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute(Form=System.Xml.Schema.XmlSchemaForm.Qualified, Namespace="http://dom.gosuslugi.ru/schema/integration/base/")]
        public string version { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://dom.gosuslugi.ru/schema/integration/nsi/")]
    public class importMunicipalServicesRequestImportMainMunicipalService
    {
        
        public importMunicipalServicesRequestImportMainMunicipalService()
        {
            this.GeneralNeeds = true;
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/base/", Order=0)]
        public string TransportGUID { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string ElementGuid { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public nsiRef MunicipalServiceRef { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public bool GeneralNeeds { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public bool SelfProduced { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string MainMunicipalServiceName { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("MunicipalResourceRef", Order=6)]
        public nsiRef[] MunicipalResourceRef { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/base/", Order=7)]
        public string OKEI { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("SortOrder", typeof(string), Order=8)]
        public string SortOrder { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("SortOrderNotDefined", typeof(bool), Order=9)]
        public bool SortOrderNotDefined { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://dom.gosuslugi.ru/schema/integration/nsi/")]
    public class importMunicipalServicesRequestRecoverMainMunicipalService
    {
        
        public importMunicipalServicesRequestRecoverMainMunicipalService()
        {
            this.HierarchyRecover = true;
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/base/", Order=0)]
        public string TransportGUID { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string ElementGuid { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public bool HierarchyRecover { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://dom.gosuslugi.ru/schema/integration/nsi/")]
    public class importMunicipalServicesRequestDeleteMainMunicipalService
    {
        
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/base/", Order=0)]
        public string TransportGUID { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string ElementGuid { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://dom.gosuslugi.ru/schema/integration/nsi/")]
    public class importOrganizationWorksRequest : BaseType
    {
        
        public importOrganizationWorksRequest()
        {
            this.version = "10.0.1.2";
        }
        
        [System.Xml.Serialization.XmlElementAttribute("ImportOrganizationWork", Order=0)]
        public ImportOrganizationWorkType[] ImportOrganizationWork { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("RecoverOrganizationWork", Order=1)]
        public importOrganizationWorksRequestRecoverOrganizationWork[] RecoverOrganizationWork { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("DeleteOrganizationWork", Order=2)]
        public importOrganizationWorksRequestDeleteOrganizationWork[] DeleteOrganizationWork { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute(Form=System.Xml.Schema.XmlSchemaForm.Qualified, Namespace="http://dom.gosuslugi.ru/schema/integration/base/")]
        public string version { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://dom.gosuslugi.ru/schema/integration/nsi/")]
    public class importOrganizationWorksRequestRecoverOrganizationWork
    {
        
        public importOrganizationWorksRequestRecoverOrganizationWork()
        {
            this.HierarchyRecover = true;
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/base/", Order=0)]
        public string TransportGUID { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string ElementGuid { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public bool HierarchyRecover { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://dom.gosuslugi.ru/schema/integration/nsi/")]
    public class importOrganizationWorksRequestDeleteOrganizationWork
    {
        
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/base/", Order=0)]
        public string TransportGUID { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string ElementGuid { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://dom.gosuslugi.ru/schema/integration/nsi/")]
    public class exportDataProviderNsiItemRequest : BaseType
    {
        
        public exportDataProviderNsiItemRequest()
        {
            this.version = "10.0.1.2";
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public exportDataProviderNsiItemRequestRegistryNumber RegistryNumber { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public System.DateTime ModifiedAfter { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute(Form=System.Xml.Schema.XmlSchemaForm.Qualified, Namespace="http://dom.gosuslugi.ru/schema/integration/base/")]
        public string version { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://dom.gosuslugi.ru/schema/integration/nsi/")]
    public enum exportDataProviderNsiItemRequestRegistryNumber
    {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("51")]
        Item51,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("59")]
        Item59,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("219")]
        Item219,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("302")]
        Item302,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://dom.gosuslugi.ru/schema/integration/nsi/")]
    public class exportNsiItemResult : BaseType
    {
        
        public exportNsiItemResult()
        {
            this.version = "10.0.1.2";
        }
        
        [System.Xml.Serialization.XmlElementAttribute("ErrorMessage", typeof(ErrorMessageType), Namespace="http://dom.gosuslugi.ru/schema/integration/base/", Order=0)]
        public ErrorMessageType ErrorMessage { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("NsiItem", typeof(NsiItemType), Order=1)]
        public NsiItemType NsiItem { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute(Form=System.Xml.Schema.XmlSchemaForm.Qualified, Namespace="http://dom.gosuslugi.ru/schema/integration/base/")]
        public string version { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://dom.gosuslugi.ru/schema/integration/nsi/")]
    public class exportDataProviderNsiPagingItemRequest : BaseType
    {
        
        public exportDataProviderNsiPagingItemRequest()
        {
            this.version = "11.1.0.5";
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public exportDataProviderNsiPagingItemRequestRegistryNumber RegistryNumber { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public int Page { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public System.DateTime ModifiedAfter { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute(Form=System.Xml.Schema.XmlSchemaForm.Qualified, Namespace="http://dom.gosuslugi.ru/schema/integration/base/")]
        public string version { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://dom.gosuslugi.ru/schema/integration/nsi/")]
    public enum exportDataProviderNsiPagingItemRequestRegistryNumber
    {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("51")]
        Item51,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("59")]
        Item59,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("219")]
        Item219,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("302")]
        Item302,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://dom.gosuslugi.ru/schema/integration/nsi/")]
    public class exportNsiPagingItemResult : BaseType
    {
        
        public exportNsiPagingItemResult()
        {
            this.version = "11.1.0.5";
        }
        
        [System.Xml.Serialization.XmlElementAttribute("ErrorMessage", typeof(ErrorMessageType), Namespace="http://dom.gosuslugi.ru/schema/integration/base/", Order=0)]
        public ErrorMessageType ErrorMessage { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("NsiItem", typeof(exportNsiPagingItemResultNsiItem), Order=1)]
        public exportNsiPagingItemResultNsiItem NsiItem { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute(Form=System.Xml.Schema.XmlSchemaForm.Qualified, Namespace="http://dom.gosuslugi.ru/schema/integration/base/")]
        public string version { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://dom.gosuslugi.ru/schema/integration/nsi/")]
    public class exportNsiPagingItemResultNsiItem : NsiItemType
    {
        
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int TotalItemsCount { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public int TotalPages { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public object CurrentPage { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://dom.gosuslugi.ru/schema/integration/nsi/")]
    public class importCapitalRepairWorkRequest : BaseType
    {
        
        public importCapitalRepairWorkRequest()
        {
            this.version = "11.1.0.5";
        }
        
        [System.Xml.Serialization.XmlElementAttribute("ImportCapitalRepairWork", Order=0)]
        public ImportCapitalRepairWorkType[] ImportCapitalRepairWork { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("RecoverCapitalRepairWork", Order=1)]
        public importCapitalRepairWorkRequestRecoverCapitalRepairWork[] RecoverCapitalRepairWork { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("DeleteCapitalRepairWork", Order=2)]
        public importCapitalRepairWorkRequestDeleteCapitalRepairWork[] DeleteCapitalRepairWork { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute(Form=System.Xml.Schema.XmlSchemaForm.Qualified, Namespace="http://dom.gosuslugi.ru/schema/integration/base/")]
        public string version { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://dom.gosuslugi.ru/schema/integration/nsi/")]
    public class importCapitalRepairWorkRequestRecoverCapitalRepairWork
    {
        
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/base/", Order=0)]
        public string TransportGUID { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string ElementGuid { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://dom.gosuslugi.ru/schema/integration/nsi/")]
    public class importCapitalRepairWorkRequestDeleteCapitalRepairWork
    {
        
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/base/", Order=0)]
        public string TransportGUID { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string ElementGuid { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://dom.gosuslugi.ru/schema/integration/nsi/")]
    public class importBaseDecisionMSPRequest : BaseType
    {
        
        public importBaseDecisionMSPRequest()
        {
            this.version = "11.1.0.5";
        }
        
        [System.Xml.Serialization.XmlElementAttribute("ImportBaseDecisionMSP", Order=0)]
        public importBaseDecisionMSPType[] ImportBaseDecisionMSP { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("RecoverBaseDecisionMSP", Order=1)]
        public importBaseDecisionMSPRequestRecoverBaseDecisionMSP[] RecoverBaseDecisionMSP { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("DeleteBaseDecisionMSP", Order=2)]
        public importBaseDecisionMSPRequestDeleteBaseDecisionMSP[] DeleteBaseDecisionMSP { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute(Form=System.Xml.Schema.XmlSchemaForm.Qualified, Namespace="http://dom.gosuslugi.ru/schema/integration/base/")]
        public string version { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://dom.gosuslugi.ru/schema/integration/nsi/")]
    public class importBaseDecisionMSPRequestRecoverBaseDecisionMSP
    {
        
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/base/", Order=0)]
        public string TransportGUID { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string ElementGuid { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://dom.gosuslugi.ru/schema/integration/nsi/")]
    public class importBaseDecisionMSPRequestDeleteBaseDecisionMSP
    {
        
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/base/", Order=0)]
        public string TransportGUID { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string ElementGuid { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://dom.gosuslugi.ru/schema/integration/nsi/")]
    public class getStateResult : BaseAsyncResponseType
    {
        
        public getStateResult()
        {
            this.version = "10.0.1.2";
        }
        
        [System.Xml.Serialization.XmlElementAttribute("ErrorMessage", typeof(ErrorMessageType), Namespace="http://dom.gosuslugi.ru/schema/integration/base/", Order=0)]
        public ErrorMessageType ErrorMessage { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("ImportResult", typeof(CommonResultType), Order=1)]
        public CommonResultType ImportResult { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("NsiItem", typeof(NsiItemType), Order=2)]
        public NsiItemType NsiItem { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("NsiList", typeof(NsiListType), Order=3)]
        public NsiListType NsiList { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("NsiPagingItem", typeof(getStateResultNsiPagingItem), Order=4)]
        public getStateResultNsiPagingItem NsiPagingItem { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute(Form=System.Xml.Schema.XmlSchemaForm.Qualified, Namespace="http://dom.gosuslugi.ru/schema/integration/base/")]
        public string version { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://dom.gosuslugi.ru/schema/integration/nsi/")]
    public class getStateResultNsiPagingItem : NsiItemType
    {
        
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int TotalItemsCount { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public int TotalPages { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public object CurrentPage { get; set; }
    }
}
