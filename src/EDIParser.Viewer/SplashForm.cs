using System.Reflection;

namespace EDIParser.Viewer;

internal sealed class SplashForm : Form
{
    public SplashForm()
    {
        Text = "EDI Parser Viewer";
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        ControlBox = false;
        ShowInTaskbar = false;
        ClientSize = new Size(500, 305);
        BackColor = SystemColors.Control;
        AppAssets.ApplyIcon(this);

        var frame = new GroupBox
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(14)
        };

        var logo = new PictureBox
        {
            Location = new Point(36, 55),
            Size = new Size(124, 97),
            BorderStyle = BorderStyle.FixedSingle,
            BackColor = SystemColors.Window,
            SizeMode = PictureBoxSizeMode.StretchImage,
            Image = AppAssets.CreateSplashImage()
        };

        Assembly assembly = typeof(SplashForm).Assembly;
        Version version = assembly.GetName().Version ?? new Version(1, 0, 0, 0);
        string product = assembly.GetCustomAttribute<AssemblyProductAttribute>()?.Product ?? "EDI Parser Viewer";
        string company = assembly.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company ?? "EDI Parser";
        string copyright = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright
            ?? $"Copyright © {DateTime.Now.Year}";

        var companyLabel = NewLabel(company, new Point(178, 50), new Font("Arial", 13.5f, FontStyle.Bold), true);
        var productLabel = NewLabel(product, new Point(178, 79), new Font("Arial", 18f, FontStyle.Bold), true);
        var platformLabel = NewLabel("Windows", new Point(296, 160), new Font("Arial", 12f, FontStyle.Bold), true);
        var versionLabel = NewLabel($"Version {version}", new Point(296, 184), new Font("Arial", 12f, FontStyle.Bold), true);
        var copyrightLabel = NewLabel(copyright, new Point(296, 211), new Font("Arial", 8f), false);
        copyrightLabel.Size = new Size(190, 17);
        var companyFooter = NewLabel(company, new Point(296, 229), new Font("Arial", 8f), false);
        companyFooter.Size = new Size(190, 17);
        var warning = NewLabel("This program is protected by U.S. law.", new Point(20, 264), new Font("Arial", 8f), false);
        warning.Size = new Size(455, 18);

        frame.Controls.AddRange([
            logo,
            companyLabel,
            productLabel,
            platformLabel,
            versionLabel,
            copyrightLabel,
            companyFooter,
            warning
        ]);
        Controls.Add(frame);
    }

    private static Label NewLabel(string text, Point location, Font font, bool autoSize) => new()
    {
        Text = text,
        Location = location,
        Font = font,
        AutoSize = autoSize,
        BackColor = Color.Transparent
    };

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            foreach (Control control in Controls)
            {
                DisposeImages(control);
            }
        }

        base.Dispose(disposing);
    }

    private static void DisposeImages(Control control)
    {
        if (control is PictureBox pictureBox)
        {
            pictureBox.Image?.Dispose();
        }

        foreach (Control child in control.Controls)
        {
            DisposeImages(child);
        }
    }
}
