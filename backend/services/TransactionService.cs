using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.data;
using backend.interfaces;
using backend.models;
using Microsoft.EntityFrameworkCore;

namespace backend.services
{
    public class TransactionService(ApplicationDBContext _applicationDBContext) : ITransactionService
    {
        private readonly ApplicationDBContext applicationDBContext = _applicationDBContext;
        public async Task<Transaction> CreateTransaction(Transaction transaction)
        {
            await applicationDBContext.Transactions.AddAsync(transaction);
            await applicationDBContext.SaveChangesAsync();
            return transaction;
        }

        public async Task<Transaction?> DeleteTransaction(int Id)
        {
            var transaction = await applicationDBContext.Transactions.FirstOrDefaultAsync(x => x.Id == Id);
            if(transaction == null){
                return null;
            }
            applicationDBContext.Transactions.Remove(transaction);
            return transaction;
        }

        public async Task<List<Transaction>> GetAllTransactions()
        {
            return await applicationDBContext.Transactions.ToListAsync();
        }

        public async Task<Transaction?> GetTransaction(int Id)
        {
            return await applicationDBContext.Transactions.FindAsync(Id);
        }

        public Task<Transaction?> UpdateTransaction(int Id)
        {
            throw new NotImplementedException();
        }
    }
}