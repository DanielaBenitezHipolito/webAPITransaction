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
        Task <string> GetTransactionById(TransactionDTO transactionDTO);
        Task <string> GetTransactionByStatus(TransactionDTO transactionDTO);
        Task<string> UpdateTransaction(string id, TransactionDTO transactionDTO);
    }

}
