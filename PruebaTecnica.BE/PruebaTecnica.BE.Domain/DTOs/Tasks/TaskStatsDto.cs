using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.BE.Domain.DTOs.Tasks
{
    public class TaskStatsDto
    {
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int PendingTasks { get; set; }
        public decimal CompletionPercentage { get; set; }
        public List<TaskDto> RecentTasks { get; set; } = new();
    }
}
