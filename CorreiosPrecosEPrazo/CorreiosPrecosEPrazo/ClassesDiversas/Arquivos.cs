using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections;
using System.Text;
using CorreiosPrecosEPrazo.ClassesDiversas;

public class Arquivos : IDisposable
{
    //private Application exAplicacao;
    //private Workbook wbPastaDeTrabalho;
    //private Worksheet wsPlanilha;

    public int AbrirArquivo(string nomeEEnderecoDoArquivo)
    {
        try
        {
            new Process { StartInfo = { FileName = nomeEEnderecoDoArquivo } }.Start();
            return 1;
        }
        catch (Exception)
        {
            return -1;
        }
    }

    public int AbrirPasta(string CaminhoDaPasta, bool CriarSeNaoExistir, int Retorno = 1)
    {
        if (!Directory.Exists(CaminhoDaPasta) && CriarSeNaoExistir)
        {
            try
            {
                Directory.CreateDirectory(CaminhoDaPasta);
                Process.Start(CaminhoDaPasta);
                return (Retorno = 0);
            }
            catch
            {
                Retorno = -1;
            }
        }
        try
        {
            Process.Start(CaminhoDaPasta);
            return 0;
        }
        catch (Exception)
        {
            return -1;
        }
    }

    public int ApagarArquivo(string PastaArquivo)
    {
        int retorno = 0;
        try
        {
            File.Delete(PastaArquivo);
        }
        catch
        {
            retorno = -1;
        }
        return retorno;
    }

    public int CriarPasta(string CaminhoPasta)
    {
        int Retorno = 1;
        if (Directory.Exists(CaminhoPasta))
        {
            return Retorno;
        }
        try
        {
            Directory.CreateDirectory(CaminhoPasta);
            return 0;
        }
        catch
        {
            return -1;
        }
    }

    public int CriarPasta(string Local, string NomePasta)
    {
        int Retorno = 1;
        string NovaPasta = string.Join(@"\", new string[] { Local, NomePasta });
        if (Directory.Exists(NovaPasta))
        {
            return Retorno;
        }
        try
        {
            DirectoryInfo a = Directory.CreateDirectory(NovaPasta);
            return 0;
        }
        catch
        {
            return -1;
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    //public int GerarArquivoXls(List<string> cabecalho, System.Data.DataTable tabela, string CaminhoMaisArquivo)
    //{
    //    int retorno = -1;
    //    try
    //    {
    //        this.exAplicacao = new ApplicationClass();
    //        this.wbPastaDeTrabalho = this.exAplicacao.get_Workbooks().Add((XlWBATemplate) (-4167));
    //        this.wsPlanilha = (Worksheet) this.wbPastaDeTrabalho.get_Worksheets().get__Default(1);
    //        this.wsPlanilha.set_Name("Rensoftware");
    //        int lin = 1;
    //        for (int i = 0; i < cabecalho.Count; i++)
    //        {
    //            this.wsPlanilha.get_Cells().set__Default(lin, i + 1, cabecalho[i]);
    //        }
    //        lin = 0;
    //        int col = 0;
    //        for (int linha = 2; linha <= (tabela.Rows.Count + 1); linha++)
    //        {
    //            for (int coluna = 1; coluna <= cabecalho.Count; coluna++)
    //            {
    //                if (tabela.Columns[col].DataType == typeof(string))
    //                {
    //                    this.wsPlanilha.get_Cells().set__Default(linha, coluna, "'" + tabela.Rows[lin][col].ToString());
    //                    col++;
    //                }
    //                else
    //                {
    //                    this.wsPlanilha.get_Cells().set__Default(linha, coluna, tabela.Rows[lin][col]);
    //                    col++;
    //                }
    //            }
    //            lin++;
    //            col = 0;
    //        }
    //        this.wbPastaDeTrabalho.SaveAs(CaminhoMaisArquivo, (XlFileFormat) (-4143), Type.Missing, Type.Missing, false, false, 1, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
    //        this.wbPastaDeTrabalho.Close(null, null, null);
    //        retorno = 0;
    //        CS$1$0000 = retorno;
    //    }
    //    catch (Exception ex)
    //    {
    //        this.wbPastaDeTrabalho.Close(false, null, null);
    //        throw ex;
    //    }
    //    finally
    //    {
    //        this.exAplicacao.Quit();
    //        Marshal.ReleaseComObject(this.wbPastaDeTrabalho);
    //        Marshal.ReleaseComObject(this.exAplicacao);
    //    }
    //    return CS$1$0000;
    //}

    public void GravarArquivo(string PastaArquivo, string Linha)
    {
        try
        {
            this.ApagarArquivo(PastaArquivo);
            using (StreamWriter Log = new StreamWriter(PastaArquivo, true))
            {
                Log.WriteLine(Linha);
                Log.Close();
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    public void VerificaPasta(string CaminhoPasta)
    {
        DirectoryInfo Pasta = new DirectoryInfo(CaminhoPasta);
        if (!Pasta.Exists)
        {
            Pasta.Create();
        }
    }





    public static ArrayList LerArquivo(string Arquivo)
    {
        ArrayList Retorno = new ArrayList();

        FileInfo _arquivo = new FileInfo(Arquivo);
        StreamReader linha = new StreamReader(_arquivo.FullName, Encoding.Default);

        string Linha;

        while (!linha.EndOfStream)
        {
            Linha = linha.ReadLine();
            Retorno.Add(Linha);
        }

        return Retorno;
    }

    /// <summary>
    /// Metodo para escrever texto na ultima linha de um arquivo
    /// </summary>
    /// <param name="Linha">Informe a mensagem que gostaria de escrever</param>
    /// <param name="Arquivo">Informe o caminho juntamente com o nome do arquivo</param>
    /// <example>GravarLinhaNoFinalDoArquivo("mensagem teste de gravação",@"C:\teste\log.txt");</example>
    /// <!--Metodo já testado-->
    public static void GravarLinhaNoFinalDoArquivo(string Linha, string Arquivo)
    {
        FileStream _Arquivo = new FileStream(Arquivo, FileMode.Append);
        StreamWriter sw = new StreamWriter(_Arquivo, Encoding.UTF8);
        sw.WriteLine(Linha);

        sw.Flush();
        sw.Close();
        _Arquivo.Close();
    }

    /// <summary>
    /// Carrega caminho para salvar arquivo
    /// </summary>
    /// <param name="_Titulo">Informe o titulo do SaveFileDialog</param>
    /// <param name="_DiretorioInicial">Informe o diretório inicial</param>
    /// <param name="ExtencaoDefault">Informe a extenção padrão</param>
    /// <param name="Filtro">Informe o filtro de extenção</param>
    /// <example>
    /// SalvarArquivo("Titulo do arquivo", "C:\\Teste",".jpg","Arquivos JPG|*.jpg|Arquivos GIF|*.gif");
    /// 
    /// OU
    ///
    /// SalvarArquivo("Titulo do arquivo", "C:\\Teste",".jpg","Imagem|*.jpg;*.gif;*.png");
    /// </example>
    /// <returns></returns>
    public static string SalvarArquivo(string _Titulo, string _DiretorioInicial, string ExtencaoDefault, string Filtro)
    {
        System.Windows.Forms.SaveFileDialog SalvarArquivo = new System.Windows.Forms.SaveFileDialog();

        SalvarArquivo.Title = _Titulo;
        SalvarArquivo.InitialDirectory = _DiretorioInicial;
        SalvarArquivo.FileName = string.Empty;
        SalvarArquivo.DefaultExt = ExtencaoDefault;
        SalvarArquivo.Filter = Filtro;
        SalvarArquivo.RestoreDirectory = true;

        if (SalvarArquivo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            return SalvarArquivo.FileName;
        }
        return "";
    }

    /// <summary>
    /// Carrega arquivo do HD utilizando OpenFileDialog
    /// </summary>
    /// <param name="_Titulo">Informe o titulo do OpenFileDialog</param>
    /// <param name="_DiretorioInicial">Informe o diretório inicial</param>
    /// <param name="ExtencaoDefault">Informe a extenção padrão</param>
    /// <param name="Filtro">Informe o filtro de estenção</param>
    /// <example>
    /// BuscarArquivo("Titulo do arquivo", "C:\\Teste",".jpg","Arquivos JPG|*.jpg|Arquivos GIF|*.gif");
    /// 
    /// OU
    ///
    /// BuscarArquivo("Titulo do arquivo", "C:\\Teste",".jpg","Imagem|*.jpg;*.gif;*.png");
    /// /// 
    /// </example>
    /// <returns></returns>
    public static string BuscarArquivo(string _Titulo, string _DiretorioInicial, string ExtencaoDefault, string Filtro)
    {
        System.Windows.Forms.OpenFileDialog AbrirArquivo = new System.Windows.Forms.OpenFileDialog();

        AbrirArquivo.Title = _Titulo;
        AbrirArquivo.InitialDirectory = _DiretorioInicial;
        AbrirArquivo.FileName = string.Empty;
        AbrirArquivo.DefaultExt = ExtencaoDefault;
        AbrirArquivo.Filter = Filtro;
        AbrirArquivo.RestoreDirectory = true;

        if (AbrirArquivo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            return AbrirArquivo.FileName;
        }
        return "";
    }

    public static void CriptografarArquivo(string Arquivo)
    {
        CriptografarDescriptografarArquivo(Arquivo, true);
    }

    public static void DescriptografarArquivo(string Arquivo)
    {
        CriptografarDescriptografarArquivo(Arquivo, false);
    }

    private static void CriptografarDescriptografarArquivo(string Arquivo, bool Criptografa)
    {
        if (!Existe(Arquivo, false))
        {
            throw new Exception("O arquivo não exite");
        }

        FileInfo arquivo = new FileInfo(Arquivo);
        StreamReader linha = new StreamReader(arquivo.FullName);
        string ArquivoTemporario = Environment.CurrentDirectory + "\\" + Guid.NewGuid().ToString() + ".txt";
        string Linha;

        while (Existe(ArquivoTemporario, false))
        {
            ArquivoTemporario = Environment.CurrentDirectory + "\\" + Guid.NewGuid().ToString() + ".txt";
        }


        //GravarLinhaNoFinalDoArquivo("", ArquivoTemporario);

        try
        {
            if (arquivo.Exists == true)
            {

                while (!linha.EndOfStream)
                {
                    Linha = linha.ReadLine();
                    if (Linha != null)
                    {
                        if (Criptografa)
                            GravarLinhaNoFinalDoArquivo(Criptografia.Criptografa(Linha), ArquivoTemporario);
                        else
                            GravarLinhaNoFinalDoArquivo(Criptografia.Descriptografa(Linha), ArquivoTemporario);
                    }
                }
                linha.Close();
                MoverArquivo(ArquivoTemporario, Arquivo, true);
            }
        }
        catch (Exception)
        {
            //Classes.Logs.Logs.GravaLogExcessao("Erro ao criptografar arquivo.", ex, Versao.RetornaVersao(), Versao.RetornaDataVersao());
        }
    }

    /// <summary>
    /// Metodo para saber se o arquivo ou um diretório existe
    /// </summary>
    /// <param name="Arquivo">Informe o caminho do arquivo ou do diretório que deseja verificar</param>
    /// <example>Existe("C:\\Log\\Teste.TXT");</example>
    /// <returns>Se o arquivo existir retorna TRUE, senão retorna FALSE</returns>
    /// <!--Metodo já testado-->
    public static bool Existe(string Arquivo, bool Diretorio)
    {
        if (Diretorio)
        {
            if (System.IO.Directory.Exists(Arquivo))
                return true;
            else
                return false;
        }
        if (File.Exists(Arquivo))
            return true;
        else
            return false;
    }

    /// <summary>
    /// Metodo para deletar arquivo
    /// </summary>
    /// <param name="RaizDiretorio">Informe a raiz do arquivo</param>
    /// <param name="NomeArquivoCompleto">Informe o caminho do arquivo</param>
    /// <example>DeletarArquivo("C:\\","TESTE\\TESTE.TXT");</example>
    /// <returns>Se operação realizada com sucesso, retorna TRUE, senão Retorna FALSE</returns>
    /// <!--Metodo já testado-->
    public static bool DeletarArquivo(string RaizDiretorio, string NomeArquivoCompleto)//, string Arquivo)
    {
        try
        {
            FileInfo Arquivo = new FileInfo(NomeArquivoCompleto);
            DirectoryInfo DiretorioArquivo = new DirectoryInfo(RaizDiretorio);
            if (DiretorioArquivo.Exists)
            {
                Arquivo.Delete();
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
        
    /// <summary>
    /// Carrega o arquivo do disco byte a byte.
    /// </summary>
    /// <param name="caminhoOrigem">Informe o caminho do arquivo a ser carregado</param>
    /// <returns>Retorna o arquivo carregado do disco</returns>
    /// <!--Metodo já testado-->
    public static byte[] CarregarArquivo(string CaminhoOrigem)
    {
        byte[] Dados = null;
        FileInfo Arquivo = new FileInfo(CaminhoOrigem);
        long Bytes = Arquivo.Length;
        FileStream fStream = new FileStream(CaminhoOrigem, FileMode.Open, FileAccess.Read);
        BinaryReader bReader = new BinaryReader(fStream);
        Dados = bReader.ReadBytes((int)Bytes);
        fStream.Close();
        return Dados;
    }

    /// <summary>
    /// Copia arquivo de um diretorio para outro, substituindo o arquivo existente
    /// </summary>
    /// <param name="Origem">Informe o diretorio de origem</param>
    /// <param name="Destino">Informe o diretorio de destino</param>
    /// <param name="NomeArquivo">Informe o nome do arquivo a ser copiado</param>
    /// <!--Metodo já testado-->
    public static void CopiarArquivo(string Origem, string Destino, string NomeArquivo)
    {
        string Arquivo = NomeArquivo;

        // Crie uma nova pasta de estino, se necessario.
        CriarDiretorio(Destino);

        //verifica se o diretorio de destino existe
        if (System.IO.Directory.Exists(Destino))
        {
            string ArquivoOrigem = System.IO.Path.Combine(Origem, Arquivo);
            string ArquivoDestino = System.IO.Path.Combine(Destino, Arquivo);

            //Para copiar um ariquivo para outro local e sobrescrever o existente
            System.IO.File.Copy(ArquivoOrigem, ArquivoDestino, true);
        }
    }

    /// <summary>
    /// Metodo para criar diretório
    /// </summary>
    /// <param name="Destino">Passe o caminho do diretório que deseja criar</param>
    public static void CriarDiretorio(string Destino)
    {
        //se não existir o diretório será criado
        if (!System.IO.Directory.Exists(Destino))
        {
            System.IO.Directory.CreateDirectory(Destino);
            //return "Diretório criado com sucesso!";
        }
        else
        {
            //return "Diretório já existe!";
        }
    }


    /// <summary>
    /// metodo para excluir arquivo
    /// </summary>
    /// <param name="Origem">informe o caminho do arquivo com o arquivo</param>
    /// <example>ExcluirArquivo(@"C:\SISTEMA\TESTE.TXT");</example>
    /// <!--Metodo já testado-->
    public static void ExcluirArquivo(string Origem)
    {
        if (Existe(Origem, false))
        {
            System.IO.File.Delete(Origem);
        }

        #region Deletar
        // ...or by using FileInfo instance method.
        //System.IO.FileInfo fi = new System.IO.FileInfo(@"C:\Users\Public\DeleteTest\test2.txt");
        //try
        //{
        //    fi.Delete();
        //}
        //catch (System.IO.IOException e)
        //{
        //    Console.WriteLine(e.Message);
        //}

        // Delete a directory. Must be writable or empty.
        //try
        //{
        //    System.IO.Directory.Delete(Origem);
        //}
        //catch (System.IO.IOException e)
        //{
        //    Console.WriteLine(e.Message);
        //}
        //// Delete a directory and all subdirectories with Directory static method...
        //if (System.IO.Directory.Exists(Origem))
        //{
        //    try
        //    {
        //        System.IO.Directory.Delete(Origem, true);
        //    }

        //    catch (System.IO.IOException e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //}

        //// ...or with DirectoryInfo instance method.
        //System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Origem);
        //// Delete this dir and all subdirs.
        //try
        //{
        //    di.Delete(true);
        //}
        //catch (System.IO.IOException e)
        //{
        //    Console.WriteLine(e.Message);
        //}
        #endregion
    }

    /// <summary>
    /// Metodo para excluir um diretorio que esteja vazio
    /// </summary>
    /// <param name="Origem">Informe o caminho do diretorio que deseja excluir</param>
    /// <example>ExcluirDiretorioVazio(@"C:\SISTEMA\TESTE");</example>
    public static void ExcluirDiretorioVazio(string Origem)
    {
        try
        {
            System.IO.Directory.Delete(Origem);
        }
        catch (System.IO.IOException e)
        {
            Console.WriteLine(e.Message);
        }

    }

    /// <summary>
    /// Metodo para excluir diretório e subdiretórios
    /// </summary>
    /// <param name="Origem">Informe o caminho do diretório que deseja excluir</param>
    /// <example>ExcluirDiretorioESubDiretorios(@"C:\SISTEMA\TESTE");</example>
    public static void ExcluirDiretorioESubDiretorios(string Origem)
    {
        if (Existe(Origem, true))
        {
            try
            {
                System.IO.Directory.Delete(Origem, true);
            }

            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    /// <summary>
    /// Chama um arquivo passado por parametro
    /// </summary>
    /// <param name="Origem">Arquivo a ser execultado</param>
    /// <!--Metodo já testado-->
    public static void ExecultarArquivo(string Origem)
    {
        if (Existe(Origem, false))
            Process.Start(Origem);
    }

    /// <summary>
    /// Chama um arquivo passado por parametro
    /// </summary>
    /// <param name="Origem">Arquivo a ser execultado</param>
    /// <!--Metodo já testado-->
    public static void ExecultarArquivo(string Origem, string Args)
    {
        if (Existe(Origem, false))
            Process.Start(Origem, Args);
    }

    /// <summary>
    /// Metodo para renomear um arquivo
    /// </summary>
    /// <param name="NomeAntigo">Informe o caminho juntamente com o antigo nome do arquivo</param>
    /// <param name="NomeNovo">Informe o caminho juntamente com o novo nome do artivo</param>
    public static void RenomearArquivo(string NomeAntigo, string NomeNovo)
    {
        // vamos renomear o arquivo
        if (Existe(NomeAntigo, false))
            if (!Existe(NomeNovo, false))
                File.Move(NomeAntigo, NomeNovo);
    }

    /// <summary>
    /// Metodo utilizado para mover arquivos de um diretório para outro
    /// </summary>
    /// <param name="Origem">Informe o caminho de origem juntamente com o nome do arquivo</param>
    /// <param name="Destino">Informe o caminho de destino juntamente com o nome do arquivo</param>
    public static void MoverArquivo(string Origem, string Destino, bool SubstituirExistente)
    {
        try
        {
            if (Existe(Origem, false))
                if (SubstituirExistente || !Existe(Destino, false))
                {
                    ExcluirArquivo(Destino);
                    File.Move(Origem, Destino);
                }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    
}

