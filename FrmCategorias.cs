using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace _23_09_2021
{
    public partial class FrmCategorias : Form
    {

        MySqlConnection conexao;
        MySqlCommand comando;
        String banco = "server=localhost;port=3306;uid=root;pwd=etecjau;database=ds";
        MySqlDataAdapter Adaptador;
        DataTable datTabela;

        public FrmCategorias()
        {
            InitializeComponent();
        }

        private void FrmCategorias_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    conexao = new MySqlConnection("server=localhost;uid=root;pwd=etecjau");
            //    conexao.Open();
            //    comando = new MySqlCommand("CREATE TABLE IF NOT EXISTS categorias" +
            //                               "(id integer not null auto_increment primary key, " +
            //                               "nome char(40))", conexao);
            //    comando.ExecuteNonQuery();
            //    conexao.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    conexao.Close();
            //}
            btnConsultar.PerformClick();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new MySqlConnection(banco);
                conexao.Open();
                comando = new MySqlCommand("insert into categorias (nome) values (@nome)", conexao);
                comando.Parameters.AddWithValue("@nome", txtNome.Text);
                comando.ExecuteNonQuery();
                conexao.Close();

                btnCancelar.PerformClick();
                btnConsultar.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conexao.Close();
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new MySqlConnection(banco);
                conexao.Open();
                comando = new MySqlCommand("select * from categorias where nome like @nome order by nome", conexao);
                comando.Parameters.AddWithValue("@nome", txtPesquisa.Text + "%");
                Adaptador = new MySqlDataAdapter(comando);
                Adaptador.Fill(datTabela = new DataTable());
                dgvCategorias.DataSource = datTabela;
                conexao.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conexao.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtID.Clear();
            txtNome.Clear();
            txtPesquisa.Clear();

            txtNome.Focus();

            btnConsultar.PerformClick();
        }

        private void dgvCategorias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCategorias.RowCount > 0)
            {
                txtID.Text = dgvCategorias.CurrentRow.Cells[0].Value.ToString();
                txtNome.Text = dgvCategorias.CurrentRow.Cells[1].Value.ToString();
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new MySqlConnection(banco);
                conexao.Open();
                comando = new MySqlCommand("update categorias set nome = @nome where id=@id", conexao);
                comando.Parameters.AddWithValue("@nome", txtNome.Text);
                comando.Parameters.AddWithValue("@id", txtID.Text);
                comando.ExecuteNonQuery();
                conexao.Close();
                btnCancelar.PerformClick();
                btnConsultar.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conexao.Close();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Deseja excluir o registro?", "Exclusão", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    conexao = new MySqlConnection(banco);
                    conexao.Open();
                    comando = new MySqlCommand("delete from categorias where id=@id", conexao);
                    comando.Parameters.AddWithValue("@id", txtID.Text);
                    comando.ExecuteNonQuery();
                    conexao.Close();
                    btnCancelar.PerformClick();
                    btnConsultar.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conexao.Close();
            }
        }

        private void dgvCategorias_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCategorias.RowCount > 0)
            {
                txtID.Text = dgvCategorias.CurrentRow.Cells[0].Value.ToString();
                txtNome.Text = dgvCategorias.CurrentRow.Cells[1].Value.ToString();
            }
        }
    }
}
