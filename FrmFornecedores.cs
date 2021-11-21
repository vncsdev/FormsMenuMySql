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
    public partial class FrmFornecedores : Form
    {
        MySqlConnection conexao;
        MySqlCommand comando;
        String banco = "server=localhost;port=3308;uid=root;pwd=etecjau;database=ds";
        MySqlDataAdapter Adaptador;
        DataTable datTabela;

        public FrmFornecedores()
        {
            InitializeComponent();
        }

        private void FrmFornecedores_Load(object sender, EventArgs e)
        {
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

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new MySqlConnection(banco);
                conexao.Open();
                comando = new MySqlCommand("select * from fornecedores where fantasia like @fantasia order by fantasia", conexao);
                comando.Parameters.AddWithValue("@fantasia", txtPesquisa.Text + "%");
                Adaptador = new MySqlDataAdapter(comando);
                Adaptador.Fill(datTabela = new DataTable());
                dgvFornecedores.DataSource = datTabela;
                conexao.Close();

                dgvFornecedores.Columns["id_cidade"].Visible = false;

                dgvFornecedores.Columns["id"].HeaderText = "Código";
                dgvFornecedores.Columns["razao_social"].HeaderText = "Razão Social";
                dgvFornecedores.Columns["fantasia"].HeaderText = "Nome Fantasia";
                dgvFornecedores.Columns["cnpj"].HeaderText = "CNPJ";
                dgvFornecedores.Columns["ie"].HeaderText = "Inscrição Estadual";
                dgvFornecedores.Columns["endereco"].HeaderText = "Endereço";
                dgvFornecedores.Columns["numero"].HeaderText = "Número";
                dgvFornecedores.Columns["complemento"].HeaderText = "Complemento";
                dgvFornecedores.Columns["bairro"].HeaderText = "Bairro";
                dgvFornecedores.Columns["cep"].HeaderText = "CEP";
                dgvFornecedores.Columns["fone"].HeaderText = "Telefone";
                dgvFornecedores.Columns["representante"].HeaderText = "Representante";
                dgvFornecedores.Columns["email"].HeaderText = "Email";
                dgvFornecedores.Columns["foto"].HeaderText = "Logo";

                dgvFornecedores.Columns["id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvFornecedores.Columns["id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvFornecedores.Columns["razao_social"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvFornecedores.Columns["cnpj"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

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
            txtBairro.Clear();
            txtComplemento.Clear();
            txtEmail.Clear();
            txtEndereco.Clear();
            txtNomeFantasia.Clear();
            txtNum.Clear();
            txtPesquisa.Clear();
            txtRazaoSocial.Clear();
            txtUf.Clear();
            mtbCep.Clear();
            mtbCNPJ.Clear();
            mtbIE.Clear();
            mtbRepresentante.Clear();
            mtbTelefone.Clear();
            cboCidades.SelectedIndex = -1;
            txtRazaoSocial.Focus();
            picFoto.ImageLocation = "";

            txtRazaoSocial.Focus();
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new MySqlConnection(banco);
                conexao.Open();
                comando = new MySqlCommand("insert into fornecedores (razao_social, fantasia, cnpj, ie, endereco, numero, complemento, bairro, cep, id_cidade, " +
                                           "fone, representante, email, foto) " +
                                           "values (@razao_social, @fantasia, @cnpj, @ie, @endereco, @numero, @complemento, @bairro, @cep, @id_cidade, " +
                                           "@fone, @representante, @email, @foto)", conexao);
                comando.Parameters.AddWithValue("@razao_social", txtRazaoSocial.Text);
                comando.Parameters.AddWithValue("@fantasia", txtNomeFantasia.Text);
                comando.Parameters.AddWithValue("@cnpj", mtbCNPJ.Text);
                comando.Parameters.AddWithValue("@ie", mtbIE.Text);
                comando.Parameters.AddWithValue("@endereco", txtEndereco.Text);
                comando.Parameters.AddWithValue("@numero", txtNum.Text);
                comando.Parameters.AddWithValue("@complemento", txtComplemento.Text);
                comando.Parameters.AddWithValue("@bairro", txtBairro.Text);
                comando.Parameters.AddWithValue("@cep", mtbCep.Text);
                comando.Parameters.AddWithValue("@id_cidade", cboCidades.SelectedValue);
                comando.Parameters.AddWithValue("@fone", mtbTelefone.Text);
                comando.Parameters.AddWithValue("@representante", mtbRepresentante.Text);
                comando.Parameters.AddWithValue("@email", txtEmail.Text);
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

        private void dgvFornecedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvFornecedores.RowCount > 0)
            {
                txtId.Text = dgvFornecedores.CurrentRow.Cells[0].Value.ToString();
                txtRazaoSocial.Text = dgvFornecedores.CurrentRow.Cells[1].Value.ToString();
                txtNomeFantasia.Text = dgvFornecedores.CurrentRow.Cells[2].Value.ToString();
                mtbCNPJ.Text = dgvFornecedores.CurrentRow.Cells[3].Value.ToString();
                mtbIE.Text = dgvFornecedores.CurrentRow.Cells[4].Value.ToString();
                txtEndereco.Text = dgvFornecedores.CurrentRow.Cells[5].Value.ToString();
                txtNum.Text = dgvFornecedores.CurrentRow.Cells[6].Value.ToString();
                txtComplemento.Text = dgvFornecedores.CurrentRow.Cells[7].Value.ToString();
                txtBairro.Text = dgvFornecedores.CurrentRow.Cells[8].Value.ToString();
                mtbCep.Text = dgvFornecedores.CurrentRow.Cells[9].Value.ToString();
                cboCidades.Text = dgvFornecedores.CurrentRow.Cells[10].Value.ToString();
                mtbTelefone.Text = dgvFornecedores.CurrentRow.Cells[11].Value.ToString();
                mtbRepresentante.Text = dgvFornecedores.CurrentRow.Cells[12].Value.ToString();
                txtEmail.Text = dgvFornecedores.CurrentRow.Cells[13].Value.ToString();
                picFoto.ImageLocation = dgvFornecedores.CurrentRow.Cells[14].Value.ToString();
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new MySqlConnection(banco);
                conexao.Open();
                comando = new MySqlCommand("update fornecedores set razao_social = @razao_social, " +
                                           "fantasia = @fantasia, " +
                                           "endereco = @endereco, " +
                                           "numero = @numero, " +
                                           "complemento = @complemento, " +
                                           "bairro = @bairro, " +
                                           "cep = @cep, " +
                                           "id_cidade = @id_cidade, " +
                                           "cnpj = @cnpj, " +
                                           "ie = @ie, " +
                                           "fone = @fone, " +
                                           "representante = @representante, " +
                                           "email = @email, " +
                                           "foto = @foto " +
                                           "where id=@id", conexao);
                comando.Parameters.AddWithValue("@id", txtId.Text);
                comando.Parameters.AddWithValue("@razao_social", txtRazaoSocial.Text);
                comando.Parameters.AddWithValue("@fantasia", txtNomeFantasia.Text);
                comando.Parameters.AddWithValue("@cnpj", mtbCNPJ.Text);
                comando.Parameters.AddWithValue("@ie", mtbIE.Text);
                comando.Parameters.AddWithValue("@endereco", txtEndereco.Text);
                comando.Parameters.AddWithValue("@numero", txtNum.Text);
                comando.Parameters.AddWithValue("@complemento", txtComplemento.Text);
                comando.Parameters.AddWithValue("@bairro", txtBairro.Text);
                comando.Parameters.AddWithValue("@cep", mtbCep.Text);
                comando.Parameters.AddWithValue("@id_cidade", cboCidades.SelectedValue);
                comando.Parameters.AddWithValue("@fone", mtbTelefone.Text);
                comando.Parameters.AddWithValue("@representante", mtbRepresentante.Text);
                comando.Parameters.AddWithValue("@email", txtEmail.Text);
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

        private void picFoto_Click(object sender, EventArgs e)
        {
            ofdFoto.InitialDirectory = "D:\\fotos\\";

            ofdFoto.FileName = "";

            ofdFoto.ShowDialog();

            picFoto.ImageLocation = ofdFoto.FileName;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Deseja excluir o registro?", "Exclusão", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    conexao = new MySqlConnection(banco);
                    conexao.Open();
                    comando = new MySqlCommand("delete from fornecedores where id=@id", conexao);
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
    }
}
