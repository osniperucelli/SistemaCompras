using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControllerProject;

namespace ViewProject
{
    //declaracao de controladores no form da janela principal
    public partial class FormJanelaPrincipal : Form
    {
        private FornecedorController fornecedorController = new FornecedorController();
        private ProdutoController produtoController = new ProdutoController();
        private NotaEntradaController notaEntradaController = new NotaEntradaController();

        public FormJanelaPrincipal()
        {
            InitializeComponent();
        }

        //captura evento click menu fornecedores
        private void fornecedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormFornecedor(fornecedorController).ShowDialog();
        }

        //adicionado  do exemplo do livro 
        private void produtoToolStripMenuItem_click(object sender, EventArgs e)
        {
            new FormProduto().ShowDialog();
        }

        private void compraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormNotaEntrada(notaEntradaController, fornecedorController, produtoController).ShowDialog();
        }
    }
}
