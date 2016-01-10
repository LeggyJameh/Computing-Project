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
        List<Achievement> achievementsToBeAwarded = new List<Achievement>();
        protected void Page_Load(object sender, EventArgs e)
        {
            currentUser = AppCode.Database.GetLoggedInUser(Session.SessionID);
            if (currentUser != null)
            {
                achievementsToBeAwarded = AppCode.Database.GetUserAchievements(currentUser, false, false);

                if (achievementsToBeAwarded.Count > 0)
                {
                    foreach (Achievement a in achievementsToBeAwarded)
                    {
                        // TODO: Show message boxes for the achievements
                        // TODO: Include awards
                    }
                }
                else
                {
                    Console.WriteLine("No achievements to be awarded for user " + currentUser.ID);
                }

                userAchievements = AppCode.Database.GetUserAchievements(currentUser, false, true);
                if (userAchievements.Count > 0)
                {
                    foreach (Achievement a in userAchievements)
                    {
                        TableRow row = new TableRow();
                        row.Height = 100;

                        TableCell cell = new TableCell();

                        CAT_2015.Pages.User.Controls.AchievementElement achievementElement =
                        (CAT_2015.Pages.User.Controls.AchievementElement)Page.LoadControl
                        ("~/Pages/User/Controls/AchievementElement.ascx");

                        cell.Controls.Add(achievementElement);
                        row.Controls.Add(cell);
                        tableAchievements.Controls.Add(row);
                        achievementElement.SetAchievement(a, true, currentUser);
                    }

                    List<int> recentAchievements = AppCode.Database.GetRecentAchievementIDsForUser(currentUser);
                    if (recentAchievements.Count > 0)
                    {
                        foreach (Achievement a in userAchievements)
                        {
                            foreach (int id in recentAchievements)
                            {
                                if (a.ID == id)
                                {
                                    TableRow row = new TableRow();
                                    row.Height = 100;

                                    TableCell cell = new TableCell();

                                    CAT_2015.Pages.User.Controls.AchievementElement achievementElement =
                                    (CAT_2015.Pages.User.Controls.AchievementElement)Page.LoadControl
                                    ("~/Pages/User/Controls/AchievementElement.ascx");

                                    cell.Controls.Add(achievementElement);
                                    row.Controls.Add(cell);
                                    tableRecentAchievements.Controls.Add(row);
                                    achievementElement.SetAchievement(a, true, currentUser);
                                }
                            }
                        }
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