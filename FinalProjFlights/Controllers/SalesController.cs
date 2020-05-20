using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalProjFlights.Models;

namespace FinalProjFlights.Controllers
{
    public class SalesController : ApiController
    {
        // GET api/Sales
        public List<Sales> Get()
        {
            Sales sale = new Sales();
            return sale.GetSales();
        }


        [HttpGet]
        [Route("api/Sales/{Discount}")]
        // GET api/Sales/Discount
        public List<Sales> Get2()
        {
            Sales sale = new Sales();
            return sale.DiscountSales();
        }

        // DELETE api/Sales
        public void Delete([FromBody] int id)
        {
            Sales sale = new Sales();
            sale.DeleteSale(id);
        }


        // PUT api/Sales
        public void Put([FromBody]Sales s)
        {
            Sales sale = new Sales();
            sale.PutSale(s);
        }

        // POST api/Sales
        public void Post([FromBody]Sales s)
        {
            Sales sale = new Sales();
            sale.PostSale(s);
        }
      
    }
}
