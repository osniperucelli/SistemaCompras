using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class NotaEntrada
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        //public string NomeFornecedor
        //{
        //    get
        //    {
        //        if (FornecedorNota == null)
        //        {
        //            return string.Empty;
        //        }
        //        else
        //        {
        //            return FornecedorNota.Nome;
        //        }
        //    }
        //}
        public Fornecedor FornecedorNota { get; set; } //na tabela vai ser uma FK, chave estrangeira para a tabela de fornecedores
        public DateTime DataEmissao { get; set; }
        public DateTime DataEntrada { get; set; }
        public IList<ProdutoNotaEntrada> Produtos { get; set; }

        public NotaEntrada()
        {
            this.Produtos = new List<ProdutoNotaEntrada>();
        }

        public NotaEntrada(int id, string numero, Fornecedor fornecedor, DateTime dataEmissao, DateTime dataEntrada)
        {
            Id = id;
            Numero = numero;
            FornecedorNota = fornecedor;
            DataEmissao = dataEmissao;
            DataEntrada = dataEntrada;
        }

        public void RegistrarProduto(ProdutoNotaEntrada produto){
            if (!this.Produtos.Contains(produto))
            {
                this.Produtos.Remove(produto);
            }

            this.Produtos.Add(produto);
        }

        public void RemoverProduto(ProdutoNotaEntrada produto){
            this.Produtos.Remove(produto);
        }

        public void RemoverTodosProdutos(){
            this.Produtos.Clear();
        }
    }
}
