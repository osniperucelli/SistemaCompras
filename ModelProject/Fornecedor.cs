using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class Fornecedor
    {
        public Guid Id { get; set; }   //guid representa n inteiro de 16 bytes
        public string Nome { get; set; }
        public string CNPJ { get; set; }


        //subscrevendo metodos Equals e GetHashCode para que objeto possa ser buscado na selecao em fornecedor.
        protected bool Equals(Fornecedor other) {
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Fornecedor))
                return false;
            return Equals((Fornecedor) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
