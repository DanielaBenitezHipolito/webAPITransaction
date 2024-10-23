using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test.application.TransactionServices;
using test.application.DTO;
using test.domain;
using test.domain.interfaces;

namespace test.infraestructure.TransactionServices
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepositoryMongo<Transaction> _transaction;
        public TransactionService(IRepositoryMongo<Transaction> transaction) {
            _transaction = transaction;
        }
        public string InsertTransaction(TransactionDTO transactionDTO)
        {
            try
            {
                Transaction transaction = new Transaction();
                transaction.Id = new ObjectId();
                transaction.Amount = transactionDTO.Amount;
                transaction.Currency = transactionDTO.Currency;
                transaction.Date = transactionDTO.Date;
                transaction.Status = transactionDTO.Status;
                var result = _transaction.AddAsync(transaction);
                return "Se ha insertado la información con exito";
            }
            catch
            {
                return "";
            }
        }
        public string GetTransactionById(TransactionDTO body)
        {
            throw new NotImplementedException();
        }

        public string GetTransactionByStatus(TransactionDTO body)
        {
            throw new NotImplementedException();
        }

        

        public string UpdateTransaction(TransactionDTO body)
        {
            throw new NotImplementedException();
        }
    }
}
