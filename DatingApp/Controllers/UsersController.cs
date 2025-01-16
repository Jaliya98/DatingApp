using AutoMapper;
using DatingApp.Data;
using DatingApp.DTO;
using DatingApp.Entities;
using DatingApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Controllers
{
    
    public class UsersController(IUserRepository userRepository, IMapper mapper) : BaseApiController
    {
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUser()
        {
            var users = await userRepository.GetUsersAsync();
            return Ok(users);
        }

        
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            var user = await userRepository.GetMemberAsync(username);
            if (user == null)
            {
                return NotFound();
            }
            
            return user ;
        }
    }
}
