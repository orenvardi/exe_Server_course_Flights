using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalProjFlights.Models;

namespace FinalProjFlights.Controllers
{
    public class FavoriteController : ApiController
    {
        public void Post([FromBody] Favorite f)
        {
            Favorite Fav = new Favorite();
            Fav.insertFavorite(f);
        }
        public List<Favorite> Get()
        {
            Favorite ap = new Favorite();
            return ap.getFavorites();
        }
    }
}
