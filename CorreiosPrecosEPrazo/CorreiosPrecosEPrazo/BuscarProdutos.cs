using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CorreiosPrecosEPrazo
{
    public partial class BuscarProdutos : Form
    {
        private DataTable Dados = new DataTable();
        public bool cancelado = false;
        public DataRow retornoLinhaSelecionada = null;
        private bool TelaBuscaProdutosAdicionais = false;

        public BuscarProdutos()
        {
            InitializeComponent();
        }

        public BuscarProdutos(DataTable dt)
        {
            Dados = dt;
            InitializeComponent();
        }

        public BuscarProdutos(DataTable dt, bool _telaBuscaProdutosAdicionais)
        {
            Dados = dt;
            TelaBuscaProdutosAdicionais = _telaBuscaProdutosAdicionais;
            InitializeComponent();
        }

        private void BuscarProdutos_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Dados;
            if (TelaBuscaProdutosAdicionais)
            {
                dataGridView1.Columns[1].DefaultCellStyle.Format = "c2";
                dataGridView1.Columns[1].DefaultCellStyle.FormatProvider = System.Globalization.CultureInfo.GetCultureInfo("pt-BR");
                dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            Dados.DefaultView.RowFilter = string.Format("Tipo LIKE '{0}'", "Caixa");

            // Set the column header style.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.DarkGray;
            columnHeaderStyle.ForeColor = Color.Black;
            columnHeaderStyle.Font = new Font("Verdana", 12, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;


            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dataGridView1.RowsDefaultCellStyle.BackColor = Color.White;
            this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            groupBox1.Focus();
        }

        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            retornoLinhaSelecionada = (dataGridView1.Rows[dataGridView1.CurrentRow.Index].DataBoundItem as DataRowView).Row;
            this.Close();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            retornoLinhaSelecionada = null;
            cancelado = true;
            this.Close();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                retornoLinhaSelecionada = (dataGridView1.Rows[dataGridView1.CurrentRow.Index].DataBoundItem as DataRowView).Row;
                this.Close();
            }
            if (e.KeyCode == Keys.Escape)
            {
                retornoLinhaSelecionada = null;
                cancelado = true;
                this.Close();
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            retornoLinhaSelecionada = (dataGridView1.Rows[dataGridView1.CurrentRow.Index].DataBoundItem as DataRowView).Row;
            this.Close();
        }

        private void BuscarProdutos_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.C)
            {
                radioButtonCaixas.Checked = true;
            }
            if(e.KeyData == Keys.E)
            {
                radioButtonEnvelopes.Checked = true;
            }
            if (e.KeyData == Keys.G)
            {
                radioButtonGeral.Checked = true;
            }
        }

        private void radioButtonCaixas_CheckedChanged(object sender, EventArgs e)
        {
            Dados.DefaultView.RowFilter = string.Format("Tipo LIKE '{0}'", "Caixa");
            dataGridView1.Focus();
        }

        private void radioButtonEnvelopes_CheckedChanged(object sender, EventArgs e)
        {
            Dados.DefaultView.RowFilter = string.Format("Tipo LIKE '{0}'", "Envelope");
            dataGridView1.Focus();
        }

        private void radioButtonGeral_CheckedChanged(object sender, EventArgs e)
        {
            Dados.DefaultView.RowFilter = string.Format("Tipo LIKE '{0}'", "Geral");
            dataGridView1.Focus();
        }
    }
}
