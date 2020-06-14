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
    public partial class Form1 : Form
    {
        private string DirArquivo { get; set; }
        private DataTable TblListaAtual { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void MenuArquivoExcelXLSX_Click(object sender, EventArgs e)
        {
            AbrirTamplates.Title = "Buscar Arquivo Excel";
            //AbrirTamplates.InitialDirectory = DirArquivo;
            //AbrirTamplates.FileName = string.Empty;
            AbrirTamplates.DefaultExt = ".xlsx";
            AbrirTamplates.Filter = "Arquivos Excel|*.xlsx";
            AbrirTamplates.RestoreDirectory = true;

            if (AbrirTamplates.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string NomePlan = RetornaNomePlanilhaSelecionadoXLS(AbrirTamplates.FileName);
                if (string.IsNullOrEmpty(NomePlan)) return;

                try
                {
                    using (DataTable dt = new ImportarArquivos().ImportarXLSXNovo(AbrirTamplates.FileName, string.Format("{0}$", NomePlan.Replace("$", "")),"*", 0))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            //CarregaGridView(dt);
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Não foi possível carregar nenhum registro apartir do .xlsx informado. Por favor selecione outro arquivo.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Não foi possível carregar o arquivo: {0}", ex.Message));
                }
            }
        }

        private void MenuArquivoExcelXLS_Click(object sender, EventArgs e)
        {
            AbrirTamplates.Title = "Buscar Arquivo Excel";
            //AbrirTamplates.InitialDirectory = DirArquivo;
            //AbrirTamplates.FileName = string.Empty;
            AbrirTamplates.DefaultExt = ".xls";
            AbrirTamplates.Filter = "Arquivos Excel|*.xls*";
            AbrirTamplates.RestoreDirectory = true;

            if (AbrirTamplates.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string NomePlan = RetornaNomePlanilhaSelecionadoXLS(AbrirTamplates.FileName);
                if (string.IsNullOrEmpty(NomePlan)) return;

                try
                {
                    //using (DataTable dt = new ImportarArquivos().ImportarXLS(AbrirTamplates.FileName, NomePlan))
                    using (DataTable dt = new ImportarArquivos().ImportarXLSXNovo(AbrirTamplates.FileName, string.Format("{0}$", NomePlan.Replace("$", "")), "*", 0))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            //CarregaGridView(dt);
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Não foi possível carregar nenhum registro apartir do .xls informado. Por favor selecione outro arquivo.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Não foi possível carregar o arquivo: {0}", ex.Message));
                }
            }
        }

        private void MenuArquivoExcelCSV_Click(object sender, EventArgs e)
        {
            AbrirTamplates.Title = "Buscar Arquivo Excel";
            //AbrirTamplates.InitialDirectory = DirArquivo;
            //AbrirTamplates.FileName = string.Empty;
            AbrirTamplates.DefaultExt = ".csv";
            AbrirTamplates.Filter = "Arquivos Excel|*.csv";
            AbrirTamplates.RestoreDirectory = true;

            if (AbrirTamplates.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //string NomePlan = RetornaNomePlanilhaSelecionado();
                //if (string.IsNullOrEmpty(NomePlan)) return;

                try
                {
                    ImportarArquivos csv = new ImportarArquivos();
                    using (DataTable dt = csv.ImportarSCV(AbrirTamplates.FileName))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            //CarregaGridView(dt);
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Não foi possível carregar nenhum registro apartir do .csv informado. Por favor selecione outro arquivo.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Não foi possível carregar o arquivo: {0}", ex.Message));
                }
            }
        }

        private void MenuArquivoExcelXML_Click(object sender, EventArgs e)
        {
            AbrirTamplates.Title = "Buscar Arquivo XML";
            AbrirTamplates.InitialDirectory = DirArquivo;
            AbrirTamplates.FileName = string.Empty;
            AbrirTamplates.DefaultExt = ".xml";
            AbrirTamplates.Filter = "Arquivos XML|*.xml|*.XML|";
            AbrirTamplates.RestoreDirectory = true;

            if (AbrirTamplates.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    using (DataSet Ds = new DataSet())
                    {
                        Ds.ReadXml(AbrirTamplates.FileName);
                        if (Ds != null && Ds.Tables[0].Rows.Count > 0)
                        {
                            //CarregaGridView(Ds.Tables[0]);
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Não foi possível carregar nenhum registro apartir do XML informado. Por favor selecione outro arquivo.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Não foi possível carregar o arquivo XML. {0}", ex.Message));
                }
            }
        }

        private string RetornaNomePlanilhaSelecionadoXLS(string nomeArquivoBuscado)
        {
            List<DataTable> ListaDt = new List<DataTable>();
            int qtdLinhasDesejadas = 10;
            List<string> ListaNomePlan = new ImportarArquivos().ListSheetInExcel(String.Format(@"{0}", nomeArquivoBuscado));
            List<string> novaListaPlan = new List<string>();
            foreach (string item in ListaNomePlan)
            {
                string lllll = item.Replace("$_", "$");
                if (novaListaPlan.AsEnumerable().Any(m => m.Contains(lllll)) == false)
                {
                    novaListaPlan.Add(lllll);
                }
            }
            if (novaListaPlan.Count == 0)
            {
                return "";
            }
            if (novaListaPlan.Count == 1)
            {
                return novaListaPlan[0];
            }
            foreach (string itemNomePlan in novaListaPlan)
            {
                using (DataTable dt = new ImportarArquivos().ImportarXLSXNovo(nomeArquivoBuscado, itemNomePlan, "*", qtdLinhasDesejadas))
                {
                    if (dt != null && dt.Rows.Count == 0)
                    {
                        DataTable data = new DataTable();
                        data.Columns.Add("  -");
                        data.Columns.Add("A");
                        data.Columns.Add("B");
                        data.Columns.Add("C");
                        data.Columns.Add("D");
                        data.Columns.Add("E");
                        data.Columns.Add("F");
                        data.Columns.Add("G");
                        data.Columns.Add("H");
                        data.Columns.Add("I");
                        data.Columns.Add("J");
                        data.Columns.Add("K");
                        data.Columns.Add("L");
                        data.Columns.Add("M");
                        data.Columns.Add("N");
                        data.Columns.Add("O");
                        data.Columns.Add("P");
                        data.Columns.Add("Q");

                        for (int i = 1; i <= qtdLinhasDesejadas; i++)
                        {
                            DataRow row = data.NewRow();
                            row["  -"] = i;
                            row["A"] = null;
                            row["B"] = "";
                            row["C"] = "";
                            row["D"] = "";
                            row["E"] = "";
                            row["F"] = "";
                            row["G"] = "";
                            row["H"] = "";
                            row["I"] = "";
                            row["J"] = "";
                            row["K"] = "";
                            row["L"] = "";
                            row["M"] = "";
                            row["N"] = "";
                            row["O"] = "";
                            row["P"] = "";
                            row["Q"] = "";
                            data.Rows.Add(row);
                        }
                        ListaDt.Add(data);
                    }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ListaDt.Add(dt);
                    }
                }
            }

            using (FormPlan plan = new FormPlan(ListaDt, novaListaPlan, nomeArquivoBuscado))
            {
                plan.ShowDialog(this);

                if (plan.cancelado == true)
                    return "";
                else
                    return plan.retorno;
            }
        }
    }
}
