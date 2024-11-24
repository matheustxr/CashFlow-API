using CashFlow.Application.UseCases.Expenses;
using CashFlow.Communication.Enums;
using CashFlow.Exception;
using CommonTestUnilities.Requests;
using FluentAssertions;

namespace Validators.Tests.Expenses;

public class ExpenseValidatorTests
{
    [Fact]
    public void Sucess()
    {
        //Arange
        var validator = new ExpenseValidator();
        var request = RequestExpenseJsonBuilder.Build();

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData(null)]
    public void Error_Title_Empty(string title)
    {
        //Arange
        var validator = new ExpenseValidator();
        var request = RequestExpenseJsonBuilder.Build();
        request.Title = title;

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.TITLE_REQUIRED));
    }

    [Fact]
    public void Error_Date_Future()
    {
        //Arange
        var validator = new ExpenseValidator();
        var request = RequestExpenseJsonBuilder.Build();
        request.Date = DateTime.UtcNow.AddDays(1);

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.DATE_MUST_TODAY));
    }

    [Fact]
    public void Error_Payment_Type_Invalid()
    {
        //Arange
        var validator = new ExpenseValidator();
        var request = RequestExpenseJsonBuilder.Build();
        request.PaymentType = (PaymentType)700;

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.PAYMENT_TYPE_INVALID));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-3)]
    public void Error_Amount_Invalid(decimal amount)
    {
        //Arange
        var validator = new ExpenseValidator();
        var request = RequestExpenseJsonBuilder.Build();
        request.Amount = amount;

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.AMOUNT_GREATER_ZERO));
    }
}
