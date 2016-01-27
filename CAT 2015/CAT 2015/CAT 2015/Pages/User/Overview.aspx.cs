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
        CAT_2015.User currentUser = null; // The current session's associated user
        // The achievements that the user has achieved
        List<Achievement> userAchievements = new List<Achievement>();
        // The achievements that the user has achieved and need to be awarded
        List<Achievement> achievementsToBeAwarded = new List<Achievement>();
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the current session's user and set currentUser to it
            currentUser = AppCode.Database.GetLoggedInUser(Session.SessionID);
            if (currentUser != null) // If a user was found...
            {
                // Get the achievements that need to be awarded
                achievementsToBeAwarded = AppCode.Database.GetUserAchievements(currentUser, false, false);

                if (achievementsToBeAwarded.Count > 0) // If any were found...
                {
                    foreach (Achievement a in achievementsToBeAwarded) // Award them
                    {
                        // TODO: Show message boxes for the achievements
                        // TODO: Include awards
                        // TODO: Add the achievements here to user achievements
                    }
                }
                else
                {
                    // Otherwise, print to the console that no achievements
                    // were found for this user to be awarded
                    Console.WriteLine("No achievements to be awarded for user " + currentUser.ID);
                }

                // Now get the achievements that the user has already had awarded.
                userAchievements = AppCode.Database.GetUserAchievements(currentUser, false, true);
                if (userAchievements.Count > 0) // If there are any...
                {
                    foreach (Achievement a in userAchievements) // Take each one and...
                    {
                        TableRow row = new TableRow(); // Create a row
                        row.Height = 100;

                        TableCell cell = new TableCell(); // Create a cell

                        // Create an achievement element
                        CAT_2015.Pages.User.Controls.AchievementElement achievementElement =
                        (CAT_2015.Pages.User.Controls.AchievementElement)Page.LoadControl
                        ("~/Pages/User/Controls/AchievementElement.ascx");

                        // Put the achievement element in the cell, the cell in the
                        // row and the row in the achievement table
                        cell.Controls.Add(achievementElement);
                        row.Controls.Add(cell);
                        tableAchievements.Controls.Add(row);

                        // Then set the achievement element to display the current achievement
                        achievementElement.SetAchievement(a, true, false, currentUser);
                    }

                    // Finally, get the most recent achievements' IDs
                    List<int> recentAchievements = AppCode.Database.GetRecentAchievementIDsForUser(currentUser);
                    if (recentAchievements.Count > 0) // If any are found...
                    {
                        // Match them up to the achievements in userAchievements
                        foreach (Achievement a in userAchievements)
                        {
                            foreach (int id in recentAchievements)
                            {
                                if (a.ID == id)
                                {
                                    // And create a row, cell and achievement element as before
                                    TableRow row = new TableRow();
                                    row.Height = 100;

                                    TableCell cell = new TableCell();

                                    CAT_2015.Pages.User.Controls.AchievementElement achievementElement =
                                    (CAT_2015.Pages.User.Controls.AchievementElement)Page.LoadControl
                                    ("~/Pages/User/Controls/AchievementElement.ascx");

                                    // Put them in eachother as before, and set the achievement
                                    cell.Controls.Add(achievementElement);
                                    row.Controls.Add(cell);
                                    tableRecentAchievements.Controls.Add(row);
                                    achievementElement.SetAchievement(a, true, false, currentUser);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                // If no user is found, redirect the client to the default home address as defined
                // by the app settings.
                Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["DefaultHome"]);
            }
        }
    }
}