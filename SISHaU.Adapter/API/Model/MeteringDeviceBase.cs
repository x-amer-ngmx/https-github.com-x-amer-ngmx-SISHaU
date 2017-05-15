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


namespace Integration.MeteringDeviceBase
{
    
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ExportElectricMeteringValueType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(MunicipalResourceElectricType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/metering-device-base/")]
    public class ElectricMeteringValueType
    {
        
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public decimal MeteringValueT1
        {
            get;
            set;
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public decimal MeteringValueT2
        {
            get;
            set;
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public decimal MeteringValueT3
        {
            get;
            set;
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string ReadingsSource
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/metering-device-base/")]
    public class ExportElectricMeteringValueType : ElectricMeteringValueType
    {
        
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public System.DateTime EnterIntoSystem
        {
            get;
            set;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ExportOneRateMeteringValueType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(MunicipalResourceNotElectricType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/metering-device-base/")]
    public class OneRateMeteringValueType
    {
        
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public nsiRef MunicipalResource
        {
            get;
            set;
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public decimal MeteringValue
        {
            get;
            set;
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string ReadingsSource
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://dom.gosuslugi.ru/schema/integration/metering-device-base/")]
    public class ExportOneRateMeteringValueType : OneRateMeteringValueType
    {
        
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public System.DateTime EnterIntoSystem
        {
            get;
            set;
        }
    }
}