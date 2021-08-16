using System.Threading.Tasks;
using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace VacationTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationContext _applicationContext;

        public UsersController(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserAsync(int userId)
        {
            var user = await _applicationContext.Employees.
                SingleOrDefaultAsync(it => it.Id == userId);
            if (user == null)
            {
                return NotFound(userId);
            }
            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetUsersAsync()
        {
            var users = _applicationContext.Employees;            
            return Ok(users);
        }
    }
}
