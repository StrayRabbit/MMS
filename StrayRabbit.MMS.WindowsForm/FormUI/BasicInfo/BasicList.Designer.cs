namespace StrayRabbit.MMS.WindowsForm.FormUI.BasicInfo
{
    partial class BasicList
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
            this.components = new System.ComponentModel.Container();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.tl_dict = new DevExpress.XtraTreeList.TreeList();
            this.tlc_Name = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.cmbPageNum = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btn_Preview = new DevExpress.XtraEditors.SimpleButton();
            this.btn_MoveNext = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Last = new DevExpress.XtraEditors.SimpleButton();
            this.btn_First = new DevExpress.XtraEditors.SimpleButton();
            this.lbl_Sum = new DevExpress.XtraEditors.LabelControl();
            this.gd_list = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_search = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Add = new DevExpress.XtraEditors.SimpleButton();
            this.txt_search = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tl_dict)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPageNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gd_list)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_search.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Location = new System.Drawing.Point(2, 2);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.tl_dict);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.cmbPageNum);
            this.splitContainerControl1.Panel2.Controls.Add(this.btn_Preview);
            this.splitContainerControl1.Panel2.Controls.Add(this.btn_MoveNext);
            this.splitContainerControl1.Panel2.Controls.Add(this.btn_Last);
            this.splitContainerControl1.Panel2.Controls.Add(this.btn_First);
            this.splitContainerControl1.Panel2.Controls.Add(this.lbl_Sum);
            this.splitContainerControl1.Panel2.Controls.Add(this.gd_list);
            this.splitContainerControl1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(964, 480);
            this.splitContainerControl1.SplitterPosition = 214;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // tl_dict
            // 
            this.tl_dict.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.tlc_Name});
            this.tl_dict.Location = new System.Drawing.Point(0, 0);
            this.tl_dict.Name = "tl_dict";
            this.tl_dict.Size = new System.Drawing.Size(207, 480);
            this.tl_dict.TabIndex = 0;
            this.tl_dict.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.tl_dict_FocusedNodeChanged);
            // 
            // tlc_Name
            // 
            this.tlc_Name.Caption = "名称";
            this.tlc_Name.FieldName = "MenuName";
            this.tlc_Name.Name = "tlc_Name";
            this.tlc_Name.OptionsColumn.AllowEdit = false;
            this.tlc_Name.Visible = true;
            this.tlc_Name.VisibleIndex = 0;
            // 
            // cmbPageNum
            // 
            this.cmbPageNum.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cmbPageNum.Location = new System.Drawing.Point(560, 452);
            this.cmbPageNum.Name = "cmbPageNum";
            this.cmbPageNum.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPageNum.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbPageNum.Size = new System.Drawing.Size(59, 20);
            this.cmbPageNum.TabIndex = 27;
            this.cmbPageNum.SelectedIndexChanged += new System.EventHandler(this.cmbPageNum_SelectedIndexChanged);
            // 
            // btn_Preview
            // 
            this.btn_Preview.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_Preview.Location = new System.Drawing.Point(516, 451);
            this.btn_Preview.Name = "btn_Preview";
            this.btn_Preview.Size = new System.Drawing.Size(38, 23);
            this.btn_Preview.TabIndex = 26;
            this.btn_Preview.Text = "<";
            this.btn_Preview.Click += new System.EventHandler(this.btn_Preview_Click);
            // 
            // btn_MoveNext
            // 
            this.btn_MoveNext.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_MoveNext.Location = new System.Drawing.Point(625, 451);
            this.btn_MoveNext.Name = "btn_MoveNext";
            this.btn_MoveNext.Size = new System.Drawing.Size(38, 23);
            this.btn_MoveNext.TabIndex = 25;
            this.btn_MoveNext.Text = ">";
            this.btn_MoveNext.Click += new System.EventHandler(this.btn_MoveNext_Click);
            // 
            // btn_Last
            // 
            this.btn_Last.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_Last.Location = new System.Drawing.Point(667, 451);
            this.btn_Last.Name = "btn_Last";
            this.btn_Last.Size = new System.Drawing.Size(38, 23);
            this.btn_Last.TabIndex = 24;
            this.btn_Last.Text = ">|";
            this.btn_Last.Click += new System.EventHandler(this.btn_Last_Click);
            // 
            // btn_First
            // 
            this.btn_First.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_First.Location = new System.Drawing.Point(472, 451);
            this.btn_First.Name = "btn_First";
            this.btn_First.Size = new System.Drawing.Size(38, 23);
            this.btn_First.TabIndex = 23;
            this.btn_First.Text = "|<";
            this.btn_First.Click += new System.EventHandler(this.btn_First_Click);
            // 
            // lbl_Sum
            // 
            this.lbl_Sum.Location = new System.Drawing.Point(15, 455);
            this.lbl_Sum.Name = "lbl_Sum";
            this.lbl_Sum.Size = new System.Drawing.Size(36, 14);
            this.lbl_Sum.TabIndex = 22;
            this.lbl_Sum.Text = "总数：";
            // 
            // gd_list
            // 
            this.gd_list.ContextMenuStrip = this.contextMenuStrip1;
            this.gd_list.Location = new System.Drawing.Point(2, 65);
            this.gd_list.MainView = this.gridView1;
            this.gd_list.Name = "gd_list";
            this.gd_list.Size = new System.Drawing.Size(740, 412);
            this.gd_list.TabIndex = 15;
            this.gd_list.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.编辑ToolStripMenuItem,
            this.删除ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 48);
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.编辑ToolStripMenuItem.Text = "编辑";
            this.编辑ToolStripMenuItem.Click += new System.EventHandler(this.编辑ToolStripMenuItem_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn1,
            this.gridColumn2});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            this.gridView1.GridControl = this.gd_list;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "ID";
            this.gridColumn3.FieldName = "Id";
            this.gridColumn3.Name = "gridColumn3";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "名称";
            this.gridColumn1.FieldName = "Name";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "简码";
            this.gridColumn2.FieldName = "Character";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_search);
            this.groupBox1.Controls.Add(this.btn_Add);
            this.groupBox1.Controls.Add(this.txt_search);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Location = new System.Drawing.Point(2, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(740, 50);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // btn_search
            // 
            this.btn_search.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_search.Location = new System.Drawing.Point(231, 20);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 23);
            this.btn_search.TabIndex = 19;
            this.btn_search.Text = "查  询";
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Add.Location = new System.Drawing.Point(650, 19);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 18;
            this.btn_Add.Text = "新  增";
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // txt_search
            // 
            this.txt_search.Location = new System.Drawing.Point(88, 21);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(137, 20);
            this.txt_search.TabIndex = 14;
            this.txt_search.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_search_KeyPress);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(65, 14);
            this.labelControl1.TabIndex = 13;
            this.labelControl1.Text = "名称/简码：";
            // 
            // BasicInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 504);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "BasicInfo";
            this.Text = "基础信息";
            this.Load += new System.EventHandler(this.BasicInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tl_dict)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPageNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gd_list)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_search.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraTreeList.TreeList tl_dict;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tlc_Name;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SimpleButton btn_Add;
        private DevExpress.XtraEditors.TextEdit txt_search;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.GridControl gd_list;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.ComboBoxEdit cmbPageNum;
        private DevExpress.XtraEditors.SimpleButton btn_Preview;
        private DevExpress.XtraEditors.SimpleButton btn_MoveNext;
        private DevExpress.XtraEditors.SimpleButton btn_Last;
        private DevExpress.XtraEditors.SimpleButton btn_First;
        private DevExpress.XtraEditors.LabelControl lbl_Sum;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.SimpleButton btn_search;
    }
}