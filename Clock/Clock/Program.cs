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
            while (true)
            {
                Console.WriteLine("1 - Clock\n2 - Stopwatch\n3 - Timer\nSpacebar - exit");
                char choise = '-';
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.D1)
                {
                    choise = '1';
                }
                else if (key.Key == ConsoleKey.D2)
                {
                    choise = '2';
                }
                else if (key.Key == ConsoleKey.D3)
                {
                    choise = '3';
                }
                else if (key.Key == ConsoleKey.Spacebar)
                {
                    choise = '4';
                }
                switch (choise)
                {
                    case '1':
                        Console.Clear();
                        Clock clock = new Clock();
                        clock.Start();
                        break;
                    case '2':
                        Console.Clear();
                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();
                        break;
                    case '3':
                        Console.Clear();
                        Timer timer = new Timer();
                        timer.Start();
                        break;
                    case '4':
                        Environment.Exit(0);
                        break;
                    case ' ':
                        Console.Clear();
                        break;
                    default:
                        Console.Clear();
                        break;
                }
            }
        }
    }
    public class Clock {
        DateTime DT;
        private static bool IsExists = true;
        public void Start() {
            while (IsExists)
            {
                Thread thread = new Thread(InThread);
                thread.Start();
                DT = DateTime.Now;
                Console.WriteLine($"{DT.Hour}:{DT.Minute}:{DT.Second}");
                Thread.Sleep(1000);
                Console.Clear();
            }
        }
        public void InThread() {
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Escape)
            {
                IsExists = false;
                return;
            }
            return;
        }
    }
    public class Stopwatch
    {
        DateTime DT;
        private static bool IsExists = true;
        public Stopwatch() { DT = DateTime.Today; }
        public void Start()
        {
            while (IsExists)
            {
                Thread thread = new Thread(InThread);
                thread.Start();
                Console.WriteLine($"{DT.Hour}:{DT.Minute}:{DT.Second}");
                DT = DT.AddSeconds(1);
                Thread.Sleep(1000);
                Console.Clear();
            }
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
