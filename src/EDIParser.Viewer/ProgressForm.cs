namespace EDIParser.Viewer;

internal sealed class ProgressForm : Form
{
    private readonly ProgressBar _progress = new() { Dock = DockStyle.Top, Height = 24, Style = ProgressBarStyle.Marquee };
    private readonly Button _cancel = new() { Dock = DockStyle.Bottom, Height = 30, Text = "Cancel" };
    public bool CancelRequested { get; private set; }

    public ProgressForm()
    {
        AppAssets.ApplyIcon(this);
        Text = "Parsing message";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        ControlBox = false;
        ClientSize = new Size(360, 70);
        Controls.Add(_progress);
        Controls.Add(_cancel);
        _cancel.Click += (_, _) => { CancelRequested = true; _cancel.Enabled = false; _cancel.Text = "Cancelling…"; };
    }
}
