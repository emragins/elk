using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elk
{
    public class ElkEventLog
    {
        private string _logName;
        private string _providerName;
        private string _host;

        public ElkEventLog(string logName, string providerName, string host)
        {
            this._logName = logName;
            this._providerName = providerName;
            this._host = host;
        }

        private EventLogSession GetSession()
        {
            return new EventLogSession(_host);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="EventLogNotFoundException"></exception>
        internal List<EventRecord> RunQuery(ElkQuery elkQuery)
        {
            string queryString = elkQuery.Build(_providerName);

            System.Diagnostics.Debug.WriteLine(string.Format("Query String: {0}", queryString));

            List<EventRecord> eventRecords = new List<EventRecord>();
            EventRecord eventRecord;
            EventLogQuery query = new EventLogQuery(_logName, PathType.LogName, queryString);

            if (!string.IsNullOrWhiteSpace(_host))
                query.Session = GetSession();

            EventLogReader reader = new EventLogReader(query);
            while ((eventRecord = reader.ReadEvent()) != null)
            {
                eventRecords.Add(eventRecord);
            }

            return eventRecords;
        }


        public ElkQuery NewQuery()
        {
            return new ElkQuery(this);
        }
    }

}
