namespace Sat.Recruitment.Api.Model
{
    /// <summary>
    /// Premiun User Class
    /// </summary>
    public class PremiunUser : User
    {
        /// <summary>
        /// Method that calculate the money belonging to the user regarding his type.
        /// </summary>
        /// <param name="money"></param>
        public override void CalculatePercentage(string money)
        {
            if (decimal.Parse(money) > 100)
            {
                var gif = decimal.Parse(money) * 2;
                Money = Money + gif;
            }
        }
    }
}