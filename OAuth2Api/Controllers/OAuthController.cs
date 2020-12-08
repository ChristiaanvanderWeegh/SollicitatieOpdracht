using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using OAuth2Api.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OAuth2Api.Controllers
{
    public class OAuthController : Controller
    {
        /// <summary>
        /// Landing page for authorization
        /// </summary>
        /// <param name="code">Unique authorisation code</param>
        /// <param name="client_id">Unique client id</param>
        /// <param name="client_secret">Unique client side secret</param>
        /// <param name="redirect_uri">Redirect URI from the client</param>
        /// <param name="scope">The kind of wanted access</param>
        /// <param name="state">Random string that is used created by the client</param>
        /// <returns></returns>
        [HttpGet]

        public IActionResult Authorize(
            string code,
            string client_id,
            string client_secret,
            string redirect_uri,
            string scope,
            string state)
        {
            var model = new AuthorizeRequestModel()
            {
                code = code,
                client_id = client_id,
                client_secret = client_secret,
                redirect_uri = redirect_uri,
                scope = scope,
                state = state
            };

            return View(model);
        }

        /// <summary>
        /// Confirmation for authorize, redirect to actual authorization handling
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Authorize(AuthorizeRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var query = new QueryBuilder();
                //query.Add("username", model.username);
                //query.Add("code", model.code);
                //query.Add("clientid", model.client_id);
                //query.Add("clientsecret", model.client_secret);
                //query.Add("scope", model.scope);
                query.Add("code", "somerandomcode");
                query.Add("state", model.state);

                return Redirect($"{model.redirect_uri}{query}");
            }
            return View(model);
        }

        /// <summary>
        /// Create the access token for the user
        /// </summary>
        /// <param name="grant_type"></param>
        /// <param name="code"></param>
        /// <param name="redirect_uri"></param>
        /// <param name="client_id"></param>
        /// <param name="refresh_token"></param>
        /// <returns></returns>
        public async Task<IActionResult> Token(
            string grant_type,
            string code,
            string redirect_uri,
            string client_id,
            string refresh_token)
        {
            // mechanism for validating the code

            var claims = new[]
          {
                new Claim(JwtRegisteredClaimNames.Sub, "some_id"),
            };

            var secretBytes = Encoding.UTF8.GetBytes(Constants.Secret);
            var key = new SymmetricSecurityKey(secretBytes);
            var algorithm = SecurityAlgorithms.HmacSha256;

            var signingCredentials = new SigningCredentials(key, algorithm);

            var token = new JwtSecurityToken(
                Constants.Issuer,
                Constants.Audiance,
                claims,
                notBefore: DateTime.Now,
                expires: grant_type == "refresh_token"
                    ? DateTime.Now.AddMinutes(5)
                    : DateTime.Now.AddMilliseconds(1),
                signingCredentials);

            var access_token = new JwtSecurityTokenHandler().WriteToken(token);

            var responseObject = new
            {
                access_token,
                token_type = "Bearer",
                raw_claim = "oauthtoken",
                refresh_token = "randomrefreshtokenvalue"
            };

            var responseJson = JsonConvert.SerializeObject(responseObject);
            var responseBytes = Encoding.UTF8.GetBytes(responseJson);

            await Response.Body.WriteAsync(responseBytes, 0, responseBytes.Length);

            return Redirect(redirect_uri);
        }

        [Authorize]
        public IActionResult Validate()
        {
            if (HttpContext.Request.Query.TryGetValue("access_token", out var accessToken))
            {

                return Ok();
            }
            return BadRequest();
        }
    }

}
