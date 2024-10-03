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
using GUI.Usercontrols;
namespace GUI
{
    /// <summary>
    /// Interaction logic for UserControlSupplier.xaml
    /// </summary>
    public partial class UserControlSupplier : UserControl
    {
        ClassBIZ BIZ;
        UserControlEditSupplier ucEditSupplier;
       // Grid MotherGrid;
        public UserControlSupplier(ClassBIZ inBIZ)
        {
            InitializeComponent();
            BIZ = inBIZ;
            MainGrid.DataContext = BIZ;
            ucEditSupplier = new UserControlEditSupplier(BIZ, MainGrid);
            Grid.SetRow(ucEditSupplier, 0);
            Grid.SetRowSpan(ucEditSupplier, 2);
            Grid.SetColumn(ucEditSupplier, 1);
            Grid.SetColumnSpan(ucEditSupplier, 2);
        }

        private void ButtonOpret_Click(object sender, RoutedEventArgs e)
        {
            BIZ.fallbackSupplier= new ClassSupplier();
            MainGrid.Children.Add(ucEditSupplier);
            ChangeEditingLock();


        }

        private void ButtonRediger_Click(object sender, RoutedEventArgs e)
        {
            BIZ.fallbackSupplier= new ClassSupplier(BIZ.selectedSupplier);
            MainGrid.Children.Add(ucEditSupplier);
            ChangeEditingLock();

        }

        

       
        private void ChangeEditingLock()
        {
            if(BIZ.editingLock == true)
            {
                BIZ.editingLock = false;
                
            }
            else
            {
                BIZ.editingLock = true;
                
            }
        }
    }
}
