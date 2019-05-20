using System;
using System.Collections.Generic;
using System.Text;

namespace WonkaBlazorWebApp.Shared
{
    public class WBWAGrove
    {
        public string GroveId { get; set; }

        public string GroveDesc { get; set; }

        public List<string> GroveMembers { get; set; }

        public bool SerializeToBlockchain { get; set; }

    }
}
