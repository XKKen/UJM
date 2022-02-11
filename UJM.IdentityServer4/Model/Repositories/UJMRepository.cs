using UJM.IdentityServer4.Model.DataBase;
using UJM.Repository.EFCore.Interface.Implementation;

namespace UJM.IdentityServer4.Model.Repositories
{
    public class UJMRepository<T> : Repository<T, AuthorzeContext>, IUJMRepository<T>
       where T : class
    {

        public UJMRepository(AuthorzeContext dbContext) : base(dbContext)
        {
        }
    }
}
