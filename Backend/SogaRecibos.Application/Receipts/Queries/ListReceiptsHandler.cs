using SogaRecibos.Application.Abstractions.Auth;
using SogaRecibos.Application.Abstractions.Persistence;
using SogaRecibos.Application.Receipts.Specs;
using SogaRecibos.Application.Users.Specs;
using SogaRecibos.Domain.Receipts;
using SogaRecibos.Domain.Users;

namespace SogaRecibos.Application.Receipts.Queries;

public sealed class ListReceiptsHandler : IListReceiptsHandler
{
    private readonly IRepository<Receipt> _receiptRepo;
    private readonly IRepository<User> _userRepo;
    private readonly IUserResolver _userResolver;

    public ListReceiptsHandler(IRepository<Receipt> receiptRepo, IRepository<User> userRepo, IUserResolver userResolver)
    {
        _receiptRepo = receiptRepo;
        _userRepo = userRepo;
        _userResolver = userResolver;
    }

    public async Task<IReadOnlyList<Receipt>> HandleAsync(CancellationToken ct)
    {
        var currentUser = _userResolver.GetCurrentUser();
        var user = await _userRepo.FirstOrDefaultAsync(new UserByExternalIdSpec(currentUser.ExternalId), ct);

        if (user is null)
            return Array.Empty<Receipt>();

        return await _receiptRepo.ListAsync(new ReceiptByOwnerSpec(user.Id), ct);
    }
}
