using SogaRecibo.Domain.Receipts;
using SogaRecibos.Application.Abstractions.Auth;
using SogaRecibos.Application.Abstractions.Persistence;
using SogaRecibos.Application.Receipts.Factories;
using SogaRecibos.Application.Receipts.Specs;
using SogaRecibos.Application.Users.Specs;
using SogaRecibos.Domain.Receipts;
using SogaRecibos.Domain.Users;

namespace SogaRecibos.Application.Receipts.Commands;

public sealed class CreateReceiptHandler : ICreateReceiptHandler
{
    private readonly IRepository<Receipt> _receiptRepo;
    private readonly IRepository<User> _userRepo;
    private readonly IUnitOfWork _uow;
    private readonly IReceiptValidatorFactory _validatorFactory;
    private readonly IUserResolver _userResolver;

    public CreateReceiptHandler(
        IRepository<Receipt> receiptRepo,
        IRepository<User> userRepo,
        IUnitOfWork uow,
        IReceiptValidatorFactory validatorFactory,
        IUserResolver userResolver)
    {
        _receiptRepo = receiptRepo;
        _userRepo = userRepo;
        _uow = uow;
        _validatorFactory = validatorFactory;
        _userResolver = userResolver;
    }

    public async Task<Guid> HandleAsync(CreateReceiptCommand cmd, CancellationToken ct)
    {
        var currentUser = _userResolver.GetCurrentUser();
        var userId = await GetOrCreateUserAsync(currentUser, ct);

        var validator = _validatorFactory.GetValidatorByServiceProvider(cmd.Service);
        var result = await validator.ValidateAsync(cmd.Identifier, ct);
        if (!result.IsValid)
            throw new ArgumentException($"Invalid identifier: {result.Reason}");

        var identifier = ReceiptIdentifier.Create(cmd.Identifier);
        var dupSpec = new ReceiptByOwnerServiceAndIdentifierSpec(userId, cmd.Service, identifier.Value);
        if (await _receiptRepo.AnyAsync(dupSpec, ct))
            throw new InvalidOperationException("Duplicate receipt");

        var receipt = Receipt.Create(userId, cmd.Service, identifier, cmd.Alias);
        await _receiptRepo.AddAsync(receipt, ct);
        await _uow.SaveChangesAsync(ct);
        return receipt.Id;
    }

    private async Task<Guid> GetOrCreateUserAsync(CurrentUser currentUser, CancellationToken ct)
    {
        var user = await _userRepo.FirstOrDefaultAsync(new UserByExternalIdSpec(currentUser.ExternalId), ct);

        if (user is not null)
            return user.Id;

        var newUser = User.Create(currentUser.ExternalId, currentUser.Email);
        await _userRepo.AddAsync(newUser, ct);
        // Note: SaveChangesAsync will be called by the main handler logic

        return newUser.Id;
    }
}