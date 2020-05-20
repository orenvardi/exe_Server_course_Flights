using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalProjFlights.Models;

namespace FinalProjFlights.Controllers
{
    public class CustomerController : ApiController
    {
        // POST api/Customer
        public void Post([FromBody] Customer c)
        {
            Customer customer = new Customer();
            customer.insertCustomer(c);
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
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