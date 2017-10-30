using System.Threading.Tasks;

namespace hwapp.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}