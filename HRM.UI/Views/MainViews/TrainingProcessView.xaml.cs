﻿using HRM.UI.ViewModels;
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

namespace HRM.UI.Views
{
    /// <summary>
    /// Interaction logic for TrainingProcessView.xaml
    /// </summary>
    public partial class TrainingProcessView : UserControl
    {
        public TrainingProcessView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            (this.DataContext as TrainingProcessViewModel).LoadData();
        }
    }

    
}
