namespace Automatos
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
            this.DtPassos = new System.Windows.Forms.DataGridView();
            this.DtSolucao = new System.Windows.Forms.DataGridView();
            this.DtFinal = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DtPassos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtSolucao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtFinal)).BeginInit();
            this.SuspendLayout();
            // 
            // DtPassos
            // 
            this.DtPassos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DtPassos.Dock = System.Windows.Forms.DockStyle.Left;
            this.DtPassos.Location = new System.Drawing.Point(0, 0);
            this.DtPassos.Name = "DtPassos";
            this.DtPassos.Size = new System.Drawing.Size(348, 505);
            this.DtPassos.TabIndex = 0;
            // 
            // DtSolucao
            // 
            this.DtSolucao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DtSolucao.Dock = System.Windows.Forms.DockStyle.Top;
            this.DtSolucao.Location = new System.Drawing.Point(348, 0);
            this.DtSolucao.Name = "DtSolucao";
            this.DtSolucao.Size = new System.Drawing.Size(909, 239);
            this.DtSolucao.TabIndex = 1;
            // 
            // DtFinal
            // 
            this.DtFinal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DtFinal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DtFinal.Location = new System.Drawing.Point(348, 239);
            this.DtFinal.Name = "DtFinal";
            this.DtFinal.Size = new System.Drawing.Size(909, 266);
            this.DtFinal.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1257, 505);
            this.Controls.Add(this.DtFinal);
            this.Controls.Add(this.DtSolucao);
            this.Controls.Add(this.DtPassos);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.DtPassos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtSolucao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtFinal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DtPassos;
        private System.Windows.Forms.DataGridView DtSolucao;
        private System.Windows.Forms.DataGridView DtFinal;
    }
}

