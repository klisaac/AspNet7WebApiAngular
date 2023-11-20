﻿using AspNet7.Application.Queries;
using FluentValidation;

namespace AspNet7.Application.Validations
{
    public class SearchArgsValidator : AbstractValidator<PageSearchArgs>
    {
        public SearchArgsValidator()
        {
            RuleFor(request => request.Args).NotNull();
            RuleFor(request => request.Args.PageIndex).GreaterThan(0);
            RuleFor(request => request.Args.PageSize).InclusiveBetween(10, 100);
        }
    }
}
