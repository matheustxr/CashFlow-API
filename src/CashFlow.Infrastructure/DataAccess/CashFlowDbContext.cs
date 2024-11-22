using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess;
public class CashFlowDbContext : DbContext
{
    public CashFlowDbContext (DbContextOptions options) : base(options) { }
    public DbSet<Expense> Expenses { get; set; } //aqui eu seto uma tabela que desejo usar dentro do meu db
    public DbSet<User> Users { get; set; }
}
