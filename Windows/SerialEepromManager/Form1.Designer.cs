namespace SerialEepromManager
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.cBSerialPorts = new System.Windows.Forms.ComboBox();
            this.bConnetti = new System.Windows.Forms.Button();
            this.tBInfo = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.bDisconnetti = new System.Windows.Forms.Button();
            this.bReadAll = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.bWriteAll = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cBSerialPorts
            // 
            this.cBSerialPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBSerialPorts.FormattingEnabled = true;
            this.cBSerialPorts.Location = new System.Drawing.Point(398, 23);
            this.cBSerialPorts.Name = "cBSerialPorts";
            this.cBSerialPorts.Size = new System.Drawing.Size(108, 21);
            this.cBSerialPorts.TabIndex = 0;
            this.cBSerialPorts.SelectedIndexChanged += new System.EventHandler(this.cBSerialPorts_SelectedIndexChanged);
            // 
            // bConnetti
            // 
            this.bConnetti.Enabled = false;
            this.bConnetti.Location = new System.Drawing.Point(398, 50);
            this.bConnetti.Name = "bConnetti";
            this.bConnetti.Size = new System.Drawing.Size(108, 23);
            this.bConnetti.TabIndex = 1;
            this.bConnetti.Text = "Connetti";
            this.bConnetti.UseVisualStyleBackColor = true;
            this.bConnetti.Click += new System.EventHandler(this.bConnetti_Click);
            // 
            // tBInfo
            // 
            this.tBInfo.BackColor = System.Drawing.SystemColors.Info;
            this.tBInfo.Location = new System.Drawing.Point(12, 12);
            this.tBInfo.Multiline = true;
            this.tBInfo.Name = "tBInfo";
            this.tBInfo.ReadOnly = true;
            this.tBInfo.Size = new System.Drawing.Size(367, 228);
            this.tBInfo.TabIndex = 2;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 255);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(533, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(142, 17);
            this.toolStripStatusLabel1.Text = "Dispositivo non connesso";
            // 
            // bDisconnetti
            // 
            this.bDisconnetti.Enabled = false;
            this.bDisconnetti.Location = new System.Drawing.Point(398, 79);
            this.bDisconnetti.Name = "bDisconnetti";
            this.bDisconnetti.Size = new System.Drawing.Size(108, 23);
            this.bDisconnetti.TabIndex = 1;
            this.bDisconnetti.Text = "Disconnetti";
            this.bDisconnetti.UseVisualStyleBackColor = true;
            this.bDisconnetti.Click += new System.EventHandler(this.bDisconnetti_Click);
            // 
            // bReadAll
            // 
            this.bReadAll.Enabled = false;
            this.bReadAll.Location = new System.Drawing.Point(398, 144);
            this.bReadAll.Name = "bReadAll";
            this.bReadAll.Size = new System.Drawing.Size(108, 23);
            this.bReadAll.TabIndex = 1;
            this.bReadAll.Text = "Leggi intera ROM";
            this.bReadAll.UseVisualStyleBackColor = true;
            this.bReadAll.Click += new System.EventHandler(this.bReadAll_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "File bin|*.bin|Tutti i file|*.*";
            this.saveFileDialog1.Title = "Salva ROM";
            // 
            // bWriteAll
            // 
            this.bWriteAll.Enabled = false;
            this.bWriteAll.Location = new System.Drawing.Point(398, 173);
            this.bWriteAll.Name = "bWriteAll";
            this.bWriteAll.Size = new System.Drawing.Size(108, 23);
            this.bWriteAll.TabIndex = 1;
            this.bWriteAll.Text = "Scrivi intera ROM";
            this.bWriteAll.UseVisualStyleBackColor = true;
            this.bWriteAll.Click += new System.EventHandler(this.bWriteAll_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "File bin|*.bin|Tutti i file|*.*";
            this.openFileDialog1.Title = "Scrivi ROM";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 277);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tBInfo);
            this.Controls.Add(this.bWriteAll);
            this.Controls.Add(this.bReadAll);
            this.Controls.Add(this.bDisconnetti);
            this.Controls.Add(this.bConnetti);
            this.Controls.Add(this.cBSerialPorts);
            this.Name = "Form1";
            this.Text = "Form1";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cBSerialPorts;
        private System.Windows.Forms.Button bConnetti;
        private System.Windows.Forms.TextBox tBInfo;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button bDisconnetti;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button bReadAll;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button bWriteAll;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

