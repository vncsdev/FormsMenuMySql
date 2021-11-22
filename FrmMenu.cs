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
    public partial class frmMenu : Form
    {
        MySqlConnection conexao = new MySqlConnection("server=localhost;port=3306;uid=root;pwd=etecjau");
        MySqlCommand comando;

        public frmMenu()
        {
            InitializeComponent();
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            try
            {
                
                conexao.Open();

                comando = new MySqlCommand("CREATE DATABASE IF NOT EXISTS ds; USE ds;" , conexao);
                comando.ExecuteNonQuery();

                comando = new MySqlCommand("CREATE TABLE IF NOT EXISTS Cidades " +
                                           "(id integer not null auto_increment primary key, " +
                                           "nome char(40), " +
                                           "uf char(02))", conexao);
                comando.ExecuteNonQuery();

                comando = new MySqlCommand("CREATE TABLE IF NOT EXISTS categorias" +
                                           "(id integer not null auto_increment primary key, " +
                                           "nome char(40))", conexao);
                comando.ExecuteNonQuery();

                comando = new MySqlCommand("CREATE TABLE IF NOT EXISTS usuarios " +
                                           "(id integer not null auto_increment primary key, " +
                                           "nome char(40), " +
                                           "senha char(20), " +
                                           "adm boolean)", conexao);
                comando.ExecuteNonQuery();

                comando = new MySqlCommand("CREATE TABLE IF NOT EXISTS clientes" +
                                           "(id integer not null auto_increment primary key, " +
                                           "nome char(40) not null," +
                                           "rg char(12) not null, " +
                                           "cpf char(14) not null, " +
                                           "data_nascimento date not null, " +
                                           "endereco char(50) not null, " +
                                           "numero smallint not null, " +
                                           "complemento varchar(50), " +
                                           "bairro char(40) not null, " +
                                           "id_cidade smallint not null, " +
                                           "cep char(10) not null, " +
                                           "telefone char(13), " +
                                           "celular char(14) not null, " +
                                           "email varchar(50) not null, " +
                                           "ativo bool not null, " +
                                           "foto char(100) not null)", conexao);
                comando.ExecuteNonQuery();

                comando = new MySqlCommand("CREATE TABLE IF NOT EXISTS fornecedores " +
                                           "(id integer not null auto_increment primary key, " +
                                           "razao_social char(40), " +
                                           "fantasia char(30), " +
                                           "cnpj char(18), " +
                                           "ie char(15), " +
                                           "endereco char(40), " +
                                           "numero char(10), " +
                                           "complemento char(30), " +
                                           "bairro char(30), " +
                                           "cep char(30), " +
                                           "id_cidade int(11), " +                                       
                                           "fone char(14), " +
                                           "representante char(14), " +
                                           "email varchar(60), " +
                                           "foto varchar(100))", conexao);
                comando.ExecuteNonQuery();

                comando = new MySqlCommand("CREATE TABLE IF NOT EXISTS produtos " +
                                           "(id integer not null auto_increment primary key, " +
                                           "descricao char(40), " +
                                           "codigo_barras char(20), " +
                                           "id_categoria int(11), " +
                                           "id_fornecedor int(11), " +
                                           "estoque decimal(10,3), " +
                                           "estoque_min decimal(10,3), " +
                                           "valor_venda decimal(10,2), " +
                                           "valor_custo decimal(10,2), " +
                                           "foto varchar(100), " +
                                           "video varchar(100))", conexao);
                comando.ExecuteNonQuery();

                conexao.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conexao.Close();
            }
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmClientes form = new FrmClientes();
            form.Show();
        }

        private void finalizarOSistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fornecedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmFornecedores form = new FrmFornecedores();
            form.Show();
        }

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCategorias form = new FrmCategorias();
            form.Show();
        }

        private void produtosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProdutos form = new FrmProdutos();
            form.Show();
        }

        private void pedidoDeCompraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPedidoDeCompra form = new FrmPedidoDeCompra();
            form.Show();
        }

        private void entradaDeMercadoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmEntradaMercadorias form = new FrmEntradaMercadorias();
            form.Show();
        }

        private void orçamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmOrcamentos form = new FrmOrcamentos();
            form.Show();
        }

        private void pedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPedidos form = new FrmPedidos();
            form.Show();
        }

        private void contasAPagarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmContasPagar form = new FrmContasPagar();
            form.Show();
        }

        private void contasAReceberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmContasReceber form = new FrmContasReceber();
            form.Show();
        }

        private void clientesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmRelClientes form = new FrmRelClientes();
            form.Show();
        }

        private void produtosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmRelProdutos form = new FrmRelProdutos();
            form.Show();
        }

        private void comprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRelCompras form = new FrmRelCompras();
            form.Show();
        }

        private void vendasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmRelVendas form = new FrmRelVendas();
            form.Show();
        }

        private void contasAPagarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmRelContasPagar form = new FrmRelContasPagar();
            form.Show();
        }

        private void contasAReceberToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmRelContasReceber form = new FrmRelContasReceber();
            form.Show();
        }
        private void cidadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCidades form = new frmCidades();
            form.Show();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmUsuarios form = new FrmUsuarios();
            form.Show();
        }

        private void finalizarOSistemaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
