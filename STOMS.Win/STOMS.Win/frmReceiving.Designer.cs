namespace STOMS.Win
{
    partial class frmReceiving
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
            this.gbxLeft = new System.Windows.Forms.GroupBox();
            this.dgvSpecimenNo = new System.Windows.Forms.DataGridView();
            this.txtNoOfValues = new System.Windows.Forms.TextBox();
            this.btnGenerator = new System.Windows.Forms.Button();
            this.gbxRight = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.gbxLogin = new System.Windows.Forms.GroupBox();
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ltrMsg = new System.Windows.Forms.Label();
            this.optPaper = new System.Windows.Forms.RadioButton();
            this.optForm = new System.Windows.Forms.RadioButton();
            this.optTT = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.gbxLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpecimenNo)).BeginInit();
            this.gbxRight.SuspendLayout();
            this.gbxLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxLeft
            // 
            this.gbxLeft.Controls.Add(this.dgvSpecimenNo);
            this.gbxLeft.Controls.Add(this.txtNoOfValues);
            this.gbxLeft.Controls.Add(this.btnGenerator);
            this.gbxLeft.Location = new System.Drawing.Point(12, 12);
            this.gbxLeft.Name = "gbxLeft";
            this.gbxLeft.Size = new System.Drawing.Size(298, 496);
            this.gbxLeft.TabIndex = 0;
            this.gbxLeft.TabStop = false;
            this.gbxLeft.Visible = false;
            // 
            // dgvSpecimenNo
            // 
            this.dgvSpecimenNo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSpecimenNo.Location = new System.Drawing.Point(21, 86);
            this.dgvSpecimenNo.Name = "dgvSpecimenNo";
            this.dgvSpecimenNo.Size = new System.Drawing.Size(253, 387);
            this.dgvSpecimenNo.TabIndex = 2;
            // 
            // txtNoOfValues
            // 
            this.txtNoOfValues.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoOfValues.Location = new System.Drawing.Point(21, 32);
            this.txtNoOfValues.Name = "txtNoOfValues";
            this.txtNoOfValues.Size = new System.Drawing.Size(54, 35);
            this.txtNoOfValues.TabIndex = 1;
            this.txtNoOfValues.Text = "1";
            this.txtNoOfValues.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnGenerator
            // 
            this.btnGenerator.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerator.Location = new System.Drawing.Point(92, 32);
            this.btnGenerator.Name = "btnGenerator";
            this.btnGenerator.Size = new System.Drawing.Size(182, 37);
            this.btnGenerator.TabIndex = 0;
            this.btnGenerator.Text = "Generator # (s)";
            this.btnGenerator.UseVisualStyleBackColor = true;
            this.btnGenerator.Click += new System.EventHandler(this.btnGenerator_Click);
            // 
            // gbxRight
            // 
            this.gbxRight.Controls.Add(this.optTT);
            this.gbxRight.Controls.Add(this.optForm);
            this.gbxRight.Controls.Add(this.optPaper);
            this.gbxRight.Controls.Add(this.btnClose);
            this.gbxRight.Controls.Add(this.btnPrint);
            this.gbxRight.Location = new System.Drawing.Point(325, 12);
            this.gbxRight.Name = "gbxRight";
            this.gbxRight.Size = new System.Drawing.Size(298, 496);
            this.gbxRight.TabIndex = 1;
            this.gbxRight.TabStop = false;
            this.gbxRight.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(58, 436);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(182, 37);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(58, 256);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(182, 37);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // gbxLogin
            // 
            this.gbxLogin.Controls.Add(this.label4);
            this.gbxLogin.Controls.Add(this.ltrMsg);
            this.gbxLogin.Controls.Add(this.btnEnd);
            this.gbxLogin.Controls.Add(this.btnLogin);
            this.gbxLogin.Controls.Add(this.txtPassword);
            this.gbxLogin.Controls.Add(this.txtUserName);
            this.gbxLogin.Controls.Add(this.label3);
            this.gbxLogin.Controls.Add(this.label2);
            this.gbxLogin.Controls.Add(this.label1);
            this.gbxLogin.Location = new System.Drawing.Point(12, 12);
            this.gbxLogin.Name = "gbxLogin";
            this.gbxLogin.Size = new System.Drawing.Size(614, 496);
            this.gbxLogin.TabIndex = 2;
            this.gbxLogin.TabStop = false;
            // 
            // btnEnd
            // 
            this.btnEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnd.Location = new System.Drawing.Point(361, 336);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(95, 33);
            this.btnEnd.TabIndex = 6;
            this.btnEnd.Text = "Close";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.Location = new System.Drawing.Point(248, 336);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(95, 33);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "Submit";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(248, 280);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(208, 26);
            this.txtPassword.TabIndex = 4;
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.Location = new System.Drawing.Point(248, 224);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(208, 26);
            this.txtUserName.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(156, 285);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(156, 229);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Login ID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(266, 175);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sign-in";
            // 
            // ltrMsg
            // 
            this.ltrMsg.AutoSize = true;
            this.ltrMsg.ForeColor = System.Drawing.Color.Red;
            this.ltrMsg.Location = new System.Drawing.Point(304, 66);
            this.ltrMsg.Name = "ltrMsg";
            this.ltrMsg.Size = new System.Drawing.Size(0, 13);
            this.ltrMsg.TabIndex = 7;
            this.ltrMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // optPaper
            // 
            this.optPaper.AutoSize = true;
            this.optPaper.Checked = true;
            this.optPaper.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optPaper.Location = new System.Drawing.Point(73, 43);
            this.optPaper.Name = "optPaper";
            this.optPaper.Size = new System.Drawing.Size(108, 20);
            this.optPaper.TabIndex = 2;
            this.optPaper.TabStop = true;
            this.optPaper.Text = "Label (Paper)";
            this.optPaper.UseVisualStyleBackColor = true;
            // 
            // optForm
            // 
            this.optForm.AutoSize = true;
            this.optForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optForm.Location = new System.Drawing.Point(73, 152);
            this.optForm.Name = "optForm";
            this.optForm.Size = new System.Drawing.Size(102, 20);
            this.optForm.TabIndex = 3;
            this.optForm.TabStop = true;
            this.optForm.Text = "Label (Form)";
            this.optForm.UseVisualStyleBackColor = true;
            // 
            // optTT
            // 
            this.optTT.AutoSize = true;
            this.optTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optTT.Location = new System.Drawing.Point(73, 94);
            this.optTT.Name = "optTT";
            this.optTT.Size = new System.Drawing.Size(127, 20);
            this.optTT.TabIndex = 4;
            this.optTT.TabStop = true;
            this.optTT.Text = "Label (Test tube)";
            this.optTT.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Chocolate;
            this.label4.Location = new System.Drawing.Point(120, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(421, 24);
            this.label4.TabIndex = 8;
            this.label4.Text = "STORMS - Specimen Unique Number Generator";
            // 
            // frmReceiving
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 524);
            this.Controls.Add(this.gbxLogin);
            this.Controls.Add(this.gbxRight);
            this.Controls.Add(this.gbxLeft);
            this.Name = "frmReceiving";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "STORMS - Specimen Unique Number Generator";
            this.gbxLeft.ResumeLayout(false);
            this.gbxLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpecimenNo)).EndInit();
            this.gbxRight.ResumeLayout(false);
            this.gbxRight.PerformLayout();
            this.gbxLogin.ResumeLayout(false);
            this.gbxLogin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxLeft;
        private System.Windows.Forms.TextBox txtNoOfValues;
        private System.Windows.Forms.Button btnGenerator;
        private System.Windows.Forms.DataGridView dgvSpecimenNo;
        private System.Windows.Forms.GroupBox gbxRight;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.GroupBox gbxLogin;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ltrMsg;
        private System.Windows.Forms.RadioButton optTT;
        private System.Windows.Forms.RadioButton optForm;
        private System.Windows.Forms.RadioButton optPaper;
        private System.Windows.Forms.Label label4;
    }
}

