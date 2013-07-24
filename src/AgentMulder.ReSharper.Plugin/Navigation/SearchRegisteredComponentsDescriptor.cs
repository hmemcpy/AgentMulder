using System.Collections.Generic;
using JetBrains.ReSharper.Feature.Services.Search;
using JetBrains.ReSharper.Feature.Services.Search.SearchRequests;
using JetBrains.ReSharper.Features.Common.Occurences;
using JetBrains.ReSharper.Features.Finding.Search;
using JetBrains.Util;

namespace AgentMulder.ReSharper.Plugin.Navigation
{
    public class SearchRegisteredComponentsDescriptor : SearchDescriptor
    {
        public SearchRegisteredComponentsDescriptor(SearchRequest request, ICollection<IOccurence> results)
            : base(request, results)
        {
        }

        public override string GetResultsTitle(OccurenceSection section)
        {
            string singular = "registered component";
            int totalCount = section.TotalCount;
            string str;
            if (totalCount > 0)
            {
                if (totalCount > 1)
                {
                    singular = NounUtil.GetPlural(singular);
                }
                str = totalCount == section.FilteredCount
                    ? string.Format("Found {0} {1}", totalCount, singular)
                    : string.Format("Displaying {0} of {1} found {2}", section.FilteredCount, totalCount, singular);
            }
            else
            {
                str = string.Format("No registered components", new object[0]);
            }

            return str;

        }
    }
}