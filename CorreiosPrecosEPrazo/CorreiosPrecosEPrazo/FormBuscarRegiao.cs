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
    public partial class FormBuscarRegiao : Form
    {
        public string CEPRetorno = string.Empty;
        public string TipoSelecionado = string.Empty;
        public string RegiaoSelecionada = string.Empty;
        public string CEPOrigem = string.Empty;

        public FormBuscarRegiao(string cepOrigem)
        {
            CEPOrigem = cepOrigem;
            InitializeComponent();
        }

        private void FormBuscarRegiao_Load(object sender, EventArgs e)
        {

        }

        private void buttonCapital_Local_Click(object sender, EventArgs e)
        {
            CEPRetorno = CEPOrigem;
            RegiaoSelecionada = buttonCapital_Local.Text;
            TipoSelecionado = "Capital";
            this.Close();
        }

        private void buttonCapital_Estadual_Divisa_Click(object sender, EventArgs e)
        {
            CEPRetorno = "77001-970";
            RegiaoSelecionada = buttonCapital_Estadual_Divisa.Text;
            TipoSelecionado = "Capital";
            this.Close();
        }

        private void buttonCapital_DF_GO_Click(object sender, EventArgs e)
        {
            CEPRetorno = "71691-024";
            RegiaoSelecionada = buttonCapital_DF_GO.Text;
            TipoSelecionado = "Capital";
            this.Close();
        }

        private void buttonCapital_PA_Click(object sender, EventArgs e)
        {
            CEPRetorno = "66017-970";
            RegiaoSelecionada = buttonCapital_PA.Text;
            TipoSelecionado = "Capital";
            this.Close();
        }

        private void buttonCapital_AP_MT_MS_MG_RJ_SP_Click(object sender, EventArgs e)
        {
            CEPRetorno = "01031-970";
            RegiaoSelecionada = buttonCapital_AP_MT_MS_MG_RJ_SP.Text;
            TipoSelecionado = "Capital";
            this.Close();
        }

        private void buttonCapital_AM_PR_BA_ES_PE_RR_SC_Click(object sender, EventArgs e)
        {
            CEPRetorno = "40015-970";
            RegiaoSelecionada = buttonCapital_AM_PR_BA_ES_PE_RR_SC.Text;
            TipoSelecionado = "Capital";
            this.Close();
        }

        private void buttonCapital_AL_MA_RS_SE_AC_CE_PB_PI_RN_RO_Click(object sender, EventArgs e)
        {
            CEPRetorno = "65010-970";
            RegiaoSelecionada = buttonCapital_AL_MA_RS_SE_AC_CE_PB_PI_RN_RO.Text;
            TipoSelecionado = "Capital";
            this.Close();
        }

        private void buttonInterior_Local_Click(object sender, EventArgs e)
        {
            CEPRetorno = CEPOrigem;
            RegiaoSelecionada = buttonInterior_Local.Text;
            TipoSelecionado = "Interior";
            this.Close();
        }

        private void buttonInterior_Estadual_Divisa_Click(object sender, EventArgs e)
        {
            CEPRetorno = "77993-000";
            RegiaoSelecionada = buttonInterior_Estadual_Divisa.Text;
            TipoSelecionado = "Interior";
            this.Close();

        }

        private void buttonInterior__DF_GO_Click(object sender, EventArgs e)
        {
            CEPRetorno = "76590-000";
            RegiaoSelecionada = buttonInterior__DF_GO.Text;
            TipoSelecionado = "Interior";
            this.Close();
        }

        private void buttonInterior_PA_Click(object sender, EventArgs e)
        {
            CEPRetorno = "68540-000";
            RegiaoSelecionada = buttonInterior_PA.Text;
            TipoSelecionado = "Interior";
            this.Close();
        }

        private void buttonInterior_AP_MT_MS_MG_RJ_SP_Click(object sender, EventArgs e)
        {
            CEPRetorno = "78580-000";
            RegiaoSelecionada = buttonInterior_AP_MT_MS_MG_RJ_SP.Text;
            TipoSelecionado = "Interior";
            this.Close();
        }

        private void buttonInterior_AM_PR_BA_ES_PE_RR_SC_Click(object sender, EventArgs e)
        {
            CEPRetorno = "89770-000";
            RegiaoSelecionada = buttonInterior_AM_PR_BA_ES_PE_RR_SC.Text;
            TipoSelecionado = "Interior";
            this.Close();
        }

        private void buttonInterior_AL_MA_RS_SE_AC_CE_PB_PI_RN_RO_Click(object sender, EventArgs e)
        {
            CEPRetorno = "63260-000";
            RegiaoSelecionada = buttonInterior_AL_MA_RS_SE_AC_CE_PB_PI_RN_RO.Text;
            TipoSelecionado = "Interior";
            this.Close();
        }
    }
}
