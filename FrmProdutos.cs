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
    public partial class FrmProdutos : Form
    {
        MySqlConnection conexao;
        MySqlCommand comando;
        String banco = "server=localhost;port=3308;uid=root;pwd=etecjau;database=ds";
        MySqlDataAdapter Adaptador;
        DataTable datTabela;

        public FrmProdutos()
        {
            InitializeComponent();
        }

        private void FrmProdutos_Load(object sender, EventArgs e)
        {
            try
            {
                conexao = new MySqlConnection(banco);
                conexao.Open();
                comando = new MySqlCommand("select * from categorias order by nome", conexao);
                Adaptador = new MySqlDataAdapter(comando);
                Adaptador.Fill(datTabela = new DataTable());
                cboCategoria.DataSource = datTabela;
                cboCategoria.DisplayMember = "nome";
                cboCategoria.ValueMember = "id";
                conexao.Close();
                btnCancelar.PerformClick();
                btnConsultar.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conexao.Close();
            }
            try
            {
                conexao = new MySqlConnection(banco);
                conexao.Open();
                comando = new MySqlCommand("select * from fornecedores order by fantasia", conexao);
                Adaptador = new MySqlDataAdapter(comando);
                Adaptador.Fill(datTabela = new DataTable());
                cboFornecedor.DataSource = datTabela;
                cboFornecedor.DisplayMember = "fantasia";
                cboFornecedor.ValueMember = "id";
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

        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new MySqlConnection(banco);
                conexao.Open();
                comando = new MySqlCommand("insert into produtos (descricao, codigo_barras, id_categoria, id_fornecedor, estoque, estoque_min, " +
                                           "valor_venda, valor_custo, foto, video) " +
                                           "values (@descricao, @codigo_barras, @id_categoria, @id_fornecedor, @estoque, @estoque_min, " +
                                           "@valor_venda, @valor_custo, @foto, @video)", conexao);
                comando.Parameters.AddWithValue("@descricao", txtDescricao.Text);
                comando.Parameters.AddWithValue("@codigo_barras", txtCodigoDeBarras.Text);
                comando.Parameters.AddWithValue("@id_categoria", cboCategoria.SelectedValue);
                comando.Parameters.AddWithValue("@id_fornecedor", cboFornecedor.SelectedValue);
                comando.Parameters.AddWithValue("@estoque", txtEstoque.Text);
                comando.Parameters.AddWithValue("@estoque_min", txtEstoqueMinimo.Text);
                comando.Parameters.AddWithValue("@valor_venda", txtValorVenda.Text);
                comando.Parameters.AddWithValue("@valor_custo", txtValorCusto.Text);
                comando.Parameters.AddWithValue("@foto", picFoto.ImageLocation);
                comando.Parameters.AddWithValue("@video", txtVideo.Text);
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
                comando = new MySqlCommand("select * from produtos where descricao like @descricao order by descricao", conexao);
                comando.Parameters.AddWithValue("@descricao", txtPesquisa.Text + "%");
                Adaptador = new MySqlDataAdapter(comando);
                Adaptador.Fill(datTabela = new DataTable());
                dgvProdutos.DataSource = datTabela;
                conexao.Close();

                dgvProdutos.Columns["id_fornecedor"].Visible = false;
                dgvProdutos.Columns["id_categoria"].Visible = false;

                dgvProdutos.Columns["id"].HeaderText = "Código";
                dgvProdutos.Columns["descricao"].HeaderText = "Descrição";
                dgvProdutos.Columns["codigo_barras"].HeaderText = "Código de Barras";
                dgvProdutos.Columns["estoque"].HeaderText = "Estoque";
                dgvProdutos.Columns["estoque_min"].HeaderText = "Estoque Mínimo";
                dgvProdutos.Columns["valor_venda"].HeaderText = "Valor de Venda";
                dgvProdutos.Columns["valor_custo"].HeaderText = "Valor de Custo";
                dgvProdutos.Columns["foto"].HeaderText = "Foto";
                dgvProdutos.Columns["video"].HeaderText = "Vídeo";

                dgvProdutos.Columns["id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvProdutos.Columns["id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


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

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new MySqlConnection(banco);
                conexao.Open();
                comando = new MySqlCommand("update produtos set descricao = @descricao, " +
                                           "codigo_barras = @codigo_barras, " +
                                           "id_categoria = @id_categoria, " +
                                           "id_fornecedor = @id_fornecedor, " +
                                           "estoque = @estoque, " +
                                           "estoque_min = @estoque_min, " +
                                           "valor_venda = @valor_venda, " +
                                           "valor_custo = @valor_custo, " +
                                           "foto = @foto, " +
                                           "video = @video " +
                                           "where id=@id", conexao);
                comando.Parameters.AddWithValue("@id", txtId.Text);
                comando.Parameters.AddWithValue("@descricao", txtDescricao.Text);
                comando.Parameters.AddWithValue("@codigo_barras", txtCodigoDeBarras.Text);
                comando.Parameters.AddWithValue("@id_categoria", cboCategoria.SelectedValue);
                comando.Parameters.AddWithValue("@id_fornecedor", cboFornecedor.SelectedValue);
                comando.Parameters.AddWithValue("@estoque", txtEstoque.Text);
                comando.Parameters.AddWithValue("@estoque_min", txtEstoqueMinimo.Text);
                comando.Parameters.AddWithValue("@valor_venda", txtValorVenda.Text);
                comando.Parameters.AddWithValue("@valor_custo", txtValorCusto.Text);
                comando.Parameters.AddWithValue("@foto", picFoto.ImageLocation);
                comando.Parameters.AddWithValue("@video", txtVideo.Text);
                
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

        private void dgvProdutos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProdutos.RowCount > 0)
            {
                txtId.Text = dgvProdutos.CurrentRow.Cells[0].Value.ToString();
                txtDescricao.Text = dgvProdutos.CurrentRow.Cells[1].Value.ToString();
                txtCodigoDeBarras.Text = dgvProdutos.CurrentRow.Cells[2].Value.ToString();
                cboCategoria.Text = dgvProdutos.CurrentRow.Cells[3].Value.ToString();
                cboFornecedor.Text = dgvProdutos.CurrentRow.Cells[4].Value.ToString();
                txtEstoque.Text = dgvProdutos.CurrentRow.Cells[5].Value.ToString();
                txtEstoqueMinimo.Text = dgvProdutos.CurrentRow.Cells[6].Value.ToString();
                txtValorVenda.Text = dgvProdutos.CurrentRow.Cells[7].Value.ToString();
                txtValorCusto.Text = dgvProdutos.CurrentRow.Cells[8].Value.ToString();
                picFoto.ImageLocation = dgvProdutos.CurrentRow.Cells[9].Value.ToString();
                txtVideo.Text = dgvProdutos.CurrentRow.Cells[10].Value.ToString();
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
                    comando = new MySqlCommand("delete from produtos where id=@id", conexao);
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
            {
                ofdFoto.InitialDirectory = "D:\\fotos\\";

                ofdFoto.FileName = "";

                ofdFoto.ShowDialog();

                picFoto.ImageLocation = ofdFoto.FileName;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtId.Clear();
            txtCodigoDeBarras.Clear();
            txtDescricao.Clear();
            txtEstoque.Clear();
            txtEstoqueMinimo.Clear();
            txtPesquisa.Clear();
            txtValorCusto.Clear();
            txtValorVenda.Clear();
            txtVideo.Clear();
            cboCategoria.SelectedIndex = -1;
            cboFornecedor.SelectedIndex = -1;
            
            picFoto.ImageLocation = "";

            txtDescricao.Focus();
        }
    }
}
