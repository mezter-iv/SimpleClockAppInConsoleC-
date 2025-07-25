using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Clock
{
    public class Start {
        public Start() {
            string choice = "";
            while (true)
            {
                Console.WriteLine("--- Simple Watch App ---\n1 - Clock\n2 - Stopwatch\n3 - Timer\n4 - Check for flags\n5 - Clean Flags\nSpacebar - exit\n------------------------");
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
                else if (key.Key == ConsoleKey.D4)
                {
                    choice = "4";
                }
                else if (key.Key == ConsoleKey.D5)
                {
                    choice = "5";
                }
                else if (key.Key == ConsoleKey.Spacebar)
                {
                    choice = "6";
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
                        Console.Clear();
                        break;
                    case "2":
                        Console.Clear();
                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();
                        Console.Clear();
                        break;
                    case "3":
                        Console.Clear();
                        Timer timer = new Timer();
                        timer.Start();
                        Console.Clear();
                        break;
                    case "4":
                        Console.Clear(); 
                        try
                        {
                            using (var sr = new StreamReader($"Flags.txt"))
                            {
                                Console.WriteLine("--- Recorded Flags ---"); 
                                string text;
                                while ((text = sr.ReadLine()) != null)
                                {
                                    Console.WriteLine(text);
                                }
                                Console.WriteLine("----------------------");
                            }
                        }
                        catch (FileNotFoundException)
                        {
                            Console.WriteLine("The file 'Flags.txt' was not found. Have you recorded any flags yet?");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
                        }

                        Console.WriteLine("\nPress any key to return to the main menu...");
                        Console.ReadKey(true);
                        Console.Clear();
                        break;
                    case "5":
                        Console.Clear();
                        using (var fs = new FileStream(@"Flags.txt", FileMode.Truncate))
                        {
                        }
                        break;
                    case "6":
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
                Console.WriteLine("--- Simple Clock ---\nEscape to Exit");

                DateTime currentTime = DateTime.Now;
                Console.WriteLine($"{currentTime.Hour:D2}:{currentTime.Minute:D2}:{currentTime.Second:D2}\n--------------------");

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
                Console.WriteLine("--- Simple Stopwatch ---\nEscape to Exit (Spacebar to Toggle Pause / Enter for Making a Flag)");

                TimeSpan elapsed = _internalStopwatch.Elapsed;

                Console.SetCursorPosition(0, 1);
                Console.WriteLine($"\nElapsed: {elapsed.Hours:D2}:{elapsed.Minutes:D2}:{elapsed.Seconds:D2}.{elapsed.Milliseconds:D3}\n------------------------");
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.Escape)
                    {
                        _isRunning = false;
                    }
                    else if (key.Key == ConsoleKey.Spacebar)
                    {
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
                    else if (key.Key == ConsoleKey.Enter) {
                        if (_internalStopwatch.IsRunning)
                        {
                            try
                            {
                                using (var sr = new StreamWriter("Flags.txt", true))
                                {
                                    Console.WriteLine($"Flag Added with {elapsed.Hours:D2}:{elapsed.Minutes:D2}:{elapsed.Seconds:D2}.{elapsed.Milliseconds:D3}");
                                    sr.WriteLine($"{elapsed.Hours:D2}:{elapsed.Minutes:D2}:{elapsed.Seconds:D2}.{elapsed.Milliseconds:D3}");
                                }
                            }
                            catch {
                                Console.WriteLine("Cannot to open a file");
                            }
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
        private bool _isRunning = true;
        public Timer() { DT = DateTime.Today; }
        public void Start()
        {
            Thread plaY = new Thread(PlayMusic);
            Console.Write("--- Simple Timer ---\nSet timer (00:00:00 or write Esc to Exit): ");
            string Getting = Console.ReadLine();
            if (Getting != "Esc") {
                try
                {
                    Time = DateTime.ParseExact(Getting, "HH:mm:ss", CultureInfo.InvariantCulture);
                    while (_isRunning)
                    {
                        Console.Clear();
                        Console.WriteLine("Escape to Exit");
                        if (Console.KeyAvailable)
                        {
                            ConsoleKeyInfo key = Console.ReadKey(true);
                            key = Console.ReadKey(true);
                            if (key.Key == ConsoleKey.Escape)
                            {
                                _isRunning = false;
                            }
                            while (Console.KeyAvailable)
                            {
                                Console.ReadKey(true);
                            }
                        }
                        if (CheckTimer())
                        {
                            plaY.Start();
                            Console.WriteLine("Timer's gone!");
                            Thread.Sleep(1000);
                            Console.Clear();
                            return;
                        }
                        Console.WriteLine($"{Time.Hour:D2}:{Time.Minute:D2}:{Time.Second:D2}");
                        Console.WriteLine($"{DT.Hour:D2}:{DT.Minute:D2}:{DT.Second:D2}");
                        Console.WriteLine("--------------------");
                        DT = DT.AddSeconds(1);
                        Thread.Sleep(1000);
                        Console.Clear();
                    }
                }
                catch
                {
                    Console.WriteLine("Incorrect time");
                    return;
                }              
            }
            return;
        }
        public bool CheckTimer() {
            if (Time == DT) {
                return true;
            }
            return false;
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
        } 
    }
}
