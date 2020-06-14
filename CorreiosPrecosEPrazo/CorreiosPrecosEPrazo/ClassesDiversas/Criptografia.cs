using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace CorreiosPrecosEPrazo.ClassesDiversas
{
    /// <summary>
    /// Classe de segurança do sistema
    /// </summary>
    public static class Criptografia
    {
        /// <summary>
        /// Criptografa string
        /// </summary>
        /// <param name="Texto">Texto a ser criptgrafado</param>
        /// <returns>Valor criptografado</returns>
        public static string Criptografa(string Texto)
        {
            //Byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(Texto);
            //return Convert.ToBase64String(b);

            string Retorno;
            //Byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(Texto);
            Byte[] b = System.Text.UTF8Encoding.UTF8.GetBytes(Texto);
            Retorno = Convert.ToBase64String(b);
            Retorno = FormataString.Fibonacci(Retorno + "yrcyovtipyohtsbsfo");
            //b = System.Text.ASCIIEncoding.ASCII.GetBytes(Retorno);
            b = System.Text.UTF8Encoding.UTF8.GetBytes(Retorno);
            Retorno = Convert.ToBase64String(b);
            //b = System.Text.ASCIIEncoding.ASCII.GetBytes(Retorno);
            b = System.Text.UTF8Encoding.UTF8.GetBytes(Retorno);
            Retorno = Convert.ToBase64String(b);
            Retorno = FormataString.Fibonacci(Retorno);
            //b = System.Text.ASCIIEncoding.ASCII.GetBytes(Retorno);
            b = System.Text.UTF8Encoding.UTF8.GetBytes(Retorno);
            Retorno = Convert.ToBase64String(b);
            Retorno = FormataString.Fibonacci(Retorno);
            return Retorno;
        }



        /// <summary>
        /// Descriptografa string
        /// </summary>
        /// <param name="Texto">Valor criptografado</param>
        /// <returns>Valor descriptografado</returns>
        public static string Descriptografa(string Texto)
        {
            try
            {
                #region DELETAR
                //string Retorno;
                //Byte[] b = Convert.FromBase64String(Texto);
                //Retorno = System.Text.Encoding.ASCII.GetString(b);
                //Retorno = FormataString.Fibonacci(Retorno + "yrcyovtipyohtsbsfo");
                //b = Convert.FromBase64String(Retorno);
                //Retorno = System.Text.Encoding.ASCII.GetString(b);
                //Retorno = FormataString.Fibonacci(Retorno);
                //b = Convert.FromBase64String(Retorno);
                //Retorno = System.Text.Encoding.ASCII.GetString(b);
                //Retorno = FormataString.Fibonacci(Retorno);
                //return Retorno;

                ////
                #endregion

                string Retorno;
                Retorno = FormataString.Fibonacci(Texto);
                Byte[] b = Convert.FromBase64String(Retorno);
                //Retorno = System.Text.Encoding.ASCII.GetString(b);
                Retorno = System.Text.UTF8Encoding.UTF8.GetString(b);
                Retorno = FormataString.Fibonacci(Retorno);
                b = Convert.FromBase64String(Retorno);
                //Retorno = System.Text.Encoding.ASCII.GetString(b);
                Retorno = System.Text.UTF8Encoding.UTF8.GetString(b);
                b = Convert.FromBase64String(Retorno);
                //Retorno = System.Text.Encoding.ASCII.GetString(b);
                Retorno = System.Text.UTF8Encoding.UTF8.GetString(b);
                Retorno = FormataString.Fibonacci(Retorno);
                Retorno = Retorno.Replace("yrcyovtipyohtsbsfo", "");

                b = Convert.FromBase64String(Retorno);
                //Retorno = System.Text.Encoding.ASCII.GetString(b);
                Retorno = System.Text.UTF8Encoding.UTF8.GetString(b);

                return Retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Retorna uma string criptografada usando o formato Hash
        /// </summary> 
        /// <param name="text">Texto que será criptografado</param>
        public static string Cript_Hash(string text)
        {
            UnicodeEncoding Ue = new UnicodeEncoding();
            byte[] ByteSourceText = Ue.GetBytes(text);
            MD5CryptoServiceProvider Md5 = new MD5CryptoServiceProvider();
            byte[] ByteHash = Md5.ComputeHash(ByteSourceText);
            return Convert.ToBase64String(ByteHash);
        }

        /// <summary>
        /// Utiliza se para criptografar a senha do usuário, a mesma não poderá ser descriptografada
        /// </summary>
        /// <param name="Senha">Infromar a senha do usuário</param>
        /// <param name="CodigoFuncionario">Informar o codigo do funcionario</param>
        /// <returns>Retorna a senha criptografada</returns>
        public static string CriptografarSenha(string Senha, int CodigoFuncionario)
        {
            return Cript_Hash(Cript_Hash(CodigoFuncionario.ToString() + Senha) + CodigoFuncionario.ToString());
        }
    }
}
