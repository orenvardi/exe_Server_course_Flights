using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProjFlights.Models.DAL;

namespace FinalProjFlights.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string FlightID { get; set; }
        public string Airline { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string DepDate { get; set; }
        public string DepTime { get; set; }
        public double Price { get; set; }
        public int PassengerQty { get; set; }
        public string OrderDate { get; set; }
        public string OrderTime { get; set; }
        public string Passengers { get; set; }

        public Order(int orderID, string flightID, string airline, string origin, string destination, string depDate, string depTime, double price, int passengerQty, string orderDate, string orderTime, string passengers)
        {
            OrderID = orderID;
            FlightID = flightID;
            Airline = airline;
            Origin = origin;
            Destination = destination;
            DepDate = depDate;
            DepTime = depTime;
            Price = price;
            PassengerQty = passengerQty;
            OrderDate = orderDate;
            OrderTime = orderTime;
            Passengers = passengers;
        }

        public Order() { }
        
        public List<Order> getOrders()
        {
            DBservices dBservices = new DBservices();
            List<Order> OrdersList = dBservices.getOrdersList();
            return OrdersList;
        }

        public int insertOrder(Order order)
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.insertOrder(order);
            return numAffected;
        }
    }
}