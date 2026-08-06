using System.Text;
using EDIParser;

namespace EDIParser.Viewer;

internal sealed class MainForm : Form
{
    private readonly ViewerSettings _settings = ViewerSettings.Load();
    private readonly TreeView _tree = new() { Dock = DockStyle.Fill, HideSelection = false };
    private readonly TextBox _text = CreateTextBox();
    private readonly TextBox _hex = CreateTextBox();
    private readonly TextBox _report = CreateTextBox();
    private readonly ToolStripStatusLabel _status = new() { Text = "Ready" };
    private Parser? _parser;
    private ProgressForm? _progress;

    public MainForm(string[] args)
    {
        AppAssets.ApplyIcon(this);
        Text = "EDI Parser Viewer";
        Width = 1050;
        Height = 760;
        StartPosition = FormStartPosition.CenterScreen;

        var menu = BuildMenu();
        var split = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Horizontal, SplitterDistance = 350 };
        split.Panel1.Controls.Add(_tree);
        var tabs = new TabControl { Dock = DockStyle.Fill };
        tabs.TabPages.Add(new TabPage("Text") { Controls = { _text } });
        tabs.TabPages.Add(new TabPage("Hex") { Controls = { _hex } });
        tabs.TabPages.Add(new TabPage("Report") { Controls = { _report } });
        split.Panel2.Controls.Add(tabs);
        var statusStrip = new StatusStrip(); statusStrip.Items.Add(_status);
        Controls.Add(split); Controls.Add(statusStrip); Controls.Add(menu);
        MainMenuStrip = menu;

        Shown += (_, _) =>
        {
            if (args.Length > 0 && File.Exists(args[0])) OpenPath(args[0], ParserChoice.Auto);
        };
    }

    private static TextBox CreateTextBox() => new()
    {
        Dock = DockStyle.Fill,
        Multiline = true,
        ScrollBars = ScrollBars.Both,
        WordWrap = false,
        Font = new Font(FontFamily.GenericMonospace, 9f)
    };

    private MenuStrip BuildMenu()
    {
        var menu = new MenuStrip();
        var file = new ToolStripMenuItem("&File");
        file.DropDownItems.Add("&Open / Auto Detect…", null, (_, _) => OpenDialog(ParserChoice.Auto));
        file.DropDownItems.Add("Open &X12…", null, (_, _) => OpenDialog(ParserChoice.X12));
        file.DropDownItems.Add("Open &HL7…", null, (_, _) => OpenDialog(ParserChoice.HL7));
        file.DropDownItems.Add("Open &EDIFACT…", null, (_, _) => OpenDialog(ParserChoice.Edifact));
        file.DropDownItems.Add(new ToolStripSeparator());
        file.DropDownItems.Add("E&xit", null, (_, _) => Close());
        var tools = new ToolStripMenuItem("&Tools");
        tools.DropDownItems.Add("&Settings…", null, (_, _) => new SettingsForm(_settings).ShowDialog(this));
        var help = new ToolStripMenuItem("&Help");
        help.DropDownItems.Add("&About…", null, (_, _) => new AboutForm().ShowDialog(this));
        menu.Items.AddRange([file, tools, help]);
        return menu;
    }

    private void OpenDialog(ParserChoice choice)
    {
        using var dialog = new OpenFileDialog
        {
            Filter = "EDI messages|*.edi;*.x12;*.hl7;*.txt;*.dat|All files|*.*",
            Multiselect = false,
            CheckFileExists = true
        };
        if (dialog.ShowDialog(this) == DialogResult.OK) OpenPath(dialog.FileName, choice);
    }

    private void OpenPath(string path, ParserChoice choice)
    {
        try
        {
            var bytes = File.ReadAllBytes(path);
            var message = Encoding.ASCII.GetString(bytes);
            _text.Text = message;
            _hex.Text = ToHex(bytes);
            _report.Clear(); _tree.Nodes.Clear();
            _parser = CreateParser(message, choice);
            _parser.ParsedSegment += ParserOnParsedSegment;
            _parser.ConserveMemory = true;
            _progress = new ProgressForm();
            _progress.Show(this);
            _status.Text = $"Parsing {Path.GetFileName(path)}…";
            Application.DoEvents();
            _parser.ParseMsg(message);
            _status.Text = $"{_tree.Nodes.Count:N0} segments — {Path.GetFileName(path)}";
            _tree.ExpandAll();
        }
        catch (Exception ex)
        {
            _status.Text = "Parse failed";
            MessageBox.Show(this, ex.ToString(), "EDI Parser Viewer", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            if (_parser is not null) _parser.ParsedSegment -= ParserOnParsedSegment;
            _progress?.Close(); _progress?.Dispose(); _progress = null;
        }
    }

    private Parser CreateParser(string message, ParserChoice choice)
    {
        if (choice == ParserChoice.Auto)
        {
            var trimmed = message.TrimStart('\uFEFF', ' ', '\t', '\r', '\n');
            choice = trimmed.StartsWith("ISA", StringComparison.Ordinal) ? ParserChoice.X12
                : trimmed.StartsWith("MSH", StringComparison.Ordinal) ? ParserChoice.HL7
                : ParserChoice.Edifact;
        }

        return choice switch
        {
            ParserChoice.X12 => ConfigureX12(new X12Parser()),
            ParserChoice.HL7 => ConfigureHl7(new HL7Parser()),
            _ => new EdiFactParser()
        };
    }

    private X12Parser ConfigureX12(X12Parser parser)
    {
        if (!_settings.DetectX12Separators)
        {
            parser.SegmentSeparator = ViewerSettings.Decode(_settings.X12SegmentSeparator);
            parser.FieldSeparator = ViewerSettings.Decode(_settings.X12FieldSeparator);
            parser.ComponentSeparator = ViewerSettings.Decode(_settings.X12ComponentSeparator);
            parser.RepetitionSeparator = ViewerSettings.Decode(_settings.X12RepetitionSeparator);
        }
        return parser;
    }

    private HL7Parser ConfigureHl7(HL7Parser parser)
    {
        if (!_settings.DetectHl7Separators)
        {
            parser.SegmentSeparator = ViewerSettings.Decode(_settings.Hl7SegmentSeparator);
            parser.FieldSeparator = ViewerSettings.Decode(_settings.Hl7FieldSeparator);
            parser.ComponentSeparator = ViewerSettings.Decode(_settings.Hl7ComponentSeparator);
            parser.SubComponentSeparator = ViewerSettings.Decode(_settings.Hl7SubComponentSeparator);
            parser.RepetitionSeparator = ViewerSettings.Decode(_settings.Hl7RepetitionSeparator);
        }
        return parser;
    }

    private void ParserOnParsedSegment(object sender, int segmentNbr, ref Segment segment, ref bool cancel)
    {
        AddSegment(segment);
        cancel = _progress?.CancelRequested == true;
        if (segmentNbr % 25 == 0) Application.DoEvents();
    }

    private void AddSegment(Segment segment)
    {
        var segmentNode = _tree.Nodes.Add(segment.Name, segment.Name);
        _report.AppendText(segment.Name + Environment.NewLine);
        foreach (Field field in segment.Fields)
        {
            var fieldPath = $"{segment.Name}.{field.Name}";
            var fieldNode = segmentNode.Nodes.Add(fieldPath, field.Components.Count == 0 ? $"{fieldPath} - {field.Value}" : fieldPath);
            _report.AppendText($"    {fieldNode.Text}{Environment.NewLine}");
            var repeatCount = Math.Max(1, field.RepetitionCount);
            for (var repeatIndex = 1; repeatIndex <= repeatCount; repeatIndex++)
            {
                var components = field.ComponentsByRepetitionIndexer[repeatIndex];
                var componentParent = repeatCount > 1 ? fieldNode.Nodes.Add($"Repetition {repeatIndex}") : fieldNode;
                foreach (EDIParser.Component component in components)
                {
                    var componentPath = $"{fieldPath}.{component.Name}";
                    var componentNode = componentParent.Nodes.Add(componentPath, $"{componentPath} - {component.Value}");
                    _report.AppendText($"        {componentNode.Text}{Environment.NewLine}");
                    foreach (SubComponent sub in component.SubComponents)
                        componentNode.Nodes.Add($"{componentPath}.{sub.Name} - {sub.Value}");
                    foreach (Repetition repetition in component.Repetitions)
                    {
                        var repetitionNode = componentNode.Nodes.Add($"Repetition {repetition.Name} - {repetition.Value}");
                        foreach (SubComponent sub in repetition.SubComponents)
                            repetitionNode.Nodes.Add($"{componentPath}.{repetition.Name}.{sub.Name} - {sub.Value}");
                    }
                }
            }
        }
    }

    private static string ToHex(byte[] bytes)
    {
        var output = new StringBuilder(bytes.Length * 4);
        for (var offset = 0; offset < bytes.Length; offset += 16)
        {
            output.Append(offset.ToString("X8")).Append("  ");
            var lineLength = Math.Min(16, bytes.Length - offset);
            for (var i = 0; i < 16; i++) output.Append(i < lineLength ? bytes[offset + i].ToString("X2") + " " : "   ");
            output.Append(' ');
            for (var i = 0; i < lineLength; i++)
            {
                var value = bytes[offset + i];
                output.Append(value is >= 32 and <= 126 ? (char)value : '.');
            }
            output.AppendLine();
        }
        return output.ToString();
    }

    private enum ParserChoice { Auto, X12, HL7, Edifact }
}
