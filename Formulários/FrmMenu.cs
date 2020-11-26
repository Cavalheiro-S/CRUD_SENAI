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
    public partial class FrmMenu : Form
    {
        Panel pnlFornecedor ;
        public FrmMenu()
        {
            InitializeComponent();
        }
        private void MenuFornecedor()
        {
            pnlFornecedor = new Panel();
            this.Controls.Add(pnlFornecedor);

            btnFornecedor.MouseMove += new MouseEventHandler(cima);
            btnFornecedor.MouseLeave += new EventHandler(saiu);
            
            void cima (object sender , EventArgs e)
            {
                this.pnlFornecedor.Size = new Size(185, 100);
                this.pnlFornecedor.Name = "pnlFornecedor";
                this.pnlFornecedor.Location = new Point(186, 100);
                this.pnlFornecedor.BackColor = Color.Black;
            }

            void saiu (object sender , EventArgs e)
            {
                this.Controls.Remove(pnlFornecedor);
            }
        }
        private void FrmMenu_Load(object sender, EventArgs e)
        {
            ConexaoFornecedor conexao = new ConexaoFornecedor();
            Estilos style = new Estilos();
            style.CorBotaoBranco(btnProduto);
            style.CorBotaoBranco(btnFornecedor);
        }
        private void FrmMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void btnFornecedor_Click(object sender, EventArgs e)
        {
            FrmFornecedor fornecedor = new FrmFornecedor();
            fornecedor.Show();
        }
        private void btnProduto_Click(object sender, EventArgs e)
        {
            FrmProduto produto = new FrmProduto();
            produto.Show();
        }
    }
}
