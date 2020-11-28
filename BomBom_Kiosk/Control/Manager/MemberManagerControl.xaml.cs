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

namespace BomBom_Kiosk.Control.Manager
{
    /// <summary>
    /// Interaction logic for MemberManagerControl.xaml
    /// </summary>
    public partial class MemberManagerControl : UserControl
    {
        public MemberManagerControl()
        {
            InitializeComponent();

            Loaded += MemberManagerControl_Loaded;
        }

        private void MemberManagerControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = App.managerViewModel;
        }
    }
}
