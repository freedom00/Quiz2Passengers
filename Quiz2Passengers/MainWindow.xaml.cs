using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Quiz2Passengers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public enum SortOrderEnum { Name, PassNo, Dest, DeptDateTime }

        public static SortOrderEnum CurrSortOrder = SortOrderEnum.Name;

        private delegate IEnumerable<Passenger> ListHandler(IEnumerable<Passenger> passengerList);

        public MainWindow()
        {
            InitializeComponent();
            RefreshList(new ListHandler(GetList));
        }

        private void RefreshList(ListHandler listHandler, IEnumerable<Passenger> passengerList = null)
        {
            lvPassengers.ItemsSource = listHandler(passengerList);
            lvPassengers.Items.Refresh();
        }

        private IEnumerable<Passenger> SortList(IEnumerable<Passenger> passengerList)
        {
            IEnumerable<Passenger> result = null;
            switch (CurrSortOrder)
            {
                case SortOrderEnum.Name:
                    result = from p in passengerList orderby p.Name select p;
                    break;

                case SortOrderEnum.PassNo:
                    result = from p in passengerList orderby p.PassportNo select p;
                    break;

                case SortOrderEnum.Dest:
                    result = from p in passengerList orderby p.Destination select p;
                    break;

                case SortOrderEnum.DeptDateTime:
                    result = from p in passengerList orderby p.DepartureDateTime select p;
                    break;
            }
            return result;
        }

        private IEnumerable<Passenger> FilterList(IEnumerable<Passenger> passengerList)
        {
            string keyword = tbSearch.Text;
            if (keyword == "")
            {
                return passengerList;
            }
            return from p in passengerList where p.Name.Contains(keyword) || p.Destination.Contains(keyword) select p;
        }

        private IEnumerable<Passenger> GetList(IEnumerable<Passenger> passengerList = null)
        {
            try
            {
                return Globals.DB.GetAll();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(this, ex.Message, "Input Data Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Passenger>();
            }
        }

        private void AddAndEdit(Passenger passenger = null)
        {
            new AddEditDialog(passenger).ShowDialog();
            RefreshList(new ListHandler(GetList));
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            AddAndEdit();
        }

        private void lvPassengers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Passenger passenger = (Passenger)lvPassengers.SelectedValue;
            if (passenger == null)
            {
                return;
            }
            AddAndEdit(passenger);
        }

        private void btSortBy_Click(object sender, RoutedEventArgs e)
        {
            new SortByDialog().ShowDialog();
            RefreshList(new ListHandler(SortList), GetList());
        }

        private void tbSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string keyword = tbSearch.Text;
            if (keyword == "")
            {
                return;
            }
            RefreshList(new ListHandler(FilterList), GetList());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Globals.DB.Close();
        }
    }
}