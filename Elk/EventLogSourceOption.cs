using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elk
{
    public class EventLogSourceOption
    {
        public EventLogSourceOption(string name, string host, string logName, string providerName)
        {
            this.Name = name;
            this.Host = host;
            this.LogName = logName;
            this.ProviderName = providerName;
        }

        public string Name { get; private set; }

        public string Host { get; private set; }

        public string LogName { get; private set; }

        public string ProviderName { get; private set; }

        public override string ToString()
        {
            return string.Format("Name: [{0}]; Host: [{1}]; LogName: [{2}]; Provider: [{3}]", Name, Host, LogName, ProviderName);
        }
    }

}
