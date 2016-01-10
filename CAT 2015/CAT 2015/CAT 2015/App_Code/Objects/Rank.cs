using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// Last Modified
// 27/11/15

/// <summary>
/// Rank object type. Used to store rank data.
/// </summary>
namespace CAT_2015
{
    public class Rank
    {
        private int id;
        private string name;
        private int minScore;
        private string image;

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

        public int MinScore
        {
            get
            {
                return minScore;
            }
        }

        public string Image
        {
            get
            {
                return image;
            }
        }

        /// <summary>
        /// Creates an entirely new rank class and then adds it to the rank table. Then gets
        /// database-specific properties and pulls them too.
        /// </summary>
        public Rank(string name, int minScore, string image)
        {
            // TODO: this = Database.CreateRank(name, minScore, image);
        }

        /// <summary>
        /// Consructor used by the database class to create ranks returned from the ranks table.
        /// </summary>
        public Rank(int id, string name, int minScore, string image)
        {
            this.id = id;
            this.name = name;
            this.minScore = minScore;
            this.image = image;
        }
    }
}
