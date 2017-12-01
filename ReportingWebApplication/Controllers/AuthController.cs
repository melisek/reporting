﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using szakdoga.Models;
using szakdoga.Models.Dtos;
using szakdoga.Models.RepositoryInterfaces;
using szakdoga.Others;

namespace szakdoga.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserJwtMapRepository _userJwtMapRepository;
        private readonly IConfigurationRoot _cfg;
        private readonly ILogger<AuthController> _logger;
        private readonly int expiryMinutes = 10;

        public AuthController(IUserRepository userRepository, IUserJwtMapRepository userJwtMapRepository, IConfigurationRoot cfg, ILogger<AuthController> logger)
        {
            _userRepository = userRepository;
            _userJwtMapRepository = userJwtMapRepository;
            _cfg = cfg;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody]CredentialDto credDto)
        {
            try
            {
                if (credDto == null) throw new BasicException("Wrong data syntax.");
                if (!ModelState.IsValid) BadRequest(ModelState);

                User user = _userRepository.GetAll().FirstOrDefault(x => x.EmailAddress.Equals(credDto.EmailAddress));
                if (user == null || !user.Password.Equals(CalculateHash(credDto.Password)))
                {
                    return Unauthorized();
                }

                var token = new JwtTokenBuilder()
                                    .AddSecurityKey(JwtSecurityKey.Create(_cfg["Tokens:Key"]))
                                    .AddSubject(user.UserGUID)
                                    .AddIssuer(_cfg["Tokens:Issuer"])
                                    .AddAudience(_cfg["Tokens:Audience"])
                                    .AddClaim("EmailAddress", user.EmailAddress)
                                    .AddExpiry(expiryMinutes)
                                    .Build();

                //clean expried json
                DateTime curruntTime = DateTime.Now;
                _userJwtMapRepository.RemoveRecordBefore(curruntTime);

                //store jwt and userid in userjwtmap
                _userJwtMapRepository.AddUserJwtMapRecord(token.Value, user, curruntTime.AddMinutes(expiryMinutes));

                return Ok(Json(new
                {
                    message = "success",
                    jwt = token.Value
                }));
            }
            catch (BasicException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            catch (PermissionException ex)
            {
                _logger.LogError(ex.Message);
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]RegisterDto register)
        {
            try
            {
                if (register == null) throw new BasicException("Wrong data syntax.");
                if (!ModelState.IsValid) BadRequest(ModelState);

                var user = _userRepository.GetAll().FirstOrDefault(x => x.EmailAddress.Equals(register.EmailAddress));
                if (user != null) throw new BasicException("Emailaddress is reserved.");

                _userRepository.Add(
                    new User
                    {
                        Name = register.Name,
                        UserGUID = CreateGUID.GetGUID(),
                        EmailAddress = register.EmailAddress,
                        Password = CalculateHash(register.Password)
                    });

                return Login(new CredentialDto { EmailAddress = register.EmailAddress, Password = register.Password });
            }
            catch (BasicException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            catch (PermissionException ex)
            {
                _logger.LogError(ex.Message);
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [Authorize]
        [HttpGet("GetUserName")]
        public IActionResult GetUserName()
        {
            try
            {
                User user = _userRepository.GetByEmailAdd(this.User.Claims.SingleOrDefault(x => x.Type == "EmailAddress").Value);
                return Ok(user.Name);
            }
            catch (BasicException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            catch (PermissionException ex)
            {
                _logger.LogError(ex.Message);
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        private string CalculateHash(string clearTextPassword)
        {
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(clearTextPassword);
            byte[] hash = new SHA256Managed().ComputeHash(saltedHashBytes);
            return Convert.ToBase64String(hash);
        }
    }
}