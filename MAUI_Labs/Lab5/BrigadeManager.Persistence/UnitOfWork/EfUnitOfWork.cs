using BrigadeManager.Persistence.Data;
using BrigadeManager.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrigadeManager.Persistence.UnitOfWork
{
    public class EfUnitOfWork(AppDbContext context) : IUnitOfWork
    {
        private readonly AppDbContext _context = context;
        private readonly Lazy<IRepository<Brigade>> _brigadeRepository = new Lazy<IRepository<Brigade>>(() => 
                                                                            new EfRepository<Brigade>(context));
        private readonly Lazy<IRepository<Work>> _workRepository = new Lazy<IRepository<Work>>(() =>
                                                                            new EfRepository<Work>(context));

        public IRepository<Brigade> BrigadeRepository => _brigadeRepository.Value;
        public IRepository<Work> WorkRepository => _workRepository.Value;

        public async Task CreateDataBaseAsync() => await _context.Database.EnsureCreatedAsync();

        public async Task DeleteDataBaseAsync() => await _context.Database.EnsureDeletedAsync();

        public async Task SaveAllAsync() => await _context.SaveChangesAsync();
    }
}
