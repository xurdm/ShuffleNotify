using System;
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

        public event Action<string> OnMessage;

        public ShuffleNotify(string tessDataPath, string queueText, string windowTitle, int pollingInterval)
        {
            _tessDataPath = tessDataPath;
            _queueText = queueText;
            _windowTitle = windowTitle;
            _pollingInterval = pollingInterval;
        }

        public void StartDetection()
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

                        if (detectedText.Contains(_queueText, StringComparison.OrdinalIgnoreCase))
                        {
                            OnMessage?.Invoke("Queue popped detected!");
                            StopDetection();
                        }
                    }
                    else
                    {
                        OnMessage?.Invoke("Game window not found.");
                    }

                    Thread.Sleep(_pollingInterval); 
                }
            }).Start();
        }

        public void StopDetection()
        {
            _isRunning = false;
        }

        private Bitmap CaptureGameWindow(string windowTitle)
        {
            IntPtr hWnd = FindWindow(null, windowTitle);
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
                g.CopyFromScreen(rect.Left, rect.Top, 0, 0, new Size(width, height));
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
