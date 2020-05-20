using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalProjFlights.Models;

namespace FinalProjFlights.Controllers
{
    public class OrderController : ApiController
    {
        // POST api/Order
        public void Post([FromBody] Order o)
        {
            Order customer = new Order();
            customer.insertOrder(o);
        }

        // GET api/<controller>/5
        public List<Order> Get()
        {
            Order order = new Order();
            return order.getOrders();
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
