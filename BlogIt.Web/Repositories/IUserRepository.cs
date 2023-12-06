using Microsoft.AspNetCore.Identity;

namespace BlogIt.Web.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
