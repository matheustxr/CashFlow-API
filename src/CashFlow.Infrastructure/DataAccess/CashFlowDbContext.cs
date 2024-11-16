using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess;

internal class CashFlowDbContext : DbContext
{
    public DbSet<Expense> Expenses { get; set; } //aqui eu seto uma tabela que desejo usar dentro do meu db

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Server=localhost;Database=cashflow_db;Uid=root; Pwd=Minecr@ft0";

        var version = new Version(8, 0, 40);
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 40));

        optionsBuilder.UseMySql(connectionString, serverVersion);
    }
}
