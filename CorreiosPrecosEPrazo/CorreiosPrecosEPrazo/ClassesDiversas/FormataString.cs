using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Collections;

namespace CorreiosPrecosEPrazo.ClassesDiversas
{
    /// <summary>
    /// Classe com Extension Methods para manupulação de strings.
    /// </summary>
    public static class FormataString
    {
        /// <summary>
        /// Remove todos os espaços do texto.
        /// </summary>
        /// <param name="Texto">Texto a ser alterado</param>
        /// <returns>Texto alterado</returns>
        public static string RemoveTodoEspaco(this string Texto)
        {
            Texto = Texto.Trim();

            while (Texto.Contains(" "))
            {
                Texto = Texto.Remove(Texto.IndexOf(' '), 1);
            }

            return Texto;
        }

        /// <summary>
        /// Remove acentos de uma string
        /// </summary>
        /// <param name="match">Texto</param>
        /// <returns>String sem acentos</returns>
        public static string RemoveAcento_DICIONARIO(this String match)
        {
            const string ComAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            const string SemAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";
            Dictionary<string, string> dicStr = new Dictionary<string, string>();
            for (int i = 0; i < ComAcentos.Length; i++)
                dicStr.Add(ComAcentos[i].ToString(), SemAcentos[i].ToString());
            if (dicStr.ContainsKey(match))
            {
                return dicStr[match].ToString();
            }
            else
            {
                return match;
            }
        }

        /// <summary>
        /// Remove o acento de todas as palavras do texto.
        /// </summary>
        /// <param name="match">Texto a ser alterado</param>
        /// <returns>Texto alterado</returns>
        public static string RemoveAcento_UNICODE(this string texto)
        {
            string s = texto.Normalize(NormalizationForm.FormD);

            StringBuilder sb = new StringBuilder();

            for (int k = 0; k < s.Length; k++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(s[k]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(s[k]);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Retorna a string invertida
        /// </summary>
        /// <param name="Texto">Texto</param>
        /// <returns>Texto invertido</returns>
        public static string InvertString(this string Texto)
        {
            char[] ITENS = Texto.ToCharArray();
            string Retorno = "";
            for (int i = ITENS.Length - 1; i >= 0; i--)
            {
                Retorno += ITENS[i].ToString();
            }
            return Retorno;
        }

        public static string Fibonacci(string Texto)
        {
            string Retorno = "";
            for (int i = Texto.Length; i > 0; i--)
            {
                Retorno = Retorno + Texto.Substring(i - 1, 1);
            }
            return Retorno;
        }

        /// <summary>
        /// Remove espaços no começo, no fim da frase e substitui espaços duplos no meio da fase por espaço simples.
        /// </summary>
        /// <param name="Texto">Texto a ser modificado</param>
        /// <returns>Texto modificado</returns>
        public static string RemoveExcessoEspaco(this string Texto)
        {
            Texto = Texto.Trim();

            while (Texto.Contains("  "))
            {
                Texto = Texto.Replace("  ", " ");
            }

            return Texto;
        }

        /// <summary>
        /// Remove todos os elementos de uma IList
        /// </summary>
        public static void RemoveTodos(this IList List)
        {
            for (int i = (List.Count - 1); i >= 0; i--)
                List.RemoveAt(i);
        }

        /// <summary>
        /// Mescla todos os elementos do array.
        /// </summary>
        /// <param name="Array1">Array a ser mesclado.</param>
        /// <returns>Mesclagem de todos os itens.</returns>
        /// <by>Ricardo Santos Cruz</by>
        /// <date>24-06-2009</date>
        public static string JoinString(this object[] Array1)
        {
            string Retorno = string.Empty;

            Array.ForEach(Array1, item => Retorno += Retorno.Equals(string.Empty) ? item.ToString() : String.Format("{0}", item));

            return Retorno;
        }

        /// <summary>
        /// Mescla todos os elementos do array.
        /// </summary>
        /// <param name="separador">Separador dos objetos</param>
        /// <param name="Array1">Array a ser mesclado.</param>
        /// <returns>Mesclagem de todos os itens com o separador escolhido.</returns>
        /// <by>Ricardo Santos Cruz</by>
        /// <date>14-01-2010</date>
        public static string JoinString(this object[] Array1, string separador)
        {
            string Retorno = string.Empty;

            Array.ForEach(Array1, item => Retorno += Retorno.Equals(string.Empty) ? item.ToString() : String.Format("{0}{1}", separador, item));

            return Retorno;
        }

        /// <summary>
        /// Remove todos os espaços do texto.
        /// </summary>
        /// <param name="Texto">Texto a ser alterado</param>
        /// <returns>Texto alterado</returns>
        public static string RemoverEspaco(this string Texto)
        {
            Texto = Texto.Trim();

            while (Texto.Contains(" "))
            {
                Texto = Texto.Remove(Texto.IndexOf(' '));
            }

            return Texto;
        }

        /// <summary>
        /// Remove a pontuação de uma estring.
        /// </summary>
        public static string RemovePontuacao(this string Texto)
        {
            string Retorno = string.Empty;

            foreach (char item in Texto)
            {
                if (!char.IsPunctuation(item))
                {
                    Retorno += item.ToString();
                }
            }

            return Retorno;
        }

        /// <summary>
        /// Remove os simbolos de uma string.
        /// </summary>
        public static string RemoveSimbolos(this string Texto)
        {
            string Retorno = string.Empty;

            foreach (char item in Texto)
            {
                if (!char.IsSymbol(item))
                {
                    Retorno += item.ToString();
                }
            }

            return Retorno;
        }

        /// <summary>
        /// Retorna somente os números do texto.
        /// </summary>
        /// <param name="Texto">Texto a ser removido</param>
        public static string SomenteNumeros(this string Texto)
        {
            string Retorno = string.Empty;

            foreach (char item in Texto)
            {
                if (char.IsNumber(item))
                {
                    Retorno += item.ToString();
                }
            }

            return Retorno;
        }

        /// <summary>
        /// Retorna somente as letras do texto.
        /// </summary>
        /// <param name="Texto">Texto a ser removido</param>
        public static string SomenteLetras(this string Texto)
        {
            string Retorno = string.Empty;

            foreach (char item in Texto)
            {
                if (char.IsLetter(item))
                {
                    Retorno += item.ToString();
                }
            }

            return Retorno;
        }

        /// <summary>
        /// Incrementa as casas decimais até o número indicado.
        /// </summary>
        /// <param name="Texto">Valor a ser incrementado</param>
        /// <param name="QtdCasas">Quantidade de casas decimais</param>
        /// <returns>Valor alterado</returns>
        public static string CasasDecimais(this string Texto, int QtdCasas)
        {
            string Retorno = string.Empty;
            string DeposDoPonto = string.Empty;

            bool temPonto = false;
            foreach (char item in Texto)
            {
                if (item.Equals(','))
                {
                    temPonto = true;
                }
            }

            if (temPonto)
            {
                Texto = Texto.Replace(',', '.');
                DeposDoPonto = Texto.Substring(Texto.IndexOf('.') + 1);

                //fixa o tamanho das casas decimais
                if (DeposDoPonto.Length > QtdCasas)
                {
                    DeposDoPonto = DeposDoPonto.Substring(0, QtdCasas);
                }
                else if (DeposDoPonto.Length < QtdCasas)
                {
                    while (DeposDoPonto.Length < QtdCasas)
                    {
                        DeposDoPonto += "0";
                    }
                }

                Retorno += Texto.Substring(0, Texto.IndexOf('.'));
            }
            else
            {
                Retorno = Texto;
                for (int i = 0; i < QtdCasas; i++)
                {
                    DeposDoPonto += "0";
                }
            }

            return String.Format("{0}.{1}", Retorno, DeposDoPonto);
        }

        /// <summary>
        /// Este trecho de código mostra como remover quebras de 
        /// linhas de uma string.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static string RemoveQuebraDeLinhas(this string texto)
        {
            string s = texto;
            // remove as quebras de linhas
            s = s.Replace("\n", "");
            s = s.Replace("\r", "");

            return s.ToString();
        }

        /// <summary>
        /// Transformar em maiúscula apenas a primeira letra de uma string.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static string PrimeiraLetraMaiuscula(this string texto)
        {
            string txt = texto;

            char primeira = char.ToUpper(txt[0]);
            txt = primeira + txt.Substring(1);

            return txt;
        }

        /// <summary>
        /// Transformar em letras maiúsculas as iniciais de cada palavra em uma string
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static string IniciaisMaiusculoDeCadaPalavra(this string texto)
        {
            // using System.Globalization;

            string frase = texto;

            frase =
              CultureInfo.CurrentCulture.TextInfo.ToTitleCase(frase);

            return frase;
        }

        /// <summary>
        /// Substitui todos os espaços em uma string pelo caractere de sublinhado(underline).
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static string substituiEspacosPorUnderline(this string texto)
        {
            string frase = texto;

            // substitui os espaços por underline
            frase = frase.Replace(" ", "_");

            return frase;
        }

        /// <summary>
        /// Conta as palavras de uma string usando o espaço como separador
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static string contaPalavrasDeString(this string texto)
        {
            string frase = texto;
            int cont = 0;

            // remove os espaços em excesso
            frase = frase.RemoveExcessoEspaco();

            // conta as palavras
            cont = frase.Split(' ').Length;

            return cont.ToString();
        }

        /// <summary>
        /// Retorna a quantidade de linhas do texto.
        /// </summary>
        /// <param name="texto">Texto a ser verificado.</param>
        /// <returns>Número de linhas</returns>
        public static int QuantidadeLinhas(this string texto)
        {
            string[] textoDividido = texto.Split('\r', '\n');
            return textoDividido.Count(r => r != "");
        }

        /// <summary>
        /// Formata a string para ficar dentro dos padrões XML, sem
        /// acentos e caracteres especiais.
        /// </summary>
        /// <param name="Texto">Texto a ser verificado.</param>
        /// <returns>Texto alterado.</returns>
        public static string FormataStringXML(this string Texto)
        {
            Texto = Texto.RemoveExcessoEspaco().RemoveAcento_DICIONARIO().RemoveAcento_UNICODE().RemoveSimbolos();
            Texto = Texto.Replace("<", "&lt;");
            Texto = Texto.Replace(">", "&gt;");
            Texto = Texto.Replace("&", "&amp;");
            Texto = Texto.Replace("'", "&#39;");
            Texto = Texto.Replace("\"", "&quot;");
            return Texto;
        }

        /// <summary>
        /// Formata uma string no formato 01012010 para 01/01/2010.
        /// </summary>
        /// <param name="Valor">String com o texto da data sem os separadores.</param>
        /// <returns>Retorna string formatada.</returns>
        public static string RetornaFormatoData(this string Valor)
        {
            try
            {
                Valor = Valor.Replace("/", "");
                return string.Format("{0}/{1}/{2}", Valor.Substring(0, 2), Valor.Substring(2, 2), Valor.Substring(4, 4));
            }
            catch
            {
                return "";
            }
        }
    }
}
