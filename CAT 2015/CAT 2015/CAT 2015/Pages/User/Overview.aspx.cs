using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CAT_2015.Pages.User
{
    public partial class Overview : System.Web.UI.Page
    {
        CAT_2015.User currentUser = null;
        List<Achievement> userAchievements = new List<Achievement>();
        protected void Page_Load(object sender, EventArgs e)
        {
            currentUser = AppCode.Database.GetLoggedInUser(Session.SessionID);
            if (currentUser != null)
            {
                userAchievements = AppCode.Database.GetUserAchievements(currentUser, false);
                if (userAchievements.Count > 0)
                {
                    foreach (Achievement a in userAchievements)
                    {
                        if (a.
                    }
                }
            }
            else
            {
                Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["DefaultHome"]);
            }
        }
    }
}