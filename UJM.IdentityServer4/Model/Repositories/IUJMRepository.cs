using UJM.IdentityServer4.Model.DataBase;
using UJM.Repository.EFCore.Interface.Contracts;

namespace UJM.IdentityServer4.Model.Repositories
{
    public interface IUJMRepository<T> : IRepository<T, AuthorzeContext>
     where T : class
    {
    }
}
