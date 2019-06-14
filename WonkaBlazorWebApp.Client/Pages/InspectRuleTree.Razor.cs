using Microsoft.AspNetCore.Components;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

using WonkaBlazorWebApp.Shared;

namespace WonkaBlazorWebApp.Client.Pages
{

    public class InspectRuleTreeIndexModel : ComponentBase
    {
        [Inject]
        protected HttpClient Client { get; set; }

        protected WBWARuleTree ruleTreeProps;

        protected bool requestDone;

        protected string groveId        = "DummyGrove";
        protected string groveIdx       = "0";
        protected string originUrl      = "https://yoursite/rules.xml";
        protected string serializedFlag = "true";
        protected string minGasCost     = "0";
        protected string maxGasCost     = "9999999";

        protected string ruleTreeId;

        protected override async Task OnInitAsync()
        {
            if (ruleTreeProps == null)
            {
                string sLastRuleTreeCreated = WonkaBlazorWebApp.Client.Shared.SimpleCache.LastRuleTreeCreated;
                if (!String.IsNullOrEmpty(sLastRuleTreeCreated))
                {
                    ruleTreeId = sLastRuleTreeCreated;
                }
                else
                {
                    ruleTreeId = "";
                }

                if (!String.IsNullOrEmpty(ruleTreeId))
                {
                    ruleTreeProps = 
                        await Client.GetJsonAsync<WonkaBlazorWebApp.Shared.WBWARuleTree>("api/RuleTree?rtid=" + ruleTreeId);
                }
                else
                {
                    ruleTreeProps = await Client.GetJsonAsync<WBWARuleTree>("sample-data/ruletree.json");
                }

                SetPageFields();
            }

            base.OnInitAsync();
        }

        protected async Task GetTreeProps()
        {
            ruleTreeProps =
                await Client.GetJsonAsync<WonkaBlazorWebApp.Shared.WBWARuleTree>("api/RuleTree?rtid=" + ruleTreeId);

            SetPageFields();
        }

        protected async Task Refresh()
        {
            /*
            if (String.IsNullOrEmpty(attrValue))
            {
                attrValue = await Client.GetJsonAsync<string>("api/chain?name=" + attrName);
            }
            */
        }

        protected void SetPageFields()
        {
            if (ruleTreeProps != null)
            {
                groveId        = ruleTreeProps.GroveId;
                groveIdx       = Convert.ToString(ruleTreeProps.GroveIndex);
                originUrl      = ruleTreeProps.RuleTreeOriginUrl;
                serializedFlag = Convert.ToString(ruleTreeProps.SerializeToBlockchain);
                minGasCost     = Convert.ToString(ruleTreeProps.MinGasCost);
                maxGasCost     = Convert.ToString(ruleTreeProps.MaxGasCost);
            }
        }

    }

}
