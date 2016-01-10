using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CAT_2015.Pages
{
    public partial class NavMenu : System.Web.UI.UserControl
    {
        CAT_2015.User currentUser = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Pull the current user via the client's sessionID
            currentUser = AppCode.Database.GetLoggedInUser(Session.SessionID);
            // If a user was found...
            if (currentUser != null)
            {
                // Check if the user is an admin. If yes..
                if (currentUser.PermissionLevel > 0)
                {
                    // Add the buttons to the nav menu for admins
                    addButtons(AppCode.PageAccess.AdminPages);
                }
                else // If the user is just a normal user...
                {
                    // Add the buttons to the nav menu for users
                    addButtons(AppCode.PageAccess.UserPages);
                }
            }
            else
            {
                // If no user was found, add the default non-user pages
                addButtons(AppCode.PageAccess.DefaultPages);
            }
        }

        /// <summary>
        /// Uses the specified links dictionary to add buttons to the naviation menu
        /// </summary>
        void addButtons(Dictionary<string, string> links)
        {
            // For each item in the dictionary..
            foreach (string name in links.Keys)
            {
                // Create a row, cell and button
                TableRow row = new TableRow();
                TableCell cell = new TableCell();
                Button button = new Button();

                // Set the button's properties accordingly
                button.ID = "button_" + name;
                button.CssClass = "button";
                button.Text = name;
                button.Click += buttonUser_Click;

                // And add the button, cell and row to the table
                cell.Controls.Add(button);
                row.Cells.Add(cell);
                NavTable.Rows.Add(row);
            }
        }
        
        /// <summary>
        /// Send the client to the correct page when the button is pressed
        /// </summary>
        void buttonUser_Click(object sender, EventArgs e)
        {
            // Cast the sender as a button
            Button button = (Button)sender;
            // Log the user out if the button says logout
            if (button.Text == "Logout")
            {
                AppCode.Database.RemoveSession(Session.SessionID);
                Response.Redirect(CAT_2015.AppCode.PageAccess.DefaultPages["Login"]);
            }
            else // Otherwise, redirect to the relavent page
            {
                if (currentUser != null) // If the user exists..
                {
                    if (currentUser.PermissionLevel > 0) // If the user is an admin..
                    {
                        Response.Redirect(CAT_2015.AppCode.PageAccess.AdminPages[button.Text]);
                    }
                    else // If the user is not an admin..
                    {
                        Response.Redirect(CAT_2015.AppCode.PageAccess.UserPages[button.Text]);
                    }
                }
                else // If there is no user..
                {
                    Response.Redirect(CAT_2015.AppCode.PageAccess.DefaultPages[button.Text]);
                }
            }
        }
    }
}