namespace WindowsFormsApp1
{
    partial class Form5
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
            this.TimeTxt = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TimeTxt
            // 
            this.TimeTxt.AutoSize = true;
            this.TimeTxt.Font = new System.Drawing.Font("Yu Gothic", 18F, System.Drawing.FontStyle.Bold);
            this.TimeTxt.Location = new System.Drawing.Point(373, 792);
            this.TimeTxt.Name = "TimeTxt";
            this.TimeTxt.Size = new System.Drawing.Size(34, 39);
            this.TimeTxt.TabIndex = 0;
            this.TimeTxt.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Yu Gothic", 18F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(237, 792);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 39);
            this.label2.TabIndex = 1;
            this.label2.Text = "ZAMAN";
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Yu Gothic", 18F, System.Drawing.FontStyle.Bold);
            this.button1.Image = global::WindowsFormsApp1.Properties.Resources.WhatsApp_Görsel_2024_11_08_saat_12_37_27_7df86689;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button1.Location = new System.Drawing.Point(460, 744);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 84);
            this.button1.TabIndex = 2;
            this.button1.Text = "MENÜ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic", 18F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(12, 724);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(238, 39);
            this.label1.TabIndex = 3;
            this.label1.Text = "HAMLE SAYISI";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Yu Gothic", 18F, System.Drawing.FontStyle.Bold);
            this.Label3.Location = new System.Drawing.Point(276, 724);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(34, 39);
            this.Label3.TabIndex = 4;
            this.Label3.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Yu Gothic", 18F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(12, 792);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 39);
            this.label4.TabIndex = 5;
            this.label4.Text = "PUAN";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Yu Gothic", 18F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(120, 792);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 39);
            this.label5.TabIndex = 6;
            this.label5.Text = "0";
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(612, 840);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TimeTxt);
            this.Name = "Form5";
            this.Text = "Form5";
            this.Load += new System.EventHandler(this.Form5_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TimeTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}