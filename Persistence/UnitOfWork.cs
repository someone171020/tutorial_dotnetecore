using System.Threading.Tasks;
using hwapp.Core;

namespace hwapp.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HelloDbContext context;
        public UnitOfWork(HelloDbContext context)
        {
            this.context = context;
        }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}