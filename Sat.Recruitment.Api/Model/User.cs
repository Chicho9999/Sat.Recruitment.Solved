namespace Sat.Recruitment.Api.Model
{
    public abstract class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public decimal Money { get; set; }
        public abstract void CalculatePercentage(string money);
    }
}
