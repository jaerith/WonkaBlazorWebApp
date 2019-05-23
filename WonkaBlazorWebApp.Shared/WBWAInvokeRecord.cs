using System;
using System.Collections.Generic;
using System.Text;

namespace WonkaBlazorWebApp.Shared
{
    public class WBWAInvokeRecord
    {
        public Dictionary<string, string> RecordData;

        public WBWAInvokeRecord()
        {
            InvokeOnChain = false;
            RecordData    = new Dictionary<string, string>();
        }

        public void SetRuleTreeId(string psRuleTreeId)
        {
            RecordData["RuleTreeId"] = psRuleTreeId;
        }

        public bool InvokeOnChain { get; set; }
    }
}
