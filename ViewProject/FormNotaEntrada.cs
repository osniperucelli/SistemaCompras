using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ModelProject;
using ControllerProject;

namespace ViewProject
{
    public partial class FormNotaEntrada : Form
    {
        private NotaEntradaController notaEntradaController;
        private FornecedorController fornecedorController;
        private ProdutoController produtoController;

        private NotaEntrada notaAtual;
                
        public FormNotaEntrada(NotaEntradaController controller, FornecedorController fornecedorController, ProdutoController produtoController)
        {
            InitializeComponent();
            this.notaEntradaController = controller;
            this.fornecedorController = fornecedorController;
            this.produtoController = produtoController;
            InicializaComboBoxs();   //controle q possui lista suspensa c itens q podem ser selecionados.
            AtualizarDataGridViewNotas();
        }


        //metodo responsavel por inicializacao dos comboBox
        //O método InicializaComboBoxs() , inicialmente, limpa o
        //conteúdo dos ComboBox s e depois os “popula”, por meio dos
        //métodos GetAll() de seus controladores.

        private void InicializaComboBoxs()
        {
            cbxFornecedor.Items.Clear();
            cbxProduto.Items.Clear();

            foreach (Fornecedor fornecedor in this.fornecedorController.GetAll())
            {
                cbxFornecedor.Items.Add(fornecedor);
            }
            
            foreach (Produto produto in this.produtoController.GetAll())
            {
                cbxProduto.Items.Add(produto);
            }
        }


        //botao Novo Nota
        private void btnNovoNota_Click(object sender, System.EventArgs e)
        {
            ClearControlsNota();
        }


        //botao gravar ou gravar nota dif da pag 173
        private void btnGravarNota_Click(object sender, System.EventArgs e)
        {
            var notaEntrada = new NotaEntrada()
            {
                DataEmissao = dtpEmissao.Value,
                DataEntrada = dtpEntrada.Value,
                FornecedorNota = (Fornecedor)cbxFornecedor.SelectedItem,
                Numero = txtNumero.Text
            };

            if (!string.IsNullOrEmpty(this.txtIDNota.Text))
            {
                notaEntrada.Id = Convert.ToInt32(txtIDNota.Text);
                notaEntradaController.Update(notaEntrada);
            }
            else
            {
                notaEntradaController.Insert(notaEntrada);
            }

            dgvNotasEntrada.DataSource = null;
            dgvNotasEntrada.DataSource = this.notaEntradaController.GetAll();
            ClearControlsNota();
        }

        private void AtualizarDataGridViewNotas()
        {
            dgvNotasEntrada.DataSource = null;   //funciona com reset de dados no controle.
            dgvNotasEntrada.DataSource = this.notaEntradaController.GetAll();  //getAll é um ilist tornando controlador e repositorio mais independentes.
            dgvNotasEntrada.Refresh();
        }

        //botao cancelar nota
        private void btnCancelarNota_Click(object sender, System.EventArgs e)
        {
            ClearControlsNota();
        }


        //botao remover nota
        private void btnRemoverNota_Click(object sender, System.EventArgs e)
        {
            if (txtIDNota.Text == string.Empty)
            {
                MessageBox.Show("Selecione a NOTA a ser removida no GRID");
            }
            else
            {
                this.notaEntradaController.Remove
                (
                    new NotaEntrada()
                    {
                        Id = Convert.ToInt32(txtIDNota.Text)
                    }
                );
                dgvNotasEntrada.DataSource = null;
                dgvNotasEntrada.DataSource = this.notaEntradaController.GetAll();
                ClearControlsNota();
            }
        }



        //metodo clearControlsNota
        //método que realiza a preparação dos
        //controles do corpo da nota para uma nova inserção de dados
        private void ClearControlsNota()
        {
            dgvNotasEntrada.ClearSelection();
            dgvProdutos.ClearSelection();
            txtIDNota.Text = string.Empty;
            cbxFornecedor.SelectedIndex = -1;  //-1 é para nao apresentar nenhum item selecionado na combobox
            txtNumero.Text = string.Empty;
            dtpEmissao.Value = DateTime.Now;
            dtpEntrada.Value = DateTime.Now;
            cbxFornecedor.Focus();
        }

        //metodo SelectionChanged
        private void dgvNotasEntrada_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                this.notaAtual = this.notaEntradaController.
                    GetNotaEntradabyId((Guid)dgvNotasEntrada.
                    CurrentRow.Cells[0].Value);
                txtIDNota.Text = notaAtual.Id.
                    ToString();
                txtNumero.Text = notaAtual.Numero;
                cbxFornecedor.SelectedItem = notaAtual.
                    FornecedorNota;
                dtpEmissao.Value = notaAtual.DataEmissao;
                dtpEntrada.Value = notaAtual.DataEntrada;
                UpdateProdutosGrid();
            }
            catch //(Exception exception)
            {
                this.notaAtual = new NotaEntrada();
            }
        }


        //metodo update
        private void UpdateProdutosGrid()
        {
            dgvProdutos.DataSource = null;
            if (this.notaAtual.Produtos.Count > 0)
            {
                dgvProdutos.DataSource = this.notaAtual.
                    Produtos;
            }
        }







        //botao Novo Produto 182
        private void btnNovoProduto_Click(object sender, EventArgs e)
        {
            ClearControlsProduto();
            if (txtIDNota.Text == string.Empty)
            {
                MessageBox.Show("Selecione a NOTA, que terá " +
                    "inserção de produtos, no GRID");
            }
            else
            {
                ChangeStatusOfControls(true);
            }
        }


        //metodo Change Status of Controls
        private void ChangeStatusOfControls(bool newStatus)
        {
            cbxProduto.Enabled = newStatus;
            txtCusto.Enabled = newStatus;
            txtQuantidade.Enabled = newStatus;
            btnNovoProduto.Enabled = !newStatus;
            btnGravarProduto.Enabled = newStatus;
            btnCancelarProduto.Enabled = newStatus;
            btnRemoverProduto.Enabled = newStatus;
        }


        //botao gravar produto
        private void btnGravarProduto_Click(object sender, EventArgs e)
        {
            var produtoNota = new ProdutoNotaEntrada()
            {
                Id = (txtIDProduto.Text == string.Empty ?
                    Guid.NewGuid() : new Guid(txtIDProduto.
                    Text)),
                PrecoCustoCompra = Convert.ToDouble(
                    txtCusto.Text),
                ProdutoNota = (Produto)cbxProduto.
                    SelectedItem,
                QuantidadeComprada = Convert.ToDouble(
                    txtQuantidade.Text)
            };
            this.notaAtual.RegistrarProduto(produtoNota);
            this.notaAtual = this.notaEntradaController.Update(
                this.notaAtual);
            ChangeStatusOfControls(false);
            UpdateProdutosGrid();
            ClearControlsProduto();
        }

        //incluido do livro 183
        //private void RegistrarProduto(ProdutoNotaEntrada produto)
        //{
        //    if (this.Produtos.Contains(produto))
        //        this.Produtos.Remove(produto);
        //    this.Produtos.Add(produto);
        //}


        //botao cancelar produto
        private void btnCancelarProduto_Click(object sender, EventArgs e)
        {
            ClearControlsProduto();
            ChangeStatusOfControls(false);
        }


        //botao remover produto
        private void btnRemoverProduto_Click(object sender, EventArgs e)
        {
            this.notaAtual.RemoverProduto(
                new ProdutoNotaEntrada()
                {
                    Id = new Guid(txtIDProduto.Text)
                }
            );
            this.notaEntradaController.Update(this.notaAtual);
            UpdateProdutosGrid();
            ClearControlsProduto();
            ChangeStatusOfControls(false);
        }

        
        //metodo Clear Controls Produt
        private void ClearControlsProduto()
        {
            ChangeStatusOfControls(true);
        }

    }
}
