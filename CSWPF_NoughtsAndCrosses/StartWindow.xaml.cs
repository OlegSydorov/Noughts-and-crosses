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
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public delegate void StartSelect(object obj, bool youStart);
    public partial class StartWindow : Window
    {
        public event StartSelect OnStartSelected;
        bool closeFlag;
        bool repeatFlag;
        bool youStart;
        public StartWindow()
        {
            closeFlag = false;
            repeatFlag = false;
            youStart = false;
            InitializeComponent();
        }

        private void _okClick(object sender, RoutedEventArgs e)
        {
            if (closeFlag == false && repeatFlag == false)
            {
                if (_text.Text.Length != 0)
                {
                    bool isIntString = _text.Text.All(char.IsDigit);
                    if (isIntString)
                    {
                        int zzz = Convert.ToInt32(_text.Text);
                        if (zzz > 0 && zzz < 100)
                        {
                            Random rand = new Random();
                            int compfig = rand.Next(1, 100);
                            int playerFig = Convert.ToInt32(_text.Text);
                            if (compfig > playerFig)
                            {
                                _textButton.Content = "Computer selected figure " + compfig + ".\r\nIt is bigger than yours.\r\nTherefore, computer makes the first move.\r\nPress OK to continue.";
                                closeFlag = true;

                            }
                            else if (compfig < playerFig)
                            {
                                _textButton.Content = "Computer selected figure " + compfig + ".\r\nIt is smaller than yours.\r\nTherefore, You make the first move.\r\nPress OK to continue.";
                                closeFlag = true;
                                youStart = true;
                            }
                            else if (compfig == playerFig)
                            {
                                _textButton.Content = "Computer selected figure " + compfig + ".\r\nIt is the same as the one selected by You.\r\nMake your selection again.\r\nPress OK to continue.";
                                repeatFlag = true;
                            }
                        }
                        else
                        {
                            _textButton.Content = "You have not entered correct figure!\r\n Press OK to continue";
                            _text.Text = null;
                            repeatFlag = true;
                        }
                    }
                    else
                    {
                        _textButton.Content = "You have not entered correct sequence!\r\n Press OK to continue";
                        _text.Text = null;
                        repeatFlag = true;
                    }
                }
                else
                {
                    _textButton.Content = "You have not entered anything!\r\n Press OK to continue";
                    repeatFlag = true;
                }
            }
            else if (closeFlag == false && repeatFlag == true)
            {
                _textButton.Content = "To find out who starts\r\n - you or computer -\r\ntype a figure from 1 to 99 in the box below\r\nand\r\npress OK";
                _text.Text = null;
                repeatFlag = false;
            }
            else if (closeFlag == true && repeatFlag == false)
            {
                OnStartSelected?.Invoke(this, youStart);

                this.Close();
            }                
        }
    }
}
