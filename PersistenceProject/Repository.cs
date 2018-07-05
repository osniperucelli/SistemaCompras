using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelProject;

namespace PersistenceProject
{
    public class Repository
    {
        //para Fornecedor
        private IList<Fornecedor> fornecedores = new List<Fornecedor>();
        private IList<Produto> produtos = new List<Produto>();
        private IList<NotaEntrada> notasEntrada = new List<NotaEntrada>();

        private DatabaseConnection conn;

        public Repository()
        {
            this.conn = new DatabaseConnection();
        }

        public Fornecedor InsertFornecedor(Fornecedor fornecedor)
        {
            string query = "INSERT INTO Fornecedores VALUES (@Nome, @CNPJ); SELECT SCOPE_IDENTITY();";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@Nome", SqlDbType.VarChar);
            parameters[0].Value = fornecedor.Nome;

            parameters[1] = new SqlParameter("@CNPJ", SqlDbType.VarChar);
            parameters[1].Value = fornecedor.CNPJ;

            conn.ExecuteInsert(query, parameters);

            //this.fornecedores.Add(fornecedor);

            return fornecedor;
        }
        
        public void RemoveFornecedor(Fornecedor fornecedor)
        {
            string query = "DELETE FROM Fornecedores WHERE ID = @ID";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@ID", SqlDbType.VarChar);
            parameters[0].Value = fornecedor.Codigo;

            conn.ExecuteNonQuery(query, parameters);

            this.fornecedores.Remove(fornecedor);
        }
        
        public IList<Fornecedor> GetAllFornecedores()
        {
            string query = "SELECT * FROM Fornecedores";
            DataTable dt = conn.ExecuteSelectQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                int id = (int) row["ID"];
                string nome = row["Nome"] as string;
                string cnpj = row["CNPJ"] as string;

                Fornecedor fornecedor = new Fornecedor(id, nome, cnpj);
                fornecedores.Add(fornecedor);
            }

            return this.fornecedores;
        }

        public Fornecedor UpdateFornecedor(Fornecedor fornecedor)
        {
            string query = "UPDATE Fornecedores SET Nome = @Nome, CNPJ = @CNPJ WHERE ID = @ID";
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@Nome", SqlDbType.VarChar);
            parameters[0].Value = fornecedor.Nome;

            parameters[1] = new SqlParameter("@CNPJ", SqlDbType.VarChar);
            parameters[1].Value = fornecedor.CNPJ;

            parameters[2] = new SqlParameter("@ID", SqlDbType.Int);
            parameters[2].Value = fornecedor.Codigo;

            conn.ExecuteNonQuery(query, parameters);

            //this.fornecedores[this.fornecedores.IndexOf(fornecedor)] = fornecedor;

            return fornecedor;
        }

        //para Nota Entrada

        public NotaEntrada InsertNotaEntrada(NotaEntrada notaEntrada)
        {
            this.notasEntrada.Add(notaEntrada);
            return notaEntrada;
        }

        public void RemoveNotaEntrada(NotaEntrada notaEntrada)
        {
            this.notasEntrada.Remove(notaEntrada);
        }


        public IList<NotaEntrada> GetAllNotasEntrada()
        {
            return this.notasEntrada;
        }

        
        public NotaEntrada UpdateNotaEntrada(NotaEntrada notaEntrada)
        {
            this.notasEntrada[this.notasEntrada.IndexOf(notaEntrada)] = notaEntrada;
            return notaEntrada;
        }


        public NotaEntrada GetNotaEntradaById(Guid Id)
        {
            var notaEntrada = this.notasEntrada[
            this.notasEntrada.IndexOf(
                new NotaEntrada() { Id = Id }
                )];
            return notaEntrada;
        }



        //para Produto

        public Produto InsetProduto(Produto produto)
        {
            this.produtos.Add(produto);
            return produto;
        }


        public void RemoveProduto(Produto produto)
        {
            this.produtos.Remove(produto);
        }

        public IList<Produto> GetAllProdutos()
        {
            return this.produtos;
        }


        public Produto UpdateProduto(Produto produto)
        {
            this.produtos[this.produtos.IndexOf(produto)] = produto;
            return produto;
        }

        
    }
}
