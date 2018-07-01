using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    class NotaEntrada
    {
        public Guid Id { get; set; }
        public string Numero { get; set; }
        public string Fornecedor FornecedorNota { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataEntrada { get; set; }
        public IList<ProdutoNotaEntrada> Produtos { get; set; }

        public NotaEntrada(){
        this.Produtos = new List<ProdutoNotaEntrada>();
        }

        public void RegistrarProduto(ProdutoNotaEntrada){
        if (!this.Produtos.Contains(Produto))
            this.Produtos.Add(produto);
        }

        public void RemoverProduto(ProdutoNotaEntrada){
        this.Produtos.Remove(produto);
        }

        public void RemoverTodosProdutos(){
        this.Produtos.Clear();
        }
    }
}
