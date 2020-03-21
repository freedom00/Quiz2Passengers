using System;
using System.Data.SqlClient;
using System.Windows;

namespace Quiz2Passengers
{
    /// <summary>
    /// Interaction logic for AddEditDialog.xaml
    /// </summary>
    public partial class AddEditDialog : Window
    {
        private Passenger passenger;

        public enum FuelTypeEnum
        { Diesel, Hybrid, Electric, Other }

        public AddEditDialog()
        {
            InitializeComponent();
            Init();
        }

        public AddEditDialog(Passenger passenger)
        {
            this.passenger = passenger;
            InitializeComponent();
            Init();
            AddEdit();
        }

        private void Init()
        {
            for (int i = 0; i < 24; i++)
            {
                comboDepTime.Items.Add(string.Format("{0:00}:00", i));
                comboDepTime.Items.Add(string.Format("{0:00}:30", i));
            }
        }

        private void AddEdit()
        {
            //comboFuelType.ItemsSource = Enum.GetValues(typeof(Car.FuelTypeEnum));
            //comboFuelType.SelectedItem = comboFuelType.Items[0];
            if (passenger == null)
            {
                btAdd.Content = "Add";
            }
            else
            {
                btAdd.Content = "Save";
                lblId.Content = passenger.Id;
                tbName.Text = passenger.Name;
                tbPassportNo.Text = passenger.PassportNo;
                tbDestination.Text = passenger.Destination;
                dpDepDate.Text = passenger.DepDate;
                comboDepTime.Text = passenger.DepTime;
            }
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            if (passenger == null)
            {
                return;
            }
            try
            {
                Globals.DB.Delete(passenger.Id);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(this, ex.Message, "Input Data Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            this.DialogResult = true;
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            string name = tbName.Text;
            string passportNo = tbPassportNo.Text;
            string destination = tbDestination.Text;
            //string depDate = tbDepDate.Text;
            string depDate = dpDepDate.Text;
            string depTime = comboDepTime.Text;
            try
            {
                if (passenger == null)
                {
                    //carList.Add(new Car(makeModel, engineSizeL, fuelType));
                    Globals.DB.Add(new Passenger(name, passportNo, destination, depDate, depTime));
                }
                else
                {
                    passenger.Name = name;
                    passenger.PassportNo = passportNo;
                    passenger.Destination = destination;
                    passenger.DepDate = depDate;
                    passenger.DepTime = depTime;
                    Globals.DB.Update(passenger);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(this, ex.Message, "Input Data Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(this, ex.Message, "Input Data Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            this.DialogResult = true;
        }
    }
}