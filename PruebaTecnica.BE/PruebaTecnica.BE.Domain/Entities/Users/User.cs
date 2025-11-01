using PruebaTecnica.BE.Domain.Common;
using TaskItem = PruebaTecnica.BE.Domain.Entities.Tasks.Task;

namespace PruebaTecnica.BE.Domain.Entities.Users
{
    public class User : BaseEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();

    }
}
