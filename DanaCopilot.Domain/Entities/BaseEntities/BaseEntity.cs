using System.ComponentModel.DataAnnotations;

namespace DanaCopilot.Domain.Entities.BaseEntities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            this.CreateDate = DateTime.UtcNow;
        }
        [Key]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
