using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using System.Data;
using System.Data.SqlClient;

namespace IO
{
    public class ClassLuxYachtDieselDB : ClassDbCon
    {
        private static string sqlConStr = @"Server=(localdb)\MSSQLLocalDB;Database=LuxYachtDieselDB;Trusted_Connection=True;";
        public ClassLuxYachtDieselDB() : base(sqlConStr)
        {
            
        }

        #region Customer Data Management
        public List<ClassCustomer> GetAllCustomersFromDB()
        {
            List<ClassCustomer> templist = new List<ClassCustomer>();
            string sqlQuery = "SELECT Customers.Id, Customers.name, Customers.address, Customers.city, Customers.postalCode, Customers.country, CountryCurrency.country " +
                "AS countryName, CountryCurrency.countryCode, CountryCurrency.currency, CountryCurrency.currencyCode, Customers.phone, Customers.mailAdr " +
                "FROM Customers LEFT OUTER JOIN CountryCurrency ON Customers.country = CountryCurrency.Id";
            using(DataTable dataTable = DBReturnDataTable(sqlQuery))
            {
                foreach(DataRow row in dataTable.Rows)
                {
                    ClassCustomer cc = new ClassCustomer();
                    cc.Id = Convert.ToInt32(row["Id"]);
                    cc.name = row["name"].ToString();
                    cc.address = row["address"].ToString();
                    cc.city = row["city"].ToString();
                    cc.postalCode = row["postalCode"].ToString();
                    cc.country.Id = Convert.ToInt32(row["country"]);
                    cc.country.country = row["countryName"].ToString();
                    cc.country.countryCode = row["countryCode"].ToString();
                    cc.country.currency = row["currency"].ToString();
                    cc.country.currencyCode = row["currencyCode"].ToString();
                    cc.phone=row["phone"].ToString();
                    cc.mailAdr= row["mailAdr"].ToString();
                    templist.Add(cc);
                }
            }

            return templist;
        }
        public void SaveCustomersInDB(ClassCustomer inClassCustomer)
        {

        }
        public void UpdateCustomerInDB(ClassCustomer inClassCustomer)
        {

        }
        #endregion
        #region Supplier Data Management
        public List<ClassSupplier> GetAllSuppliersFromDB()
        {
            List<ClassSupplier> templist = new List<ClassSupplier>();
            string sqlQuery = "SELECT Suppliers.Id, Suppliers.companyName, Suppliers.contactName, Suppliers.address, Suppliers.city, Suppliers.postalCode, CountryCurrency.Id " + 
                "AS countryId, CountryCurrency.country, CountryCurrency.countryCode, CountryCurrency.currency, CountryCurrency.currencyCode, Suppliers.phone, Suppliers.mailAdr "+
                "FROM Suppliers LEFT OUTER JOIN CountryCurrency ON Suppliers.country = CountryCurrency.Id";
            using (DataTable dt = DBReturnDataTable(sqlQuery))
            {
                foreach (DataRow row in dt.Rows)
                {
                    ClassSupplier cs = new ClassSupplier();
                    cs.Id = Convert.ToInt32(row["Id"]);
                    cs.firmName = row["companyName"].ToString();
                    cs.contactName = row["contactName"].ToString();
                    cs.address = row["address"].ToString();
                    cs.city = row["city"].ToString();
                    cs.postalCode = row["postalCode"].ToString();
                    cs.country.Id = Convert.ToInt32(row["countryId"]);
                    cs.country.country = row["country"].ToString();
                    cs.country.countryCode = row["countryCode"].ToString();
                    cs.country.currency= row["currency"].ToString();
                    cs.country.currencyCode=row["currencyCode"].ToString();
                    cs.phone=row["phone"].ToString();
                    cs.mailAdr=row["mailAdr"].ToString();
                    templist.Add(cs);
                }
            }
            return templist;
        }
        public void SaveSupplierInDB(ClassSupplier inClassSupplier)
        {

        }
        public void UpdateSupplierInDB(ClassSupplier inClassSupplier)
        {

        }
        #endregion
        #region Order Data Management
        public void SaveOrderToDB(ClassOrder inOrder)
        {

        }
        #endregion
        #region Oil Data Management
        public ClassDieselPrice GetOilPriceFromDB()
        {
            ClassDieselPrice tempClass = new ClassDieselPrice();

            return tempClass;
        }
        public List<ClassDieselPrice> GetAllOilPriceFromDB()
        {
            List<ClassDieselPrice> tempList = new List<ClassDieselPrice>();
            return tempList;
        }
        #endregion

    }
}
