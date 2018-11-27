using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gala.Data.Runtime
{
    using Galatea.AI.Abstract;

    static class Queries
    {
        public static void Initialize()
        {
            Galatea.Runtime.Queries.GetEqualCompareValueItems = GetEqualCompareValueItems;
        }

        private static IList<BaseTemplate> GetEqualCompareValueItems(BaseTemplate value, IBaseTemplateCollection collection)
        {
            var items = collection
                .GetBaseTemplateItems()
                .Where(t => t.CompareValue == value.CompareValue);

            var result = items
                .OrderBy(t => t.ID);

            IList<BaseTemplate> resultList = result.ToList();

            return resultList;
        }
    }
}
