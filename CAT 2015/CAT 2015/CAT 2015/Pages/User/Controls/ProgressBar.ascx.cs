using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CAT_2015.Pages.User.Controls
{
    public partial class ProgressBar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void setPercent(int percentage)
        {
            part1.Width = Unit.Percentage(percentage);
            part2.Width = Unit.Percentage(100 - percentage);
        }
    }
}