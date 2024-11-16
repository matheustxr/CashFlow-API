using CashFlow.Domain.Entities;
using System.Xml.Serialization;

namespace CashFlow.Domain.Repositories.Expenses;
public interface IExpensesRepository
{
    void Add(Expense expense);
}
