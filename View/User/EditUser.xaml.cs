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
    public partial class EditUser : Page
    {
        public EditUser()
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


                TxtUserID.Text = TxtUserIdentityNumber.Text = "";

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

        public bool IsValidInput()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(TxtUserIdentityNumber.Text))
            {
                LblUserIdentityNumber.Visibility = Visibility.Visible;
                LblUserIdentityNumber.Text = "사번을 입력하세요";
                isValid = false;
            }


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
                LblUserPassword.Text = "비밀번호를을 입력하세요";
                isValid = false;
            }
            if (CboUserAdmin.SelectedIndex < 0)
            {
                LblUserAdmin.Visibility = Visibility.Visible;
                LblUserAdmin.Text = "관리자여부를 선택하세요";
                isValid = false;
            }
            if (CboUserActivated.SelectedIndex < 0)
            {
                LblUserActivated.Visibility = Visibility.Visible;
                LblUserActivated.Text = "활성여부를 선택하세요";
                isValid = false;
            }
            if (!Commons.IsValidEmail(TxtUserEmail.Text)) // IsValidEmail은 사용자가 입력한 메일 주소가 메일형식에 맞는지 확인하는 함수
            {
                LblUserEmail.Visibility = Visibility.Visible;
                LblUserEmail.Text = "이메일 형식이 올바르지 않습니다.";
                isValid = false;
            }
            return isValid;


        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = true; // 입력된 값이 모두 만족하는지 판별하는 플래그

            LblUserIdentityNumber.Visibility = LblUserSurname.Visibility =
                    LblUserName.Visibility = LblUserPassword.Visibility =
                    LblUserEmail.Visibility = LblUserAdmin.Visibility =
                    LblUserActivated.Visibility = Visibility.Hidden;

            var user = GrdData.SelectedItem as Model.User;

            isValid = IsValidInput();
            
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

        private void GrdData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            //선택된 값이 나올 수 있도록
            var user = GrdData.SelectedItem as Model.User;

            TxtUserID.Text = user.UserID.ToString();
            TxtUserIdentityNumber.Text = user.UserIdentityNumber;
            TxtUserSurname.Text = user.UserSurname;
            TxtUserName.Text = user.UserName;
            TxtUserEmail.Text = user.UserEmail;
            CboUserAdmin.SelectedIndex = user.UserAdmin == false ? 0 : 1;
            CboUserActivated.SelectedIndex = user.UserActivated == false ? 0 : 1;
        }
    }
}
