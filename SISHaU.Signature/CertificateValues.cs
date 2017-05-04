// CertificateValues.cs
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
using System.Collections;
using System.Xml;

namespace SISHaU.Signature
{
	/// <summary>
	/// The CertificateValues element contains the full set of certificates
	/// that have been used to validate	the electronic signature, including the
	/// signer's certificate. However, it is not necessary to include one of
	/// those certificates into this property, if the certificate is already
	/// present in the ds:KeyInfo element of the signature.
	/// In fact, both the signer certificate (referenced in the mandatory
	/// SigningCertificate property element) and all certificates referenced in
	/// the CompleteCertificateRefs property element must be present either in
	/// the ds:KeyInfo element of the signature or in the CertificateValues
	/// property element.
	/// </summary>
	public class CertificateValues
	{
		#region Private variables
		private string _id;
		private EncapsulatedX509CertificateCollection _encapsulatedX509CertificateCollection;
		private OtherCertificateCollection _otherCertificateCollection;
		#endregion

		#region Public properties

		/// <summary>
		/// Optional Id of the certificate values element
		/// </summary>
		public string Id
		{
			get
			{
				return _id;
			}
			set
			{
				_id = value;
			}
		}

		/// <summary>
		/// A collection of encapsulated X509 certificates
		/// </summary>
		public EncapsulatedX509CertificateCollection EncapsulatedX509CertificateCollection
		{
			get
			{
				return _encapsulatedX509CertificateCollection;
			}
			set
			{
				_encapsulatedX509CertificateCollection = value;
			}
		}

		/// <summary>
		/// Collection of other certificates
		/// </summary>
		public OtherCertificateCollection OtherCertificateCollection
		{
			get
			{
				return _otherCertificateCollection;
			}
			set
			{
				_otherCertificateCollection = value;
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Default constructor
		/// </summary>
		public CertificateValues()
		{
			_encapsulatedX509CertificateCollection = new EncapsulatedX509CertificateCollection();
			_otherCertificateCollection = new OtherCertificateCollection();
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Check to see if something has changed in this instance and needs to be serialized
		/// </summary>
		/// <returns>Flag indicating if a member needs serialization</returns>
		public bool HasChanged()
		{
			bool retVal = false;

			if (_id != null && _id != "")
			{
				retVal = true;
			}
			if (_encapsulatedX509CertificateCollection.Count > 0)
			{
				retVal = true;
			}
			if (_otherCertificateCollection.Count > 0)
			{
				retVal = true;
			}

			return retVal;
		}

		/// <summary>
		/// Load state from an XML element
		/// </summary>
		/// <param name="xmlElement">XML element containing new state</param>
		public void LoadXml(XmlElement xmlElement)
		{
			XmlNamespaceManager xmlNamespaceManager;
		    IEnumerator enumerator = null;
			XmlElement iterationXmlElement;

		    if (xmlElement == null)
			{
				throw new ArgumentNullException("xmlElement");
			}
			if (xmlElement.HasAttribute("Id"))
			{
				_id = xmlElement.GetAttribute("Id");
			}
			else
			{
				_id = "";
			}

			xmlNamespaceManager = new XmlNamespaceManager(xmlElement.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("xades", XadesSignedXml.XadesNamespaceUri);

			_encapsulatedX509CertificateCollection.Clear();
			_otherCertificateCollection.Clear();

			var xmlNodeList = xmlElement.SelectNodes("xades:EncapsulatedX509Certificate", xmlNamespaceManager);
		    if (xmlNodeList != null)
		    {
		        enumerator = xmlNodeList.GetEnumerator();
		        try 
		        {
		            while (enumerator != null && enumerator.MoveNext()) 
		            {
		                iterationXmlElement = enumerator.Current as XmlElement;
		                if (iterationXmlElement == null) continue;
		                var newEncapsulatedX509Certificate = new EncapsulatedX509Certificate();
		                newEncapsulatedX509Certificate.LoadXml(iterationXmlElement);
		                _encapsulatedX509CertificateCollection.Add(newEncapsulatedX509Certificate);
		            }
		        }
		        finally 
		        {
		            var disposable = enumerator as IDisposable;
		            if (disposable != null)
		            {
		                disposable.Dispose();
		            }
		        }
		    }

		    xmlNodeList = xmlElement.SelectNodes("xades:OtherCertificate", xmlNamespaceManager);
		    if (xmlNodeList != null) enumerator = xmlNodeList.GetEnumerator();
		    try 
			{
				while (enumerator != null && enumerator.MoveNext()) 
				{
					iterationXmlElement = enumerator.Current as XmlElement;
				    if (iterationXmlElement == null) continue;
				    var newOtherCertificate = new OtherCertificate();
				    newOtherCertificate.LoadXml(iterationXmlElement);
				    _otherCertificateCollection.Add(newOtherCertificate);
				}
			}
			finally 
			{
				var disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		/// <summary>
		/// Returns the XML representation of the this object
		/// </summary>
		/// <returns>XML element containing the state of this object</returns>
		public XmlElement GetXml()
		{
		    var creationXmlDocument = new XmlDocument();
            var retVal = creationXmlDocument.CreateElement("xades", "CertificateValues", XadesSignedXml.XadesNamespaceUri);
			if (!string.IsNullOrEmpty(_id))
			{
				retVal.SetAttribute("Id", _id);
			}

			if (_encapsulatedX509CertificateCollection.Count > 0)
			{
				foreach (EncapsulatedX509Certificate encapsulatedX509Certificate in _encapsulatedX509CertificateCollection)
				{
					if (encapsulatedX509Certificate.HasChanged())
					{
						retVal.AppendChild(creationXmlDocument.ImportNode(encapsulatedX509Certificate.GetXml(), true));
					}
				}
			}
			if (_otherCertificateCollection.Count > 0)
			{
				foreach (OtherCertificate otherCertificate in _otherCertificateCollection)
				{
					if (otherCertificate.HasChanged())
					{
						retVal.AppendChild(creationXmlDocument.ImportNode(otherCertificate.GetXml(), true));
					}
				}
			}

			return retVal;
		}
		#endregion
	}
}
