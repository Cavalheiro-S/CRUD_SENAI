using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRAB_CRUD.Classes
{
    class ConexaoLogin
    {
        //TELA LOGIN --------------------------
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string NomeUsuario { get; set; }

        //PROPRIEDADES DE CONEXÃO
        string conexao = Properties.Settings.Default.Conexao;
        bool add = false;

        public ConexaoLogin(string usuario, string senha)
        {
            this.Usuario = usuario;
            this.Senha = senha;
        }

        //MÉTODO PARA VALIDAR O LOGIN DO USÚARIO
        public bool ValideLogin()
        {
            add = false;
            SqlConnection con = new SqlConnection(conexao);
            SqlDataReader dr;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Logon.Usuario FROM Logon WHERE Logon.Usuario = @usuario", con);
                cmd.Parameters.Add("@usuario", SqlDbType.VarChar, 20).Value = Usuario;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dr.Close();
                    dr = null;
                    cmd.CommandText = "SELECT Logon.Senha FROM Logon WHERE Logon.Senha = @senha";
                    cmd.Parameters.Add("@senha", SqlDbType.VarChar, 20).Value = Senha;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        add = true;
                        dr.Close();
                        dr = null;
                        cmd.CommandText = "SELECT Logon.NomeUsuario FROM Logon WHERE Logon.Usuario = @usuario";
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            NomeUsuario = dr[0].ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {


            }
            finally
            {
                con.Close();
            }
            return add;
        }
    }
}
