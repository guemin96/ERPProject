using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using MahApps.Metro.Controls.Dialogs;

namespace ERPProject.View.BookOut
{
    /// <summary>
    /// MyAccount.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class EditBookOut : Page
    {
        public EditBookOut()
        {
            InitializeComponent();
        }



        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LblStoreID.Visibility = LblItemQuantity.Visibility = LblItemID.Visibility = Visibility.Hidden;

                List<Model.BookOutItem> bookOutItems = new List<Model.BookOutItem>();
                bookOutItems = Logic.DataAccess.GetbookOutItems();

                this.DataContext = bookOutItems;
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 BookOutItemsList Loaded : {ex}");
                throw ex;
            }
        }



        //3/29일 영상 끝

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        //public bool IsValidInput()
        //{
            bool isValid = true;

            //if (string.IsNullOrEmpty(TxtItemName.Text))
            //{
            //    LblItemName.Visibility = Visibility.Visible;
            //    LblItemName.Text = "창고번호를 입력하세요";
            //    isValid = false;
            //}
            //else
            //{
            //    var cnt = Logic.DataAccess.Getitems().Where(i => i.ItemName.Equals(TxtItemName.Text)).Count();
            //    if (cnt > 0)
            //    {
            //        LblItemName.Visibility = Visibility.Visible;
            //        LblItemName.Text = "중복된 제품 이름이 존재합니다.";
            //        isValid = false;
            //    }
            //}

            //if (string.IsNullOrEmpty(TxtItemDescription.Text))
            //{
            //    LblItemDescription.Visibility = Visibility.Visible;
            //    LblItemDescription.Text = "제품묘사를 입력하세요";
            //    isValid = false;
            //}

            //if (CboItemStatus.SelectedIndex < 0)
            //{
            //    LblItemStatus.Visibility = Visibility.Visible;
            //    LblItemStatus.Text = "제품상태를 선택하세요";
            //    isValid = false;
            //}

           

            //if (string.IsNullOrEmpty(TxtTagID.Text))
            //{
            //    LblTagID.Visibility = Visibility.Visible;
            //    LblTagID.Text = "제품RFID를 입력하세요";
            //    isValid = false;
            //}
            //else
            //{
            //    var cnt = Logic.DataAccess.Getitems().Where(i => i.TagID.Equals(TxtTagID.Text)).Count();
            //    if (cnt > 0)
            //    {
            //        LblTagID.Visibility = Visibility.Visible;
            //        LblTagID.Text = "중복된 제품RFID가 존재합니다.";
            //        isValid = false;
            //    }
            //}

            //if (string.IsNullOrEmpty(TxtBarcodeID.Text))
            //{
            //    LblBarcodeID.Visibility = Visibility.Visible;
            //    LblBarcodeID.Text = "제품RFID를 입력하세요";
            //    isValid = false;
            //}
            //else
            //{
            //    var cnt = Logic.DataAccess.Getitems().Where(i => i.BarcodeID.Equals(TxtBarcodeID.Text)).Count();
            //    if (cnt > 0)
            //    {
            //        LblBarcodeID.Visibility = Visibility.Visible;
            //        LblBarcodeID.Text = "중복된 제품바코드가 존재합니다.";
            //        isValid = false;
            //    }
            //}
            //if (string.IsNullOrEmpty(TxtBrandID.Text))
            //{
            //    LblBrandID.Visibility = Visibility.Visible;
            //    LblBrandID.Text = "제품브랜드를 입력하세요";
            //    isValid = false;
            //}
            //if (string.IsNullOrEmpty(TxtCategoryID.Text))
            //{
            //    LblCategoryID.Visibility = Visibility.Visible;
            //    LblCategoryID.Text = "제품카테고리를 입력하세요";
            //    isValid = false;
            //}


            //return isValid;


        //}

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

            bool isValid = true;

            //LblItemName.Visibility = LblItemDescription.Visibility =
            //       LblItemStatus.Visibility = LblTagID.Visibility = LblBarcodeID.Visibility =
            //       LblBrandID.Visibility = LblCategoryID.Visibility = Visibility.Hidden;

            var item = new Model.Item();

            //isValid = IsValidInput();//유효성 체크

            //if (isValid)
            //{
            //    item.ItemName = TxtItemName.Text;
            //    item.ItemDescription = TxtItemDescription.Text;
            //    item.ItemStatus = bool.Parse(CboItemStatus.SelectedValue.ToString());
            //    item.TagID = Int32.Parse(TxtTagID.Text);
            //    item.BarcodeID = Int32.Parse(TxtBarcodeID.Text);
            //    item.BrandID = Int32.Parse(TxtBrandID.Text);
            //    item.CategoryID = Int32.Parse(TxtCategoryID.Text);

            //    try
            //    {
            //        var result = Logic.DataAccess.SetItems(item);
            //        if (result == 0)
            //        {
            //            Commons.LOGGER.Error("AddStore.xaml.cs 창고 정보 저장오류 발생");
            //            Commons.ShowMessageAsync("오류","저장시 오류가 발생했습니다.");
            //        }
            //        else
            //        {
            //            NavigationService.Navigate(new Itemlist());
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Commons.LOGGER.Error($"예외발생 : {ex}");
            //    }
            //}
        }

        
    }
}
