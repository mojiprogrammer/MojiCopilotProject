using Moji.Services.Interfaces;

namespace Moji.Controllers
{
    public class ModelTrainingBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ModelTrainingBackgroundService> _logger;

        public ModelTrainingBackgroundService(IServiceProvider serviceProvider, ILogger<ModelTrainingBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Train models every day at midnight
                    var now = DateTime.Now;
                    var nextRun = now.Date.AddDays(1);
                    var delay = nextRun - now;

                    await Task.Delay(delay, stoppingToken);

                    using var scope = _serviceProvider.CreateScope();
                    var predictionService = scope.ServiceProvider.GetRequiredService<IPredictionService>();

                    _logger.LogInformation("Starting automatic model training...");
                    var success = await predictionService.TrainModelsAsync();

                    if (success)
                        _logger.LogInformation("Model training completed successfully");
                    else
                        _logger.LogWarning("Model training failed");
                }
                catch (TaskCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in background model training");
                    await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
                }
            }
        }
    }
}
