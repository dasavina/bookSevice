using FluentValidation;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validation
{
    public class BookDtoValidator : AbstractValidator<BookDto>
    {
        public BookDtoValidator()
        {
            RuleFor(book => book.Title)
                .NotEmpty().WithMessage("Title is required.")
                .Length(1, 200).WithMessage("Title must be between 1 and 200 characters.");

            RuleFor(book => book.ISBN)
                .Matches(@"^\d{3}-\d{10}$").WithMessage("Invalid ISBN format. Correct format is xxx-xxxxxxxxxx.");

            RuleFor(book => book.PublishedDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Published date cannot be in the future.");

            RuleFor(book => book.AuthorId)
                .NotEmpty().WithMessage("Author is required.");
        }
    }
}
