using SogaRecibos.Application.Abstractions.Auth;
using SogaRecibos.Application.Abstractions.Persistence;
using SogaRecibos.Application.Receipts.Queries;
using SogaRecibos.Application.Receipts.Specs;
using SogaRecibos.Application.Users.Specs;
using SogaRecibos.Domain.Receipts;
using SogaRecibos.Domain.Users;

namespace SogaRecibos.Application.Receipts.Delete;

public sealed class DeleteReceiptHandler : IDeleteReceiptHandler
{
    private readonly IRepository<Receipt> _receiptRepo;
    private readonly IRepository<User> _userRepo;
    private readonly IUnitOfWork _uow;
    private readonly IUserResolver _userResolver;

    public DeleteReceiptHandler(IRepository<Receipt> receiptRepo, IRepository<User> userRepo, IUnitOfWork uow, IUserResolver userResolver)
    {
        _receiptRepo = receiptRepo;
        _userRepo = userRepo;
        _uow = uow;
        _userResolver = userResolver;
    }

    public async Task HandleAsync(Guid receiptId, CancellationToken ct)
    {
        var currentUser = _userResolver.GetCurrentUser();
        var user = await _userRepo.FirstOrDefaultAsync(new UserByExternalIdSpec(currentUser.ExternalId), ct);

        if (user is null)
            throw new KeyNotFoundException("User not found, cannot delete receipt.");

        var spec = new ReceiptByOwnerSpec(user.Id);
        var receipts = await _receiptRepo.ListAsync(spec, ct);
        var receiptToDelete = receipts.FirstOrDefault(r => r.Id == receiptId);

        if (receiptToDelete is null)
            throw new KeyNotFoundException("Receipt not found or you don't have permission to delete it.");

        await _receiptRepo.DeleteAsync(receiptToDelete, ct);
        await _uow.SaveChangesAsync(ct);
    }
}