using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    class Fornecedor
    {
        public Guid Id { get; set; }   //guid representa n inteiro de 16 bytes
        public string Nome { get; set; }
        public string CNPJ { get; set; }
    }
}
