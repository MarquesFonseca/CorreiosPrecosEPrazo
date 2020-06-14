namespace CorreiosPrecosEPrazo
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AbrirTamplates = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.Menu1 = new System.Windows.Forms.MenuStrip();
            this.testeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuArquivoExcelXLSX = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuArquivoExcelXLS = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuArquivoExcelCSV = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuArquivoExcelXML = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Menu1
            // 
            this.Menu1.Dock = System.Windows.Forms.DockStyle.None;
            this.Menu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testeToolStripMenuItem});
            this.Menu1.Location = new System.Drawing.Point(3, 9);
            this.Menu1.MdiWindowListItem = this.testeToolStripMenuItem;
            this.Menu1.Name = "Menu1";
            this.Menu1.Size = new System.Drawing.Size(272, 24);
            this.Menu1.TabIndex = 11;
            this.Menu1.Text = "menuStrip1";
            // 
            // testeToolStripMenuItem
            // 
            this.testeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuArquivoExcelXLSX,
            this.MenuArquivoExcelXLS,
            this.MenuArquivoExcelCSV,
            this.MenuArquivoExcelXML});
            this.testeToolStripMenuItem.Name = "testeToolStripMenuItem";
            this.testeToolStripMenuItem.Size = new System.Drawing.Size(172, 20);
            this.testeToolStripMenuItem.Text = "Buscar arquivo para Importar";
            // 
            // MenuArquivoExcelXLSX
            // 
            this.MenuArquivoExcelXLSX.Name = "MenuArquivoExcelXLSX";
            this.MenuArquivoExcelXLSX.Size = new System.Drawing.Size(188, 22);
            this.MenuArquivoExcelXLSX.Text = "Arquivo Excel (*.xlsx)";
            this.MenuArquivoExcelXLSX.Visible = false;
            this.MenuArquivoExcelXLSX.Click += new System.EventHandler(this.MenuArquivoExcelXLSX_Click);
            // 
            // MenuArquivoExcelXLS
            // 
            this.MenuArquivoExcelXLS.Name = "MenuArquivoExcelXLS";
            this.MenuArquivoExcelXLS.Size = new System.Drawing.Size(188, 22);
            this.MenuArquivoExcelXLS.Text = "Arquivo Excel ( *.xls )";
            this.MenuArquivoExcelXLS.Click += new System.EventHandler(this.MenuArquivoExcelXLS_Click);
            // 
            // MenuArquivoExcelCSV
            // 
            this.MenuArquivoExcelCSV.Name = "MenuArquivoExcelCSV";
            this.MenuArquivoExcelCSV.Size = new System.Drawing.Size(188, 22);
            this.MenuArquivoExcelCSV.Text = "Arquivo Excel ( *.csv )";
            this.MenuArquivoExcelCSV.Visible = false;
            this.MenuArquivoExcelCSV.Click += new System.EventHandler(this.MenuArquivoExcelCSV_Click);
            // 
            // MenuArquivoExcelXML
            // 
            this.MenuArquivoExcelXML.Name = "MenuArquivoExcelXML";
            this.MenuArquivoExcelXML.Size = new System.Drawing.Size(188, 22);
            this.MenuArquivoExcelXML.Text = "Arquivo XML ( *.xml )";
            this.MenuArquivoExcelXML.Visible = false;
            this.MenuArquivoExcelXML.Click += new System.EventHandler(this.MenuArquivoExcelXML_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.Menu1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Menu1.ResumeLayout(false);
            this.Menu1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog AbrirTamplates;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.MenuStrip Menu1;
        private System.Windows.Forms.ToolStripMenuItem testeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuArquivoExcelXLSX;
        private System.Windows.Forms.ToolStripMenuItem MenuArquivoExcelXLS;
        private System.Windows.Forms.ToolStripMenuItem MenuArquivoExcelCSV;
        private System.Windows.Forms.ToolStripMenuItem MenuArquivoExcelXML;
    }
}