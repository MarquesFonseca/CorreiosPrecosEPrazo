using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CorreiosPrecosEPrazo
{
    public static class ConfiguracoesExcel
    {
        public static void CarregaConfiguracoesExcel()
        {
            string NomeEndereco = string.Format(@"{0}Geral.xls", System.AppDomain.CurrentDomain.BaseDirectory);
            string NomePlan = "Configuracoes";
            try
            {
                using (DataTable dt = new ImportarArquivos().ImportarXLSXNovo(NomeEndereco, string.Format("{0}$", NomePlan.Replace("$", "")), "[Descrição], [Valor]", 0))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        CEPOrigem = dt.Rows[0][1].ToString();
                        MaoPropria = dt.Rows[1][1].ToString().Replace("R$ ","");
                        AvisoRecebimento = dt.Rows[2][1].ToString().Replace("R$ ", "");
                        PagamentoEntregaComVPNe = dt.Rows[3][1].ToString().Replace("R$ ", "");
                        PostaRestantePedida = dt.Rows[4][1].ToString().Replace("R$ ", "");
                        ValorPorteAte20g = dt.Rows[5][1].ToString().Replace("R$ ", "");
                        ValorPorteEntre20gE50g = dt.Rows[6][1].ToString().Replace("R$ ", "");
                        ValorPorteEntre50gE100g = dt.Rows[7][1].ToString().Replace("R$ ", "");
                        ValorPorteEntre100gE150g = dt.Rows[8][1].ToString().Replace("R$ ", "");
                        ValorPorteEntre150gE200g = dt.Rows[9][1].ToString().Replace("R$ ", "");
                        ValorPorteEntre200gE250g = dt.Rows[10][1].ToString().Replace("R$ ", "");
                        ValorPorteEntre250gE300g = dt.Rows[11][1].ToString().Replace("R$ ", "");
                        ValorPorteEntre300gE350g = dt.Rows[12][1].ToString().Replace("R$ ", "");
                        ValorPorteEntre350gE400g = dt.Rows[13][1].ToString().Replace("R$ ", "");
                        ValorPorteEntre400gE450g = dt.Rows[14][1].ToString().Replace("R$ ", "");
                        ValorPorteEntre450gE500g = dt.Rows[15][1].ToString().Replace("R$ ", "");
                        RegistroCarta = dt.Rows[16][1].ToString().Replace("R$ ", "");
                        CodigoSARAParaCartaRegistrada = dt.Rows[17][1].ToString();
                        CodigoSARAParaSEDEX = dt.Rows[18][1].ToString();
                        CodigoSARAParaPAC = dt.Rows[19][1].ToString();
                        CodigoSARAParaSEDEX12 = dt.Rows[20][1].ToString();
                        CodigoSARAParaSEDEX10 = dt.Rows[21][1].ToString();
                        CodigoSARAParaSEDEXHoje = dt.Rows[22][1].ToString();
                    }
                    else
                    {
                        Mensagens.Erro("Não foi possível carregar nenhum registro apartir do .xls informado. Por favor selecione outro arquivo.");
                    }
                }
            }
            catch (Exception ex)
            {
                Mensagens.Erro(string.Format("Não foi possível carregar o arquivo: {0}", ex.Message));
            }
        }

        public static string CEPOrigem;
        public static string MaoPropria;
        public static string AvisoRecebimento;
        public static string PagamentoEntregaComVPNe;
        public static string PostaRestantePedida;
        public static string ValorPorteAte20g;
        public static string ValorPorteEntre20gE50g;
        public static string ValorPorteEntre50gE100g;
        public static string ValorPorteEntre100gE150g;
        public static string ValorPorteEntre150gE200g;
        public static string ValorPorteEntre200gE250g;
        public static string ValorPorteEntre250gE300g;
        public static string ValorPorteEntre300gE350g;
        public static string ValorPorteEntre350gE400g;
        public static string ValorPorteEntre400gE450g;
        public static string ValorPorteEntre450gE500g;
        public static string RegistroCarta;
        public static string CodigoSARAParaCartaRegistrada;
        public static string CodigoSARAParaSEDEX;
        public static string CodigoSARAParaPAC;
        public static string CodigoSARAParaSEDEX12;
        public static string CodigoSARAParaSEDEX10;
        public static string CodigoSARAParaSEDEXHoje;



    }
}
