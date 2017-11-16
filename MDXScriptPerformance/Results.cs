using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AnalysisServices.AdomdClient;
using Microsoft.AnalysisServices;
using System.Collections;
using System.Diagnostics;

namespace MDXScriptPerformance
{
    public class ResultItem
    {
        private string m_scriptCommandText;
        private long m_timeTaken;
        private long m_delta;
        private int m_statementNumber;

        public ResultItem(int statementNumber, string scriptCommandText, long timeTaken, long delta)
        {
            m_scriptCommandText = scriptCommandText;
            m_timeTaken = timeTaken;
            m_statementNumber = statementNumber;
            m_delta = delta;
        }

        public string scriptCommandText
        {
            get
            {
                return m_scriptCommandText;
            }
        }
        public long timeTaken
        {
            get
            {
                return m_timeTaken;
            }
        }
        public long delta
        {
            get
            {
                return m_delta;
            }
        }
        public int statementNumber
        {
            get
            {
                return m_statementNumber;
            }
        }
    }

    public class Resultset
    {
        private List<ResultItem> m_resultItems;
        private string m_queryText;
        private System.Windows.Forms.ProgressBar m_progressBar;

        public Resultset(string queryText, System.Windows.Forms.ProgressBar progressBar)
        {
            m_queryText = queryText;
            m_resultItems = new List<ResultItem>();
            m_progressBar = progressBar;
        }


        public List<ResultItem> getResults()
        {
            return m_resultItems;
        }

        bool bCancelled = false;

        public void RunQueries(Cube cube, int iters)
        {
            bCancelled = false;
            AdomdConnection conn;
            AdomdCommand comm;
            string[] mdxScriptFragments;
            Stopwatch stp = new Stopwatch();
            string mdxStatement = "";
            int statementCount = 1;
            ArrayList previousStatements = new ArrayList();
            long previousElapsed = 0;

            /*Open ADOMD Connection to run queries*/
            conn = new AdomdConnection("Provider=msolap; Data Source=" + cube.ParentServer.Name + "; Initial Catalog=" + cube.Parent.Name + "; Cube=" + cube.Name + "; MDX Missing Member Mode=Ignore;");
            conn.Open();

            //Create a command to go with it
            comm = new AdomdCommand();
            comm.Connection = conn;

            if (cube.DefaultMdxScript != null)
            {
                /*Loop through the MDX Script, running queries*/
                foreach (Microsoft.AnalysisServices.Command mdxScriptCommand in cube.DefaultMdxScript.Commands)
                {
                    mdxScriptFragments = MDXStatementSplit(mdxScriptCommand.Text);
                    UpdateProgress(0, mdxScriptFragments.Length);

                    foreach (string fragment in mdxScriptFragments)
                    {
                        mdxStatement += fragment;
                        try
                        {
                            /*Run a Clear Cache Command, so we always know the cube's cache is cold*/
                            //cube.ParentServer.Execute(@"<Batch xmlns=""http://schemas.microsoft.com/analysisservices/2003/engine""><ClearCache><Object><DatabaseID>" + cube.Parent.ID + "</DatabaseID><CubeID>" + cube.ID + "</CubeID></Object></ClearCache></Batch>");

                            comm.CommandText = @"<Batch xmlns=""http://schemas.microsoft.com/analysisservices/2003/engine""><ClearCache><Object><DatabaseID>" + cube.Parent.ID + "</DatabaseID><CubeID>" + cube.ID + "</CubeID></Object></ClearCache></Batch>";
                            comm.ExecuteNonQuery();

                            //clearing the cache appears to invalidate the session in AS2012, so we have to close and reopen the connection after clearing the cache
                            conn.Close(true);
                            conn.Open();

                            //clear cache actually erases the impact of CLEAR CALCULATIONS
                            comm.CommandText = "CLEAR CALCULATIONS;";
                            comm.ExecuteNonQuery();

                            //Run all previously executed statements
                            foreach (string prevStatement in previousStatements)
                            {
                                comm.CommandText = prevStatement;
                                comm.ExecuteNonQuery();
                            }

                            //Run the latest section of script
                            comm.CommandText = mdxStatement;
                            comm.ExecuteNonQuery();

                            //not necessary stuff... all copied from the MDX Script Debugger built into vis studio
                            //comm.CommandText = "CREATE CELL CALCULATION CURRENTCUBE.DEBUGGERHIGHLIGHCALC FOR '*' AS '[Measures].CurrentMember' , CONDITION = CalculationPassValue( [Measures].CurrentMember , -2 , RELATIVE, ALL ) <> CalculationPassValue( [Measures].CurrentMember , -1 , RELATIVE, ALL ) , Back_Color = RGB( 255,255,0 ), Calculation_Pass_Number = -1";
                            //comm.ExecuteNonQuery();
                            //comm.CommandText = "REFRESH CUBE [" + cube.Name + "]";
                            //comm.ExecuteNonQuery();
                            //comm.CommandText = "Drop visual totals for [" + cube.Name + "]";
                            //comm.ExecuteNonQuery();

                            //Run the query and time how long it takes
                            long tix = long.MaxValue;
                            for (int i = 0; i < iters; i++)
                            {
                                stp.Reset();
                                stp.Start();
                                comm.CommandText = m_queryText;
                                CellSet cst = comm.ExecuteCellSet();
                                stp.Stop();
                                if (tix > stp.Elapsed.Ticks)
                                    tix = stp.Elapsed.Ticks;
                            }
                            m_resultItems.Add(new ResultItem(statementCount, fragment, tix, tix - previousElapsed));
                            //Add mdxStatement to list of statements already successfully executed and
                            //Set mdxStatement to an empty string if queries have executed ok
                            previousStatements.Add(mdxStatement);
                            previousElapsed = tix;
                            mdxStatement = "";
                            UpdateProgress(statementCount, mdxScriptFragments.Length);
                            statementCount++;
                            System.Windows.Forms.Application.DoEvents();
                            if (bCancelled)
                            {
                                conn.Close();
                                return;
                            }
                        }
                        catch (Exception e)
                        {
                            //Ignore any exceptions and move onto the next fragment
                            //Will be due to semicolons being used as something other than a statement delimiter
                            Console.WriteLine(e.Message);
                        }
                    }
                }
            }
            conn.Close();
        }

        protected delegate void UpdateProgress_Delegate(int statementCount, int max);
        private void UpdateProgress(int statementCount, int max)
        {
            try
            {
                if (m_progressBar.InvokeRequired)
                {
                    //avoid the "cross-thread operation not valid" error message
                    m_progressBar.BeginInvoke(new UpdateProgress_Delegate(UpdateProgress), new object[] { statementCount, max });
                }
                else
                {
                    m_progressBar.Maximum = max;
                    m_progressBar.Value = statementCount;
                    if (!m_progressBar.Visible)
                    {
                        bCancelled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }        
        
        private enum MDXSplitStatus { InMDX, InDashComment, InSlashComment, InBlockComment, InBrackets, InDoubleQuotes, InSingleQuotes };

        private string[] MDXStatementSplit(string sMDX)
        {
            MDXSplitStatus status = MDXSplitStatus.InMDX;
            int iPos = 0;
            int iLastSplit = 0;
            System.Collections.Generic.List<string> arrSplits = new System.Collections.Generic.List<string>();

            while (iPos < sMDX.Length)
            {
                try
                {
                    if (status == MDXSplitStatus.InMDX)
                    {
                        if (sMDX.Substring(iPos, 2) == "/*")
                        {
                            status = MDXSplitStatus.InBlockComment;
                            iPos += 1;
                        }
                        else if (sMDX.Substring(iPos, 2) == "--")
                        {
                            status = MDXSplitStatus.InDashComment;
                            iPos += 1;
                        }
                        else if (sMDX.Substring(iPos, 2) == "//")
                        {
                            status = MDXSplitStatus.InSlashComment;
                            iPos += 1;
                        }
                        else if (sMDX.Substring(iPos, 1) == "[")
                        {
                            status = MDXSplitStatus.InBrackets;
                        }
                        else if (sMDX.Substring(iPos, 1) == "\"")
                        {
                            status = MDXSplitStatus.InDoubleQuotes;
                        }
                        else if (sMDX.Substring(iPos, 1) == "'")
                        {
                            status = MDXSplitStatus.InSingleQuotes;
                        }
                        else if (sMDX.Substring(iPos, 1) == ";") //split on semicolon only when it's in general MDX context
                        {
                            arrSplits.Add(TrimBlankLines(sMDX.Substring(iLastSplit, iPos - iLastSplit)) + ";");
                            iLastSplit = iPos + 1;
                        }
                    }
                    else if (status == MDXSplitStatus.InDashComment || status == MDXSplitStatus.InSlashComment)
                    {
                        if (Environment.NewLine.Contains(sMDX.Substring(iPos, 1)))
                        {
                            status = MDXSplitStatus.InMDX;
                        }
                    }
                    else if (status == MDXSplitStatus.InBlockComment)
                    {
                        if (sMDX.Substring(iPos, 2) == "*/")
                        {
                            status = MDXSplitStatus.InMDX;
                            iPos += 1;
                        }
                    }
                    else if (status == MDXSplitStatus.InBrackets)
                    {
                        if (sMDX.Substring(iPos, 1) == "]" && sMDX.Substring(iPos, 2) == "]]")
                        {
                            iPos += 1;
                        }
                        else if (sMDX.Substring(iPos, 1) == "]" && sMDX.Substring(iPos, 2) != "]]")
                        {
                            status = MDXSplitStatus.InMDX;
                        }
                    }
                    else if (status == MDXSplitStatus.InDoubleQuotes)
                    {
                        if (sMDX.Substring(iPos, 1) == "\"" && sMDX.Substring(iPos, 2) == "\"\"")
                        {
                            iPos += 1;
                        }
                        else if (sMDX.Substring(iPos, 1) == "\"" && sMDX.Substring(iPos, 2) != "\"\"")
                        {
                            status = MDXSplitStatus.InMDX;
                        }
                    }
                    else if (status == MDXSplitStatus.InSingleQuotes)
                    {
                        if (sMDX.Substring(iPos, 1) == "'" && sMDX.Substring(iPos, 2) == "''")
                        {
                            iPos += 1;
                        }
                        else if (sMDX.Substring(iPos, 1) == "'" && sMDX.Substring(iPos, 2) != "''")
                        {
                            status = MDXSplitStatus.InMDX;
                        }
                    }
                    iPos++;
                }
                catch (Exception ex)
                {
                    if (TrimBlankLines(sMDX.Substring(iLastSplit)).Length > 0)
                    {
                        arrSplits.Add(TrimBlankLines(sMDX.Substring(iLastSplit)));
                    }
                    iPos = sMDX.Length;
                }
            }
            return arrSplits.ToArray();
        }

        public string TrimBlankLines(string sString)
        {
            //normalize newlines
            sString = sString.Replace("\r\n", "\n").Replace("\r", "\n");
            string[] arrLines = sString.Split("\n".ToCharArray());
            sString = "";
            string sSkipped = "";
            foreach (string sLine in arrLines)
            {
                if (sLine.Trim().Length > 0)
                {
                    if (sString == "")
                        sString = sLine;
                    else
                        sString += "\n" + sSkipped + sLine;
                    sSkipped = "";
                }
                else
                {
                    sSkipped += sLine + "\n";
                }
            }
            return sString;
        }


    }
}
