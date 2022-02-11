using UJM.Models.DataBase;

namespace UJM.Repositories
{
    public interface IUJMRepository<T> : IRepository<T, UJMContext>
        where T : class
    {
    }
}
