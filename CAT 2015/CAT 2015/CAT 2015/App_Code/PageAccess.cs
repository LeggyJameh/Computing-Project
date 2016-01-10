using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAT_2015.AppCode
{
    public static class PageAccess
    {
        // Pages that are displayed on the navigation menu when a user is logged in
        public static Dictionary<string, string> UserPages = new Dictionary<string, string>()
        {
            {"Home", "~/Pages/Default.aspx"},
            {"Overview","~/Pages/User/Overview.aspx"},
            {"Leaderboard","~/Pages/User/Leaderboard.aspx"},
            {"Achievements","~/Pages/User/Achievements.aspx"},
            {"Settings","~/Pages/User/Settings.aspx"},
            {"Logout",""}
        };

        // Pages that are displayed on the navigation menu when a admin is logged in
        public static Dictionary<string, string> AdminPages = new Dictionary<string, string>()
        {
            {"Achievement Approval","~/Pages/Admin/AchievementApproval.aspx"},
            {"Data Management","~/Pages/Admin/DataManagement.aspx"},
            {"Distribute Achievements","~/Pages/Admin/DistributeAchievements.aspx"},
            {"Overview","~/Pages/Admin/Overview.aspx"},
            {"User Administration","~/Pages/Admin/UserAdministration.aspx"},
            {"User Settings","~/Pages/Admin/UserSettings.aspx"},
            {"Website Settings","~/Pages/Admin/WebsiteSettings.aspx"},
            {"Logout",""}
        };

        // Pages that are displayed on the navigation menu of the data management
        // section of the admin interface
        public static Dictionary<string, string> DataManagementPages = new Dictionary<string, string>()
        {
            {"Achievement Manager","~/Pages/Admin/AchievementManager.aspx"},
            {"Images Manager","~/Pages/Admin/ImagesManager.aspx"},
            {"Ranks Manager","~/Pages/Admin/RanksManager.aspx"},
            {"Rewards Manager","~/Pages/Admin/RewardsManager.aspx"}
        };

        // Pages that are available without the need to log in
        public static Dictionary<string, string> DefaultPages = new Dictionary<string, string>()
        {
            {"Home", "~/Pages/Default.aspx"},
            {"Login", "~/Pages/Login.aspx"}
        };
    }
}