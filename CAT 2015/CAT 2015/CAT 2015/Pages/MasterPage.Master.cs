using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CAT_2015.Pages
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        CAT_2015.User currentUser = null; // The current session's associated user
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the current session's user and set currentUser to it
            currentUser = AppCode.Database.GetLoggedInUser(Session.SessionID);
            if (currentUser != null) // If a user is found...
            {
                // Show the user in the corner of the page
                labelUser.Text = "User: " + currentUser.Username;
            }
            // If no user is found, the label will display nothing
        }
    }
}