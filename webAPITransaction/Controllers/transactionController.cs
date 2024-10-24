using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using test.application.TransactionServices;
using test.application.DTO;
using test.domain;
using Nest;
using test.infraestructure.TransactionServices;
using System.Linq;
using test.infraestructure.AuthService;
using Microsoft.AspNetCore.Authorization;
using MongoDB.Bson;
using Elasticsearch.Net;

namespace webAPITransaction.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class transactionController : ControllerBase
    {
        private readonly ITransactionService _TransactionDocumentService;
        private readonly AuthService _authService;
        public transactionController(ITransactionService TransactionDocumentService, AuthService authService)
        {
            _TransactionDocumentService = TransactionDocumentService;
            _authService = authService;
        }
        [HttpPost]
        [Route("authentication")]
        public async Task<ActionResult> PostToken([FromBody] UserDTO userDTO)
        {
            var result = _authService.Authenticate(userDTO.User , userDTO.password);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        [Route("transactions")]

        public async Task<ActionResult> PostTransaction([FromBody] TransactionDTO transaction)
        {
            var result = await _TransactionDocumentService.InsertTransaction(transaction);
            return Ok("Se ha insertado la información con exito");
        }

        [HttpPut("transactions/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateTransaction(string id, TransactionDTO transaction)
        {
            var result = await _TransactionDocumentService.UpdateTransaction(id, transaction);

            if (result == null) 
            {
                return NotFound(new { Message = $"Transacción con id {id} no encontrada." });
            }

            return Ok("Transacción actualizada exitosamente." );

        }

        [HttpGet("transactions/{id}")]
        [Authorize]
        public async Task<ActionResult> GetTransactionById(string id)
        {
            var transaction = await _TransactionDocumentService.GetTransactionById(id);

            if (transaction is null)
            {
                return NotFound(new { Message = $"Transacción con id {id} no encontrada." });
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
        [Authorize]
        public async Task<ActionResult> GetTransactionByStatus(string status)
        {
            var transaction = await _TransactionDocumentService.GetTransactionByStatus(status);

            if (!transaction.Any())
            {
                return NotFound("No se encontraron transacciones con el estado enviado");
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
