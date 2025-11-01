using PruebaTecnica.BE.Domain.Common;
using PruebaTecnica.BE.Domain.Entities.Users;

namespace PruebaTecnica.BE.Domain.Entities.Tasks
{
    public class Task : BaseEntity
    {
        public int Id { get; set; }
        public string TaskCode { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public DateTime? DueDate { get; set; }
        public TaskPriority Priority { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
    public enum TaskPriority
    {
        Low = 1,
        Medium = 2,
        High = 3
    }
}
