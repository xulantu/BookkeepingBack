using Microsoft.Data.SqlClient;
using System.Data;
using Bookkeeping.Api.Models;

namespace Bookkeeping.Api.Services;

public class TransactionService : ITransactionService
{
    private readonly IConfiguration _config;

    public TransactionService(IConfiguration config)
    {
        _config = config;
    }

    public async Task AddTransactionAsync(TransactionDto dto)
    {
        var cs = _config.GetConnectionString("DefaultConnection");
        await using var conn = new SqlConnection(cs);
        await conn.OpenAsync();

        const string sql = @"
            INSERT INTO dbo.Transactions 
                (TxnDate, Merchant, Category, Amount, PaymentMethod)
            VALUES 
                (@TxnDate, @Merchant, @Category, @Amount, @PaymentMethod)";

        await using var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.Add("@TxnDate", SqlDbType.Date).Value = dto.TxnDate.Date;
        cmd.Parameters.Add("@Merchant", SqlDbType.NVarChar, 200).Value = dto.Merchant;
        cmd.Parameters.Add("@Category", SqlDbType.NVarChar, 100).Value = dto.Category;
        cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = dto.Amount;
        cmd.Parameters.Add("@PaymentMethod", SqlDbType.NVarChar, 50).Value = dto.PaymentMethod;

        await cmd.ExecuteNonQueryAsync();
    }
}