﻿using System;
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
namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ClassBIZ biz;
        UserControlCustomer UCC;
        UserControlDailyPrice UCDP;
        UserControlDiesel UCD;
        UserControlSupplier UCS;
        public MainWindow()
        {
            InitializeComponent();
            biz = new ClassBIZ();
            UCC = new UserControlCustomer(biz);
            UCDP = new UserControlDailyPrice(biz);
            UCD = new UserControlDiesel(biz);
            UCS = new UserControlSupplier(biz);
            MainGrid.DataContext = biz;
            KundeGrid.Children.Add( UCC);
            DieselGrid.Children.Add(UCD);
            DagPrisGrid.Children.Add(UCDP);
            LeverandorGrid.Children.Add(UCS);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await biz.GetAllCurrencysWebApi();
        }
    }
}
