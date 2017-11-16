namespace MDXScriptPerformance
{
    partial class frmMDXScript
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIters = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnRunQuery = new System.Windows.Forms.Button();
            this.cboCube = new System.Windows.Forms.ComboBox();
            this.lblCube = new System.Windows.Forms.Label();
            this.cboDatabase = new System.Windows.Forms.ComboBox();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.lblServer = new System.Windows.Forms.Label();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.rptResults = new Microsoft.Reporting.WinForms.ReportViewer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ResultItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResultItemBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtIters);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.btnRunQuery);
            this.panel1.Controls.Add(this.cboCube);
            this.panel1.Controls.Add(this.lblCube);
            this.panel1.Controls.Add(this.cboDatabase);
            this.panel1.Controls.Add(this.lblDatabase);
            this.panel1.Controls.Add(this.btnConnect);
            this.panel1.Controls.Add(this.txtServer);
            this.panel1.Controls.Add(this.lblServer);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(594, 111);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(509, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "minOf";
            // 
            // txtIters
            // 
            this.txtIters.Location = new System.Drawing.Point(549, 37);
            this.txtIters.Name = "txtIters";
            this.txtIters.Size = new System.Drawing.Size(34, 20);
            this.txtIters.TabIndex = 9;
            this.txtIters.Text = "1";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 96);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(528, 20);
            this.progressBar1.TabIndex = 8;
            this.progressBar1.Visible = false;
            // 
            // btnRunQuery
            // 
            this.btnRunQuery.Location = new System.Drawing.Point(420, 71);
            this.btnRunQuery.Margin = new System.Windows.Forms.Padding(2);
            this.btnRunQuery.Name = "btnRunQuery";
            this.btnRunQuery.Size = new System.Drawing.Size(72, 20);
            this.btnRunQuery.TabIndex = 7;
            this.btnRunQuery.Text = "Run Query";
            this.btnRunQuery.UseVisualStyleBackColor = true;
            this.btnRunQuery.Click += new System.EventHandler(this.btnRunQuery_Click);
            // 
            // cboCube
            // 
            this.cboCube.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCube.FormattingEnabled = true;
            this.cboCube.Location = new System.Drawing.Point(57, 70);
            this.cboCube.Margin = new System.Windows.Forms.Padding(2);
            this.cboCube.Name = "cboCube";
            this.cboCube.Size = new System.Drawing.Size(359, 21);
            this.cboCube.TabIndex = 6;
            this.cboCube.SelectedIndexChanged += new System.EventHandler(this.cboCube_SelectedIndexChanged);
            // 
            // lblCube
            // 
            this.lblCube.AutoSize = true;
            this.lblCube.Location = new System.Drawing.Point(12, 78);
            this.lblCube.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCube.Name = "lblCube";
            this.lblCube.Size = new System.Drawing.Size(38, 13);
            this.lblCube.TabIndex = 5;
            this.lblCube.Text = "Cube: ";
            // 
            // cboDatabase
            // 
            this.cboDatabase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDatabase.FormattingEnabled = true;
            this.cboDatabase.Location = new System.Drawing.Point(57, 40);
            this.cboDatabase.Margin = new System.Windows.Forms.Padding(2);
            this.cboDatabase.Name = "cboDatabase";
            this.cboDatabase.Size = new System.Drawing.Size(435, 21);
            this.cboDatabase.TabIndex = 4;
            this.cboDatabase.SelectedIndexChanged += new System.EventHandler(this.cboDatabase_SelectedIndexChanged);
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Location = new System.Drawing.Point(2, 40);
            this.lblDatabase.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(59, 13);
            this.lblDatabase.TabIndex = 3;
            this.lblDatabase.Text = "Database: ";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(512, 7);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(72, 20);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(57, 7);
            this.txtServer.Margin = new System.Windows.Forms.Padding(2);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(435, 20);
            this.txtServer.TabIndex = 1;
            this.txtServer.Text = "localhost";
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(2, 7);
            this.lblServer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(44, 13);
            this.lblServer.TabIndex = 0;
            this.lblServer.Text = "Server: ";
            // 
            // txtQuery
            // 
            this.txtQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuery.Location = new System.Drawing.Point(0, 0);
            this.txtQuery.Margin = new System.Windows.Forms.Padding(2);
            this.txtQuery.MaxLength = 0;
            this.txtQuery.Multiline = true;
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.Size = new System.Drawing.Size(594, 162);
            this.txtQuery.TabIndex = 1;
            this.txtQuery.TextChanged += new System.EventHandler(this.txtQuery_TextChanged);
            // 
            // rptResults
            // 
            this.rptResults.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "MDXScriptPerformance_ResultItem";
            reportDataSource1.Value = this.ResultItemBindingSource;
            this.rptResults.LocalReport.DataSources.Add(reportDataSource1);
            this.rptResults.LocalReport.ReportEmbeddedResource = "MDXScriptPerformance.ShowResults.rdlc";
            this.rptResults.Location = new System.Drawing.Point(0, 0);
            this.rptResults.Margin = new System.Windows.Forms.Padding(2);
            this.rptResults.Name = "rptResults";
            this.rptResults.Size = new System.Drawing.Size(594, 235);
            this.rptResults.TabIndex = 0;
            this.rptResults.Load += new System.EventHandler(this.rptResults_Load);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 111);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtQuery);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rptResults);
            this.splitContainer1.Size = new System.Drawing.Size(594, 400);
            this.splitContainer1.SplitterDistance = 162;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 3;
            // 
            // ResultItemBindingSource
            // 
            this.ResultItemBindingSource.DataSource = typeof(MDXScriptPerformance.ResultItem);
            this.ResultItemBindingSource.CurrentChanged += new System.EventHandler(this.ResultItemBindingSource_CurrentChanged);
            // 
            // frmMDXScript
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 511);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmMDXScript";
            this.Text = "MDX Script Performance Analyser";
            //this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMDXScript_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ResultItemBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cboCube;
        private System.Windows.Forms.Label lblCube;
        private System.Windows.Forms.ComboBox cboDatabase;
        private System.Windows.Forms.Label lblDatabase;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Button btnRunQuery;
        private System.Windows.Forms.BindingSource ResultItemBindingSource;
        private System.Windows.Forms.TextBox txtQuery;
        private Microsoft.Reporting.WinForms.ReportViewer rptResults;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox txtIters;
        private System.Windows.Forms.Label label1;
    }
}

