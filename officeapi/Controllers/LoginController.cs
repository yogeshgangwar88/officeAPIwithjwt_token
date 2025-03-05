using AutoMapper;
using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using officeapi.Services;
using ServiceLibrary.Interfaces;
using ServiceLibrary.Models;
using ServiceLibrary.Models.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace officeapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogin _loginservice;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public LoginController(ILogin login,IMapper mapper, IConfiguration config)
        {
            this._loginservice = login;
            this._mapper = mapper;
            this._config = config;
        }
        

        // POST api/<LoginController>
        [HttpPost]
        [Route("Loginuser")]
        public async Task< IActionResult> Loginuser([FromBody] LoginDTO model)
        {
            customResponse<Login> cr=new customResponse<Login>();
            try
            {
                var modelx = this._mapper.Map<Login>(model);
              
                var res = await _loginservice.loginuser(modelx);
                var  tokenvalue = "";
                if (res!=null && res.token=="true" )
                {
                    var claims = new[]
                    {
                        new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub,_config["Jwt:Subject"].ToString()),
                        new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        new Claim("Userid",model.username),
                        new Claim("password",model.password),

                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                    var signin=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
                    var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                        _config["Jwt:Issuer"],
                        _config["Jwt:Audience"],
                        claims,
                        expires:DateTime.UtcNow.AddMinutes(60),
                        signingCredentials:signin
                        );
                     tokenvalue=new JwtSecurityTokenHandler().WriteToken(token);
                }
                return Ok(new { tokenvalue= tokenvalue, res = res });
            }
            catch (Exception e)
            {
               // this._loggerservice.Log(LogLevel.Error,e.StackTrace);
                return BadRequest();
                //throw;
            }
        }
        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            customResponse<string> cr = new customResponse<string>();
            cr.statuscode = 200;
            await Task.Delay(100);
            cr.data = new List<string> { "Hello", "Test", "Data" };
            return Ok(cr);
        }
    }
}
