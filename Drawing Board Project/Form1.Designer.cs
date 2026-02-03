namespace projekt3
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_KokpitProjektuNr3 = new System.Windows.Forms.Label();
            this.bttn_LabaratoriumNr3 = new System.Windows.Forms.Button();
            this.bttn_ProjektNr3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_KokpitProjektuNr3
            // 
            this.lbl_KokpitProjektuNr3.AutoSize = true;
            this.lbl_KokpitProjektuNr3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl_KokpitProjektuNr3.Location = new System.Drawing.Point(31, 50);
            this.lbl_KokpitProjektuNr3.Name = "lbl_KokpitProjektuNr3";
            this.lbl_KokpitProjektuNr3.Size = new System.Drawing.Size(737, 25);
            this.lbl_KokpitProjektuNr3.TabIndex = 0;
            this.lbl_KokpitProjektuNr3.Text = "KOKPIT PROJEKTU N№3 (kreślenie figur, krzywych i linii geometrycznych)";
            // 
            // bttn_LabaratoriumNr3
            // 
            this.bttn_LabaratoriumNr3.Location = new System.Drawing.Point(82, 260);
            this.bttn_LabaratoriumNr3.Name = "bttn_LabaratoriumNr3";
            this.bttn_LabaratoriumNr3.Size = new System.Drawing.Size(272, 56);
            this.bttn_LabaratoriumNr3.TabIndex = 1;
            this.bttn_LabaratoriumNr3.Text = "Labaratorium Nr 3 (kreślenie krzywych geometrycznych)";
            this.bttn_LabaratoriumNr3.UseVisualStyleBackColor = true;
            this.bttn_LabaratoriumNr3.Click += new System.EventHandler(this.bttn_LabaratoriumNr3_Click);
            // 
            // bttn_ProjektNr3
            // 
            this.bttn_ProjektNr3.Location = new System.Drawing.Point(436, 260);
            this.bttn_ProjektNr3.Name = "bttn_ProjektNr3";
            this.bttn_ProjektNr3.Size = new System.Drawing.Size(299, 56);
            this.bttn_ProjektNr3.TabIndex = 2;
            this.bttn_ProjektNr3.Text = "Projekt Nr 3 (kreślenie figur i linii geometrycznych)";
            this.bttn_ProjektNr3.UseVisualStyleBackColor = true;
            this.bttn_ProjektNr3.Click += new System.EventHandler(this.bttn_ProjektNr3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.bttn_ProjektNr3);
            this.Controls.Add(this.bttn_LabaratoriumNr3);
            this.Controls.Add(this.lbl_KokpitProjektuNr3);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_KokpitProjektuNr3;
        private System.Windows.Forms.Button bttn_LabaratoriumNr3;
        private System.Windows.Forms.Button bttn_ProjektNr3;
    }
}

