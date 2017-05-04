using System;
using System.Configuration;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;
using CryptoPro.Sharpei;
using CryptoPro.Sharpei.Xml;

namespace SISHaU.Signature.Gis
{
    /// <summary>
    /// Вспомогательный класс ключ/значение
    /// </summary>
    public class ReplacementPair
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public static class GisSignatureHelper
    {
        private const bool PreserveWhitespace = true;

        private static X509Certificate2 FindCertificate(string thumbPrint)
        {
            var store = new X509Store("MY", StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

            var fcollection = store.Certificates.Find(
                X509FindType.FindByThumbprint, thumbPrint, false);

            var cert = new X509Certificate2();

            foreach (var x509 in fcollection)
            {
                cert = x509;
            }

            return cert;
        }

        public static string SignXml(string xmlText)
        {
            var tp = ConfigurationManager.AppSettings["certificate-thumbprint"].ToUpper();
            var key = ConfigurationManager.AppSettings["container-key"];
            var cert = FindCertificate(tp);

            return xmlText.Contains("signed-data-container") ?  GetSignedRequestXades(xmlText, cert, key) : xmlText;
        }

        private static string GetSignedRequestXades(string request, X509Certificate2 certificate, string privateKeyPassword)
        {
            var originalDoc = new XmlDocument { PreserveWhitespace = PreserveWhitespace };

            originalDoc.LoadXml(request);

            var signatureid = $"xmldsig-{Guid.NewGuid().ToString().ToLower()}";
            var signedXml = GetXadesSignedXml(certificate, originalDoc, signatureid, privateKeyPassword);

            var rawCertData = Convert.ToBase64String(certificate.GetRawCertData());

            var keyInfo = GetKeyInfo(rawCertData);
            signedXml.KeyInfo = keyInfo;

            var xadesInfo = GetXadesInfo(rawCertData);

            var xadesObject = GetXadesObject(xadesInfo, signatureid);
            signedXml.AddXadesObject(xadesObject);

            signedXml.ComputeSignature();

            InjectSignatureToOriginalDoc(signedXml, originalDoc);

            return originalDoc.OuterXml;
        }

        private static XadesInfo GetXadesInfo(string rawPk)
        {
            var xadesInfo = new XadesInfo
            {
                RawPk = rawPk,
                SigningDateTimeUtc = DateTime.UtcNow
            };
            var delta = TimeZoneInfo.Local.GetUtcOffset(DateTime.Now);
            xadesInfo.TimeZoneOffsetMinutes = Convert.ToInt32(delta.TotalMinutes);
            return xadesInfo;
        }

        private static void InjectSignatureToOriginalDoc(XadesSignedXml signedXml, XmlDocument originalDoc)
        {
            var xmlSig = signedXml.GetXml();
            var signedDataContainer = signedXml.GetIdElement(originalDoc, "signed-data-container");
            signedDataContainer?.InsertBefore(originalDoc.ImportNode(xmlSig, true), signedDataContainer.FirstChild);
        }

        /// <summary>
        /// Исправить строку X509IssuerName для рукожопых писателей из Ланита
        /// </summary>
        /// <param name="x509IssuerName">Исходная строка из сертификата</param>
        /// <returns>Исправленная строка, чтобы ее понимал сервер ГИС ЖКХ</returns>
        private static string IssuerNamePatcher(string x509IssuerName)
        {
            const string sc = "^_^";
            const string rc = "\\,";
            var pairs = x509IssuerName.Replace(rc, sc).Split(',').Select(part => part.Split('=')).Select(lrParts => new ReplacementPair
            {
                Key = lrParts[0], Value = lrParts.Length==2 ? lrParts[1] : string.Empty
            }).ToList();

            var nCount = pairs.Count;
            var result = new StringBuilder();
            var i = 0;

            foreach (var pair in pairs)
            {
                switch (pair.Key.ToLower())
                {
                    case "t":
                    case "title":
                        pair.Key = "2.5.4.12";
                        break;

                    case "g":
                    case "givenname":
                        pair.Key = "2.5.4.42";
                        break;

                    case "sn":
                    case "surname":
                        pair.Key = "2.5.4.4";
                        break;

                    case "ou":
                    case "orgunit":
                        pair.Key = "2.5.4.11";
                        break;

                    case "unstructured-name":
                    case "unstructuredname":
                        pair.Key = "1.2.840.113549.1.9.2";
                        break;
                }

                result.Append($"{pair.Key}={pair.Value}{(i != (nCount - 1) ? ", " : string.Empty)}");

                i++;
            }

            return result.ToString().Replace(sc,rc);
        }

        private static XadesObject GetXadesObject(XadesInfo xadesInfo, string signatureid)
        {
            var xadesObject = new XadesObject
            {
                QualifyingProperties =
                {
                    Target = $"#{signatureid}",
                    SignedProperties = {Id = $"{signatureid}-signedprops"}
                }
            };

            var signedSignatureProperties = xadesObject.QualifyingProperties.SignedProperties.SignedSignatureProperties;


            var x509CertificateParser = new Org.BouncyCastle.X509.X509CertificateParser();
            var bouncyCert = x509CertificateParser.ReadCertificate(Convert.FromBase64String(xadesInfo.RawPk));

            var x509IssuerDn = GetOidRepresentation(bouncyCert.IssuerDN.ToString());

            x509IssuerDn = IssuerNamePatcher(x509IssuerDn);

            var cert = new Cert
            {
                IssuerSerial =
                {
                    X509IssuerName = x509IssuerDn,
                    X509SerialNumber = bouncyCert.SerialNumber.ToString()
                }
            };

            cert.CertDigest.DigestMethod.Algorithm = CPSignedXml.XmlDsigGost3411UrlObsolete;

            var rawCertData = Convert.FromBase64String(xadesInfo.RawPk);
            var pkHash = HashAlgorithm.Create("GOST3411");

            if (pkHash != null)
            {
                var hashValue = pkHash.ComputeHash(rawCertData);
                cert.CertDigest.DigestValue = hashValue;
            }

            signedSignatureProperties.SigningCertificate.CertCollection.Add(cert);

            signedSignatureProperties.SigningTime = xadesInfo.SigningDateTimeUtc.AddMinutes(xadesInfo.TimeZoneOffsetMinutes);
            return xadesObject;
        }

        private static XadesSignedXml GetXadesSignedXml(X509Certificate2 certificate, XmlDocument originalDoc, string signatureid, string privateKeyPassword)
        {
            var secureString = new SecureString();
            foreach (var ch in privateKeyPassword)
                secureString.AppendChar(ch);

            var provider = (Gost3410CryptoServiceProvider)certificate.PrivateKey;
            provider.SetContainerPassword(secureString);

            var signedXml = new XadesSignedXml(originalDoc) { SigningKey = provider };

            signedXml.Signature.Id = signatureid;
            signedXml.SignatureValueId = $"{signatureid}-sigvalue";

            var reference = new Reference
            {
                Uri = "#signed-data-container",
                DigestMethod = CPSignedXml.XmlDsigGost3411UrlObsolete,
                Id = $"{signatureid}-ref0"
            };
            reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
            reference.AddTransform(new XmlDsigExcC14NTransform());
            signedXml.AddReference(reference);

            signedXml.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigCanonicalizationUrl;
            signedXml.SignedInfo.SignatureMethod = CPSignedXml.XmlDsigGost3410UrlObsolete;

            return signedXml;
        }

        private static KeyInfo GetKeyInfo(string rawPkString)
        {
            var keyInfo = new KeyInfo();

            var doc = new XmlDocument();
            var keyInfoElement = (XmlElement)doc.AppendChild(doc.CreateElement("ds", "KeyInfo", "http://www.w3.org/2000/09/xmldsig#"));
            var x509DataElement = doc.CreateElement("ds", "X509Data", "http://www.w3.org/2000/09/xmldsig#");
            var x509DataNode = keyInfoElement.AppendChild(x509DataElement);
            x509DataNode.AppendChild(doc.CreateElement("ds", "X509Certificate", "http://www.w3.org/2000/09/xmldsig#")).InnerText =
                rawPkString;

            keyInfo.AddClause(new KeyInfoNode(x509DataElement));
            //keyInfo.AddClause(new KeyInfoX509Data(certificate));
            return keyInfo;
        }

        /// <summary>
        /// Заменяет части IssuerName на OID. https://technet.microsoft.com/en-us/library/cc772812(WS.10).aspx
        /// </summary>
        /// <param name="issuerName"></param>
        /// <returns></returns>
        private static string GetOidRepresentation(string issuerName)
        {
            var result = issuerName;
            result = result.Replace("E=", "1.2.840.113549.1.9.1=");
            result = result.Replace("unstructuredName=", "1.2.840.113549.1.9.2=");
            return result;
        }
    }
}
