namespace Sat.Recruitment.Api.Model
{
    public class PremiunUser : User
    {
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
