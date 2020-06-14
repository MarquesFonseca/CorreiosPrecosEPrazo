namespace CorreiosPrecosEPrazo
{
    partial class BuscarProdutos
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BuscarProdutos));
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbQuantidade = new System.Windows.Forms.NumericUpDown();
            this.LblTituloQuantidade = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButtonGeral = new System.Windows.Forms.RadioButton();
            this.radioButtonEnvelopes = new System.Windows.Forms.RadioButton();
            this.radioButtonCaixas = new System.Windows.Forms.RadioButton();
            this.LbnTituloFormulario = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnConfirmar = new System.Windows.Forms.Button();
            this.BtnCancelar = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbQuantidade)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tbQuantidade);
            this.panel2.Controls.Add(this.LblTituloQuantidade);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.radioButtonGeral);
            this.panel2.Controls.Add(this.radioButtonEnvelopes);
            this.panel2.Controls.Add(this.radioButtonCaixas);
            this.panel2.Controls.Add(this.LbnTituloFormulario);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(639, 59);
            this.panel2.TabIndex = 1;
            // 
            // tbQuantidade
            // 
            this.tbQuantidade.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbQuantidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.tbQuantidade.ForeColor = System.Drawing.Color.Red;
            this.tbQuantidade.Location = new System.Drawing.Point(528, 26);
            this.tbQuantidade.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tbQuantidade.Name = "tbQuantidade";
            this.tbQuantidade.Size = new System.Drawing.Size(99, 26);
            this.tbQuantidade.TabIndex = 5;
            this.tbQuantidade.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbQuantidade.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // LblTituloQuantidade
            // 
            this.LblTituloQuantidade.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblTituloQuantidade.AutoSize = true;
            this.LblTituloQuantidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTituloQuantidade.ForeColor = System.Drawing.Color.DodgerBlue;
            this.LblTituloQuantidade.Location = new System.Drawing.Point(524, 5);
            this.LblTituloQuantidade.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblTituloQuantidade.Name = "LblTituloQuantidade";
            this.LblTituloQuantidade.Size = new System.Drawing.Size(107, 20);
            this.LblTituloQuantidade.TabIndex = 4;
            this.LblTituloQuantidade.Text = "Quantidade:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label2.Location = new System.Drawing.Point(18, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tipo do produto";
            // 
            // radioButtonGeral
            // 
            this.radioButtonGeral.AutoSize = true;
            this.radioButtonGeral.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonGeral.Location = new System.Drawing.Point(273, 29);
            this.radioButtonGeral.Name = "radioButtonGeral";
            this.radioButtonGeral.Size = new System.Drawing.Size(98, 20);
            this.radioButtonGeral.TabIndex = 3;
            this.radioButtonGeral.Text = "[&G] - Geral";
            this.radioButtonGeral.UseVisualStyleBackColor = true;
            this.radioButtonGeral.CheckedChanged += new System.EventHandler(this.radioButtonGeral_CheckedChanged);
            // 
            // radioButtonEnvelopes
            // 
            this.radioButtonEnvelopes.AutoSize = true;
            this.radioButtonEnvelopes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonEnvelopes.Location = new System.Drawing.Point(134, 29);
            this.radioButtonEnvelopes.Name = "radioButtonEnvelopes";
            this.radioButtonEnvelopes.Size = new System.Drawing.Size(133, 20);
            this.radioButtonEnvelopes.TabIndex = 3;
            this.radioButtonEnvelopes.Text = "[&E] - Envelopes";
            this.radioButtonEnvelopes.UseVisualStyleBackColor = true;
            this.radioButtonEnvelopes.CheckedChanged += new System.EventHandler(this.radioButtonEnvelopes_CheckedChanged);
            // 
            // radioButtonCaixas
            // 
            this.radioButtonCaixas.AutoSize = true;
            this.radioButtonCaixas.Checked = true;
            this.radioButtonCaixas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonCaixas.Location = new System.Drawing.Point(22, 29);
            this.radioButtonCaixas.Name = "radioButtonCaixas";
            this.radioButtonCaixas.Size = new System.Drawing.Size(106, 20);
            this.radioButtonCaixas.TabIndex = 2;
            this.radioButtonCaixas.TabStop = true;
            this.radioButtonCaixas.Text = "[&C] - Caixas";
            this.radioButtonCaixas.UseVisualStyleBackColor = true;
            this.radioButtonCaixas.CheckedChanged += new System.EventHandler(this.radioButtonCaixas_CheckedChanged);
            // 
            // LbnTituloFormulario
            // 
            this.LbnTituloFormulario.AutoSize = true;
            this.LbnTituloFormulario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbnTituloFormulario.Location = new System.Drawing.Point(3, 6);
            this.LbnTituloFormulario.Name = "LbnTituloFormulario";
            this.LbnTituloFormulario.Size = new System.Drawing.Size(0, 16);
            this.LbnTituloFormulario.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(639, 333);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Preview";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 16);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(633, 314);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnConfirmar);
            this.panel1.Controls.Add(this.BtnCancelar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 392);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(639, 43);
            this.panel1.TabIndex = 2;
            // 
            // BtnConfirmar
            // 
            this.BtnConfirmar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnConfirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnConfirmar.Location = new System.Drawing.Point(6, 6);
            this.BtnConfirmar.Name = "BtnConfirmar";
            this.BtnConfirmar.Size = new System.Drawing.Size(157, 32);
            this.BtnConfirmar.TabIndex = 0;
            this.BtnConfirmar.Text = "Confirmar - [Enter]";
            this.BtnConfirmar.UseVisualStyleBackColor = true;
            this.BtnConfirmar.Click += new System.EventHandler(this.BtnConfirmar_Click);
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancelar.Location = new System.Drawing.Point(473, 6);
            this.BtnCancelar.Name = "BtnCancelar";
            this.BtnCancelar.Size = new System.Drawing.Size(157, 32);
            this.BtnCancelar.TabIndex = 1;
            this.BtnCancelar.Text = "Cancela&r - [Esc]";
            this.BtnCancelar.UseVisualStyleBackColor = true;
            this.BtnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // BuscarProdutos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 435);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(655, 474);
            this.Name = "BuscarProdutos";
            this.ShowInTaskbar = false;
            this.Text = "Selecione um produto";
            this.Load += new System.EventHandler(this.BuscarProdutos_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BuscarProdutos_KeyDown);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbQuantidade)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label LbnTituloFormulario;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnConfirmar;
        private System.Windows.Forms.Button BtnCancelar;
        private System.Windows.Forms.RadioButton radioButtonEnvelopes;
        private System.Windows.Forms.RadioButton radioButtonCaixas;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label LblTituloQuantidade;
        public System.Windows.Forms.RadioButton radioButtonGeral;
        public System.Windows.Forms.NumericUpDown tbQuantidade;
    }
}