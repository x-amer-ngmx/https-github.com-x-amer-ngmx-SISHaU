// Cert.cs
//
// XAdES Starter Kit for Microsoft .NET 3.5 (and above)
// 2010 Microsoft France
// Published under the CECILL-B Free Software license agreement.
// (http://www.cecill.info/licences/Licence_CeCILL-B_V1-en.txt)
// 
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// WHETHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE. 
// THE ENTIRE RISK OF USE OR RESULTS IN CONNECTION WITH THE USE OF THIS CODE 
// AND INFORMATION REMAINS WITH THE USER. 
//

using System;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace SISHaU.Signature
{
	/// <summary>
	/// This class contains certificate identification information
	/// </summary>
	public class Cert
	{
		#region Private variables
		private DigestAlgAndValueType _certDigest;
		private IssuerSerial _issuerSerial;
		#endregion

		#region Public properties
		/// <summary>
		/// The element CertDigest contains the digest of one of the
		/// certificates referenced in the sequence
		/// </summary>
		public DigestAlgAndValueType CertDigest
		{
			get
			{
				return _certDigest;
			}
			set
			{
				_certDigest = value;
			}
		}

		/// <summary>
		/// The element IssuerSerial contains the identifier of one of the
		/// certificates referenced in the sequence. Should the
		/// X509IssuerSerial element appear in the signature to denote the same
		/// certificate, its value MUST be consistent with the corresponding
		/// IssuerSerial element.
		/// </summary>
		public IssuerSerial IssuerSerial
		{
			get
			{
				return _issuerSerial;
			}
			set
			{
				_issuerSerial = value;
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Default constructor
		/// </summary>
		public Cert()
		{
			_certDigest = new DigestAlgAndValueType("CertDigest");
			_issuerSerial = new IssuerSerial();
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Check to see if something has changed in this instance and needs to be serialized
		/// </summary>
		/// <returns>Flag indicating if a member needs serialization</returns>
		public bool HasChanged()
		{
		    return _certDigest != null && _certDigest.HasChanged() || _issuerSerial != null && _issuerSerial.HasChanged();
		}

		/// <summary>
		/// Load state from an XML element
		/// </summary>
		/// <param name="xmlElement">XML element containing new state</param>
		public void LoadXml(XmlElement xmlElement)
		{
		    XmlNodeList xmlNodeList = null;
			
			if (xmlElement == null)
			{
				throw new ArgumentNullException("xmlElement");
			}

		    if (xmlElement.OwnerDocument != null)
		    {
		        var xmlNamespaceManager = new XmlNamespaceManager(xmlElement.OwnerDocument.NameTable);
		        xmlNamespaceManager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
		        xmlNamespaceManager.AddNamespace("xsd", XadesSignedXml.XadesNamespaceUri);

		        xmlNodeList = xmlElement.SelectNodes("xsd:CertDigest", xmlNamespaceManager);
		        if (xmlNodeList != null && xmlNodeList.Count == 0)
		        {
		            throw new CryptographicException("CertDigest missing");
		        }
		        _certDigest = new DigestAlgAndValueType("CertDigest");
		        if (xmlNodeList != null) _certDigest.LoadXml((XmlElement)xmlNodeList.Item(0));

		        xmlNodeList = xmlElement.SelectNodes("xsd:IssuerSerial", xmlNamespaceManager);
		    }
		    if (xmlNodeList != null && xmlNodeList.Count == 0)
			{
				throw new CryptographicException("IssuerSerial missing");
			}
			_issuerSerial = new IssuerSerial();
		    if (xmlNodeList != null) _issuerSerial.LoadXml((XmlElement)xmlNodeList.Item(0));
		}

		/// <summary>
		/// Returns the XML representation of the this object
		/// </summary>
		/// <returns>XML element containing the state of this object</returns>
		public XmlElement GetXml()
		{
		    var creationXmlDocument = new XmlDocument();
            var retVal = creationXmlDocument.CreateElement("xades", "Cert", XadesSignedXml.XadesNamespaceUri);

			if (_certDigest != null && _certDigest.HasChanged())
			{
				retVal.AppendChild(creationXmlDocument.ImportNode(_certDigest.GetXml(), true));
			}

			if (_issuerSerial != null && _issuerSerial.HasChanged())
			{
				retVal.AppendChild(creationXmlDocument.ImportNode(_issuerSerial.GetXml(), true));
			}

			return retVal;
		}
		#endregion
	}
}