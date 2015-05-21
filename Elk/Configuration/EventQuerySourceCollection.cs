using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elk.Configuration
{

    internal class EventQuerySourceCollection
        : ConfigurationElementCollection
    {
        internal EventQuerySourceElement this[int index]
        {
            get
            {
                return base.BaseGet(index) as EventQuerySourceElement;
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        internal new EventQuerySourceElement this[string responseString]
        {
            get { return (EventQuerySourceElement)BaseGet(responseString); }
            set
            {
                if (BaseGet(responseString) != null)
                {
                    BaseRemoveAt(BaseIndexOf(BaseGet(responseString)));
                }
                BaseAdd(value);
            }
        }

        protected override System.Configuration.ConfigurationElement CreateNewElement()
        {
            return new EventQuerySourceElement();
        }

        protected override object GetElementKey(System.Configuration.ConfigurationElement element)
        {
            return ((EventQuerySourceElement)element).Name;
        }
    }

}
