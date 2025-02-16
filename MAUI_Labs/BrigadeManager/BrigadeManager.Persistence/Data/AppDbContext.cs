﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrigadeManager.Persistence.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Work> WorkS { get; }
        public DbSet<Brigade> Brigades { get; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
