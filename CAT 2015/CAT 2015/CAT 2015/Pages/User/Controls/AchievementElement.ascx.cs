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

        public void SetAchievement(Achievement achievement, DateTime dateAchieved)
        {
            labelName.Text = achievement.Name;
            labelDescription.Text = achievement.Description;
            labelPointsValue.Text = achievement.Value.ToString() + " Points";
            labelDateAchieved.Text = dateAchieved.ToShortDateString();
            imageAchievement.ImageUrl = achievement.Image;
        }
    }
}