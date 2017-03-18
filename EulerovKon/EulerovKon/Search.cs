using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;

namespace EulerovKon
{
    /// <summary>
    ///     Algoritmus pre úlohu Eulerov kôň
    /// </summary>
    public class Search
    {
        #region Constructors

        /// <summary>
        ///     Inicializuje prehľadávanie
        /// </summary>
        public Search()
        {
            _timer.Elapsed += Timer_Elapsed;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Spusti prehľadávanie pre šachovnicu určenú vstupnými argumentami
        /// </summary>
        /// <param name="width">Šírka šachovnice</param>
        /// <param name="height">Výška šachovnice</param>
        /// <param name="x">Horizontálna začiatočná pozícia</param>
        /// <param name="y">Vertikálna začiatočná pozícia</param>
        /// <param name="maxSeconds">Maximálny počet sekúnd, ktorý môže hľadať cestu</param>
        /// <exception cref="ArgumentException" />
        /// <returns>Vráti cestu ako pole x,y suradníc alebo null ak cesta nebola nájdená</returns>
        public Tuple<int, int>[] Start(int width, int height, int x, int y, int maxSeconds)
        {
            // Spusti stopky
            _stopwatch.Restart();

            // Init
            GC.Collect();
            TimedOut = false;
            MaxMemory = _proc.PrivateMemorySize64;
            _timer.Interval = 1000 * maxSeconds;
            _work = true;
            Steps = 0;
            _uzly.Clear();
            _uzly.Push(new Uzol(width, height, x, y));
            Generated = 1;

            // Ma to zmysel?
            if ((width & 1) == 1 && (height & 1) == 1 && ((x + y) & 1) == 1)
            {
                MaxMemory = Math.Max(MaxMemory, _proc.PrivateMemorySize64); // Zaznač veľkosť využitia pamäte
                _stopwatch.Stop();
                return null;
            }

            // Spustenie časovača
            _timer.Start();

            // Cyklus prehľadávania
            while (_work)
            {
                if (_uzly.Count == 0) // Ak si vyskúšak už všetky stavy
                    break; // Tak ukonči cyklus

                var uzol = _uzly.Pop(); // Vyber nasledujúci stav/uzol
                ++Steps;


                if (uzol.Victory) // Je to konečný stav?
                {
                    _timer.Stop();
                    MaxMemory = Math.Max(MaxMemory, _proc.PrivateMemorySize64); // Zaznač veľkosť využitia pamäte
                    _stopwatch.Stop();
                    return uzol.Path; // Vrať nájdenú cestu
                }

                if (uzol.Failed) // Slepá ulička ?
                    continue;

                // Vygeneruj nasledujúce uzly pomocou výsledkov z dostupných operátorov
                _tempUzly.Clear();
                foreach (var jump in uzol.Jumps)
                    _tempUzly.Add(new Uzol(uzol, jump));
                Generated += uzol.Jumps.Count;

                // Zoraď uzly od najdrahšieho po najlacnejší
                _tempUzly.Sort((a, b) => b.Cost.CompareTo(a.Cost));

                // Priraď uzly do zásobníka (najlacnejší je priradený ako posledný)
                foreach (var u in _tempUzly)
                    _uzly.Push(u);

                MaxMemory = Math.Max(MaxMemory, _proc.PrivateMemorySize64); // Zaznač veľkosť využitia pamäte
            }

            // Nepodarilo sa nájsť cestu
            _timer.Stop();
            MaxMemory = Math.Max(MaxMemory, _proc.PrivateMemorySize64); // Zaznač veľkosť využitia pamäte
            _stopwatch.Stop();
            return null;
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Zastav prehľadávanie
        /// </summary>
        /// <param name="sender">Časovač, ktorý zavolal metódu</param>
        /// <param name="e">Argumenty časovača</param>
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _work = false;
            TimedOut = true;
        }

        #endregion

        #region Constants

        /// <summary>
        ///     Maximálna Šírka šachovnice
        /// </summary>
        public const int MaxWidth = 20;

        /// <summary>
        ///     Maximálna výška šachovnice
        /// </summary>
        public const int MaxHeight = 20;

        /// <summary>
        ///     Minimálna Šírka šachovnice
        /// </summary>
        public const int MinWidth = 5;

        /// <summary>
        ///     Minimálna výška šachovnice
        /// </summary>
        public const int MinHeight = 5;

        #endregion

        #region Public Attributes & Properties

        /// <summary>
        ///     Počet skontrolovaných Uzlov
        /// </summary>
        public int Steps { get; private set; }

        /// <summary>
        ///     Počet vygenerovaných uzlov
        /// </summary>
        public int Generated { get; private set; }

        /// <summary>
        ///     Maximálne využitie pamäte od spustenia prehľadávania
        /// </summary>
        public long MaxMemory { get; private set; }

        /// <summary>
        ///     Čas trvania prehľadávania
        /// </summary>
        public TimeSpan TimeElapsed => _stopwatch.Elapsed;

        /// <summary>
        ///     Určuje či prehľadávanie bole ukončené uplninutím dostupného času.
        /// </summary>
        public bool TimedOut { get; private set; }

        #endregion

        #region Private Variables

        /// <summary>
        ///     Určuje či už uplnyul čas alebo nie
        /// </summary>
        private bool _work;

        /// <summary>
        ///     Listy prehľadávaného grafu
        /// </summary>
        private readonly Stack<Uzol> _uzly = new Stack<Uzol>();

        /// <summary>
        ///     Časovač pre ukončenie prehľadávania
        /// </summary>
        private readonly Timer _timer = new Timer {AutoReset = false};

        /// <summary>
        ///     Stopky pre zistenie trvania prehľadávania
        /// </summary>
        private readonly Stopwatch _stopwatch = new Stopwatch();

        /// <summary>
        ///     Dočasné pole pre zoradenie novo vygenerovaných uzlov
        /// </summary>
        private readonly List<Uzol> _tempUzly = new List<Uzol>(8);

        /// <summary>
        ///     AKtuálny process (pre zistenie pamäte)
        /// </summary>
        private readonly Process _proc = Process.GetCurrentProcess();

        #endregion
    }
}