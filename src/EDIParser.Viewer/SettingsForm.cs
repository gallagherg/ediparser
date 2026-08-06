namespace EDIParser.Viewer;

internal sealed class SettingsForm : Form
{
    private readonly ViewerSettings _settings;
    private readonly CheckBox _detectX12 = new() { Text = "Detect X12 separators from ISA", AutoSize = true };
    private readonly CheckBox _detectHl7 = new() { Text = "Detect HL7 separators from MSH", AutoSize = true };
    private readonly Dictionary<string, TextBox> _values = new();

    public SettingsForm(ViewerSettings settings)
    {
        AppAssets.ApplyIcon(this);
        _settings = settings;
        Text = "Viewer Settings";
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Width = 510;
        Height = 480;

        var panel = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(12), ColumnCount = 2, AutoScroll = true };
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62));
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38));
        Controls.Add(panel);
        AddFull(panel, _detectX12);
        Add(panel, "X12 segment separator", nameof(settings.X12SegmentSeparator), settings.X12SegmentSeparator);
        Add(panel, "X12 field separator", nameof(settings.X12FieldSeparator), settings.X12FieldSeparator);
        Add(panel, "X12 component separator", nameof(settings.X12ComponentSeparator), settings.X12ComponentSeparator);
        Add(panel, "X12 repetition separator", nameof(settings.X12RepetitionSeparator), settings.X12RepetitionSeparator);
        AddFull(panel, _detectHl7);
        Add(panel, "HL7 segment separator (\\r, \\n or \\r\\n)", nameof(settings.Hl7SegmentSeparator), settings.Hl7SegmentSeparator);
        Add(panel, "HL7 field separator", nameof(settings.Hl7FieldSeparator), settings.Hl7FieldSeparator);
        Add(panel, "HL7 component separator", nameof(settings.Hl7ComponentSeparator), settings.Hl7ComponentSeparator);
        Add(panel, "HL7 subcomponent separator", nameof(settings.Hl7SubComponentSeparator), settings.Hl7SubComponentSeparator);
        Add(panel, "HL7 repetition separator", nameof(settings.Hl7RepetitionSeparator), settings.Hl7RepetitionSeparator);

        var buttons = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, AutoSize = true };
        var ok = new Button { Text = "OK", DialogResult = DialogResult.OK, AutoSize = true };
        var cancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, AutoSize = true };
        buttons.Controls.Add(ok); buttons.Controls.Add(cancel);
        AddFull(panel, buttons);
        AcceptButton = ok; CancelButton = cancel;
        _detectX12.Checked = settings.DetectX12Separators;
        _detectHl7.Checked = settings.DetectHl7Separators;
        ok.Click += Save;
    }

    private static void AddFull(TableLayoutPanel panel, Control control)
    {
        panel.Controls.Add(control, 0, panel.RowCount);
        panel.SetColumnSpan(control, 2);
        panel.RowCount++;
    }

    private void Add(TableLayoutPanel panel, string label, string key, string value)
    {
        panel.Controls.Add(new Label { Text = label, AutoSize = true, Anchor = AnchorStyles.Left }, 0, panel.RowCount);
        var text = new TextBox { Text = value, Dock = DockStyle.Fill };
        panel.Controls.Add(text, 1, panel.RowCount);
        _values[key] = text;
        panel.RowCount++;
    }

    private void Save(object? sender, EventArgs e)
    {
        _settings.DetectX12Separators = _detectX12.Checked;
        _settings.DetectHl7Separators = _detectHl7.Checked;
        foreach (var item in _values)
            typeof(ViewerSettings).GetProperty(item.Key)!.SetValue(_settings, item.Value.Text);
        _settings.Save();
    }
}
