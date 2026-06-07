using DanaCopilot.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.AI.OCR
{
    public class TesseractOcrService: IOcrService
    {
        public async Task<string> ExtractTextAsync( string filePath)
        {
            return await Task.FromResult(File.ReadAllText(filePath));
        }
    }
}
