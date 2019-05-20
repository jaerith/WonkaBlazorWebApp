using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WonkaBlazorWebApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvokeController : ControllerBase
    {
        /*
        // GET: api/Invoke
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        */

        // GET: api/Invoke/5
        // [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Invoke
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Invoke/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
