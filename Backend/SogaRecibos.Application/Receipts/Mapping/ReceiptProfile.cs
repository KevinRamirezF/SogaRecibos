using AutoMapper;
using SogaRecibos.Application.Receipts.Dtos;
using SogaRecibos.Domain.Receipts;

namespace SogaRecibos.Application.Receipts.Mapping;

public sealed class ReceiptProfile : Profile
{
    public ReceiptProfile()
    {
        CreateMap<Receipt, ReceiptDto>()
            .ForMember(d => d.Service, m => m.MapFrom(s => s.Service.ToString()))
            .ForMember(d => d.Alias, m => m.MapFrom(s => s.Alias))
            .ForMember(d => d.CreatedAt, m => m.MapFrom(d => d.CreatedAt))
            .ForMember(d => d.Identifier, m => m.MapFrom(s => s.Identifier.Value));
    }
}
