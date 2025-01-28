namespace ShuffleNotify
{
    public partial class Main : Form
    {
        ShuffleNotify _notifier;

        public Main()
        {
            InitializeComponent();
            string tessDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tessdata");

            _notifier = new ShuffleNotify(tessDataPath, "Enter", "World of Warcraft", 300);
            _notifier.OnShufflePop += OnShuffleQueuePop;
            _notifier.UpdateStatus += OnUpdateStatus;
        }

        private void btnStartTracking_Click(object sender, EventArgs e)
        {
            _notifier.StartTracking();
            btnStartTracking.Enabled = false;
            lblStatus.Text = "Running";
        }
        private void btnStopTracking_Click(object sender, EventArgs e)
        {
            _notifier.StopTracking();
            btnStartTracking.Enabled = true;
            lblStatus.Text = "Not running";
        }

        private void OnShuffleQueuePop(string message, string text)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => MessageBox.Show(message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information)));
                //Invoke(new Action(() => lblText.Text = text));
            }
            else
            {
                MessageBox.Show(message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //lblText.Text = text;
            }
        }

        private void OnUpdateStatus(string text, Bitmap screenshot)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => lblText.Text = text));
                Invoke(new Action(() => pbScreenshot.Image = screenshot));
            } 
            else
            {
                lblText.Text = text;
                pbScreenshot.Image = screenshot;
            }
        }
    }
}
