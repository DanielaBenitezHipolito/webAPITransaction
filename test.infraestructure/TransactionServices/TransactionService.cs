using MongoDB.Bson;
using test.application.TransactionServices;
using test.application.DTO;
using test.domain;
using test.domain.interfaces;
using test.application.Commun;
using Nest;

namespace test.infraestructure.TransactionServices
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepositoryMongo<Transaction> _transaction;
        private readonly IRepositorySQL<Logs> _contextLogs;
        public TransactionService(IRepositoryMongo<Transaction> transaction, IRepositorySQL<Logs> contextLogs)
        {
            _transaction = transaction;
            _contextLogs = contextLogs;
        }
        public async Task<string> InsertTransaction(TransactionDTO transactionDTO)
        {
            var log = new Logs();
            try
            {
                Transaction transaction = new Transaction();
                transaction.Id = new ObjectId();
                transaction.Amount = transactionDTO.Amount;
                transaction.Currency = transactionDTO.Currency;
                transaction.Date = transactionDTO.Date;
                transaction.Status = transactionDTO.Status;

                log.MessageLog = $"insert transaction Id:{transaction.Id}" +
                    $" Amount : {transaction.Amount}" +
                    $" Currency: {transaction.Currency}" +
                    $" Date: {transaction.Date}" +
                    $" Status: {transaction.Status}";
                log.LogLevel = LogLevels.Success200.ToString();

                await _transaction.AddAsync(transaction);
                log.DateLog = DateTime.Now;
                await _contextLogs.AddAsync(log);
                return "Se ha insertado la información con exito";
            }
            catch(Exception ex)
            {
                log.MessageLog = $"Error: {ex.Message}";
                log.LogLevel = LogLevels.Error500.ToString();
                return "";
            }
            finally
            {

            }
        }
        public async Task<string> UpdateTransaction(string id, TransactionDTO transactionDTO)
        {
            if (string.IsNullOrEmpty(id) || id.Length != 24 || !ObjectId.TryParse(id, out var objectId))
            {
                return null;
            }
            var result = await _transaction.GetByIdAsync(id);
            if (result == null)
            {
                return null;
            }
                result.Id = ObjectId.Parse(id);
                result.Amount = transactionDTO.Amount;
                result.Currency = transactionDTO.Currency;
                result.Date = transactionDTO.Date;
                result.Status = transactionDTO.Status;

            await _transaction.UpdateAsync(id, result);
            return "Se actualizo el registro correctamente";
        }

        public async Task<Transaction> GetTransactionById(string id)
        {
            if (string.IsNullOrEmpty(id) || id.Length != 24 || !ObjectId.TryParse(id, out var objectId))
            {
                return null;
            }
            var transaction = await _transaction.GetByIdAsync(id);
            if (transaction == null)
            {
                throw new KeyNotFoundException($"Transaction with id {id} not found");
            }
            
            return transaction;
        }
        public async Task<IEnumerable<Transaction>> GetTransactionByStatus(string status)
        {
            var transaction = await _transaction.GetByStatusAsync(status);
            if (!transaction.Any())
            {
                return Enumerable.Empty<Transaction>();
            }

            return transaction;
        }

    }
}
