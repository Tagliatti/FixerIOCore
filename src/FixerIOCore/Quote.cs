using System;
using System.Collections.Generic;

namespace FixerIOCore
{
    public class Quote
    {
        public String Base { get; set; }
        public DateTime Date { get; set; }
        public IDictionary<string, decimal> Rates { get; set; }
    }
}
