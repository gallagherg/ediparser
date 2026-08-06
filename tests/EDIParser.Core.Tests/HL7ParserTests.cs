using EDIParser;

namespace EDIParser.Core.Tests;

public sealed class HL7ParserTests
{
    [Fact]
    public void ParseMsg_DetectsMshSeparatorsAndReadsComponents()
    {
        var parser = new HL7Parser();

        parser.ParseMsg(TestMessages.Hl7);

        Assert.Equal("|", parser.FieldSeparator);
        Assert.Equal("^", parser.ComponentSeparator);
        Assert.Equal("~", parser.RepetitionSeparator);
        Assert.Equal("\\", parser.EscapeChar);
        Assert.Equal("&", parser.SubComponentSeparator);
        Assert.Equal("12345", parser.GetValue("PID.3.1"));
        Assert.Equal("DOE", parser.GetValue("PID.5.1"));
        Assert.Equal("JOHN", parser.GetValue("PID.5.2"));
    }

    [Fact]
    public void ParseMsg_AutomaticallySwitchesToCrLfSegmentSeparator()
    {
        var parser = new HL7Parser();

        parser.ParseMsg(TestMessages.Hl7CrLf);

        Assert.Equal("\r\n", parser.SegmentSeparator);
        Assert.Equal(3, parser.Segments.Count);
        Assert.Equal("PV1", parser.Segments[3].Name);
    }

    [Fact]
    public void ContinueFieldLoop_SkipsLaterRepetitionComponentsAfterPlainValue()
    {
        var parser = new HL7Parser();

        parser.ParseMsg(TestMessages.Hl7ContinueFieldLoop);

        Assert.Equal("PLAIN", parser.GetValue("PID.5", 1, 1));
        Assert.Equal(string.Empty, parser.GetValue("PID.5.1", 1, 2));
    }

    [Fact]
    public void SetValue_UpdatesComponentValue()
    {
        var parser = new HL7Parser();
        parser.ParseMsg(TestMessages.Hl7);

        parser.SetValue("PID.5.2", "JANE");

        Assert.Equal("JANE", parser.GetValue("PID.5.2"));
        Assert.Contains("DOE^JANE", parser.Message(), StringComparison.Ordinal);
    }

    [Fact]
    public void ParseMsg_RejectsMessageTooShortForMshDelimiters()
    {
        var parser = new HL7Parser();

        Assert.Throws<ArgumentException>(() => parser.ParseMsg("MSH|^"));
    }
    [Fact]
    public void ParsedRepetitions_AreIteratedOnlyThroughRepetitionCount()
    {
        var parser = new HL7Parser();

        parser.ParseMsg(TestMessages.HL7WithRepetitions);

        Assert.Equal(3, parser.Segments.Count);

        var pid = parser.Segments[2];
        Assert.Equal("PID", pid.Name);

        var field = pid.Fields[3];

        Assert.True(field.HasRepetition);
        Assert.Equal(2, field.RepetitionCount);

        var expectedValues = new[]
        {
            ("12345", "MRN"),
            ("67890", "ALT")
        };

        for (var repetition = 1;
             repetition <= field.RepetitionCount;
             repetition++)
        {
            var components =
                field.ComponentsByRepetitionIndexer[repetition];

            Assert.Equal(4, components.Count);
            Assert.Equal(expectedValues[repetition - 1].Item1, components[1].Value);
            Assert.Equal(string.Empty, components[2].Value);
            Assert.Equal(string.Empty, components[3].Value);
            Assert.Equal(expectedValues[repetition - 1].Item2, components[4].Value);
        }
    }
    [Fact]
    public void ParsingAndGenerating_DoesNotAddEmptyRepetitions()
    {
        var parser = new HL7Parser();
        parser.ParseMsg(TestMessages.HL7WithRepetitions);

        var generated = parser.Message();
        Assert.Equal(3, parser.Segments.Count);
        var lastSegment = parser.Segments[3];

        Assert.Equal("PV1", lastSegment.Name);
        Assert.Equal("PV1|1|I|WARD^101^1", lastSegment.Value);
        Assert.False(lastSegment.Value.EndsWith("\r\n"));

        Assert.Equal(
            TestMessages.HL7WithRepetitions,
            generated);
    }
}
