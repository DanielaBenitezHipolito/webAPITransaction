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
        string InsertTransaction(TransactionDTO body);
        string UpdateTransaction(TransactionDTO body);
        string GetTransactionById(TransactionDTO body);
        string GetTransactionByStatus(TransactionDTO body);
    }

}
