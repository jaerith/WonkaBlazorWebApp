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
    public class ChainController : ControllerBase
    {
        // GET: api/Chain/5
        // [HttpGet("{id}", Name = "Get")]
        public string GetReports(string name = null)
        {
            string sAttrValue = "";

            if (!String.IsNullOrEmpty(name))
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage ResponseMsg = new HttpResponseMessage();

                    ResponseMsg = client.GetAsync(Shared.WBWAConstants.CONST_REST_API_BASE_URL + "/api/chain?type=attrval&id=" + name).Result;

                    if (ResponseMsg.IsSuccessStatusCode)
                        sAttrValue = ResponseMsg.Content.ReadAsStringAsync().Result;
                    else
                        sAttrValue = "Failed to retrieve value";
                }
            }

            return sAttrValue;
        }

    }
}
