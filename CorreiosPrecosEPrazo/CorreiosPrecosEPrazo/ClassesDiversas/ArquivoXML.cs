using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CorreiosPrecosEPrazo.ClassesDiversas
{
    public static class ArquivoXML
    {
        public static bool GravaArquivoXML(DataTable Dt, string DirArquivo, string NomeArquivo)
        {
            string Arquivo = string.Format("{0}\\{1}.XML", DirArquivo, NomeArquivo);

            if (Dt.Rows.Count >= 0)
            {
                //verifica se a pasta existe
                using (Arquivos Arq = new Arquivos())
                {
                    Arq.VerificaPasta(DirArquivo);
                }

                //DirectoryInfo dir = new DirectoryInfo(DirArquivo);      
                //Dt.WriteXmlSchema(Arquivo);
                Dt.WriteXml(Arquivo);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static DataTable AbrirArquivoXML(string DirArquivo, string NomeArquivo)
        {
            string Arquivo = string.Format("{0}\\{1}.XML", DirArquivo, NomeArquivo);
            try
            {
                using (DataSet Ds = new DataSet())
                {
                    Ds.ReadXml(Arquivo);

                    if (Ds.Tables[0].Rows.Count > 0)
                        return Ds.Tables[0];
                    else
                        return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
