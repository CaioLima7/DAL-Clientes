using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAtividadeEntrevista.Extensoes
{
    public class Row
    {
        public object CPF { get; set; }
        public string Nome { get; set; }
    }

    public class RootObject
    {
        public int current { get; set; }
        public int rowCount { get; set; }
        public List<Row> rows { get; set; }
        public int total { get; set; }
    }
}