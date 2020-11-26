using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TRAB_CRUD.Classes;

namespace TRAB_CRUD.Formulários
{
    public partial class FrmFornecedor : Form
    {
        private string modo;
        public FrmFornecedor()
        {
            InitializeComponent();
        }

        private void FrmFornecedor_Load(object sender, EventArgs e)
        {
            ConexaoFornecedor conexao = new ConexaoFornecedor();

            Estilos style = new Estilos();
            conexao.Selecionado = txtNome.Text;
            dgvFornecedor2.DataSource = conexao.LoadTabela();

            style.CorBotaoAzul(btnNovo);
            style.CorBotaoAzul(btnAlterar);
            style.CorBotaoAzul(btnDeletar);
            style.CorBotaoAzul(btnPesqCod);
            style.CorBotaoAzul(btnPesqNome);
            style.CorBotaoAzul(btnSalvar);
            style.CorBotaoAzul(btnCancelar);
            style.CorBotaoFechar(btnFechar);

        }
        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dgvFornecedor2_SelectionChanged(object sender, EventArgs e)
        {
            ConexaoFornecedor conexao = new ConexaoFornecedor();
            int linha = 0;
            linha = dgvFornecedor2.CurrentRow.Index;

            txtNome.Text = dgvFornecedor2[1, linha].Value.ToString();
            txtCod.Text = dgvFornecedor2[0, linha].Value.ToString();
        }
        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            ConexaoFornecedor conexao = new ConexaoFornecedor();

            modo = "novo";

            btnAlterar.Enabled = false;
            btnDeletar.Enabled = false;
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
            txtNome.Enabled = true;
            txtCod.Enabled = true;
            txtCod.Text = null;
            txtNome.Text = null;
            txtCod.Focus();
        }
        private void btnAlterar_Click(object sender, EventArgs e)
        {
            ConexaoFornecedor conexao = new ConexaoFornecedor();

            modo = "alterar";

            btnNovo.Enabled = false;
            btnDeletar.Enabled = false;
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
            txtCod.Enabled = true;
            txtNome.Enabled = true;
            txtNome.Focus();
        }
        private void btnDeletar_Click(object sender, EventArgs e)
        {
            ConexaoFornecedor conexao = new ConexaoFornecedor();
            conexao.Selecionado = txtNome.Text;
            if (MessageBox.Show("Tem certeza que deseja deletar " + conexao.Selecionado, "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                modo = "deletar";
                
                if (conexao.Deletar())
                {
                    MessageBox.Show("Fornecedor deletado com sucesso");
                }
                else
                {
                    MessageBox.Show("Erro ao deletar Fornecedor");
                }
                dgvFornecedor2.DataSource = conexao.LoadTabela();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnNovo.Enabled = true;
            btnAlterar.Enabled = true;
            btnDeletar.Enabled = true;
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;

            txtNome.Enabled = false;
            txtCod.Enabled = false;
        }
        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            ConexaoFornecedor conexao = new ConexaoFornecedor();
            conexao.CodNovo = txtCod.Text;
            conexao.NomeNovo = txtNome.Text;
            conexao.Selecionado = txtNome.Text;

            if (modo == "novo")
            {
                if (conexao.NovoFornecedor())
                {
                    MessageBox.Show("Fornecedor adicionado com sucesso");
                    dgvFornecedor2.DataSource = conexao.LoadTabela();

                    btnAlterar.Enabled = true;
                    btnDeletar.Enabled = true;
                    btnSalvar.Enabled = false;
                    txtNome.Enabled = false;
                    txtCod.Enabled = false;
                }

                else
                {
                    MessageBox.Show("Erro ao adicionar o fornecedor");

                    btnAlterar.Enabled = true;
                    btnDeletar.Enabled = true;
                    btnSalvar.Enabled = false;
                    txtNome.Enabled = false;
                    txtCod.Enabled = false;
                }
            }
            else if (modo == "alterar")
            {
                if (conexao.Alterar())
                {
                    MessageBox.Show("Fornecedor alterado com sucesso");
                    dgvFornecedor2.DataSource = conexao.LoadTabela();

                    btnNovo.Enabled = true;
                    btnDeletar.Enabled = true;
                    btnSalvar.Enabled = false;
                    txtNome.Enabled = false;
                    txtCod.Enabled = false;

                }

                else
                {
                    MessageBox.Show("Erro ao alterar o fornecedor");
                    btnAlterar.Enabled = true;
                    btnDeletar.Enabled = true;
                    btnSalvar.Enabled = false;
                    txtNome.Enabled = false;
                    txtCod.Enabled = false;
                }
            }
            else if (modo == "deletar")
            {
                if (conexao.Deletar())
                {
                    MessageBox.Show("Fornecedor deletado com sucesso");
                    dgvFornecedor2.DataSource = conexao.LoadTabela();
                }
                else
                {
                    MessageBox.Show("Erro ao deletar o fornecedor");
                }
            }
            btnCancelar.Enabled = false;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            ConexaoFornecedor conexao = new ConexaoFornecedor();
            conexao.NomePesquisar = txtPesqNome.Text;
            dgvFornecedor2.DataSource = conexao.PesquisarNome();
        }
        private void btnPesqCod_Click(object sender, EventArgs e)
        {
            ConexaoFornecedor conexao = new ConexaoFornecedor();
            conexao.CodPesquisar = txtPesqCod.Text;
            dgvFornecedor2.DataSource = conexao.PesquisarCod();
        }
        private void txtPesqCod_Enter(object sender, EventArgs e)
        {
            AcceptButton = btnPesqCod;
        }

        private void btnPesqNome_Click(object sender, EventArgs e)
        {
            ConexaoFornecedor conexao = new ConexaoFornecedor();
            conexao.NomePesquisar = txtPesqNome.Text;
            dgvFornecedor2.DataSource = conexao.PesquisarNome();
        }
        private void txtPesqNome_Enter(object sender, EventArgs e)
        {
            this.AcceptButton = btnPesqNome;
        }
    }
}

