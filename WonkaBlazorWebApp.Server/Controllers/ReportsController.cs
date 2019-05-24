using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WonkaBlazorWebApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        /*
        // GET: api/Reports
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        */

        // GET: api/Reports/5
        // [HttpGet("{id}", Name = "Get")]
        public string GetReports(string RuleTreeId = null, string GroveId = null)
        {
            string sReportsJson = "";

            if (!String.IsNullOrEmpty(RuleTreeId) || !String.IsNullOrEmpty(GroveId))
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage ResponseMsg = new HttpResponseMessage();

                    if (!String.IsNullOrEmpty(RuleTreeId))
                        ResponseMsg = client.GetAsync(Shared.WBWAConstants.CONST_REST_API_BASE_URL + "/api/Report?RuleTreeId=" + RuleTreeId).Result;
                    else if (!String.IsNullOrEmpty(GroveId))
                        ResponseMsg = client.GetAsync(Shared.WBWAConstants.CONST_REST_API_BASE_URL + "/api/Report?GroveId=" + GroveId).Result;

                    sReportsJson = ResponseMsg.Content.ReadAsStringAsync().Result;

                    // FOR TESTING ONLY
                    if ((RuleTreeId == "TEST") && !sReportsJson.Contains("RuleSetFailMessages"))
                    {
                        int nLastCurlyBrace = sReportsJson.LastIndexOf("}");

                        sReportsJson = sReportsJson.Remove(nLastCurlyBrace).Insert(nLastCurlyBrace, ", \"RuleSetFailMessages\": { } }]");
                    }
                }
            }

            return sReportsJson;
        }

        // POST: api/Reports
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Reports/5
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
