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
using System.Windows.Shapes;

namespace ERPProject.View
{
    /// <summary>
    /// LoginView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginView : MetroWindow
    {
        public LoginView()
        {
            InitializeComponent();
        }

        //로그인 창에서 취소버튼을 눌렀을때 실행되는 이벤트
        private async void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            var result = await this.ShowMessageAsync("종료","프로그램 종료할까요?",//마하앱 프로그램을 사용하여 await를 사용하기 때문에 void뒤에 async를 넣어준다
                MessageDialogStyle.AffirmativeAndNegative,null);
            if (result==MessageDialogResult.Affirmative) //위 비동기 프로그램에서 예를 눌렀을때의 조건식
            {
                Application.Current.Shutdown();//프로그램 종료 
            }

            //마하앱을 안썼을 경우
            #region
            //var result = MessageBox.Show("종료하시겠습니까?", "종료",MessageBoxButton.OKCancel);

            //if (result==MessageBoxResult.OK)
            //{
            //    Application.Current.Shutdown();
            //}
            #endregion
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            TxtUserEmail.Focus();
            LblResult.Visibility = Visibility.Hidden;
        }

        private void TxtUserEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                TxtUserPass.Focus();
        }

        private void TxtUserPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                BtnLogin_Click(sender, e);//로그인 버튼 클릭
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            LblResult.Visibility = Visibility.Hidden;

            if (string.IsNullOrEmpty(TxtUserEmail.Text) || string.IsNullOrEmpty(TxtUserPass.Password))
            {
                LblResult.Visibility = Visibility.Visible;
                LblResult.Content = "아이디나 패스워드를 입력하세요.";
                return;
            }
            //로그인 부분이기 때문에 DB와 연결을 시켜줘야함
            //여기서 DB에 새로운 테이블을 만드는 작업을 함(테이블 안에 사용자 아이디와 비밀번호 및 등급을 저장)
            //현재는 ERP라는 이름의 DB안에 USER이라는 테이블을 만들어놨음 
            try
            {
                var email = TxtUserEmail.Text;
                var password = TxtUserPass.Password;
                var isOurUser = Logic.DataAccess.Getusers()
                    .Where(u => u.UserEmail.Equals(email) && u.UserPassword.Equals(password)).Count();//
                // Logic이라는 폴더를 생성하고 그 후에 dataAccess라는 클래스를 만든 뒤에 DB데이터를 저장
                // isOurUser이라는 변수에 우리가 사용할 데이터만 뽑음
                // Where절이 바로 linq식으로 우리가 사용할 데이터를 뽑는 절이다.
                // linq절 마지막에 count는 DB상의 메일과 패스워드를 방금 친 메일, 패스워드와 비교하여 동일한것이 있을때 개수를 올리는 역할을 한다.
                // 동일한 것이 없을때 개수가 0이 됨으로 밑에 if문에서 사용자가 존재하지 않습니다를 뜨게한다.
                if (isOurUser == 0)
                {
                    LblResult.Visibility = Visibility.Visible;
                    LblResult.Content = "아이디나 패스워드가 일치하지 않습니다.";
                    return;
                }
                else
                {
                    Commons.LOGINED_USER = Logic.DataAccess.Getusers().Where(u => u.UserEmail.Equals(email)).FirstOrDefault();
                    //FirstOrDefault는 같은 계정이 여러개 잇는 것이 아니기때문에 맞는 아이디의 경우 아이디를 반환하고 아닌 경우에는 null을 반환한다.
                    this.Visibility = Visibility.Hidden; //아이디 패스워드가 맞기 때문에 로그인 창을 사라지게 해주는 작업
                    
                }
                //여기까지 210329 영상 5:10에서 마침

            }
            catch (Exception ex)
            {
                //예외처리
            }
        }
    }
}
