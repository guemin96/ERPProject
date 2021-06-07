using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using MahApps.Metro.Controls.Dialogs;

namespace ERPProject.View.User
{
    /// <summary>
    /// MyAccount.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DeactiveUser : Page
    {
        public DeactiveUser()
        {
            InitializeComponent();
        }

   

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //그리드 바인딩
                List<Model.User> user = Logic.DataAccess.Getusers(); //dataAccess로부터 데이터를 들고와 user에 넣고 user에 잇는 데이터를 DataContext에 넣음
                this.DataContext = user;// 수정창에 데이터를 표시해주는 역할
              
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

        private async void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = true; // 입력된 값이 모두 만족하는지 판별하는 플래그

            if (GrdData.SelectedItem==null)
            {//페이지라서 this를 사용할 수 없다. 
                await Commons.ShowMessageAsync("오류", "비활성화할 사용자를 선택하시오");
                return;
            }
           //var user = Commons.LOGINED_USER;

            
            if (isValid)
            {
                // MessageBox.Show("DB 수정처리 완료!");
                try
                {
                    var user = GrdData.SelectedItem as Model.User;
                    user.UserActivated = false; // 사용자 비활성화
                    
                    var result = Logic.DataAccess.SetUser(user);
                    if (result == 0)
                    {
                        await Commons.ShowMessageAsync("오류", "사용자 수정에 실패했습니다.");
                        //수정안됨
                    }
                    else
                    {
                        //정상적 수정됨
                        NavigationService.Navigate(new UserList());
                    }
                }
                catch (Exception ex)
                {
                    Commons.LOGGER.Error($"예외발생 : {ex}");
                }
            }
        }

        private void GrdData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                var user = GrdData.SelectedItem as Model.User;

            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 GrdData_SelectedCellsChanged : {ex}");
            }

        }

        private async void BtnActive_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = true; // 입력된 값이 모두 만족하는지 판별하는 플래그

            if (GrdData.SelectedItem == null)
            {//페이지라서 this를 사용할 수 없다. 
                await Commons.ShowMessageAsync("오류", "활성화할 사용자를 선택하시오");
                return;
            }
            //var user = Commons.LOGINED_USER;


            if (isValid)
            {
                // MessageBox.Show("DB 수정처리 완료!");
                try
                {
                    var user = GrdData.SelectedItem as Model.User;
                    user.UserActivated = true; // 사용자 비활성화

                    var result = Logic.DataAccess.SetUser(user);
                    if (result == 0)
                    {
                        await Commons.ShowMessageAsync("오류", "사용자 수정에 실패했습니다.");
                        //수정안됨
                    }
                    else
                    {
                        //정상적 수정됨
                        NavigationService.Navigate(new UserList());
                    }
                }
                catch (Exception ex)
                {
                    Commons.LOGGER.Error($"예외발생 : {ex}");
                }
            }
        }
    }
}
