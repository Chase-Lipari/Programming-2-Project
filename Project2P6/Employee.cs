using System;
using System.Collections.Generic;
using System.Text;

namespace Project2P6
{
    class Employee
    {

        private int _idNumber;
        private string _lastName;
        private string _firstName;
        private decimal _payRate;
        private TimeStamp _hoursWorked;
        private decimal _pay;
        public int IdNumber
        {
            get { return _idNumber; }
            set { _idNumber = value; } //I decided no verification is needed, I would be making the text files anyway

        }
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; } 
        }
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        public decimal PayRate
        {
            get { return _payRate; }
            set { _payRate = value; }
        }
        public TimeStamp HoursWorked
        {
            get { return _hoursWorked; }
            set { _hoursWorked = value; }
        }
        public decimal Pay
        {
            get { return _pay; }
            set { _pay = value; }
        }
        /// <summary>
        /// Default constructor
        /// </summary>
        public Employee()
        {
            _idNumber = 0;
            _lastName = string.Empty;
            _firstName = string.Empty;
            _payRate = 0;
            _hoursWorked = new TimeStamp();
            _pay = 0;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="idNumber_"></param>
        /// <param name="firstName_"></param>
        /// <param name="lastName_"></param>
        /// <param name="payRate_"></param>
        /// <param name="hoursWorked_"></param>
        /// <param name="pay_"></param>
        public Employee(int idNumber_, string firstName_, string lastName_, decimal payRate_, TimeStamp hoursWorked_, decimal pay_ )
        {
            IdNumber = idNumber_;
            FirstName = firstName_;
            LastName = lastName_;
            PayRate = payRate_;
            HoursWorked = hoursWorked_;
            Pay = pay_;
        }
        /// <summary>
        /// simple override tostring
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0,-10} {1,-10} {2,-10} {3,-10} ${4,-10} ${5:0.00}", IdNumber, LastName, FirstName, HoursWorked, PayRate, Pay);
        }

    }
}
