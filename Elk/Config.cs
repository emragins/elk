using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elk.Configuration;

namespace Elk
{
    public static class Config
    {
        public static List<EventLogSourceOption> SourceOptions = new List<EventLogSourceOption>();

        public static void LoadConfiguration()
        {
            //ElkConfigSectionGroup config = ElkConfigSectionGroup.GetConfig();
            ElkConfigSection config = ElkConfigSection.GetConfig();
            var enumerator = config.EventSourceCollection.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var item = (EventQuerySourceElement)enumerator.Current;
                SourceOptions.Add(new EventLogSourceOption(item.Name, item.Host, item.LogName, item.ProviderName));
            }

        }

    }
}
