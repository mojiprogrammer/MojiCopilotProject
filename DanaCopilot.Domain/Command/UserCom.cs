using DanaCopilot.Domain.Entities.BaseEntities;

namespace DanaCopilot.Domain.Command
{
    public class UserCom: BaseEntity
    {
        public required string FullName { get; set; }
        public required string NationalCode { get; set; }
 
    }
}
