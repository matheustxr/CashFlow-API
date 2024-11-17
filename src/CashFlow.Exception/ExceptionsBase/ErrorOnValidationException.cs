using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.API.Controllers;

public class ErrorOnValidationException : CashFlowException
{
    public List<string> Errors { get; set; }

    public ErrorOnValidationException(List<string> errorMessages) : base(string.Empty)
    {
        Errors = errorMessages;
    }
}
