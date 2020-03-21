using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz2Passengers
{
    internal class Database
    {
        private static Database uniqueInstance;
        private static readonly object locker = new object();
        private const string CONNECTION_STRING = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\jonat\OneDrive\Documents\VisualStudio\workspaces\Quiz2Passengers\Quiz2Passengers\PassengerDB.mdf;Integrated Security=True;Connect Timeout=30";
        private SqlConnection sqlConnection;

        private Database()
        {
            sqlConnection = new SqlConnection(CONNECTION_STRING);
            sqlConnection.Open();
        }

        public static Database GetInstance()
        {
            if (uniqueInstance == null)
            {
                lock (locker)
                {
                    if (uniqueInstance == null)
                    {
                        uniqueInstance = new Database();
                    }
                }
            }
            return uniqueInstance;
        }

        public void Close()
        {
            sqlConnection.Close();
        }

        public int Add(Passenger passenger)
        {
            SqlCommand cmdInsert = new SqlCommand("INSERT INTO Passengers(Name, PassportNo, Destination, DepartureDateTime) VALUES(@Name, @PassportNo, @Destination, @DepartureDateTime); SELECT SCOPE_IDENTITY()", sqlConnection);
            cmdInsert.Parameters.AddWithValue("@Name", passenger.Name);
            cmdInsert.Parameters.AddWithValue("@PassportNo", passenger.PassportNo);
            cmdInsert.Parameters.AddWithValue("@Destination", passenger.Destination);
            cmdInsert.Parameters.AddWithValue("@DepartureDateTime", passenger.DepartureDateTime);
            int id = Convert.ToInt32(cmdInsert.ExecuteScalar().ToString());
            cmdInsert.Dispose();
            return id;
        }

        public List<Passenger> GetAll()
        {
            SqlCommand cmdSelectAll = new SqlCommand("SELECT * FROM Passengers", sqlConnection);
            SqlDataReader selectAllReader = cmdSelectAll.ExecuteReader();
            List<Passenger> result = new List<Passenger>();
            while (selectAllReader.Read())
            {
                int id = selectAllReader.GetInt32(selectAllReader.GetOrdinal("Id"));
                string name = selectAllReader.GetString(selectAllReader.GetOrdinal("Name"));
                string passportNo = selectAllReader.GetString(selectAllReader.GetOrdinal("PassportNo"));
                string destination = selectAllReader.GetString(selectAllReader.GetOrdinal("Destination"));
                DateTime departureDateTime = selectAllReader.GetDateTime(selectAllReader.GetOrdinal("DepartureDateTime"));
                result.Add(new Passenger(id, name, passportNo, destination, departureDateTime));
            }
            selectAllReader.Close();
            cmdSelectAll.Dispose();
            return result;
        }

        public void Update(Passenger passenger)
        {
            SqlCommand cmdUpdate = new SqlCommand("UPDATE Passengers SET Name=@Name, PassportNo=@PassportNo,Destination=@Destination,DepartureDateTime=@DepartureDateTime WHERE Id=@Id", sqlConnection);
            cmdUpdate.Parameters.AddWithValue("@Id", passenger.Id);
            cmdUpdate.Parameters.AddWithValue("@Name", passenger.Name);
            cmdUpdate.Parameters.AddWithValue("@PassportNo", passenger.PassportNo);
            cmdUpdate.Parameters.AddWithValue("@Destination", passenger.Destination);
            cmdUpdate.Parameters.AddWithValue("@DepartureDateTime", passenger.DepartureDateTime);
            cmdUpdate.ExecuteNonQuery();
            cmdUpdate.Dispose();
        }

        public void Delete(int id)
        {
            SqlCommand cmdDelete = new SqlCommand("DELETE FROM Passengers WHERE Id=@id", sqlConnection);
            cmdDelete.Parameters.AddWithValue("@Id", id);
            cmdDelete.ExecuteNonQuery();
            cmdDelete.Dispose();
        }
    }
}