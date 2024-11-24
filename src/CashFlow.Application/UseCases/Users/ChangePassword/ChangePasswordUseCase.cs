using CashFlow.Communication.Requests;
using CashFlow.Domain.Repositories.User;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Services.LoggedUser;
using FluentValidation.Results;
using CashFlow.Exception;
using CashFlow.API.Controllers;

namespace CashFlow.Application.UseCases.Users.ChangePassword;
public class ChangePasswordUseCase : IChangePasswordUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserUpdateOnlyRepository _repository;
    private readonly IUnityOfWork _unityOfWork;
    private readonly IPasswordEncripter _passwordEncripter;

    public ChangePasswordUseCase(
        ILoggedUser loggedUser,
        IPasswordEncripter passwordEncripter,
        IUserUpdateOnlyRepository repository,
        IUnityOfWork unityOfWork)
    {
        _loggedUser = loggedUser;
        _repository = repository;
        _unityOfWork = unityOfWork;
        _passwordEncripter = passwordEncripter;
    }

    public async Task Execute(RequestChangePasswordJson request)
    {
        var loggedUser = await _loggedUser.Get();

        Validate(request, loggedUser);

        var user = await _repository.GetById(loggedUser.Id);
        user.Password = _passwordEncripter.Encrypt(request.NewPassword);

        _repository.Update(user);

        await _unityOfWork.Commit();
    }

    private void Validate(RequestChangePasswordJson request, Domain.Entities.User loggedUser)
    {
        var validator = new ChangePasswordValidator();
        
        var result = validator.Validate(request);

        var passwordMatch = _passwordEncripter.Verify(request.Password, loggedUser.Password);

        if (passwordMatch == false)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.PASSWORD_DIFFERENT_CURRENT_PASSWORD));
        }

        if (result.IsValid == false)
        {
            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errors);
        }
    }
}
