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
            string sqlQuery = "SELECT Customers.Id, Customers.name, Customers.address, Customers.city, Customers.postalCode, CountryCurrency.Id " +
                "AS countryId, CountryCurrency.country, CountryCurrency.countryCode, CountryCurrency.currency, CountryCurrency.currencyCode, Customers.phone, Customers.mailAdr " +
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
                    cc.country.Id = Convert.ToInt32(row["countryId"]);
                    cc.country.country = row["country"].ToString();
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
            int res = 0;
            string sqlQuery = "INSERT INTO Suppliers (companyName, contactName, address, city, postalCode, country, phone, mailAdr) " +
                    "VALUES (@companyName, @contactName, @address, @city, @postalCode, @country, @phone, @mailAdr) "+ 
                    "SELECT SCOPE_IDENTITY()";
            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, _con))
                {
                    cmd.Parameters.Add("@companyName", SqlDbType.NVarChar).Value = inClassSupplier.firmName;
                    cmd.Parameters.Add("@contactName", SqlDbType.NVarChar).Value = inClassSupplier.contactName;
                    cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = inClassSupplier.address;
                    cmd.Parameters.Add("@city", SqlDbType.NVarChar).Value=inClassSupplier.city;
                    cmd.Parameters.Add("@postalCode", SqlDbType.NVarChar).Value = inClassSupplier.postalCode;
                    cmd.Parameters.Add("@country", SqlDbType.Int).Value = inClassSupplier.country.Id;
                    cmd.Parameters.Add("@phone", SqlDbType.NVarChar).Value = inClassSupplier.phone;
                    cmd.Parameters.Add("@mailAdr", SqlDbType.NVarChar).Value = inClassSupplier.mailAdr;
                    OpenDB();
                    res = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            { CloseDB(); }

        }
        public void UpdateSupplierInDB(ClassSupplier inClassSupplier)
        {
            int res = 0;
            string sqlQuery = "UPDATE Suppliers "+
                "SET companyName = @companyName, " +
                "contactName = @contactName, " +
                "address = @address, "+
                "city = @city, " +
                "postalCode = @postalCode, "+
                "country = @country, "+
                "phone = @phone, "+
                "mailAdr = @mailAdr "+
                "WHERE Id = @Id";
            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, _con))
                {
                    cmd.Parameters.Add("@companyName", SqlDbType.NVarChar).Value = inClassSupplier.firmName;
                    cmd.Parameters.Add("@contactName", SqlDbType.NVarChar).Value = inClassSupplier.contactName;
                    cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = inClassSupplier.address;
                    cmd.Parameters.Add("@city", SqlDbType.NVarChar).Value=inClassSupplier.city;
                    cmd.Parameters.Add("@postalCode", SqlDbType.NVarChar).Value = inClassSupplier.postalCode;
                    cmd.Parameters.Add("@country", SqlDbType.Int).Value = inClassSupplier.country;
                    cmd.Parameters.Add("@phone", SqlDbType.NVarChar).Value = inClassSupplier.phone;
                    cmd.Parameters.Add("@mailAdr", SqlDbType.NVarChar).Value = inClassSupplier.mailAdr;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = inClassSupplier.Id;
                    OpenDB();
                    if (cmd.ExecuteNonQuery()==1)
                    {
                        res = inClassSupplier.Id;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                CloseDB();
            }

        }
        #endregion
        #region Order Data Management
        public void SaveOrderToDB(ClassOrder inOrder)
        {
            int res = 0;
            string sqlQuery = "INSERT INTO Orders (customerId, supplierId, dieselVolume, orderDate, dieselPrice, customerRate, supplierRate, ownProfit) " +
                    "VALUES (@customerId, @supplierId, @dieselVolume, @orderDate, @dieselPrice, @customerRate, @supplierRate, @ownProfit) "+
                    "SELECT SCOPE_IDENTITY()";
            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, _con))
                {
                    cmd.Parameters.Add("@customerId", SqlDbType.Int).Value = inOrder.customer.Id;
                    cmd.Parameters.Add("@supplierId", SqlDbType.Int).Value = inOrder.supplier.Id;
                    cmd.Parameters.Add("@dieselVolume", SqlDbType.Int).Value = inOrder.volume;
                    cmd.Parameters.Add("@orderDate", SqlDbType.Date).Value = inOrder.date;
                    cmd.Parameters.Add("@dieselPrice", SqlDbType.Decimal).Value = inOrder.price;
                    cmd.Parameters.Add("@customerRate", SqlDbType.Decimal).Value = inOrder.customerRate;
                    cmd.Parameters.Add("@supplierRate", SqlDbType.Decimal).Value=inOrder.supplierRate;
                    cmd.Parameters.Add("@ownProfit",SqlDbType.Decimal).Value= inOrder.ownProfit;
                    OpenDB();
                    res = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally { CloseDB(); }
        }
        #endregion
        #region Oil Data Management
        public ClassDieselPrice GetOilPriceFromDB()
        {
            ClassDieselPrice tempClass = new ClassDieselPrice();
            string sqlQuery = "SELECT TOP 1* FROM DieselPrice ORDER BY date DESC";
            try
            {
                using (DataTable dt = DBReturnDataTable(sqlQuery))
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        tempClass.Id = Convert.ToInt32(row["Id"]);
                        tempClass.date =Convert.ToDateTime(row["date"]);
                        tempClass.price = Convert.ToDouble(row["price"]);
                    }
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            {
                CloseDB();
            }
            return tempClass;
        }
        public List<ClassDieselPrice> GetAllOilPriceFromDB()
        {
            List<ClassDieselPrice> tempList = new List<ClassDieselPrice>();
            string sqlQuery = "SELECT * FROM DieselPrice ORDER BY date DESC";
            try
            {
                using(DataTable dt = DBReturnDataTable(sqlQuery))
                {
                    foreach(DataRow row in dt.Rows)
                    {
                        ClassDieselPrice tempclass = new ClassDieselPrice();
                        tempclass.Id = Convert.ToInt32(row["Id"]);
                        tempclass.date = Convert.ToDateTime(row["date"]);
                        tempclass.price = Convert.ToDouble(row["price"]);
                        tempList.Add(tempclass);
                    }
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally { CloseDB(); }
            return tempList;
        }
        #endregion

    }
}
