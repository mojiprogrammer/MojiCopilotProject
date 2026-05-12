using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.Services.Models
{
    public class DeepSeekRequest
    {
        public string Model { get; set; } = "deepseek-chat";
        public List<DeepSeekChatMessage> Messages { get; set; } = new();
        public double Temperature { get; set; } = 0.7;
        public int MaxTokens { get; set; } = 2000;
        public bool Stream { get; set; } = false;
    }
}
