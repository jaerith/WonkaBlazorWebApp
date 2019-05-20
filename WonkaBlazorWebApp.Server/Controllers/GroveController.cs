using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

namespace WonkaBlazorWebApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroveController : ControllerBase
    {
        /*
        // GET: api/Grove
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        */

        // GET: api/Grove/5
        // [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Grove
        [HttpPost]
        public HttpResponseMessage Post(WonkaBlazorWebApp.Shared.WBWAGrove poGrove)
        {
            string sBaseUrl = "http:/yourwebsite.here.net";

            HttpResponseMessage responseMessage = new HttpResponseMessage() { StatusCode = HttpStatusCode.BadRequest };

            if (poGrove != null)
            {
                using (var client = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Post, sBaseUrl + "/api/Grove"))
                    {
                        var json = JsonConvert.SerializeObject(poGrove);
                        using (var stringContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json"))
                        {
                            request.Content = stringContent;

                            responseMessage = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).Result;

                            responseMessage.EnsureSuccessStatusCode();
                        }
                    }
                }
            }

            return responseMessage;
        }

        // PUT: api/Grove/5
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
