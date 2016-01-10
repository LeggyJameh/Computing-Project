using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// Last Modified
// 27/11/15

/// <summary>
/// User object type. Used to store user data.
/// </summary>
namespace CAT_2015
{
    public class User
    {
        private int id;
        private string username;
        private DateTime dateAdded;
        private int score;
        private int rankID;
        private bool disabled;

        public string RealName;
        public string NickName;
        public string EmailAddress;
        public bool EmailPreference;
        public bool NickLocked;
        public int PermissionLevel;

        public int ID
        {
            get
            {
                return id;
            }
        }

        public string Username
        {
            get
            {
                return username;
            }
        }

        public DateTime DateAdded
        {
            get
            {
                return dateAdded;
            }
        }

        public int Score
        {
            get
            {
                return score;
            }
        }

        public int RankID
        {
            get
            {
                return rankID;
            }
        }

        public bool Disabled
        {
            get
            {
                return disabled;
            }
        }

        /// <summary>
        /// Create completely new user. Adds this user to the database. Then gets
        /// database-specific properties and pulls them too.
        /// </summary>
        public User(int id, string username, string password)
        {
            // TODO: this = Database.CreateUser(id, username, password)
        }

        /// <summary>
        /// Consructor used by the database class to create users returned from the users table.
        /// </summary>
        public User(int id, string username, string realName, string nickName, string email, bool emailPref, DateTime dateAdded, int score, int rankID, bool nickLocked, bool disabled, int permissionLevel)
        {
            this.id = id;
            this.username = username;
            this.RealName = realName;
            this.NickName = nickName;
            this.EmailAddress = email;
            this.EmailPreference = emailPref;
            this.dateAdded = dateAdded;
            this.score = score;
            this.rankID = rankID;
            this.NickLocked = nickLocked;
            this.disabled = disabled;
            this.PermissionLevel = permissionLevel;
        }
    }
}