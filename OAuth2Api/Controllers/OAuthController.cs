using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using OAuth2Api.Models;

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
        public IActionResult Authorize([FromBody] AuthorizeRequestModel model)
        {
            var query = new QueryBuilder();
            query.Add("username", model.username);
            query.Add("code", model.code);
            query.Add("clientid", model.client_id);
            query.Add("clientsecret", model.client_secret);
            query.Add("scope", model.scope);
            query.Add("state", model.state);

            return Redirect($"{model.redirect_uri}{query}");
        }
    }
}
