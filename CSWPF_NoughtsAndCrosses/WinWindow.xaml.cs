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

namespace CSWPF_NoughtsAndCrosses
{
    /// <summary>
    /// Interaction logic for WinWindow.xaml
    /// </summary>
    public delegate void ContinueSelect(object obj, bool select);
    public partial class WinWindow : Window
    {
        public event ContinueSelect OnContinueSelected;
        public WinWindow(bool youStart, string filler)
        {
            InitializeComponent();
            if (youStart == true && filler == "cross") textBox.Text = "You win!";
            else if (youStart == false && filler == "cross") textBox.Text = "You lose!";
            else if (youStart == false && filler == "nought") textBox.Text = "You win!";
            else if (youStart == true && filler == "nought") textBox.Text = "You lose!";
            else if (filler == "draw") textBox.Text = "It's a draw!";

        }

        private void _Yes_Click(object sender, RoutedEventArgs e)
        {
            bool select = true;
            OnContinueSelected?.Invoke(this, select);
            this.Close();
        }

        private void _No_Click(object sender, RoutedEventArgs e)
        {
            bool select = false;
            OnContinueSelected?.Invoke(this, select);
            this.Close();
        }
    }
}
