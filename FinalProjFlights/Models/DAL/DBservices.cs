using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Text;
using FinalProjFlights.Models;

namespace FinalProjFlights.Models.DAL
{
    public class DBservices
    {
        public SqlDataAdapter da;
        public DataTable dt;

        public DBservices()
        {
        }

        //--------------------------------------------------------------------------------------------------
        // 1.  This method creates a connection to the database according to the connectionString name in the web.config 
        //--------------------------------------------------------------------------------------------------
        public SqlConnection connect(String conString)
        {
            // read the connection string from the configuration file
            string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }
        //---------------------------------------------------------------------------------
        // 2.  Create the SqlCommand
        //---------------------------------------------------------------------------------
        private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object
            cmd.Connection = con;              // assign the connection to the command object
            cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 
            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds
            cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure
            return cmd;
        }
        //--------------------------------------------------------------------------------------------------
        // 3.  INSERT Airport - This method inserts a Airport to the Airport2_2020 table 
        //--------------------------------------------------------------------------------------------------
        public void insert(List<Airport> airport)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {// write to log
                throw (ex);
            }
            try
            {
                String cStr = "";
                string x = "";
                //StringBuilder sb = new StringBuilder();
                for (int i = 0; i < airport.Count; i++)
                {

                    cStr = BuildInsertCommand(airport[i]);      // helper method to build the insert string
                    //sb.Append(cStr);
                    x = x + cStr;

                }
                cmd = CreateCommand(x, con);             // create the command
                cmd.ExecuteNonQuery();       // execute the command
            }
            catch (Exception ex)
            { // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {// close the db connection
                    con.Close();
                }
            }
        }
        //--------------------------------------------------------------------
        // 4.  Build INSERT Airport Command
        //--------------------------------------------------------------------
        private String BuildInsertCommand(Airport airport)
        {
            String command;
            airport.Name = airport.Name.Replace("'", "");
            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}', '{4}');", airport.AirportID, airport.Lat, airport.Lon, airport.NameID, airport.Name);
            String prefix = "INSERT INTO Airport2_2020 " + "(AirportID,Lat,Lon,NameID,Name) ";
            command = prefix + sb.ToString();
            return command;
        }
        //--------------------------------------------------------------------
        // 5.  SELECT Airport 
        //--------------------------------------------------------------------
        public List<Airport> ReturnAirportsList()
        {
            List<Airport> allAirports = new List<Airport>();
            Airport oneAirport = new Airport();
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "select * from [Airport2_2020]";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                // get a reader
                SqlDataReader dr2 = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                if (dr2.HasRows)
                {
                    while (dr2.Read())
                    {   // Read till the end of the data into a row
                        oneAirport.AirportID = (string)(dr2["AirportID"]);
                        oneAirport.NameID = (string)dr2["NameID"];
                        oneAirport.Name = (string)dr2["Name"];

                        allAirports.Add(oneAirport);
                        oneAirport = new Airport();
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return allAirports;
        }
        //--------------------------------------------------------------------
        // 6.  SELECT Sales
        //--------------------------------------------------------------------
        public List<Sales> ReturnSalesList()
        {
            List<Sales> allSales = new List<Sales>();
            Sales oneSale = new Sales();
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "select * from [FlightsSales]";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                // get a reader
                SqlDataReader dr2 = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                if (dr2.HasRows)
                {
                    while (dr2.Read())
                    {   // Read till the end of the data into a row
                        oneSale.SaleID = Convert.ToInt32(dr2["SaleID"]);
                        oneSale.Airline = (string)dr2["Airline"];
                        oneSale.Origin = (string)dr2["Origin"];
                        oneSale.Destination = (string)dr2["Destination"];
                        oneSale.StartDate = (string)dr2["StartDate"];
                        oneSale.EndDate = (string)dr2["EndDate"];
                        oneSale.Discount = Convert.ToDouble(dr2["Discount"]);
                        allSales.Add(oneSale);
                        oneSale = new Sales();
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return allSales;
        }
        //--------------------------------------------------------------------------------------------------
        // 7.  INSERT Favorite - This method inserts a Favorite to the FlightsFavorites table 
        //--------------------------------------------------------------------------------------------------
        public int insertFavorite(Favorite Fav)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            String cStr = BuildInsertCommandFavorite(Fav);      // helper method to build the insert string
            cmd = CreateCommand(cStr, con);             // create the command
            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        //--------------------------------------------------------------------
        // 8.  Build INSERT Favorite Command
        //--------------------------------------------------------------------
        private String BuildInsertCommandFavorite(Favorite fav)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}', '{4}','{5}','{6}');", fav.id, fav.Airline, fav.DepartureCity, fav.ArrivalCity, fav.DepartureTime, fav.ArrivalTime,fav.ArrivalHour);
            String prefix = "INSERT INTO FlightsFavorites" + "(id,Airline,DepartureCity,ArrivalCity,DepartureTime,ArrivalTime,ArrivalHour) ";
            command = prefix + sb.ToString();
            return command;
        }
        //--------------------------------------------------------------------
        // 9.  SELECT Favorite
        //--------------------------------------------------------------------
        public List<Favorite> ReturnFavoritesList()
        {
            List<Favorite> allFavorites = new List<Favorite>();
            Favorite oneFavorite = new Favorite();
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "select [id],[Airline],[DepartureCity],[ArrivalCity],[DepartureTime],[ArrivalTime],ArrivalHour,count(*) as CountFav from[dbo].[FlightsFavorites] group by[Airline],[DepartureCity],[ArrivalCity],[DepartureTime],[ArrivalTime],[id], ArrivalHour";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                // get a reader
                SqlDataReader dr2 = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                if (dr2.HasRows)
                {
                    while (dr2.Read())
                    {  
                        //oneFavorite.id = (string)(dr2["id"]);
                        oneFavorite.Airline = (string)dr2["Airline"];
                        oneFavorite.DepartureCity = (string)dr2["DepartureCity"];
                        oneFavorite.ArrivalCity = (string)dr2["ArrivalCity"];
                        oneFavorite.DepartureTime = (string)dr2["DepartureTime"];
                        oneFavorite.ArrivalTime = (string)dr2["ArrivalTime"];
                        oneFavorite.ArrivalHour = (string)dr2["ArrivalHour"];
                        oneFavorite.CountFav = (int)dr2["CountFav"];
                        allFavorites.Add(oneFavorite);
                        oneFavorite = new Favorite();
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return allFavorites;
        }
        //--------------------------------------------------------------------------------------------------
        // 10.  INSERT Customer - This method inserts a Customer to the FlightsCustomers table 
        //--------------------------------------------------------------------------------------------------
        public int insertCustomer(Customer customer)
        {
            SqlConnection con;
            SqlCommand cmd2;
            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            String cStr = BuildInsertCommandCustomer(customer);      // helper method to build the insert string
            cmd2 = CreateCommand(cStr, con);             // create the command
            try
            {
                int numEffected = cmd2.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        //--------------------------------------------------------------------
        // 11.  Build INSERT Customer Command
        //--------------------------------------------------------------------
        private String BuildInsertCommandCustomer(Customer customer)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("SELECT DISTINCT '{0}', '{1}', '{2}', '{3}', '{4}','{5}'", customer.FirstName, customer.LastName, customer.Mail, customer.Passport, customer.Address, customer.Phone);
            String prefix = "INSERT INTO FlightsCustomers" + "(FirstName,LastName,Mail,Passport,Address,Phone) ";
            String condition = " FROM FlightsCustomers WHERE NOT EXISTS (SELECT * FROM FlightsCustomers WHERE Mail = '" + customer.Mail + "');";
            command = prefix + sb.ToString() + condition;
            return command;
        }
        //--------------------------------------------------------------------------------------------------
        // 12.  INSERT Order - This method inserts a Order to the FlightsOrders table 
        //--------------------------------------------------------------------------------------------------
        public int insertOrder(Order order)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            String cStr = BuildInsertCommandOrder(order);      // helper method to build the insert string        
            cmd = CreateCommand(cStr, con);             // create the command
            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        //--------------------------------------------------------------------
        // 13.  Build INSERT Order Command
        //--------------------------------------------------------------------
        private String BuildInsertCommandOrder(Order order)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}', '{4}','{5}','{6}','{7}','{8}','{9}','{10}');", order.FlightID, order.Airline, order.Origin, order.Destination, order.DepDate, order.DepTime, order.Price, order.PassengerQty, order.OrderDate, order.OrderTime, order.Passengers);
            String prefix = "INSERT INTO FlightsOrders" + "(FlightID,Airline,Origin,Destination,DepDate,DepTime,Price,PassengerQty,OrderDate,OrderTime,Passengers) ";
            command = prefix + sb.ToString();
            return command;
        }
        //--------------------------------------------------------------------
        // 14.  SELECT ManagerDetailsList
        //--------------------------------------------------------------------

        public List<ManagerDetails> ReturnManagerDetailsList()
        {
            List<ManagerDetails> allManagerDetails = new List<ManagerDetails>();
            ManagerDetails oneManagerDetails = new ManagerDetails();
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "select * from FlightsUsers";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                // get a reader
                SqlDataReader dr2 = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                if (dr2.HasRows)
                {
                    while (dr2.Read())
                    {   // Read till the end of the data into a row
                        oneManagerDetails.UserName = (string)(dr2["UserName"]);
                        oneManagerDetails.Password = (string)dr2["Password"];
                        oneManagerDetails.FirstName = (string)dr2["FirstName"];
                        oneManagerDetails.LastName = (string)dr2["LastName"];

                        allManagerDetails.Add(oneManagerDetails);
                        oneManagerDetails = new ManagerDetails();
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return allManagerDetails;
        }
        //--------------------------------------------------------------------------------------------------
        // 15. DELETE Sale
        //--------------------------------------------------------------------------------------------------
        public int Deletesale(int Id)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            String cStr = "delete from FlightsSales where SaleID = " + Id.ToString();      // helper method to build the insert string
            cmd = CreateCommand(cStr, con);             // create the command
            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }      
        //--------------------------------------------------------------------------------------------------
        // 16.  PUT Sale - This method update a sale 
        //--------------------------------------------------------------------------------------------------
        public int PutSale(Sales s)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            String cStr = BuildPutCommandSale(s);      // helper method to build the insert string
            cmd = CreateCommand(cStr, con);             // create the command
            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        //--------------------------------------------------------------------
        // 17.  Build PUT Sale Command
        //--------------------------------------------------------------------
        private String BuildPutCommandSale(Sales sale)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            String prefix = "UPDATE FlightsSales SET [Airline] = '"+ sale.Airline+ "', [Origin] = '"+sale.Origin+ "', [Destination] = '" + sale.Destination + "', [StartDate] = '" + sale.StartDate + "', [EndDate] =  '" + sale.EndDate + "', [Discount] =  " + sale.Discount + " WHERE [SaleID] = " + sale.SaleID+"";
            command = prefix;
            return command;
        }
        //--------------------------------------------------------------------------------------------------
        // 18.  POST Sale - This method post a sale 
        //--------------------------------------------------------------------------------------------------
        public int PostSale(Sales s)
    {
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("DBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        String cStr = BuildPOSTCommandSale(s);      // helper method to build the insert string
        cmd = CreateCommand(cStr, con);             // create the command
        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }
        //--------------------------------------------------------------------
        // 19.  Build POST Sale Command
        //--------------------------------------------------------------------
        private String BuildPOSTCommandSale(Sales sale)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}', '{4}','{5}');", sale.Airline, sale.Origin, sale.Destination, sale.StartDate, sale.EndDate, sale.Discount);
            String prefix = "INSERT INTO FlightsSales" + "(Airline,Origin,Destination,StartDate,EndDate,Discount) ";
            command = prefix + sb.ToString();
            return command;
        }

        //--------------------------------------------------------------------
        // 20.  SELECT OrdersList
        //--------------------------------------------------------------------
        public List<Order> getOrdersList()
        {
            List<Order> allOrders = new List<Order>();
            Order oneOrder = new Order();
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "select * from FlightsOrders";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                // get a reader
                SqlDataReader dr2 = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                if (dr2.HasRows)
                {
                    while (dr2.Read())
                    {   // Read till the end of the data into a row
                        oneOrder.OrderID = Convert.ToInt32(dr2["OrderID"]);
                        oneOrder.FlightID = (string)dr2["FlightID"];
                        oneOrder.Airline = (string)dr2["Airline"];
                        oneOrder.Origin = (string)dr2["Origin"];
                        oneOrder.Destination = (string)dr2["Destination"];
                        oneOrder.DepDate = (string)dr2["DepDate"];
                        oneOrder.DepTime = (string)dr2["DepTime"];
                        oneOrder.Price = Convert.ToDouble(dr2["Price"]);
                        oneOrder.PassengerQty = Convert.ToInt32(dr2["PassengerQty"]);
                        oneOrder.OrderDate = (string)dr2["OrderDate"];
                        oneOrder.OrderTime = (string)dr2["OrderTime"];
                        oneOrder.Passengers = (string)dr2["Passengers"];

                        allOrders.Add(oneOrder);
                        oneOrder = new Order();
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return allOrders;
        }
        //--------------------------------------------------------------------
        // 21.  SELECT - check Sales and Discount
        //--------------------------------------------------------------------
        public bool GetDiscountSales(Sales sale)
        {
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                String cStr = BuildGETSaleDiscountCommand(sale);      // helper method to build the insert string
                SqlCommand cmd = new SqlCommand(cStr, con);
                // get a reader
                SqlDataReader dr2 = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                if (dr2.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        //--------------------------------------------------------------------
        // 19.  Build GET Sale discount Command
        //--------------------------------------------------------------------
        private String BuildGETSaleDiscountCommand(Sales sale)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("SELECT * FROM FlightsOrders WHERE Airline = '{0}' AND Origin = '{1}' " +
                "AND Destination = '{2}' AND depDate >= '{3}' AND depDate <= '{4}';", sale.Airline, sale.Origin, sale.Destination, sale.StartDate, sale.EndDate);
            command = sb.ToString();
            return command;
        }
    }
}