using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FBLA_Members {
    /// <summary>
    /// Interaction logic for Instructions.xaml
    /// </summary>
    public partial class Instructions : MetroWindow {
        Dictionary<string, string> q = new Dictionary<string, string>();
        public Instructions () {
            InitializeComponent();
            q.Add("Create a Database?", "CreateDB.txt");
            q.Add("Create a custom report?", "CustomReport.txt");
            q.Add("Generate a report?", "MakeReport.txt");
            q.Add("Open a Database?", "OpenDB.txt");
            q.Add("Add a member?", "AddMember.txt");
            FAQList.ItemsSource = q;
        }

        private void FAQList_SelectionChanged (object sender, SelectionChangedEventArgs e) {
            var comboBox = sender as ComboBox;

            string value = "";
            foreach(var i in e.AddedItems) {
                KeyValuePair<string, string> kvp = (KeyValuePair<string, string>)i;
                value = kvp.Key;
            }
            string file = File.ReadAllText("Resources/" + q[value]);
            FAQBrowser.NavigateToString(file);

        }
    }
}
