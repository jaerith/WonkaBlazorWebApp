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

    public class FetchChainDataIndexModel : ComponentBase
    {
        [Inject]
        protected HttpClient Client { get; set; }

        protected WBWATreeAttrSource[] attrSources;

        protected bool requestDone;
        protected int  attrChosenIdx;

        protected string typeOfSrc     = "Contract";
        protected string contractAddr  = "Blah";
        protected string ctrtGetMethod = "Blah";

        protected string attrName  = "NewVATAmountForHMRC";
        protected string attrValue = "dummyVal";

        public async Task AttrNameChanged(UIChangeEventArgs evt)
        {
            attrChosenIdx = Int32.Parse(evt.Value.ToString());
            attrName      = attrSources[attrChosenIdx].AttributeName;
        }

        protected override async Task OnInitAsync()
        {
            attrChosenIdx = 0;

            string sLastRuleTreeCreated = WonkaBlazorWebApp.Client.Shared.SimpleCache.LastRuleTreeCreated;

            if (attrSources == null)
            {
                if (!String.IsNullOrEmpty(sLastRuleTreeCreated))
                {
                    WonkaBlazorWebApp.Shared.WBWARuleTree RuleTreeInfo =
                        await Client.GetJsonAsync<WonkaBlazorWebApp.Shared.WBWARuleTree>("api/RuleTree?rtid=" + sLastRuleTreeCreated);

                    attrSources = RuleTreeInfo.AttributeSources.ToArray();
                }
                else
                {
                    WBWARuleTree SampleRuleTree = await Client.GetJsonAsync<WBWARuleTree>("sample-data/ruletree.json");
                    attrSources = SampleRuleTree.AttributeSources.ToArray();
                }

                if ((attrSources != null) && (attrSources.Length > 0))
                { 
                    typeOfSrc     = attrSources[0].BlockchainTypeOfSource;
                    contractAddr  = attrSources[0].BlockchainContractAddress;
                    ctrtGetMethod = attrSources[0].BlockchainGetValueMethod;
                    attrName      = attrSources[0].AttributeName;
                }
            }

            base.OnInitAsync();
        }

        protected async Task GetValue()
        {
            Dictionary<string,string> record = await Client.GetJsonAsync<Dictionary<string,string>>("api/chain?name=" + attrName);

            if (record.ContainsKey(WBWAConstants.CONST_CHAIN_RECORD_KEY_ATTR_VAL))
            {
                attrValue = record[WBWAConstants.CONST_CHAIN_RECORD_KEY_ATTR_VAL];
            }
        }

        protected async Task Refresh()
        {
            if (String.IsNullOrEmpty(attrValue))
            {
                attrValue = await Client.GetJsonAsync<string>("api/chain?name=" + attrName);
            }
        }

    }

}
