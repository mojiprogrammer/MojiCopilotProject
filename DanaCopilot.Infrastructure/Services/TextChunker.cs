using DanaCopilot.Application.Contracts.Knowledge;
using DanaCopilot.Application.Models;

namespace DanaCopilot.Infrastructure.Services
{
    public class TextChunker : ITextChunker
    {
        private const int ChunkSize = 1000;

        public List<ChunkModel> Split(
            string text)
        {
            var result = new List<ChunkModel>();

            var index = 0;

            for (var i = 0;
                 i < text.Length;
                 i += ChunkSize)
            {
                var length =
                    Math.Min(
                        ChunkSize,
                        text.Length - i);

                result.Add(
                    new ChunkModel
                    {
                        Index = index++,
                        Content = text.Substring(i, length),
                        TokenCount = length
                    });
            }

            return result;
        }
    }
}
