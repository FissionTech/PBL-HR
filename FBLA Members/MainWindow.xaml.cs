using MahApps.Metro.Controls;
using Microsoft.Win32;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
//using CefSharp;

namespace FBLA_Members {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow {
        public MainWindow () {
            InitializeComponent();

            //if (Cef.IsInitialized == false) {
            //Cef.Initialize(new CefSettings());
            //}
            StatesArray.PopulateArray();
            Users.SelectionMode = DataGridSelectionMode.Single;
            Users.SelectionUnit = DataGridSelectionUnit.Cell;
            LoadTemplate("Resources/ReportBasic.txt");

            if((bool)Properties.Settings.Default["FirstRun"]) {
                // First run, display FirstRun
                FirstRun fr = new FirstRun();
                fr.Show();
                fr.Topmost = true;
                Properties.Settings.Default["FirstRun"] = false;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// We have to use the lengthy specifier because there are two SQLite
        /// Libraries: The .NET default and SQLite-Net. The latter is used to establish
        /// easy, LINQ-based queries whereas the former is used to handle
        /// more complex queries
        /// </summary>
        SQLite.SQLiteConnection DB;
        String DB_Connection;
        DataSet DataSet;
        String Template;


        /// <summary>
        /// Updates the DataGridView (Users) with any new information in the DB, as
        /// the DataGridView does not do so automatically
        /// </summary>
        public void SetView () {

            System.Data.SQLite.SQLiteConnection con = new System.Data.SQLite.SQLiteConnection("Data Source=" + DB_Connection + ";Version=3;");
            DataSet = new DataSet();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter("SELECT * FROM Members", con);
            dataAdapter.AcceptChangesDuringFill = false;
            dataAdapter.Fill(DataSet);
            DataSet.AcceptChanges();

            Users.DataContext = DataSet.Tables[0].DefaultView;
            con.Close();
            GC.Collect();

        }

        public void SaveDatabase () {
            DataSet changes = DataSet.GetChanges();
            if(changes != null) {
                System.Data.SQLite.SQLiteConnection con = new System.Data.SQLite.SQLiteConnection("Data Source=" + DB_Connection + ";Version=3;");
                SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter("SELECT * FROM Members", con);
                using (SQLiteCommandBuilder cb = new SQLiteCommandBuilder(dataAdapter)) {
                    dataAdapter.Update(changes);
                    DataSet.AcceptChanges();
                    con.Close();
                    GC.Collect();
                }
            }


        }

        private void LoadTemplate (String loc) {
            // Load template from file for customization & less hard-coded html
            String template = File.ReadAllText(loc);

            // Set the template variable to the loaded file
            SetTemplate(template);
        }

        private void SetTemplate (String newTemplate) {
            this.Template = newTemplate;

        }

        private void SetHTMLView (String document) {
            //HTMLViewer.Load("http://www.google.com/");
            //HTMLViewer.LoadHtml(document, "http://rendering/");
            //HTMLViewer.Navigate("http://www.google.com/");
            HTMLViewer.NavigateToString(document);

            //HTMLViewer.Refresh();
        }

        private String GetFile (String type) {
            OpenFileDialog saveFileDialog = new OpenFileDialog();

            saveFileDialog.Filter = "SQLite files (*." + type + ")|*." + type;
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == true) {

                if (File.Exists(saveFileDialog.FileName)) {
                    return saveFileDialog.FileName;
                } else {
                    return "FNF";
                }
            } else {
                return "C";
            }
        }

        byte[] GetBytes (string str) {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        private String SaveFile (String Document) {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            Stream stream;
            saveFileDialog.Filter = "HTML files (*.html)|*.html";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;


            if (saveFileDialog.ShowDialog() == true) {
                using (StreamWriter sw = new StreamWriter(saveFileDialog.OpenFile())) {
                    sw.Write(Document);
                }
                return saveFileDialog.FileName;
            } else {
                return "ERR";
            }
        }

        private String SaveFile(ExcelPackage ep) {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "Excel Spreadsheet (*.xlsx)|*.xlsx";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;

            if(saveFileDialog.ShowDialog() == true) {
                if(!File.Exists(saveFileDialog.FileName)) {
                    File.Create(saveFileDialog.FileName);
                }
                ep.SaveAs(new FileInfo(saveFileDialog.FileName));
            }

            return "Good";
        }

        private void NewMenuItem_Click (object sender, RoutedEventArgs e) {
            Stream newFileStream;
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "SQLite files (*.sqlite)|*.sqlite";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == true) {
                if ((newFileStream = saveFileDialog.OpenFile()) != null) {

                    if (File.Exists(saveFileDialog.FileName)) {
                        newFileStream.Close();

                        DB = new SQLite.SQLiteConnection(saveFileDialog.FileName);
                        DB_Connection = saveFileDialog.FileName;

                        // Hard coded members class, need to add custom table structure
                        DB.CreateTable<Members>();

                        // Possibly save DB
                        SetView();
                    }

                }
            }

        }

        private void OpenMenuItem_Click (object sender, RoutedEventArgs e) {
            Stream dbStream;
            OpenFileDialog saveFileDialog = new OpenFileDialog();

            saveFileDialog.Filter = "SQLite files (*.sqlite)|*.sqlite";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.Multiselect = false;

            if (saveFileDialog.ShowDialog() == true) {
                if (saveFileDialog.FileName != DB_Connection) {
                    if ((dbStream = saveFileDialog.OpenFile()) != null) {

                        if (File.Exists(saveFileDialog.FileName)) {


                            // !!! Must close the stream
                            dbStream.Close();

                            // Load the database into a variable database
                            DB = new SQLite.SQLiteConnection(saveFileDialog.FileName);

                            // Have the connection string available for .NET SQLite
                            DB_Connection = @saveFileDialog.FileName;
                            // Update the Data Grid View (Users)
                            SetView();
                        }
                    }
                } else {
                    MessageBox.Show("You cannot open the currently open file.");
                }
            }
        }

        private void SaveMenuItem_Click (object sender, RoutedEventArgs e) {
            if (DB != null) {
                SaveDatabase();
            } else {
                MessageBox.Show("You can't save a database before you open it.");
            }
            
        }

        private void Quick_Click (object sender, RoutedEventArgs e) {
            if (DB != null) {
                // Loop through each all 50 states and adds appropriate HTML code for all
                // entries it finds that belong to that state
                StringBuilder rp = new StringBuilder();
                foreach (var i in StatesArray.AbbrevToState) {
                    rp.Append("<h1 class='state' id='" + i.Value + "'>" + i.Value + "</h1>");
                    rp.Append("<table class='table table-striped'> <tr>");
                    // Loops through all the column names and creates an appropriate HTML Table
                    foreach (String s in Globals.MembersColumnNames) {
                        rp.Append("<th>" + s + "</th>");
                    }
                    rp.Append("</tr>");

                    // SQL Query for all members matching the current state (i)
                    var query = DB.Query<Members>("SELECT * FROM Members WHERE State=?", i.Value);
                    foreach (var r in query) {
                        rp.Append("<tr> <td>" + r.Id + "</td> <td>" + r.FirstName + "</td> <td>" + r.LastName + "</td> <td>" + r.School + "</td> <td>" + r.State + "</td> <td>" + r.Email + "</td> <td>" + r.Grade + "</td> <td>" + r.AmountOwed + "</td> <td>" + r.Active + "</td> </tr>");
                    }
                    rp.Append("</table>");
                }

                int activeMembers = DB.Query<Members>("SELECT * FROM Members WHERE Active=1").ToArray().Length;
                int nonActiveMembers = DB.Query<Members>("SELECT * FROM Members WHERE Active = 0").ToArray().Length;
                var owingMembers = DB.Query<Members>("SELECT * FROM Members WHERE AmountOwed > 0");
                float totalOwing = 0f;
                foreach (var m in owingMembers) {
                    totalOwing += m.AmountOwed;
                }
                // Replace the 'variable' sections of the HTML
                String revisedTemplate;
                revisedTemplate = Template.Replace("@TITLE", "Report");
                revisedTemplate = Template.Replace("@BODY", rp.ToString());
                revisedTemplate = revisedTemplate.Replace("@ACTIVE_MEMBERS", activeMembers.ToString());
                revisedTemplate = revisedTemplate.Replace("@NONACTIVE_MEMBERS", nonActiveMembers.ToString());
                revisedTemplate = revisedTemplate.Replace("@TOTAL_AMOUNT_OWED", totalOwing.ToString());
                revisedTemplate = revisedTemplate.Replace("@TOTAL_STUDENTS_OWING", owingMembers.ToArray().Length.ToString());
                SetHTMLView(revisedTemplate);
            } else {
                MessageBox.Show("You cannot generate a report before opening a database.");
            }
        }

        private void Quick2_Click (object sender, RoutedEventArgs e) {
            if(DB != null) {

            } else {
                
            }
            StringBuilder rp = new StringBuilder();
            rp.Append("<h1 class='title' id='title'>Senior Report</h1>");
            foreach (var i in StatesArray.AbbrevToState) {
                rp.Append("<h1 class='state' id='" + i.Value + "'>" + i.Value + "</h1>");
                rp.Append("<table class='table table-striped'> <tr>");
                rp.Append("<th>Id</th>");
                rp.Append("<th>First Name</th>");
                rp.Append("<th>Last Name</th>");
                rp.Append("<th>Email</th>");
                rp.Append("</tr>");

                // SQL Query for all members matching the current state (i)
                var query = DB.Query<Members>("SELECT * FROM Members WHERE State=? AND Grade=12", i.Value);
                foreach (var r in query) {
                    rp.Append("<tr> <td>" + r.Id + "</td> <td>" + r.FirstName + "</td> <td>" + r.LastName + "</td> <td>" + r.Email + "</td></tr>");
                }
                rp.Append("</table>");
            }
            string revisedTemplate;
            revisedTemplate = File.ReadAllText("Resources/SeniorReport.txt").Replace("@TITLE", "Report");
            revisedTemplate = revisedTemplate.Replace("@BODY", rp.ToString());
            revisedTemplate = revisedTemplate.Replace("@ACTIVE_MEMBERS", "");
            revisedTemplate = revisedTemplate.Replace("@NONACTIVE_MEMBERS", "");
            revisedTemplate = revisedTemplate.Replace("@TOTAL_AMOUNT_OWED", "");
            revisedTemplate = revisedTemplate.Replace("@TOTAL_STUDENTS_OWING", "");
            SetHTMLView(revisedTemplate);
        }

        private void LoadTemplate_Click (object sender, RoutedEventArgs e) {
            if(DB != null) {
                String file = GetFile("txt");
                LoadTemplate(file);
                // Loop through each all 50 states and adds appropriate HTML code for all
                // entries it finds that belong to that state
                StringBuilder rp = new StringBuilder();
                foreach (var i in StatesArray.AbbrevToState) {
                    rp.Append("<h1 class='state' id='" + i.Value + "'>" + i.Value + "</h1>");
                    rp.Append("<table class='table table-striped'> <tr>");
                    // Loops through all the column names and creates an appropriate HTML Table
                    foreach (String s in Globals.MembersColumnNames) {
                        rp.Append("<th>" + s + "</th>");
                    }
                    rp.Append("</tr>");

                    // SQL Query for all members matching the current state (i)
                    var query = DB.Query<Members>("SELECT * FROM Members WHERE State=?", i.Value);
                    foreach (var r in query) {

                        rp.Append("<tr> <td>" + r.Id + "</td> <td>" + r.FirstName + "</td> <td>" + r.LastName + "</td> <td>" + r.School + "</td> <td>" + r.State + "</td> <td>" + r.Email + "</td> <td>" + r.Grade + "</td> <td>" + r.AmountOwed + "</td> <td>" + r.Active + "</td> </tr>");
                    }
                    rp.Append("</table>");
                }

                int activeMembers = DB.Query<Members>("SELECT * FROM Members WHERE Active=1").ToArray().Length;
                int nonActiveMembers = DB.Query<Members>("SELECT * FROM Members WHERE Active = 0").ToArray().Length;
                var owingMembers = DB.Query<Members>("SELECT * FROM Members WHERE AmountOwed > 0");
                float totalOwing = 0f;
                foreach (var m in owingMembers) {
                    totalOwing += m.AmountOwed;
                }
                // Replace the 'variable' sections of the HTML
                String revisedTemplate;
                revisedTemplate = Template.Replace("@TITLE", "Report");
                revisedTemplate = Template.Replace("@BODY", rp.ToString());
                revisedTemplate = revisedTemplate.Replace("@ACTIVE_MEMBERS", activeMembers.ToString());
                revisedTemplate = revisedTemplate.Replace("@NONACTIVE_MEMBERS", nonActiveMembers.ToString());
                revisedTemplate = revisedTemplate.Replace("@TOTAL_AMOUNT_OWED", totalOwing.ToString());
                revisedTemplate = revisedTemplate.Replace("@TOTAL_STUDENTS_OWING", owingMembers.ToArray().Length.ToString());
                SetHTMLView(revisedTemplate);
            } else {
                MessageBox.Show("You cannot load a template before opening a database.");
            }
        }

        private void NewTemplate_Click (object sender, RoutedEventArgs e) {
            if(DB != null) {
                NewTemplate dialog = new NewTemplate();
                if (dialog.ShowDialog() == true) {
                    SetTemplate(dialog.NewTemplateText.Text);
                }
            } else {
                MessageBox.Show("You cannot create a template before opening a database.");
            }
        }

        private void Print_Click (object sender, RoutedEventArgs e) {
            // Send the Print command to WebBrowser
            if(DB != null) {
                if(HTMLViewer.Document != null) {
                    mshtml.IHTMLDocument2 doc = HTMLViewer.Document as mshtml.IHTMLDocument2;
                    doc.execCommand("Print", true, null);
                } else {
                    MessageBox.Show("You cannot print before creating a report.");
                }
            } else {
                MessageBox.Show("You cannot print a database report before you have opened a report.");
            }

        }

        private void SaveAs_Click (object sender, RoutedEventArgs e) {
            if(DB != null) {
                if(HTMLViewer.Document != null) {
                    dynamic doc = HTMLViewer.Document;
                    var htmlText = doc.documentElement.InnerHtml;
                    String f = SaveFile(htmlText);
                } else {
                    MessageBox.Show("You can't save a report before you've generated it.");
                }

            } else {
                MessageBox.Show("You cannot save a database before opening it.");
            }
        }

        private void Add_Click (object sender, RoutedEventArgs e) {
            if(DB != null) {
                NewEntry dialog = new NewEntry();

                if (dialog.ShowDialog() == true) {
                    Members m = new Members();
                    m.FirstName = dialog.FirstName.Text;
                    m.LastName = dialog.LastName.Text;
                    m.School = dialog.School.Text;
                    m.State = StatesArray.AbbrevToState[(String)dialog.StateCombo.SelectedItem];
                    m.Email = dialog.Email.Text;
                    m.Grade = byte.Parse(dialog.Grade.Text);
                    m.AmountOwed = float.Parse(dialog.AmountOwed.Text);
                    m.Active = (bool)dialog.Active.IsChecked;
                    DB.Insert(m);
                }

                SetView();
            } else {
                MessageBox.Show("You cannot add members before opening a database.");
            }
        }

        private void Instructions_Click (object sender, RoutedEventArgs e) {
            Instructions i = new Instructions();
            i.ShowDialog();
        }

        private void SeniorXLS_Click (object sender, RoutedEventArgs e) {
            if(DB != null) {
                using (ExcelPackage p = new ExcelPackage()) {
                    p.Workbook.Worksheets.Add("FBLA Seniors"); // Create the woorksheet
                    ExcelWorksheet ws = p.Workbook.Worksheets[1]; // 1 is the position of the worksheet
                    ws.Name = "FBLA Seniors"; // Not 100% sure I need this here

                    // Add some styling
                    ws.Cells[1, 1].Value = "Seniors";

                    // Loop through all the states

                    int state = 2;
                    foreach (var st in StatesArray.AbbrevToState) {
                        ws.Cells[state, 1].Value = st.Key;
                        // SQL Query for all members matching the current state (i)
                        var query = DB.Query<Members>("SELECT * FROM Members WHERE State=? AND Grade=12", st.Value);
                        int member = 1;
                        foreach (var r in query) {
                            var id = ws.Cells[state + member, 1];
                            id.Value = r.Id;
                            id.AutoFitColumns();

                            var firstname = ws.Cells[state + member, 2];
                            firstname.Value = r.FirstName;
                            firstname.AutoFitColumns();

                            var lastname = ws.Cells[state + member, 3];
                            lastname.Value = r.LastName;
                            lastname.AutoFitColumns();

                            var email = ws.Cells[state + member, 4];
                            email.Value = r.Email;
                            email.AutoFitColumns();

                            member++;
                        }
                        state += query.ToArray().Length;
                        state++;
                    }

                    SaveFile(p);
                }
            } else {
                MessageBox.Show("You can only generate reports if you have an open database.");
            }
        }
    }
}
