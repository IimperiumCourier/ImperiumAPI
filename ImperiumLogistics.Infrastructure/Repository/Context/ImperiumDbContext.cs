using ImperiumLogistics.Domain.CompanyAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Repository.Context
{
    public class ImperiumDbContext : DbContext
    {
        public ImperiumDbContext(DbContextOptions<ImperiumDbContext> options)
          : base(options)
        {

        }

        public DbSet<Company> Company { get; set; }
    }
}
