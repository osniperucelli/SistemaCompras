﻿using System;
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
    public partial class FormFornecedor : Form
    {
        private FornecedorController controller = new FornecedorController();

        public FormFornecedor(FornecedorController controller)
        {
            InitializeComponent();
            this.controller = controller;
        }

        private void btnGravar_Click(object sender, EventArgs e) {
            this.controller.Insert(new Fornecedor() {
                Id = Guid.NewGuid(),
                Nome = txtNome.Text,
                CNPJ = txtCNPJ.Text
            }
            );
        }











    }
}
