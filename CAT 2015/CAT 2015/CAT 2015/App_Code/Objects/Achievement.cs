using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// Last Modified
// 27/11/15

/// <summary>
/// Achievement object type. Used to store achievement data.
/// </summary>
namespace CAT_2015
{
    public class Achievement
    {
        private int id;
        private string name;
        private string description;
        private string category;
        private string image;
        private int value;
        private DateTime dateAdded;
        private bool hidden;

        public int ID
        {
            get
            {
                return id;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
        }

        public string Category
        {
            get
            {
                return category;
            }
        }

        public string Image
        {
            get
            {
                return image;
            }
        }

        public int Value
        {
            get
            {
                return value;
            }
        }

        public DateTime DateAdded
        {
            get
            {
                return dateAdded;
            }
        }

        public bool Hidden
        {
            get
            {
                return hidden;
            }
        }

        /// <summary>
        /// Creates an entirely new achievement class and adds it to the database.
        /// Then gets database-specific properties and pulls them too.
        /// </summary>
        public Achievement(string name, string description, string category,
        string image, int value, bool hidden)
        {
            Achievement currentAchievement = AppCode.Database.AddAchievement(name, description, category, image, value, hidden);
            this.category = currentAchievement.category;
            this.dateAdded = currentAchievement.dateAdded;
            this.description = currentAchievement.description;
            this.name = currentAchievement.name;
            this.value = currentAchievement.value;
            this.image = currentAchievement.image;
            this.id = currentAchievement.id;
            this.hidden = currentAchievement.hidden;
        }

        /// <summary>
        /// Constructor used by the database class to create achievements returned
        /// from the achievements table
        /// </summary>
        public Achievement(int id, string name, string description, string category,
        string image, int value, DateTime dateAdded, bool hidden)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.category = category;
            this.image = image;
            this.value = value;
            this.dateAdded = dateAdded;
            this.hidden = hidden;
            AppCode.Cache.AddAchievement(this);
        }
    }
}