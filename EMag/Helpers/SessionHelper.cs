using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMag.Helpers
{
    public class SessionHelper
    {
        public int UserID { get; private set; }
        public string Username { get; private set; }
        public bool IsAuthenticated { get; private set; }
        public bool IsAdministrator { get; private set; }

        private SessionHelper()
        {

        }

        public static SessionHelper Current
        {
            get
            {
                SessionHelper loginUserSession = (SessionHelper)HttpContext.Current.Session["LoginUser"];
                if (loginUserSession == null)
                {
                    loginUserSession = new SessionHelper();
                    HttpContext.Current.Session["LoginUser"] = loginUserSession;
                }
                return loginUserSession;
            }
        }

        public void SetCurrentUser(int userID, string username, bool isAdministrator)
        {
            this.IsAuthenticated = true;
            this.IsAdministrator = isAdministrator;
            this.UserID = userID;
            this.Username = username;
        }

        public void Logout()
        {
            this.IsAuthenticated = false;
            this.IsAdministrator = false;
            this.UserID = 0;
            this.Username = string.Empty;
        }
    }
}