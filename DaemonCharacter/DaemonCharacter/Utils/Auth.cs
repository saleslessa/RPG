using System;
using System.Web.Mvc;

namespace DaemonCharacter.Utils
{
    public class Auth : Controller
    {

        public Auth() : base()  {
           
        }
        public string GetLoggedUser()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                    return User.Identity.Name;

                throw new Exception("User not logged");
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

    }
}