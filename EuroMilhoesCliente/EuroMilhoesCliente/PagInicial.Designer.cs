namespace EuroMilhoesCliente
{
    partial class PagInicial
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
            this.labelNIF = new System.Windows.Forms.Label();
            this.textBoxNIF = new System.Windows.Forms.TextBox();
            this.buttonSubmeter = new System.Windows.Forms.Button();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelNIF
            // 
            this.labelNIF.AutoSize = true;
            this.labelNIF.Location = new System.Drawing.Point(93, 44);
            this.labelNIF.Name = "labelNIF";
            this.labelNIF.Size = new System.Drawing.Size(93, 15);
            this.labelNIF.TabIndex = 0;
            this.labelNIF.Text = "Insira o seu NIF: ";
            this.labelNIF.Click += new System.EventHandler(this.labelNIF_Click);
            // 
            // textBoxNIF
            // 
            this.textBoxNIF.Location = new System.Drawing.Point(192, 41);
            this.textBoxNIF.Name = "textBoxNIF";
            this.textBoxNIF.Size = new System.Drawing.Size(100, 23);
            this.textBoxNIF.TabIndex = 1;
            this.textBoxNIF.TextChanged += new System.EventHandler(this.textBoxNIF_TextChanged);
            // 
            // buttonSubmeter
            // 
            this.buttonSubmeter.Location = new System.Drawing.Point(130, 150);
            this.buttonSubmeter.Name = "buttonSubmeter";
            this.buttonSubmeter.Size = new System.Drawing.Size(109, 23);
            this.buttonSubmeter.TabIndex = 2;
            this.buttonSubmeter.Text = "Ligar ao Servidor";
            this.buttonSubmeter.UseVisualStyleBackColor = true;
            this.buttonSubmeter.Click += new System.EventHandler(this.buttonSubmeter_Click);
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(217, 92);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(100, 23);
            this.textBoxIP.TabIndex = 4;
            this.textBoxIP.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Insira o endereço do servidor: ";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // PagInicial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 207);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSubmeter);
            this.Controls.Add(this.textBoxNIF);
            this.Controls.Add(this.labelNIF);
            this.Name = "PagInicial";
            this.Text = "Página Inicial";
            this.Load += new System.EventHandler(this.PagInicial_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelNIF;
        private System.Windows.Forms.TextBox textBoxNIF;
        private System.Windows.Forms.Button buttonSubmeter;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.Label label1;
    }
}