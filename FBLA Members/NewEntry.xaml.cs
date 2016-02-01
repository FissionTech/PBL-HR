using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for NewEntry.xaml
    /// </summary>
    public partial class NewEntry : MetroWindow {
        public NewEntry () {
            InitializeComponent();

            StateCombo.ItemsSource = StatesArray.Abbreviations().ToList<string>();
            
        }

        private void OK_Click (object sender, RoutedEventArgs e) {
            bool value = true;
            foreach (TextBox tb in FindVisualChildren<TextBox>(this)) {
                if (String.IsNullOrEmpty(tb.Text) || String.IsNullOrWhiteSpace(tb.Text)) {
                    if(tb.Name != "PART_EditableTextBox") {
                        MessageBox.Show("Field " + tb.Name + " is empty. Please enter a valid string.");
                        value = false;
                    }
                }
            }
            foreach (ComboBox cb in FindVisualChildren<ComboBox>(this)) {
                if (cb.SelectedItem == null) {
                    MessageBox.Show("Drop down " + cb.Name + " is empty. Please choose a value.");
                    value = false;
                }
            }

            // I know this looks like bad code but its necessary because of the way WPF works
            if(value == true) {
                DialogResult = value;
            }

            // We can't have the dialog result set to false, or else it closes the dialog
        }

        public static IEnumerable<T> FindVisualChildren<T> (DependencyObject depObj) where T : DependencyObject {
            if (depObj != null) {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++) {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T) {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child)) {
                        yield return childOfChild;
                    }
                }
            }
        }

        private void Cancel_Click (object sender, RoutedEventArgs e) {
            DialogResult = false;
        }
    }
}
