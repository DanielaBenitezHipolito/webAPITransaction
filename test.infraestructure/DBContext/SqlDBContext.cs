using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test.application.Settings;
using test.domain;

namespace test.infraestructure.DBContext
{
    public class SqlDBContext : DbContext
    {
        private readonly SQLSettings _settings;

        public SqlDBContext(DbContextOptions<SqlDBContext> options) : base(options) { }
        public DbSet<Logs> Logs { get; set; }

       
    }
}
