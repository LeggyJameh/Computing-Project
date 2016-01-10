using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace CAT_2015.App_Code
{
    public static class Session
    {
        const int sessionIDLength = 16;

        /// <summary>
        /// Generates a sessionID using a random string generator, with the username
        /// appended to ensure it is unique.
        /// </summary>
        public static byte[] generateSessionID(string username)
        {
            // Random byte provider
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            // Create a sessionID of length decided by the above constant.
            byte[] sessionID = new byte[sessionIDLength];
            // Create a byte array from the username
            byte[] unBytes = Encoding.ASCII.GetBytes(username);
            // Get random bytes using the random byte provider, and add these to the sessionID.
            rng.GetBytes(sessionID);
            // Create a final array that is as long as both arrays put together
            byte[] finalArray = new byte[sessionID.Length + unBytes.Length];
            // Put the arrays together and return the result
            sessionID.CopyTo(finalArray, 0);
            unBytes.CopyTo(finalArray, sessionID.Length);
            return finalArray;
        }
    }
}