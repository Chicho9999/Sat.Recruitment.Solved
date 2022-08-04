using Sat.Recruitment.Api.Model;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Service.Interfaces
{
    public interface IUserService
    {
        Task CreateUser(User user);
    }
}
