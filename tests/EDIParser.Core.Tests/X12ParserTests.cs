using EDIParser;

namespace EDIParser.Core.Tests;

public sealed class X12ParserTests
{
    [Fact]
    public void ParseMsg_UsesOneBasedSegmentAndFieldIndexes()
    {
        var parser = new X12Parser();

        parser.ParseMsg(TestMessages.X12);

        Assert.Equal("ISA", parser.Segments[1].Name);
        Assert.Equal("BEG", parser.Segments[4].Name);
        Assert.Equal("12345", parser.Segments[4].Fields[3].Value);
        Assert.Equal("12345", parser.GetValue("BEG.3"));
    }

    [Fact]
    public void ParseMsg_CanReadRepeatedSegmentOccurrences()
    {
        var parser = new X12Parser();

        parser.ParseMsg(TestMessages.X12WithRepeatedN1);

        Assert.Equal("FIRST LOCATION", parser.GetValue("N1.2", 1));
        Assert.Equal("SECOND LOCATION", parser.GetValue("N1.2", 2));
    }

    [Fact]
    public void Message_RoundTripsWithoutChangingNormalizedOutput()
    {
        var first = new X12Parser();
        first.ParseMsg(TestMessages.X12);
        var generated = first.Message();

        var second = new X12Parser();
        second.ParseMsg(generated);

        Assert.Equal(generated, second.Message());
    }

    [Fact]
    public void SetValue_UpdatesExistingFieldAndGeneratedMessage()
    {
        var parser = new X12Parser();
        parser.ParseMsg(TestMessages.X12);

        parser.SetValue("BEG.3", "NEW-PO-NUMBER");

        Assert.Equal("NEW-PO-NUMBER", parser.GetValue("BEG.3"));
        Assert.Contains("BEG*00*SA*NEW-PO-NUMBER**20260731", parser.Message(), StringComparison.Ordinal);
    }

    [Fact]
    public void CheckIsaSeparator_ReadsSeparatorsFromIsaEnvelope()
    {
        var parser = new X12Parser { CheckISASeparator = true };

        parser.ParseMsg(TestMessages.X12);

        Assert.Equal("~", parser.SegmentSeparator);
        Assert.Equal("*", parser.FieldSeparator);
        Assert.Equal(":", parser.ComponentSeparator);
    }

    [Fact]
    public void ParseMsg_RejectsEmptyMessage()
    {
        var parser = new X12Parser();

        Assert.Throws<ArgumentException>(() => parser.ParseMsg(string.Empty));
    }
    [Fact]
    public void RemoveByKey_RemovesTheCorrectItem()
    {
        // Exercise through a public mutation API if possible.
    }

    [Fact]
    public void ZeroIndex_AlwaysThrows()
    {
        var parser = new X12Parser
        {
            IgnoreMissingItem = true
        };

        parser.ParseMsg(TestMessages.X12);

        Assert.Throws<ArgumentOutOfRangeException>(
            () => _ = parser.Segments[0]);
    }

    [Fact]
    public void EmptyElementPath_Throws()
    {
        var parser = new X12Parser();

        Assert.Throws<ArgumentException>(
            () => parser.SetValue("", "value"));
    }

    [Fact]
    public void IgnoreMissingItem_PropagatesBeforeParsing()
    {
        var parser = new X12Parser
        {
            IgnoreMissingItem = true
        };

        parser.ParseMsg(TestMessages.X12);

        Assert.Equal(string.Empty, parser.Segments["999"].Name);
        Assert.Equal(string.Empty, parser.Segments[1].Fields["999"].Name);
    }
    [Fact]
    public void ZeroIndex_ThrowsBecauseCollectionsAreOneBased()
    {
        var parser = new X12Parser
        {
            IgnoreMissingItem = true
        };

        parser.ParseMsg(TestMessages.X12);

        Assert.Throws<ArgumentOutOfRangeException>(
            () => _ = parser.Segments[0]);
    }

    [Fact]
    public void IndexBeyondCount_ReturnsSentinelWhenMissingItemsAreIgnored()
    {
        var parser = new X12Parser
        {
            IgnoreMissingItem = true
        };

        parser.ParseMsg(TestMessages.X12);

        var missing = parser.Segments[999];

        Assert.NotNull(missing);
        Assert.Equal(string.Empty, missing.Name);
    }
}
