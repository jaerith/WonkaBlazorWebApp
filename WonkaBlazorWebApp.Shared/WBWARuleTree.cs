using System;
using System.Collections.Generic;
using System.Text;

namespace WonkaBlazorWebApp.Shared
{
    public class WBWATreeAttrSource
    {
        public WBWATreeAttrSource()
        {}

        public WBWATreeAttrSource(string psAttrName)
        {
            AttributeName = psAttrName;
        }

        public string AttributeName { get; set; }

        public string BlockchainContractAddress { get; set; }

        public string BlockchainGetValueMethod { get; set; }

        public string BlockchainSenderAddress { get; set; }

        public string BlockchainSetValueMethod { get; set; }

        public string BlockchainSourceId { get; set; }

        public string BlockchainTypeOfSource { get; set; }
    }

    public class WBWARuleTree
    {
        public WBWARuleTree()
        {
            GroveIndex = 0;

            MinGasCost = MaxGasCost = 0;
        }

        public List<WBWATreeAttrSource> AttributeSources { get; set; }

        public string GroveId { get; set; }

        public int GroveIndex { get; set; }

        public string RuleTreeId { get; set; }

        public string RuleTreeOriginUrl { get; set; }

        public bool UsingOrchestrationMode { get; set; }

        public bool SerializeToBlockchain { get; set; }

        public int MinGasCost { get; set; }

        public int MaxGasCost { get; set; }

    }
}
