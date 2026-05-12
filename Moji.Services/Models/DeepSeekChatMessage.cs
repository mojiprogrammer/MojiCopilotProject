using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.Services.Models
{
    public class DeepSeekChatMessage
    {
        public string Role { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
