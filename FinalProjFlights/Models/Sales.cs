using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProjFlights.Models.DAL;

namespace FinalProjFlights.Models
{
    public class Sales
    {
        public int SaleID { get; set; }
        public string Airline { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public double Discount { get; set; }
        public double DiscountOfCustomer { get; set; }


        public Sales(int saleID, string airline, string origin, string destination, string startDate, string endDate, double discount, double discountOfCustomer)
        {
            SaleID = saleID;
            Airline = airline;
            Origin = origin;
            Destination = destination;
            StartDate = startDate;
            EndDate = endDate;
            Discount = discount;
            discountOfCustomer = DiscountOfCustomer;
        }

        public Sales() { }

        public List<Sales> GetSales()
        {
            DBservices dBservices = new DBservices();
            List<Sales> salesList = dBservices.ReturnSalesList();
            return salesList;
        }

        public int DeleteSale(int id)
        {
            DBservices dbs = new DBservices();
            return dbs.Deletesale(id);
        }

        public int PutSale(Sales s)
        {
            DBservices dbs = new DBservices();
            return dbs.PutSale(s);
        }
        
        public int PostSale(Sales s)
        {
            DBservices dbs = new DBservices();
            return dbs.PostSale(s);
        }

        public List<Sales> DiscountSales()
        {
            DBservices dBservices = new DBservices();
            List<Sales> salesList = dBservices.ReturnSalesList();
            int customerDiscount = 50;
            int customerDiscount2 = 60;
            for (int i = 0; i < salesList.Count; i++)
            {
                var check = dBservices.GetDiscountSales(salesList[i]);
                salesList[i].DiscountOfCustomer = salesList[i].Discount * (check == true ? customerDiscount : customerDiscount2) / 100;
            }
            return salesList;
        }
    }
}