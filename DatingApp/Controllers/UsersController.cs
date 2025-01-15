using DatingApp.Data;
using DatingApp.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Controllers
{
    
    public class UsersController(DataContext dataContext) : BaseApiController
    {

        [HttpGet]
        public ActionResult<IEnumerable<AppUser>> GetUser()
        {
            var users = dataContext.Users.ToList();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public ActionResult<AppUser> GetUser(int id)
        {
            var user = dataContext.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
