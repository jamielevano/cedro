using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Security;

namespace Web
{
    public static class Auth
    {
        private const int AuthenticationTicketVersion = 2;

        public static void SetAuthCookie(string user, AuthTicketData data, bool createPersistentCookie = false)
        {
            var now = DateTime.Now;

            var ticket = new FormsAuthenticationTicket(
                AuthenticationTicketVersion,
                user,
                now,
                now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes),
                createPersistentCookie,
                JsonConvert.SerializeObject(data));

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket))
            {
                HttpOnly = true,
                Secure = FormsAuthentication.RequireSSL,
                Path = FormsAuthentication.FormsCookiePath,
                Domain = FormsAuthentication.CookieDomain,
            };

            HttpContext.Current.Response.SetCookie(cookie);
        }

        public static AuthTicketData GetTicketData()
        {
            return JsonConvert.DeserializeObject<AuthTicketData>(((FormsIdentity)HttpContext.Current.User.Identity).Ticket.UserData);
        }

        
    }

    public class AuthTicketData
    {

        public bool NotAdmin { get { return !Admin; } }

        public bool Admin { get; set; }
        public bool Coordinador { get; set; }

        public int TelecentroId { get; set; }

        public int EjeId { get; set; }
    }
}