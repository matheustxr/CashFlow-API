using CashFlow.Application.UseCases.Expenses.Register;
using CommonTestUnilities.Requests;
using FluentValidation;

namespace Validators.Tests.Expenses.Register;

public class RegisterExpenseValidatorTests
{
    [Fact]
    public void Sucess()
    {
        //Arange
        var validator = new RegisterExpenseValidator();

        var request = RequestRegisterExpenseJsonBuilder.Build();

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.True(result.IsValid);
    }
}
