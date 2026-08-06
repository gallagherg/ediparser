using EDIParser;

namespace EDIParser.Core.Tests;

public sealed class EdiFactParserTests
{
    [Fact]
    public void ParseMsg_ReadsUnaSeparatorsAndCompositeElements()
    {
        var parser = new EdiFactParser();

        parser.ParseMsg(TestMessages.Edifact);

        Assert.Equal("'", parser.SegmentSeparator);
        Assert.Equal("+", parser.FieldSeparator);
        Assert.Equal(":", parser.ComponentSeparator);
        Assert.Equal("?", parser.ReleaseIndicator);
        Assert.Equal("PO123", parser.GetValue("BGM.2"));
        Assert.Equal("ORDERS", parser.GetValue("UNH.2.1"));
        Assert.Equal("96A", parser.GetValue("UNH.2.3"));
    }

    [Fact]
    public void Message_PreservesUnaHeaderAndRoundTrips()
    {
        var first = new EdiFactParser();
        first.ParseMsg(TestMessages.Edifact);
        var generated = first.Message();

        var second = new EdiFactParser();
        second.ParseMsg(generated);

        Assert.StartsWith("UNA", generated, StringComparison.Ordinal);
        Assert.Equal(generated, second.Message());
    }

    [Fact]
    public void ParseMsg_RejectsMessageTooShortForUnaDelimiters()
    {
        var parser = new EdiFactParser();

        Assert.Throws<ArgumentException>(() => parser.ParseMsg("UNA:+"));
    }
}
