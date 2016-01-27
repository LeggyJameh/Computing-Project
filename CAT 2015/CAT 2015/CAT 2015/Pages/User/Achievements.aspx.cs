using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CAT_2015.Pages.User
{
    public partial class Achievements : System.Web.UI.Page
    {
        CAT_2015.User currentUser = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the current user
            currentUser = AppCode.Database.GetLoggedInUser(Session.SessionID);
            // If the current user does exist...
            if (currentUser != null)
            {
                // Get the user's obtained and requested achievements
                List<int> obtainedAchievements = AppCode.Database.GetUserAchievementIDs(currentUser, false, true);
                List<int> requestedAchievements = AppCode.Database.GetUserAchievementIDs(currentUser, true, false);
                int obtainedIndex = 0;
                int requestedIndex = 0;
                int unachievedIndex = 0;

                foreach(string s in AppCode.Cache.Achievements.Keys)
                {
                    createHeaderRow(s);
                    unachievedIndex++;
                    obtainedIndex = unachievedIndex;
                    requestedIndex = unachievedIndex;
                    foreach (Achievement a in AppCode.Cache.Achievements[s])
                    {
                        if (obtainedAchievements.Contains(a.ID))
                        {
                            createRow(a, true, false, obtainedIndex);
                            obtainedIndex++;
                            requestedIndex++;
                            unachievedIndex++;
                        }
                        else
                        {
                            if (requestedAchievements.Contains(a.ID))
                            {
                                createRow(a, false, true, requestedIndex);
                                requestedIndex++;
                                unachievedIndex++;
                            }
                            else
                            {
                                createRow(a, false, false, unachievedIndex);
                                unachievedIndex++;
                            }
                        }

                    }
                }
            }
        }
 
        /// <summary>
        /// Adds the achievement to the table.
        /// </summary>
        private void createRow(Achievement achievement, bool achieved, bool requested, int index)
        {
            // Create a cell and row
            TableCell cell = new TableCell();
            TableRow row = new TableRow();

            row.Height = 100;

            // Create an achievement element from the achievement
            CAT_2015.Pages.User.Controls.AchievementElement achievementElement =
            (CAT_2015.Pages.User.Controls.AchievementElement)Page.LoadControl
            ("~/Pages/User/Controls/AchievementElement.ascx");

            // Add the achievement element to the cell, the cell to the row and the row to the table
            cell.Controls.Add(achievementElement);
            row.Cells.Add(cell);
            tableAchievements.Rows.AddAt(index, row);
            // Display the achievement on the achievement element.
            achievementElement.SetAchievement(achievement, achieved, requested, currentUser);
        }

        private void createHeaderRow(string text)
        {
            // Create a cell and row
            TableCell cell = new TableCell();
            TableRow row = new TableRow();
            Label label = new Label();
            label.Text = text;
            label.CssClass = "AchievementsHeader";
            label.Height = Unit.Percentage(100);
            label.Width = Unit.Percentage(100);
            cell.Controls.Add(label);
            row.Cells.Add(cell);
            tableAchievements.Rows.Add(row);
        }
    }
}