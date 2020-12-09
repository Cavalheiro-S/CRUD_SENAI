using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TRAB_CRUD.Classes;

namespace TRAB_CRUD.Formulários
{
    public partial class FrmProduto : Form
    {
        private string modo;
        public FrmProduto()
        {
            InitializeComponent();
        }

        private void FrmProduto_Load(object sender, EventArgs e)
        {
            ConexaoProduto conexao = new ConexaoProduto();
            List<Marca> marcas = conexao.carregarCombo();

            foreach (var item in marcas)
            {
                cmbMarcas.Items.Add(item.Nome);
            }
            dgvTabela.DataSource = conexao.CarregarTabela();

            rdbImportado.Enabled = false;
            rdbNacional.Enabled = false;
            Estilos style = new Estilos();

            //ESTILO PARA OS BOTÕES QUANDO O MOUSE PASSA POR CIMA
            style.CorBotaoAzul(btnSalvar);
            style.CorBotaoAzul(btnCancelar);
            style.CorBotaoAzul(btnNovo);
            style.CorBotaoAzul(btnAlterar);
            style.CorBotaoAzul(btnDeletar);
            style.CorBotaoAzul(btnPesqNome);
            style.CorBotaoAzul(btnPesqId);
            style.CorBotaoFechar(btnExit);
        }
        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dgvTabela_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                ConexaoProduto conexao = new ConexaoProduto();
                int linha = 0;
                linha = dgvTabela.CurrentRow.Index;
                conexao.CodSelecionado = Convert.ToInt32(dgvTabela[0, linha].Value.ToString());
                conexao.ExibirCampos();

                //PASSANDO VALORES PARA PROPRIEDADES DA CONEXAO PRODUTO
                txtModelo.Text = conexao.Modelo;
                txtCodigo.Text = conexao.CodSelecionado.ToString();
                txtArmazenamento.Text = conexao.Armazenamento;
                txtCor.Text = conexao.Cor;
                txtPreco.Text = conexao.Preco.ToString();
                txtSistOpera.Text = conexao.SistOpera;
                cmbMarcas.SelectedItem = conexao.NomeMarca;
                //

                if (conexao.Importado)
                {
                    rdbImportado.Checked = true;
                }
                else
                {
                    rdbNacional.Checked = true;
                }
            }
            catch(NullReferenceException er)
            {
                MessageBox.Show("Linha não encontrada"+ er.Message);
            }
        }
        private void CamposOn()
        {
            txtArmazenamento.Enabled = true;
            txtCor.Enabled = true;
            txtModelo.Enabled = true;
            txtPreco.Enabled = true;
            txtSistOpera.Enabled = true;
            cmbMarcas.Enabled = true;
            rdbImportado.Enabled = true;
            rdbNacional.Enabled = true;
        }
        private void CamposOff()
        {
            txtArmazenamento.Enabled = false;
            txtCor.Enabled = false;
            txtModelo.Enabled = false;
            txtPreco.Enabled = false;
            txtSistOpera.Enabled = false;
            cmbMarcas.Enabled = false;
            rdbImportado.Enabled = false;
            rdbNacional.Enabled = false;
        }
        public void LimparCampos()
        {
            txtArmazenamento.Text = null;
            txtCodigo.Text = null;
            txtCor.Text = null;
            txtModelo.Text = null;
            txtPreco.Text = null;
            txtSistOpera.Text = null;
            cmbMarcas.SelectedIndex = 0;
            rdbNacional.Checked = true;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            modo = "novo";
            btnNovo.Enabled = true;
            btnAlterar.Enabled = false;
            btnDeletar.Enabled = false;
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
            txtCodigo.Enabled = true;
            CamposOn();
        }
        private void btnAlterar_Click(object sender, EventArgs e)
        {
            modo = "alterar";
            btnNovo.Enabled = false;
            btnAlterar.Enabled = true;
            btnDeletar.Enabled = false;
            btnCancelar.Enabled = true;
            btnSalvar.Enabled = true;
            CamposOn();
            txtModelo.Focus();
        }
        private void btnDeletar_Click(object sender, EventArgs e)
        {
            modo = "deletar";
            ConexaoProduto conexao = new ConexaoProduto();
            int linha = 0;
            linha = dgvTabela.CurrentRow.Index;
            conexao.CodSelecionado = Convert.ToInt32(dgvTabela[0, linha].Value.ToString());

            DialogResult resu = MessageBox.Show("Tem certeza que deseja excluir o produto de código " + conexao.CodSelecionado, "Confirmação", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if(resu == DialogResult.Yes)
            {
                if (conexao.DeletarProduto())
                {
                    MessageBox.Show("Produto deletado com sucesso", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvTabela.DataSource = conexao.CarregarTabela();
                }
                else
                {
                    MessageBox.Show("Erro ao deletar o produto", "Falha", MessageBoxButtons.OK, MessageBoxIcon.Error)                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               ;
                }
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            ConexaoProduto conexaoNovo = new ConexaoProduto(Convert.ToInt32(txtCodigo.Text), cmbMarcas.SelectedItem.ToString(), txtModelo.Text, txtSistOpera.Text,
            txtArmazenamento.Text, Convert.ToDecimal(txtPreco.Text), txtCor.Text);
            if (modo == "novo")
            {
                if (rdbNacional.Checked)
                {
                    conexaoNovo.Importado = false;
                }
                else
                {
                    conexaoNovo.Importado = true;
                }

                if (conexaoNovo.InserirProduto())
                {
                    MessageBox.Show("Produto adicionado com sucesso","Exito",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    dgvTabela.DataSource = conexaoNovo.CarregarTabela();
                }
                else
                {
                    MessageBox.Show("Erro ao adicionar o produto","Falha", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if(modo == "alterar")
            {
                ConexaoProduto conexaoAlterar = new ConexaoProduto(Convert.ToInt32(txtCodigo.Text), cmbMarcas.SelectedItem.ToString(), txtModelo.Text, txtSistOpera.Text,
            txtArmazenamento.Text, Convert.ToDecimal(txtPreco.Text), txtCor.Text);
                if (rdbNacional.Checked)
                {
                    conexaoAlterar.Importado = false;
                }
                else
                {
                    conexaoAlterar.Importado = true;
                }
                if (conexaoAlterar.AlteraProduto())
                {
                    MessageBox.Show("Produto alterado com sucesso", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvTabela.DataSource = conexaoAlterar.CarregarTabela();
                }
                else
                {
                    MessageBox.Show("Erro ao alterar o produto", "Falha", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            btnNovo.Enabled = true;
            btnAlterar.Enabled = true;
            btnDeletar.Enabled = true;
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
            txtCodigo.Enabled = false;
            CamposOff();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnNovo.Enabled = true;
            btnAlterar.Enabled = true;
            btnDeletar.Enabled = true;
            txtCodigo.Enabled = false;
            CamposOff();
        }

        private void btnPesqId_Click(object sender, EventArgs e)
        {
            ConexaoProduto conexao = new ConexaoProduto();

            if (txtPesqCod.Text == "")
            {
                MessageBox.Show("Campo de pesquisa por código não pode ser vazio", "AVISO", MessageBoxButtons.OK);
                dgvTabela.DataSource = conexao.CarregarTabela();
            }
            else
            {
                conexao.PesqId = txtPesqCod.Text;
                dgvTabela.DataSource = conexao.PesquisarId();
            }
        }
        private void txtPesqCod_Enter(object sender, EventArgs e)
        {
            AcceptButton = btnPesqId;
        }

        private void btnPesqNome_Click(object sender, EventArgs e)
        {
            ConexaoProduto conexao = new ConexaoProduto();

            if (txtPesqNome.Text == null)
            {
                dgvTabela.DataSource = conexao.CarregarTabela();
            }
            else
            {
                conexao.PesqNome = txtPesqNome.Text;
                dgvTabela.DataSource = conexao.PesquisarNome();
            }
        }
        private void txtPesqNome_Enter(object sender, EventArgs e)
        {
            AcceptButton = btnPesqNome;
        }
    }
}
