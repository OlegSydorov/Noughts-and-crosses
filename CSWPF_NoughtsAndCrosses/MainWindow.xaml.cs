using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace CSWPF_NoughtsAndCrosses
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ImageBrush ib1, ib2, ib3;
        bool youStart;
        bool playReady;
        Dictionary<int, string> matrix;
        Dictionary<int, Rectangle> figures;
        int move;
       // Dictionary<int, (string, string, string)> lines;
        List<Line> lines;
        public MainWindow()
        {
           
            InitializeComponent();

            playReady = false;
            move = 0;
           // lines = new Dictionary<int, (string, string, string)>();
            lines = new List<Line>();
            matrix = new Dictionary<int, string>();
            figures = new Dictionary<int, Rectangle>();
            matrix.Add(0, null);
            figures.Add(0, _00);
            matrix.Add(1, null);
            figures.Add(1, _01);
            matrix.Add(2, null);
            figures.Add(2, _02);
            matrix.Add(10, null);
            figures.Add(10, _10);
            matrix.Add(11, null);
            figures.Add(11, _11);
            matrix.Add(12, null);
            figures.Add(12, _12);
            matrix.Add(20, null);
            figures.Add(20, _20);
            matrix.Add(21, null);
            figures.Add(21, _21);
            matrix.Add(22, null);
            figures.Add(22, _22);


            youStart = true;

            ib1 = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"cross.PNG", UriKind.Relative)) };
            ib2 = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"nought.PNG", UriKind.Relative)) };
            ib3 = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"win1.png", UriKind.Relative)) };

            StartWindow sWin = new StartWindow();
            sWin.OnStartSelected += Form_OnStartSelected;

            bool result = (bool)sWin.ShowDialog();
        }

        private void Form_OnStartSelected(object sender, bool x)
        {
            youStart = (x) ? true : false;

            if (!youStart) ComputerMove();
            //else MessageBox.Show("Make your move!");
            playReady = true;

        }
        private void rectangleMouseDown(object sender, RoutedEventArgs e)
        {
            if (playReady == true)
            {
                int cellNumber = Grid.GetRow((UIElement)sender) * 10 + Grid.GetColumn((UIElement)sender);
                if (matrix[cellNumber] == null)
                {
                    ((Rectangle)sender).Fill = (youStart) ? ib1 : ib2;
                    matrix[cellNumber] = (youStart) ? "cross" : "nought";
                    move++;
                    playReady = false;
                    if (!WinCheck()) ComputerMove();
                }
            }
        }

        private int ThreatCheck(string filler)
        {
            List<(int, string)> candidates = new List<(int, string)>();
            // rows check
            //first row check
            if (matrix[0] == matrix[1] && matrix[0]!=null && matrix[2] == null)
            {
               candidates.Add((2, matrix[0]));
            }
            if (matrix[1] == matrix[2] && matrix[1] != null && matrix[0] == null)
            {
                candidates.Add((0, matrix[1]));
            }
            if (matrix[0] == matrix[2] && matrix[0] != null && matrix[1] == null)
            {
                candidates.Add((1, matrix[0]));
            }
            //second row check
            if (matrix[11] == matrix[12] && matrix[11] != null && matrix[10] == null)
            {
                candidates.Add((10, matrix[11]));
            }
            if (matrix[10] == matrix[12] && matrix[10] != null && matrix[11] == null)
            {
                candidates.Add((11, matrix[10]));
            }
            if (matrix[10] == matrix[11] && matrix[10] != null && matrix[12] == null)
            {
                candidates.Add((12, matrix[10]));
            }
            //third row check
            if (matrix[21] == matrix[22] && matrix[21] != null && matrix[20] == null)
            {
                candidates.Add((20, matrix[21]));
            }
             if (matrix[20] == matrix[22] && matrix[20] != null && matrix[21] == null)
            {
                candidates.Add((21, matrix[20]));
            }
             if (matrix[20] == matrix[21] && matrix[20] != null && matrix[22] == null)
            {
                candidates.Add((22, matrix[20]));
            }
            //columns check
            //first column check
             if (matrix[10] == matrix[20] && matrix[10] != null && matrix[0] == null)
            {
                candidates.Add((0, matrix[10]));
            }
             if (matrix[0] == matrix[20] && matrix[0] != null && matrix[10] == null)
            {
                candidates.Add((10, matrix[0]));
            }
             if (matrix[0] == matrix[10] && matrix[0] != null && matrix[20] == null)
            {
                candidates.Add((20, matrix[0]));
            }
            //second column check
             if (matrix[11] == matrix[21] && matrix[11] != null && matrix[1] == null)
            {
                candidates.Add((1, matrix[11]));
            }
             if (matrix[1] == matrix[21] && matrix[1] != null && matrix[11] == null)
            {
                candidates.Add((11, matrix[1]));
            }
             if (matrix[1] == matrix[11] && matrix[1] != null && matrix[21] == null)
            {
                candidates.Add((21, matrix[1]));
            }
            //third column check
             if (matrix[12] == matrix[22] && matrix[12] != null && matrix[2] == null)
            {
                candidates.Add((2, matrix[12]));
            }
             if (matrix[2] == matrix[22] && matrix[2] != null && matrix[12] == null)
            {
                candidates.Add((12, matrix[2]));
            }
             if (matrix[2] == matrix[12] && matrix[12] != null && matrix[22] == null)
            {
                candidates.Add((22, matrix[2]));
            }
            //diagonals check
            //right to left diagonal check
             if (matrix[11] == matrix[20] && matrix[11] != null && matrix[2] == null)
            {
                candidates.Add((2, matrix[11]));
            }
             if (matrix[2] == matrix[20] && matrix[2] != null && matrix[11] == null)
            {
                candidates.Add((11, matrix[2]));
            }
             if (matrix[2] == matrix[11] && matrix[11] != null && matrix[20] == null)
            {
                candidates.Add((20, matrix[2]));
            }
            //left to right diagonal check
             if (matrix[11] == matrix[22] && matrix[11] != null && matrix[0] == null)
            {
                candidates.Add((0, matrix[11]));
            }
             if (matrix[0] == matrix[22] && matrix[0] != null && matrix[11] == null)
            {
                candidates.Add((11, matrix[0]));
            }
             if (matrix[0] == matrix[11] && matrix[0] != null && matrix[22] == null)
            {
                candidates.Add((22, matrix[0]));
            }
            int n = 0;
            foreach (var d in candidates)
            {
                if (d.Item2 == filler)
                {
                    n++;
                    return d.Item1;
                }
            }
            if (n == 0)
            {
                foreach (var d in candidates)
                {
                    if (d.Item2 != filler)
                    {
                        return d.Item1;
                    }
                }
            }

            return 99;
        }
        private void LineList()
        {
            lines.Clear();
            lines.Add(new Line(1, 0, matrix[0], 1, matrix[1], 2, matrix[2]));
            lines.Add(new Line(2,10,  matrix[10], 11, matrix[11], 12, matrix[12]));
            lines.Add(new Line(3, 20, matrix[20], 21, matrix[21], 22, matrix[22]));
            lines.Add(new Line(4, 0, matrix[0], 10, matrix[10], 20, matrix[20]));
            lines.Add(new Line(5, 10, matrix[1], 11, matrix[11], 21, matrix[21]));
            lines.Add(new Line(6, 2, matrix[2], 12, matrix[12], 22, matrix[22]));
            lines.Add(new Line (7, 0, matrix[0], 11, matrix[11], 22, matrix[22]));
            lines.Add(new Line(8, 2, matrix[2], 11, matrix[11], 20, matrix[20]));
        }

        private int BestMoveCheck(string xFiller)
        {           
            List<Line> nullLines = new List<Line>();
            
            foreach (Line l in lines) if (l.NullCount == 2) nullLines.Add(l);

            List<(int, string)> candidates = new List<(int, string)>();

            if (nullLines.Count > 1)
            {
                foreach (Line l in nullLines)
                {
                    foreach (var d in l.Elements)
                    {
                        if (d.Value == null)
                        {
                            foreach (Line l1 in nullLines)
                            {
                                if (l1.Name == l.Name)
                                { continue; }
                                else
                                {
                                    if (l.Filler == l1.Filler)
                                    {
                                        foreach (var d1 in l1.Elements)
                                        {
                                            if (d1.Key == d.Key && d1.Value == d.Value)
                                            {
                                                candidates.Add((d1.Key, l1.Filler));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            int n = 0;
            foreach (var d in candidates)
            {
                if (d.Item2 == xFiller)
                {
                    n++;
                    return d.Item1;
                }
            }
            if (n == 0)
            {
                foreach (var d in candidates)
                {
                    if (d.Item2 != xFiller)
                    {
                        return d.Item1;
                    }
                }
            }

            return 99;
        }
   

        private int FreeMove(string filler)
        {
            foreach (var d in matrix)
            {
                if (d.Value == null) return d.Key;                
            }
            return 99;
        }
        private bool WinCheck()
        {
            if (matrix[0] == matrix[1] && matrix[1] == matrix[2] && matrix[0] != null)
            {
                _00.Fill = ib3;
                _01.Fill = ib3;
                _02.Fill = ib3;

                WinWindow wWin = new WinWindow(youStart, matrix[0]);
                wWin.OnContinueSelected += Form_OnContinueSelected;
                bool result = (bool)wWin.ShowDialog();
                return true;
            }
            else if (matrix[0] == matrix[11] && matrix[11] == matrix[22] && matrix[0] != null)
            {
                _00.Fill = ib3;
                _11.Fill = ib3;
                _22.Fill = ib3;

                WinWindow wWin = new WinWindow(youStart, matrix[0]);
                wWin.OnContinueSelected += Form_OnContinueSelected;
                bool result = (bool)wWin.ShowDialog();
                return true;
            }
            else if (matrix[0] == matrix[10] && matrix[10] == matrix[20] && matrix[0] != null)
            {
                _00.Fill = ib3;
                _10.Fill = ib3;
                _20.Fill = ib3;

                WinWindow wWin = new WinWindow(youStart, matrix[0]);
                wWin.OnContinueSelected += Form_OnContinueSelected;
                bool result = (bool)wWin.ShowDialog();
                return true;
            }
            else if (matrix[10] == matrix[11] && matrix[11] == matrix[12] && matrix[10] != null)
            {
                _10.Fill = ib3;
                _11.Fill = ib3;
                _12.Fill = ib3;

                WinWindow wWin = new WinWindow(youStart, matrix[10]);
                wWin.OnContinueSelected += Form_OnContinueSelected;
                bool result = (bool)wWin.ShowDialog();
                return true;
            }
            else if (matrix[20] == matrix[21] && matrix[21] == matrix[22] && matrix[20] != null)
            {
                _20.Fill = ib3;
                _21.Fill = ib3;
                _22.Fill = ib3;

                WinWindow wWin = new WinWindow(youStart, matrix[20]);
                wWin.OnContinueSelected += Form_OnContinueSelected;
                bool result = (bool)wWin.ShowDialog();
                return true;
            }
            else if (matrix[1] == matrix[11] && matrix[11] == matrix[21] && matrix[1] != null)
            {
                _01.Fill = ib3;
                _11.Fill = ib3;
                _21.Fill = ib3;

                WinWindow wWin = new WinWindow(youStart, matrix[1]);
                wWin.OnContinueSelected += Form_OnContinueSelected;
                bool result = (bool)wWin.ShowDialog();
                return true;
            }
            else if (matrix[2] == matrix[12] && matrix[12] == matrix[22] && matrix[2] != null)
            {
                _02.Fill = ib3;
                _12.Fill = ib3;
                _22.Fill = ib3;

                WinWindow wWin = new WinWindow(youStart, matrix[2]);
                wWin.OnContinueSelected += Form_OnContinueSelected;
                bool result = (bool)wWin.ShowDialog();
                return true;
            }
            else if (matrix[2] == matrix[11] && matrix[11] == matrix[20] && matrix[2] != null)
            {
                _02.Fill = ib3;
                _11.Fill = ib3;
                _20.Fill = ib3;

                WinWindow wWin = new WinWindow(youStart, matrix[02]);
                wWin.OnContinueSelected += Form_OnContinueSelected;
                bool result = (bool)wWin.ShowDialog();
                return true;
            }
            else if (move == 9)
            {
                WinWindow wWin = new WinWindow(youStart, "draw");
                wWin.OnContinueSelected += Form_OnContinueSelected;
                bool result = (bool)wWin.ShowDialog();
                return true;
            }
            return false;
        }

        private void Form_OnContinueSelected(object sender, bool x)
        {
            if (x == true)
            {
               
                matrix.Clear();
                matrix.Add(0, null);
                matrix.Add(1, null);
                matrix.Add(2, null);
                matrix.Add(10, null);
                matrix.Add(11, null);
                matrix.Add(12, null);
                matrix.Add(20, null);
                matrix.Add(21, null);
                matrix.Add(22, null);
                _00.Fill = new SolidColorBrush(Colors.White);
                _01.Fill = new SolidColorBrush(Colors.White);
                _02.Fill = new SolidColorBrush(Colors.White);
                _10.Fill = new SolidColorBrush(Colors.White);
                _11.Fill = new SolidColorBrush(Colors.White);
                _12.Fill = new SolidColorBrush(Colors.White);
                _20.Fill = new SolidColorBrush(Colors.White);
                _21.Fill = new SolidColorBrush(Colors.White);
                _22.Fill = new SolidColorBrush(Colors.White);
                move = 0;
                StartWindow ssWin = new StartWindow();
                ssWin.OnStartSelected += Form_OnStartSelected;
                bool result = (bool)ssWin.ShowDialog();
            }
            else this.Close();            
        }





        private void ComputerMove()
        {
            playReady = true;
            switch (move)
            {
                case 0:
                    {
                        Random rand = new Random();
                        int z = rand.Next(1, 10);
                        switch (z)
                        {
                            case 1:
                                {
                                    _00.Fill = ib1;
                                    matrix[0] = "cross";
                                    break;
                                }
                            case 2:
                                {
                                    _01.Fill = ib1;
                                    matrix[1] = "cross";
                                    break;
                                }
                            case 3:
                                {
                                    _02.Fill = ib1;
                                    matrix[2] = "cross";
                                    break;
                                }
                            case 4:
                                {
                                    _10.Fill = ib1;
                                    matrix[10] = "cross";
                                    break;
                                }
                            case 5:
                                {
                                    _11.Fill = ib1;
                                    matrix[11] = "cross";
                                    break;
                                }
                            case 6:
                                {
                                    _12.Fill = ib1;
                                    matrix[12] = "cross";
                                    break;
                                }
                            case 7:
                                {
                                    _20.Fill = ib1;
                                    matrix[20] = "cross";
                                    break;
                                }
                            case 8:
                                {
                                    _21.Fill = ib1;
                                    matrix[21] = "cross";
                                    break;
                                }
                            case 9:
                                {
                                    _22.Fill = ib1;
                                    matrix[22] = "cross";
                                    break;
                                }
                        }
                        move++;
                        break;
                    }
                case 1:
                    {
                        if (matrix[0] == "cross" || matrix[2] == "cross" || matrix[20] == "cross" || matrix[22] == "cross")
                        {
                            matrix[11] = "nought";
                            _11.Fill = ib2;
                        }
                        else if (matrix[11] == "cross")
                        {
                            Random rand = new Random();
                            int z = rand.Next(1, 5);
                            switch (z)
                            {
                                case 1:
                                    {
                                        _00.Fill = ib2;
                                        matrix[0] = "nought";
                                        break;
                                    }
                                case 2:
                                    {
                                        _02.Fill = ib2;
                                        matrix[2] = "nought";
                                        break;
                                    }
                                case 3:
                                    {
                                        _20.Fill = ib2;
                                        matrix[20] = "nought";
                                        break;
                                    }
                                case 4:
                                    {
                                        _22.Fill = ib2;
                                        matrix[22] = "nought";
                                        break;
                                    }
                            }

                        }
                        else if (matrix[1] == "cross")
                        {
                            Random rand = new Random();
                            int z = rand.Next(1, 5);
                            switch (z)
                            {
                                case 1:
                                    {
                                        _00.Fill = ib2;
                                        matrix[0] = "nought";
                                        break;
                                    }
                                case 2:
                                    {
                                        _02.Fill = ib2;
                                        matrix[2] = "nought";
                                        break;
                                    }
                                case 3:
                                    {                                       
                                        _11.Fill = ib2;
                                        matrix[11] = "nought";
                                        break;
                                    }
                                case 4:
                                    {
                                        _21.Fill = ib2;
                                        matrix[21] = "nought";
                                        break;
                                    }
                            }
                                   
                        }
                        else if (matrix[10] == "cross")
                        {
                            Random rand = new Random();
                            int z = rand.Next(1, 5);
                            switch (z)
                            {
                                case 1:
                                    {
                                        _00.Fill = ib2;
                                        matrix[0] = "nought";
                                        break;
                                    }
                                case 2:
                                    {                                       
                                        _12.Fill = ib2;
                                        matrix[12] = "nought";
                                        break;
                                    }
                                case 3:
                                    {
                                        _20.Fill = ib2;
                                        matrix[20] = "nought";
                                        break;
                                    }
                                case 4:
                                    {
                                        _11.Fill = ib2;
                                        matrix[11] = "nought";
                                        break;
                                    }                                
                            }
                        }
                        else if (matrix[12] == "cross")
                        {
                            Random rand = new Random();
                            int z = rand.Next(1, 5);
                            switch (z)
                            {
                                case 1:
                                    {                                       
                                        _02.Fill = ib2;
                                        matrix[2] = "nought";
                                        break;
                                    }
                                case 2:
                                    {
                                        _10.Fill = ib2;
                                        matrix[10] = "nought";
                                        break;
                                    }
                                case 3:
                                    {
                                        _22.Fill = ib2;
                                        matrix[22] = "nought";
                                        break;
                                    }
                                case 4:
                                    {
                                        _11.Fill = ib2;
                                        matrix[11] = "nought";
                                        break;
                                    }                               
                            }
                        }
                        else if (matrix[21] == "cross")
                        {
                            Random rand = new Random();
                            int z = rand.Next(1, 5);
                            switch (z)
                            {
                                case 1:
                                    {
                                        _01.Fill = ib2;
                                        matrix[1] = "nought";
                                        break;
                                    }
                                case 2:
                                    {
                                        _22.Fill = ib2;
                                        matrix[22] = "nought";
                                        break;
                                    }
                                case 3:
                                    {                                       
                                        _11.Fill = ib2;
                                        matrix[11] = "nought";
                                        break;
                                    }
                                case 4:
                                    {
                                        _20.Fill = ib2;
                                        matrix[20] = "nought";
                                        break;
                                    }
                            }
                        }
                        move++;
                        break;
                    }
                case 2:
                    {
                        if (matrix[0] == "cross")
                        {
                            if (matrix[2] == "nought" || matrix[1] == "nought" || matrix[10] == "nought" || matrix[20] == "nought" || matrix[11] == "nought")
                            {
                                matrix[22] = "cross";
                                _22.Fill = ib1;
                            }
                            else if (matrix[12] == "nought" || matrix[22] == "nought" || matrix[21] == "nought")
                            {
                                matrix[2] = "cross";
                                _02.Fill = ib1;
                            }
                        }
                        else if (matrix[2] == "cross")
                        {
                            if (matrix[22] == "nought" || matrix[12] == "nought" || matrix[1] == "nought" || matrix[0] == "nought" || matrix[11] == "nought")
                            {
                                matrix[20] = "cross";
                                _20.Fill = ib1;
                            }
                            else if (matrix[20] == "nought" || matrix[10] == "nought" || matrix[21] == "nought")
                            {
                                matrix[0] = "cross";
                                _00.Fill = ib1;
                            }
                        }
                        else if (matrix[22] == "cross")
                        {
                            if (matrix[20] == "nought" || matrix[21] == "nought" || matrix[12] == "nought" || matrix[2] == "nought" || matrix[11] == "nought")
                            {
                                matrix[0] = "cross";
                                _00.Fill = ib1;
                            }
                            else if (matrix[0] == "nought" || matrix[1] == "nought" || matrix[10] == "nought")
                            {
                                matrix[20] = "cross";
                                _20.Fill = ib1;
                            }

                        }
                        else if (matrix[20] == "cross")
                        {
                            if (matrix[21] == "nought" || matrix[22] == "nought" || matrix[10] == "nought" || matrix[0] == "nought" || matrix[11] == "nought")
                            {
                                matrix[2] = "cross";
                                _02.Fill = ib1;
                            }
                            else if (matrix[2] == "nought" || matrix[1] == "nought" || matrix[12] == "nought")
                            {
                                matrix[0] = "cross";
                                _00.Fill = ib1;
                            }
                        }

                        else if (matrix[11] == "cross")
                        {
                            if (matrix[0] == "nought" || matrix[2] == "nought" || matrix[20] == "nought" || matrix[22] == "nought")
                            {
                                Random rand = new Random();
                                int z = rand.Next(0, 8);
                                switch (z)
                                {
                                    case 0:
                                        {
                                            if (matrix[0] != "nought")
                                            {
                                                _00.Fill = ib1;
                                                matrix[0] = "cross";
                                            }
                                            else
                                            {
                                                _02.Fill = ib1;
                                                matrix[2] = "cross";
                                            }
                                            break;
                                        }
                                    case 1:
                                        {
                                            _01.Fill = ib1;
                                            matrix[1] = "cross";
                                            break;
                                        }
                                    case 2:
                                        {
                                            if (matrix[2] != "nought")
                                            {
                                                _02.Fill = ib1;
                                                matrix[2] = "cross";
                                            }
                                            else
                                            {
                                                _00.Fill = ib1;
                                                matrix[0] = "cross";
                                            }
                                            break;
                                        }
                                    case 3:
                                        {
                                            _10.Fill = ib1;
                                            matrix[10] = "cross";
                                            break;
                                        }
                                    case 4:
                                        {
                                            _12.Fill = ib1;
                                            matrix[12] = "cross";
                                            break;
                                        }
                                    case 5:
                                        {
                                            if (matrix[20] != "nought")
                                            {
                                                _20.Fill = ib1;
                                                matrix[20] = "cross";
                                            }
                                            else
                                            {
                                                _22.Fill = ib1;
                                                matrix[22] = "cross";
                                            }
                                            break;
                                        }
                                    case 6:
                                        {
                                            _21.Fill = ib1;
                                            matrix[21] = "cross";
                                            break;
                                        }
                                    case 7:
                                        {
                                            if (matrix[22] != "nought")
                                            {
                                                _22.Fill = ib1;
                                                matrix[22] = "cross";
                                            }
                                            else
                                            {
                                                _20.Fill = ib1;
                                                matrix[20] = "cross";
                                            }
                                            break;
                                        }

                                }
                            }

                            else if (matrix[1] == "nought" || matrix[10] == "nought" || matrix[12] == "nought" || matrix[21] == "nought")
                            {
                                Random rand = new Random();
                                int z = rand.Next(1, 5);
                                switch (z)
                                {
                                    case 1:
                                        {
                                            _00.Fill = ib1;
                                            matrix[0] = "cross";
                                            break;
                                        }
                                    case 2:
                                        {
                                            if (matrix[2] != "nought")
                                            {
                                                _02.Fill = ib1;
                                                matrix[2] = "cross";
                                            }
                                            break;
                                        }
                                    case 3:
                                        {
                                            _20.Fill = ib1;
                                            matrix[20] = "cross";
                                            break;
                                        }
                                    case 4:
                                        {
                                            _22.Fill = ib1;
                                            matrix[22] = "cross";
                                            break;
                                        }
                                }
                            }
                        }
                        else if ((matrix[10] == "cross" || matrix[1] == "cross" || matrix[12] == "cross" || matrix[21] == "cross")
                                  && matrix[11] == "nought")
                        {
                            Random rand = new Random();
                            int z = rand.Next(0, 6);
                            switch (z)
                            {
                                case 0:
                                    {
                                        _00.Fill = ib1;
                                        matrix[0] = "cross";
                                        break;

                                    }
                                case 1:
                                    {
                                        if (matrix[1] != "cross" && matrix[21] != "cross")
                                        {
                                            _01.Fill = ib1;
                                            matrix[1] = "cross";
                                        }
                                        else
                                        {
                                            _10.Fill = ib1;
                                            matrix[10] = "cross";
                                        }
                                        break;
                                    }
                                case 2:
                                    {
                                        _02.Fill = ib1;
                                        matrix[2] = "cross";
                                        break;
                                    }
                                case 3:
                                    {
                                        if (matrix[10] != "cross" && matrix[12] != "cross")
                                        {
                                            _10.Fill = ib1;
                                            matrix[10] = "cross";
                                        }
                                        else
                                        {
                                            _01.Fill = ib1;
                                            matrix[1] = "cross";
                                        }
                                        break;
                                    }
                                case 4:
                                    {
                                        _20.Fill = ib1;
                                        matrix[20] = "cross";
                                        break;
                                    }
                                case 5:
                                    {
                                        if (matrix[10] != "cross" && matrix[12] != "cross")
                                        {
                                            _12.Fill = ib1;
                                            matrix[12] = "cross";
                                        }
                                        else
                                        {
                                            _21.Fill = ib1;
                                            matrix[21] = "cross";
                                        }
                                        break;
                                    }
                                case 6:
                                    {
                                        _22.Fill = ib1;
                                        matrix[22] = "cross";
                                        break;
                                    }
                                case 7:
                                    {
                                        if (matrix[1] != "cross" && matrix[21] != "cross")
                                        {
                                            _21.Fill = ib1;
                                            matrix[21] = "cross";
                                        }
                                        else
                                        {
                                            _12.Fill = ib1;
                                            matrix[12] = "cross";
                                        }
                                        break;
                                    }
                            }
                        }
                        else if (matrix[10] == "cross" &&
                                (matrix[0] == "nought" || matrix[1] == "nought" || matrix[2] == "nought" || matrix[20] == "nought" || matrix[21] == "nought" || matrix[22] == "nought"))
                        {
                            Random rand = new Random();
                            int z = rand.Next(1, 3);
                            switch (z)
                            {
                                case 1:
                                    {
                                        _11.Fill = ib1;
                                        matrix[11] = "cross";
                                        break;

                                    }
                                case 2:
                                    {
                                        if (matrix[0] == "nought" || matrix[1] == "nought" || matrix[2] == "nought")
                                        {
                                            _22.Fill = ib1;
                                            matrix[22] = "cross";
                                        }
                                        else if (matrix[20] == "nought" || matrix[21] == "nought" || matrix[22] == "nought")
                                        {
                                            _02.Fill = ib1;
                                            matrix[2] = "cross";
                                        }
                                        break;
                                    }
                            }
                        }
                        else if (matrix[1] == "cross" &&
                                (matrix[0] == "nought" || matrix[10] == "nought" || matrix[20] == "nought" || matrix[2] == "nought" || matrix[12] == "nought" || matrix[22] == "nought"))
                        {
                            Random rand = new Random();
                            int z = rand.Next(1, 3);
                            switch (z)
                            {
                                case 1:
                                    {
                                        _11.Fill = ib1;
                                        matrix[11] = "cross";
                                        break;

                                    }
                                case 2:
                                    {
                                        if (matrix[0] == "nought" || matrix[10] == "nought" || matrix[20] == "nought")
                                        {
                                            _22.Fill = ib1;
                                            matrix[22] = "cross";
                                        }
                                        else if (matrix[2] == "nought" || matrix[12] == "nought" || matrix[22] == "nought")
                                        {
                                            _20.Fill = ib1;
                                            matrix[20] = "cross";
                                        }
                                        break;
                                    }
                            }
                        }
                        else if (matrix[12] == "cross" &&
                                (matrix[0] == "nought" || matrix[2] == "nought" || matrix[3] == "nought" || matrix[20] == "nought" || matrix[21] == "nought" || matrix[22] == "nought"))
                        {
                            Random rand = new Random();
                            int z = rand.Next(1, 3);
                            switch (z)
                            {
                                case 1:
                                    {
                                        _11.Fill = ib1;
                                        matrix[11] = "cross";
                                        break;

                                    }
                                case 2:
                                    {
                                        if (matrix[0] == "nought" || matrix[2] == "nought" || matrix[3] == "nought")
                                        {
                                            _20.Fill = ib1;
                                            matrix[20] = "cross";
                                        }
                                        else if (matrix[20] == "nought" || matrix[21] == "nought" || matrix[22] == "nought")
                                        {
                                            _00.Fill = ib1;
                                            matrix[0] = "cross";
                                        }
                                        break;
                                    }
                            }
                        }
                        else if (matrix[21] == "cross" &&
                                (matrix[0] == "nought" || matrix[10] == "nought" || matrix[20] == "nought" || matrix[2] == "nought" || matrix[12] == "nought" || matrix[22] == "nought"))
                        {
                            Random rand = new Random();
                            int z = rand.Next(1, 3);
                            switch (z)
                            {
                                case 1:
                                    {
                                        _11.Fill = ib1;
                                        matrix[11] = "cross";
                                        break;

                                    }
                                case 2:
                                    {
                                        if (matrix[0] == "nought" || matrix[10] == "nought" || matrix[20] == "nought")
                                        {
                                            _02.Fill = ib1;
                                            matrix[2] = "cross";
                                        }
                                        else if (matrix[2] == "nought" || matrix[12] == "nought" || matrix[22] == "nought")
                                        {
                                            _00.Fill = ib1;
                                            matrix[00] = "cross";
                                        }
                                        break;
                                    }
                            }
                        }
                        else if ((matrix[1] == "cross" && matrix[21] == "nought") ||
                                  (matrix[10] == "cross" && matrix[12] == "nought") ||
                                  (matrix[12] == "cross" && matrix[10] == "nought") ||
                                  (matrix[21] == "cross" && matrix[1] == "nought"))
                        {
                            Random rand = new Random();
                            int z = rand.Next(1, 6);
                            switch (z)
                            {
                                case 1:
                                    {
                                        _11.Fill = ib1;
                                        matrix[11] = "cross";
                                        break;

                                    }
                                case 2:
                                    {
                                        _00.Fill = ib1;
                                        matrix[0] = "cross";
                                        break;
                                    }
                                case 3:
                                    {
                                        _02.Fill = ib1;
                                        matrix[2] = "cross";
                                        break;
                                    }
                                case 4:
                                    {
                                        _20.Fill = ib1;
                                        matrix[20] = "cross";
                                        break;
                                    }
                                case 5:
                                    {
                                        _22.Fill = ib1;
                                        matrix[22] = "cross";
                                        break;
                                    }
                            }
                        }
                        move++;
                        break;
                    }
                case 3:
                    {
                        if ((matrix[0] == "cross" && matrix[22] == "cross" && matrix[11] == "nought") ||
                             (matrix[2] == "cross" && matrix[20] == "cross" && matrix[11] == "nought"))
                        {
                            Random rand = new Random();
                            int z = rand.Next(1, 5);
                            switch (z)
                            {
                                case 1:
                                    {
                                        _01.Fill = ib2;
                                        matrix[1] = "nought";
                                        break;
                                    }
                                case 2:
                                    {
                                        _10.Fill = ib2;
                                        matrix[10] = "nought";
                                        break;
                                    }
                                case 3:
                                    {
                                        _12.Fill = ib2;
                                        matrix[12] = "nought";
                                        break;
                                    }
                                case 4:
                                    {
                                        _21.Fill = ib2;
                                        matrix[21] = "nought";
                                        break;
                                    }

                            }
                        }
                        else
                        {
                            int tCheck = ThreatCheck("nought");
                            if (tCheck != 99)
                            {
                                matrix[tCheck] = "nought";
                                figures[tCheck].Fill = ib2;
                            }
                            else
                            {
                                LineList();
                                int bmCheck = BestMoveCheck("nought");
                                if (bmCheck != 99)
                                {
                                    matrix[bmCheck] = "nought";
                                    figures[bmCheck].Fill = ib2;
                                }
                                else
                                {
                                    int fCheck = FreeMove("nought");
                                    if (fCheck != 99)
                                    {
                                        matrix[fCheck] = "nought";
                                        figures[fCheck].Fill = ib2;
                                    }
                                }
                            }
                        }
                        move++;
                        break;
                    }
                case 4:
                    {                        
                        int tCheck = ThreatCheck("cross");
                        if (tCheck != 99)
                        {
                            matrix[tCheck] = "cross";
                            figures[tCheck].Fill = ib1;
                        }
                        else
                        {
                            LineList();
                            int bmCheck = BestMoveCheck("cross");
                            if (bmCheck != 99)
                            {
                                matrix[bmCheck] = "cross";
                                figures[bmCheck].Fill = ib1;
                            }
                            else
                            {
                                int fCheck = FreeMove("cross");
                                if (fCheck != 99)
                                {
                                    matrix[fCheck] = "cross";
                                    figures[fCheck].Fill = ib1;
                                }
                            }
                        }
                        move++;
                        break;
                    }
                case 5:
                    {
                        int tCheck = ThreatCheck("nought");
                        if (tCheck != 99)
                        {
                            matrix[tCheck] = "nought";
                            figures[tCheck].Fill = ib2;
                        }
                        else
                        {
                            LineList();
                            int bmCheck = BestMoveCheck("nought");
                            if (bmCheck != 99)
                            {
                                matrix[bmCheck] = "nought";
                                figures[bmCheck].Fill = ib2;
                            }
                            else
                            {
                                int fCheck = FreeMove("nought");
                                if (fCheck != 99)
                                {
                                    matrix[fCheck] = "nought";
                                    figures[fCheck].Fill = ib2;
                                }
                            }
                        }
                        move++;
                        break;
                    }
                case 6:
                    {
                        int tCheck = ThreatCheck("cross");
                        if (tCheck != 99)
                        {
                            matrix[tCheck] = "cross";
                            figures[tCheck].Fill = ib1;
                        }
                        else
                        {
                            LineList();
                            int bmCheck = BestMoveCheck("cross");
                            if (bmCheck != 99)
                            {
                                matrix[bmCheck] = "cross";
                                figures[bmCheck].Fill = ib1;
                            }
                            else
                            {
                                int fCheck = FreeMove("cross");
                                if (fCheck != 99)
                                {
                                    matrix[fCheck] = "cross";
                                    figures[fCheck].Fill = ib1;
                                }
                            }
                        }
                        move++;
                        break;
                    }
                case 7:
                    {
                        int tCheck = ThreatCheck("nought");
                        if (tCheck != 99)
                        {
                            matrix[tCheck] = "nought";
                            figures[tCheck].Fill = ib2;
                        }
                        else
                        {
                            LineList();
                            int bmCheck = BestMoveCheck("nought");
                            if (bmCheck != 99)
                            {
                                matrix[bmCheck] = "nought";
                                figures[bmCheck].Fill = ib2;
                            }
                            else
                            {
                                int fCheck = FreeMove("nought");
                                if (fCheck != 99)
                                {
                                    matrix[fCheck] = "nought";
                                    figures[fCheck].Fill = ib2;
                                }
                            }
                        }
                        move++;
                        break;
                    }
                case 8:
                    {
                        int tCheck = ThreatCheck("cross");
                        if (tCheck != 99)
                        {
                            matrix[tCheck] = "cross";
                            figures[tCheck].Fill = ib1;
                        }
                        else
                        {
                            LineList();
                            int bmCheck = BestMoveCheck("cross");
                            if (bmCheck != 99)
                            {
                                matrix[bmCheck] = "cross";
                                figures[bmCheck].Fill = ib1;
                            }
                            else
                            {
                                int fCheck = FreeMove("cross");
                                if (fCheck != 99)
                                {
                                    matrix[fCheck] = "cross";
                                    figures[fCheck].Fill = ib1;
                                }
                            }
                        }
                        move++;
                        break;
                    }
            }
            bool ok= WinCheck() ;
        }
    }
}
