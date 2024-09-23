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

namespace GUI
{
    /// <summary>
    /// Interaction logic for UserControlDiesel.xaml
    /// </summary>
    public partial class UserControlDiesel : UserControl
    {
        ClassBIZ Biz;
        public UserControlDiesel(ClassBIZ inBIZ)
        {
            InitializeComponent();
            Biz= inBIZ;
            MainGrid.DataContext = Biz;
        }
    }
}
