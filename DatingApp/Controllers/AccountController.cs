using DatingApp.Data;
using DatingApp.DTO;
using DatingApp.Entities;
using DatingApp.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DatingApp.Controllers
{
    public class AccountController(DataContext dataConext, ITokenService tokenService) : BaseApiController
    {
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if(await UserExists(registerDto.Username))
            {
                return BadRequest("Username is taken");
            }
            return Ok();
            //using var hmac = new HMACSHA512();

            //var user = new AppUser
            //{
            //    userName = registerDto.Username.ToLower(),
            //    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            //    PasswordSalt = hmac.Key
            //};
            //dataConext.Users.Add(user);
            //await dataConext.SaveChangesAsync();

            //return new UserDto
            //{
            //    userName = user.userName,
            //    Token = tokenService.CreateToken(user)
            //};
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Loging(LoginDto loginDto)
        {
            var user = await dataConext.Users.FirstOrDefaultAsync(x =>
            x.userName.ToLower() == loginDto.Username.ToLower());

            if(user == null)
            {
                return Unauthorized("Invalid username");

            }

            using var hamc = new HMACSHA512(user.PasswordSalt);
            var computedHash = hamc.ComputeHash(Encoding.UTF8.GetBytes(loginDto.password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return Unauthorized("Invalid password");
                }

            }
            return new UserDto
            {
                userName = user.userName,
                Token = tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExists(string username)
        {
            return await dataConext.Users.AnyAsync(x => x.userName.ToLower() == username.ToLower());
        }
    }
}
