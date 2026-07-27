using DanaCopilot.Domain.Entities.BaseEntities;


namespace DanaCopilot.Domain.Entities
{
    public class UserComEntity : BaseEntity
    {
        public required string FullName { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Salt { get; set; }
        public required string NationalId { get; set; }
    }
}
