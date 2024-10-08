﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IO;
using Repository;

namespace BIZ
{
    public class ClassBIZ:ClassNotify
    {
        ClassCallWebAPI CCWA = new ClassCallWebAPI();
        private ClassCurrency _currency;
        private int _volume;
        private List<ClassCustomer> _listCustomers;
        private ClassCustomer _selectedCustomer;
        private ClassCustomer _fallbackCustomer;
		private ClassLuxYachtDieselDB _classDB;
        private List<ClassSupplier> _listSuppliers;
        private ClassSupplier _selectedSupplier;
        private ClassSupplier _fallbackSupplier;
        private ClassOrder _order;
        private ClassDieselPrice _dieselPrice;
        private bool _editingLock;
        private List<ClassCountry> _ListCountries;
        public ClassBIZ()
        {
			volume=0;
            order = new ClassOrder();
            dieselPrice= new ClassDieselPrice();
			_classDB = new ClassLuxYachtDieselDB();
			currency = new ClassCurrency();        
            listCustomers = new List<ClassCustomer>(GetAllCustomerForListFromDB());
			selectedCustomer = new ClassCustomer();
			fallbackCustomer = new ClassCustomer();
			listSuppliers=new List<ClassSupplier>(GetAllSuppliersForListFromDB());
			selectedSupplier= new ClassSupplier();
			fallbackSupplier=new ClassSupplier();
			ListCountries= new List<ClassCountry>(_classDB.GetAllCountryFromDB());
			editingLock = true;
        }
		

		public int volume
		{
			get { return _volume; }
			set
			{
				if (_volume != value)
				{
					_volume = value;
					if (order != null)
					{
						order.volume = value;
						if (order.customer.Id>0 && order.volume >=0 && order.supplier.Id>0 && order.price >0)
						{
							order.customerRate=((order.volume * order.price)+(0.148 * (order.volume * order.price))) * Convert.ToDouble(currency.rates[$"{order.customer.country.currencyCode}"]);
							order.supplierRate=(order.volume * order.price) * Convert.ToDouble(currency.rates[$"{order.supplier.country.currencyCode}"]);
							order.ownProfit=((order.volume * order.price)+(0.148 * (order.volume * order.price))-(order.volume *order.price)) * Convert.ToDouble(currency.rates[$"DKK"]);
                        } 
					}
                }
				Notify("volume");
			}
		}
		

		public bool editingLock
		{
			get { return _editingLock; }
			set
			{
				if (_editingLock != value)
				{
					_editingLock = value;
				}
				Notify("editingLock");
			}
		}



		#region Customer Data Properties



		public ClassCustomer fallbackCustomer
		{
			get { return _fallbackCustomer; }
			set
			{
				if (_fallbackCustomer != value)
				{
					_fallbackCustomer = value;
				}
				Notify("fallbackCustomer");
			}
		}


		public ClassCustomer selectedCustomer
		{
			get { return _selectedCustomer; }
			set
			{
				if (_selectedCustomer != value)
				{
					_selectedCustomer = value;
					if (editingLock == true)
					{
						if (selectedCustomer.Id>0)
						{
							order.customer= new ClassCustomer(selectedCustomer);
							//order.customerRate = Convert.ToDouble(currency.rates[$"{order.customer.country.currencyCode}"]);
							if (order.customer.Id>0 && order.volume >=0 && order.supplier.Id>0 && order.price >0)
							{
								order.customerRate=((order.volume * order.price)+(0.148 * (order.volume * order.price))) * Convert.ToDouble(currency.rates[$"{order.customer.country.currencyCode}"]);
								order.supplierRate=(order.volume * order.price) * Convert.ToDouble(currency.rates[$"{order.supplier.country.currencyCode}"]);
								order.ownProfit=((order.volume * order.price)+(0.148 * (order.volume * order.price))-(order.volume *order.price)) * Convert.ToDouble(currency.rates[$"DKK"]);

							}
						} 
					}
                }
				Notify("selectedCustomer");
			}
		}



		public List<ClassCustomer> listCustomers
		{
			get { return _listCustomers; }
			set
			{
				if (_listCustomers != value)
				{
					_listCustomers = value;
				}
				Notify("listCustomers");
			}
		}
		#endregion
		#region Supplier Data Properties
		

		public ClassSupplier fallbackSupplier
		{
			get { return _fallbackSupplier; }
			set
			{
				if (_fallbackSupplier != value)
				{
					_fallbackSupplier = value;
				}
				Notify("fallbackSupplier");
			}
		}


		public ClassSupplier selectedSupplier
		{
			get { return _selectedSupplier; }
			set
			{
				if (_selectedSupplier != value)
				{
					_selectedSupplier = value;
					if (editingLock == true)
					{
						if (selectedSupplier.Id>0)
						{
							order.supplier = new ClassSupplier(_selectedSupplier);
							//order.supplierRate = Convert.ToDouble(currency.rates[$"{order.supplier.country.currencyCode}"]);
							if (order.customer.Id>0 && order.volume >=0 && order.supplier.Id>0 && order.price >0)
							{
                                order.customerRate=((order.volume * order.price)+(0.148 * (order.volume * order.price))) * Convert.ToDouble(currency.rates[$"{order.customer.country.currencyCode}"]);
                                order.supplierRate=(order.volume * order.price) * Convert.ToDouble(currency.rates[$"{order.supplier.country.currencyCode}"]);
                                order.ownProfit=((order.volume * order.price)+(0.148 * (order.volume * order.price))-(order.volume *order.price)) * Convert.ToDouble(currency.rates[$"DKK"]);

                            }
						} 
					}
				}
				Notify("selectedSupplier");
			}
		}


		public List<ClassSupplier> listSuppliers
		{
			get { return _listSuppliers; }
			set
			{
				if (_listSuppliers != value)
				{
					_listSuppliers = value;
				}
				Notify("listSuppliers");
			}
		}


		#endregion
		#region Order Data Properties

		

		public ClassOrder order
		{
			get { return _order; }
			set
			{
				if (_order != value)
				{
					_order = value;
                   
                }
				Notify("order");
			}
		}

		#endregion
		#region Oil Data Properties
		

		public ClassDieselPrice dieselPrice
		{
			get { return _dieselPrice; }
			set
			{
				if (_dieselPrice != value)
				{
					_dieselPrice = value;
					order.price = dieselPrice.price;
				}
				Notify("dieselPrice");
			}
		}

		#endregion
		#region Customer Data Methods

		public List<ClassCustomer> GetAllCustomerForListFromDB()
		{
			List<ClassCustomer> templist = new List<ClassCustomer>(_classDB.GetAllCustomersFromDB());
			return templist;

		}
		public void UpdateOrInsertCustomerInDB()
		{
			if (selectedCustomer.Id == 0)
			{
				_classDB.SaveCustomersInDB(fallbackCustomer);
				selectedCustomer = new ClassCustomer();
				listCustomers.Clear();
				listCustomers = new List<ClassCustomer>(GetAllCustomerForListFromDB());

			}
			else
			{
				_classDB.UpdateCustomerInDB(fallbackCustomer);
                selectedCustomer = new ClassCustomer();
                listCustomers.Clear();
                listCustomers = new List<ClassCustomer>(GetAllCustomerForListFromDB());
				SelectCustomerInListView(fallbackCustomer.Id);
            }
		}
		public void RegretUpdateOrNewCustomerInDB()
		{
			selectedCustomer = new ClassCustomer();
            listCustomers.Clear();
            listCustomers = new List<ClassCustomer>(GetAllCustomerForListFromDB());
            SelectCustomerInListView(fallbackCustomer.Id);
        }
		private void SelectCustomerInListView(int inId)
		{
			foreach (ClassCustomer cc in listCustomers)
			{
				if(cc.Id == inId)
				{
					selectedCustomer = cc;
					break;
				}
			}
		}
		
		#endregion
		#region Supplier Data Methods
		public List<ClassSupplier> GetAllSuppliersForListFromDB()
		{
			List<ClassSupplier> templist =new List<ClassSupplier>(_classDB.GetAllSuppliersFromDB());
			return templist;
		}
		public void UpdateOrInsertSupplierInDB()
		{
			if (selectedSupplier.Id == 0)
			{
				_classDB.SaveSupplierInDB(fallbackSupplier);
				selectedSupplier = new ClassSupplier();
				listSuppliers.Clear();
                listSuppliers=new List<ClassSupplier>(_classDB.GetAllSuppliersFromDB());
            }
			else
			{
				_classDB.UpdateSupplierInDB(fallbackSupplier);
                selectedSupplier = new ClassSupplier();
                listSuppliers.Clear();
                listSuppliers=new List<ClassSupplier>(_classDB.GetAllSuppliersFromDB());
                SelectSupplierInListView(fallbackSupplier.Id);
            }
		}
		public void RegretUpdateOrNewSupplierInDB()
		{
			selectedSupplier = new ClassSupplier();
            listSuppliers.Clear();
            listSuppliers=new List<ClassSupplier>(_classDB.GetAllSuppliersFromDB());
			SelectSupplierInListView(fallbackSupplier.Id);
        }
        private void SelectSupplierInListView(int inId)
        {
            foreach (ClassSupplier cc in listSuppliers)
            {
                if (cc.Id == inId)
                {
                    selectedSupplier = cc;
                    break;
                }
            }
        }
		
        #endregion
        #region Country Data Properties


        public List<ClassCountry> ListCountries
		{
			get { return _ListCountries; }
			set
			{
				if (_ListCountries != value)
				{
					_ListCountries = value;
				}
				Notify("ListCountries");
			}
		}

		#endregion
		public ClassCurrency currency
		{
			get { return _currency; }
			set
			{
				if (_currency != value)
				{
					_currency = value;
				}
				Notify("currency");
			}
		}



		public async Task GetAllCurrencysWebApi()
        {
			currency = await CCWA.GetURLContentsAsync();
        }

    }
}
