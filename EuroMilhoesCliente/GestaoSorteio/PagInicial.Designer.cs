
namespace GestaoSorteio
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
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSubmeter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(213, 60);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(100, 23);
            this.textBoxIP.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Insira o endereço do servidor: ";
            // 
            // buttonSubmeter
            // 
            this.buttonSubmeter.Location = new System.Drawing.Point(126, 118);
            this.buttonSubmeter.Name = "buttonSubmeter";
            this.buttonSubmeter.Size = new System.Drawing.Size(109, 23);
            this.buttonSubmeter.TabIndex = 8;
            this.buttonSubmeter.Text = "Ligar ao Servidor";
            this.buttonSubmeter.UseVisualStyleBackColor = true;
            this.buttonSubmeter.Click += new System.EventHandler(this.buttonSubmeter_Click);
            // 
            // PagInicial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 172);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSubmeter);
            this.Name = "PagInicial";
            this.Text = "Gestão de Sorteio";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSubmeter;
    }
}