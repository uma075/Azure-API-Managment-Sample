using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ColorAPI.Handlers
{
    public class AuthHandler: DelegatingHandler
    {
        public override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Authorization == null)
            {
                 return request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized");                
            }
            else
            {
                string token= request.Headers.GetValues("Authorization").FirstOrDefault();
                if(token!=null)
                {
                    byte[] data = Convert.FromBase64String(token);
                    string decodedString = Encoding.UTF8.GetString(data);
                    string[] tokensValues = decodedString.Split(':');

                    UserMaser ObjUser = new CredentialChecker().CheckCredential(tokensValues[0], tokensValues[1]);
                    if (ObjUser != null)
                    {
                        IPrincipal principal = new GenericPrincipal(new GenericIdentity(ObjUser.name), ObjUser.UserRole.Split(','));
                        Thread.CurrentPrincipal = principal;
                        HttpContext.Current.User = principal;
                    }
                    else
                    {
                        //The user is unauthorize and return 401 status  
                        var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                        var tsc = new TaskCompletionSource<HttpResponseMessage>();
                        tsc.SetResult(response);
                        return tsc.Task;
                    }
                }
            }
        }
    }
}