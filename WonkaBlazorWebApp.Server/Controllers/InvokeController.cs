using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

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
        public string Post(WonkaBlazorWebApp.Shared.WBWAInvokeRecord poInvokeRecord)
        {
            string sResponseJsonMsg = "";

            if (poInvokeRecord != null)
            {
                using (var client = new HttpClient())
                {
                    HttpMethod methodType = HttpMethod.Put;
                    if (!poInvokeRecord.InvokeOnChain)
                        methodType = HttpMethod.Post;

                    using (var request = new HttpRequestMessage(methodType, Shared.WBWAConstants.CONST_REST_API_BASE_URL + "/api/Invoke"))
                    {
                        var json = JsonConvert.SerializeObject(poInvokeRecord.RecordData);
                        using (var stringContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json"))
                        {
                            request.Content = stringContent;

                            var responseMessage = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).Result;

                            sResponseJsonMsg = responseMessage.Content.ReadAsStringAsync().Result;
                        }
                    }
                }
            }

            return sResponseJsonMsg;
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
