using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using test.application.TransactionServices;
using test.application.DTO;
using test.domain;

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
        [Route("transactionsPOST")]
        public ActionResult PostTransaction([FromBody] TransactionDTO transaction)
        {
            var result = _TransactionDocumentService.InsertTransaction(transaction);
            return Ok("El endpoint funciona");
        }

        [HttpPut("/{id}")]
        public string Put()
        {
            return "El endpoint funciona";
        }

        [HttpGet("/{id}")]
        public string Get()
        {
            return "El endpoint funciona";
        }
        //[HttpGet("/{status}")]
        //public string Get()
        //{
        //    return "El endpoint funciona";
        //}

    }
}
