using ERPProject.View;
using ERPProject.View.Account;
using ERPProject.View.BookOut;
using ERPProject.View.Product;
using ERPProject.View.Stock;
using ERPProject.View.Store;
using ERPProject.View.User;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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

namespace ERPProject
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_ContentRendered(object sender, EventArgs e)
        {
            ShowLoginView();
        }

        private void MetroWindow_Activated(object sender, EventArgs e)
        {
            if (Commons.LOGINED_USER != null)
                BtnLoginedId.Content = $"{ Commons.LOGINED_USER.UserEmail}({Commons.LOGINED_USER.UserName})";
            //상태창(제일 윗줄)에 로그인한 아이디를 표시해주는 역할
        }

        private async void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            //TODO : 모든 화면을 해제하고 첫화면으로 돌려놔야 함.
            var result = await this.ShowMessageAsync("로그아웃", "로그아웃하시겠습니까?", 
                MessageDialogStyle.AffirmativeAndNegative, null);

            if (result == MessageDialogResult.Affirmative)
            {
                Commons.LOGINED_USER = null; //로그인했던 사용자 정보를 삭제
                ShowLoginView();
            }
        }

        private void ShowLoginView()
        {
            LoginView view = new LoginView();
            view.Owner = this;
            view.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            view.ShowDialog();
        }

        private async void BtnAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ActiveControl.Content = new MyAccount();
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 BtnAccount_Click : {ex}");
                await this.ShowMessageAsync("예외", $"예외발생 : {ex}");
            }
        }

        private async void BtnUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ActiveControl.Content = new UserList();
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 BtnUser_Click : {ex}");
                await this.ShowMessageAsync("예외", $"예외발생 : {ex}");
            }
        }

        private async void BtnStore_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ActiveControl.Content = new StoreList();
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 BtnUser_Click : {ex}");
                await this.ShowMessageAsync("예외", $"예외발생 : {ex}");
            }
        }

        private async void BtnProducts_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ActiveControl.Content = new Itemlist();
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 BtnUser_Click : {ex}");
                await this.ShowMessageAsync("예외", $"예외발생 : {ex}");
            }
        }

        private async void BtnBooks_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ActiveControl.Content = new StockList();
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 BtnUser_Click : {ex}");
                await this.ShowMessageAsync("예외", $"예외발생 : {ex}");
            }
        }

        private async void BtnBookout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ActiveControl.Content = new BookOutList();
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 BtnUser_Click : {ex}");
                await this.ShowMessageAsync("예외", $"예외발생 : {ex}");
            }
        }
    }
}
