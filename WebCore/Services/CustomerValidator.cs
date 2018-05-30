using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using WebCore.Models;

namespace WebCore.Services
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.Name).NotEmpty();
            RuleFor(customer => customer.Codice).NotEmpty().WithMessage("Please specify a code");
            RuleFor(customer => customer.Numero).NotEmpty().WithMessage("Specificare un numero");
            //RuleFor(customer => customer.Address).Length(20, 250);
            //RuleFor(customer => customer.Postcode).Must(BeAValidPostcode).WithMessage("Please specify a valid postcode");
        }
    }
}
