using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ColorAPI.Controllers
{
    public class ColorsController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new List<string>() { Color.Red.Name, Color.Green.Name, Color.Blue.Name };
        }

        // GET api/values/5
        public string Get(string hexString)
        {
            return ((Color)new ColorConverter().ConvertFromString(string.Concat("#",hexString))).Name;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
