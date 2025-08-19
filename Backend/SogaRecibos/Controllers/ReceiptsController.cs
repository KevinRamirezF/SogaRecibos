using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SogaRecibos.Application.Receipts.Commands;
using SogaRecibos.Application.Receipts.Delete;
using SogaRecibos.Application.Receipts.Dtos;
using SogaRecibos.Application.Receipts.Factories;
using SogaRecibos.Application.Receipts.Queries;
using SogaRecibos.Domain.Services;
using SogaRecibo.Domain.Receipts;

namespace SogaRecibos.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ReceiptsController : ControllerBase
{
    private readonly ICreateReceiptHandler _create;
    private readonly IListReceiptsHandler _list;
    private readonly IDeleteReceiptHandler _delete;
    private readonly IReceiptValidatorFactory _validators;
    private readonly IRedirectUrlBuilderFactory _redirectors;
    private readonly IValidateReceiptHandler _validateReceiptHandler;
    private readonly IRedirectToPayHandler _redirectToPayHandler;
    private readonly IMapper _mapper;

    public ReceiptsController(
        ICreateReceiptHandler create, IListReceiptsHandler list, IDeleteReceiptHandler delete,
        IReceiptValidatorFactory validators, IRedirectUrlBuilderFactory redirectors,
        IValidateReceiptHandler validateReceiptHandler, IRedirectToPayHandler redirectToPayHandler,
        IMapper mapper)
    {
        _create = create;
        _list = list;
        _delete = delete;
        _validators = validators;
        _redirectors = redirectors;
        _validateReceiptHandler = validateReceiptHandler;
        _redirectToPayHandler = redirectToPayHandler;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ReceiptDto>>> Get(CancellationToken ct)
    {
        var data = await _list.HandleAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<ReceiptDto>>(data));
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateReceiptCommand body, CancellationToken ct)
    {
        var id = await _create.HandleAsync(body, ct);
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _delete.HandleAsync(id, ct);
        return NoContent();
    }

    [HttpGet("validate")]
    public async Task<ActionResult<object>> Validate([FromQuery] ValidateReceiptQuery query, CancellationToken ct)
    {
        var result = await _validateReceiptHandler.HandleAsync(query, ct);
        return Ok(new { status = result.IsValid ? "valid" : "invalid", reason = result.Reason });
    }

    [HttpPost("pay/redirect")]
    public IActionResult RedirectToPay([FromBody] RedirectToPayCommand body)
    {
        var url = _redirectToPayHandler.Handle(body);
        return Redirect(url); // 302
    }
}
