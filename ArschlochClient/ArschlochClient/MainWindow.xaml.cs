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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ArschlochClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnRules_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Rules rulesWindow = new Rules();
            rulesWindow.Show();
            this.Close();
        }
        private void BtnJoin_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CardField cardFieldWindow = new CardField();
            cardFieldWindow.Show();
            this.Close();
        }
    }
}
