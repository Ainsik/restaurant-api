using Application.Models.Dto.User;
using FluentValidation.TestHelper;
using Infrastructure.Validations.User;
using Xunit;

namespace InfrastructureTests.Validations.User;
public class LoginValidatorTests
{
    [Fact]
    public void Validate_WithValidCommand_ShouldNotHaveValidationErrors()
    {
        //arrange
        var validator = new LoginValidator();
        var command = new LoginDto
        {
            Email = "admin@gmail.com",
            Password = "Admin01!"
        };

        //act
        var result = validator.TestValidate(command);

        //assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WithInvalidCommand_ShouldHaveValidationErrors()
    {
        //arrange
        var validator = new LoginValidator();
        var command = new LoginDto
        {
            Email = "admingmail.com",
            Password = "Admin1"
        };

        //act
        var result = validator.TestValidate(command);

        //assert
        result.ShouldHaveAnyValidationError();
    }

    [Fact]
    public void Validate_WithInvalidCommand_ShouldHaveValidationErrorForEmail()
    {
        //arrange
        var validator = new LoginValidator();
        var command = new LoginDto
        {
            Email = "admingmail.com",
            Password = "Admin01!"
        };

        //act
        var result = validator.TestValidate(command);

        //assert
        result.ShouldHaveValidationErrorFor(c => c.Email);
        result.ShouldNotHaveValidationErrorFor(c => c.Password);
    }

    [Fact]
    public void Validate_WithInvalidCommand_ShouldHaveValidationErrorForPassword()
    {
        //arrange
        var validator = new LoginValidator();
        var command = new LoginDto
        {
            Email = "admin@gmail.com",
            Password = "Admin01"
        };

        //act
        var result = validator.TestValidate(command);

        //assert
        result.ShouldHaveValidationErrorFor(c => c.Password);
        result.ShouldNotHaveValidationErrorFor(c => c.Email);

    }
}