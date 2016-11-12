using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FixerIOCore
{
    public class Quote
    {
        public String Base { get; set; }
        public DateTime Date { get; set; }
        public IDictionary<string, decimal> Rates { get; set; }
    }
}
