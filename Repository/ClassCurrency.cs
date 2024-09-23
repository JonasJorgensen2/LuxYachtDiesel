using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ClassCurrency : ClassNotify
    {
        private Dictionary<string, decimal> _rates;
        private string _disclaimer;
        private string _license;
        private string _base;
        private long _timestamp;
        public ClassCurrency()
        {
            rates = new Dictionary<string, decimal>();
			disclaimer = "";
			license = "";
			Base = "";
			timestamp = 0L;
        }
        public ClassCurrency(ClassCurrency inClassCurrency)
        {
            rates = inClassCurrency.rates;
			disclaimer = inClassCurrency.disclaimer;
			license = inClassCurrency.license;
			Base = inClassCurrency.Base;
			timestamp = inClassCurrency.timestamp;
        }

		
        public long timestamp
		{
			get { return _timestamp; }
			set
			{
				if (_timestamp != value)
				{
					_timestamp = value;
				}
				Notify("timestamp");
			}
		}

		[JsonProperty("base")]
		public string Base
		{
			get { return _base; }
			set
			{
				if (_base != value)
				{
					_base = value;
				}
				Notify("myVar");
			}
		}


		public string license
		{
			get { return _license; }
			set
			{
				if (_license != value)
				{
					_license = value;
				}
				Notify("license");
			}
		}


		public string disclaimer
		{
			get { return _disclaimer; }
			set
			{
				if (_disclaimer != value)
				{
					_disclaimer = value;
				}
				Notify("disclaimer");
			}
		}


		public Dictionary<string, decimal> rates
		{
			get { return _rates; }
			set
			{
				if (_rates != value)
				{
					_rates = value;
				}
				Notify("rates");
			}
		}

	}
}
