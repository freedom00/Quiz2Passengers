using System;
using System.Text.RegularExpressions;

namespace Quiz2Passengers
{
    public class Passenger
    {
        public int Id { get; set; } // INT PK IDENTITY(1,1)
        private string name; // VC(100) NOT NULL
        private string passportNo; // VC(10) NOT NULL
        private string destination; // VC(100) NOT NULL
        private string depDate;
        private string depTime;
        private DateTime departureDateTime;

        public DateTime DepartureDateTime { get => departureDateTime; set => departureDateTime = value; } // DateTime type, visible as two inputs in UI

        public string Name
        {
            get => name;
            set
            {
                if (value.Length < 1 || value.Length > 100)
                {
                    throw new ArgumentException("Name must be 1-100.");
                }
                name = value;
            }
        }

        public string PassportNo
        {
            get => passportNo;
            set
            {
                if (!Regex.IsMatch(value, @"[A-Z]{2}\d{6}"))
                {
                    throw new ArgumentException("Password number must be two uppercase letters followed by 8 digits.");
                }
                passportNo = value;
            }
        }

        public string Destination
        {
            get => destination;
            set
            {
                if (value.Length < 1 || value.Length > 100)
                {
                    throw new ArgumentException("Destination must be 1-100.");
                }
                destination = value;
            }
        }

        public string DepDate
        {
            get => departureDateTime.ToString("yyyy-MM-dd");
            set
            {
                depDate = value;
                departureDateTime = Convert.ToDateTime(depDate + " " + depTime);
            }
        }

        public string DepTime
        {
            get => departureDateTime.ToString("HH:mm");
            set
            {
                depTime = value;
                departureDateTime = Convert.ToDateTime(depDate + " " + depTime);
            }
        }

        public Passenger(string name, string passportNo, string destination, DateTime departureDateTime)
        {
            Name = name;
            PassportNo = passportNo;
            Destination = destination;
            DepartureDateTime = departureDateTime;
        }

        public Passenger(string name, string passportNo, string destination, string depDate, string depTime)
        {
            Name = name;
            PassportNo = passportNo;
            Destination = destination;
            DepDate = depDate;
            DepTime = depTime;
        }

        public Passenger(int id, string name, string passportNo, string destination, DateTime departureDateTime)
        {
            Id = id;
            Name = name;
            PassportNo = passportNo;
            Destination = destination;
            DepartureDateTime = departureDateTime;
        }
    }
}