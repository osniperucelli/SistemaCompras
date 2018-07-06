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
        //para FORNECEDOR
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

            int idGerado = conn.ExecuteInsert(query, parameters);
            fornecedor.Id = idGerado;

            //this.fornecedores.Add(fornecedor);

            return fornecedor;
        }
        
        public void RemoveFornecedor(Fornecedor fornecedor)
        {
            string query = "DELETE FROM Fornecedores WHERE ID = @ID";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@ID", SqlDbType.Int);
            parameters[0].Value = fornecedor.Id;

            conn.ExecuteNonQuery(query, parameters);

            this.fornecedores.Remove(fornecedor);
        }

        public Fornecedor GetFornecedor(int idFornecedor)
        {
            string query = "SELECT * FROM Fornecedores WHERE ID = @ID";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@ID", SqlDbType.Int);
            parameters[0].Value = idFornecedor;

            DataTable dt = conn.ExecuteSelectQuery(query, parameters);
            Fornecedor fornecedor = null;
            foreach (DataRow row in dt.Rows)
            {
                int id = (int)row["ID"];
                string nome = row["Nome"] as string;
                string cnpj = row["CNPJ"] as string;

                fornecedor = new Fornecedor(id, nome, cnpj);
            }

            return fornecedor;
        }
        
        public IList<Fornecedor> GetAllFornecedores()
        {
            fornecedores.Clear();

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
            parameters[2].Value = fornecedor.Id;

            conn.ExecuteNonQuery(query, parameters);

            //this.fornecedores[this.fornecedores.IndexOf(fornecedor)] = fornecedor;

            return fornecedor;
        }

        //para NOTA ENTRADA

        public NotaEntrada InsertNotaEntrada(NotaEntrada notaEntrada)
        {
            string query = "INSERT INTO Notas VALUES (@Numero, @Fornecedor, @DataEmissao, @DataEntrada); SELECT SCOPE_IDENTITY();";
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@Numero", SqlDbType.VarChar);
            parameters[0].Value = notaEntrada.Numero;

            parameters[1] = new SqlParameter("@Fornecedor", SqlDbType.Int);
            parameters[1].Value = notaEntrada.FornecedorNota.Id;

            parameters[2] = new SqlParameter("@DataEmissao", SqlDbType.DateTime);
            parameters[2].Value = notaEntrada.DataEmissao;

            parameters[3] = new SqlParameter("@DataEntrada", SqlDbType.DateTime);
            parameters[3].Value = notaEntrada.DataEntrada;

            int idGerado = conn.ExecuteInsert(query, parameters);
            notaEntrada.Id = idGerado;

            //this.notasEntrada.Add(notaEntrada);

            return notaEntrada;
        }

        public void RemoveNotaEntrada(NotaEntrada notaEntrada)
        {
            string query = "DELETE FROM Notas WHERE ID = @ID";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@ID", SqlDbType.Int);
            parameters[0].Value = notaEntrada.Id;

            conn.ExecuteNonQuery(query, parameters);

            this.notasEntrada.Remove(notaEntrada);
        }


        public IList<NotaEntrada> GetAllNotasEntrada()
        {
            notasEntrada.Clear();

            string query = "SELECT * FROM Notas";
            DataTable dt = conn.ExecuteSelectQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                int id = (int)row["ID"];
                string nome = row["Numero"] as string;
                int idFornecedor = (int) row["Fornecedor"]; //chave estrangeira
                DateTime dataEmissao = (DateTime)row["DataEmissao"];
                DateTime dataEntrada = (DateTime)row["DataEntrada"];

                Fornecedor f = GetFornecedor(idFornecedor);
                NotaEntrada nota = new NotaEntrada(id, nome, f, dataEmissao, dataEntrada);
                notasEntrada.Add(nota);
            }

            return this.notasEntrada;
        }

        
        public NotaEntrada UpdateNotaEntrada(NotaEntrada notaEntrada)
        {
            string query = "UPDATE Notas SET Numero = @Numero, Fornecedor = @Fornecedor, DataEmissao = @DataEmissao, DataEntrada = @DataEntrada WHERE ID = @ID";
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("@Numero", SqlDbType.VarChar);
            parameters[0].Value = notaEntrada.Numero;

            parameters[1] = new SqlParameter("@Fornecedor", SqlDbType.Int);
            parameters[1].Value = notaEntrada.FornecedorNota.Id;

            parameters[2] = new SqlParameter("@DataEmissao", SqlDbType.DateTime);
            parameters[2].Value = notaEntrada.DataEmissao;

            parameters[3] = new SqlParameter("@DataEntrada", SqlDbType.DateTime);
            parameters[3].Value = notaEntrada.DataEntrada;

            parameters[4] = new SqlParameter("@ID", SqlDbType.Int);
            parameters[4].Value = notaEntrada.Id;

            conn.ExecuteNonQuery(query, parameters);

            //this.notasEntrada[this.notasEntrada.IndexOf(notaEntrada)] = notaEntrada;

            return notaEntrada;
        }


        //public NotaEntrada GetNotaEntradaById(Guid Id)
        //{
        //    var notaEntrada = this.notasEntrada[
        //    this.notasEntrada.IndexOf(
        //        new NotaEntrada() { Id = Id }
        //        )];
        //    return notaEntrada;
        //}



        //para PRODUTO 
        public Produto InsertProduto(Produto produto)
        {
            string query = "INSERT INTO Produtos (Nome) VALUES (@Nome); SELECT SCOPE_IDENTITY();";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Nome", SqlDbType.VarChar);
            parameters[0].Value = produto.Nome;



            int idGerado = conn.ExecuteInsert(query, parameters);
            produto.Id = idGerado;

            //this.produtos.Add(produto);

            return produto;
        }

        public void RemoveProduto(Produto produto)
        {
            string query = "DELETE FROM Produtos WHERE ID = @ID";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@ID", SqlDbType.Int);
            parameters[0].Value = produto.Id;

            conn.ExecuteNonQuery(query, parameters);

            this.produtos.Remove(produto);
        }

        public IList<Produto> GetAllProdutos()
        {
            produtos.Clear();

            string query = "SELECT * FROM Produtos";
            DataTable dt = conn.ExecuteSelectQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                int id = (int)row["ID"];
                string nome = row["Nome"] as string;

                Produto p = new Produto(id, nome);
                produtos.Add(p);
            }

            return produtos;
        }

        public Produto UpdateProduto(Produto produto)
        {
            string query = "UPDATE Produtos SET Nome = @Nome WHERE ID = @ID";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@Nome", SqlDbType.VarChar);
            parameters[0].Value = produto.Nome;

            parameters[1] = new SqlParameter("@ID", SqlDbType.Int);
            parameters[1].Value = produto.Id;

            conn.ExecuteNonQuery(query, parameters);

            //this.produtos[this.produtos.IndexOf(produto)] = produto;

            return produto;
        }
    }
}
