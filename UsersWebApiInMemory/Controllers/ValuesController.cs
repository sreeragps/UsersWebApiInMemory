using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersWebApiInMemory.Model;

namespace UsersWebApiInMemory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserDbContext _context;

        public UsersController(UserDbContext context)
        {
            _context = context;

        }
        [HttpPost]
        public IActionResult AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return Created("api/users/" + user.Name, user);
        }
        [HttpGet]
        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var usr = _context.Users.SingleOrDefault(x => x.Id == id);
            if(usr == null)
            {
                return NotFound("User does not exist");
            }
            _context.Users.Remove(usr);
            _context.SaveChanges();
            return Ok("User is deleted successfully");
        }
    }
}
