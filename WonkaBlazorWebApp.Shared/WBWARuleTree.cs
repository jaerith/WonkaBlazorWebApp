using System;
using System.Collections.Generic;
using System.Text;

namespace WonkaBlazorWebApp.Shared
{
    public class WBWARuleTree
    {
        public string GroveId { get; set; }

        public string RuleTreeId { get; set; }

        public string RuleTreeOriginUrl { get; set; }

        public bool UsingOrchestrationMode { get; set; }

        public bool SerializeToBlockchain { get; set; }
    }
}
