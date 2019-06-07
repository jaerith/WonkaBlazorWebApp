using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WonkaBlazorWebApp.Client.Shared
{
    public static class SimpleCache
    {
        public static WonkaBlazorWebApp.Shared.WBWARuleTree LastRuleTreeCreated { get; set; }
    }
}
