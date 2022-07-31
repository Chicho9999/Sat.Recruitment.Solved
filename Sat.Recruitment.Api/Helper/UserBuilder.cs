using Sat.Recruitment.Api.Model;

namespace Sat.Recruitment.Api.Helper
{
    /// <summary>
    /// Class that provide User Object Creation
    /// </summary>
    public class UserBuilder
    {
        private readonly string name;
        private readonly string email;
        private readonly string address;
        private readonly string phone;
        private readonly string money;

        public UserBuilder(string name, string email, string address, string phone, string money)
        {
            this.name = name;
            this.email = email;
            this.address = address;
            this.phone = phone;
            this.money = money;
        }

        /// <summary>
        /// Build the User Object depending the type
        /// </summary>
        /// <param name="userType"></param>
        /// <returns></returns>
        public User Build(string userType)
        {
            switch (userType)
            {
                case "Normal":
                    return new NormalUser()
                    {
                        Name = name,
                        Email = email,
                        Address = address,
                        Phone = phone,
                        Money = decimal.Parse(money)
                    };
                case "SuperUser":
                    return new SuperUser()
                    {
                        Name = name,
                        Email = email,
                        Address = address,
                        Phone = phone,
                        Money = decimal.Parse(money)
                    };
                default:
                    return new PremiunUser()
                    {
                        Name = name,
                        Email = email,
                        Address = address,
                        Phone = phone,
                        Money = decimal.Parse(money)
                    };
            }
        }
    }
}