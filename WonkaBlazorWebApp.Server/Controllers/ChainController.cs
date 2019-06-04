using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using WonkaBlazorWebApp.Shared;

namespace WonkaBlazorWebApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChainController : ControllerBase
    {
        // GET: api/Chain/5
        // [HttpGet("{id}", Name = "Get")]
        public string GetValueOnChain(string name = null)
        {
            string sReportsJson = "";

            if (!String.IsNullOrEmpty(name))
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage ResponseMsg = new HttpResponseMessage();

                    ResponseMsg = client.GetAsync(Shared.WBWAConstants.CONST_REST_API_BASE_URL + "/api/chain?type=attrval&id=" + name).Result;

                    if (ResponseMsg.IsSuccessStatusCode)
                        sReportsJson = ResponseMsg.Content.ReadAsStringAsync().Result;
                    else
                        sReportsJson = "{ \"" + WBWAConstants.CONST_CHAIN_RECORD_KEY_ATTR_VAL + "\" : \"Failed to retrieve value\" }";
                }
            }

            return sReportsJson;
        }

    }
}
