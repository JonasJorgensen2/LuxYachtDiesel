using BIZ;
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
using Repository;

namespace GUI.Usercontrols
{
    /// <summary>
    /// Interaction logic for UserControlEditSupplier.xaml
    /// </summary>
    public partial class UserControlEditSupplier : UserControl
    {
        ClassBIZ BIZ;
        Grid MotherGrid;
        public UserControlEditSupplier( ClassBIZ inBIZ, Grid inGrid)
        {
            InitializeComponent();
            BIZ = inBIZ;
            MainGrid.DataContext = BIZ;
            MotherGrid = inGrid;
        }
        private void ButtonGem_Click(object sender, RoutedEventArgs e)
        {
            if (BIZ.fallbackSupplier.firmName.Length != 0 && BIZ.fallbackSupplier.contactName.Length != 0 && BIZ.fallbackSupplier.address.Length != 0 && BIZ.fallbackSupplier.city.Length != 0 && BIZ.fallbackSupplier.country.Id != 0 && BIZ.fallbackSupplier.phone.Length != 0 && BIZ.fallbackSupplier.mailAdr.Length != 0)
            {
                BIZ.UpdateOrInsertSupplierInDB();
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
