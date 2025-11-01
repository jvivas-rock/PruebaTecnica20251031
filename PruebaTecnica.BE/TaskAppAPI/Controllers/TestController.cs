using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.BE.Infrastructure.Database;

namespace TaskAppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly DBContext _context;

        public TestController(DBContext context)
        {
            _context = context;
        }
        [HttpGet("database")]
        public async Task<IActionResult> TestDatabaseConnection()
        {
            try
            {
                var canConnect = await _context.Database.CanConnectAsync();
                var usersCount = await _context.Users.CountAsync();
                var tasksCount = await _context.Tasks.CountAsync();

                return Ok(new
                {
                    Message = "Conexión exitosa a la base de datos",
                    Database = _context.Database.GetDbConnection().Database,
                    Server = _context.Database.GetDbConnection().DataSource,
                    CanConnect = canConnect,
                    UsersCount = usersCount,
                    TasksCount = tasksCount
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Error conectando a la base de datos",
                    Error = ex.Message,
                    ConnectionString = _context.Database.GetDbConnection().ConnectionString
                });
            }
        }
    }
}
