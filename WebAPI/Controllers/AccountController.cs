using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Handlers;
using WebAPI.Modes;
using WebAPI.Repository.Data;
using WebAPI.ViewModel;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AccountController : ControllerBase
    {
        private readonly AccountRepository _repository;
        public IConfiguration _configuration;
        public AccountController(AccountRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public ActionResult Login(LoginVM login)
        {
            try
            {
                var data = _repository.GetDataLogin(login.Email, login.Password);
                if (data == null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Gagal login, password atau email tidak valid"
                    });
                }
                else
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("userId", data.Id.ToString()),
                        new Claim("fullName", data.FullName),
                        new Claim("role", data.RoleName),
                        new Claim("email", data.Email)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    string tokenCode = new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Berhasil login",
                        Token = tokenCode
                    });

                    //return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
            }

            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public ActionResult Register(string fullName, string email, string password, DateTime birthDate, string gender, int salary, string city)
        {
            try
            {
                var IsEmailAvailable = _repository.CheckEmail(email);

                if (IsEmailAvailable)
                {
                    var resultEmployee = _repository.CreateEmployee(fullName, email, birthDate, gender, salary, city);
                    if (resultEmployee > 0)
                    {
                        var resultUser = _repository.CreateUser(email, password);
                        if (resultUser == 0)
                        {
                            return Ok(new
                            {
                                StatusCode = 200,
                                Message = "Gagal memasukkan data"
                            }); ;
                        }
                        else
                        {
                            return Ok(new
                            {
                                StatusCode = 200,
                                Message = "Berhasil registasi"
                            });
                        }
                    }
                }
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Email telah digunakan"
                }); ;
            }

            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }

        [Authorize(Roles = "Staff")]
        [HttpPut("ChangePassword")]
        public ActionResult ChangePassword(string email, string oldPassword, string newPassword)
        {
            try
            {
                var data = _repository.GetDataLogin(email, oldPassword);
                if (data == null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Email atau password lama tidak valid",
                    });
                }
                else
                {
                    var result = _repository.UpdatePasswordUser(email, oldPassword, newPassword);
                    if (result > 0)
                    {
                        return Ok(new
                        {
                            StatusCode = 200,
                            Message = "Berhasil mengganti password"
                        });
                    }
                    else
                    {
                        return Ok(new
                        {
                            StatusCode = 200,
                            Message = "Gagal mengganti password"
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message,
                });
            }
        }

        [Authorize(Roles = "Staff")]
        [HttpPut("ResetPassword")]
        public ActionResult ResetPassword(string fullName, string email, string newPassword, DateTime birthDate)
        {
            try
            {
                var user = _repository.FindUserFromEmployee(fullName, email, birthDate);

                if (user == null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data yang dimasukkan tidak valid"
                    });
                }

                else
                {
                    var result = _repository.ResetPassword(user, newPassword);
                    if (result > 0)
                    {
                        return Ok(new
                        {
                            StatusCode = 200,
                            Message = "Berhasil reset password"
                        });
                    }

                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Gagal mengganti password"
                    });
                }
            }
            
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }
    }
}
