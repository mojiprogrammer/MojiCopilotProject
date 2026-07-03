namespace DanaCopilot.Application.Modules.Oee.Models
{
    public sealed class OeeResult
    {
        public decimal Availability { get; set; }

        public decimal Performance { get; set; }

        public decimal Quality { get; set; }

        public decimal Oee => Availability * Performance * Quality;
    }
}
