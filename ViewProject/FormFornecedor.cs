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
        }

        //botao gravar
        private void btnGravar_Click(object sender, EventArgs e)
        {
            //var fornecedor = this.controller.Insert(
            var fornecedor = new Fornecedor()
            {
                Id = (txtID.Text == string.Empty ?    //operador ternario  o ?:
                    Guid.NewGuid() : new Guid(txtID.Text)),
                Nome = txtNome.Text,
                CNPJ = txtCNPJ.Text
            };
            fornecedor = (txtID.Text == string.Empty ?
                this.controller.Insert(fornecedor) : 
                this.controller.Update(fornecedor));                
                //txtID.Text = fornecedor.Id.ToString();
            dgvFornecedores.DataSource = null;   //funciona com reset de dados no controle.
            dgvFornecedores.DataSource = this.controller.GetAll();  //getAll é um ilist tornando controlador e repositorio mais independentes.
            ClearControls();
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
            txtID.Text = dgvFornecedores.CurrentRow.Cells[0].Value.ToString();
            txtNome.Text = dgvFornecedores.CurrentRow.Cells[1].Value.ToString();
            txtCNPJ.Text = dgvFornecedores.CurrentRow.Cells[2].Value.ToString();
        }


        //botao remover
        private void btnRemover_Click(object sender, EventArgs e) {
            if (txtID.Text == string.Empty){
                MessageBox.Show("Selecione o FORNECEDOR a ser removido no GRID.");
            }
            else
            {
                this.controller.Remove(new Fornecedor()
                {
                    Id = new Guid(txtID.Text)
                }                
                );
                dgvFornecedores.DataSource = null;
                dgvFornecedores.DataSource = this.controller.GetAll();
                ClearControls();
            }                
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClearControls();
        }


    }
}
