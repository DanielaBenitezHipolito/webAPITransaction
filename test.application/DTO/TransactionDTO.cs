using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.application.DTO
{
    public class TransactionDTO
    {

        public ObjectId Id { get; set;}
        public decimal Amount { get; set;}
        public string Currency { get; set; }
        public DateTime Date { get; set;}
        public string Status {  get; set; }
    }
}
