using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.domain
{
    public class Logs
    {
            public int Id { get; set; }
            public string LogLevel { get; set; }
            public string MessageLog { get; set; }
            public DateTime DateLog { get; set; }
    }
}
