using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.models;

namespace backend.interfaces
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetAllTransactions();
        Task<Transaction?> GetTransaction(int Id);
        Task<Transaction> CreateTransaction(Transaction transaction);

        Task<Transaction?> UpdateTransaction(int Id);

        Task<Transaction?> DeleteTransaction(int Id);
    }
}