using System;

namespace Sat.Recruitment.Api.Model
{
    public class NormalUser : User
    {
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
