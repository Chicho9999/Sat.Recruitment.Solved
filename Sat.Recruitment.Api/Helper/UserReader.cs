using System.IO;

namespace Sat.Recruitment.Api.Helper
{
    public static class UserReader
    {
        /// <summary>
        /// Get information of users from a text file.
        /// </summary>
        /// <returns>List of users</returns>
        public static StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }
    }
}
