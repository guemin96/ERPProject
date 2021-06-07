using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace ERPProject.View.BookOut
{
    /// <summary>
    /// MyAccount.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class BookOutList : Page
    {
        public BookOutList()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
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

        private void BtnAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //NavigationService.Navigate(new AddStock());
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 BtnAddStore_Click : {ex}");
                throw ex;
            }
        }

        private void BtnEditItem_Click(object sender, RoutedEventArgs e)
        {
            if (GrdData.SelectedItem == null)
            {
                Commons.ShowMessageAsync("출고제품수정", "출고수정할 제품를 선택하세요");
                return;
            }

            try
            {
                var itemId = (GrdData.SelectedItem as Model.Item).ItemID;
               // NavigationService.Navigate(new EditItem(itemId));
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 BtnEditStore_Click : {ex}");
                throw ex;
            }
        }
    }
}
