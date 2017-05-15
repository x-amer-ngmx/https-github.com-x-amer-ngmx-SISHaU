//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Integration.Xmldsig
{
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class ObjectType
    {
        
        [System.Xml.Serialization.XmlTextAttribute()]
        [System.Xml.Serialization.XmlAnyElementAttribute(Order=0)]
        public System.Xml.XmlNode[] Any { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="ID")]
        public string Id { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MimeType { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="anyURI")]
        public string Encoding { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class SPKIDataType
    {
        
        [System.Xml.Serialization.XmlAnyElementAttribute(Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("SPKISexp", typeof(byte[]), DataType="base64Binary", Order=0)]
        public object[] Items { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class PGPDataType
    {
        
        [System.Xml.Serialization.XmlElementAttribute("PGPKeyID", typeof(byte[]), DataType="base64Binary", Order=0)]
        public byte[] PGPKeyID { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("PGPKeyPacket", typeof(byte[]), DataType="base64Binary", Order=1)]
        public byte[] PGPKeyPacket { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class X509IssuerSerialType
    {
        
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string X509IssuerName { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(DataType="integer", Order=1)]
        public string X509SerialNumber { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class X509DataType
    {
        
        [System.Xml.Serialization.XmlElementAttribute("X509CRL", typeof(byte[]), DataType="base64Binary", Order=0)]
        public byte[] X509CRL { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("X509Certificate", typeof(byte[]), DataType="base64Binary", Order=1)]
        public byte[] X509Certificate { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("X509IssuerSerial", typeof(X509IssuerSerialType), Order=2)]
        public X509IssuerSerialType X509IssuerSerial { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("X509SKI", typeof(byte[]), DataType="base64Binary", Order=3)]
        public byte[] X509SKI { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("X509SubjectName", typeof(string), Order=4)]
        public string X509SubjectName { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class RetrievalMethodType
    {
        
        [System.Xml.Serialization.XmlArrayAttribute(Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Transform", IsNullable=false)]
        public TransformType[] Transforms { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="anyURI")]
        public string URI { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="anyURI")]
        public string Type { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class TransformType
    {
        
        [System.Xml.Serialization.XmlAnyElementAttribute(Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("XPath", typeof(string), Order=0)]
        public object[] Items { get; set; }
        
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="anyURI")]
        public string Algorithm { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class RSAKeyValueType
    {
        
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", Order=0)]
        public byte[] Modulus { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", Order=1)]
        public byte[] Exponent { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class DSAKeyValueType
    {
        
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", Order=0)]
        public byte[] P { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", Order=1)]
        public byte[] Q { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", Order=2)]
        public byte[] G { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", Order=3)]
        public byte[] Y { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", Order=4)]
        public byte[] J { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", Order=5)]
        public byte[] Seed { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", Order=6)]
        public byte[] PgenCounter { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class KeyValueType
    {
        
        [System.Xml.Serialization.XmlElementAttribute("DSAKeyValue", typeof(DSAKeyValueType), Order=0)]
        public DSAKeyValueType DSAKeyValue { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("RSAKeyValue", typeof(RSAKeyValueType), Order=1)]
        public RSAKeyValueType RSAKeyValue { get; set; }
        
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class KeyInfoType
    {
        
        [System.Xml.Serialization.XmlElementAttribute("KeyName", typeof(string), Order=0)]
        public string KeyName { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("KeyValue", typeof(KeyValueType), Order=1)]
        public KeyValueType KeyValue { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("MgmtData", typeof(string), Order=2)]
        public string MgmtData { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("PGPData", typeof(PGPDataType), Order=3)]
        public PGPDataType PGPData { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("RetrievalMethod", typeof(RetrievalMethodType), Order=4)]
        public RetrievalMethodType RetrievalMethod { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("SPKIData", typeof(SPKIDataType), Order=5)]
        public SPKIDataType SPKIData { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("X509Data", typeof(X509DataType), Order=6)]
        public X509DataType X509Data { get; set; }
        
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="ID")]
        public string Id { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class SignatureValueType
    {
        
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="ID")]
        public string Id { get; set; }
        
        [System.Xml.Serialization.XmlTextAttribute(DataType="base64Binary")]
        public byte[] Value { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class DigestMethodType
    {
        
        [System.Xml.Serialization.XmlTextAttribute()]
        [System.Xml.Serialization.XmlAnyElementAttribute(Order=0)]
        public System.Xml.XmlNode[] Any { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="anyURI")]
        public string Algorithm { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class ReferenceType
    {
        
        [System.Xml.Serialization.XmlArrayAttribute(Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Transform", IsNullable=false)]
        public TransformType[] Transforms { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public DigestMethodType DigestMethod { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", Order=2)]
        public byte[] DigestValue { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="ID")]
        public string Id { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="anyURI")]
        public string URI { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="anyURI")]
        public string Type { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class SignatureMethodType
    {
        
        [System.Xml.Serialization.XmlElementAttribute(DataType="integer", Order=0)]
        public string HMACOutputLength { get; set; }
        
        [System.Xml.Serialization.XmlTextAttribute()]
        [System.Xml.Serialization.XmlAnyElementAttribute(Order=1)]
        public System.Xml.XmlNode[] Any { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="anyURI")]
        public string Algorithm { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class CanonicalizationMethodType
    {
        
        [System.Xml.Serialization.XmlTextAttribute()]
        [System.Xml.Serialization.XmlAnyElementAttribute(Order=0)]
        public System.Xml.XmlNode[] Any { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="anyURI")]
        public string Algorithm { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class SignedInfoType
    {
        
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public CanonicalizationMethodType CanonicalizationMethod { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public SignatureMethodType SignatureMethod { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("Reference", Order=2)]
        public ReferenceType[] Reference { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="ID")]
        public string Id { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class SignatureType
    {
        
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public SignedInfoType SignedInfo { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public SignatureValueType SignatureValue { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public KeyInfoType KeyInfo { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("Object", Order=3)]
        public ObjectType[] Object { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="ID")]
        public string Id { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class SignaturePropertyType
    {
        
        [System.Xml.Serialization.XmlAnyElementAttribute(Order=0)]
        public System.Xml.XmlElement[] Items { get; set; }
        
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="anyURI")]
        public string Target { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="ID")]
        public string Id { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class SignaturePropertiesType
    {
        
        [System.Xml.Serialization.XmlElementAttribute("SignatureProperty", Order=0)]
        public SignaturePropertyType[] SignatureProperty { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="ID")]
        public string Id { get; set; }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Svc2CodeConverter", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class ManifestType
    {
        
        [System.Xml.Serialization.XmlElementAttribute("Reference", Order=0)]
        public ReferenceType[] Reference { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="ID")]
        public string Id { get; set; }
    }
}
