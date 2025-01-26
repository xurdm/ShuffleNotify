namespace ShuffleNotify
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnStartTracking_Click(object sender, EventArgs e)
        {
            ShuffleNotify notifier = new ShuffleNotify(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tessdata"), "queueText", "windowTitle", 1000);
        }
    }
}
