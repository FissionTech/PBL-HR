﻿using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Text;
using System.Windows;
//using CefSharp;

namespace Design {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        public MainWindow () {
            InitializeComponent();

            //if (Cef.IsInitialized == false) {
            //Cef.Initialize(new CefSettings());
            //}
            StatesArray.PopulateArray();
            
            LoadTemplate("Resources/ReportBasic.txt");
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

        /// <summary>
        /// Updates the DataGridView (Users) with any new information in the DB, as
        /// the DataGridView does not do so automatically
        /// </summary>
        public void SetView () {

            System.Data.SQLite.SQLiteConnection con = new System.Data.SQLite.SQLiteConnection("Data Source=" + DB_Connection + ";Version=3;");
            DataSet dataSet = new DataSet();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter("SELECT * FROM Members", con);
            dataAdapter.Fill(dataSet);

            Users.ItemsSource = dataSet.Tables[0].DefaultView;
            con.Close();
            GC.Collect();

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
                if ((dbStream = saveFileDialog.OpenFile()) != null) {

                    if (File.Exists(saveFileDialog.FileName)) {
                        // !!! Must close the stream
                        dbStream.Close();

                        // Have the connection string available for .NET SQLite
                        DB_Connection = saveFileDialog.FileName;
                        // Update the Data Grid View (Users)
                        SetView();
                    }

                }
            }
        }

        private void SaveMenuItem_Click (object sender, RoutedEventArgs e) {
            Stream dbStream;
            OpenFileDialog saveFileDialog = new OpenFileDialog();

            saveFileDialog.Filter = "SQLite files (*.sqlite)|*.sqlite";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == true) {
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

        private void Quick_Click (object sender, RoutedEventArgs e) {
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

            // Replace the 'variable' sections of the HTML
            String revisedTemplate;
            revisedTemplate = Template.Replace("#TITLE", "Quick Report");
            revisedTemplate = Template.Replace("@BODY", rp.ToString());
            SetHTMLView(revisedTemplate);
            // DEBUG: Clipboard.SetText(rp.ToString());
        }

        private void LoadTemplate_Click (object sender, RoutedEventArgs e) {
            String file = GetFile("txt");
            LoadTemplate(file);
        }

        private void NewTemplate_Click (object sender, RoutedEventArgs e) {

            NewTemplate dialog = new Design.NewTemplate();
            if (dialog.ShowDialog() == true) {
                SetTemplate(dialog.NewTemplateText.Text);
            }
        }

        private void Print_Click (object sender, RoutedEventArgs e) {
            // Send the Print command to WebBrowser
            mshtml.IHTMLDocument2 doc = HTMLViewer.Document as mshtml.IHTMLDocument2;
            doc.execCommand("Print", true, null);
        }

        private void SaveAs_Click (object sender, RoutedEventArgs e) {
            dynamic doc = HTMLViewer.Document;
            var htmlText = doc.documentElement.InnerHtml;
            String f = SaveFile(htmlText);
        }

        private void Add_Click (object sender, RoutedEventArgs e) {
            NewEntry dialog = new Design.NewEntry();

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
        }
    }
}