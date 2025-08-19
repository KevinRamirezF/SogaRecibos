
namespace SogaRecibo.Domain.Receipts;
public sealed record ReceiptIdentifier
{
    public string Value { get; }
    private ReceiptIdentifier(string value) => Value = value;
    public static ReceiptIdentifier Create(string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) throw new ArgumentException("Empty identifier");
        var trimmed = input.Trim();
        if (trimmed.Length is < 6 or > 30) throw new ArgumentException("Identifier length out of bounds");
        return new ReceiptIdentifier(trimmed);
    }
    public override string ToString() => Value;
}
