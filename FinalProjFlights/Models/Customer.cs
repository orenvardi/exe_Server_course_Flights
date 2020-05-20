using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProjFlights.Models.DAL;

namespace FinalProjFlights.Models
{
    public class Customer
    {
        public int CustID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string Passport { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public Customer(int custID, string firstName, string lastName, string mail, string passport, string address, string phone)
        {
            CustID = custID;
            FirstName = firstName;
            LastName = lastName;
            Mail = mail;
            Passport = passport;
            Address = address;
            Phone = phone;
        }

        public Customer() { }

        public int insertCustomer(Customer customer)
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.insertCustomer(customer);
            return numAffected;
        }
    }
}