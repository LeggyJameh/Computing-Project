using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// Last Modified
// 27/11/15

/// <summary>
/// Reward object type. Used to store reward data.
/// </summary>
namespace CAT_2015
{
    public class Reward
    {
        private int id;
        private string name;
        private string description;
        private string image;
        private int rankID;

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

        public string Image
        {
            get
            {
                return image;
            }
        }

        public int RankID
        {
            get
            {
                return rankID;
            }
        }

        /// <summary>
        /// Creates an entirely new reward class and then adds it to the reward table. Then gets
        /// database-specific properties and pulls them too.
        /// </summary>
        public Reward(string name, string description, string image, int rankID)
        {
            // TODO: Database.CreateReward(name, description, image, rankID);
        }

        /// <summary>
        /// Consructor used by the database class to create rewards returned from the rewards table.
        /// </summary>
        public Reward(int id, string name, string description, string image, int rankID)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.image = image;
            this.rankID = rankID;
        }
    }
}