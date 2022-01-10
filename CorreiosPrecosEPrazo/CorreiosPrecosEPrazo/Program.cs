using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;
using CorreiosPrecosEPrazo.ClassesDiversas;
using System.Data;

//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Net;
//using HtmlAgilityPack;
//using System.Data;

namespace CorreiosPrecosEPrazo
{
    static class Program
    {
        public static string DataExpiracaoSistema = "31-12-2020";// Último dia de utilização
        
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("pt-BR");

            string data = CriptografiaHelper.Criptografa(DataExpiracaoSistema);

            GeraArquivoConfig();

            if (VerificaChaveAcesso())
            {
                //Application.Run(new Form1());
                Application.Run(new PrecosEPrazoContrato());
            }
            else
            {
                Mensagens.Alerta("Versão demonstração.\nFavor contactar o administrador do sistema. \nMarques Fonseca (63) 99208-2269");
                return;
            }
        }

        private static bool VerificaChaveAcesso()
        {
            //string data = "30-09-2019";
            string valorCriptografado = "";

            #region Retorna data do banco
            string curDir = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
            string nomeArquivo = string.Format("Config");
            if (!Arquivos.Existe(string.Format("{0}\\{1}.XML", curDir, nomeArquivo), false)) Configuracoes.GeraArquivoConfig();
            DataTable dt = ClassesDiversas.ArquivoXML.AbrirArquivoXML(curDir, nomeArquivo);
            if (dt.Rows.Count >= 1)
            {
                foreach (DataRow item in dt.Rows)
                {
                    if (item["Nome"].ToString() == "ChaveAcesso")
                    {
                        valorCriptografado = item["Valor"].ToString();
                        break;
                    }
                }
            }
            #endregion

            try
            {
                string DataRetornadaBancoDados = ClassesDiversas.CriptografiaHelper.Descriptografa(valorCriptografado);
                if (DateTime.Now.Date.ToShortDateString().ToDateTime().Date <= string.Format("{0:dd/MM/yyyy}", DataRetornadaBancoDados).ToDateTime())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static void GeraArquivoConfig()
        {
            string curDir = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
            string nomeArquivo = string.Format("Config");
            if (!Arquivos.Existe(string.Format("{0}\\{1}.XML", curDir, nomeArquivo), false))
            {
                DataTable dt = new DataTable("Config");
                dt.Columns.Add("Nome", typeof(string));
                dt.Columns.Add("Valor", typeof(string));
                DataRow dr = dt.NewRow();
                //dr["Nome"] = "FecharAplicacoesAbertas";
                //dr["Valor"] = 0;
                //dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["Nome"] = "ChaveAcesso";
                //dr["Valor"] = ClassesDiversas.CriptografiaHelper.Criptografa(DataExpiracaoSistema);
                dr["Valor"] = "tBfC6CeoTmSvL1C7B34jqw=="; //ClassesDiversas.CriptografiaHelper.Criptografa("30-09-2019");
                dt.Rows.Add(dr);

                ClassesDiversas.ArquivoXML.GravaArquivoXML(dt, curDir, nomeArquivo);
            }
        }
    }
}
