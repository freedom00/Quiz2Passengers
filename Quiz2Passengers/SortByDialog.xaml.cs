using System.Windows;

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