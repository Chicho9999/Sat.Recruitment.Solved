namespace Sat.Recruitment.Api.Model
{
    /// <summary>
    /// Base User Class
    /// </summary>
    public abstract class User
    {
        /// <summary>
        /// Name of the User
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Email of the User
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Address of the User
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Phone Number of the User
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Money of the User
        /// </summary>
        public decimal Money { get; set; }
        /// <summary>
        /// Method that calculate the money belonging to the user regarding his type.
        /// </summary>
        /// <param name="money"></param>
        public abstract void CalculatePercentage(string money);
    }
}
