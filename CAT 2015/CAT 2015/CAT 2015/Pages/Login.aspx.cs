using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CAT_2015.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        // The current session's associated user
        CAT_2015.User currentUser = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the current session's user and set currentUser to it
            currentUser = AppCode.Database.GetLoggedInUser(Session.SessionID);
            buttonLogin.Click += buttonLogin_Click;
        }

        void buttonLogin_Click(object sender, EventArgs e)
        {
            // If there is no user logged in...
            if (currentUser == null)
            {
                // Make sure there is something in the textboxes..
                if (textBoxUsername.Text.Length >= 1 && textBoxPassword.Text.Length >= 1)
                {
                    textBoxUsername.Text.Replace(@"\'", "");
                    // If the password is correct for the user (if the user exists)..
                    if (CAT_2015.AppCode.Database.AuthenticateUser(
                    textBoxUsername.Text.ToUpper(),
                    textBoxPassword.Text, Session.SessionID))
                    {
                        // Set the current user to that of the user that was logged in,
                        // and set that user's sessionID to this client's sessionID
                        currentUser = AppCode.Database.GetLoggedInUser(Session.SessionID);

                        // Then take the user to their appropriate home page
                        redirectHome();
                    }
                    else
                    {
                        labelError.Text = "Could not authenticate user. Please check your "
                        + "username and password and try again. The user could also no "
                        + "longer be avaialable.";
                    }
                }
                else
                {
                    labelError.Text = "Please enter in a valid username and password.";
                }
            }
            else
            {
                // Take the user to their appropriate home page
                redirectHome();
            }
        }

        /// <summary>
        /// Take the user to their appropriate home page
        /// </summary>
        private void redirectHome()
        {
            // If there is a current user logged in...
            if (currentUser != null)
            {
                // If the user is an admin...
                if (currentUser.PermissionLevel > 0)
                {
                    Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["AdminHome"]);
                }
                else
                {
                    Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["UserHome"]);
                }
            }
        }
    }
}