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
        CAT_2015.User currentUser = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            currentUser = AppCode.Database.GetLoggedInUser(Session.SessionID);
            if (currentUser != null)
            {
                labelUser.Text = "User: " + currentUser.Username;
            }
        }
    }
}