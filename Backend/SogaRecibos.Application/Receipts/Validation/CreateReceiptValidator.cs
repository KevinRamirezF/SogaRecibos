using FluentValidation;
using SogaRecibos.Application.Receipts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogaRecibos.Application.Receipts.Validation
{
    public sealed class CreateReceiptValidator : AbstractValidator<CreateReceiptCommand>
    {
        public CreateReceiptValidator()
        {
            RuleFor(x => x.Identifier).NotEmpty().MaximumLength(64);
            RuleFor(x => x.Alias).MaximumLength(64);
            RuleFor(x => x.Service).IsInEnum();
        }
    }
}
