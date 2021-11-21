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
    public partial class FrmClientes : Form
    {

        MySqlConnection conexao;
        MySqlCommand comando;
        String banco = "server=localhost;port=3308;uid=root;pwd=etecjau;database=ds";
        MySqlDataAdapter Adaptador;
        DataTable datTabela;

        public FrmClientes()
        {
            InitializeComponent();
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    conexao = new MySqlConnection("server=localhost;uid=root;pwd=etecjau");
            //    conexao.Open();

            //    comando = new MySqlCommand("CREATE TABLE IF NOT EXISTS clientes" + 
            //                               "(id integer not null auto_increment primary key, " + 
            //                               "nome char(40) not null," + 
            //                               "rg char(12) not null, " + 
            //                               "cpf char(14) not null, " + 
            //                               "data_nascimento date not null, " + 
            //                               "endereco char(50) not null, " + 
            //                               "numero smallint not null, " + 
            //                               "complemento varchar(50), " + 
            //                               "bairro char(40) not null, " + 
            //                               "id_cidade smallint not null, " +
            //                               "cep char(10) not null, " +
            //                               "telefone char(13), " +
            //                               "celular char(14) not null, " +
            //                               "email varchar(50) not null, " +
            //                               "ativo bool not null, " +
            //                               "foto char(100) not null)", conexao);
            //    comando.ExecuteNonQuery();

            //    conexao.Close();
            //    btnConsultar.PerformClick();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    conexao.Close();
            //}
            try
            {
                conexao = new MySqlConnection(banco);
                conexao.Open();
                comando = new MySqlCommand("select * from cidades order by nome", conexao);
                Adaptador = new MySqlDataAdapter(comando);
                Adaptador.Fill(datTabela = new DataTable());
                cboCidades.DataSource = datTabela;
                cboCidades.DisplayMember = "nome";
                cboCidades.ValueMember = "id";
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

        private void btnFechar_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new MySqlConnection(banco);
                conexao.Open();
                comando = new MySqlCommand("select a.*, c.nome cidade, c.uf uf from clientes a left join cidades " +
                                           "c on (a.id_cidade = c.id) where a.nome like @nome order by a.nome", conexao);
                comando.Parameters.AddWithValue("@nome", txtPesquisa.Text + "%");
                Adaptador = new MySqlDataAdapter(comando);
                Adaptador.Fill(datTabela = new DataTable());
                dgvClientes.DataSource = datTabela;
                conexao.Close();

                dgvClientes.Columns["id_cidade"].Visible = false;

                dgvClientes.Columns["id"].HeaderText = "Código";
                dgvClientes.Columns["id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvClientes.Columns["nome"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvClientes.Columns["rg"].Width = 100;
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
            txtUf.Clear();
            txtPesquisa.Clear();
            txtEmail.Clear();
            txtBairro.Clear();
            txtComplemento.Clear();
            txtEndereco.Clear();
            txtNum.Clear();
            txtUf.Clear();
            txtRG.Clear();
            mtbCelular.Clear();
            mtbCep.Clear();
            mtbTelefone.Clear();
            mtbCPF.Clear();
            mtbDataNascimento.Clear();
            cboCidades.SelectedIndex = -1;
            chkAtivo.Checked = false;
            txtNome.Focus();
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new MySqlConnection(banco);
                conexao.Open();
                comando = new MySqlCommand("insert into clientes (nome, rg, cpf, data_nascimento, endereco, numero, complemento, " + 
                                            "bairro, id_cidade, cep, telefone, celular, email, ativo, foto) " + 
                                            "values (@nome, @rg, @cpf, @data_nascimento, @endereco, @numero, @complemento, " +
                                            "@bairro, @id_cidade, @cep, @telefone, @celular, @email, @ativo, @foto)", conexao);
                comando.Parameters.AddWithValue("@nome", txtNome.Text);
                comando.Parameters.AddWithValue("@rg", txtRG.Text);
                comando.Parameters.AddWithValue("@cpf", mtbCPF.Text);
                comando.Parameters.AddWithValue("@data_nascimento", Convert.ToDateTime(mtbDataNascimento.Text));
                comando.Parameters.AddWithValue("@endereco", txtEndereco.Text);
                comando.Parameters.AddWithValue("@numero", txtNum.Text);
                comando.Parameters.AddWithValue("@complemento", txtComplemento.Text);
                comando.Parameters.AddWithValue("@bairro", txtBairro.Text);
                comando.Parameters.AddWithValue("@id_cidade", cboCidades.SelectedValue);
                comando.Parameters.AddWithValue("@cep", mtbCep.Text);
                comando.Parameters.AddWithValue("@telefone", mtbTelefone.Text);
                comando.Parameters.AddWithValue("@celular", mtbCelular.Text);
                comando.Parameters.AddWithValue("@email", txtEmail.Text);
                comando.Parameters.AddWithValue("@ativo", chkAtivo.Checked);
                comando.Parameters.AddWithValue("@foto", picFoto.ImageLocation);
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

        private void cboCidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCidades.SelectedIndex != -1)
            {
                DataRowView reg = (DataRowView)cboCidades.SelectedItem;
                txtUf.Text = reg["uf"].ToString();
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new MySqlConnection(banco);
                conexao.Open();
                comando = new MySqlCommand("update clientes set nome = @nome, " +
                                           "endereco = @endereco, " +
                                           "numero = @numero, " +
                                           "rg = @rg, " +
                                           "cpf = @cpf, " +
                                           "data_nascimento = @data_nascimento, " +
                                           "complemento = @complemento,  " +
                                           "bairro = @bairro, " +
                                           "id_cidade = @id_cidade, " +
                                           "cep = @cep, " +
                                           "telefone = @telefone, " +
                                           "celular = @celular, " +
                                           "email = @email, " +
                                           "ativo = @ativo, " +
                                           "foto = @foto " +
                                           "where id=@id", conexao);
                comando.Parameters.AddWithValue("@id", txtId.Text);
                comando.Parameters.AddWithValue("@nome", txtNome.Text);
                comando.Parameters.AddWithValue("@endereco", txtEndereco.Text);
                comando.Parameters.AddWithValue("@numero", txtNum.Text);
                comando.Parameters.AddWithValue("@rg", txtRG.Text);
                comando.Parameters.AddWithValue("@cpf", mtbCPF.Text);
                comando.Parameters.AddWithValue("@data_nascimento", Convert.ToDateTime(mtbDataNascimento.Text));
                comando.Parameters.AddWithValue("@complemento", txtComplemento.Text);
                comando.Parameters.AddWithValue("@bairro", txtBairro.Text);
                comando.Parameters.AddWithValue("@id_cidade", cboCidades.SelectedValue);
                comando.Parameters.AddWithValue("@cep", mtbCep.Text);
                comando.Parameters.AddWithValue("@telefone", mtbTelefone.Text);
                comando.Parameters.AddWithValue("@celular", mtbCelular.Text);
                comando.Parameters.AddWithValue("@email", txtEmail.Text);
                comando.Parameters.AddWithValue("@ativo", chkAtivo.Checked);
                comando.Parameters.AddWithValue("@foto", picFoto.ImageLocation);
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
                    comando = new MySqlCommand("delete from clientes where id=@id", conexao);
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

        private void picFoto_Click(object sender, EventArgs e)
        {
            ofdFoto.InitialDirectory = "D:\\fotos\\";

            ofdFoto.FileName = "";

            ofdFoto.ShowDialog();

            picFoto.ImageLocation = ofdFoto.FileName;   
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvClientes.RowCount > 0)
            {
                txtId.Text = dgvClientes.CurrentRow.Cells[0].Value.ToString();
                txtNome.Text = dgvClientes.CurrentRow.Cells[1].Value.ToString();
                txtRG.Text = dgvClientes.CurrentRow.Cells[2].Value.ToString();
                mtbCPF.Text = dgvClientes.CurrentRow.Cells[3].Value.ToString();
                mtbDataNascimento.Text = dgvClientes.CurrentRow.Cells[4].Value.ToString();
                txtEndereco.Text = dgvClientes.CurrentRow.Cells[5].Value.ToString();
                txtNum.Text = dgvClientes.CurrentRow.Cells[6].Value.ToString();
                txtComplemento.Text = dgvClientes.CurrentRow.Cells[7].Value.ToString();
                txtBairro.Text = dgvClientes.CurrentRow.Cells[8].Value.ToString();
                cboCidades.Text = dgvClientes.CurrentRow.Cells[9].Value.ToString();
                mtbCep.Text = dgvClientes.CurrentRow.Cells[10].Value.ToString();
                mtbTelefone.Text = dgvClientes.CurrentRow.Cells[11].Value.ToString();
                mtbCelular.Text = dgvClientes.CurrentRow.Cells[12].Value.ToString();
                txtEmail.Text = dgvClientes.CurrentRow.Cells[13].Value.ToString();
                picFoto.ImageLocation = dgvClientes.CurrentRow.Cells[15].Value.ToString();
            }
        }
    }
}
