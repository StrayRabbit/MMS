namespace StrayRabbit.MMS.WindowsForm.FormUI.Other
{
    partial class UpdatePwd
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
            this.txt_NewPwd = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btn_cancle = new DevExpress.XtraEditors.SimpleButton();
            this.btn_save = new DevExpress.XtraEditors.SimpleButton();
            this.txt_OldPwd = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txt_NewPwd2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txt_NewPwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_OldPwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_NewPwd2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_NewPwd
            // 
            this.txt_NewPwd.Location = new System.Drawing.Point(125, 89);
            this.txt_NewPwd.Name = "txt_NewPwd";
            this.txt_NewPwd.Properties.PasswordChar = '*';
            this.txt_NewPwd.Size = new System.Drawing.Size(100, 20);
            this.txt_NewPwd.TabIndex = 10;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(71, 92);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 14;
            this.labelControl3.Text = "新密码：";
            // 
            // btn_cancle
            // 
            this.btn_cancle.Location = new System.Drawing.Point(162, 186);
            this.btn_cancle.Name = "btn_cancle";
            this.btn_cancle.Size = new System.Drawing.Size(75, 23);
            this.btn_cancle.TabIndex = 13;
            this.btn_cancle.Text = "取 消";
            this.btn_cancle.Click += new System.EventHandler(this.btn_cancle_Click);
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(59, 186);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 12;
            this.btn_save.Text = "保 存";
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // txt_OldPwd
            // 
            this.txt_OldPwd.Location = new System.Drawing.Point(125, 45);
            this.txt_OldPwd.Name = "txt_OldPwd";
            this.txt_OldPwd.Properties.PasswordChar = '*';
            this.txt_OldPwd.Size = new System.Drawing.Size(100, 20);
            this.txt_OldPwd.TabIndex = 9;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(59, 48);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "原始密码：";
            // 
            // txt_NewPwd2
            // 
            this.txt_NewPwd2.Location = new System.Drawing.Point(125, 129);
            this.txt_NewPwd2.Name = "txt_NewPwd2";
            this.txt_NewPwd2.Properties.PasswordChar = '*';
            this.txt_NewPwd2.Size = new System.Drawing.Size(100, 20);
            this.txt_NewPwd2.TabIndex = 11;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(47, 132);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(72, 14);
            this.labelControl2.TabIndex = 16;
            this.labelControl2.Text = "确认新密码：";
            // 
            // UpdatePwd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.txt_NewPwd2);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txt_NewPwd);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.btn_cancle);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.txt_OldPwd);
            this.Controls.Add(this.labelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdatePwd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改密码";
            ((System.ComponentModel.ISupportInitialize)(this.txt_NewPwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_OldPwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_NewPwd2.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txt_NewPwd;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btn_cancle;
        private DevExpress.XtraEditors.SimpleButton btn_save;
        private DevExpress.XtraEditors.TextEdit txt_OldPwd;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txt_NewPwd2;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}