using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;

namespace Elk
{
    public class ElkQuery
    {
        private Dictionary<string, string> _args;
        private string _builtQuery = string.Empty;
        private ElkEventLog _source;

        internal ElkQuery(ElkEventLog source)
        {
            _source = source;
            _args = new Dictionary<string, string>();
        }

        public ElkQuery FilterByLogLevel(LogLevels level)
        {
            if (level == LogLevels.AllLevels)
                return this;

            AddOrUpdate("LEVEL", "(Level = {0})", (int)level);
            return this;
        }

        public ElkQuery FilterByTimeAgo(TimeSpan timeAgo)
        {
            var time = DateTime.Now.Subtract(timeAgo);
            AddOrUpdate("TIME_GT", "TimeCreated[@SystemTime >= '{0}']", FormatTime(time));
            return this;
        }

        public ElkQuery FilterByStartTime(DateTime startTime)
        {
            AddOrUpdate("TIME_GT", "TimeCreated[@SystemTime >= '{0}']", FormatTime(startTime));
            return this;
        }


        public ElkQuery FilterByEndTime(DateTime endTime)
        {
            AddOrUpdate("TIME_LT", "TimeCreated[@SystemTime <= '{0}']", FormatTime(endTime));
            return this;
        }

        public List<EventRecord> Run()
        {
            return _source.RunQuery(this);
        }

        internal string Build(string providerName)
        {
            if (!string.IsNullOrEmpty(_builtQuery)) return _builtQuery;

            var templ = "*[System[{0} and Provider[@Name = '{1}'] {2}]]";

            string level = _args.ContainsKey("LEVEL") ? level = _args["LEVEL"] : "";

            string latterHalf = string.Join(" and ", _args.Where(kvp => kvp.Key != "LEVEL").Select(kvp => kvp.Value));
            string query = string.Format(templ, level, providerName,
                string.IsNullOrWhiteSpace(latterHalf) ? "" : " and " + latterHalf
                );

            //return query.Replace("  ", " ");
            return query;
        }


        private string FormatTime(DateTime dateTime)
        {
            return dateTime.ToUniversalTime().ToString("o");
        }

        private void AddOrUpdate(string key, string value, params object[] valueArgs)
        {
            value = string.Format(value, valueArgs);

            if (!_args.ContainsKey(key))
            {
                _args.Add(key, value);
            }
            else
                _args[key] = value;
        }
    }
}
