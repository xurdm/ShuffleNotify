using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Tesseract;


namespace ShuffleNotify
{
    internal class ShuffleNotify
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        [DllImport("user32.dll")]
        private static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, uint nFlags);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        private readonly int _pollingInterval;
        private readonly string _tessDataPath;
        private readonly string _queueText;
        private readonly string _windowTitle;
        private bool _isRunning;

        public event Action<string, string> OnShufflePop;
        public event Action<string, Bitmap> UpdateStatus;

        public ShuffleNotify(string tessDataPath, string queueText, string windowTitle, int pollingInterval)
        {
            _tessDataPath = tessDataPath;
            _queueText = queueText;
            _windowTitle = windowTitle;
            _pollingInterval = pollingInterval;
        }

        public void StartTracking()
        {
            _isRunning = true;

            new Thread(() =>
            {
                while (_isRunning)
                {
                    Bitmap screenshot = CaptureGameWindow(_windowTitle);

                    if (screenshot != null)
                    {
                        string detectedText = GetTextFromBitmap(screenshot);

                        UpdateStatus?.Invoke(detectedText, screenshot);

                        if (detectedText.Contains(_queueText, StringComparison.OrdinalIgnoreCase))
                        {
                            OnShufflePop?.Invoke("Queue pop detected!", detectedText);
                            StopTracking();
                        }
                    }
                    else
                    {
                        OnShufflePop?.Invoke("Game window not found.", "");
                        UpdateStatus?.Invoke("Game window not found.", screenshot);
                    }

                    Thread.Sleep(_pollingInterval); 
                }
            }).Start();
        }

        public void StopTracking()
        {
            _isRunning = false;
        }

        private IntPtr GetWindowHandle(string windowTitle)
        {
            IntPtr hWnd = FindWindow(null, windowTitle);
            if (hWnd == IntPtr.Zero)
            {
                return IntPtr.Zero; 
            }

            uint processId;
            GetWindowThreadProcessId(hWnd, out processId);

            var process = Process.GetProcessById((int)processId);
            if (process.ProcessName.Equals("Wow", StringComparison.OrdinalIgnoreCase))
            {
                return hWnd;
            }

            return IntPtr.Zero; 
        }
        private Bitmap CaptureGameWindow(string windowTitle)
        {
            IntPtr hWnd = GetWindowHandle(windowTitle);

            if (hWnd == IntPtr.Zero)
            {
                return null;
            }

            GetWindowRect(hWnd, out RECT rect);

            int width = rect.Right - rect.Left;
            int height = rect.Bottom - rect.Top;

            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                IntPtr hdc = g.GetHdc();
                try
                {
                    if (!PrintWindow(hWnd, hdc, 2))
                    {
                        throw new InvalidOperationException("PrintWindow failed");
                    }
                }  
                finally
                {
                    g.ReleaseHdc(hdc);
                }
            }

            return bmp;
        }

        private string GetTextFromBitmap(Bitmap image)
        {
            using (var engine = new TesseractEngine(_tessDataPath, "eng", EngineMode.Default))
            {
                using (var img = PixConverter.ToPix(image))
                {
                    using (var page = engine.Process(img))
                    {
                        return page.GetText();
                    }
                }
            }
        }

    }
}
