using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using test.application.TransactionServices;
using test.application.DTO;
using test.domain;
using Nest;
using test.infraestructure.TransactionServices;
using System.Linq;

namespace webAPITransaction.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class transactionController : ControllerBase
    {
        private readonly ITransactionService _TransactionDocumentService;
        private readonly ILogger<transactionController> _logger;
        public transactionController(ITransactionService TransactionDocumentService, ILogger<transactionController> logger)
        {
            _TransactionDocumentService = TransactionDocumentService;
            _logger = logger;
        }

        [HttpPost]
        [Route("transactions")]
        public async Task<ActionResult> PostTransaction([FromBody] TransactionDTO transaction)
        {
            var result = await _TransactionDocumentService.InsertTransaction(transaction);
            return Ok("El endpoint funciona");
        }

        [HttpPut("transactions/{id}")]
        public async Task<IActionResult> UpdateTransaction(string id, TransactionDTO transaction)
        {

            var result = await _TransactionDocumentService.UpdateTransaction(id,transaction);

            if (string.IsNullOrEmpty(result))
            {
                return NotFound(new { Message = $"Transaction with id {id} not found." });
            }
            return Ok(result);
        }

        [HttpGet("transactions/{id}")]
        public async Task<ActionResult> GetTransactionById(string id)
        {
            var transaction = await _TransactionDocumentService.GetTransactionById(id);

            if (transaction is null)
            {
                return NotFound();
            }
            var result = new
            {
                Id = transaction.Id.ToString(),  // Convierte ObjectId a string para la respuesta
                transaction.Amount,
                transaction.Currency,
                transaction.Date,
                transaction.Status
            };

            return Ok(result);
        }
        [HttpGet("transactions/status/{status}")]
        public async Task<ActionResult> GetTransactionByStatus(string status)
        {
            var transaction = await _TransactionDocumentService.GetTransactionByStatus(status);

            if (!transaction.Any())
            {
                return NotFound();
            }
            var result = transaction.Select(transaction => new
            {
                Id = transaction.Id.ToString(),  // Convierte ObjectId a string para la respuesta
                transaction.Amount,
                transaction.Currency,
                transaction.Date,
                transaction.Status
            }).ToList();

            return Ok(result);
        }

    }
}
