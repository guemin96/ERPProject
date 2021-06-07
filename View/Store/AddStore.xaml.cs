using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using MahApps.Metro.Controls.Dialogs;

namespace ERPProject.View.Store
{
    /// <summary>
    /// MyAccount.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AddStore : Page
    {
        public AddStore()
        {
            InitializeComponent();
        }



        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LblStoreLocation.Visibility = LblStoreName.Visibility = Visibility.Hidden;

                //콤보박스 초기화

                TxtStoreID.Text = TxtStoreName.Text = TxtStoreLocation.Text = " ";
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 MyAccount Loaded : {ex}");
                throw ex;
            }
        }



        //3/29일 영상 끝

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        public bool IsValidInput()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(TxtStoreName.Text))
            {
                LblStoreName.Visibility = Visibility.Visible;
                LblStoreName.Text = "창고번호를 입력하세요";
                isValid = false;
            }
            else
            {
                var cnt = Logic.DataAccess.Getusers().Where(u => u.UserIdentityNumber.Equals(TxtStoreName.Text)).Count();
                if (cnt > 0)
                {
                    LblStoreName.Visibility = Visibility.Visible;
                    LblStoreName.Text = "중복된 창고 번호가 존재합니다.";
                    isValid = false;
                }
            }

            if (string.IsNullOrEmpty(TxtStoreLocation.Text))
            {
                LblStoreLocation.Visibility = Visibility.Visible;
                LblStoreLocation.Text = "창고 위치를 입력하세요";
                isValid = false;
            }
            
            return isValid;


        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

            bool isValid = true;

            LblStoreName.Visibility = LblStoreLocation.Visibility = Visibility.Hidden;

            var store = new Model.Store();

            isValid = IsValidInput();//유효성 체크

            if (isValid)
            {
                store.StoreName = TxtStoreName.Text;
                store.StoreLocation = TxtStoreLocation.Text;

                try
                {
                    var result = Logic.DataAccess.SetStores(store);
                    if (result == 0)
                    {
                        Commons.LOGGER.Error("AddStore.xaml.cs 창고 정보 저장오류 발생");
                        Commons.ShowMessageAsync("오류","저장시 오류가 발생했습니다.");
                    }
                    else
                    {
                        NavigationService.Navigate(new StoreList());
                    }
                }
                catch (Exception ex)
                {
                    Commons.LOGGER.Error($"예외발생 : {ex}");
                }
            }
        }

        private void TxtStoreName_LostFocus(object sender, RoutedEventArgs e)
        {
            IsValidInput();
        }

        private void TxtStoreLocation_LostFocus(object sender, RoutedEventArgs e)
        {
            IsValidInput();
        }
    }
}
