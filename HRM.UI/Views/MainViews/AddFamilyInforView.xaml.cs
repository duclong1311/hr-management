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
using HRM.UI.ViewModels;

namespace HRM.UI.Views
{
    /// <summary>
    /// Interaction logic for AddFamilyInforView.xaml
    /// </summary>
    public partial class AddFamilyInforView : UserControl
    {
        public AddFamilyInforView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            (this.DataContext as AddFamilyInforViewModel).LoadData();
        }
    }
}
