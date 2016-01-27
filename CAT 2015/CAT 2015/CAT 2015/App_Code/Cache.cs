using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAT_2015.AppCode
{
    static public class Cache
    {
        static public Dictionary<string, List<Achievement>> Achievements
        {
            get
            {
                return achievements;
            }
        }
        static private Dictionary<string, List<Achievement>> achievements = new Dictionary<string, List<Achievement>>();

        static Cache()
        {
            List<string> categories = Database.GetCategories();
            foreach (string s in categories)
            {
                achievements.Add(s, new List<Achievement>());
            }
            Database.GetAchievements(categories);
        }

        static public void AddAchievement(Achievement achievement)
        {
            achievements[achievement.Category].Add(achievement);
            achievements[achievement.Category].OrderBy(x => x.Name);
        }

        static public void RemoveAchievement(Achievement achievement)
        {
            achievements[achievement.Category].Remove(achievement);
        }

    }
}