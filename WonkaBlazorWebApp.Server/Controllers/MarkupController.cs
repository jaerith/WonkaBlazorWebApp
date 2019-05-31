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
    public class MarkupController : ControllerBase
    {
        // [HttpGet("{id}", Name = "Get")]
        public string Get(string MarkupStorageUrl)
        {
            string sRulesMarkupXml = "";

            if (MarkupStorageUrl != null)
            {
                using (var client = new HttpClient())
                {
                    HttpMethod methodType = HttpMethod.Get;

                    using (var request = new HttpRequestMessage(methodType, Shared.WBWAConstants.CONST_REST_API_BASE_URL + "/api/Markup?MarkupId=" + MarkupStorageUrl))
                    {
                        using (var stringContent = new StringContent(MarkupStorageUrl, System.Text.Encoding.UTF8, "application/json"))
                        {
                            request.Content = stringContent;

                            var responseMessage = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).Result;

                            sRulesMarkupXml = responseMessage.Content.ReadAsStringAsync().Result;
                        }
                    }
                }
            }

            return sRulesMarkupXml;
        }

        [HttpPost]
        public string Post([FromBody] string RulesMarkupXml)
        {
            string sMarkupStorageUrl = "";

            if (RulesMarkupXml != null)
            {
                using (var client = new HttpClient())
                {
                    HttpMethod methodType = HttpMethod.Post;

                    using (var request = new HttpRequestMessage(methodType, Shared.WBWAConstants.CONST_REST_API_BASE_URL + "/api/markup"))
                    {
                        using (var stringContent = new StringContent(RulesMarkupXml, System.Text.Encoding.UTF8, "application/xml"))
                        {
                            request.Content = stringContent;

                            var responseMessage = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).Result;

                            sMarkupStorageUrl = responseMessage.Content.ReadAsStringAsync().Result;
                        }
                    }
                }
            }

            return sMarkupStorageUrl;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
