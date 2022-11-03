using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebAPI.Handlers;
using WebAPI.Modes;
using WebAPI.Repository.Data;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountRepository _repository;
        public AccountController(AccountRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("Login")]
        public ActionResult Login(string email, string password)
        {
            try
            {
                var data = _repository.GetDataLogin(email, password);
                if (data == null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Gagal login, password atau email tidak valid"
                    }); ;
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Berhasil login",
                        Data = data
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

        [HttpPost("Register")]
        public ActionResult Register(string fullName, string email, string password, DateTime birthDate)
        {
            try
            {
                var IsEmailAvailable = _repository.CheckEmail(email);

                if (IsEmailAvailable)
                {
                    var resultEmployee = _repository.CreateEmployee(fullName, email, birthDate);
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
