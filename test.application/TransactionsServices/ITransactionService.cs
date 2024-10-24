using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test.application.DTO;
using test.domain;

namespace test.application.TransactionServices
{
    public interface ITransactionService
    {

        Task<string> InsertTransaction(TransactionDTO transactionDTO);
        Task <Transaction> GetTransactionById(string id);
        Task<IEnumerable<Transaction>> GetTransactionByStatus(string status);
        Task<string> UpdateTransaction(string id, TransactionDTO transactionDTO);
    }

}
