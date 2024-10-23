using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using test.application.DTO;
using test.application.Settings;
using test.domain;

namespace test.infraestructure.DBContext
{

    public class SqlDBContext : DbContext
    {
        private readonly string _connectionString;

        public SqlDBContext(DbContextOptions<SqlDBContext> options/*, IOptions<SQLConnection> settings*/)
            : base(options)
        {
            //_connectionString = settings.Value.ConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseSqlServer(_connectionString);
            //}
        }

        public DbSet<Logs> Logs { get; set; }
    }

}