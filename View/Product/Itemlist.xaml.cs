using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace ERPProject.View.Product
{
    /// <summary>
    /// MyAccount.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Itemlist : Page
    {
        public Itemlist()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Model.Item> items = new List<Model.Item>();
                items = Logic.DataAccess.Getitems();

                this.DataContext = items;
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 MyAccount Loaded : {ex}");
                throw ex;
            }
        }

        

        private void BtnExportExcel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new AddItem());
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
                Commons.ShowMessageAsync("제품수정", "수정할 제품를 선택하세요");
                return;
            }

            try
            {
                var itemId = (GrdData.SelectedItem as Model.Item).ItemID;
                NavigationService.Navigate(new EditItem(itemId));
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 BtnEditStore_Click : {ex}");
                throw ex;
            }
        }
    }
}
