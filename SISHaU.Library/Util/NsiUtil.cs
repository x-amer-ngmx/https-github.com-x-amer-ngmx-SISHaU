using System.Collections.Generic;
using System.Linq;
using Integration.NsiBase;
using Integration.NsiCommon;

namespace SISHaU.Library.Util
{
    public class NsiUtil : GisUtil
    {
        protected nsiRef GetNsiRef(exportNsiItemResult response, string strFilter, string strFilterItem)
        {
            var res = from t in response.NsiItem.NsiElement.Where(ne => ne.IsActual)
                select new
                {
                    type =
                    t.NsiElementField.OfType<NsiElementStringFieldType>()
                        .First(nt => string.IsNullOrEmpty(strFilter) || nt.Name.Equals(strFilter))
                        .Value,
                    name =
                    t.NsiElementField.OfType<NsiElementStringFieldType>()
                        .First(nt => string.IsNullOrEmpty(strFilter) || nt.Name.Equals(strFilter))
                        .Name,
                    guid = t.GUID,
                    code = t.Code
                }
                into g
                where string.IsNullOrEmpty(strFilter) || g.name.Equals(strFilter) && g.type.Equals(strFilterItem)
                select new nsiRef { Name = g.type, GUID = g.guid, Code = g.code };

            var nsiRefs = res as nsiRef[] ?? res.ToArray();
            return nsiRefs.Any() ? nsiRefs.First() : null;
        }

        public Dictionary<string, nsiRef> GetNsiRefList(exportNsiItemResult response, string strFilter)
        {
            var res = from t in response.NsiItem.NsiElement.Where(ne => ne.IsActual)
                select new
                {
                    type =
                    t.NsiElementField.OfType<NsiElementStringFieldType>()
                        .First(nt => string.IsNullOrEmpty(strFilter) || nt.Name.Equals(strFilter))
                        .Value,
                    name =
                    t.NsiElementField.OfType<NsiElementStringFieldType>()
                        .First(nt => string.IsNullOrEmpty(strFilter) || nt.Name.Equals(strFilter))
                        .Name,
                    guid = t.GUID,
                    code = t.Code
                }
                into g
                where string.IsNullOrEmpty(strFilter) || g.name.Equals(strFilter)
                select new
                {
                    key = g.type,
                    value = new nsiRef { Name = g.type, GUID = g.guid, Code = g.code }

                };

            return res.ToDictionary(t => t.key, t => t.value);
        }

        public Dictionary<string, string> GetNsiList(exportNsiItemResult response, string strFilter)
        {
            var res = from t in response.NsiItem.NsiElement.Where(ne => ne.IsActual)
                select new
                {
                    type =
                    t.NsiElementField.OfType<NsiElementStringFieldType>()
                        .First(nt => string.IsNullOrEmpty(strFilter) || nt.Name.Equals(strFilter))
                        .Value,
                    name =
                    t.NsiElementField.OfType<NsiElementStringFieldType>()
                        .First(nt => string.IsNullOrEmpty(strFilter) || nt.Name.Equals(strFilter))
                        .Name,
                    guid = t.GUID
                }
                into g
                where string.IsNullOrEmpty(strFilter) || g.name.Equals(strFilter)
                select new { key = g.type, value = g.guid };

            return res.ToDictionary(t => t.key, t => t.value);
        }
    }
}
