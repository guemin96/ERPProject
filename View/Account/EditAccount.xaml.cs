using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using MahApps.Metro.Controls.Dialogs;

namespace ERPProject.View.Account
{
    /// <summary>
    /// MyAccount.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class EditAccount : Page
    {
        public EditAccount()
        {
            InitializeComponent();
        }

   

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LblUserIdentityNumber.Visibility = LblUserSurname.Visibility = 
                    LblUserName.Visibility = LblUserPassword.Visibility =
                    LblUserEmail.Visibility = LblUserAdmin.Visibility =
                    LblUserActivated.Visibility = Visibility.Hidden;

                //콤보박스 초기화
                List<string> comboValues = new List<string>
                {
                    "False", // 0, index 0
                    "True"   // 1, index 1
                };
                //comboValues라는 이름의 리스트에 true와 false를 넣어주고 밑에 Admin과 Activated에 이 리스트를 넣어주면서 초기화를 시켜준다.
                CboUserAdmin.ItemsSource = comboValues;
                CboUserActivated.ItemsSource = comboValues;


                var user = Commons.LOGINED_USER;
                TxtUserID.Text = user.UserID.ToString();
                TxtUserIdentityNumber.Text = user.UserIdentityNumber.ToString();
                TxtUserSurname.Text = user.UserSurname.ToString();
                TxtUserName.Text = user.UserName.ToString();
                TxtUserEmail.Text = user.UserEmail.ToString();
                //TxtUserPassword.Password = user.UserPassword; //필요없음??? 왜 필요없지??
                CboUserAdmin.SelectedIndex = user.UserAdmin == false ? 0 : 1;
                CboUserActivated.SelectedIndex = user.UserActivated == false ? 0 : 1;
               //TxtUserAdmin.Text = user.UserAdmin.ToString();
               //TxtUserActivated.Text = user.UserActivated.ToString();
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

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = true; // 입력된 값이 모두 만족하는지 판별하는 플래그

            LblUserIdentityNumber.Visibility = LblUserSurname.Visibility =
                    LblUserName.Visibility = LblUserPassword.Visibility =
                    LblUserEmail.Visibility = LblUserAdmin.Visibility =
                    LblUserActivated.Visibility = Visibility.Hidden;

            var user = Commons.LOGINED_USER;

            if (string.IsNullOrEmpty(TxtUserSurname.Text))
            {
                LblUserSurname.Visibility = Visibility.Visible;
                LblUserSurname.Text = "이름(성)을 입력하세요";
                isValid = false;
            }
            if (string.IsNullOrEmpty(TxtUserName.Text))
            {
                LblUserName.Visibility = Visibility.Visible;
                LblUserName.Text = "이름을 입력하세요";
                isValid = false;
            }
            if (string.IsNullOrEmpty(TxtUserEmail.Text))
            {
                LblUserEmail.Visibility = Visibility.Visible;
                LblUserEmail.Text = "메일을 입력하세요";
                isValid = false;
            }
            if (string.IsNullOrEmpty(TxtUserPassword.Password))
            {
                LblUserPassword.Visibility = Visibility.Visible;
                LblUserPassword.Text = "비밀번호를 입력하세요";
                isValid = false;
            }
            if (isValid)
            {
                // MessageBox.Show("DB 수정처리 완료!");
                user.UserSurname = TxtUserSurname.Text;
                user.UserName = TxtUserName.Text;
                user.UserEmail = TxtUserEmail.Text;
                user.UserPassword = TxtUserPassword.Password;
                user.UserAdmin = bool.Parse(CboUserAdmin.SelectedValue.ToString());
                user.UserActivated = bool.Parse(CboUserActivated.SelectedValue.ToString());

                try
                {
                    var mdHash = MD5.Create();
                    user.UserPassword = Commons.GetMd5Hash(mdHash, user.UserPassword); // 패스워드가 GetMd5Hast를 통해 암호화가 됨
                    //DB상에는 해쉬함수로 암호화된 복잡한 문자들이 입력되어있음
                      
                    var result = Logic.DataAccess.SetUser(user);
                    if (result == 0)
                    {
                        //수정안됨
                        LblResult.Text = "계정 수정에 문제가 발생했습니다. 관리자에게 문의 바랍니다.";
                        LblResult.Foreground = Brushes.OrangeRed;
                    }
                    else
                    {
                        //정상적 수정됨
                        LblResult.Text = "정상적으로 수정했습니다.";
                        LblResult.Foreground = Brushes.DeepSkyBlue;
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
