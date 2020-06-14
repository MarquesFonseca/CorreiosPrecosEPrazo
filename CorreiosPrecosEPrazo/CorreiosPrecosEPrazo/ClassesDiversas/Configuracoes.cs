using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
//using Rensoftware.Classes;
//using Rensoftware.Classes.ClassesEstaticas;
using CorreiosPrecosEPrazo.ClassesDiversas;
using System.Collections.Generic;
//using Kemuel.Formularios;

namespace CorreiosPrecosEPrazo.ClassesDiversas
{
    public static class Configuracoes
    {
        private static string _strConexao = System.Configuration.ConfigurationManager.ConnectionStrings["cadastroConnectionString"].ConnectionString + ";Jet OLEDB:Database Password=9342456;";
        public static string strConexao
        {
            get { return _strConexao; }
            set { _strConexao = value; }
        }


        private static TipoAmbiente _tipoAmbiente;
        public static TipoAmbiente TipoAmbiente
        {
            get { return _tipoAmbiente; }
            set { _tipoAmbiente = value; }
        }

        private static string _statusPrincipal = "";
        public static string StatusPrincipal
        {
            get { return _statusPrincipal; }
            set { _statusPrincipal = value; }
        }

        public static bool ExibicaoMensagensParaDesenvolvedor = false;

        public static void GeraArquivoConfig()
        {
            string curDir = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
            string nomeArquivo = string.Format("Config");
            if (!Arquivos.Existe(string.Format("{0}\\{1}.XML", curDir, nomeArquivo), false))
            {
                DataTable dt = new DataTable("Config");
                dt.Columns.Add("Nome", typeof(string));
                dt.Columns.Add("Valor", typeof(string));
                DataRow dr = dt.NewRow();
                dr["Nome"] = "FecharAplicacoesAbertas";
                dr["Valor"] = 0;
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["Nome"] = "ChaveAcesso";
                //dr["Valor"] = ClassesDiversas.CriptografiaHelper.Criptografa("30-09-2019");
                dr["Valor"] = "tBfC6CeoTmSvL1C7B34jqw=="; //ClassesDiversas.CriptografiaHelper.Criptografa("30-09-2019");

                dt.Rows.Add(dr);

                ClassesDiversas.ArquivoXML.GravaArquivoXML(dt, curDir, nomeArquivo);
            }
        }

        public static void VerificaSeFecharAplicacaoParaAtualizacao()
        {
            #region verifica se é para fechar todas as aplicações para atualizar....
            string curDir = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
            string nomeArquivo = string.Format("Config");
            if (!Arquivos.Existe(string.Format("{0}\\{1}.XML", curDir, nomeArquivo), false)) Configuracoes.GeraArquivoConfig();
            DataTable dt = ClassesDiversas.ArquivoXML.AbrirArquivoXML(curDir, nomeArquivo);
            if (dt.Rows.Count >= 1)
            {
                if (dt.Rows[0]["Valor"].ToString() == "1")
                {
                    var processo = System.Diagnostics.Process.GetCurrentProcess();
                    foreach (System.Diagnostics.Process itens in System.Diagnostics.Process.GetProcessesByName(processo.ProcessName).Where(p => p.Id != processo.Id))
                    {
                        itens.Kill();
                    }
                    foreach (System.Diagnostics.Process itens in System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName))
                    {
                        itens.Kill();
                    }
                }
            }
            #endregion
        }

        public static bool VerificaChaveAcesso()
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
                    if(item["Nome"].ToString() == "ChaveAcesso")
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

        /// <summary>
        /// Mensagem 'A conexão com o banco de dados foi perdida.'
        /// </summary>
        public static string MensagemPerdaConexao = "A conexão com o banco de dados foi perdida.";

        /// <summary>
        /// Codigo da versão do leiaute da EFD
        /// </summary>
        public static string COD_VER = string.Empty;

        /// <summary>
        /// Ano da geração do arquivo EFD
        /// </summary>
        public static int AnoGeracaoArquivo = 0;

        //Local do arquivo de conexão
        private static string lLocalArquivoConexao = String.Format("{0}\\Rensoftware\\SEDsce\\SEDFiscal", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
        public static string LocalArquivoConexao
        {
            get { return lLocalArquivoConexao; }
        }

        //Local com o nome do arquivo de conexão
        private static string LNomeELocalArquivoConexao = String.Format("{0}\\Rensoftware\\SendEMail Rensoftware 2.0\\Conexao.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
        public static string NomeELocalArquivoConexao
        {
            get { return LNomeELocalArquivoConexao; }
            set { LNomeELocalArquivoConexao = value; }
        }

        public static string LocalArquivoAtualizacao { get; set; }

        //Indica o nome do usuario do  servidor
        private static string LNomeUsuarioServido = "SA";//"SUPERSCERG";
        public static string NomeUsuarioServido
        {
            get { return LNomeUsuarioServido; }
        }

        //Indica a senha do usuario do servidor
        private static string LSenhaUsuarioServidor = "S35SUP5RSRG";
        public static string SenhaUsuarioServidor
        {
            get { return LSenhaUsuarioServidor; }
        }

        //Indica o servidor que o sistema esta conectado 
        private static string LServidorLogado = string.Empty;
        public static string ServidorLogado
        {
            get { return LServidorLogado; }
            set { LServidorLogado = value; }
        }

        //Indica o banco que o sistema esta conectado 
        private static string LBancoLogado = string.Empty;
        public static string BancoLogado
        {
            get { return LBancoLogado; }
            set { LBancoLogado = value; }
        }

        //private static string LStringConexao = String.Format(@"Data Source=222.222.3.2;Initial Catalog=EnvioEmail;Persist Security Info=True;User ID={0};Password={1}", LNomeUsuarioServido, LSenhaUsuarioServidor);
        //public static string StringConexao
        //{
        //    get { return LStringConexao; }
        //    set
        //    {
        //        LStringConexao = value;
        //    }
        //}

        public static int CodigoLoja { get; set; }

        public static string NomeEmpresa { get; set; }

        public static string UFEmpresa { get; set; }

        //public static TipoPerfilEFD PerfilEFD { get; set; }

        //Indica o nome do usuarioLogado
        private static string LUsuarioLogado = string.Empty;
        public static string UsuarioLogado
        {
            get { return LUsuarioLogado; }
            set { LUsuarioLogado = value; }
        }

        public static string CodigoUsuarioLogado { get; set; }

        public static bool FecharApplication { get; set; }



        static Configuracoes()
        {

            //TODO: CASO O NOME DA EMPRESA NÃO ESTEJA SENDO POPULADO AO EXECUTAR A FUNÇÃO DE LOGAR, DÊ UM JEITO DE FAZER ISSO AQUI...
            //Configuracoes.NomeEmpresa = "Buscar o nome da empresa";

            //LocalArquivoAtualizacao = String.Format("{0}\\Rensoftware\\SEDsce\\SEDFiscal\\AtualizaBanco\\AtualizaBancoEFD.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
            Configuracoes.Conf = ConfigurationManager.OpenMachineConfiguration();
        }

        /// <summary>
        /// Inicia configuração da string de conexão.
        /// </summary>
        /// <param name="PassouUmaVez">
        /// Como o método é recursivo, há necessidade de condições de parada
        /// caso ainda não tenha passado nele ou já passou e não configurou uma
        /// conexão e optou por configurar, o método torna a chamar a si mesmo
        /// caso contrario, ele sai da cadeia recursiva
        /// </param>
        /// <Modificado por>Thiago Nery Macêdo</Modificado>
        //public static void ConfiguraConexao(bool PassouUmaVez)
        //{
        //    FecharApplication = false;
        //    if (!Directory.Exists(LocalArquivoConexao))
        //        Directory.CreateDirectory(LocalArquivoConexao);

        //    //Verifica a existencia do arquivo de conexão. caso o arquivo exista prepara a conexão.
        //    //Se o arquivo não existe é chamando o formulario de configuração de conexão.
        //    if (File.Exists(NomeELocalArquivoConexao))
        //    {
        //        BancoLogado = string.Empty;
        //        ServidorLogado = string.Empty;

        //        DataSet DsStringsConexao = new DataSet();
        //        //ConfiguraConexao ConfConexao = new ConfiguraConexao();

        //        try
        //        {
        //            DsStringsConexao.ReadXml(NomeELocalArquivoConexao);

        //                //Opção quando tem apenas uma conexão feita
        //                ServidorLogado = DsStringsConexao.Tables["Estrutura"].Rows[0]["SERVIDOR"].ToString();
        //                BancoLogado = DsStringsConexao.Tables["Estrutura"].Rows[0]["BANCO"].ToString();

        //            //    LStringConexao = ConfConexao.MontaEstrigConexao(ServidorLogado, BancoLogado, LNomeUsuarioServido, LSenhaUsuarioServidor);
        //            LStringConexao = DAO.MontaStringConexao(ServidorLogado,LNomeUsuarioServido,LSenhaUsuarioServidor,BancoLogado,TipoBanco.SqlServer);

        //            if (!DAO.TestaConexao(LStringConexao))
        //            {

        //            }
        //                //if (!ConfConexao.TestaConexao(LStringConexao))
        //                //{
        //                //    //Deu erro na conexão.
        //                //    ConfConexao.Dispose();
        //                //    PFormManutencaoConexao = new FormManutencaoConexao();
        //                //    PFormManutencaoConexao.ShowDialog();
        //                //    ConfiguraConexao(PassouUmaVez);
        //                //    return;
        //                //}
        //                //else
        //                //{
        //                //    ConfConexao.Dispose();
        //                //}

        //        }
        //        catch (Exception Ex)
        //        {
        //            //Mensagens.MsgErroGenerico(Ex.Message);
        //            //PFormManutencaoConexao = new FormManutencaoConexao();
        //            //PFormManutencaoConexao.ShowDialog();

        //            //if (PFormManutencaoConexao.BSDados.Count > 0)
        //            //    return;
        //            //else if (Mensagens.Pergunta("A conexão não foi configurada corretamente, deseja continuar tentando?") == DialogResult.Yes)
        //            //    ConfiguraConexao(PassouUmaVez);
        //            //else
        //            //{
        //            //    FecharApplication = true;
        //            //    Application.Exit();
        //            //    return;
        //            //}
        //        }
        //    }
        //    else
        //    {
        //        //if (!PassouUmaVez || (PassouUmaVez && Mensagens.Pergunta("A conexão não foi configurada corretamente, deseja continuar tentando?") == DialogResult.Yes))
        //        //{
        //        //    PFormManutencaoConexao = new FormManutencaoConexao();
        //        //    PFormManutencaoConexao.ShowDialog();
        //        //    PassouUmaVez = true;
        //        //    ConfiguraConexao(PassouUmaVez);
        //        //}
        //        //else
        //        //{
        //        //    FecharApplication = true;
        //        //    Application.Exit();
        //        //}

        //        return;
        //    }
        //}

        #region Configuracoes no app.config

        /// <summary>
        /// Carrega o arquivo de configurações (app.config)
        /// </summary>
        public static Configuration Conf { get; set; }

        #region Metodos de gravação e recuperação de configurações no app.config
        /// <summary>
        /// Grava o valor de uma configuração no arquivo app.config
        /// </summary>
        /// <param name="Chave">Chave do registro</param>
        /// <param name="Valor">Valor do registro</param>
        public static void GravaConfig(string Chave, string Valor)
        {
            try
            {
                Chave = Chave.RemoveTodoEspaco().RemoveAcento_DICIONARIO().ToUpper();
                //Se não existir a chave, cria uma nova e atribui o valor
                Configuracoes.Conf.AppSettings.Settings[Chave].Value = Valor;
                Configuracoes.Conf.Save();
            }
            catch
            {
                //Se ja existir, atualiza o valor
                Configuracoes.Conf.AppSettings.Settings.Add(Chave, Valor);
                Configuracoes.Conf.Save();
            }
        }

        /// <summary>
        /// Retorna o valor de uma configuração
        /// </summary>
        /// <param name="Chave">Chave que contém o valor</param>
        /// <returns>Valor gravado no arquivo app.config</returns>
        public static string RetornaConfig(string Chave, string DefaultValue)
        {
            try
            {
                Chave = Chave.RemoveTodoEspaco().RemoveAcento_DICIONARIO().ToUpper();
                //Caso a chave exista, retorna o valor dela
                return Configuracoes.Conf.AppSettings.Settings[Chave].Value.ToString();
            }
            catch
            {
                //Caso não exista, cria uma chave e retorna valor uma string vazia
                Configuracoes.Conf.AppSettings.Settings.Add(Chave, DefaultValue);
                Configuracoes.Conf.Save();
                return DefaultValue;
            }
        }
        #endregion

        #region Metodos de apoio
        /// <summary>
        /// Retorna um valor boleano para a string passada como parametro,
        /// Caso a string seja igual a 1 ou 'S' ou 's' retorno = True
        /// , senão retorno = False;
        /// </summary>
        public static bool GetBoolByName(string SimNao)
        {
            if (SimNao.ToUpper().Contains("S"))
                return true;
            else
                return false;
        }

        public static FormWindowState GetWindowStateByName(string formWindowState)
        {
            if (formWindowState.ToUpper() == FormWindowState.Maximized.ToString().ToUpper())
                return FormWindowState.Maximized;
            else if (formWindowState.ToUpper() == FormWindowState.Minimized.ToString().ToUpper())
                return FormWindowState.Minimized;
            else if (formWindowState.ToUpper() == FormWindowState.Normal.ToString().ToUpper())
                return FormWindowState.Normal;
            else
                return FormWindowState.Maximized;
        }

        public static System.Drawing.Size StringToSize(string _Size)
        {
            string[] Tamanhos = _Size.Replace("{", string.Empty).Replace("}", string.Empty).ToUpper().Trim().Split(',');
            int Width = 0, Height = 0;
            string Val = string.Empty;

            try
            {
                Val = Tamanhos.ToList().Where(T => T.ToUpper().Contains("WIDTH")).First();
                Width = Convert.ToInt32(Val.Substring(Val.IndexOf("=") + 1));

                Val = Tamanhos.ToList().Where(T => T.ToUpper().Contains("HEIGHT")).First();
                Height = Convert.ToInt32(Val.Substring(Val.IndexOf("=") + 1));
            }
            catch { }

            return new System.Drawing.Size(Width, Height);
        }

        public static System.Drawing.Point StringToPoint(string _Size)
        {
            string[] Tamanhos = _Size.Replace("{", string.Empty).Replace("}", string.Empty).ToUpper().Trim().Split(',');
            int X = 0, Y = 0;
            string Z = string.Empty;

            try
            {
                Z = Tamanhos.ToList().Where(T => T.ToUpper().Contains("X")).First();
                X = Convert.ToInt32(Z.Substring(Z.IndexOf("=") + 1));

                Z = Tamanhos.ToList().Where(T => T.ToUpper().Contains("Y")).First();
                Y = Convert.ToInt32(Z.Substring(Z.IndexOf("=") + 1));
            }
            catch { }

            return new System.Drawing.Point(X, Y);
        }
        #endregion

        #region Propriedades de configuração
        public static FormWindowState EstadoFormPrincipal
        {
            get
            {
                return GetWindowStateByName(RetornaConfig("EstadoFormPrincipal", FormWindowState.Maximized.ToString()));
            }
            set
            {
                GravaConfig("EstadoFormPrincipal", value.ToString());
            }
        }

        public static System.Drawing.Size TamanhoFormPrincipal
        {
            get
            {
                return StringToSize(RetornaConfig("TamanhoFormPrincipal", Screen.PrimaryScreen.WorkingArea.Size.ToString()));
            }
            set
            {
                GravaConfig("TamanhoFormPrincipal", value.ToString());
            }
        }

        public static System.Drawing.Point PosicaoFormPrincipal
        {
            get
            {
                return StringToPoint(RetornaConfig("PosicaoFormPrincipal", new System.Drawing.Point(0, 0).ToString()));
            }
            set
            {
                GravaConfig("PosicaoFormPrincipal", value.ToString());
            }
        }

        public static string NomeMenuSelecionadoFormPrincipal
        {
            get
            {
                return RetornaConfig("MenuSelecionadoFormPrincipal", string.Empty);
            }
            set
            {
                GravaConfig("MenuSelecionadoFormPrincipal", value);
            }
        }

        /// <summary>
        /// Estilo, Skim ou esquema de cores da aplicação
        /// </summary>
        public static string EstiloAplicacao
        {
            get
            {
                return RetornaConfig("EstiloAplicacao", "Black");
            }
            set
            {
                GravaConfig("EstiloAplicacao", value);
            }
        }
        #endregion

        #endregion
    }
}
