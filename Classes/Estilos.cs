using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TRAB_CRUD.Classes
{
    public class Estilos
    {
        public void CorBotaoBranco(Button btn)
        {
            btn.MouseMove += new MouseEventHandler(cima);
            btn.MouseLeave += new EventHandler(saiu);


            void cima(object sender, EventArgs e)
            {
                btn.BackColor = Color.RoyalBlue;
                btn.ForeColor = Color.White;
            }
            void saiu(object sender, EventArgs e)
            {
                btn.BackColor = Color.Empty;
                btn.ForeColor = Color.White;
                
            }
        }
        public void CorBotaoPreto(Button btn)
        {
            btn.MouseMove += new MouseEventHandler(cima);
            btn.MouseLeave += new EventHandler(saiu);
            btn.FlatStyle = FlatStyle.Flat;

            void cima(object sender, EventArgs e)
            {
                btn.BackColor = Color.RoyalBlue;
                btn.ForeColor = Color.Black;
            }
            void saiu(object sender, EventArgs e)
            {
                btn.BackColor = Color.Empty;
                btn.ForeColor = Color.Black;
            }
        }

        public void CorBotaoAzul(Button btn)
        {
            btn.MouseMove += new MouseEventHandler(cima);
            btn.MouseLeave += new EventHandler(saiu);

            void cima(object sender, EventArgs e)
            {
                btn.BackColor = Color.FromArgb(31,143,209);
                btn.ForeColor = Color.White;
            }
            void saiu(object sender, EventArgs e)
            {
                btn.BackColor = Color.RoyalBlue;
                btn.ForeColor = Color.White;
            }
        }

        public void CorBotaoFechar(Button btn)
        {
            btn.MouseMove += new MouseEventHandler(cima);
            btn.MouseLeave += new EventHandler(saiu);

            void cima(object sender, EventArgs e)
            {
                btn.ForeColor = Color.White;
                btn.BackColor = Color.Red;
            }
            void saiu(object sender, EventArgs e)
            {
                btn.ForeColor = Color.White;
                btn.BackColor = Color.Empty;
            }
        }
    




        
    }
}
