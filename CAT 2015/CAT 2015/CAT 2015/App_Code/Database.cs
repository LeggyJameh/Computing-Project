using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Odbc;
using System.Configuration;
using System.Data.Common;

// Last Modified
// 01/12/15

namespace CAT_2015.AppCode
{
    public static class Database
    {
        #region Variables
        private static OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString);

        // Bunch of constants that can be used if there is a change in the database's table names.
        private static string userTable = ConfigurationManager.AppSettings["UsersTableName"];
        private static string achievementTable = ConfigurationManager.AppSettings["AchievementsTableName"];
        private static string achievementDataTable = ConfigurationManager.AppSettings["AchievementDataTableName"];
        private static string rewardsTable = ConfigurationManager.AppSettings["RewardsTableName"];
        private static string rewardsDataTable = ConfigurationManager.AppSettings["RewardsDataTableName"];
        private static string ranksTable = ConfigurationManager.AppSettings["RanksTableName"];

        /// <summary>
        /// Variable that when set, displays output to console window. Alerts the admin of issues.
        /// </summary>
        private static string error
        {
            set
            {
                Console.WriteLine(value);
            }
        }
        #endregion

        #region connection

        /// <summary>
        /// Opens the connection and allows queries to be executed. Returns true if successful.
        /// </summary>
        public static bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (OdbcException ex)
            {
                error = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Closes the connection. Will keep trying until it manages to do so. Should only occur open the program closing.
        /// </summary>
        public static void CloseConnection()
        {
            do
            {
                try
                {
                    connection.Close();
                }
                catch (OdbcException ex)
                {
                    error = ex.Message;
                }
            }
            while (connection.State != System.Data.ConnectionState.Closed);
        }

        /// <summary>
        /// Check to see if the connection is open. If it is not, tries to open the connection. If this fails, returns false. If connection is open or is successfully opened, returns true.
        /// </summary>
        private static bool connectionOpen()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                return true;
            }
            else
            {
                return OpenConnection();
            }
        }
        #endregion

        #region queries

        /// <summary>
        /// Returns a list of achievements that are of the specified categories. 27/11/15
        /// </summary>
        public static List<Achievement> GetAchievements(List<string> categories)
        {
            // Create a list to store the returned achievements
            List<Achievement> achievements = new List<Achievement>();
            // Get all achievements where the category is...
            string query = "SELECT * FROM `" + achievementTable + "` WHERE `Category` IN (";

            // ..any of the categories specified.
            foreach (string x in categories)
            {
                query = (query + '"' + x + '"' + ",");
            }

            // Remove the succeeding comma that is added due to the previous foreach loop,
            // and finish the query with a bracket.
            query = query.Remove(query.Length - 1);
            query = query + ")";

            if (connectionOpen())
            {
                // Create a database command from the query and existing connection.
                OdbcCommand cmd = new OdbcCommand(query, connection);
                try
                {
                    // Execute the command and open a reader.
                    OdbcDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read()) // Read the next record.
                    {
                        // Get the id first, and check to make sure it is something.
                        int id = dataReader.GetInt16(0);
                        if (id != 0)
                        {
                            // Adds the achievement that is read from the database to the
                            // list of achievements that is to be returned.
                            achievements.Add(new Achievement(id, dataReader.GetString(1),
                            dataReader.GetString(2), dataReader.GetString(3),
                            dataReader.GetString(4), dataReader.GetInt16(5),
                            dataReader.GetDateTime(6), dataReader.GetBoolean(7)));
                        }
                    }
                    dataReader.Close();
                }
                catch (OdbcException ex)
                {
                    // Displays an error if something bad occurs while executing the command
                    error = ex.Message;
                }
            }
            return achievements;
        }

        /// <summary>
        /// Returns a dictionary of achievements and the number of times they have been achieved,
        /// based on the categories set from the achievementData table. 27/11/15
        /// </summary>
        public static Dictionary<Achievement, int> GetAchievementCounts(List<string> categories)
        {
            // Get the achievements of the specified categories
            List<Achievement> achievements = GetAchievements(categories);
            // Plonk these achievements into a dictionary used for the counts of these achievements
            Dictionary<Achievement, int> counts = achievements.ToDictionary(x => x, x => 0);
            string query;

            foreach (Achievement x in counts.Keys)
            {
                // For each achievement, find the number of times it occurs in the achievementData
                // table where it has been achieved and not just requested
                query = "SELECT COUNT(`ID`) FROM `" + achievementDataTable + "` WHERE `Requested`='0'";

                if (connectionOpen())
                {
                    // Creates a database command from the query and existing connection
                    OdbcCommand cmd = new OdbcCommand(query, connection);
                    try
                    {
                        int count = 0;
                        // Execute the command and open the reader
                        OdbcDataReader dataReader = cmd.ExecuteReader();
                        dataReader.Read(); // Get the first record.
                        count = dataReader.GetInt16(0); // Read the first value in the record
                        dataReader.Close();
                        counts[x] = count;
                    }
                    catch (OdbcException ex)
                    {
                        // Displays an error if something bad occurs while executing the command
                        error = ex.Message;
                    }
                }
            }
            return counts;
        }

        /// <summary>
        /// Returns a list of achievements that the user has obtained based on the
        /// requested parameter. 01/12/15
        /// </summary>
        public static List<Achievement> GetUserAchievements(User user, bool requested, bool awarded)
        {
            return GetAchievementsFromIDs(GetUserAchievementIDs(user, requested, awarded));
        }

        /// <summary>
        /// Returns a list of ids of the achievements that the user has obtained. 01/12/15
        /// </summary>
        public static List<int> GetUserAchievementIDs(User user, bool requested, bool awarded)
        {
            // List of achievement ID's from the achievementData table
            List<int> achievementIDs = new List<int>();
            string query;

            /* Get all achievement ID's from the achievementData table for this user
             * that have pertain to the requested parameter. */
            query = "SELECT `AchievementID` FROM `" + achievementDataTable + "` WHERE `UserID`='"
            + user.ID + "' AND `Requested`='" + Convert.ToInt16(requested) + "' AND `Awarded`='" + Convert.ToInt16(awarded) + "'";

            if (connectionOpen())
            {
                // Creates a database command from the query and existing connection.
                OdbcCommand cmd = new OdbcCommand(query, connection);
                try
                {
                    // Execute the command and open a reader
                    OdbcDataReader dataReader = cmd.ExecuteReader();
                    // While there are more records to read...
                    while (dataReader.Read())
                    {
                        // Add the returned int to the achievementID list
                        achievementIDs.Add(dataReader.GetInt16(0));
                    }
                    dataReader.Close();
                }
                catch (OdbcException ex)
                {
                    // Displays an error if something bad occurs while executing the command
                    error = ex.Message;
                }
            }

            return achievementIDs;
        }

        /// <summary>
        /// Returns a list of achievements from a list of achievement id's 01/12/15
        /// </summary>
        public static List<Achievement> GetAchievementsFromIDs(List<int> ids)
        {
            // List of achievements to store the achievements to return
            List<Achievement> achievements = new List<Achievement>();
            string query;
            OdbcCommand cmd;

            // For each of the returned IDs
            foreach (int x in ids)
            {
                // Create a query to get that achievement's data from the achievement table
                query = "SELECT * FROM `" + achievementTable + "` WHERE `ID`='" + x + "'";
                // Create a database command from the query and existing connection
                cmd = new OdbcCommand(query, connection);

                if (connectionOpen())
                {
                    try
                    {
                        // Execute the command and open a reader
                        OdbcDataReader dataReader = cmd.ExecuteReader();
                        // While there are more records to process...
                        while (dataReader.Read())
                        {
                            // Get the ID and make sure it is an actual achievement
                            int id = dataReader.GetInt16(0);
                            if (id >= 1)
                            {
                                // Then add the returned achievement to the achievement list
                                achievements.Add(readAchievement(dataReader));
                            }
                        }
                        dataReader.Close();
                    }
                    catch (OdbcException ex)
                    {
                        // Displays an error if something bad occurs while executing the command
                        error = ex.Message;
                    }
                }
            }
            return achievements; // Return the final list of acievments. May be empty if the
            // query failed
        }

        /// <summary>
        /// Returns a list of all users in the system. 07/12/15  NEEDS TO BE UPDATED AS NEW ENTRY FOR READ
        /// </summary>
        public static List<User> GetUsers()
        {
            // Create a list to store the return values in
            List<User> users = new List<User>();
            // Create a query to return all users
            string query = "SELECT * FROM `" + userTable + "`";
            // Create a command from the query and the existing connection
            OdbcCommand cmd = new OdbcCommand(query, connection);

            if (connectionOpen())
            {
                try
                {
                    // Execute the command and open a reader
                    OdbcDataReader dataReader = cmd.ExecuteReader();
                    // While there are more records to process...
                    while (dataReader.Read())
                    {
                        // Grab the ID and make sure it is an actual user...
                        int id = dataReader.GetInt16(0);
                        if (id >= 1)
                        {
                            // Then add the returned user to the user list
                            users.Add(readUser(dataReader));
                        }
                    }
                    dataReader.Close();
                }
                catch (OdbcException ex)
                {
                    // Displays an error if something bad occurs while executing the command
                    error = ex.Message;
                }
            }
            return users; // Return the final list of users, may be empty if the query failed.
        }

        /// <summary>
        /// Returns a dictionary of categories, which contains a dictionary of users and their
        /// scores in those categories. 07/12/15
        /// </summary>
        public static Dictionary<string, Dictionary<User, int>> GetUserScoresForCategories(List<string> categories)
        {
            // Double dictionary to store values to be returned in organised manner
            Dictionary<string, Dictionary<User, int>> scores = new Dictionary<string, Dictionary<User, int>>();
            // Get all of the users
            List<User> users = GetUsers();

            // For each category...
            foreach (string cat in categories)
            {
                // Create a dictionary and..
                scores.Add(cat, new Dictionary<User, int>());

                // For each user...
                foreach (User user in users)
                {
                    // Add that user to the category along with their score for that category
                    scores[cat].Add(user, GetUserScoreForCategory(cat, user));
                }
            }
            return scores; // Return the monolithic double dictionary of doom
        }

        /// <summary>
        /// Returns a KeyValuePair where the key is the user, and the value is the score for
        /// the specified user and for the specified category, returned from the database 07/12/15
        /// </summary>
        public static int GetUserScoreForCategory(string category, User user)
        {
            // Get all achievments that the specified user has achieved and not just requested
            string query = "SELECT `AchievementID` FROM `" + achievementDataTable +
            "` WHERE `Requested`='0' AND `UserID`='" + user.ID + "'";

            // Temporary list of achievement IDs used to store the data from the achievementData table
            List<int> achievementIDs = new List<int>();

            // Create a database command from the query and existing connection
            OdbcCommand cmd = new OdbcCommand(query, connection);

            if (connectionOpen())
            {
                try
                {
                    // Execute the command and open the reader
                    OdbcDataReader dataReader = cmd.ExecuteReader();

                    // While there are more records to be read...
                    while (dataReader.Read())
                    {
                        // Add to the achievementID list the ID of the achievement just read
                        achievementIDs.Add(dataReader.GetInt16(0));
                    }
                    dataReader.Close();
                }
                catch (OdbcException ex)
                {
                    // Displays an error if something bad occurs while executing the command
                    error = ex.Message;
                }
            }

            // Return the total worth of all of the specified achievements
            return GetAchievementsScore(achievementIDs, new List<string> { category });
        }

        /// <summary>
        /// Returns the total score from for the category from the specified achievment ID list 07/12/15
        /// </summary>
        public static int GetAchievementsScore(List<int> achievementIDs, List<string> categories)
        {
            // Get all of the achievements from the database that have the specified category
            List<Achievement> achievements = GetAchievements(categories);
            int score = 0; // Variable that the sum of the achievements is to be stored in

            // For each achievement..
            foreach (Achievement achievement in achievements)
            {
                // See if the user has it.. and if it does..
                if (achievementIDs.Contains(achievement.ID))
                {
                    // Add the value of the achievement onto the total score
                    score += achievement.Value;
                }
            }
            return score; // Return the total score
        }

        /// <summary>
        /// Used to login the user. Uses the username and password provided and hashes the
        /// password, checks it against the existing password and authenticates, if
        /// successful, returns true with UserID out for cookie storage of user session.
        /// 14/12/15
        /// </summary>
        public static bool AuthenticateUser(string username, string password, string sessionID)
        {
            // Pull the password from the database for the specified user
            string query = "SELECT `sahp`, `disabled` FROM `" + userTable + "` WHERE `Username`='" + username + "'";
            string sahp = "";
            bool disabled = true;
            // Creates a database command from the query and existing connection
            OdbcCommand cmd = new OdbcCommand(query, connection);

            if (connectionOpen())
            {
                try
                {
                    // Execute the command and open a reader
                    OdbcDataReader dataReader = cmd.ExecuteReader();
                    // If the query has returned anything...
                    if (dataReader.HasRows)
                    {
                        // Advance to first row
                        dataReader.Read();
                        // Get the salted and hashed password
                        sahp = dataReader.GetString(0);
                        disabled = dataReader.GetBoolean(1);
                    }
                    dataReader.Close();
                }
                catch (OdbcException ex)
                {
                    // Displays an error if something bad occurs while executing the command
                    error = ex.Message;
                }

                // Checks that the user has not been disabled
                if (!disabled)
                {
                    // Takes the salted and hashed password and uses it to validate the provided password
                    if (PasswordHash.ValidatePassword(password, sahp))
                    {
                        // If the password is correct, sets the SessionID in the database to
                        // the current client's SessionID and returns that authentication was sucessful
                        ExecuteNonQuery("UPDATE `" + userTable + "` SET `SessionID`='" + sessionID +
                        "' WHERE `Username`='" + username + "';");
                        return true;
                    }
                }
            }
            // If something didn't pass, authentication was unsucessful
            return false;
        }

        /// <summary>
        /// Returns the logged in user using the sessionID. 14/12/15
        /// </summary>
        public static User GetLoggedInUser(string sessionID)
        {
            // Find a user with the current client's sessionID
            string query = "SELECT * FROM `" + userTable + "` WHERE `SessionID`='" + sessionID + "'";
            User user = null;
            // Creates a database command from the query and existing connection
            OdbcCommand cmd = new OdbcCommand(query, connection);

            if (connectionOpen())
            {
                try
                {
                    // Execute the command and open a reader
                    OdbcDataReader dataReader = cmd.ExecuteReader();
                    // Advance to the first row
                    dataReader.Read();
                    // If the query has returned a user...
                    if (dataReader.HasRows)
                    {
                        // Get the user
                        user = readUser(dataReader);
                    }
                    dataReader.Close();
                }
                catch (OdbcException ex)
                {
                    // Displays an error if something bad occurs while executing the command
                    error = ex.Message;
                }
            }
            // Returns the constructed user, or null if something bad occurred
            return user;
        }

        /// <summary>
        /// Returns the rank with the associated id 15/12/15
        /// </summary>
        public static Rank GetRankFromID(int id)
        {
            // Find the rank associated with the provided id
            string query = "SELECT * FROM `" + ranksTable + "` WHERE `ID`='" + id + "'";
            // Creates a database command from the query and existing connection
            OdbcCommand cmd = new OdbcCommand(query, connection);
            Rank rank = null;

            if (connectionOpen())
            {
                try
                {
                    // Execute the command and open a reader
                    OdbcDataReader dataReader = cmd.ExecuteReader();
                    // If the query has returned any results at all..
                    if (dataReader.HasRows)
                    {
                        // Get the next record
                        dataReader.Read();
                        // Read the record as a rank
                        rank = readRank(dataReader);
                        // Close the data reader
                        dataReader.Close();
                    }
                }
                catch (OdbcException ex)
                {
                    // Displays an error if something bad occurs while executing the command
                    error = ex.Message;
                }
            }
            // Returns the constructed rank, or returns null if something bad happened
            return rank;
        }

        /// <summary>
        /// Returns the rank above the supplied rank 15/12/15
        /// </summary>
        public static Rank GetNextRankUp(Rank currentRank)
        {
            // Find the first rank with a higher MinScore than the current rank
            string query = "SELECT * FROM `" + ranksTable + "` WHERE `MinScore`>'" + currentRank.MinScore +
            "' ORDER BY `MinScore` ASC LIMIT 1";
            // Creates a database command from the query and existing connection
            OdbcCommand cmd = new OdbcCommand(query, connection);
            Rank rank = null;

            if (connectionOpen())
            {
                try
                {
                    // Execute the command and open a reader
                    OdbcDataReader dataReader = cmd.ExecuteReader();
                    // If the data reader returns anything
                    if (dataReader.HasRows)
                    {
                        // Read the first record
                        dataReader.Read();
                        // Read the record as a rank and add it to the rank list
                        rank = readRank(dataReader);
                    }
                    // Close the data reader
                    dataReader.Close();
                }
                catch (OdbcException ex)
                {
                    // Displays an error if something bad occurs while executing the command
                    error = ex.Message;
                }
            }
            return rank;
        }

        /// <summary>
        /// Returns the ranking of the current rank. Example: Would return 1 for Inigma, assuming
        /// default dataset. 15/12/15
        /// </summary>
        public static int GetRankRanking(Rank rank)
        {
            // Get the number of ranks where the MinScore is below the current rank's MinScore
            string query = "SELECT COUNT(0) FROM `" + ranksTable + "` WHERE `MinScore`<'" + rank.MinScore + "'";
            int ranking = 0;
            // Create a database command from the query and existing connection
            OdbcCommand cmd = new OdbcCommand(query, connection);

            if (connectionOpen())
            {
                try
                {
                    // Execute the command and open a reader
                    OdbcDataReader dataReader = cmd.ExecuteReader();
                    // If any data was returned by the reader..
                    if (dataReader.HasRows)
                    {
                        // Advance to first record
                        dataReader.Read();
                        // Read the ranking
                        ranking = dataReader.GetInt16(0);
                        // Close the reader
                        dataReader.Close();
                    }
                }
                catch (OdbcException ex)
                {
                    // Displays an error if something bad occurs while executing the command
                    error = ex.Message;
                }
            }
            // Return the number of ranks with lower minScore + 1 for ranking position
            return ranking + 1;
        }

        /// <summary>
        /// Returns the date that the achievement was obtained. Retuns today if achievement was
        /// never obtained or the query failed. CREATED 06/01/16
        /// </summary>
        public static DateTime GetUserAchievementDateAchieved(CAT_2015.Achievement achievement,
        CAT_2015.User user)
        {
            // Get the date updated for this achievement from the achievementdata table.
            string query = "SELECT `DateUpdated` FROM `" + achievementDataTable + "` WHERE `AchievementID`='" + achievement.ID +
            "' AND `UserID`='" + user.ID + "'";
            // Set the dateAchieved to something other than null.
            DateTime dateAchieved = DateTime.Today;
            // Create a command from the query and existing connection.
            OdbcCommand cmd = new OdbcCommand(query, connection);

            // If the connection is already open...
            if (connectionOpen())
            {
                try
                {
                    // Execute the command and open a reader.
                    OdbcDataReader dataReader = cmd.ExecuteReader();
                    // If any rows are returned...
                    if (dataReader.HasRows)
                    {
                        // Load the first record.
                        dataReader.Read();
                        // Format the return as a DateTime.
                        dateAchieved = dataReader.GetDateTime(0);
                        // Close the datareader.
                        dataReader.Close();
                    }
                }
                
                catch (OdbcException ex)
                {
                    // Displays an error if something bad occurs while executing the command
                    error = ex.Message;
                }
            }
            // Return the dateAchieved. Will return today if something bad occurs or the awarded
            // achievement could not be found.
            return dateAchieved;
        }

        /// <summary>
        /// Gets the set number of most recent achievement's IDs for adding to the recent
        /// achievement table ADDED 06/01/16
        /// </summary>
        public static List<int> GetRecentAchievementIDsForUser(User currentUser)
        {
            // Create a list of integers to store the achievement IDs
            List<int> achievementIDs = new List<int>();
            // Get the achievementID's of the most recently obtained achievments for this user
            string query = "SELECT `AchievementID` FROM `" + achievementDataTable + "` WHERE `UserID`='" +
            currentUser.ID + "' AND `Awarded`='1' ORDER BY `DateUpdated` DESC LIMIT " +
            ConfigurationManager.AppSettings["NumOfRecentAchievements"];
            // Create a command from the query and existing connection
            OdbcCommand cmd = new OdbcCommand(query, connection);
            
            // If the connection is open...
            if (connectionOpen())
            {
                try
                {
                    // Execute the command and open a data reader
                    OdbcDataReader dataReader = cmd.ExecuteReader();
                    // If anything was returned...
                    if (dataReader.HasRows)
                    {
                        while(dataReader.Read()) // While there are still more values to read...
                        {
                            // Read the output as an integer and add it to the list.
                            achievementIDs.Add(dataReader.GetInt16(0));
                        }
                    }
                    // Close the datareader once all values have been processed.
                    dataReader.Close();
                }
                catch (OdbcException ex)
                {
                    // Displays an error if something bad occurs while executing the command
                    error = ex.Message;
                }
            }
            // Return the list of achievementID's. If something bad occurs or the user has no achievements,
            // returns an empty list.
            return achievementIDs;
        }

        /// <summary>
        /// Returns a list of all the categories that the achievements pertain to.
        /// </summary>
        public static List<string> GetCategories()
        {
            // Return all unique values in the category column
            string query = "SELECT DISTINCT `category` FROM `" + achievementTable + "`";
            // Create a command from the query and existing connection
            OdbcCommand cmd = new OdbcCommand(query, connection);
            // Initialise the categories list
            List<string> categories = new List<string>();

            // If the connection is currently open...
            if (connectionOpen())
            {
                try
                {
                    // Execute the command and open a reader
                    OdbcDataReader dataReader = cmd.ExecuteReader();

                    // If the reader returns anything...
                    if (dataReader.HasRows)
                    {
                        // While there are still entries to read...
                        while (dataReader.Read())
                        {
                            // Read it as a string and add it to the categories list
                            categories.Add(dataReader.GetString(0));
                        }
                    }
                    // Close the data reader
                    dataReader.Close();
                }
                catch (OdbcException ex)
                {
                    // Display an error if something goes wrong
                    error = ex.Message;
                }
            }
            return categories;
        }

        /// <summary>
        /// Logs out the given session by removing them from their associated user in the database
        /// </summary>
        public static bool RemoveSession(string SessionID)
        {
            // Set the sessionID to be nothing where the sessionID is the current sessionID
            string query = "UPDATE `" + userTable + "` SET `SessionID`='' WHERE `SessionID`='" + SessionID + "';";
            return ExecuteNonQuery(query);
        }

        /// <summary>
        /// Used by the achievement constructor to add the achievement to the database.
        /// Returns the finished achievement which the original gets replaced with.
        /// </summary>
        public static Achievement AddAchievement(string name, string description, string category, string image, int value, bool hidden)
        {
            string query = "INSERT INTO `" + achievementTable + "` (`Name`, `Description`, `Category`, `Image`, `Value`, `Hidden`) VALUES ('" + name + "', '" + description + "', '" + category + "', '" + image + "', '" + value + "', '" + hidden + "');";
            OdbcCommand cmd = new OdbcCommand(query, connection);

            ExecuteNonQuery(query);
            return GetAchievementByID(GetLastInsertID());
        }

        public static Achievement GetAchievementByID(int id)
        {
            string query = "SELECT * FROM `" + achievementTable + "` WHERE `id`='" + id + "' LIMIT 1";
            OdbcCommand cmd = new OdbcCommand(query, connection);
            Achievement achievement = null;
            
            if (connectionOpen())
            {
                try
                {
                    OdbcDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        dataReader.Read();
                        achievement = readAchievement(dataReader);
                    }
                    dataReader.Close();
                }
                catch (OdbcException ex)
                {
                    error = ex.Message;
                }
            }
            return achievement;
        }

        public static int GetLastInsertID()
        {
            string query = "SELECT last_insert_id()";
            OdbcCommand cmd = new OdbcCommand(query, connection);
            int id = -1;

            if (connectionOpen())
            {
                try
                {
                    OdbcDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        dataReader.Read();
                        id = dataReader.GetInt16(0);
                    }
                    dataReader.Close();
                }
                catch (OdbcException ex)
                {
                    error = ex.Message;
                }
            }
            return id;
        }

        /// <summary>
        /// Executes a query without a return. 27/11/15
        /// </summary>
        private static bool ExecuteNonQuery(string query)
        {
            if (connectionOpen())
            {
                // Creates a database command from the query and existing connection
                OdbcCommand cmd = new OdbcCommand(query, connection);
                try
                {
                    cmd.ExecuteNonQueryAsync(); // Executes the command
                    return true;
                }
                catch (OdbcException ex)
                {
                    // Displays an error if something bad occurs while executing the command
                    error = ex.Message;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Constructs a user from the output of a datareader. Assumes that there is data
        /// ready to be read from the current record 14/12/15
        /// </summary>
        private static User readUser(OdbcDataReader dataReader)
        {
            return new User(dataReader.GetInt16(0), dataReader.GetString(1),
            dataReader.GetString(3), dataReader.GetString(4), dataReader.GetString(5),
            dataReader.GetBoolean(6), dataReader.GetDateTime(7), dataReader.GetInt16(8),
            dataReader.GetInt16(9), dataReader.GetBoolean(10), dataReader.GetBoolean(11),
            dataReader.GetInt16(13));
        }

        /// <summary>
        /// Constructs an achievement from the output of a datareader. Assumes that there
        /// is data ready to be read from the current record 14/12/15
        /// </summary>
        private static Achievement readAchievement(OdbcDataReader dataReader)
        {
            return new Achievement(dataReader.GetInt16(0), dataReader.GetString(1),
            dataReader.GetString(2), dataReader.GetString(3), dataReader.GetString(4),
            dataReader.GetInt16(5), dataReader.GetDateTime(6), dataReader.GetBoolean(7));
        }

        /// <summary>
        /// Constructs a rank from the output of a datareader. Assumes that there
        /// is data ready to be read from the current record 15/12/15
        /// </summary>
        private static Rank readRank(OdbcDataReader dataReader)
        {
            return new Rank(dataReader.GetInt16(0), dataReader.GetString(1),
            dataReader.GetInt16(2), dataReader.GetString(3));
        }

        /// <summary>
        /// Constructs a reward from the output of a datareader. Assumes that there
        /// is data ready to be read from the current record 15/12/15
        /// </summary>
        private static Reward readReward(OdbcDataReader dataReader)
        {
            return new Reward(dataReader.GetInt16(0), dataReader.GetString(1),
            dataReader.GetString(2), dataReader.GetString(3), dataReader.GetInt16(4));
        }

        #endregion
    }
}

