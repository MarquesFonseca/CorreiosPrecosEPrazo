using System;
using System.Windows.Forms;

namespace CorreiosPrecosEPrazo.ClassesDiversas
{
    public static class Mensagens2
    {
        private static string Titulo;

        static Mensagens2()
        {
            Mensagens2.Titulo = "Rensoftware AutoNF-e";
        }

        #region MesagensEstaticas

        /// <summary>
        /// Os dados do campo \"{0}\" não podem ficam em branco.
        /// </summary>
        /// <param name="Objeto">Objeto da lista de componente a validar do formulário base.</param>
        public static DialogResult MsgObjetoNulo(object Objeto)
        {
             return Mensagens2.Informa(string.Format("Os dados do campo \"{0}\" não podem ficam em branco.", Objeto));
        }

        /// <summary>
        /// Os dados do campo \"{0}\" não podem ficam em branco.
        /// </summary>
        /// <param name="Objeto">Descrição do campo.</param>
        public static DialogResult MsgObjetoNulo(string Descricao)
        {
            return Mensagens2.Informa(string.Format("Os dados do campo \"{0}\" não podem ficam em branco.", Descricao));
        }

        /// <summary>
        /// Tem certeza de que deseja realizar esta operação?
        /// </summary>
        public static DialogResult MsgConfirmaAcao()
        {
            return Pergunta("Tem certeza de que deseja realizar esta operação?");
        }

        /// <summary>
        /// Você está desativando este registro. Deseja continuar?
        /// </summary>
        public static DialogResult MsgDesativaRegistro()
        {
            return Pergunta("Você está desativando este registro. Deseja continuar?");
        }

        /// <summary>
        /// Operação realizada com sucesso.
        /// </summary>
        public static void MsgSucesso()
        {
            Informa("Operação realizada com sucesso.");
        }
        
        /// <summary>
        /// O valor informado não é valido para este tipo de operação.
        /// </summary>
        public static void MsgValorInvalido()
        {
            Erro("O valor informado não é valido para este tipo de operação.");
        }

        /// <summary>
        /// O registro foi excluído com sucesso
        /// </summary>
        public static void MsgExcluir()
        {
            Informa("O registro foi excluído com sucesso.");
        }

        /// <summary>
        /// "Erro na tentativa de salvar os dados do atual registro. \n" + ERRO
        /// </summary>
        public static void MsgErroGravar(string ERRO)
        {
            Erro(String.Format("Erro na tentativa de salvar os dados do atual registro. \n{0}", ERRO));
        }

        /// <summary>
        /// "Erro na tentativa de salvar os dados do atual registro.
        /// </summary>
        public static void MsgErroGravar()
        {
            Erro("Erro na tentativa de salvar os dados do atual registro.");
        }

        /// <summary>
        /// "Erro ao excluir o registro. \n" + ERRO
        /// </summary>
        public static void MsgErroExcluir(string ERRO)
        {
            Erro(String.Format("Erro ao excluir o registro. \n{0}", ERRO));
        }

        /// <summary>
        /// "Erro ao excluir o registro.
        /// </summary>
        public static void MsgErroExcluir()
        {
            Erro("Erro ao excluir o registro.");
        }

        /// <summary>
        /// "Esta operação não pode ser executada devido a um erro interno. \n" + ERRO
        /// </summary>
        public static void MsgErroGenerico(string ERRO)
        {
            Erro(String.Format("Esta operação não pode ser executada devido a um erro interno. \n{0}", ERRO));
        }

        /// <summary>
        /// "Esta operação não pode ser executada devido a um erro interno.
        /// </summary>
        public static void MsgErroGenerico()
        {
            Erro("Esta operação não pode ser executada devido a um erro interno.");
        }

        /// <summary>
        /// Deseja realmente excluir este registro?
        /// </summary>
        public static DialogResult MsgPergExcluir()
        {
            return Pergunta("Deseja realmente excluir este registro?");
        }

        #endregion
        
        #region Mensagens dinamicas

        /// <summary>
        /// Mensagem de questionamento (??)
        /// </summary>
        /// <param name="mensagem">Texto que aparecerá na mensagem</param>
        /// <returns>Botão selecionado</returns>
        public static DialogResult Pergunta(string mensagem)
        {
            return MessageBox.Show(null, mensagem, Titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// Mensagem de questionamento (??)
        /// </summary>
        /// <returns>Botão selecionado</returns>
        public static DialogResult Pergunta(string mensagem, MessageBoxButtons Botoes)
        {
            return MessageBox.Show(null, mensagem, Titulo, Botoes, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// Mensagem de questionamento (??)
        /// </summary>
        /// <returns>Botão selecionado</returns>
        public static DialogResult Pergunta(string mensagem, MessageBoxButtons Botoes, MessageBoxIcon Icone)
        {
            return MessageBox.Show(null, mensagem, Titulo, Botoes, Icone, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// Mensagem de questionamento (??)
        /// </summary>
        /// <returns>Botão selecionado</returns>
        public static DialogResult Pergunta(string mensagem, MessageBoxButtons Botoes, MessageBoxIcon Icone, MessageBoxDefaultButton BotaoPadrao)
        {
            return MessageBox.Show(null, mensagem, Titulo, Botoes, Icone, BotaoPadrao);
        }

        /// <summary>
        /// Mostra mensagem de informação padrão
        /// </summary>
        /// <returns>Botão selecionado na mensagem</returns>
        public static DialogResult Informa(string mensagem)
        {
            return MessageBox.Show(null, mensagem, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// Mostra mensagem de informação padrão
        /// </summary>
        /// <returns>Botão selecionado na mensagem</returns>
        public static DialogResult Informa(string mensagem, MessageBoxIcon Icone)
        {
            return MessageBox.Show(null, mensagem, Titulo, MessageBoxButtons.OK, Icone, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// Mostra mensagem de informação padrão
        /// </summary>
        /// <returns>Botão selecionado na mensagem</returns>
        public static DialogResult Informa(string mensagem, MessageBoxIcon Icone, MessageBoxButtons Botoes)
        {
            return MessageBox.Show(null, mensagem, Titulo, Botoes, Icone, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// Mostra mensagem de informação padrão
        /// </summary>
        /// <returns>Botão selecionado na mensagem</returns>
        public static DialogResult Informa(string mensagem, MessageBoxIcon Icone, MessageBoxButtons Botoes, MessageBoxDefaultButton BotaoPadrao)
        {
            return MessageBox.Show(null, mensagem, Titulo, Botoes, Icone, BotaoPadrao);
        }

        /// <summary>
        /// Exibe mensagem de erro
        /// </summary>
        public static void Erro(string mensagem)
        {
            MessageBox.Show(mensagem, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }

        #endregion
    }    
}
