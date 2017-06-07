using System.Collections.Generic;
using System.Linq;
using Integration.NsiBase;
using Integration.NsiCommon;

namespace SISHaU.Library.Util
{
    /// <summary>
    /// Рефлексия творит чудеса
    /// </summary>
    public class NsiUtil : GisUtil
    {
        protected nsiRef GetNsiRef(exportNsiItemResult response, string strFilter, string strFilterItem)
        {
            var list = GetNsiRefList(response, strFilter);
            return list.ContainsKey(strFilterItem) ? list[strFilterItem] : null;
        }

        public Dictionary<string, nsiRef> GetNsiRefList(exportNsiItemResult response, string strFilter)
        {
            return GetNsiObjs(response, strFilter)
                .ToDictionary(t => GetPropValue(t.NsiElementField.First(f => f.Name.Equals(strFilter)), "Value") as string,
                    t => new nsiRef { Code = t.Code, Name = GetPropValue(t, "Name") as string, GUID = t.GUID });
        }

        public Dictionary<string, string> GetNsiList(exportNsiItemResult response, string strFilter)
        {
            return GetNsiObjs(response, strFilter)
                .ToDictionary(t => GetPropValue(t.NsiElementField.First(f => f.Name.Equals(strFilter)), "Value") as string,
                    t => t.GUID);
        }

        private IEnumerable<NsiElementType> GetNsiObjs(exportNsiItemResult response, string strFilter)
        {
            return response.NsiItem.NsiElement
                .Where(ne => ne.IsActual && ne.NsiElementField.Any(t => string.IsNullOrEmpty(strFilter) || t.Name.Equals(strFilter)))
                .Select(t => t);
        }
    }
}
