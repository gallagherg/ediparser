using System.Text.Json;

namespace EDIParser.Viewer;

internal sealed class ViewerSettings
{
    public bool DetectX12Separators { get; set; } = true;
    public bool DetectHl7Separators { get; set; } = true;
    public string X12SegmentSeparator { get; set; } = "~";
    public string X12FieldSeparator { get; set; } = "*";
    public string X12ComponentSeparator { get; set; } = ">";
    public string X12RepetitionSeparator { get; set; } = "^";
    public string Hl7SegmentSeparator { get; set; } = "\\r";
    public string Hl7FieldSeparator { get; set; } = "|";
    public string Hl7ComponentSeparator { get; set; } = "^";
    public string Hl7SubComponentSeparator { get; set; } = "&";
    public string Hl7RepetitionSeparator { get; set; } = "~";

    private static string SettingsPath => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "EDIParserViewer", "settings.json");

    public static ViewerSettings Load()
    {
        try
        {
            return File.Exists(SettingsPath)
                ? JsonSerializer.Deserialize<ViewerSettings>(File.ReadAllText(SettingsPath)) ?? new ViewerSettings()
                : new ViewerSettings();
        }
        catch
        {
            return new ViewerSettings();
        }
    }

    public void Save()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(SettingsPath)!);
        File.WriteAllText(SettingsPath, JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true }));
    }

    public static string Decode(string value) => value
        .Replace("\\r\\n", "\r\n", StringComparison.Ordinal)
        .Replace("\\r", "\r", StringComparison.Ordinal)
        .Replace("\\n", "\n", StringComparison.Ordinal);
}
