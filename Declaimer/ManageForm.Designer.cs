namespace Declaimer
{
    partial class ManageForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.metroSplitContainerMain = new MetroFramework.Controls.MetroSplitContainer();
            this.metroBtnConnect = new MetroFramework.Controls.MetroButton();
            this.metroBtnStart = new MetroFramework.Controls.MetroButton();
            this.metroBtnStop = new MetroFramework.Controls.MetroButton();
            this.metroBtnDisConn = new MetroFramework.Controls.MetroButton();
            this.metroPTitle = new MetroFramework.Controls.MetroPanel();
            this.metroLabTotal = new MetroFramework.Controls.MetroLabel();
            this.metroLabTitle = new MetroFramework.Controls.MetroLabel();
            this.metroLabCountVal = new MetroFramework.Controls.MetroLabel();
            this.metroPanel2 = new MetroFramework.Controls.MetroPanel();
            this.metroLabStatusMsg = new MetroFramework.Controls.MetroLabel();
            this.panelStatusColor = new System.Windows.Forms.Panel();
            this.metroTagGrid = new MetroFramework.Controls.MetroGrid();
            ((System.ComponentModel.ISupportInitialize)(this.metroSplitContainerMain)).BeginInit();
            this.metroSplitContainerMain.Panel1.SuspendLayout();
            this.metroSplitContainerMain.Panel2.SuspendLayout();
            this.metroSplitContainerMain.SuspendLayout();
            this.metroPTitle.SuspendLayout();
            this.metroPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroTagGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // metroSplitContainerMain
            // 
            this.metroSplitContainerMain.CustomBackground = false;
            this.metroSplitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroSplitContainerMain.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.metroSplitContainerMain.Location = new System.Drawing.Point(20, 60);
            this.metroSplitContainerMain.Name = "metroSplitContainerMain";
            // 
            // metroSplitContainerMain.Panel1
            // 
            this.metroSplitContainerMain.Panel1.Controls.Add(this.metroBtnDisConn);
            this.metroSplitContainerMain.Panel1.Controls.Add(this.metroBtnStop);
            this.metroSplitContainerMain.Panel1.Controls.Add(this.metroBtnStart);
            this.metroSplitContainerMain.Panel1.Controls.Add(this.metroBtnConnect);
            // 
            // metroSplitContainerMain.Panel2
            // 
            this.metroSplitContainerMain.Panel2.Controls.Add(this.metroTagGrid);
            this.metroSplitContainerMain.Panel2.Controls.Add(this.metroPanel2);
            this.metroSplitContainerMain.Panel2.Controls.Add(this.metroPTitle);
            this.metroSplitContainerMain.Size = new System.Drawing.Size(792, 406);
            this.metroSplitContainerMain.SplitterDistance = 138;
            this.metroSplitContainerMain.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroSplitContainerMain.StyleManager = null;
            this.metroSplitContainerMain.TabIndex = 0;
            this.metroSplitContainerMain.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // metroBtnConnect
            // 
            this.metroBtnConnect.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroBtnConnect.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.metroBtnConnect.Highlight = true;
            this.metroBtnConnect.Location = new System.Drawing.Point(3, 3);
            this.metroBtnConnect.Name = "metroBtnConnect";
            this.metroBtnConnect.Size = new System.Drawing.Size(132, 35);
            this.metroBtnConnect.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroBtnConnect.StyleManager = null;
            this.metroBtnConnect.TabIndex = 1;
            this.metroBtnConnect.Text = "&Connect";
            this.metroBtnConnect.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroBtnConnect.Click += new System.EventHandler(this.metroBtnConnect_Click);
            // 
            // metroBtnStart
            // 
            this.metroBtnStart.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroBtnStart.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.metroBtnStart.Highlight = true;
            this.metroBtnStart.Location = new System.Drawing.Point(3, 44);
            this.metroBtnStart.Name = "metroBtnStart";
            this.metroBtnStart.Size = new System.Drawing.Size(132, 35);
            this.metroBtnStart.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroBtnStart.StyleManager = null;
            this.metroBtnStart.TabIndex = 2;
            this.metroBtnStart.Text = "&Start Read";
            this.metroBtnStart.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroBtnStart.Click += new System.EventHandler(this.metroBtnStart_Click);
            // 
            // metroBtnStop
            // 
            this.metroBtnStop.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroBtnStop.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.metroBtnStop.Highlight = true;
            this.metroBtnStop.Location = new System.Drawing.Point(3, 85);
            this.metroBtnStop.Name = "metroBtnStop";
            this.metroBtnStop.Size = new System.Drawing.Size(132, 35);
            this.metroBtnStop.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroBtnStop.StyleManager = null;
            this.metroBtnStop.TabIndex = 3;
            this.metroBtnStop.Text = "&Stop Read";
            this.metroBtnStop.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroBtnStop.Click += new System.EventHandler(this.metroBtnStop_Click);
            // 
            // metroBtnDisConn
            // 
            this.metroBtnDisConn.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroBtnDisConn.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.metroBtnDisConn.Highlight = true;
            this.metroBtnDisConn.Location = new System.Drawing.Point(3, 126);
            this.metroBtnDisConn.Name = "metroBtnDisConn";
            this.metroBtnDisConn.Size = new System.Drawing.Size(132, 35);
            this.metroBtnDisConn.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroBtnDisConn.StyleManager = null;
            this.metroBtnDisConn.TabIndex = 4;
            this.metroBtnDisConn.Text = "&Dis Connect";
            this.metroBtnDisConn.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroBtnDisConn.Click += new System.EventHandler(this.metroBtnDisConn_Click);
            // 
            // metroPTitle
            // 
            this.metroPTitle.Controls.Add(this.metroLabCountVal);
            this.metroPTitle.Controls.Add(this.metroLabTitle);
            this.metroPTitle.Controls.Add(this.metroLabTotal);
            this.metroPTitle.CustomBackground = false;
            this.metroPTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPTitle.HorizontalScrollbar = false;
            this.metroPTitle.HorizontalScrollbarBarColor = true;
            this.metroPTitle.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPTitle.HorizontalScrollbarSize = 10;
            this.metroPTitle.Location = new System.Drawing.Point(0, 0);
            this.metroPTitle.Name = "metroPTitle";
            this.metroPTitle.Size = new System.Drawing.Size(650, 38);
            this.metroPTitle.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroPTitle.StyleManager = null;
            this.metroPTitle.TabIndex = 0;
            this.metroPTitle.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroPTitle.VerticalScrollbar = false;
            this.metroPTitle.VerticalScrollbarBarColor = true;
            this.metroPTitle.VerticalScrollbarHighlightOnWheel = false;
            this.metroPTitle.VerticalScrollbarSize = 10;
            // 
            // metroLabTotal
            // 
            this.metroLabTotal.AutoSize = true;
            this.metroLabTotal.CustomBackground = false;
            this.metroLabTotal.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabTotal.FontWeight = MetroFramework.MetroLabelWeight.Light;
            this.metroLabTotal.LabelMode = MetroFramework.Controls.MetroLabelMode.Default;
            this.metroLabTotal.Location = new System.Drawing.Point(290, 6);
            this.metroLabTotal.Name = "metroLabTotal";
            this.metroLabTotal.Size = new System.Drawing.Size(98, 25);
            this.metroLabTotal.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabTotal.StyleManager = null;
            this.metroLabTotal.TabIndex = 2;
            this.metroLabTotal.Text = "Total Tags: ";
            this.metroLabTotal.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroLabTotal.UseStyleColors = false;
            // 
            // metroLabTitle
            // 
            this.metroLabTitle.AutoSize = true;
            this.metroLabTitle.CustomBackground = false;
            this.metroLabTitle.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabTitle.FontWeight = MetroFramework.MetroLabelWeight.Light;
            this.metroLabTitle.LabelMode = MetroFramework.Controls.MetroLabelMode.Default;
            this.metroLabTitle.Location = new System.Drawing.Point(3, 6);
            this.metroLabTitle.Name = "metroLabTitle";
            this.metroLabTitle.Size = new System.Drawing.Size(77, 25);
            this.metroLabTitle.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabTitle.StyleManager = null;
            this.metroLabTitle.TabIndex = 3;
            this.metroLabTitle.Text = "Tag Grid";
            this.metroLabTitle.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroLabTitle.UseStyleColors = false;
            // 
            // metroLabCountVal
            // 
            this.metroLabCountVal.AutoSize = true;
            this.metroLabCountVal.CustomBackground = false;
            this.metroLabCountVal.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabCountVal.FontWeight = MetroFramework.MetroLabelWeight.Light;
            this.metroLabCountVal.LabelMode = MetroFramework.Controls.MetroLabelMode.Default;
            this.metroLabCountVal.Location = new System.Drawing.Point(394, 6);
            this.metroLabCountVal.Name = "metroLabCountVal";
            this.metroLabCountVal.Size = new System.Drawing.Size(40, 25);
            this.metroLabCountVal.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabCountVal.StyleManager = null;
            this.metroLabCountVal.TabIndex = 4;
            this.metroLabCountVal.Text = "0(0)";
            this.metroLabCountVal.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroLabCountVal.UseStyleColors = false;
            // 
            // metroPanel2
            // 
            this.metroPanel2.Controls.Add(this.panelStatusColor);
            this.metroPanel2.Controls.Add(this.metroLabStatusMsg);
            this.metroPanel2.CustomBackground = false;
            this.metroPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.metroPanel2.HorizontalScrollbar = false;
            this.metroPanel2.HorizontalScrollbarBarColor = true;
            this.metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel2.HorizontalScrollbarSize = 10;
            this.metroPanel2.Location = new System.Drawing.Point(0, 373);
            this.metroPanel2.Name = "metroPanel2";
            this.metroPanel2.Size = new System.Drawing.Size(650, 33);
            this.metroPanel2.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroPanel2.StyleManager = null;
            this.metroPanel2.TabIndex = 2;
            this.metroPanel2.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroPanel2.VerticalScrollbar = false;
            this.metroPanel2.VerticalScrollbarBarColor = true;
            this.metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel2.VerticalScrollbarSize = 10;
            // 
            // metroLabStatusMsg
            // 
            this.metroLabStatusMsg.AutoSize = true;
            this.metroLabStatusMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.metroLabStatusMsg.CustomBackground = false;
            this.metroLabStatusMsg.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabStatusMsg.FontWeight = MetroFramework.MetroLabelWeight.Light;
            this.metroLabStatusMsg.LabelMode = MetroFramework.Controls.MetroLabelMode.Default;
            this.metroLabStatusMsg.Location = new System.Drawing.Point(3, 4);
            this.metroLabStatusMsg.Name = "metroLabStatusMsg";
            this.metroLabStatusMsg.Size = new System.Drawing.Size(152, 25);
            this.metroLabStatusMsg.Style = MetroFramework.MetroColorStyle.Green;
            this.metroLabStatusMsg.StyleManager = null;
            this.metroLabStatusMsg.TabIndex = 4;
            this.metroLabStatusMsg.Text = "Connection Is Ok! ";
            this.metroLabStatusMsg.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroLabStatusMsg.UseStyleColors = false;
            // 
            // panelStatusColor
            // 
            this.panelStatusColor.Location = new System.Drawing.Point(597, 2);
            this.panelStatusColor.Name = "panelStatusColor";
            this.panelStatusColor.Size = new System.Drawing.Size(50, 30);
            this.panelStatusColor.TabIndex = 5;
            // 
            // metroTagGrid
            // 
            this.metroTagGrid.AllowUserToResizeRows = false;
            this.metroTagGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.metroTagGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.metroTagGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.metroTagGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroTagGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.metroTagGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.metroTagGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTagGrid.EnableHeadersVisualStyles = false;
            this.metroTagGrid.FontSize = MetroFramework.MetroDataGridSize.Medium;
            this.metroTagGrid.FontWeight = MetroFramework.MetroDataGridWeight.Regular;
            this.metroTagGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.metroTagGrid.Location = new System.Drawing.Point(0, 38);
            this.metroTagGrid.Name = "metroTagGrid";
            this.metroTagGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroTagGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.metroTagGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.metroTagGrid.RowTemplate.Height = 23;
            this.metroTagGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.metroTagGrid.Size = new System.Drawing.Size(650, 335);
            this.metroTagGrid.TabIndex = 3;
            // 
            // ManageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 486);
            this.Controls.Add(this.metroSplitContainerMain);
            this.Name = "ManageForm";
            this.Text = "ManageForm";
            this.metroSplitContainerMain.Panel1.ResumeLayout(false);
            this.metroSplitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.metroSplitContainerMain)).EndInit();
            this.metroSplitContainerMain.ResumeLayout(false);
            this.metroPTitle.ResumeLayout(false);
            this.metroPTitle.PerformLayout();
            this.metroPanel2.ResumeLayout(false);
            this.metroPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroTagGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroSplitContainer metroSplitContainerMain;
        private MetroFramework.Controls.MetroButton metroBtnConnect;
        private MetroFramework.Controls.MetroButton metroBtnStart;
        private MetroFramework.Controls.MetroButton metroBtnStop;
        private MetroFramework.Controls.MetroButton metroBtnDisConn;
        private MetroFramework.Controls.MetroPanel metroPTitle;
        private MetroFramework.Controls.MetroLabel metroLabTotal;
        private MetroFramework.Controls.MetroLabel metroLabTitle;
        private MetroFramework.Controls.MetroLabel metroLabCountVal;
        private MetroFramework.Controls.MetroPanel metroPanel2;
        private MetroFramework.Controls.MetroLabel metroLabStatusMsg;
        private System.Windows.Forms.Panel panelStatusColor;
        private MetroFramework.Controls.MetroGrid metroTagGrid;
    }
}

