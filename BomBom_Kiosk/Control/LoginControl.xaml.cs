﻿using BomBom_Kiosk.Model;
using BomBom_Kiosk.Properties;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BomBom_Kiosk.Control
{
    public partial class LoginControl : UserControl
    {
        public List<MemberModel> Members { get; set; } = new List<MemberModel>();

        public delegate void LoginEventHandler(bool success);
        public event LoginEventHandler LoginAction;

        public LoginControl()
        {
            InitializeComponent();
            Loaded += LoginControl_Loaded;
            IsVisibleChanged += LoginControl_IsVisibleChanged;
        }

        private async void LoginControl_Loaded(object sender, RoutedEventArgs e)
        {
            await InitData();
        }

        private void LoginControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                btnLogin.IsEnabled = false;
                LoginAction?.Invoke(false);
            }
            else
            {
                LoginAction?.Invoke(true);
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            foreach (var member in Members)
            {
                if (member.Id == tbId.Text && member.Pw == tbPw.Password)
                {
                    Settings.Default.isAutoLogin = (bool)cbAutoLogin.IsChecked;
                    Settings.Default.Save();

                    App.uiManager.PushUC(Service.UICategory.HOME);
                    return;
                }
            }

            tbStatus.Text = "아이디 또는 비밀번호를 확인해주세요.";
        }

        public async Task InitData()
        { 
            tbStatus.Text = "DB에 연결중입니다...";
            progressRing.IsActive = true;
            
            if (await Task.Run(() => App.dbManager.ConnectDB()))
            {
                tbStatus.Text = "데이터를 로드중입니다...";

                await Task.Run(() =>
                {
                    App.orderViewModel.InitData();
                    App.paymentViewModel.InitMembers();
                    App.managerViewModel.InitData();
                    SetMembers();
                    App.network.ConnectServer();
                });

                tbStatus.Text = "서버에 연결중입니다...";

                if (await Task.Run(() => App.network.ConnectServer()))
                {
                    tbStatus.Text = "로그인을 해주세요.";
                }
                else
                {
                    tbStatus.Text = "서버에 연결되지 않았습니다.";
                }

                CheckIsAutoLogin();

                btnLogin.IsEnabled = true;
            }
            else
            {
                tbStatus.Text = "DB에 연결되지 않았습니다.";
            }

            progressRing.IsActive = false;
        }

        private void SetMembers()
        {
            Members = App.dbManager.GetMembers();
        }

        private void CheckIsAutoLogin()
        {
            if (Settings.Default.isAutoLogin)
            {
                App.uiManager.PushUC(Service.UICategory.HOME);
            }
        }
    }

    /// <summary>
    /// Interaction logic for LoginControl.xaml
    /// </summary>
    public class PasswordBoxMonitor : DependencyObject
    {
        public static bool GetIsMonitoring(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsMonitoringProperty);
        }

        public static void SetIsMonitoring(DependencyObject obj, bool value)
        {
            obj.SetValue(IsMonitoringProperty, value);
        }

        public static readonly DependencyProperty IsMonitoringProperty =
            DependencyProperty.RegisterAttached("IsMonitoring", typeof(bool), typeof(PasswordBoxMonitor), new UIPropertyMetadata(false, OnIsMonitoringChanged));

        public static int GetPasswordLength(DependencyObject obj)
        {
            return (int)obj.GetValue(PasswordLengthProperty);
        }

        public static void SetPasswordLength(DependencyObject obj, int value)
        {
            obj.SetValue(PasswordLengthProperty, value);
        }

        public static readonly DependencyProperty PasswordLengthProperty =
            DependencyProperty.RegisterAttached("PasswordLength", typeof(int), typeof(PasswordBoxMonitor), new UIPropertyMetadata(0));

        private static void OnIsMonitoringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pb = d as PasswordBox;
            if (pb == null)
            {
                return;
            }
            if ((bool)e.NewValue)
            {
                pb.PasswordChanged += PasswordChanged;
            }
            else
            {
                pb.PasswordChanged -= PasswordChanged;
            }
        }

        static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            var pb = sender as PasswordBox;
            if (pb == null)
            {
                return;
            }
            SetPasswordLength(pb, pb.Password.Length);
        }
    }
}
