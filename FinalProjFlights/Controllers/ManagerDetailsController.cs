using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalProjFlights.Models;

namespace FinalProjFlights.Controllers
{
    public class ManagerDetailsController : ApiController
    {
        public List<ManagerDetails> Get()
        {
            ManagerDetails md = new ManagerDetails();
            return md.getManagerDetails();

        }
    }
}
