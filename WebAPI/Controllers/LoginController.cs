using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TestTaskLib.Models.DataDb;
using WebAPI.Models;
using WebAPI.Models.Requests;
using WebAPI.Models.Responses;

namespace WebAPI.Controllers
{
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly AuthOptions _authOptions;

        public LoginController(ApplicationContext context, AuthOptions authOptions)
        {
            _context = context;
            _authOptions = authOptions;
        }

        /// <summary>
        /// Get JWT Access token by username and password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="401">Unauthorized</response>
        [HttpPost("api/[Controller]/getToken")]
        public ActionResult<ApiResponse<TokenResponse>> GetToken(TokenRequest request)
        {
            var response = new ApiResponse<TokenResponse>();

            try
            {
                var identity = GetIdentity(request.UserName, request.Password);
                if (identity == null)
                {
                    response.Status.Code = 1;
                    response.Status.Message = "Wrong username or password";
                    return response;
                }
                
                var now = DateTime.UtcNow;
                var expires = now.Add(TimeSpan.FromMinutes(_authOptions.Lifetime));
                
                var jwt = new JwtSecurityToken(
                    issuer: _authOptions.Issuer,
                    audience: _authOptions.Audience,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: expires,
                    signingCredentials: new SigningCredentials(_authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                response.Data = new TokenResponse()
                {
                    Token = encodedJwt,
                    UserName = identity.Name,
                    Expires = jwt.ValidTo
                };
                
            }
            catch(Exception e)
            {
                response.Status.Code = 1;
                response.Status.Message = e.Message;
            }
            return response;
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            var user = _context.AppUsers.FirstOrDefault(x => x.UserName == username && x.Password == password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }
    }
}
