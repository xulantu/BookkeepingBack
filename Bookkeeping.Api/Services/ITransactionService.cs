using Bookkeeping.Api.Models;

namespace Bookkeeping.Api.Services;

public interface ITransactionService
{
    Task AddTransactionAsync(TransactionDto dto);
}