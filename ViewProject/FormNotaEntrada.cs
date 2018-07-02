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
        private NotaEntradaController controller;
        private FornecedorController fornecedorController;
        private ProdutoController produtoController;

        private NotaEntrada notaAtual;
                
        public FormNotaEntrada(NotaEntradaController controller, FornecedorController fornecedorController, ProdutoController produtoController)
        {
            InitializeComponent();
            this.controller = controller;
            this.fornecedorController = fornecedorController;
            this.produtoController = produtoController;
            InicializaComboBoxs();   //controle q possui lista suspensa c itens q podem ser selecionados.
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


        //botao gravar nota
        private void btnGravarNota_Click(object sender, System.EventArgs e)
        {
            var notaEntrada = new NotaEntrada()
            {
                Id = (txtIDNota.Text == string.Empty ? Guid.NewGuid() : new Guid(txtIDNota.Text)),
                DataEmissao = dtpEmissao.Value,
                DataEntrada = dtpEntrada.Value,
                FornecedorNota = (Fornecedor)cbxFornecedor.SelectedItem,
                Numero = txtNumero.Text
            };
            notaEntrada = (txtIDNota.Text == string.Empty ? 
                this.controller.Insert(notaEntrada));
                this.controller.Update(notaEntrada));
            dgvNotasEntrada.DataSource = null;
            dgvNotasEntrada.DataSource = this.controller.GetAllNotasEntrada();
            ClearControlsNota();
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
                MessageBox.Show(
                   "Selecione a NOTA a ser removida no GRID");
            }
            else
            {
                this.controller.Remove(
                    new NotaEntrada()
                    {
                        Id = new Guid(txtIDNota.Text)
                    }
                );
                dgvNotasEntrada.DataSource = null;
                dgvNotasEntrada.DataSource =
                    this.controller.GetAll();
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
                this.notaAtual = this.controller.
                    GetNotaEntradaById((Guid)dgvNotasEntrada.
                    CurrentRow.Cells[0].Value);
                txtIDNotaEntrada.Text = notaAtual.Id.
                    ToString();
                txtNumero.Text = notaAtual.Numero;
                cbxFornecedor.SelectedItem = notaAtual.
                    FornecedorNota;
                dtpEmissao.Value = notaAtual.DataEmissao;
                dtpEntrada.Value = notaAtual.DataEntrada;
                UpdateProdutosGrid();
            }
            catch
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


        //botao Novo Produto
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
            this.notaAtual = this.controller.Update(
                this.notaAtual);
            ChangeStatusOfControls(false);
            UpdateProdutosGrid();
            ClearControlsProduto();
        }


        //botao cancelar produto
        private void btnCancelarProduto_Click(object sender, EventArgs e)
        {
            ClearControlsProduto();
            ChangeStatusOfControls(false);
        }


        //metodo Clear Controls Produt
        private void ClearControlsProduto()
        {
            //TODO: [Implementar]
            MessageBox.Show("Implementar");
        }

    }
}
