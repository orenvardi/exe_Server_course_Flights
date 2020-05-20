using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProjFlights.Models.DAL;

namespace FinalProjFlights.Models
{
    public class Favorite
    {
        public string id { get; set;}
        public string Airline { get; set; }
        public string DepartureCity { set; get; }
        public string ArrivalCity { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public string ArrivalHour { get; set; }
        public int CountFav { get; set;  }

        public Favorite() { }
        public Favorite(string id, string Airline, string DepartureCity, string ArrivalCity, string DepartureTime, string ArrivalTime,int CountFav, string ArrivalHour)
        {
            this.id = id;
            this.Airline = Airline;
            this.DepartureCity = DepartureCity;
            this.ArrivalCity = ArrivalCity;
            this.DepartureTime = DepartureTime;
            this.ArrivalTime = ArrivalTime;
            this.CountFav = CountFav;
            this.ArrivalHour = ArrivalHour;
        }

        public void insertFavorite(Favorite fav)
        {
            DBservices dbs = new DBservices();
            int num=dbs.insertFavorite(fav);           
        }

        public List<Favorite> getFavorites()
        {
            DBservices dBservices = new DBservices();
            List<Favorite> FavoritesList = dBservices.ReturnFavoritesList();
            return FavoritesList;
        }
    }
}