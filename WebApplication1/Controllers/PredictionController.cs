using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moji.Controllers.Models;
using Moji.Services.Interfaces;
using Moji.Services.Models;

namespace Moji.Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PredictionController : ControllerBase
    {
        private readonly IPredictionService _predictionService;
        private readonly ILogger<PredictionController> _logger;
        public PredictionController(IPredictionService predictionService, ILogger<PredictionController> logger)
        {
            _predictionService = predictionService;
            _logger = logger;
        }

        [HttpGet("GoldPredict")]
        public async Task<IActionResult> PredictGoldPrice([FromQuery] int days = 7)
        {
            if (days < 1 || days > 30)
                return BadRequest("Days must be between 1 and 30");

            var result = await _predictionService.PredictGoldPriceAsync(days);

            if (!result.Success)
                return StatusCode(500, result);

            return Ok(new
            {
                result.Success,
                result.CurrentPrice,
                result.PredictedPrices,
                result.PredictionDates,
                Trend = result.GetTrend(),
                ExpectedChangePercentage = result.GetExpectedChangePercentage(),
                result.ModelVersion,
                Recommendations = GenerateRecommendations(result)
            });
        }

        [HttpGet("Currency/{code}/Predict")]
        public async Task<IActionResult> PredictCurrencyPrice(string code, [FromQuery] int days = 7)
        {
            if (string.IsNullOrWhiteSpace(code))
                return BadRequest("Currency code is required");

            if (days < 1 || days > 30)
                return BadRequest("Days must be between 1 and 30");

            var result = await _predictionService.PredictCurrencyPriceAsync(code.ToUpper(), days);

            if (!result.Success)
                return StatusCode(500, result);

            return Ok(new
            {
                result.Success,
                result.CurrentPrice,
                result.PredictedPrices,
                result.PredictionDates,
                Trend = result.GetTrend(),
                ExpectedChangePercentage = result.GetExpectedChangePercentage(),
                CurrencyCode = code.ToUpper(),
                result.ModelVersion
            });
        }

        [HttpPost("Train")]
        public async Task<IActionResult> TrainModels()
        {
            var success = await _predictionService.TrainModelsAsync();

            if (success)
                return Ok(new { Message = "Models trained successfully" });

            return StatusCode(500, new { Message = "Model training failed" });
        }

        [HttpGet("Accuracy/{assetType}")]
        public async Task<IActionResult> GetModelAccuracy(string assetType)
        {
            var accuracy = await _predictionService.GetModelAccuracyAsync(assetType);
            return Ok(accuracy);
        }

        [HttpGet("Compare")]
        public async Task<IActionResult> ComparePredictions(
            [FromQuery] string assetType,
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            var comparison = await _predictionService.ComparePredictionsWithActualsAsync(assetType, startDate, endDate);
            return Ok(comparison);
        }

        [HttpPost("UpdateActual")]
        public async Task<IActionResult> UpdateActualPrice([FromBody] UpdateActualPriceRequest request)
        {
            // Implementation for updating actual prices after they become available
            // This would trigger accuracy recalculation
            return Ok(new { Message = "Actual price recorded" });
        }

        private object GenerateRecommendations(PricePredictionResult prediction)
        {
            var trend = prediction.GetTrend();
            var changePercent = prediction.GetExpectedChangePercentage();

            var recommendations = new List<string>();

            if (trend == "UP" && changePercent > 5)
            {
                recommendations.Add("Expected significant price increase - consider buying");
                recommendations.Add("Set stop-loss orders to protect against volatility");
            }
            else if (trend == "DOWN" && changePercent < -5)
            {
                recommendations.Add("Expected significant price decrease - consider selling or holding");
                recommendations.Add("Watch for support levels");
            }
            else if (trend == "STABLE")
            {
                recommendations.Add("Market showing stability - maintain current position");
            }

            recommendations.Add($"Confidence interval range: ±{prediction.ConfidenceIntervals.FirstOrDefault():F2}");

            return new
            {
                Trend = trend,
                ExpectedChange = changePercent,
                Recommendations = recommendations,
                Confidence = prediction.ConfidenceIntervals.Length > 0 ? "Medium" : "Low"
            };
        }
    }
}
