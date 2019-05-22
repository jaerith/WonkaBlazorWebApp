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
        public string GetReports(string RuleTreeId)
        {
            string sReportsJson = "";

            if (!String.IsNullOrEmpty(RuleTreeId))
            {
                using (HttpClient client = new HttpClient())
                {
                    var ResponseMsg = client.GetAsync(Shared.WBWAConstants.CONST_REST_API_BASE_URL + "/api/Report?RuleTreeId=" + RuleTreeId).Result;

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
