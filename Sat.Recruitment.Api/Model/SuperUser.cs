using System;

namespace Sat.Recruitment.Api.Model
{
    public class SuperUser : User
    {
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
