//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Integration.NsiBase;


namespace Integration.OrganizationsRegistryBase
{
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/organizations-registry-base/")]
    public class RegOrgType
    {
        
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string orgRootEntityGUID
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/organizations-registry-base/")]
    public class RegOrgVersionType
    {
        
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string orgVersionGUID
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/organizations-registry-base/")]
    public class ForeignBranchType
    {
        
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string FullName
        {
            get;
            set;
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string ShortName
        {
            get;
            set;
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/organizations-base/", Order=2)]
        public string NZA
        {
            get;
            set;
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/organizations-base/", Order=3)]
        public string INN
        {
            get;
            set;
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/organizations-base/", Order=4)]
        public string KPP
        {
            get;
            set;
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string Address
        {
            get;
            set;
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public string FIASHouseGuid
        {
            get;
            set;
        }
        
        [System.Xml.Serialization.XmlElementAttribute(DataType="date", Order=7)]
        public System.DateTime AccreditationStartDate
        {
            get;
            set;
        }
        
        [System.Xml.Serialization.XmlElementAttribute(DataType="date", Order=8)]
        public System.DateTime AccreditationEndDate
        {
            get;
            set;
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=9)]
        public string RegistrationCountry
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/organizations-registry-base/")]
    public class SubsidiaryType
    {
        
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string FullName
        {
            get;
            set;
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string ShortName
        {
            get;
            set;
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/organizations-base/", Order=2)]
        public string OGRN
        {
            get;
            set;
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/organizations-base/", Order=3)]
        public string INN
        {
            get;
            set;
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/organizations-base/", Order=4)]
        public string KPP
        {
            get;
            set;
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/organizations-base/", Order=5)]
        public string OKOPF
        {
            get;
            set;
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public string Address
        {
            get;
            set;
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public string FIASHouseGuid
        {
            get;
            set;
        }
        
        [System.Xml.Serialization.XmlElementAttribute(DataType="date", Order=8)]
        public System.DateTime ActivityEndDate
        {
            get;
            set;
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=9)]
        public SubsidiaryTypeSourceName SourceName
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://dom.gosuslugi.ru/schema/integration/organizations-registry-base/")]
    public class SubsidiaryTypeSourceName
    {
        
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="date")]
        public System.DateTime Date
        {
            get;
            set;
        }
        
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get;
            set;
        }
    }
}