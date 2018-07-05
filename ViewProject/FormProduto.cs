using ControllerProject;
using ModelProject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ViewProject
{
    public partial class FormProduto : Form
    {
        private ProdutoController controller;

        public FormProduto()
        {
            InitializeComponent();
            controller = new ProdutoController();
            AtualizarDataGridView();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Produto p = new Produto();
            p.Nome = this.txtNomeProduto.Text;
            if (!string.IsNullOrEmpty(this.txtIDProduto.Text))
            {
                p.Id = Convert.ToInt32(txtIDProduto.Text);
                controller.Update(p);
            }
            else
            {
                controller.Insert(p);
            }

            ClearControls();
            AtualizarDataGridView();
        }

        private void AtualizarDataGridView()
        {
            dgvProdutos.DataSource = null;   //funciona com reset de dados no controle.
            dgvProdutos.DataSource = this.controller.GetAll();  //getAll é um ilist tornando controlador e repositorio mais independentes.
            dgvProdutos.Refresh();
        }

        private void ClearControls()
        {
            dgvProdutos.ClearSelection();
            txtIDProduto.Text = string.Empty;
            txtNomeProduto.Text = string.Empty;
            txtNomeProduto.Focus();
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIDProduto.Text))
            {
                MessageBox.Show("Selecione o PRODUTO a ser removido no GRID.");
            }
            else
            {
                this.controller.Remove(new Produto()
                {
                    Id = Convert.ToInt32(txtIDProduto.Text)
                });

                ClearControls();
                AtualizarDataGridView();
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        private void dgvProdutos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProdutos.SelectedRows.Count > 0)
            {
                txtIDProduto.Text = dgvProdutos.CurrentRow.Cells[0].Value.ToString(); //na tela esse eh o codigo do banco
                txtNomeProduto.Text = dgvProdutos.CurrentRow.Cells[1].Value.ToString(); //na tela esse eh o nome
            }
        }
    }
}
