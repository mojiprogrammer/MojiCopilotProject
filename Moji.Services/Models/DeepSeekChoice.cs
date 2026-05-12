using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.Services.Models
{
    public class DeepSeekChoice
    {
        public DeepSeekChatMessage Message { get; set; } = new();
        public int Index { get; set; }
    }
}
