using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProjFlights.Models.DAL;

namespace FinalProjFlights.Models
{
    public class Airport
    {
        public string AirportID { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string NameID { get; set; }
        public string Name { get; set; }

        public Airport() { }

        public Airport(string AirportID, double Lat, double Lon, string NameID, string Name)
        {
            this.AirportID = AirportID;
            this.Lat = Lat;
            this.Lon = Lon;
            this.NameID = NameID;
            this.Name = Name;
        }

        public void insert(List<Airport> airport)
        {
            DBservices dbs = new DBservices();
            dbs.insert(airport);
        }

        public List<Airport> getAirports()
        {
            DBservices dBservices = new DBservices();
            List<Airport> AirportList = dBservices.ReturnAirportsList();
            return AirportList;
        }
    }
}