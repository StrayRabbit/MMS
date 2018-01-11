namespace StrayRabbit.MMS.WindowsForm.FormUI.BasicInfo
{
    partial class BasicDetail
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txt_name = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lbl_ParentName = new DevExpress.XtraEditors.LabelControl();
            this.btn_save = new DevExpress.XtraEditors.SimpleButton();
            this.btn_cancle = new DevExpress.XtraEditors.SimpleButton();
            this.txt_character = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txt_name.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_character.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(50, 77);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "字典名称：";
            // 
            // txt_name
            // 
            this.txt_name.Location = new System.Drawing.Point(116, 74);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(100, 20);
            this.txt_name.TabIndex = 1;
            this.txt_name.EditValueChanged += new System.EventHandler(this.txt_name_EditValueChanged);
            this.txt_name.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_name_KeyPress);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(50, 42);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "上级名称：";
            // 
            // lbl_ParentName
            // 
            this.lbl_ParentName.Location = new System.Drawing.Point(116, 42);
            this.lbl_ParentName.Name = "lbl_ParentName";
            this.lbl_ParentName.Size = new System.Drawing.Size(11, 14);
            this.lbl_ParentName.TabIndex = 3;
            this.lbl_ParentName.Text = "lbl";
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(51, 137);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 4;
            this.btn_save.Text = "保 存";
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_cancle
            // 
            this.btn_cancle.Location = new System.Drawing.Point(153, 137);
            this.btn_cancle.Name = "btn_cancle";
            this.btn_cancle.Size = new System.Drawing.Size(75, 23);
            this.btn_cancle.TabIndex = 5;
            this.btn_cancle.Text = "取 消";
            this.btn_cancle.Click += new System.EventHandler(this.btn_cancle_Click);
            // 
            // txt_character
            // 
            this.txt_character.Location = new System.Drawing.Point(116, 100);
            this.txt_character.Name = "txt_character";
            this.txt_character.Size = new System.Drawing.Size(100, 20);
            this.txt_character.TabIndex = 7;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(66, 103);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(44, 14);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "简  码：";
            // 
            // BasicDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 182);
            this.Controls.Add(this.txt_character);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.btn_cancle);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.lbl_ParentName);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txt_name);
            this.Controls.Add(this.labelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BasicDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "详细信息";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BasicDetail_FormClosed);
            this.Load += new System.EventHandler(this.BasicDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txt_name.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_character.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txt_name;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl lbl_ParentName;
        private DevExpress.XtraEditors.SimpleButton btn_save;
        private DevExpress.XtraEditors.SimpleButton btn_cancle;
        private DevExpress.XtraEditors.TextEdit txt_character;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}