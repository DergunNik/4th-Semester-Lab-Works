using BrigadeManager.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrigadeManager.Persistence.UnitOfWork
{
    public class FakeUnitOfWork : IUnitOfWork
    {
        private readonly FakeBrigadeRepository _fakeBrigadeRepository = new();
        private readonly FakeWorkRepository _fakeWorkRepository = new();

        public IRepository<Work> WorkRepository => _fakeWorkRepository;
        public IRepository<Brigade> BrigadeRepository => _fakeBrigadeRepository;

        public Task SaveAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteDataBaseAsync()
        {
            throw new NotImplementedException();
        }

        public Task CreateDataBaseAsync()
        {
            throw new NotImplementedException();
        }
    }
}
