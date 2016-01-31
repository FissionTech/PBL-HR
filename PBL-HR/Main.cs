using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace PBL_HR {
    public partial class Main : Form {
        public Main () {
            InitializeComponent();
            StatesArray.PopulateArray();
            LoadTemplate("ReportBasic.txt");
            this.Users.CellValueChanged += Users_CellValueChanged;
        }

        /// <summary>
        /// We have to use the lengthy specifier because there are two SQLite
        /// Libraries: The .NET default and SQLite-Net. The latter is used to establish
        /// easy, LINQ-based queries whereas the former is used to handle
        /// more complex queries
        /// </summary>
        SQLite.SQLiteConnection DB;
        String DB_Connection;
        String Template;

        private void menuStrip1_ItemClicked (object sender, ToolStripItemClickedEventArgs e) {

        }

        /// <summary>
        /// Opens a file dialog to read one of (possibly) many SQLite Databases
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click (object sender, EventArgs e) {
            Stream dbStream;
            OpenFileDialog saveFileDialog = new OpenFileDialog();

            saveFileDialog.Filter = "SQLite files (*.sqlite)|*.sqlite";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                if ((dbStream = saveFileDialog.OpenFile()) != null) {

                    if (File.Exists(saveFileDialog.FileName)) {
                        // !!! Must close the stream
                        dbStream.Close();

                        // Load the database into a variable database
                        DB = new SQLite.SQLiteConnection(saveFileDialog.FileName);
                        // Have the connection string available for .NET SQLite
                        DB_Connection = saveFileDialog.FileName;
                        // Update the Data Grid View (Users)
                        SetView();
                    }

                }
            }
        }

        /// <summary>
        /// Opens a file dialog that allows for creation of multiple databases
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createFileToolStripMenuItem_Click (object sender, EventArgs e) {
            Stream newFileStream;
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "SQLite files (*.sqlite)|*.sqlite";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
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

        /// <summary>
        /// Updates the DataGridView (Users) with any new information in the DB, as
        /// the DataGridView does not do so automatically
        /// </summary>
        public void SetView() {

            System.Data.SQLite.SQLiteConnection con = new System.Data.SQLite.SQLiteConnection("Data Source=" + DB_Connection + ";Version=3;");
            DataSet dataSet = new DataSet();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter("SELECT * FROM Members", con);
            dataAdapter.Fill(dataSet);

            Users.DataSource = dataSet.Tables[0].DefaultView;

        }

        /// <summary>
        /// Uses a dialog to allow the user to add a new entry in the DB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click (object sender, EventArgs e) {
            using (NewEntry dialog = new NewEntry()) {

                if (dialog.ShowDialog() == DialogResult.OK) {
                    Members m = new Members() {
                        FirstName = dialog.FNText.Text,
                        LastName = dialog.LNText.Text,
                        School = dialog.SchoolText.Text,
                        State = StatesArray.AbbrevToState[(String) dialog.StateCombo.SelectedItem],
                        Email = dialog.EmailText.Text,
                        Grade = byte.Parse(dialog.GradeText.Text),
                        AmountOwed = float.Parse(dialog.AmountOwedText.Text),
                        Active = dialog.ActiveCheckbox.Checked
                    };
                    DB.Insert(m);
                }

                SetView();

            }
        }

        /// <summary>
        /// Updates, or commits all changes to the DB to the DB File
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click (object sender, EventArgs e) {
            DB.Commit();
            DB.Close();
        }

        /// <summary>
        /// Uupdate the DB instance with the value that has changed in the DataGridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Users_CellValueChanged (object sender, DataGridViewCellEventArgs e) {
            int row = e.RowIndex;
            int column = e.ColumnIndex;  

            int id = int.Parse(Users.Rows[row].Cells[0].Value.ToString());
            var value = Users.Rows[row].Cells[column].Value;

            Console.WriteLine("UPDATE " + Globals.MembersColumnNames[column] + "=" + value + " WHERE Id=" + id);
            DB.Execute("UPDATE Members SET " + Globals.MembersColumnNames[column] + "=" + value + " WHERE Id=" + id);

        }

        private void LoadTemplate(String loc) {
            // Load template from file for customization & less hard-coded html
            String template = File.ReadAllText(loc);

            // Set the template variable to the loaded file
            SetTemplate(template);
        }

        private void SetTemplate(String newTemplate) {
            this.Template = newTemplate;
        }

        private void SetHTMLView(String document) {
            HTMLViewer.Navigate("about:blank");
            HTMLViewer.Document.OpenNew(false);
            HTMLViewer.Document.Write(document);
            HTMLViewer.Refresh();
        }

        private String GetFile (String type) {
            OpenFileDialog saveFileDialog = new OpenFileDialog();

            saveFileDialog.Filter = "SQLite files (*." + type + ")|*." + type;
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK) {

                if (File.Exists(saveFileDialog.FileName)) {
                    return saveFileDialog.FileName;
                } else {
                    return "FNF";
                }
            } else {
                return "C";
            }
        }

        /// <summary>
        /// Generates a quick report from a pre-built template (The quick report is
        /// the FBLA required guidelines)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quickToolStripMenuItem_Click (object sender, EventArgs e) {
            // Loop through each all 50 states and adds appropriate HTML code for all
            // entries it finds that belong to that state
            StringBuilder rp = new StringBuilder();
            foreach(var i in StatesArray.AbbrevToState) {
                rp.Append("<h1 class='state' id='" + i.Value + "'>" + i.Value + "</h1>");
                rp.Append("<table class='table table-striped'> <tr>");
                // Loops through all the column names and creates an appropriate HTML Table
                foreach(String s in Globals.MembersColumnNames) {
                    rp.Append("<th>" + s + "</th>");
                }
                rp.Append("</tr>");

                // SQL Query for all members matching the current state (i)
                var query = DB.Query<Members>("SELECT * FROM Members WHERE State=?", i.Value);
                foreach(var r in query) {
                    rp.Append("<tr> <td>" + r.Id + "</td> <td>" + r.FirstName + "</td> <td>" + r.LastName + "</td> <td>" + r.School + "</td> <td>" + r.State + "</td> <td>" + r.Email + "</td> <td>" + r.Grade + "</td> <td>" + r.AmountOwed + "</td> <td>" + r.Active + "</td> </tr>");
                }
                rp.Append("</table>");
            }

            // DEBUG: Clipboard.SetText(rp.ToString());

            // Replace the 'variable' sections of the HTML
            String revisedTemplate;
            revisedTemplate = Template.Replace("@TITLE", "Quick Report");
            revisedTemplate = Template.Replace("@BODY", rp.ToString());
            SetHTMLView(revisedTemplate);
            // DEBUG: Clipboard.SetText(template);
        }

        private void loadTemplateToolStripMenuItem_Click (object sender, EventArgs e) {
            String file = GetFile("txt");
            LoadTemplate(file);
        }

        private void printToolStripMenuItem_Click_1 (object sender, EventArgs e) {
            HTMLViewer.Print();
        }

        private void saveToolStripMenuItem1_Click (object sender, EventArgs e) {
            HTMLViewer.ShowSaveAsDialog();
        }



        private void newTemplateToolStripMenuItem_Click (object sender, EventArgs e) {
            using (NewTemplate dialog = new NewTemplate()) {
                if(dialog.ShowDialog() == DialogResult.OK) {
                    SetTemplate(dialog.Editor.Text);
                }
            }
        }
    }
}
