using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Clock
{
    public class Start {
        public void Starting() {
            string choice = "";
            while (true)
            {
                Console.WriteLine("1 - Clock\n2 - Stopwatch\n3 - Timer\nSpacebar - exit");
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.D1)
                {
                    choice = "1";
                }
                else if (key.Key == ConsoleKey.D2)
                {
                    choice = "2";
                }
                else if (key.Key == ConsoleKey.D3)
                {
                    choice = "3";
                }
                else if (key.Key == ConsoleKey.Spacebar)
                {
                    choice = "4";
                }
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }
                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Clock clock = new Clock();
                        clock.Start();
                        break;
                    case "2":
                        Console.Clear();
                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();
                        break;
                    case "3":
                        Console.Clear();
                        Timer timer = new Timer();
                        timer.Start();
                        break;
                    case "4":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        break;
                }
            }
        }
    }
    public class Clock
    {
        private bool _isRunning = true;

        public void Start()
        {
            Console.CursorVisible = false;

            while (_isRunning)
            {
                Console.Clear();
                Console.WriteLine("Escape to Exit");

                DateTime currentTime = DateTime.Now;
                Console.WriteLine($"{currentTime.Hour:D2}:{currentTime.Minute:D2}:{currentTime.Second:D2}");

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.Escape)
                    {
                        _isRunning = false;
                    }
                    while (Console.KeyAvailable)
                    {
                        Console.ReadKey(true);
                    }
                }
                Thread.Sleep(100); 
   
            }

            Console.Clear();
            Console.WriteLine("Clock stopped. Returning to main menu...");
            Console.CursorVisible = true;
        }
    }
    public class Stopwatch
    {
        private bool _isRunning = true;
        private System.Diagnostics.Stopwatch _internalStopwatch;
        public void Start()
        {
            Console.CursorVisible = false;

            _internalStopwatch = new System.Diagnostics.Stopwatch();
            _internalStopwatch.Start();

            Console.Clear();
            while (_isRunning)
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Escape to Exit (Spacebar to Toggle Pause)");

                TimeSpan elapsed = _internalStopwatch.Elapsed;

                Console.SetCursorPosition(0, 1);
                Console.WriteLine($"Elapsed: {elapsed.Hours:D2}:{elapsed.Minutes:D2}:{elapsed.Seconds:D2}.{elapsed.Milliseconds:D3}");
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.Escape)
                    {
                        _isRunning = false;
                    }
                    else if (key.Key == ConsoleKey.Spacebar) {
                        if (_internalStopwatch.IsRunning)
                        {
                            _internalStopwatch.Stop();
                            Console.SetCursorPosition(0, 2);
                            Console.WriteLine("Paused. Press Spacebar to resume");
                        }
                        else
                        {
                            _internalStopwatch.Start();
                            Console.SetCursorPosition(0, 2);
                            Console.WriteLine("                                ");
                        }
                    }
                    while (Console.KeyAvailable)
                        {
                            Console.ReadKey(true);
                        }
                }
                Thread.Sleep(50);
            }
            _internalStopwatch.Stop();
            Console.Clear();
            Console.WriteLine($"Final Elapsed Time: {_internalStopwatch.Elapsed.Hours:D2}:{_internalStopwatch.Elapsed.Minutes:D2}:{_internalStopwatch.Elapsed.Seconds:D2}.{_internalStopwatch.Elapsed.Milliseconds:D3}");
            Console.WriteLine("Clock stopped. Returning to main menu...");
            Console.CursorVisible = true;
        }
    }
    public class Timer {
        DateTime DT;
        DateTime Time;
        private static bool IsExists = true;
        public Timer() { DT = DateTime.Today; }
        public void Start()
        {
            Thread plaY = new Thread(PlayMusic);
            Console.Write("Set timer (00:00:00): ");
            try {
                Time = DateTime.ParseExact(Console.ReadLine(), "HH:mm:ss", CultureInfo.InvariantCulture);
            }
            catch { 
                Console.WriteLine("Incorrect time");
                return;
            }
            while (IsExists)
            {
                if (CheckTimer()) {
                    plaY.Start();
                    Console.WriteLine("Timer's gone!");
                    Thread.Sleep(1000);
                    Console.Clear();
                    return;
                }
                Thread thread = new Thread(InThread);
                thread.Start();
                Console.WriteLine($"{Time.Hour}:{Time.Minute}:{Time.Second}");
                Console.WriteLine($"{DT.Hour}:{DT.Minute}:{DT.Second}");
                DT = DT.AddSeconds(1);
                Thread.Sleep(1000);
                Console.Clear();
            }
        }
        public bool CheckTimer() {
            if (Time == DT) {
                return true;
            }
            return false;
        }
        public void InThread()
        {
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Escape)
            {
                IsExists = false;
                return;
            }
            return;
        }
        public void PlayMusic()
        {
            try
            {
                SoundPlayer player = new SoundPlayer();
                player.SoundLocation = @"13285_uGp8bNpU.wav";
                player.Load();
                player.PlaySync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error playing sound: " + ex.Message);
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Start start = new Start();
            start.Starting();
        } 
    }
}
