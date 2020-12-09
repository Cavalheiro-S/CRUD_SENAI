using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRAB_CRUD.Classes
{
    class ConexaoProduto
    {
        //PROPRIEDADES DO FORMULÁRIO DE PRODUTOS
        public int Codigo { get; set; }
        public string Modelo { get; set; }
        public string SistOpera { get; set; }
        public string Armazenamento { get; set; }
        public decimal Preco { get; set; }
        public string Cor { get; set; }
        public int CodigoMarca { get; set; }
        public bool Importado { get; set; }
        
        //PROPRIEDADES PARA PESQUISAR
        public string PesqNome { get; set; }
        public string PesqId { get; set; }

        public string NomeMarca { get; set; }
        public int CodSelecionado { get; set; }
            
        //PROPRIEDADES DE CONEXÃO
        
        string conexao = Properties.Settings.Default.Conexao;
        bool add = false;

        public ConexaoProduto() { }

        public ConexaoProduto(int codigo,string nomeMarca ,string modelo, string sistOpera, string armazenamento, decimal preco, string cor)
        {
            this.Codigo = codigo;
            this.NomeMarca = nomeMarca;
            this.Modelo = modelo;
            this.SistOpera = sistOpera;
            this.Armazenamento = armazenamento;
            this.Preco = preco;
            this.Cor = cor;
            this.CodigoMarca = CodMarca();
        }

       
        //MÉTODO PARA CARREGAR DATA GRID VIEW
        public DataTable CarregarTabela()
        {
            SqlConnection con = new SqlConnection(conexao);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM vw_Produto", con);
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
        //MÉTODO PARA CARREGAR COMBO BOX 
        public List<Marca> carregarCombo()
        {
            SqlConnection con = new SqlConnection(conexao);
            List<Marca> fornecedores = new List<Marca>();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Marca", con);
                SqlDataReader dr= cmd.ExecuteReader();

                while (dr.Read())
                {
                    fornecedores.Add(new Marca
                    {
                        Nome = dr["Nome"].ToString(),
                        Indice = Convert.ToInt32(dr["Codigo"])
                    });
                }
            }
            catch (Exception)
            {


            }
            finally
            {
                con.Close();
            }
            return fornecedores;
        }

        //MÉTODO COM STORED PROCEDURE PARA INSERIR PRODUTO NO BANCO DE DADOS 
        public bool InserirProduto()
        {
            
            SqlConnection con = new SqlConnection(conexao);
            try
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("sp_inserirProduto", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@codigo", SqlDbType.Int).Value = Codigo;
                cmd.Parameters.AddWithValue("modelo", SqlDbType.VarChar).Value = Modelo;
                cmd.Parameters.AddWithValue("sistOpera", SqlDbType.VarChar).Value = SistOpera;
                cmd.Parameters.AddWithValue("@armazenamento", SqlDbType.VarChar).Value = Armazenamento;
                cmd.Parameters.AddWithValue("@preco", SqlDbType.Decimal).Value = Preco;
                cmd.Parameters.AddWithValue("cor", SqlDbType.VarChar).Value = Cor;
                cmd.Parameters.AddWithValue("@codigoMarca", SqlDbType.Int).Value = CodigoMarca;
                cmd.Parameters.AddWithValue("@importacao", SqlDbType.Bit).Value = Importado;
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

        //MÉTODO COM PROCEDURE PARA ALTERAR PRODUTO
        public bool AlteraProduto()
        {
            SqlConnection con = new SqlConnection(conexao);
            try
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("sp_alterarProduto", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Codigo", SqlDbType.Int).Value = Codigo;
                cmd.Parameters.AddWithValue("@Modelo", SqlDbType.VarChar).Value = Modelo;
                cmd.Parameters.AddWithValue("@SistOpera", SqlDbType.VarChar).Value = SistOpera;
                cmd.Parameters.AddWithValue("@Armazenamento", SqlDbType.VarChar).Value = Armazenamento;
                cmd.Parameters.AddWithValue("@Preco", SqlDbType.Decimal).Value = Preco;
                cmd.Parameters.AddWithValue("@Cor", SqlDbType.VarChar).Value = Cor;
                cmd.Parameters.AddWithValue("@CodigoMarca", SqlDbType.Int).Value = CodigoMarca;
                cmd.Parameters.AddWithValue("@Importacao", SqlDbType.Bit).Value = Importado;
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

        //MÉTODO COM PROCEDURE PARA DELETAR PRODUTO
        public bool DeletarProduto()
        {
            SqlConnection con = new SqlConnection(conexao);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_deletarProduto", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Codigo", SqlDbType.Int).Value = CodSelecionado;
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

        //MÉTODO PARA EXIBIR AS LINHAS SELECIONADAS PELO USÚARIO NO DATA GRID VIEW
        public void ExibirCampos()
        {
            SqlConnection con = new SqlConnection(conexao);
            List<ConexaoProduto> produtos = new List<ConexaoProduto>();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Produto WHERE Codigo = @codigo", con);
                cmd.Parameters.Add("@codigo", SqlDbType.Int).Value = CodSelecionado;
                SqlDataReader dr= cmd.ExecuteReader();

                while (dr.Read())
                {

                    Codigo = Convert.ToInt32(dr["Codigo"].ToString());
                    Modelo = dr["Modelo"].ToString();
                    SistOpera = dr["SistOperacional"].ToString();
                    Armazenamento = dr["Armazenamento"].ToString();
                    Preco = Convert.ToDecimal(dr["Preco"].ToString());
                    Cor = dr["Cor"].ToString();
                    NomePorCodigo(Convert.ToInt32(dr["CodMarca"].ToString()));
                    Importado = Convert.ToBoolean(dr["Importacao"].ToString());

                    
                }

                
            }
            catch (Exception)
            {


            }
            finally
            {
                con.Close();
            }
            
        }

  
        //MÉTODO PARA ACHAR O NOME DA MARCA , COM BASE NO CODIGO INFORMADO
        private void NomePorCodigo(int codMarca)
        {
            SqlConnection con = new SqlConnection(conexao);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Marca.Nome FROM Marca WHERE Codigo = @codigo", con);
                cmd.Parameters.Add("@codigo", SqlDbType.VarChar).Value = codMarca;

                SqlDataReader dr= cmd.ExecuteReader();
                while (dr.Read())
                {
                    NomeMarca = dr["Nome"].ToString();
                }
            }
            catch (Exception)
            {


            }
            finally
            {
                con.Close();
            }
        }


        //MÉTODO PARA ACHAR O CODIGO DO PRODUTO , COM BASE NO NOME
        private int CodMarca()
        {
            SqlConnection con = new SqlConnection(conexao);
            int codigoMarca = 0;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Marca WHERE Nome = @nome", con);
                cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = NomeMarca;
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    codigoMarca = Convert.ToInt32(dr["codigo"].ToString());

                }
                
            }
            catch (Exception)
            {


            }
            finally
            {
                con.Close();
            }
            return codigoMarca;
        }
        //MÉTODO PARA PESQUISAR O NOME DO PRODUTO NO BANCO DE DADOS
        public DataTable PesquisarNome()
        {
            SqlConnection con = new SqlConnection(conexao);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Produto WHERE Modelo LIKE @pesqNome ", con);
                cmd.Parameters.Add("@pesqNome", SqlDbType.VarChar).Value = "%"+PesqNome+"%";
                SqlDataReader dr= cmd.ExecuteReader();
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

        //MÉTODO PARA PESQUISAR O PRODUTO, COM BASE NO CÓDIGO
        public DataTable PesquisarId()
        {
            SqlConnection con = new SqlConnection(conexao);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM vw_Produto WHERE Codigo = @codigo", con);
                cmd.Parameters.Add("@codigo", SqlDbType.VarChar).Value = PesqId;
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
    }
}
