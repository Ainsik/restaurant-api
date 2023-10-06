using Application.Models.Pagination;
using FluentValidation;

namespace Infrastructure.Validations.Pagination;
public class PaginationQueryValidator :AbstractValidator<PaginationQuery>
{
    private readonly int[] _allowedPageSize = new[] {5, 10, 15};
    public PaginationQueryValidator()
    {
        RuleFor(p => p.SearchPhrase)
            .MaximumLength(50).WithMessage("Search phrase can't be longer than 50 characters.");

        RuleFor(p => p.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("Page number must be equal to or greater than 1.");

        RuleFor(p => p.PageSize).Custom((value, context) =>
        {
            if (!_allowedPageSize.Contains(value))
            {
                context.AddFailure("PageSize", $"Page size must be included in [{string.Join(",", _allowedPageSize)}].");
            }
        });
    }
}