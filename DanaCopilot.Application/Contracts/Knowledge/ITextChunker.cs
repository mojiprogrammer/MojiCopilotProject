using DanaCopilot.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.Contracts.Knowledge
{
    public interface ITextChunker
    {
        List<ChunkModel> Split(string text);
    }
}
