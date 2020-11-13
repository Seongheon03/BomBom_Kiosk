﻿using BomBom_Kiosk.Service;
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

namespace BomBom_Kiosk.Control
{
    /// <summary>
    /// Interaction logic for HomeControl.xaml
    /// </summary>
    public partial class HomeControl : UserControl
    {
        public HomeControl()
        {
            InitializeComponent();
            Loaded += HomeControl_Loaded;
            mediaelement.Play();
        }

        private void HomeControl_Loaded(object sender, RoutedEventArgs e)
        {
            IsVisibleChanged += HomeControl_IsVisibleChanged;

            //App.orderViewModel.LoadingAction += App_LoadingAction;
            //App.orderViewModel.InitData();
        }

        private void HomeControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                mediaelement.Play();
            }
            else
            {
                mediaelement.Stop();
            }
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            App.uiManager.PushUC(UICategory.ORDER);
        }
    }
}
