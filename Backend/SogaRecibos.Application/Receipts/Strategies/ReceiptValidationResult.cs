using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogaRecibos.Application.Receipts.Strategies
{
    public class ReceiptValidationResult
    {
        public bool IsValid { get; set; }
        public string Reason { get; set; } = string.Empty;
        public ReceiptValidationResult(bool isValid, string reason)
        {
            IsValid = isValid;
            Reason = reason;
        }
    }
}
