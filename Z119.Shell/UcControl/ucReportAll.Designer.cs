namespace Z119.Shell.UcControl
{
    partial class ucReportAll
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucReportAll));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.nvgPage = new DevComponents.DotNetBar.Controls.BindingNavigatorEx(this.components);
            this.bindingNavigatorAddNewItem = new DevComponents.DotNetBar.ButtonItem();
            this.lblNVGTotalPage = new DevComponents.DotNetBar.LabelItem();
            this.bindingNavigatorDeleteItem = new DevComponents.DotNetBar.ButtonItem();
            this.btnNVGFirst = new DevComponents.DotNetBar.ButtonItem();
            this.btnNGVPrevious = new DevComponents.DotNetBar.ButtonItem();
            this.txtNVGCurrentPage = new DevComponents.DotNetBar.TextBoxItem();
            this.btnNVGNext = new DevComponents.DotNetBar.ButtonItem();
            this.btnNVGLast = new DevComponents.DotNetBar.ButtonItem();
            this.dgvData = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BookCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PageCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1.SuspendLayout();
            this.panelEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nvgPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.panelEx3);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1030, 610);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 3;
            this.panelEx1.Text = "panelEx1";
            // 
            // panelEx3
            // 
            this.panelEx3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.nvgPage);
            this.panelEx3.Controls.Add(this.dgvData);
            this.panelEx3.Location = new System.Drawing.Point(3, 3);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(1024, 604);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 1;
            this.panelEx3.Text = "panelEx3";
            // 
            // nvgPage
            // 
            this.nvgPage.AddNewRecordButton = this.bindingNavigatorAddNewItem;
            this.nvgPage.AntiAlias = true;
            this.nvgPage.CountLabel = this.lblNVGTotalPage;
            this.nvgPage.CountLabelFormat = "of {0}";
            this.nvgPage.DeleteButton = this.bindingNavigatorDeleteItem;
            this.nvgPage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.nvgPage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nvgPage.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnNVGFirst,
            this.btnNGVPrevious,
            this.txtNVGCurrentPage,
            this.lblNVGTotalPage,
            this.btnNVGNext,
            this.btnNVGLast,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem});
            this.nvgPage.Location = new System.Drawing.Point(0, 579);
            this.nvgPage.MoveFirstButton = this.btnNVGFirst;
            this.nvgPage.MoveLastButton = this.btnNVGLast;
            this.nvgPage.MoveNextButton = this.btnNVGNext;
            this.nvgPage.MovePreviousButton = this.btnNGVPrevious;
            this.nvgPage.Name = "nvgPage";
            this.nvgPage.PositionTextBox = this.txtNVGCurrentPage;
            this.nvgPage.Size = new System.Drawing.Size(1024, 25);
            this.nvgPage.Stretch = true;
            this.nvgPage.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.nvgPage.TabIndex = 1;
            this.nvgPage.TabStop = false;
            this.nvgPage.Text = "bindingNavigatorEx1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.Text = "Add new";
            this.bindingNavigatorAddNewItem.Visible = false;
            // 
            // lblNVGTotalPage
            // 
            this.lblNVGTotalPage.Name = "lblNVGTotalPage";
            this.lblNVGTotalPage.Text = "of {0}";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.Text = "Delete";
            this.bindingNavigatorDeleteItem.Visible = false;
            // 
            // btnNVGFirst
            // 
            this.btnNVGFirst.Image = ((System.Drawing.Image)(resources.GetObject("btnNVGFirst.Image")));
            this.btnNVGFirst.Name = "btnNVGFirst";
            this.btnNVGFirst.Text = "Move first";
            // 
            // btnNGVPrevious
            // 
            this.btnNGVPrevious.Image = ((System.Drawing.Image)(resources.GetObject("btnNGVPrevious.Image")));
            this.btnNGVPrevious.Name = "btnNGVPrevious";
            this.btnNGVPrevious.Text = "Move previous";
            // 
            // txtNVGCurrentPage
            // 
            this.txtNVGCurrentPage.AccessibleName = "Position";
            this.txtNVGCurrentPage.BeginGroup = true;
            this.txtNVGCurrentPage.Name = "txtNVGCurrentPage";
            this.txtNVGCurrentPage.Text = "0";
            this.txtNVGCurrentPage.TextBoxWidth = 54;
            this.txtNVGCurrentPage.WatermarkColor = System.Drawing.SystemColors.GrayText;
            // 
            // btnNVGNext
            // 
            this.btnNVGNext.BeginGroup = true;
            this.btnNVGNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNVGNext.Image")));
            this.btnNVGNext.Name = "btnNVGNext";
            this.btnNVGNext.Text = "Move next";
            // 
            // btnNVGLast
            // 
            this.btnNVGLast.Image = ((System.Drawing.Image)(resources.GetObject("btnNVGLast.Image")));
            this.btnNVGLast.Name = "btnNVGLast";
            this.btnNVGLast.Text = "Move last";
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Category,
            this.BookCount,
            this.PageCount});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.EnableHeadersVisualStyles = false;
            this.dgvData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.dgvData.Location = new System.Drawing.Point(0, 0);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvData.RowTemplate.Height = 24;
            this.dgvData.Size = new System.Drawing.Size(1024, 604);
            this.dgvData.TabIndex = 0;
            // 
            // Category
            // 
            this.Category.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Category.DataPropertyName = "Category";
            this.Category.HeaderText = "Danh mục";
            this.Category.Name = "Category";
            this.Category.ReadOnly = true;
            // 
            // BookCount
            // 
            this.BookCount.DataPropertyName = "BookCount";
            this.BookCount.HeaderText = "Số lượng sách";
            this.BookCount.Name = "BookCount";
            this.BookCount.ReadOnly = true;
            this.BookCount.Width = 400;
            // 
            // PageCount
            // 
            this.PageCount.DataPropertyName = "PageCount";
            this.PageCount.HeaderText = "Số lượng trang";
            this.PageCount.Name = "PageCount";
            this.PageCount.ReadOnly = true;
            this.PageCount.Width = 400;
            // 
            // ucReportAll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelEx1);
            this.Name = "ucReportAll";
            this.Size = new System.Drawing.Size(1030, 610);
            this.panelEx1.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nvgPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.Controls.BindingNavigatorEx nvgPage;
        private DevComponents.DotNetBar.ButtonItem bindingNavigatorAddNewItem;
        private DevComponents.DotNetBar.LabelItem lblNVGTotalPage;
        private DevComponents.DotNetBar.ButtonItem bindingNavigatorDeleteItem;
        private DevComponents.DotNetBar.ButtonItem btnNVGFirst;
        private DevComponents.DotNetBar.ButtonItem btnNGVPrevious;
        private DevComponents.DotNetBar.TextBoxItem txtNVGCurrentPage;
        private DevComponents.DotNetBar.ButtonItem btnNVGNext;
        private DevComponents.DotNetBar.ButtonItem btnNVGLast;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvData;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category;
        private System.Windows.Forms.DataGridViewTextBoxColumn BookCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn PageCount;
    }
}
