using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using MySql.Data.MySqlClient;

namespace _23_09_2021
{
    public partial class FrmRelVendas : Form
    {

        MySqlConnection conexao;
        MySqlCommand comando;
        String banco = "server=localhost;port=3306;uid=root;pwd=etecjau;database=ds";
        MySqlDataAdapter Adaptador;
        DataTable datTabela;

        public FrmRelVendas()
        {
            InitializeComponent();
        }

        private void FrmRelVendas_Load(object sender, EventArgs e)
        {

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            conexao = new MySqlConnection(banco);
            conexao.Open();
            comando = new MySqlCommand("select c.nome Categoria, sum(vd.qtde) quantidade from categorias c " +
                                       "inner join produtos p on (c.id = p.id_categoria) " +
                                       "inner join venda_det vd on (vd.id_produto = p.id) " +
                                       "inner join venda_cab vc on (vc.id = vd.id_venda) " +
                                       "where vc.data >= @dataini and vc.data  <= @datafim group by c.id", conexao);
            comando.Parameters.AddWithValue("@dataini", DateTime.Parse(dtpInicio.Value.ToString("dd/MM/yyyy")));
            comando.Parameters.AddWithValue("@datafim", DateTime.Parse(dtpFinal.Value.ToString("dd/MM/yyyy")));
            Adaptador = new MySqlDataAdapter(comando);
            Adaptador.Fill(datTabela = new DataTable());
            dgvConsulta.DataSource = datTabela;
            conexao.Close();
        }

        private void btnGrafico_Click(object sender, EventArgs e)
        {
            //criação do gráfico através de arrays
            String[] categoria = new String[dgvConsulta.RowCount];
            double[] qtde = new double[dgvConsulta.RowCount];
            int i = 0;

            foreach (DataGridViewRow linha in dgvConsulta.Rows)
            {
                categoria[i] = linha.Cells[0].Value.ToString();
                qtde[i] = Convert.ToDouble(linha.Cells[1].Value);
                i += 1;
            }
            chartVendas.Series[0].ChartType = SeriesChartType.Pie;
            chartVendas.Titles.Add("Gráfico de Vendas por Categoria");
            chartVendas.ChartAreas[0].Area3DStyle.Enable3D = true;
            chartVendas.Series[0].Points.DataBindXY(categoria, qtde);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //configura a janela de visualização do modo maximizado
            printPreviewDialog1.WindowState = FormWindowState.Maximized;

            //abre a tela de visualização do relatório
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //desenvolvimento do cabeçalho do relatório
            e.Graphics.DrawString("Reltório de Vendas", new Font("Arial", 30, FontStyle.Bold), Brushes.Black, 230, 10);
            e.Graphics.DrawLine(Pens.Black, 100, 90, 720, 90);
            e.Graphics.DrawString("Categoria", new Font("Arial", 10), Brushes.Black, 100, 95);
            e.Graphics.DrawString("Quantidade", new Font("Arial", 10), Brushes.Black, 600, 95);
            e.Graphics.DrawLine(Pens.Black, 100, 120, 720, 120);

            //desenvolvimento da interface do corpo do relatorio 
            int posicao_y = 100;
            foreach (DataGridViewRow linha in dgvConsulta.Rows)
            {
                posicao_y += 30;
                e.Graphics.DrawString(linha.Cells[0].Value.ToString(), new Font("Arial", 10), Brushes.Black, 100, posicao_y);
                e.Graphics.DrawString(linha.Cells[1].Value.ToString(), new Font("Arial", 10), Brushes.Black, 600, posicao_y);
            }

            //desenvolvimento da interface do rodapé do relatório
            e.Graphics.DrawLine(Pens.Black, 100, 1060, 720, 1060);
            e.Graphics.DrawString("Total de Categorias", new Font("Arial", 10), Brushes.Black, 100, 1065);
            e.Graphics.DrawString(dgvConsulta.RowCount.ToString(), new Font("Arial", 10), Brushes.Black, 250, 1065);
            e.Graphics.DrawString(System.DateTime.Now.ToString(), new Font("Arial", 10), Brushes.Black, 500, 1065);
            e.Graphics.DrawLine(Pens.Black, 100, 1090, 720, 1090);
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
