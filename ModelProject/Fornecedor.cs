using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class Fornecedor
    {
        public int Codigo { get; set; } //id do banco de dados
        public string Nome { get; set; }
        public string CNPJ { get; set; }

        public Fornecedor()
        {

        }

        public Fornecedor(int codigo, string nome, string cnpj)
        {
            Codigo = codigo;
            Nome = nome;
            CNPJ = cnpj;
        }

        //subscrevendo metodos Equals e GetHashCode para que objeto possa ser buscado na selecao em fornecedor.
        protected bool Equals(Fornecedor other) {
            return Codigo.Equals(other.Codigo);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Fornecedor))
                return false;
            return Equals((Fornecedor) obj);
        }
    }
}
