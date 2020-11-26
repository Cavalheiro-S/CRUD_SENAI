using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TRAB_CRUD.Classes;
using TRAB_CRUD.Formulários;

namespace TRAB_CRUD
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            Estilos style = new Estilos();
        }
        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void btnFechar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void ckbShow_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbShow.Checked)
            {
                txtSenha.UseSystemPasswordChar = false;
            }
            else
            {
                txtSenha.UseSystemPasswordChar = true;
            }
        }
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtUsuario.Clear();
            txtSenha.Clear();
            txtUsuario.Focus();
        }
        private void btnEntrar_Click(object sender, EventArgs e)
        {
            FrmMenu menu = new FrmMenu();
            ConexaoLogin conexao = new ConexaoLogin(txtUsuario.Text, txtSenha.Text);
            if (conexao.ValideLogin())
            {
                MessageBox.Show("Logado com sucesso");
                this.Hide();
                menu.Show();
                menu.lblNome.Text = conexao.NomeUsuario;
            }
            else
            {
                MessageBox.Show("Erro ao logar ","Erro",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }
    }
}
