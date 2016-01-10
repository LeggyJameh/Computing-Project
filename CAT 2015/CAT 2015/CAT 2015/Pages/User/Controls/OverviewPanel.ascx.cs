using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CAT_2015.Pages.User.Controls
{
    public partial class OverviewPanel : System.Web.UI.UserControl
    {
        CAT_2015.User currentUser = null; // Current client's associated user
        CAT_2015.Rank currentRank = null; // Current user's current rank
        CAT_2015.Rank nextRank = null; // Current user's next rank

        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the logged in user
            currentUser = AppCode.Database.GetLoggedInUser(Session.SessionID);
            if (currentUser != null) // If a user was found..
            {
                // Get the user's current rank
                currentRank = AppCode.Database.GetRankFromID(currentUser.RankID);
                // And fill the labels with the relavent information..
                labelUsername.Text = "Username: " + currentUser.Username;
                labelNickname.Text = "Nickname: " + currentUser.NickName;
                labelActualname.Text = "Real Name: " + currentUser.RealName;
                labelTotalPoints.Text = currentUser.Score.ToString() + " Points";
                // If the nickname is locked..
                if (currentUser.NickLocked)
                {
                    // Make it red and display (Locked)
                    labelNickname.CssClass = "nickLocked";
                    labelNickname.Text += " (Locked)";
                }
                if (currentRank != null) // If a current rank was found..
                {
                    // Get the next rank
                    nextRank = AppCode.Database.GetNextRankUp(currentRank);
                    // And fill out information relating to the current rank
                    labelRankName.Text = currentRank.Name;
                    labelRankNumber.Text = "Level " +
                    AppCode.Database.GetRankRanking(currentRank).ToString();
                    imageRank.ImageUrl = currentRank.Image;
                    imageRank.ImageAlign = ImageAlign.Middle;
                    if (nextRank != null) // If the user's next rank was found..
                    {
                        int percentage =
                        Convert.ToInt16(((double)(currentUser.Score - currentRank.MinScore) /
                        (double)(nextRank.MinScore - currentRank.MinScore)) * 100);
                        progressBar.setPercent(percentage);
                        // Fill the labels with relavent information
                        labelProgressPercentage.Text = "Next Level: " +
                        (currentUser.Score - currentRank.MinScore).ToString() +
                        " / " +
                        (nextRank.MinScore - currentRank.MinScore).ToString() +
                        " ( " +
                        percentage.ToString() +
                        "% ) ";
                    }
                    else
                    {
                        //if (AppCode.Database.IsMaxRank(currentRank))
                        //{
                        labelProgressPercentage.Text = "Max Level Achieved!";
                        // }
                    }
                }
            }
        }
    }
}