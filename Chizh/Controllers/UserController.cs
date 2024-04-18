using Chizh.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chizh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private User24Context _context;

        public UserController(User24Context context)
        {
            _context = context;
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<List<UserDTO>>> GetUsers()
        {
            List<UserDTO> users = _context.Users.ToList().Select(s => new UserDTO
            {
                Id = s.Id,
                Name = s.Name,
                Password = s.Password,
                Weight = s.Weight,
                Height = s.Height
            }).ToList();
            return users;
        }

        [HttpGet("GetUser")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var s = _context.Users.FirstOrDefault(s => s.Id == id);
            if (s == null)
            {
                return NotFound();

            }
            return Ok(new UserDTO
            {
                Id = s.Id,
                Name = s.Name,
                Password = s.Password,
                Weight = s.Weight,
                Height = s.Height
            });
        }

        [HttpPost("UserLogin")]

        public ActionResult<UserDTO> UserLogin(UserDTO userDTO)
        {

            User user = _context.Users.FirstOrDefault(a => a.Name == userDTO.Name && a.Password == userDTO.Password);
            if (user != null)
            {
                return new UserDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Password = user.Password,
                    Weight = user.Weight,
                    Height = user.Height
                };
            }
            else
            {
                return BadRequest("нЕПРАВИЛЬНЫЙ лОгин или пароль");
            }

        }
    }
}
