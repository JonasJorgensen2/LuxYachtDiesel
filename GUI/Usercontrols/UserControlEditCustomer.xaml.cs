using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BIZ;
using Repository;
namespace GUI.Usercontrols
{
    /// <summary>
    /// Interaction logic for UserControlEditCustomer.xaml
    /// </summary>
    public partial class UserControlEditCustomer : UserControl
    {
        ClassBIZ BIZ;
        Grid MotherGrid;
        public UserControlEditCustomer(ClassBIZ inBIZ, Grid inGrid)
        {
            InitializeComponent();
            BIZ = inBIZ;
            MainGrid.DataContext = BIZ;
            MotherGrid = inGrid;
            
            
        }

        private void ButtonGem_Click(object sender, RoutedEventArgs e)
        {
            if (BIZ.fallbackCustomer.name.Length !=0 && BIZ.fallbackCustomer.address.Length != 0 && BIZ.fallbackCustomer.city.Length != 0 && BIZ.fallbackCustomer.postalCode.Length != 0 && BIZ.fallbackCustomer.country.Id != 0 && BIZ.fallbackCustomer.phone.Length != 0 && BIZ.fallbackCustomer.mailAdr.Length != 0)
            {
                BIZ.UpdateOrInsertCustomerInDB();
                BIZ.editingLock = true;
                MotherGrid.Children.Remove(this);
            }
        }

        private void ButtonFortryd_Click(object sender, RoutedEventArgs e)
        {
            BIZ.editingLock = true;
            MotherGrid.Children.Remove(this);
        }
    }
}

