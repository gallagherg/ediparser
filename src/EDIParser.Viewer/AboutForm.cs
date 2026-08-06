namespace EDIParser.Viewer;

internal sealed class AboutForm : Form
{
    public AboutForm()
    {
        AppAssets.ApplyIcon(this);
        Text = "About EDI Parser Viewer";
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ClientSize = new Size(430, 190);
        var version = typeof(AboutForm).Assembly.GetName().Version?.ToString() ?? "1.0";
        var label = new Label
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(20),
            TextAlign = ContentAlignment.MiddleCenter,
            Text = $"EDI Parser Viewer\nVersion {version}"
        };
        var ok = new Button { Dock = DockStyle.Bottom, Height = 32, Text = "OK", DialogResult = DialogResult.OK };
        Controls.Add(label); Controls.Add(ok); AcceptButton = ok;
    }
}
