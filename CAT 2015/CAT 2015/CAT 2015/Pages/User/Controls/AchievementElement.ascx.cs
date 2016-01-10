using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CAT_2015.Pages.User.Controls
{
    public partial class AchievementElement : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void SetAchievement(Achievement achievement, bool achieved, CAT_2015.User user)
        {
            if (achievement != null && user != null)
            {
                labelName.Text = achievement.Name;
                labelDescription.Text = achievement.Description;
                labelPointsValue.Text = achievement.Value.ToString() + " Points";
                imageAchievement.ImageUrl = achievement.Image;

                if (!achieved)
                {
                    Table1.CssClass += "Grey";
                    cellImage.CssClass += "Grey";
                    imageAchievement.CssClass += "Grey";
                    cellName.CssClass += "Grey";
                    labelName.CssClass += "Grey";
                    cellPointsValue.CssClass += "Grey";
                    labelPointsValue.CssClass += "Grey";
                    cellDescription.CssClass += "Grey";
                    labelDescription.CssClass += "Grey";
                    cellDateAchieved.CssClass += "Grey";
                    labelDateAchieved.CssClass += "Grey";

                    if (achievement.Hidden)
                    {
                        labelName.Text = "SECRET ACHIEVEMENT";
                        labelDescription.Text = "WOULDN'T YOU LIKE TO KNOW HOW TO GET THIS!";
                        labelPointsValue.Text = achievement.Value.ToString() + " Points";
                        imageAchievement.ImageUrl = "~/Images/achievementLocked.png";
                    }
                }
                else
                {
                    labelDateAchieved.Text = "Achieved " + AppCode.Database.GetUserAchievementDateAchieved(achievement, user).ToShortDateString();
                }
            }
        }
    }
}