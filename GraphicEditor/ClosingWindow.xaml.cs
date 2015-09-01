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

namespace GraphicEditor
{
    /// <summary>
    /// Interaction logic for ClosingWindow.xaml
    /// </summary>
    public partial class ClosingWindow : Window
    {
        MainWindow mainWindow;

        public ClosingWindow(MainWindow main) : this()
        {
            this.mainWindow = main;
        }


        public ClosingWindow()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs r = new RoutedEventArgs(MenuItem.ClickEvent);
            mainWindow.saveFile.RaiseEvent(r);
            this.Close();
            mainWindow = null;
        }

        private void noButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow = null;
            this.Close();
        }
    }
}
