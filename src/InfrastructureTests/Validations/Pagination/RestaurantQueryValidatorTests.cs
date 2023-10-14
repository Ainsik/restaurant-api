using Application.Models.Pagination;
using Core.Entities;
using FluentValidation.TestHelper;
using Infrastructure.Validations.Pagination;
using Xunit;

namespace InfrastructureTests.Validations.Pagination;
public class RestaurantQueryValidatorTests
{
    public static IEnumerable<object[]> GetSampleValidData()
    {
        var list = new List<PaginationQuery>()
            {
                new PaginationQuery()
                {
                    PageNumber = 1,
                    PageSize = 10
                },
                new PaginationQuery()
                {
                    PageNumber = 2,
                    PageSize = 15
                },
                new PaginationQuery()
                {
                    PageNumber = 22,
                    PageSize = 5,
                    SortBy = nameof(Restaurant.Name)
                },
                new PaginationQuery()
                {
                    PageNumber = 12,
                    PageSize = 15,
                    SortBy = nameof(Restaurant.Category)
                }
            };

        return list.Select(q => new object[] { q });
    }

    public static IEnumerable<object[]> GetSampleInvalidData()
    {
        var list = new List<PaginationQuery>()
            {
                new PaginationQuery()
                {
                    PageNumber = 0,
                    PageSize = 10
                },
                new PaginationQuery()
                {
                    PageNumber = 2,
                    PageSize = 13
                },
                new PaginationQuery()
                {
                    PageNumber = 22,
                    PageSize = 5,
                    SortBy = nameof(Restaurant.ContactEmail)
                },
                new PaginationQuery()
                {
                    PageNumber = 12,
                    PageSize = 15,
                    SortBy = nameof(Restaurant.ContactNumber)
                }
            };

        return list.Select(q => new object[] { q });
    }


    [Theory]
    [MemberData(nameof(GetSampleValidData))]
    public void Validate_ForCorrectModel_ReturnsSuccess(PaginationQuery model)
    {
        // arrange
        var validator = new PaginationQueryValidator();


        // act
        var result = validator.TestValidate(model);

        // assert
        result.ShouldNotHaveAnyValidationErrors();
    }


    [Theory]
    [MemberData(nameof(GetSampleInvalidData))]
    public void Validate_ForIncorrectModel_ReturnsFailure(PaginationQuery model)
    {
        // arrange
        var validator = new PaginationQueryValidator();


        // act
        var result = validator.TestValidate(model);

        // assert
        result.ShouldHaveAnyValidationError();
    }
}