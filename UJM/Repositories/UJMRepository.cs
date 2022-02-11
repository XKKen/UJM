using UJM.Models.DataBase;

namespace UJM.Repositories
{
    public class UJMRepository<T> : Repository<T, UJMContext>, IUJMRepository<T>
        where T : class
    {

        public UJMRepository(UJMContext dbContext) : base(dbContext)
        {
        }
    }
}
