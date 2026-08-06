using System.Reflection;

namespace EDIParser.Viewer;

internal static class AppAssets
{
    private const string IconResourceName = "EDIParser.Viewer.Resources.EDI.ico";
    private const string SplashResourceName = "EDIParser.Viewer.Resources.edi.bmp";

    public static Icon? CreateIcon()
    {
        using Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(IconResourceName);
        return stream is null ? null : (Icon)new Icon(stream).Clone();
    }

    public static Image? CreateSplashImage()
    {
        using Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(SplashResourceName);
        return stream is null ? null : new Bitmap(stream);
    }

    public static void ApplyIcon(Form form)
    {
        Icon? icon = CreateIcon();
        if (icon is not null)
        {
            form.Icon = icon;
        }
    }
}
