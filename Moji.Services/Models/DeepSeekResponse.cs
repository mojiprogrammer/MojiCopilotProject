using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.Services.Models
{
    public class DeepSeekResponse
    {
        public List<DeepSeekChoice> Choices { get; set; } = new();
        public DeepSeekUsage? Usage { get; set; }
    }
}
