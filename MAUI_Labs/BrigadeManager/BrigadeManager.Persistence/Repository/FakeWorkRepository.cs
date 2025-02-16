using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BrigadeManager.Persistence.Repository
{
    public class FakeWorkRepository : IRepository<Work>
    {
        private readonly List<Work> _works = new();

        public FakeWorkRepository()
        {
            int k = 1;
            Random random = new Random();
            for (int i = 1; i <= 2; i++)
                for (int j = 0; j < 10; j++)
                {
                    var id = i;
                    _works.Add(new Work()
                    {
                        Name = $"Work {id}",
                        Id = id,
                        Rating = i * 5 - j,
                        StartDate = new DateTime(2025, i, j + random.Next(1, 10)),
                        EndDate = new DateTime(2025, i + random.Next(1, 6), j + random.Next(1, 10)),
                        BrigadeId = i,
                        ImageSrc = "default_image.jpg"
                    });
                }
        }

        public Task<Work> GetByIdAsync(int id, CancellationToken cancellationToken = default,
            params Expression<Func<Work, object>>[]? includesProperties)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Work>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Work>> ListAsync(Expression<Func<Work, bool>>? filter,
            CancellationToken cancellationToken = default,
            params Expression<Func<Work, object>>[]? includesProperties)
        {
            IEnumerable<Work> query = _works; 
            if (filter != null)
            {
                query = query.Where(filter.Compile());
            }
            return await Task.FromResult(query.ToList());
        }

        public async Task AddAsync(Work entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Work entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Work entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Work> FirstOrDefaultAsync(Expression<Func<Work, bool>> filter,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
