using System;

namespace Sat.Recruitment.Api.Model
{
    /// <summary>
    /// Normal User Class
    /// </summary>
    public class NormalUser : User
    {
        /// <summary>
        /// Method that calculate the money belonging to the user regarding his type.
        /// </summary>
        /// <param name="money"></param>
        public override void CalculatePercentage(string money)
        {
            if (decimal.Parse(money) > 100)
            {
                var percentage = Convert.ToDecimal(0.12);
                //If new user is normal and has more than USD100
                var gif = decimal.Parse(money) * percentage;
                Money = Money + gif;
            }
            if (decimal.Parse(money) < 100 && decimal.Parse(money) > 10)
            {
                var percentage = Convert.ToDecimal(0.8);
                var gif = decimal.Parse(money) * percentage;
                Money = Money + gif;
            }
        }
    }
}