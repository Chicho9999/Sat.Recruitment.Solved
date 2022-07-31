using System;

namespace Sat.Recruitment.Api.Model
{
    /// <summary>
    /// Super User Class
    /// </summary>
    public class SuperUser : User
    {
        /// <summary>
        /// Method that calculate the money belonging to the user regarding his type.
        /// </summary>
        /// <param name="money"></param>
        public override void CalculatePercentage(string money)
        {
            if (decimal.Parse(money) > 100)
            {
                var percentage = Convert.ToDecimal(0.20);
                var gif = decimal.Parse(money) * percentage;
                Money = Money + gif;
            }
        }
    }
}