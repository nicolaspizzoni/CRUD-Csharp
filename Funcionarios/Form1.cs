using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Funcionarios
{
    public partial class Funcionários : Form
    {
        DAO dao = new DAO();
        int id = 0;

        public void atualizarGrid()
        {
            dataGridFuncionario.DataSource = dao.listarFuncionarios();
        }

        public void limpar()
        {
            txtNome.Text = "";
            txtIdade.Text = "";
            txtEmail.Text = "";
            txtNome.Focus();
            id = 0;
        }

        public Funcionários()
        {
            InitializeComponent();
            atualizarGrid();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Funcionario f = new Funcionario();
            f.nome = txtNome.Text;
            f.email = txtEmail.Text;
            f.idade = Int32.Parse(txtIdade.Text);

            DAO dao = new DAO();
            dao.inserirFuncionario(f);

            MessageBox.Show("Salvo com sucesso");
            atualizarGrid();
            limpar();
        }

        private void dataGridFuncionario_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtNome.Text = Convert.ToString(dataGridFuncionario.Rows[e.RowIndex].Cells[1].Value);
            txtEmail.Text = Convert.ToString(dataGridFuncionario.Rows[e.RowIndex].Cells[2].Value);
            txtIdade.Text = Convert.ToString(dataGridFuncionario.Rows[e.RowIndex].Cells[3].Value);
            id = Convert.ToInt32(dataGridFuncionario.Rows[e.RowIndex].Cells[0].Value);
        }

        private void dataGridFuncionario_MultiSelectChanged(object sender, EventArgs e)
        {

        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if(id > 0)
            {
                Funcionario funcionario = new Funcionario();
                funcionario.id = this.id;
                funcionario.nome = txtNome.Text;
                funcionario.email = txtEmail.Text;
                funcionario.idade = Convert.ToInt32(txtIdade.Text);

                this.dao.alterarFuncionario(funcionario);
                this.atualizarGrid();
                this.limpar();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(id > 0)
            {
                Funcionario funcionario = new Funcionario();
                funcionario.id = this.id;
                this.dao.excluirFuncionario(funcionario);
                this.atualizarGrid();
                this.limpar();
            }
        }
    }
}
