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
    /// Interaction logic for NewTemplate.xaml
    /// </summary>
    public partial class NewTemplate : MetroWindow {
        
        public NewTemplate () {
            
            InitializeComponent();
            NewTemplateText.AcceptsReturn = true;
            NewTemplateText.AcceptsTab = true;
        }

        private void OK_Click (object sender, RoutedEventArgs e) {
            DialogResult = true;
        }
    }
}
