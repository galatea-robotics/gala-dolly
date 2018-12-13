using System;
using System.Collections.ObjectModel;
using Galatea.AI.Abstract;
using Galatea.AI.Characterization;

namespace Gala.Data.Runtime
{
    internal sealed class FeedbackCache : KeyedCollection<string, FeedbackNode>, IFeedbackCache
    {
        protected override string GetKeyForItem(FeedbackNode item)
        {
            if (item == null) throw new Galatea.TeaArgumentNullException(nameof(item));
            return item.BaseTemplate.Name;
        }

        void IFeedbackCache.Add(BaseTemplate item)
        {
            base.Add(new FeedbackNode(item));
        }
    }
}
