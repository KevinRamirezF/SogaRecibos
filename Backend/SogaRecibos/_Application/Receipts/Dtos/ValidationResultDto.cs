namespace SogaRecibos.Application.Receipts.Dtos;

public class ValidationResultDto
{
    public bool IsValid { get; set; }
    public string? Reason { get; set; }
}
