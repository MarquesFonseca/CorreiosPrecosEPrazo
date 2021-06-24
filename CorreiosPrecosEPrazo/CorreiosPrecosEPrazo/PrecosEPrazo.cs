using CorreiosPrecosEPrazo.Correios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows.Forms;


using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace CorreiosPrecosEPrazo
{
    public partial class PrecosEPrazo : Form
    {
        bool selectByMouse = false;
        private decimal ValorTotalProdutosAdicionados = 0;

        public PrecosEPrazo()
        {
            InitializeComponent();
            ConfiguracoesExcel.CarregaConfiguracoesExcel();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbMaoPropria.Text = string.Format("&Mão própria - {0}", Helpers.returnDinheiro(ConfiguracoesExcel.MaoPropria));
            cbAvisoRecebimento.Text = string.Format("&Aviso de recebimento - {0}", Helpers.returnDinheiro(ConfiguracoesExcel.AvisoRecebimento));
            cbPagamentoEntrega.Text = string.Format("Pagamento na Entrega - {0}", Helpers.returnDinheiro(ConfiguracoesExcel.PagamentoEntregaComVPNe));
            cbPostaRestantePedida.Text = string.Format("Posta Restante Pedida - {0}", Helpers.returnDinheiro(ConfiguracoesExcel.PostaRestantePedida));

            tbCepOrigem.Text = ConfiguracoesExcel.CEPOrigem;
            tbCepDestino.Text = ConfiguracoesExcel.CEPOrigem;

            panelResultado.Visible = false; // esconde a tela com os dados calculados
            // Mostra apenas as entradas obrigatórias para Caixa/Pacote
            tbLargura.Enabled = true;
            tbComprimento.Enabled = true;
            tbAltura.Enabled = true;
            LblDiametro.Enabled = false;
            tbDiametro.Enabled = false;
            //////

            ToMoney(tbDeclaracaoValor, "N2");

            LbInformeOValorDeclarado.Enabled = false;
            tbDeclaracaoValor.Minimum = Convert.ToDecimal("0");
            tbDeclaracaoValor.Enabled = false;
            tbDeclaracaoValor.Value = 0;
            //cbDeclaracaoDeValor.Checked = true;

            cbAvisoRecebimento.Checked = true;

            LbRegiaoSelecionada.Visible = false;

            checkBoxOpcaoCartaRegistrada.Checked = false;
            checkBoxOpcaoCartaRegistrada.Enabled = false;

            tbCepDestino.Focus();
            tbCepDestino.SelectAll();
        }

        public void ToMoney(NumericUpDown text, string format = "C2")
        {
            double value;
            if (double.TryParse(text.Text, out value))
            {
                text.Text = value.ToString(format);
            }
            else
            {
                text.Text = "0,00";
            }
        }

        /// <summary>
        ///     Click do botão buscar no Correio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;



            //limpa campos
            reiniciarEntradasDoSedex();
            reiniciarEntradasDoPAC();
            reiniciarEntradasDoSedex12();
            reiniciarEntradasDoSedex10();
            reiniciarEntradasDoSedexHoje();

            configuraOpçoesResultado();

            /// Declaração de variáveis para passar como parâmetro
            string CdEmpresa = "";
            string DsSenha = "";
            List<string> listaCodigos = retornarServico();
            string cepDeOrigem = tbCepOrigem.Text;
            string cepDeDestino = tbCepDestino.Text;
            string peso = tbPeso.Text;
            int formato = 1;
            decimal comprimento = Helpers.returnDecimal(tbComprimento.Text);
            decimal altura = Helpers.returnDecimal(tbAltura.Text);
            decimal largura = Helpers.returnDecimal(tbLargura.Text);
            decimal diametro = Helpers.returnDecimal(tbDiametro.Text);
            string maoPropria = "N";
            decimal valorDeclarado = 0;
            string avisoDeRecebimento = "N";
            ////////////////////////////////////////

            /// Validação das entradas ////
            if (rbCaixa.Checked)
            {
                formato = 1;
            }
            else if (rbCilindro.Checked)
            {
                formato = 2;
            }
            else
            {
                formato = 3;
            }


            if (cbMaoPropria.Checked)
            {
                maoPropria = "S";
            }
            else
            {
                maoPropria = "N";
            }

            if (cbAvisoRecebimento.Checked)
            {
                avisoDeRecebimento = "S";
            }
            else
            {
                avisoDeRecebimento = "N";
            }

            if (cbDeclaracaoDeValor.Checked)
            {
                valorDeclarado = Helpers.returnDecimal(tbDeclaracaoValor.Text);
            }
            else
            {
                valorDeclarado = 0;
            }

            foreach (string Codigo in listaCodigos)
            {
                //Chamada do procedimento para consultar preços e prazos nos correios
                cResultado precoEPrazo = Consulta.ConsultarPrecosEPrazos(CdEmpresa, DsSenha, Codigo, cepDeOrigem, cepDeDestino, peso, formato, comprimento, altura, largura, diametro, maoPropria, valorDeclarado, avisoDeRecebimento);

                if (precoEPrazo != null)
                {
                    panelResultado.Visible = true; // Mostra o painel que estava oculto

                    inicializarEntradas(precoEPrazo); // inicializa as entradas de acordo com o retorno dos Correios
                }
            }

            this.Cursor = Cursors.Default;
        }        

        private void EscreveArquivoCEPOrigem(string text)
        {
            ////declarando a variavel do tipo StreamWriter para 
            //abrir ou criar um arquivo para escrita 
            System.IO.StreamWriter arquivo = null;

            ////Colocando o caminho fisico e o nome do arquivo a ser criado
            //finalizando com .txt
            string CaminhoNome = string.Format(@"{0}CEPOrigem.txt", System.AppDomain.CurrentDomain.BaseDirectory);

            System.IO.File.Delete(CaminhoNome);
            if (!System.IO.File.Exists(CaminhoNome))
                arquivo = System.IO.File.CreateText(CaminhoNome);

            //escrevendo o titulo
            arquivo.WriteLine(text);

            //fechando o arquivo texto com o método .Close()
            arquivo.Close();
        }

        //private string RetornaCEPOrigemTxt()
        //{
        //    //declarando a variável do tipo StreamReader
        //    //que é a variável usada para abrir um arquivo texto para leitura 
        //    System.IO.StreamReader arquivo = null;

        //    //Colocando o endereço físico (caminho do arquivo texto)
        //    string CaminhoNome = string.Format(@"{0}CEPOrigem.txt", System.AppDomain.CurrentDomain.BaseDirectory);
        //    if (!System.IO.File.Exists(CaminhoNome)) return "";

        //    //abrindo um arquivo texto indicado
        //    arquivo = System.IO.File.OpenText(CaminhoNome);

        //    //lendo conteúdo da linha do arquivo texto
        //    string retorno = arquivo.ReadLine();

        //    arquivo.Close();
        //    return retorno;
        //}

        private void configuraOpçoesResultado()
        {
            PainelSedex.Visible = checkBoxOpcaoSedex.Checked;
            PainelPAC.Visible = checkBoxOpcaoPAC.Checked;
            PainelCartaRegistrada.Visible = checkBoxOpcaoCartaRegistrada.Checked;
            PainelSedex12.Visible = checkBoxOpcaoSedex12.Checked;
            PainelSedex10.Visible = checkBoxOpcaoSedex10.Checked;
            PainelSedexHoje.Visible = checkBoxOpcaoSedexHoje.Checked;
        }

        /// <summary>
        ///     Deixa a tela com os dados dos serviços oculto.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void btnFechar_Click(object sender, EventArgs e)
        {
            panelResultado.Visible = false;
            reiniciarEntradasDoSedex();
            reiniciarEntradasDoPAC();
            reiniciarEntradasDoSedex12();
            reiniciarEntradasDoSedex10();
            reiniciarEntradasDoSedexHoje();

            tbCepDestino.Focus();
            tbCepDestino.SelectAll();
        }

        /// <summary>
        ///     Mostra apenas as entradas obrigatórias para Caixa/Pacote
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void rbCaixa_Click(object sender, EventArgs e)
        {
            tbAltura.Enabled = true;
            LblAltura.Enabled = true;

            tbLargura.Enabled = true;
            LblLargura.Enabled = true;

            tbComprimento.Enabled = true;
            LblComprimento.Enabled = true;

            tbDiametro.Enabled = false;
            LblDiametro.Enabled = false;

            checkBoxOpcaoCartaRegistrada.Checked = false;
            checkBoxOpcaoCartaRegistrada.Enabled = false;

            BtnBuscarDimensoesCaixa.Enabled = true;
        }

        /// <summary>
        ///     Mostra apenas as entradas obrigatórias para Envelope
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void rbEnvelope_Click(object sender, EventArgs e)
        {
            tbLargura.Enabled = true;
            LblLargura.Enabled = true;

            tbComprimento.Enabled = true;
            LblComprimento.Enabled = true;

            tbAltura.Enabled = false;
            LblAltura.Enabled = false;

            tbDiametro.Enabled = false;
            LblDiametro.Enabled = false;

            checkBoxOpcaoPAC.Checked = false;

            checkBoxOpcaoCartaRegistrada.Checked = true;
            checkBoxOpcaoCartaRegistrada.Enabled = true;

            BtnBuscarDimensoesCaixa.Enabled = true;
        }

        /// <summary>
        ///     Mostra apenas as entradas obrigatórias para Rolo/Cilindro ou Esfera
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void rbCilindro_Click(object sender, EventArgs e)
        {
            tbLargura.Enabled = false;
            LblLargura.Enabled = false;

            tbComprimento.Enabled = true;
            LblComprimento.Enabled = true;

            tbAltura.Enabled = false;
            LblAltura.Enabled = false;

            tbDiametro.Enabled = true;
            LblDiametro.Enabled = true;

            checkBoxOpcaoCartaRegistrada.Checked = false;
            checkBoxOpcaoCartaRegistrada.Enabled = false;

            BtnBuscarDimensoesCaixa.Enabled = false;
        }


        /// <summary>
        ///     Permite apenas números no TextBox peso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void tbPeso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        ///     Permite apenas números no TextBox declaração de valor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void tbDeclaracaoValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        ///     Formata o texto para dinheiro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void tbDeclaracaoValor_TextChanged(object sender, EventArgs e)
        {
            Helpers.retornarMoedaFormatada(ref tbDeclaracaoValor);
        }

        /// <summary>
        ///     Formata o texto para peso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void tbPeso_TextChanged(object sender, EventArgs e)
        {
            Helpers.retornarMoedaFormatada(ref tbPeso);
        }

        /// <summary>
        ///     Retorna o valor do serviço dos correios, de acordo com a entrada do index do item selecionado no comboBox de serviços.
        /// </summary>
        /// <param name="index"></param>
        ///
        private List<string> retornarServico()
        {
            /*
            04014 SEDEX à vista 
            04510 PAC à vista 
            12483 CARTA REGISTRADA
            04782 SEDEX 12 ( à vista) 
            04790 SEDEX 10 (à vista) 
            04804 SEDEX Hoje à vista 
            */

            List<string> codigos = new List<string>();

            if (checkBoxOpcaoSedex.Checked) codigos.Add(ConfiguracoesExcel.CodigoSARAParaSEDEX);
            if (checkBoxOpcaoPAC.Checked) codigos.Add(ConfiguracoesExcel.CodigoSARAParaPAC);
            if (checkBoxOpcaoCartaRegistrada.Checked) codigos.Add(ConfiguracoesExcel.CodigoSARAParaCartaRegistrada);
            if (checkBoxOpcaoSedex12.Checked) codigos.Add(ConfiguracoesExcel.CodigoSARAParaSEDEX12);
            if (checkBoxOpcaoSedex10.Checked) codigos.Add(ConfiguracoesExcel.CodigoSARAParaSEDEX10);
            if (checkBoxOpcaoSedexHoje.Checked) codigos.Add(ConfiguracoesExcel.CodigoSARAParaSEDEXHoje);

            return codigos;
        }

        /// <summary>
        ///     Inicializa as entradas com os valores de cada serviço requisitado dos correios 
        /// </summary>
        /// <param name="precoEPrazo"></param>
        ///
        private void inicializarEntradas(cResultado precoEPrazo)
        {
            lbDiaAtual.Text = Helpers.retornarDataSimples();
            foreach (var item in precoEPrazo.servicos.CServico)
            {
                escolherCodigo(item.Codigo, item);
            }
        }

        /// <summary>
        ///     De acordo com o valor recebido dos serviços dos Correios, inicializa cada componente da tela
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="precoEPrazo"></param>
        ///
        private void escolherCodigo(string codigo, cResultado.Servicos.cServico precoEPrazo)
        {
            /*
            04014 SEDEX à vista 
            04510 PAC à vista 
            04782 SEDEX 12 ( à vista) 
            04790 SEDEX 10 (à vista) 
            04804 SEDEX Hoje à vista 
            */
            switch (codigo)
            {
                case "04014":  //04014 SEDEX à vista 
                case "4014":
                    inicializarSedex(precoEPrazo);
                    break;
                case "04510": //04510 PAC à vista
                case "4510":
                    inicializarPAC(precoEPrazo);
                    break;


                case "12483": //12483 CartaRegistrada
                case "10030":
                    inicializarCartaRegistrada(precoEPrazo);
                    break;


                case "04782": //04782 SEDEX 12(à vista)
                case "4782":
                    inicializarSedex12(precoEPrazo);
                    break;
                case "04790": //04790 SEDEX 10(à vista)
                case "4790":
                    inicializarSedex10(precoEPrazo);
                    break;
                case "04804": //04804 SEDEX Hoje à vista
                case "4804":
                    inicializarSedexHoje(precoEPrazo);
                    break;
            }
        }

        /// <summary>
        ///     Inicializa as entradas dos componentes do Sedex
        /// </summary>
        /// <param name="precoEPrazo"></param>
        ///
        private void inicializarSedex(cResultado.Servicos.cServico precoEPrazo)
        {
            decimal valorPostaRestantePedida = Convert.ToDecimal("0"); //inicia com zero.. set ao valor se for marcado... //Posta Restante Pedida (31/01/2019)	3,20
            decimal valorPagamentoEntregaComVPNe = Convert.ToDecimal("0"); //inicia com zero.. seta valor se for marcado... //Pagamento na Entrega com VPNe: R$ 16,71

            lbEntregaSedex.Text = "Dia da postagem + " + precoEPrazo.PrazoEntrega + " dia(s) útil";
            lbPrecoSedex.Text = Helpers.returnDinheiro(precoEPrazo.ValorSemAdicionais);
            lbMaoSedex.Text = Helpers.returnDinheiro(precoEPrazo.ValorMaoPropria);
            lbRecebimentoSedex.Text = Helpers.returnDinheiro(precoEPrazo.ValorAvisoRecebimento);
            lbValorDeclaradoSedex.Text = Helpers.returnDinheiro(precoEPrazo.ValorValorDeclarado);

            decimal valorDeclaradoQuandoPagamentoEntrega = 0;
            if ((cbDeclaracaoDeValor.Checked) && (cbPagamentoEntrega.Checked))
            {
                /*
                 SEDEX Pagamento na Entrega com VPNe: não possui Indenização Automática, sendo
                 obrigatória a Declaração de Valor. O Ad Valorem de 2,0% incidirá sobre o valor total 
                 declarado em Nota Fiscal ou no Formulário de Discriminação de Conteúdo, fornecido pelos Correios.
                */
                valorDeclaradoQuandoPagamentoEntrega = ((tbDeclaracaoValor.Value * 2) / 100);
                lbValorDeclaradoSedex.Text = Helpers.returnDinheiro(valorDeclaradoQuandoPagamentoEntrega.ToString());
            }
            if (cbPagamentoEntrega.Checked)
            {
                valorPagamentoEntregaComVPNe = Convert.ToDecimal(ConfiguracoesExcel.PagamentoEntregaComVPNe);
            }
            if (cbPostaRestantePedida.Checked && cbPostaRestantePedida.Enabled)
            {
                valorPostaRestantePedida = Convert.ToDecimal(ConfiguracoesExcel.PostaRestantePedida);
            }

            lbDomiciliarSedex.Text = Helpers.returnResposta(precoEPrazo.EntregaDomiciliar);
            lbSabadoSedex.Text = Helpers.returnResposta(precoEPrazo.EntregaSabado);
            lbErroSedex.Text = precoEPrazo.Erro == "0" ? "" : precoEPrazo.Erro + "";
            lbMsgErroSedex.Text = precoEPrazo.MsgErro;
            lbSemAdicionaisSedex.Text = Helpers.returnDinheiro(precoEPrazo.ValorSemAdicionais);
            rtbObsSedex.Text = precoEPrazo.obsFim;

            if (cbPagamentoEntrega.Checked)
            {
                decimal NovoValorTotal =
                Convert.ToDecimal(precoEPrazo.Valor) - Convert.ToDecimal(precoEPrazo.ValorValorDeclarado) +
                valorDeclaradoQuandoPagamentoEntrega +
                valorPagamentoEntregaComVPNe +
                valorPostaRestantePedida +
                ValorTotalProdutosAdicionados;
                lbTotalSedex.Text = Helpers.returnDinheiro(NovoValorTotal.ToString());
            }
            else
            {
                decimal NovoValorTotal =
                Convert.ToDecimal(precoEPrazo.Valor) +
                valorDeclaradoQuandoPagamentoEntrega +
                valorPagamentoEntregaComVPNe +
                valorPostaRestantePedida +
                ValorTotalProdutosAdicionados;
                lbTotalSedex.Text = Helpers.returnDinheiro(NovoValorTotal.ToString());
            }
        }

        /// <summary>
        ///     Inicializa as entradas dos componentes do PAC
        /// </summary>
        /// <param name="precoEPrazo"></param>
        ///
        private void inicializarPAC(cResultado.Servicos.cServico precoEPrazo)
        {
            decimal valorPostaRestantePedida = Convert.ToDecimal("0"); //inicia com zero.. set ao valor se for marcado... //Posta Restante Pedida (31/01/2019)	3,20
            decimal valorPagamentoEntregaComVPNe = Convert.ToDecimal("0"); //inicia com zero.. seta valor se for marcado... //Pagamento na Entrega com VPNe: R$ 16,71

            lbEntregaPAC.Text = "Dia da postagem + " + precoEPrazo.PrazoEntrega + " dia(s) útil";
            lbPrecoPAC.Text = Helpers.returnDinheiro(precoEPrazo.ValorSemAdicionais);
            lbMaoPAC.Text = Helpers.returnDinheiro(precoEPrazo.ValorMaoPropria);
            lbRecebimentoPAC.Text = Helpers.returnDinheiro(precoEPrazo.ValorAvisoRecebimento);
            lbValorDeclaradoPAC.Text = Helpers.returnDinheiro(precoEPrazo.ValorValorDeclarado);

            decimal valorDeclaradoQuandoPagamentoEntrega = 0;
            if ((cbDeclaracaoDeValor.Checked) && (cbPagamentoEntrega.Checked))
            {
                /*
                 SEDEX Pagamento na Entrega com VPNe: não possui Indenização Automática, sendo
                 obrigatória a Declaração de Valor. O Ad Valorem de 2,0% incidirá sobre o valor total 
                 declarado em Nota Fiscal ou no Formulário de Discriminação de Conteúdo, fornecido pelos Correios.
                */
                valorDeclaradoQuandoPagamentoEntrega = ((tbDeclaracaoValor.Value * 2) / 100);
                lbValorDeclaradoPAC.Text = Helpers.returnDinheiro(valorDeclaradoQuandoPagamentoEntrega.ToString());
            }
            if (cbPagamentoEntrega.Checked)
            {
                valorPagamentoEntregaComVPNe = Convert.ToDecimal(ConfiguracoesExcel.PagamentoEntregaComVPNe);
            }
            if (cbPostaRestantePedida.Checked && cbPostaRestantePedida.Enabled)
            {
                valorPostaRestantePedida = Convert.ToDecimal(ConfiguracoesExcel.PostaRestantePedida);
            }

            lbDomiciliarPAC.Text = Helpers.returnResposta(precoEPrazo.EntregaDomiciliar);
            lbSabadoPAC.Text = Helpers.returnResposta(precoEPrazo.EntregaSabado);
            lbErroPAC.Text = precoEPrazo.Erro == "0" ? "" : precoEPrazo.Erro + "";
            lbMsgErroPAC.Text = precoEPrazo.MsgErro;
            lbSemAdicionaisPAC.Text = Helpers.returnDinheiro(precoEPrazo.ValorSemAdicionais);
            rtbObsPAC.Text = precoEPrazo.obsFim;

            if (cbPagamentoEntrega.Checked)
            {
                decimal NovoValorTotal =
                Convert.ToDecimal(precoEPrazo.Valor) - Convert.ToDecimal(precoEPrazo.ValorValorDeclarado) +
                valorDeclaradoQuandoPagamentoEntrega +
                valorPagamentoEntregaComVPNe +
                valorPostaRestantePedida +
                ValorTotalProdutosAdicionados;
                lbTotalPAC.Text = Helpers.returnDinheiro(NovoValorTotal.ToString());
            }
            else
            {
                decimal NovoValorTotal =
                    Convert.ToDecimal(precoEPrazo.Valor) +
                    valorDeclaradoQuandoPagamentoEntrega +
                    valorPagamentoEntregaComVPNe +
                    valorPostaRestantePedida +
                    ValorTotalProdutosAdicionados;
                lbTotalPAC.Text = Helpers.returnDinheiro(NovoValorTotal.ToString());
            }
        }

        /// <summary>
        ///     Inicializa as entradas dos componentes do CartaRegistrada
        /// </summary>
        /// <param name="precoEPrazo"></param>
        ///
        private void inicializarCartaRegistrada(cResultado.Servicos.cServico precoEPrazo)
        {
            /*
             * preço do serviço: 2,05 + 6,35 = 8,40
             * mao propria: 7,50
             * Aviso de Recebimento: 6,35
             * Valor Declarado: 0,00
             * valor sem adicional: 2,05 + 6,35 = 8,40
             * Valor Total: 2,05 + 6,35 + MP + AR + VD
             */

            //decimal PrecoServico = Convert.ToDecimal( + );
            decimal valorPorte = retornaPorteCartaRegistradaPeloPeso(tbPeso.Value);
            lbEntregaCartaRegistrada.Text = "Dia da postagem + " + Convert.ToString(Convert.ToInt32(precoEPrazo.PrazoEntrega) + 7) + " dia(s) útil";
            lbPrecoCartaRegistrada.Text = Helpers.returnDinheiro(valorPorte.ToString());
            lbMaoCartaRegistrada.Text = Helpers.returnDinheiro(cbMaoPropria.Checked ? ConfiguracoesExcel.MaoPropria : "0,00");
            lbRecebimentoCartaRegistrada.Text = Helpers.returnDinheiro(cbAvisoRecebimento.Checked ? ConfiguracoesExcel.AvisoRecebimento : "0,00");
            lbValorDeclaradoCartaRegistrada.Text = Helpers.returnDinheiro(precoEPrazo.ValorValorDeclarado);

            if (cbDeclaracaoDeValor.Checked)
            {
                if (tbDeclaracaoValor.Value >= 100)
                {
                    decimal NovoValorDeclarado = 100;
                    NovoValorDeclarado = (NovoValorDeclarado * 2) / 100;
                    precoEPrazo.ValorValorDeclarado = NovoValorDeclarado.ToString();
                    lbValorDeclaradoCartaRegistrada.Text = Helpers.returnDinheiro(precoEPrazo.ValorValorDeclarado);
                }
                if (tbDeclaracaoValor.Value < 100)
                {
                    decimal NovoValorDeclarado = tbDeclaracaoValor.Value;
                    NovoValorDeclarado = (NovoValorDeclarado * 2) / 100;
                    precoEPrazo.ValorValorDeclarado = NovoValorDeclarado.ToString();
                    lbValorDeclaradoCartaRegistrada.Text = Helpers.returnDinheiro(precoEPrazo.ValorValorDeclarado);
                }
            }

            lbDomiciliarCartaRegistrada.Text = Helpers.returnResposta(precoEPrazo.EntregaDomiciliar);
            lbSabadoCartaRegistrada.Text = Helpers.returnResposta(precoEPrazo.EntregaSabado);
            lbErroCartaRegistrada.Text = precoEPrazo.Erro == "0" ? "" : precoEPrazo.Erro + "";
            lbMsgErroCartaRegistrada.Text = precoEPrazo.MsgErro;
            lbSemAdicionaisCartaRegistrada.Text = Helpers.returnDinheiro((valorPorte + Convert.ToDecimal(ConfiguracoesExcel.RegistroCarta)).ToString());
            rtbObsCartaRegistrada.Text = precoEPrazo.obsFim;
            decimal NovoValorTotal = valorPorte +
                Convert.ToDecimal(ConfiguracoesExcel.RegistroCarta) +
                Convert.ToDecimal(cbMaoPropria.Checked ? ConfiguracoesExcel.MaoPropria : "0,00") +
                Convert.ToDecimal(cbAvisoRecebimento.Checked ? ConfiguracoesExcel.AvisoRecebimento : "0,00") +
                Convert.ToDecimal(precoEPrazo.ValorValorDeclarado) +
                Convert.ToDecimal((cbPostaRestantePedida.Checked && cbPostaRestantePedida.Enabled) ? ConfiguracoesExcel.PostaRestantePedida : "0,00") +
                ValorTotalProdutosAdicionados;
            lbTotalCartaRegistrada.Text = Helpers.returnDinheiro(NovoValorTotal.ToString());
        }

        private decimal retornaPorteCartaRegistradaPeloPeso(decimal value)
        {
            /*
             *Carta e Cartão Postal à vista e a faturar
            Vigência: 31/01/2020
            Gramas	Básico	Registro	
            Reg.+AR Reg.+ MP Reg.+AR+MP
            Até		20	            2,05	8,40	14,75	15,90	22,25
            Mais de	20	até  50	    2,85	9,20	15,55	16,70	23,05
            Mais de	50	até 100	    3,95	10,30	16,65	17,80	24,15
            Mais de	100	até 150	    4,80	11,15	17,50	18,65	25,00
            Mais de	150	até 200	    5,65	12,00	18,35	19,50	25,85
            Mais de	200	até 250	    6,55	12,90	19,25	20,40	26,75
            Mais de	250	até 300	    7,50	13,85	20,20	21,35	27,70
            Mais de	300	até 350	    8,35	14,70	21,05	22,20	28,55
            Mais de	350	até 400	    9,25	15,60	21,95	23,10	29,45
            Mais de	400	até 450	    10,10	16,45	22,80	23,95	30,30
            Mais de	450	até 500	    11,00	17,35	23,70	24,85	31,20 
             */
            value = value * 1000;
            decimal ValorPorte = Convert.ToDecimal(ConfiguracoesExcel.ValorPorteAte20g);
            if (value <= 20) ValorPorte = Convert.ToDecimal(ConfiguracoesExcel.ValorPorteAte20g);
            if (value > 20 && value <= 50) ValorPorte = Convert.ToDecimal(ConfiguracoesExcel.ValorPorteEntre20gE50g);
            if (value > 50 && value <= 100) ValorPorte = Convert.ToDecimal(ConfiguracoesExcel.ValorPorteEntre50gE100g);
            if (value > 100 && value <= 150) ValorPorte = Convert.ToDecimal(ConfiguracoesExcel.ValorPorteEntre100gE150g);
            if (value > 150 && value <= 200) ValorPorte = Convert.ToDecimal(ConfiguracoesExcel.ValorPorteEntre150gE200g);
            if (value > 200 && value <= 250) ValorPorte = Convert.ToDecimal(ConfiguracoesExcel.ValorPorteEntre200gE250g);
            if (value > 250 && value <= 300) ValorPorte = Convert.ToDecimal(ConfiguracoesExcel.ValorPorteEntre250gE300g);
            if (value > 300 && value <= 350) ValorPorte = Convert.ToDecimal(ConfiguracoesExcel.ValorPorteEntre300gE350g);
            if (value > 350 && value <= 400) ValorPorte = Convert.ToDecimal(ConfiguracoesExcel.ValorPorteEntre350gE400g);
            if (value > 400 && value <= 450) ValorPorte = Convert.ToDecimal(ConfiguracoesExcel.ValorPorteEntre400gE450g);
            if (value > 450 && value <= 500) ValorPorte = Convert.ToDecimal(ConfiguracoesExcel.ValorPorteEntre450gE500g);
            if (value > 500) ValorPorte = Convert.ToDecimal(ConfiguracoesExcel.ValorPorteEntre450gE500g);
            return ValorPorte;

            throw new NotImplementedException();
        }

        /// <summary>
        ///     Inicializa as entradas dos componentes do Sedex12
        /// </summary>
        /// <param name="precoEPrazo"></param>
        ///
        private void inicializarSedex12(cResultado.Servicos.cServico precoEPrazo)
        {
            lbEntregaSedex12.Text = "Dia da postagem + " + precoEPrazo.PrazoEntrega + " dia(s) útil";
            lbPrecoSedex12.Text = Helpers.returnDinheiro(precoEPrazo.ValorSemAdicionais);
            lbMaoSedex12.Text = Helpers.returnDinheiro(precoEPrazo.ValorMaoPropria);
            lbRecebimentoSedex12.Text = Helpers.returnDinheiro(precoEPrazo.ValorAvisoRecebimento);
            lbValorDeclaradoSedex12.Text = Helpers.returnDinheiro(precoEPrazo.ValorValorDeclarado);
            lbDomiciliarSedex12.Text = Helpers.returnResposta(precoEPrazo.EntregaDomiciliar);
            lbSabadoSedex12.Text = Helpers.returnResposta(precoEPrazo.EntregaSabado);
            lbErroSedex12.Text = precoEPrazo.Erro == "0" ? "" : precoEPrazo.Erro + "";
            lbMsgErroSedex12.Text = precoEPrazo.MsgErro;
            lbSemAdicionaisSedex12.Text = Helpers.returnDinheiro(precoEPrazo.ValorSemAdicionais);
            rtbObsSedex12.Text = precoEPrazo.obsFim;
            lbTotalSedex12.Text = Helpers.returnDinheiro(precoEPrazo.Valor);
        }

        /// <summary>
        ///     Inicializa as entradas dos componentes do Sedex10
        /// </summary>
        /// <param name="precoEPrazo"></param>
        ///
        private void inicializarSedex10(cResultado.Servicos.cServico precoEPrazo)
        {
            lbEntregaSedex10.Text = "Dia da postagem + " + precoEPrazo.PrazoEntrega + " dia(s) útil";
            lbPrecoSedex10.Text = Helpers.returnDinheiro(precoEPrazo.ValorSemAdicionais);
            lbMaoSedex10.Text = Helpers.returnDinheiro(precoEPrazo.ValorMaoPropria);
            lbRecebimentoSedex10.Text = Helpers.returnDinheiro(precoEPrazo.ValorAvisoRecebimento);
            lbValorDeclaradoSedex10.Text = Helpers.returnDinheiro(precoEPrazo.ValorValorDeclarado);
            lbDomiciliarSedex10.Text = Helpers.returnResposta(precoEPrazo.EntregaDomiciliar);
            lbSabadoSedex10.Text = Helpers.returnResposta(precoEPrazo.EntregaSabado);
            lbErroSedex10.Text = precoEPrazo.Erro == "0" ? "" : precoEPrazo.Erro + "";
            lbMsgErroSedex10.Text = precoEPrazo.MsgErro;
            lbSemAdicionaisSedex10.Text = Helpers.returnDinheiro(precoEPrazo.ValorSemAdicionais);
            rtbObsSedex10.Text = precoEPrazo.obsFim;
            lbTotalSedex10.Text = Helpers.returnDinheiro(precoEPrazo.Valor);
        }

        /// <summary>
        ///     Inicializa as entradas dos componentes do SedexHoje
        /// </summary>
        /// <param name="precoEPrazo"></param>
        ///
        private void inicializarSedexHoje(cResultado.Servicos.cServico precoEPrazo)
        {
            lbEntregaSedexHoje.Text = "Dia da postagem + " + precoEPrazo.PrazoEntrega + " dia(s) útil";
            lbPrecoSedexHoje.Text = Helpers.returnDinheiro(precoEPrazo.ValorSemAdicionais);
            lbMaoSedexHoje.Text = Helpers.returnDinheiro(precoEPrazo.ValorMaoPropria);
            lbRecebimentoSedexHoje.Text = Helpers.returnDinheiro(precoEPrazo.ValorAvisoRecebimento);
            lbValorDeclaradoSedexHoje.Text = Helpers.returnDinheiro(precoEPrazo.ValorValorDeclarado);
            lbDomiciliarSedexHoje.Text = Helpers.returnResposta(precoEPrazo.EntregaDomiciliar);
            lbSabadoSedexHoje.Text = Helpers.returnResposta(precoEPrazo.EntregaSabado);
            lbErroSedexHoje.Text = precoEPrazo.Erro == "0" ? "" : precoEPrazo.Erro + "";
            lbMsgErroSedexHoje.Text = precoEPrazo.MsgErro;
            lbSemAdicionaisSedexHoje.Text = Helpers.returnDinheiro(precoEPrazo.ValorSemAdicionais);
            rtbObsSedexHoje.Text = precoEPrazo.obsFim;
            lbTotalSedexHoje.Text = Helpers.returnDinheiro(precoEPrazo.Valor);
        }

        /// <summary>
        ///     Reinicia as entradas dos componentes do Sedex
        /// </summary>
        private void reiniciarEntradasDoSedex()
        {
            lbEntregaSedex.Text = String.Empty;
            lbPrecoSedex.Text = String.Empty;
            lbMaoSedex.Text = String.Empty;
            lbRecebimentoSedex.Text = String.Empty;
            lbValorDeclaradoSedex.Text = String.Empty;
            lbDomiciliarSedex.Text = String.Empty;
            lbSabadoSedex.Text = String.Empty;
            lbErroSedex.Text = String.Empty;
            lbMsgErroSedex.Text = String.Empty;
            lbSemAdicionaisSedex.Text = String.Empty;
            rtbObsSedex.Text = String.Empty;
            lbTotalSedex.Text = String.Empty;

            if (cbPagamentoEntrega.Checked == true)
            {
                pictureBoxSedex.Visible = false;
                pictureBoxSedexPagamentoEntrega.Visible = true;
            }
            if (cbPagamentoEntrega.Checked == false)
            {
                pictureBoxSedex.Visible = true;
                pictureBoxSedexPagamentoEntrega.Visible = false;
            }
        }

        /// <summary>
        ///     Reinicia as entradas dos componentes do PAC
        /// </summary>
        private void reiniciarEntradasDoPAC()
        {
            lbEntregaPAC.Text = String.Empty;
            lbPrecoPAC.Text = String.Empty;
            lbMaoPAC.Text = String.Empty;
            lbRecebimentoPAC.Text = String.Empty;
            lbValorDeclaradoPAC.Text = String.Empty;
            lbDomiciliarPAC.Text = String.Empty;
            lbSabadoPAC.Text = String.Empty;
            lbErroPAC.Text = String.Empty;
            lbMsgErroPAC.Text = String.Empty;
            lbSemAdicionaisPAC.Text = String.Empty;
            rtbObsPAC.Text = String.Empty;
            lbTotalPAC.Text = String.Empty;

            if (cbPagamentoEntrega.Checked == true)
            {
                pictureBoxPAC.Visible = false;
                pictureBoxPACPagamentoEntrega.Visible = true;
            }
            if (cbPagamentoEntrega.Checked == false)
            {
                pictureBoxPAC.Visible = true;
                pictureBoxPACPagamentoEntrega.Visible = false;
            }
        }

        /// <summary>
        ///     Reinicia as entradas dos componentes do Sedex10
        /// </summary>
        private void reiniciarEntradasDoSedex12()
        {
            lbEntregaSedex12.Text = String.Empty;
            lbPrecoSedex12.Text = String.Empty;
            lbMaoSedex12.Text = String.Empty;
            lbRecebimentoSedex12.Text = String.Empty;
            lbValorDeclaradoSedex12.Text = String.Empty;
            lbDomiciliarSedex12.Text = String.Empty;
            lbSabadoSedex12.Text = String.Empty;
            lbErroSedex12.Text = String.Empty;
            lbMsgErroSedex12.Text = String.Empty;
            lbSemAdicionaisSedex12.Text = String.Empty;
            rtbObsSedex12.Text = String.Empty;
            lbTotalSedex12.Text = String.Empty;
        }

        /// <summary>
        ///     Reinicia as entradas dos componentes do Sedex10
        /// </summary>
        private void reiniciarEntradasDoSedex10()
        {
            lbEntregaSedex10.Text = String.Empty;
            lbPrecoSedex10.Text = String.Empty;
            lbMaoSedex10.Text = String.Empty;
            lbRecebimentoSedex10.Text = String.Empty;
            lbValorDeclaradoSedex10.Text = String.Empty;
            lbDomiciliarSedex10.Text = String.Empty;
            lbSabadoSedex10.Text = String.Empty;
            lbErroSedex10.Text = String.Empty;
            lbMsgErroSedex10.Text = String.Empty;
            lbSemAdicionaisSedex10.Text = String.Empty;
            rtbObsSedex10.Text = String.Empty;
            lbTotalSedex10.Text = String.Empty;
        }

        /// <summary>
        ///     Reinicia as entradas dos componentes do Sedex Hoje
        /// </summary>
        private void reiniciarEntradasDoSedexHoje()
        {
            lbEntregaSedexHoje.Text = String.Empty;
            lbPrecoSedexHoje.Text = String.Empty;
            lbMaoSedexHoje.Text = String.Empty;
            lbRecebimentoSedexHoje.Text = String.Empty;
            lbValorDeclaradoSedexHoje.Text = String.Empty;
            lbDomiciliarSedexHoje.Text = String.Empty;
            lbSabadoSedexHoje.Text = String.Empty;
            lbErroSedexHoje.Text = String.Empty;
            lbMsgErroSedexHoje.Text = String.Empty;
            lbSemAdicionaisSedexHoje.Text = String.Empty;
            rtbObsSedexHoje.Text = String.Empty;
            lbTotalSedexHoje.Text = String.Empty;
        }

        private void cbDeclaracaoDeValor_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDeclaracaoDeValor.Checked)
            {
                LbInformeOValorDeclarado.Enabled = true;
                tbDeclaracaoValor.Minimum = Convert.ToDecimal(ConfiguracoesExcel.ValorPorteAte20g) * 10;
                tbDeclaracaoValor.Enabled = true;
                tbDeclaracaoValor.Focus();
                tbDeclaracaoValor.Select(0, tbDeclaracaoValor.Text.Length);
            }
            else
            {
                LbInformeOValorDeclarado.Enabled = false;
                tbDeclaracaoValor.Minimum = Convert.ToDecimal("0");
                tbDeclaracaoValor.Enabled = false;
            }
        }

        private void tbCepOrigem_KeyDown(object sender, KeyEventArgs e)
        {
            //e.SuppressKeyPress = true;
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void rbPorCEP_CheckedChanged(object sender, EventArgs e)
        {
            //LbRegiaoSelecionada.Visible = !rbPorCEP.Checked;

            if (rbPorCEP.Checked)
            {
                LbCEPDestino.Visible = tbCepDestino.Visible = pictureBoxBuscarCep.Visible = LblBuscaCEP.Visible = true;
                //LbRegiaoSelecionada.Visible = false;
                LblBuscaCEP.Text = "";
                tbCepDestino.Focus();
                tbCepDestino.SelectAll();
            }
            else
            {
                LbCEPDestino.Visible = tbCepDestino.Visible = pictureBoxBuscarCep.Visible = LblBuscaCEP.Visible = false;
            }
        }

        private void rbPorRegiao_CheckedChanged(object sender, EventArgs e)
        {
            LbRegiaoSelecionada.Visible = false;
            if (rbPorRegiao.Checked)
            {

                string retorno = string.Empty;
                if (tbCepOrigem.Text == "")
                {
                    MessageBox.Show("Informe um Cep Origem.");
                    return;
                }
                using (FormBuscarRegiao form = new FormBuscarRegiao(tbCepOrigem.Text))
                {
                    form.ShowDialog();
                    tbCepDestino.Text = form.CEPRetorno;
                    LbRegiaoSelecionada.Text = string.Format("{0}: {1}", form.TipoSelecionado, form.RegiaoSelecionada);

                    LbRegiaoSelecionada.Visible = true;
                    if (string.IsNullOrEmpty(form.CEPRetorno))
                    {
                        rbPorCEP.Checked = true;
                        //tbCepDestino.Focus();
                        //tbCepDestino.SelectAll();
                    }
                    else
                    {
                        SendKeys.Send("{TAB}");
                    }
                }
            }
        }

        private void tbCepDestino_KeyDown(object sender, KeyEventArgs e)
        {
            //e.SuppressKeyPress = true;
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void checkBoxOpcaoSedex_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void checkBoxOpcaoPAC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void checkBoxOpcaoCartaRegistrada_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void checkBoxOpcaoSedex12_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void checkBoxOpcaoSedex10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void checkBoxOpcaoSedexHoje_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void rbCaixa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void rbEnvelope_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void rbCilindro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void tbPeso_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void cbMaoPropria_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void cbAvisoRecebimento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void cbDeclaracaoDeValor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void cbPagamentoEntrega_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void cbPostaRestantePedida_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void tbDeclaracaoValor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void rbPorCEP_KeyDown(object sender, KeyEventArgs e)
        {
            tbCepDestino.Focus();
            tbCepDestino.SelectAll();
        }

        private void PrecosEPrazo_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    this.SelectNextControl(this.ActiveControl, !e.Shift, false, true, true);
            //}
            if (e.KeyCode == Keys.Escape && panelResultado.Visible)
            {
                btnFechar_Click(null, null);
            }
            else
            {
                if (e.KeyCode == Keys.Escape)
                {
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    var result = MessageBox.Show("Realmente deseja limpar o formulário atual?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.No)
                        return;
                    if (result == DialogResult.Yes)
                        BtnLimpar_Click(null, null);
                }
            }

            if (e.KeyCode == Keys.F2)
            {
                BtnBuscarDimensoesCaixa_Click(null, null);
            }
            if (e.KeyCode == Keys.F3)
            {
                BtnBuscarProdutosAdicionais_Click(null, null);
            }
        }

        private void BtnLimpar_Click(object sender, EventArgs e)
        {
            tbDeclaracaoValor.Value = Convert.ToDecimal(ConfiguracoesExcel.ValorPorteAte20g) * 10;
            cbDeclaracaoDeValor.Checked = false;
            cbAvisoRecebimento.Checked = true;
            cbMaoPropria.Checked = false;
            tbPeso.Value = Convert.ToDecimal("0,300");
            tbDiametro.Value = 0;
            tbComprimento.Value = 16;
            tbLargura.Value = 11;
            tbAltura.Value = 2;
            rbCaixa.Checked = true;
            checkBoxOpcaoSedex12.Checked = checkBoxOpcaoSedex10.Checked = checkBoxOpcaoSedexHoje.Checked = false;
            checkBoxOpcaoSedex.Checked = checkBoxOpcaoPAC.Checked = checkBoxOpcaoCartaRegistrada.Checked = true;
            tbCepOrigem.Text = tbCepDestino.Text = ConfiguracoesExcel.CEPOrigem;
            rbPorCEP.Checked = true;

            rbCaixa_Click(null, null);

            ValorTotalProdutosAdicionados = 0;
            LbValorTotalAdicional.Text = "R$0,00";
            listBox1.Items.Clear();

            tbCepDestino.Focus();
            tbCepDestino.SelectAll();
        }

        private void cbPagamentoEntrega_CheckedChanged(object sender, EventArgs e)
        {
            cbPostaRestantePedida.Enabled = !cbPagamentoEntrega.Checked;
        }

        private void BtnBuscarDimensoesCaixa_Click(object sender, EventArgs e)
        {
            string NomeEndereco = string.Format(@"{0}Geral.xls", System.AppDomain.CurrentDomain.BaseDirectory);
            string NomePlan = "Produtos";
            try
            {
                using (DataTable dt = new ImportarArquivos().ImportarXLSXNovo(NomeEndereco, string.Format("{0}$", NomePlan.Replace("$", "")), "[Nome do Produto], [Dimensões (em cm)], [Tipo]", 0))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow retorno = null;
                        //CarregaGridView(dt);
                        using (BuscarProdutos form = new BuscarProdutos(dt))
                        {
                            form.radioButtonGeral.Visible = false;
                            form.LblTituloQuantidade.Visible = false;
                            form.tbQuantidade.Visible = false;

                            form.ShowDialog(this);

                            if (form.cancelado == true || form.retornoLinhaSelecionada == null)
                            {
                                tbAltura.Focus();
                                tbAltura.Select(0, tbAltura.Text.Length);
                                return;
                            }
                            else
                            {
                                retorno = form.retornoLinhaSelecionada;
                            }
                        }
                        if (retorno != null)
                        {
                            var Medidadas = retorno[1].ToString().ToUpper().Trim().Replace(" ", "").Split('X');
                            if (Medidadas.Length == 2)
                            {
                                string ComprimentoRetornado = Medidadas[0].ToString();
                                tbComprimento.Value = Convert.ToDecimal(ComprimentoRetornado);

                                string LarguraRetornado = Medidadas[1].ToString();
                                tbLargura.Value = Convert.ToDecimal(LarguraRetornado);

                                string AlturaRetornado = "2";
                                tbAltura.Value = Convert.ToDecimal(AlturaRetornado);
                            }
                            if (Medidadas.Length == 3)
                            {
                                string ComprimentoRetornado = Medidadas[0].ToString();
                                tbComprimento.Value = Convert.ToDecimal(ComprimentoRetornado);

                                string LarguraRetornado = Medidadas[1].ToString();
                                tbLargura.Value = Convert.ToDecimal(LarguraRetornado);

                                string AlturaRetornado = Medidadas[2].ToString();
                                tbAltura.Value = Convert.ToDecimal(AlturaRetornado);
                            }

                            tbPeso.Focus();
                            tbPeso.Select(0, tbPeso.Text.Length);

                            //MessageBox.Show(string.Format("Retorno selecionado:{0} - {1}", retorno[0], retorno[1]));
                        }
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

        private void BtnBuscarProdutosAdicionais_Click(object sender, EventArgs e)
        {
            string NomeEndereco = string.Format(@"{0}Geral.xls", System.AppDomain.CurrentDomain.BaseDirectory);
            string NomePlan = "Produtos";
            try
            {
                using (DataTable dt = new ImportarArquivos().ImportarXLSXNovo(NomeEndereco, string.Format("{0}$", NomePlan.Replace("$", "")), "[Nome do Produto], [Preços (em reais - R$)], [Tipo]", 0))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow retorno = null;
                        decimal quantidadeDesejada = 0;
                        using (BuscarProdutos form = new BuscarProdutos(dt, _telaBuscaProdutosAdicionais: true))
                        {
                            //form.dataGridView1.Columns[1].DefaultCellStyle.Format = "c2";
                            //form.dataGridView1.Columns[1].DefaultCellStyle.FormatProvider = System.Globalization.CultureInfo.GetCultureInfo("pt-BR");
                            form.ShowDialog(this);

                            if (form.cancelado == true || form.retornoLinhaSelecionada == null)
                            {
                                return;
                            }
                            else
                            {
                                retorno = form.retornoLinhaSelecionada;
                                quantidadeDesejada = form.tbQuantidade.Value;
                            }
                        }
                        if (retorno != null)
                        {
                            decimal valorAtual = Convert.ToDecimal(retorno[1]) * quantidadeDesejada;

                            ValorTotalProdutosAdicionados = ValorTotalProdutosAdicionados + valorAtual;
                            LbValorTotalAdicional.Text = Helpers.returnDinheiro(ValorTotalProdutosAdicionados.ToString());
                            listBox1.Items.Add(string.Format("{0} X {1} --> {2}", Convert.ToInt32(quantidadeDesejada), retorno[0], Helpers.returnDinheiro(valorAtual.ToString())));
                            listBox1.SelectedIndex = listBox1.Items.Count - 1;
                        }
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



        /// <summary>
        ///     Formata o texto para decimal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void tbAltura_TextChanged(object sender, EventArgs e)
        {
            Helpers.retornarMoedaFormatada(ref tbAltura);
        }

        /// <summary>
        ///     Permite apenas números no TextBox altura
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void tbAltura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tbAltura_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void tbAltura_Enter(object sender, EventArgs e)
        {
            tbAltura.Select(0, tbAltura.Text.Length);
            if (MouseButtons == MouseButtons.Left)
            {
                selectByMouse = true;
            }
        }

        private void tbAltura_MouseDown(object sender, MouseEventArgs e)
        {
            if (selectByMouse)
            {
                tbAltura.Select(0, tbAltura.Text.Length);
                selectByMouse = false;
            }
        }

        /// <summary>
        ///     Formata o texto para decimal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void tbLargura_TextChanged(object sender, EventArgs e)
        {
            Helpers.retornarMoedaFormatada(ref tbLargura);
        }

        /// <summary>
        ///     Permite apenas números no TextBox largura
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void tbLargura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tbLargura_Enter(object sender, EventArgs e)
        {
            tbLargura.Select(0, tbLargura.Text.Length);
            if (MouseButtons == MouseButtons.Left)
            {
                selectByMouse = true;
            }
        }

        private void tbLargura_MouseDown(object sender, MouseEventArgs e)
        {
            if (selectByMouse)
            {
                tbLargura.Select(0, tbLargura.Text.Length);
                selectByMouse = false;
            }
        }

        private void tbLargura_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        /// <summary>
        ///     Formata o texto para decimal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void tbComprimento_TextChanged(object sender, EventArgs e)
        {
            Helpers.retornarMoedaFormatada(ref tbComprimento);
        }

        /// <summary>
        ///     Permite apenas números no TextBox comprimento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void tbComprimento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tbComprimento_Enter(object sender, EventArgs e)
        {
            tbComprimento.Select(0, tbComprimento.Text.Length);
            if (MouseButtons == MouseButtons.Left)
            {
                selectByMouse = true;
            }
        }

        private void tbComprimento_MouseDown(object sender, MouseEventArgs e)
        {
            if (selectByMouse)
            {
                tbComprimento.Select(0, tbComprimento.Text.Length);
                selectByMouse = false;
            }
        }

        private void tbComprimento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        /// <summary>
        ///     Permite apenas números no TextBox diâmetro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void tbDiametro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tbDiametro_Enter(object sender, EventArgs e)
        {
            tbDiametro.Select(0, tbDiametro.Text.Length);
            if (MouseButtons == MouseButtons.Left)
            {
                selectByMouse = true;
            }
        }

        private void tbDiametro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        /// <summary>
        ///     Formata o texto para decimal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void tbDiametro_TextChanged(object sender, EventArgs e)
        {
            Helpers.retornarMoedaFormatada(ref tbDiametro);
        }

        private void tbDiametro_MouseDown(object sender, MouseEventArgs e)
        {
            if (selectByMouse)
            {
                tbDiametro.Select(0, tbDiametro.Text.Length);
                selectByMouse = false;
            }
        }

        private void tbPeso_Enter(object sender, EventArgs e)
        {
            tbPeso.Select(0, tbPeso.Text.Length);
            if (MouseButtons == MouseButtons.Left)
            {
                selectByMouse = true;
            }
        }

        private void tbPeso_MouseDown(object sender, MouseEventArgs e)
        {
            if (selectByMouse)
            {
                tbPeso.Select(0, tbPeso.Text.Length);
                selectByMouse = false;
            }
        }

        private void tbCepDestino_Enter(object sender, EventArgs e)
        {
            tbCepDestino.Select(0, tbCepDestino.Text.Length);
            if (MouseButtons == MouseButtons.Left)
            {
                selectByMouse = true;
            }
        }

        private void tbCepDestino_MouseDown(object sender, MouseEventArgs e)
        {
            if (selectByMouse)
            {
                tbCepDestino.Select(0, tbCepDestino.Text.Length);
                selectByMouse = false;
            }
        }

        private void tbCepOrigem_Enter(object sender, EventArgs e)
        {
            tbCepOrigem.Select(0, tbCepOrigem.Text.Length);
            if (MouseButtons == MouseButtons.Left)
            {
                selectByMouse = true;
            }
        }

        private void tbCepOrigem_MouseDown(object sender, MouseEventArgs e)
        {
            if (selectByMouse)
            {
                tbCepOrigem.Select(0, tbCepOrigem.Text.Length);
                selectByMouse = false;
            }
        }

        private void tbDeclaracaoValor_Enter(object sender, EventArgs e)
        {
            tbDeclaracaoValor.Select(0, tbDeclaracaoValor.Text.Length);
            if (MouseButtons == MouseButtons.Left)
            {
                selectByMouse = true;
            }
        }

        private void tbDeclaracaoValor_MouseDown(object sender, MouseEventArgs e)
        {
            if (selectByMouse)
            {
                tbDeclaracaoValor.Select(0, tbDeclaracaoValor.Text.Length);
                selectByMouse = false;
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (listBox1.Items.Count == 0) return;
                string valorItemRemovido = listBox1.SelectedItem.ToString().Replace(" ", "").Split('$')[1];
                ValorTotalProdutosAdicionados = ValorTotalProdutosAdicionados - Convert.ToDecimal(valorItemRemovido);
                LbValorTotalAdicional.Text = Helpers.returnDinheiro(ValorTotalProdutosAdicionados.ToString());
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
            }
        }

        private void pictureBoxBuscarCep_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(tbCepDestino.Text))
            {
                this.Cursor = Cursors.WaitCursor;
                ConsultaCEP(tbCepDestino.Text);
                this.Cursor = Cursors.Default;
            }
            else
            {
                MessageBox.Show("Informe um CEP para a consulta.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tbCepDestino.Focus();
            }
        }

        private void ConsultaCEP(string cep)
        {
            if (!string.IsNullOrWhiteSpace(cep))
            {
                using (var ws = new BuscaCEP.AtendeClienteClient())
                {
                    try
                    {
                        var endereco = ws.consultaCEP(cep.Trim());
                        LblBuscaCEP.Text = endereco.end + " " + endereco.complemento2 + "- " + endereco.bairro + "\n" + endereco.cidade + "/" + endereco.uf;
                    }
                    catch (Exception err)
                    {
                        string numberErr = err.GetHashCode().ToString();
                        string errMessage = err.Message;
                        string message = string.Concat("Ocorreu um erro inexperado. Erro Nº: ", numberErr, ". ", errMessage, ".");
                        if (err.Message == "CEP INVÁLIDO")
                        {
                            MessageBox.Show("CEP inválido!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else if (err.Message == "CEP NAO ENCONTRADO")
                        {
                            MessageBox.Show("CEP não encontrado!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else if (err.GetHashCode() == -2146233087)
                        {
                            MessageBox.Show("Falha na comunicação com o WS dos correios. Verifique sua conexão com a internet.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            MessageBox.Show(message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Informe um CEP para a consulta.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tbCepDestino.Focus();
            }


        }
    }
}
