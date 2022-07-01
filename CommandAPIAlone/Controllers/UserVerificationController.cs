using CommandAPIAlone.Data;
using CommandAPIAlone.Interfaces;
using CommandAPIAlone.Interfaces.Services;
using CommandAPIAlone.Models;
using CommandAPIAlone.Models.Requests.EmailService;
using CommandAPIAlone.Models.Requests.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace CommandAPIAlone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserVerificationController : ControllerBase
    {
        private readonly IUserVerificationRepository _repository;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public UserVerificationController(IUserVerificationRepository repository, IEmailService emailService, IConfiguration configuration)
        {
            _repository = repository;
            _emailService = emailService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRegisterRequest request)
        {

            var userInDb = await _repository.GetUserByEmailAsync(request.Email);

            if (userInDb != null) 
            {
                return BadRequest("E-mail address already used!");
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                VerificationToken = await _repository.CreateUniqueRandomTokenAsync(),
            };

            await _repository.CreateUserAsync(user);
            await _repository.SaveChangesAsync();

            EmailRequest emailTosend = new EmailRequest()
            {
                To = request.Email,
                Subject = "Sucsessful reigstration",
                Body = "You have registered successfully, please verify yourt e-mail at this link: \n" +
                "https://localhost:7267/api/UserVerification/verify?token=" + user.VerificationToken
            };

            try
            {
                _emailService.SendEmail(emailTosend); // elküldeni egy olyan url-t ami a frontendre vezet az pedig meghívhja az emailben lévő endpointot
                return Ok("User created");
            }
            catch 
            {
                return BadRequest("Error in e-mail sending");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLoginRequest request)
        {
            var userInDb = await _repository.GetUserByEmailAsync(request.Email);

            if (userInDb == null) 
            {
                return BadRequest("Incorrect login paramaters");
            }

            if (!VerifyPasswordHash(request.Password, userInDb.PasswordHash, userInDb.PasswordSalt))
            {
                return BadRequest("Incorrect login paramaters");
            }

            if (userInDb.VerifiedAt == null) 
            {
                return BadRequest("Email is not verified");
            }

            try
            {
                var jwt = CreateJwtToken(userInDb);
                return Ok("Bearer " + jwt);
            }
            catch 
            {
                return BadRequest();
            }
        }

        [HttpPost("verify")]
        public async Task<ActionResult> Verify(string token)
        {
            
            var userInDb = await _repository.GetUserByVerificationTokenAsync(token);

            if (userInDb == null)
            {
                return BadRequest();
            }

            userInDb.VerifiedAt = DateTime.Now.AddHours(-2); // nálam valamiért rosszul tárolja
            await _repository.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("forgot-password")]
        public async Task<ActionResult> ForgotPassword(string email)
        {
            var userInDb = await _repository.GetUserByEmailAsync(email);

            if (userInDb == null)
            {
                return BadRequest();
            }

            userInDb.PasswordResetToken = await _repository.CreateUniqueRandomTokenAsync();
            userInDb.ResetTokenExpires = DateTime.Now.AddHours(-2).AddDays(1);
            await _repository.SaveChangesAsync();

            EmailRequest emailTosend = new EmailRequest()
            {
                To = userInDb.Email,
                Subject = "Reset password",
                Body = "You may not reset your password at this link: +\n" +
                "https://localhost:7267/api/UserVerification/reset-password"
            };

            try
            {
                _emailService.SendEmail(emailTosend); // elküldeni egy olyan url-t ami a frontendre vezet az pedig meghívhja az emailben lévő endpointot
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword(UserResetPasswordRerquest rerquest)
        {
            var userInDb = await _repository.GetUserByPasswordResetTokenAsync(rerquest.Token);

            if (userInDb == null || userInDb.ResetTokenExpires < DateTime.Now.AddHours(-2))
            {
                return BadRequest();
            }

            CreatePasswordHash(rerquest.Password, out byte[] passwordHash, out byte[] passwordSalt);

            userInDb.PasswordHash = passwordHash;
            userInDb.PasswordSalt = passwordSalt;
            userInDb.PasswordResetToken = null;
            userInDb.ResetTokenExpires = null;

            await _repository.SaveChangesAsync();

            return Ok();
        }

        private string CreateJwtToken(User user) 
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
