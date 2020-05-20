using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProjFlights.Models.DAL;

namespace FinalProjFlights.Models
{
    public class ManagerDetails
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ManagerDetails() { }

        public ManagerDetails(string UserName, string Password, string FirstName, string LastName)
        {
            this.UserName = UserName;
            this.Password = Password;
            this.FirstName = FirstName;
            this.LastName = LastName;
          
        }

   
        List<ManagerDetails> ManagerDetailsList = new List<ManagerDetails>();
        public List<ManagerDetails> getManagerDetails()
        {
            DBservices dBservices = new DBservices();
            ManagerDetailsList = dBservices.ReturnManagerDetailsList();
            return ManagerDetailsList;
        }
    }
}