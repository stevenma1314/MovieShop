using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Services;
using MovieShop.API.DTO;
using MovieShop.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        public UserController(IUserService userService,IConfiguration configuration)
        {
            _userService = userService;
            _config = configuration;
        }

        [HttpPost]
        public async Task<ActionResult> CreateUserAsync([FromBody]CreateUserDTO createUserDTO)
        {
            if (createUserDTO == null || createUserDTO.email == null || createUserDTO.password == null || createUserDTO.firstName == null || createUserDTO.lastName == null)
            {
                return BadRequest("Bad request");
            }

            var user = await _userService.CreateUser(createUserDTO.email, createUserDTO.password, createUserDTO.firstName, createUserDTO.lastName);
            if (user == null)
            {
                return BadRequest("emial already exist");
            }
            return Ok("Cool");
        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> VaildateUser([FromBody]ValidateUserDTO validateUserDTO)
        {

            var user = await _userService.VaildateUser(validateUserDTO.email, validateUserDTO.password);
            if (user == null)
            {
                return BadRequest("not exsit");
            }
            else
            {
                return Ok(new { 
                token = GenrateToken(user)
                });
            }
        }
        [Authorize]
        [HttpGet]
        [Route("{id}/purchases")]
        public async Task<ActionResult> GetUserPurchasedMovies(int id)
        {
            var userMovies = await _userService.GetPurchases(id);
            return Ok(userMovies);
        }

        private string GenrateToken(User user)
        {
            //create claim information 
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim("alias", user.FirstName[0] + user.LastName[0].ToString()),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName)
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenSettings:PrivateKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["TokenSettings:ExpirationDays"]));

            var tokenDescriptor = new SecurityTokenDescriptor     
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                SigningCredentials = credentials,
                Issuer = _config["TokenSettings:Issuer"],
                Audience = _config["TokenSettings:Audience"]
            };
           

            var encodedJwt = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
            return new JwtSecurityTokenHandler().WriteToken(encodedJwt);
        }

    }
}