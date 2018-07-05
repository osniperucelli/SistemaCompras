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
    //VERSAO 2
    //public partial class FormFornecedor : Form
    //{
    //    private FornecedorController controller;
    //
    //    public FormFornecedor(FornecedorController controller)
    //   {
    //        InitializeComponent();
    //        this.controller = controller;
    //    }
    //}

    //VERSAO 1 
    public partial class FormFornecedor : Form
    {
        //declaracao do controlador na camada de apresentacao 
        private FornecedorController controller = new FornecedorController();
        
        public FormFornecedor(FornecedorController controller)
        {
            InitializeComponent();
            this.controller = controller;
            AtualizarDataGridView();
            //InserirElementosDataGridView();
        }

        //botao gravar
        private void btnGravar_Click(object sender, EventArgs e)
        {
            //var fornecedor = this.controller.Insert(

            Fornecedor fornecedor = new Fornecedor();
            fornecedor.Nome = txtNome.Text;
            fornecedor.CNPJ = txtCNPJ.Text;
            if (!string.IsNullOrEmpty(txtID.Text))
            {
                fornecedor.Id = Convert.ToInt32(txtID.Text);
            }

            fornecedor = (txtID.Text == string.Empty ? this.controller.Insert(fornecedor) : this.controller.Update(fornecedor));                
                //txtID.Text = fornecedor.Id.ToString();
            ClearControls();
            AtualizarDataGridView();

            //InserirElementosDataGridView();
        }

        private void AtualizarDataGridView()
        {
            dgvFornecedores.DataSource = null;   //funciona com reset de dados no controle.
            dgvFornecedores.DataSource = this.controller.GetAll();  //getAll é um ilist tornando controlador e repositorio mais independentes.
            dgvFornecedores.Refresh();
        }

        //Exemplo de datagridview inserindo elementos manualmente
        private void InserirElementosDataGridView()
        {
            dgvFornecedores.Columns.Clear();
            dgvFornecedores.Rows.Clear();
            dgvFornecedores.Refresh();

            IList<Fornecedor> fornecedores = this.controller.GetAll();
            dgvFornecedores.ColumnCount = 3;
            dgvFornecedores.Columns[0].Name = "Id";
            dgvFornecedores.Columns[1].Name = "Nome";
            dgvFornecedores.Columns[2].Name = "CNPJ";
            foreach (Fornecedor f in fornecedores)
            {
                string[] dadosFornecedor = new string[] { f.Id.ToString(), f.Nome, f.CNPJ };
                dgvFornecedores.Rows.Add(dadosFornecedor);
            }
        }

        //Metodo clearControl
        private void ClearControls()
        {
            dgvFornecedores.ClearSelection();
            txtID.Text = string.Empty;
            txtNome.Text = string.Empty;
            txtCNPJ.Text = string.Empty;
            txtNome.Focus();  //atribui foto ao primeiro controle editavel do formulario
        }
        

        //botao novo
        private void btnNovo_Click(object sender, EventArgs e)
        {
            ClearControls();
            //txtID.Text = string.Empty;
            //txtNome.Text = string.Empty;
            //txtCNPJ.Text = string.Empty;
        }


        //metodo captura evento selecionado
        private void dgvFornecedores_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvFornecedores.SelectedRows.Count > 0)
            {
                txtID.Text = dgvFornecedores.CurrentRow.Cells[0].Value.ToString(); //na tela esse eh o codigo do banco
                txtNome.Text = dgvFornecedores.CurrentRow.Cells[1].Value.ToString(); //na tela esse eh o nome
                txtCNPJ.Text = dgvFornecedores.CurrentRow.Cells[2].Value.ToString(); //na tela esse eh o cnpj
            }
        }


        //botao remover
        private void btnRemover_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Selecione o FORNECEDOR a ser removido no GRID.");
            }
            else
            {
                this.controller.Remove(new Fornecedor()
                {
                    Id = Convert.ToInt32(txtID.Text)
                });

                ClearControls();
                AtualizarDataGridView();
                //InserirElementosDataGridView();
            }                
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClearControls();
        }
    }
}
