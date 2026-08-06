namespace EDIParser.Viewer;

internal static class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        ApplicationConfiguration.Initialize();

        using (var splash = new SplashForm())
        {
            splash.Show();
            splash.Refresh();

            DateTime closeAt = DateTime.UtcNow.AddSeconds(2);
            while (DateTime.UtcNow < closeAt)
            {
                Application.DoEvents();
                Thread.Sleep(25);
            }

            splash.Close();
        }

        Application.Run(new MainForm(args));
    }
}
