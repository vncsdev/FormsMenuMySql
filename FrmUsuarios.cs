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
    public partial class FrmUsuarios : Form
    {
        MySqlConnection conexao;
        MySqlCommand comando;
        String banco = "server=localhost;port=3308;uid=root;pwd=etecjau;database=ds";
        MySqlDataAdapter Adaptador;
        DataTable datTabela;

        public FrmUsuarios()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    conexao = new MySqlConnection(banco);
            //    conexao.Open();
            //    comando = new MySqlCommand("CREATE TABLE IF NOT EXISTS usuarios " +
            //                               "(id integer not null auto_increment primary key, " +
            //                               "nome char(40), " +
            //                               "senha char(20), " +
            //                               "adm boolean)", conexao);
            //    comando.ExecuteNonQuery();
            //    conexao.Close();

            //    btnConsultar.PerformClick();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    conexao.Close();
            //}
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new MySqlConnection(banco);
                conexao.Open();
                comando = new MySqlCommand("insert into usuarios (nome, senha, adm) values (@nome, @senha, @adm)", conexao);
                comando.Parameters.AddWithValue("@nome", txtNome.Text);
                comando.Parameters.AddWithValue("@senha", txtSenha.Text);
                comando.Parameters.AddWithValue("@adm", chkAdm.Checked);

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

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new MySqlConnection(banco);
                conexao.Open();
                comando = new MySqlCommand("update usuarios set nome = @nome, senha = @senha, adm = @adm where id=@id", conexao);
                comando.Parameters.AddWithValue("@nome", txtNome.Text);
                comando.Parameters.AddWithValue("@senha", txtSenha.Text);
                comando.Parameters.AddWithValue("@id", chkAdm.Checked);
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
                if (MessageBox.Show("Deseja excluir o registro", "Exclusão", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    conexao = new MySqlConnection(banco);
                    conexao.Open();
                    comando = new MySqlCommand("delete from usuarios where id=@id", conexao);
                    comando.Parameters.AddWithValue("@id", txtId.Text);
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtId.Clear();
            txtNome.Clear();
            txtSenha.Clear();
            txtPesquisa.Clear();
            chkAdm.Checked = false;

            txtNome.Focus();

            btnConsultar.PerformClick();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvLogin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvUsuarios.RowCount > 0)
            {
                txtId.Text = dgvUsuarios.CurrentRow.Cells[0].Value.ToString();
                txtNome.Text = dgvUsuarios.CurrentRow.Cells[1].Value.ToString();
                txtSenha.Text = dgvUsuarios.CurrentRow.Cells[2].Value.ToString();
                chkAdm.Checked = Convert.ToBoolean(dgvUsuarios.CurrentRow.Cells[3].Value.ToString());
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new MySqlConnection(banco);
                conexao.Open();
                comando = new MySqlCommand("select * from usuarios where nome like @nome order by nome", conexao);
                comando.Parameters.AddWithValue("@nome", txtPesquisa.Text + "%");
                Adaptador = new MySqlDataAdapter(comando);
                Adaptador.Fill(datTabela = new DataTable());
                dgvUsuarios.DataSource = datTabela;
                conexao.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conexao.Close();
            }
        }
    }
}
