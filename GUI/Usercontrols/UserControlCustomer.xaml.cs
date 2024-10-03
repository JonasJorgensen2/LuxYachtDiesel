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
using GUI.Usercontrols;
using Repository;
namespace GUI
{
    /// <summary>
    /// Interaction logic for UserControlCustomer.xaml
    /// </summary>
    public partial class UserControlCustomer : UserControl
    {
        ClassBIZ BIZ;
        UserControlEditCustomer UCEC;
        public UserControlCustomer(ClassBIZ inBiz)
        {
            InitializeComponent();
            BIZ = inBiz;
            MainGrid.DataContext= BIZ;
            UCEC = new UserControlEditCustomer(inBiz,MainGrid);
            Grid.SetRow(UCEC, 0);
            Grid.SetRowSpan(UCEC, 13);
            Grid.SetColumn(UCEC, 1);
            Grid.SetColumnSpan(UCEC, 2);
        }
        private void ChangeEditingLock()
        {
            if (BIZ.editingLock == true)
            {
                BIZ.editingLock = false;
                
            }
            else
            {
                BIZ.editingLock = true;
                
            }
        }

        private void ButtonOpret_Click(object sender, RoutedEventArgs e)
        {
            BIZ.fallbackCustomer = new ClassCustomer();
            MainGrid.Children.Add(UCEC);
            ChangeEditingLock();
            
        }

        private void ButtonRediger_Click(object sender, RoutedEventArgs e)
        {
            BIZ.fallbackCustomer = new ClassCustomer(BIZ.selectedCustomer);
            MainGrid.Children.Add(UCEC);
            ChangeEditingLock();
        }

        
    }
}
