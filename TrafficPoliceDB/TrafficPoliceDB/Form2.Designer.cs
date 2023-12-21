namespace TrafficPoliceDB
{
    partial class Form2
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
            this.label1 = new System.Windows.Forms.Label();
            this.login_box = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.password_box = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.last_name_box = new System.Windows.Forms.TextBox();
            this.first_name_box = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.patr_box = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.registr_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(220, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Логин";
            // 
            // login_box
            // 
            this.login_box.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.login_box.Location = new System.Drawing.Point(165, 37);
            this.login_box.Name = "login_box";
            this.login_box.Size = new System.Drawing.Size(175, 30);
            this.login_box.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(212, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Пароль";
            // 
            // password_box
            // 
            this.password_box.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.password_box.Location = new System.Drawing.Point(165, 106);
            this.password_box.Name = "password_box";
            this.password_box.PasswordChar = '*';
            this.password_box.Size = new System.Drawing.Size(175, 30);
            this.password_box.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.Location = new System.Drawing.Point(63, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Фамилия";
            // 
            // last_name_box
            // 
            this.last_name_box.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.last_name_box.Location = new System.Drawing.Point(30, 184);
            this.last_name_box.Name = "last_name_box";
            this.last_name_box.Size = new System.Drawing.Size(175, 30);
            this.last_name_box.TabIndex = 5;
            // 
            // first_name_box
            // 
            this.first_name_box.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.first_name_box.Location = new System.Drawing.Point(308, 184);
            this.first_name_box.Name = "first_name_box";
            this.first_name_box.Size = new System.Drawing.Size(175, 30);
            this.first_name_box.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label4.Location = new System.Drawing.Point(370, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 25);
            this.label4.TabIndex = 7;
            this.label4.Text = "Имя";
            // 
            // patr_box
            // 
            this.patr_box.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.patr_box.Location = new System.Drawing.Point(165, 249);
            this.patr_box.Name = "patr_box";
            this.patr_box.Size = new System.Drawing.Size(175, 30);
            this.patr_box.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label5.Location = new System.Drawing.Point(201, 221);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 25);
            this.label5.TabIndex = 9;
            this.label5.Text = "Отчество";
            // 
            // registr_button
            // 
            this.registr_button.Location = new System.Drawing.Point(145, 311);
            this.registr_button.Name = "registr_button";
            this.registr_button.Size = new System.Drawing.Size(210, 83);
            this.registr_button.TabIndex = 10;
            this.registr_button.Text = "Зарегистрироваться";
            this.registr_button.UseVisualStyleBackColor = true;
            this.registr_button.Click += new System.EventHandler(this.registr_button_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(515, 450);
            this.Controls.Add(this.registr_button);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.patr_box);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.first_name_box);
            this.Controls.Add(this.last_name_box);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.password_box);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.login_box);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(533, 497);
            this.MinimumSize = new System.Drawing.Size(533, 497);
            this.Name = "Form2";
            this.Text = "Регистрация";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox login_box;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox password_box;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox last_name_box;
        private System.Windows.Forms.TextBox first_name_box;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox patr_box;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button registr_button;
    }
}