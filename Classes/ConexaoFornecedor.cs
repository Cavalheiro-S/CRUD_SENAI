using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TRAB_CRUD.Formulários
{
    class ConexaoFornecedor
    {

        //TELA FORNECEDOR ----------------------
        public string Fornecedor { get; set; }
        public string Selecionado { get; set; }

        //PROPRIEDADES PARA PESQUISA
        public string NomePesquisar { get; set; }
        public string CodPesquisar { get; set; }

        public string CodNovo { get; set; }
        public string NomeNovo { get; set; }
        //-------------------------------------
        private string conexao = Properties.Settings.Default.Conexao;
        
        private bool add = false;

        public ConexaoFornecedor( string fornecedor)
        {
            this.Fornecedor = fornecedor;
        }
        public ConexaoFornecedor() { }



        //MÉTODO PARA ADICIONAR NOVO FORNECEDOR
        public bool NovoFornecedor()
        
        {
            add = false;
            SqlConnection con = new SqlConnection(conexao);
            try
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Marca VALUES(@codigo,@nome)", con);

                cmd.Parameters.Add("@codigo", SqlDbType.Int).Value = CodNovo;
                cmd.Parameters.Add("@nome", SqlDbType.VarChar, 30).Value = NomeNovo;

                int resu = cmd.ExecuteNonQuery();

                if (resu > 0)
                {
                    add = true;
                }

                
            }
            catch (Exception )
            {

            }
            finally
            {
                con.Close();
            }

            return add;
        }

        //MÉTODO PARA ALTERAR FORNECEDOR 
        public bool Alterar()
        {
            add = false;
            SqlConnection con = new SqlConnection(conexao);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Marca SET Nome = @nome WHERE Codigo = @codigo", con);
                cmd.Parameters.Add("@codigo", SqlDbType.VarChar, 30).Value = CodNovo;
                cmd.Parameters.Add("@nome", SqlDbType.VarChar, 30).Value = NomeNovo;
                int resu = cmd.ExecuteNonQuery();
                if (resu > 0)
                {
                    add = true;
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
        //MÉTODO PARA DELETAR FORNECEDOR
        public bool Deletar()
        {
            add = false;
            SqlConnection con = new SqlConnection(conexao);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Marca WHERE Nome = @selecionado", con);
                cmd.Parameters.Add("@selecionado", SqlDbType.VarChar, 30).Value = Selecionado;

                int resu = cmd.ExecuteNonQuery();

                if(resu > 0)
                {
                    add = true;
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

 
        //MÉTODO PARA PESQUISAR NOME NA BASE DE DADOS
        public DataTable PesquisarNome()
        {
            add = false;
            SqlConnection con = new SqlConnection(conexao);
            DataTable dt = new DataTable();

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                if (NomePesquisar == null || NomePesquisar == "")
                {
                    cmd.CommandText = "SELECT * FROM Marca ";
                }
                else
                {
                    cmd.CommandText = "SELECT * FROM Marca WHERE Nome LIKE @NomePesquisar " ;
                    cmd.Parameters.Add("@NomePesquisar", SqlDbType.VarChar, 30).Value = "%"+ NomePesquisar+"%";
                }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception)
            {


            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        //MÉTODO PARA PESQUISAR FORNECEDOR PELO CÓDIGO NA BASE DE DADOS
        public DataTable PesquisarCod()
        {
            add = false;
            SqlConnection con = new SqlConnection(conexao);
            DataTable dt = new DataTable();

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                if (CodPesquisar == null || CodPesquisar == "")
                {
                    cmd.CommandText = "SELECT * FROM Marca ";
                }
                else
                {
                    cmd.CommandText = "SELECT * FROM Marca WHERE Codigo = @codPesquisar ";
                    cmd.Parameters.Add("@codPesquisar", SqlDbType.VarChar, 30).Value = CodPesquisar;
                }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception)
            {


            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        //MÉTODO PARA CARREGAR O DATA GRID VIEW DO FORMULÁRIO DE FORNECEDOR
        public DataTable LoadTabela()
        {
            SqlConnection con = new SqlConnection(conexao);
            DataTable table = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Marca", con);

                table.Load(cmd.ExecuteReader());

            }
            catch (Exception)
            {

            }
            finally
            {
                con.Close();
            }

            return table;
        }

    }
}
