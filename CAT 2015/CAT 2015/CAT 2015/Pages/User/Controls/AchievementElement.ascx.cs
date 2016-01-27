using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace CAT_2015.Pages.User.Controls
{
    public partial class AchievementElement : System.Web.UI.UserControl
    {
        Achievement achievement = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void SetAchievement(Achievement achievement, bool achieved, bool requested, CAT_2015.User user)
        {
            if (achievement != null && user != null) // If the achievement passed was not a null value...
            {
                // Set the name, description, image and points value to the correct values.
                labelName.Text = achievement.Name;
                labelDescription.Text = achievement.Description;
                imageAchievement.ImageUrl = achievement.Image;

                // If the value of the achievement is anything other than 1 or -1...
                if ((achievement.Value > 1 || achievement.Value < -1) && achievement.Value != 0)
                {
                    // Make the points value plural
                    labelPointsValue.Text = achievement.Value.ToString() + " " +
                    ConfigurationManager.AppSettings["nameOfPointsValue"];
                }
                else
                {
                    // Make the points value single
                    labelPointsValue.Text = achievement.Value.ToString() + " " +
                    ConfigurationManager.AppSettings["nameOfPointsValueSingle"];
                }

                if (!achieved) // If the achievement has yet to be achieved...
                {
                    if (this.Parent != null)
                    {
                        if (this.Parent.Parent != null)
                        {
                            if (this.Parent.Parent.Parent != null)
                            {
                                if (this.Parent.Parent.Parent.ID == "tableAchievements")
                                {
                                    CheckBox cb = new CheckBox();
                                    cb.Text = "Request";
                                    cb.CssClass = "base";

                                    cellButtonRequest.Controls.Add(cb);
                                    cb.CheckedChanged += cb_CheckedChanged;
                                }
                            }
                        }
                    }
                    if (requested)
                    {
                        Table1.CssClass += "Yellow";
                        cellImage.CssClass += "Yellow";
                        imageAchievement.CssClass += "Yellow";
                        cellName.CssClass += "Yellow";
                        labelName.CssClass += "Yellow";
                        cellPointsValue.CssClass += "Yellow";
                        labelPointsValue.CssClass += "Yellow";
                        cellDescription.CssClass += "Yellow";
                        labelDescription.CssClass += "Yellow";
                        cellDateAchieved.CssClass += "Yellow";
                        labelDateAchieved.CssClass += "Yellow";
                    }
                    else
                    {
                    // Make the entire style of the achievement grey
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

                        if (achievement.Hidden) // If the achievement is hidden
                        {
                            // Make it look like the template defined by the Web.config
                            labelName.Text = ConfigurationManager.AppSettings["hiddenAchievementName"];
                            labelDescription.Text =
                            ConfigurationManager.AppSettings["hiddenAchievementDescription"];
                            imageAchievement.ImageUrl = AppCode.ImageManager.GetGreyScale(
                            ConfigurationManager.AppSettings["hiddenAchievementImage"]);
                        }
                        else
                        {
                            imageAchievement.ImageUrl = AppCode.ImageManager.GetGreyScale(achievement.Image);
                        }
                    }

                    
                }
                else // If the achievement has been achieved...
                {
                    // Make it display that it was achieved on the specified day
                    labelDateAchieved.Text = "Achieved " +
                    AppCode.Database.GetUserAchievementDateAchieved(achievement, user).ToShortDateString();
                }
            }
        }

        void cb_CheckedChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}