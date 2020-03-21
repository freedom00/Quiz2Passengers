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

namespace Quiz2Passengers
{
    /// <summary>
    /// Interaction logic for SortByDialog.xaml
    /// </summary>
    public partial class SortByDialog : Window
    {
        public SortByDialog()
        {
            InitializeComponent();
        }

        private void btApply_Click(object sender, RoutedEventArgs e)
        {
            if (rbName.IsChecked == true)
            {
                MainWindow.CurrSortOrder = MainWindow.SortOrderEnum.Name;
            }
            else if (rbPassNo.IsChecked == true)
            {
                MainWindow.CurrSortOrder = MainWindow.SortOrderEnum.PassNo;
            }
            else if (rbDest.IsChecked == true)
            {
                MainWindow.CurrSortOrder = MainWindow.SortOrderEnum.Dest;
            }
            else if (rbDepDateTime.IsChecked == true)
            {
                MainWindow.CurrSortOrder = MainWindow.SortOrderEnum.DeptDateTime;
            }
            this.DialogResult = true;
        }
    }
}