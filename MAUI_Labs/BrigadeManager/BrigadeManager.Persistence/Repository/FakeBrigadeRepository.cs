using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BrigadeManager.Persistence.Repository
{
    public class FakeBrigadeRepository : IRepository<Brigade>
    {
        private readonly List<Brigade> _brigade;

        public FakeBrigadeRepository()
        {
            _brigade = new List<Brigade>
            {
                new()
                {
                    Id = 1,
                    Name = "First Brigade",
                    Leader = "Tyler Durden",
                    WorkersNumber = 42,
                    Works = []
                },
                new()
                {
                    Id = 2,
                    Name = "Second Brigade",
                    Leader = "The Narrator",
                    WorkersNumber = 24,
                    Works = []
                }
            };
        }

        public Task<Brigade> GetByIdAsync(int id, CancellationToken cancellationToken = default,
            params Expression<Func<Brigade, object>>[]? includesProperties)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Brigade>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => (IReadOnlyList<Brigade>)_brigade.AsReadOnly(), cancellationToken);
        }

        public Task<IReadOnlyList<Brigade>> ListAsync(Expression<Func<Brigade, bool>>? filter,
            CancellationToken cancellationToken = default,
            params Expression<Func<Brigade, object>>[]? includesProperties)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(Brigade entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Brigade entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Brigade entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Brigade> FirstOrDefaultAsync(Expression<Func<Brigade, bool>> filter,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
