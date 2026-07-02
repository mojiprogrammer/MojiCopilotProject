using DanaCopilot.Application.Modules.RunTime.Models;
using System.Threading.Channels;

namespace DanaCopilot.Application.Modules.RunTime
{
    public static class RuntimeChannel
    {
        public static readonly Channel<RuntimeDataItem> Channel = System.Threading.Channels.Channel.CreateUnbounded<RuntimeDataItem>();
    }
}
