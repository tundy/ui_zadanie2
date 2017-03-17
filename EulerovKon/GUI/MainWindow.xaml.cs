using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using EulerovKon;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// Trieda obsahujúca algoritmus prehľadávania
        /// </summary>
        private readonly Search _search = new Search();

        /// <summary>
        /// Vykreslené riešenie
        /// </summary>
        private readonly List<Line> _lines = new List<Line>(400);

        /// <summary>
        /// Inicializuj GUI
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            ChessBoard.Width = WidthSlider.Value * 20;
            ChessBoard.Height = HeightSlider.Value * 20;
        }

        /// <summary>
        /// Aktualizuj šírku šachovnice
        /// </summary>
        private void WidthSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ChessBoard.Width = e.NewValue * 20;
        }

        /// <summary>
        /// Aktualizuj výšku šachovnice
        /// </summary>
        private void HeightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ChessBoard.Height = e.NewValue * 20;
        }

        /// <summary>
        /// Prekresli štvorčeky v šachovnici
        /// </summary>
        private void ChessBoard_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ChessBoard.Children.Clear();
            _lines.Clear();
            for (var x = 0; x < WidthSlider.Value; x++)
            {
                for (var y = 0; y < HeightSlider.Value; y++)
                {
                    if(((x & 1) == 1 && (y & 1) == 0) || ((x & 1) == 0 && (y & 1) == 1)) continue;
                    ChessBoard.Children.Add(new Rectangle { Width = 20, Height = 20, Fill = SystemColors.ControlBrush, Margin = new Thickness(x * 20, y * 20, 0, 0), VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left});
                }
            }
            if (AutoSearch.IsChecked == true)
                Generate();
        }

        readonly NumberFormatInfo _format = new NumberFormatInfo { NumberGroupSeparator = " " };

        /// <summary>
        /// Ošetri vstupy, zavolaj vyhľadávanie a vykresli cestu
        /// </summary>
        private void Generate()
        {
            // Clear Gui
            foreach (var line in _lines)
                ChessBoard.Children.Remove(line);
            _lines.Clear();

            // Ošetri vstup
            var x = int.Parse(Xvalue.Text) - 1;
            if (x < 0 || x >= (int) WidthSlider.Value)
            {
                MessageBox.Show("Neplatná hodnota pre X.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var y = int.Parse(Yvalue.Text) - 1;
            if (y < 0 || y >= (int)HeightSlider.Value)
            {
                MessageBox.Show("Neplatná hodnota pre Y.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var seconds = int.Parse(MaxSeconds.Text);
            if(seconds <= 0)
            {
                MessageBox.Show("Neplatný počet sekúnd.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Nájdi cestu
            var path = _search.Start((int)WidthSlider.Value, (int)HeightSlider.Value, x, y, seconds);

            // Vypíš štatistiku 
            TimeElapsed.Text = _search.TimeElapsed.ToString();
            Generated.Text = _search.Generated.ToString("n0", _format);
            Steps.Text = _search.Steps.ToString("n0", _format);
            Memory.Text = (_search.MaxMemory >> 10).ToString("n0", _format);

            // Update GUI
            if (path == null)
            {
                if (_search.TimedOut)
                    MessageBox.Show("Nepodarilo sa nájsť cestu v stanovenom čase.", "Vypršal čas", MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                else
                    MessageBox.Show("Riešenie pre daný vstup neexistuje.", "Chyba", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                return;
            }
            for (var i = 0; i < path.Length - 1;)
            {
                var ciara = new Line { X1 = path[i].Item1 * 20 + 10, Y1 = path[i].Item2 * 20 + 10, Stroke = SystemColors.ControlTextBrush };
                ++i;
                ciara.X2 = path[i].Item1 * 20 + 10;
                ciara.Y2 = path[i].Item2 * 20 + 10;
                ChessBoard.Children.Add(ciara);
                _lines.Add(ciara);
            }
        }

        /// <summary>
        /// Stlačenie tlačidla Hľadaj
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Generate();
        }

        /// <summary>
        /// Stlačenie tlačidla O programe
        /// </summary>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                $"Vytvoril Matúš Tundér na základe Warnsdorf pravidla (H. C. von Warnsdorf v roku 1823) pre školské účely (c) 2017{Environment.NewLine}Ikona programu je pod licencou Creative Commons, autor: https://www.fatcow.com",
                "O programe", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Zakáž Copy, Paste a Cut
        /// </summary>
        /// <param name="sender">UserControl na ktorom sa zakáže Copy, Paste a Cut</param>
        private void textBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Copy ||
                e.Command == ApplicationCommands.Cut ||
                e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Zisti či sa jedná o čislo
        /// </summary>
        /// <param name="text">Text, ktorý sa skontroluje</param>
        /// <returns>Akceptovateľnosť textu</returns>
        private static bool IsTextAllowed(string text)
        {
            uint x;
            return uint.TryParse(text, out x);
        }

        /// <summary>
        /// Zavolása pri pokuse o písania do Textboxu
        /// </summary>
        /// <param name="e">Argument textboxu</param>
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        /// <summary>
        /// Po načítaní okna spusti vyhľadávanie
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Generate();
        }
    }
}
