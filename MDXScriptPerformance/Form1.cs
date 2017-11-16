using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.AnalysisServices.AdomdClient;
using Microsoft.AnalysisServices;
using Microsoft.SqlServer.MessageBox;

namespace MDXScriptPerformance
{
    public partial class frmMDXScript : Form
    {
        private Microsoft.AnalysisServices.Server m_server;
        private Microsoft.AnalysisServices.Database m_database;
        private Microsoft.AnalysisServices.Cube m_cube;
        private Resultset displayResultset;

        public frmMDXScript()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            disableControls();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (null != m_server)
                {
                    m_server.Disconnect();
                }
                else
                {
                    m_server = new Server();
                }

                m_server.Connect(txtServer.Text);
                enableControls();
                populateDBDropDown();
            }
            catch (AmoException exception)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(exception);
                emb.Show(this);
                disableControls();
            }
        }

        private void populateDBDropDown()
        {
            cboDatabase.Items.Clear();
            foreach (Database d in m_server.Databases)
                cboDatabase.Items.Add(d.Name);

        }

        private void populateCubeDropDown()
        {
            cboCube.Items.Clear();
            foreach (Cube c in m_database.Cubes)
                cboCube.Items.Add(c.Name);
        }


        private void enableControls()
        {
            cboCube.Enabled = true;
            cboDatabase.Enabled = true;
            txtQuery.Enabled = true;
            rptResults.Enabled = true;
            btnRunQuery.Enabled = true;
        }

        private void disableControls()
        {
            cboCube.Enabled = false;
            cboDatabase.Enabled = false;
            txtQuery.Enabled = false;
            rptResults.Enabled = false;
            btnRunQuery.Enabled = false;
        }

        private void frmMDXScript_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (null != m_server)
                {
                    m_server.Disconnect();
                }
            }
            finally
            {
                if (null != m_server)
                {
                    m_server.Dispose();
                    m_database.Dispose();
                    m_cube.Dispose();
                }
            }
        }

        private void cboDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_database = m_server.Databases.GetByName(cboDatabase.SelectedItem.ToString());
            populateCubeDropDown();
        }

        private bool bRunning = false;
        private BackgroundWorker worker;

        private void btnRunQuery_Click(object sender, EventArgs e)
        {
            if (worker != null && worker.IsBusy && btnRunQuery.Text == "Cancelling")
            {
                MessageBox.Show("The previous run is still cancelling. Please wait.");
            }
            else if (!bRunning)
            {
                displayResultset = new Resultset(txtQuery.Text, progressBar1);
                if (displayResultset.TrimBlankLines(txtQuery.Text).Length > 0)
                {
                    bRunning = true;
                    btnRunQuery.Text = "Cancel";
                    progressBar1.Visible = true;
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = 100;
                    progressBar1.Value = progressBar1.Minimum;
                    progressBar1.Step = 1;
                    progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
                    Application.DoEvents();

                    worker = new BackgroundWorker();
                    worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                    worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                    worker.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("Enter an MDX query to test!");
                }
            }
            else
            {
                progressBar1.Visible = false;
                btnRunQuery.Text = "Cancelling";
                bRunning = false;
            }
        }

        protected delegate void worker_RunWorkerCompleted_Delegate(object source, RunWorkerCompletedEventArgs e);
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    //avoid the "cross-thread operation not valid" error message
                    this.BeginInvoke(new worker_RunWorkerCompleted_Delegate(worker_RunWorkerCompleted), new object[] { null, null });
                }
                else
                {
                    ResultItemBindingSource.DataSource = displayResultset.getResults();
                    Microsoft.Reporting.WinForms.ReportParameter m_query = new Microsoft.Reporting.WinForms.ReportParameter("pQueryText", txtQuery.Text);
                    rptResults.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter[] { m_query });
                    rptResults.RefreshReport();

                    btnRunQuery.Text = "Run Query";
                    bRunning = false;
                    progressBar1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                m_cube.Refresh(true, RefreshType.LoadedObjectsOnly); //if you've updated the calc script on the server, be sure to refresh it in memory
                int iters;
                if (!int.TryParse(txtIters.Text, out iters))
                {
                    iters = 1;
                    txtIters.Text = "1";
                }

                displayResultset.RunQueries(m_cube, iters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        private void cboCube_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_cube = m_database.Cubes.GetByName(cboCube.SelectedItem.ToString()); 
            if (txtQuery.Text.Length == 0)
            {
                txtQuery.Text = String.Format("SELECT [Measures].DefaultMember ON 0 FROM [{0}]", m_cube.Name);
            }
        }

        private void rptResults_Load(object sender, EventArgs e)
        {

        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtQuery_TextChanged(object sender, EventArgs e)
        {

        }

        private void ResultItemBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}