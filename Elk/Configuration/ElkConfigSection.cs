using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elk.Configuration
{
    internal class ElkConfigSection : ConfigurationSection
    {
        internal static ElkConfigSection GetConfig()
        {
            return (ElkConfigSection)System.Configuration.ConfigurationManager.GetSection("elk")
                ?? new ElkConfigSection();
        }

        [ConfigurationProperty("eventLogSources", IsDefaultCollection = true)]
        public EventQuerySourceCollection EventSourceCollection
        {
            get { return (EventQuerySourceCollection)base["eventLogSources"]; }
        }
    }
}
