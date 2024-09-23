using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ClassDieselPrice : ClassNotify
    {
        private int _Id;
        private DateTime _date;
        private double _price;
        public ClassDieselPrice()
        {
			Id = 9000;
            date = DateTime.Now;
            price = 1.75803D;
        }
        public ClassDieselPrice(ClassDieselPrice inClassDieselPrice)
        {
            Id= inClassDieselPrice.Id;
			date = inClassDieselPrice.date;
			price = inClassDieselPrice.price;
        }


        public double price
		{
			get { return _price; }
			set
			{
				if (_price != value)
				{
					_price = value;
				}
				Notify("price");
			}
		}


		public DateTime date
		{
			get { return _date; }
			set
			{
				if (_date != value)
				{
					_date = value;
				}
				Notify("date");
			}
		}

		public int Id
		{
			get { return _Id; }
			set
			{
				if (_Id != value)
				{
					_Id = value;
				}
				Notify("Id");
			}
		}

	}
}
