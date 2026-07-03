namespace DanaCopilot.Application.Modules.RunTime.Interfaces
{
  
    public interface IOEECalculationService
    {
        decimal CalculateAvailability(int runTime, int plannedTime);

        decimal CalculatePerformance(decimal idealCycleTime, decimal actualCycleTime, decimal totalCount);

        decimal CalculateQuality(decimal goodQty, decimal totalQty);

        decimal CalculateOEE(decimal availability, decimal performance, decimal quality);
    }
}
