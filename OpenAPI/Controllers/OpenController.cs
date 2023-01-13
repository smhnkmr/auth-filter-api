using OpenAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace OpenAPI.Controllers
{
    
    [RoutePrefix("api/open")]
    public class OpenController : ApiController
    {
        [BasicAuthentication]
        [Route("authorize")]
        [HttpGet]
        public HttpResponseMessage Authorize()
        {
            var token = TokenManager.GenerateToken(getUserName());

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, token);
        }


        [AuthFilter]
        [Route("testaction")]
        [HttpGet]
        public HttpResponseMessage TestAction()
        {
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "Success: Authentication and API call limit Implementation successful");
        }

        private string getUserName()
        {
            var authenticationToken = Request.Headers.Authorization.Parameter;
            var decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
            var usernamePasswordArray = decodedAuthenticationToken.Split(':');
            var username = usernamePasswordArray[0];

            return username;
        }
    }
}