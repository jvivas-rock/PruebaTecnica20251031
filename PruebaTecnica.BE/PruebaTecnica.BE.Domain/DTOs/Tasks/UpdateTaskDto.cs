using PruebaTecnica.BE.Domain.Entities.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.BE.Domain.DTOs.Tasks
{
    public class UpdateTaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public DateTime? DueDate { get; set; }
        public TaskPriority Priority { get; set; }
    }
}
