using System.Text;
using EDIParser;

namespace EDIParser.Core.Tests;

public sealed class ParserBehaviorTests
{
    [Fact]
    public void BufferedParsing_RaisesEventsInOrderAndHonorsCancellation()
    {
        var parser = new X12Parser();
        var observed = new List<string>();
        parser.ParsedSegment += (object sender, int number, ref Segment segment, ref bool cancel) =>
        {
            observed.Add($"{number}:{segment.Name}");
            if (number == 3)
            {
                cancel = true;
            }
        };
        parser.SegmentParsingOption = Parser.SegmentParsingOptions.Buffered;

        parser.ParseMsg(TestMessages.X12);

        Assert.Equal(new[] { "1:ISA", "2:GS", "3:ST" }, observed);
    }

    [Fact]
    public void ConserveMemory_ClearsStoredParserTreeAfterEvents()
    {
        var parser = new X12Parser();
        parser.ParsedSegment += (object sender, int number, ref Segment segment, ref bool cancel) => { };
        parser.SegmentParsingOption = Parser.SegmentParsingOptions.Buffered;
        parser.ConserveMemory = true;

        parser.ParseMsg(TestMessages.X12);

        Assert.Equal(0, parser.Segments.Count);
    }

    [Fact]
    public void ConserveMemory_RequiresParsedSegmentSubscriber()
    {
        var parser = new X12Parser();

        Assert.Throws<ApplicationException>(() => parser.ConserveMemory = true);
    }

    [Fact]
    public void BufferedMode_RequiresParsedSegmentSubscriber()
    {
        var parser = new X12Parser();

        Assert.Throws<ApplicationException>(() =>
            parser.SegmentParsingOption = Parser.SegmentParsingOptions.Buffered);
    }

    [Fact]
    public void MissingItem_ReturnsEmptyObjectWhenIgnoreMissingItemIsTrue()
    {
        var parser = new X12Parser { IgnoreMissingItem = true };
        parser.ParseMsg(TestMessages.X12);

        var missing = parser.Segments["999"];

        Assert.NotNull(missing);
        Assert.Equal(string.Empty, missing.Name);
    }

    [Fact]
    public void MissingItem_ThrowsWhenIgnoreMissingItemIsFalse()
    {
        var parser = new X12Parser { IgnoreMissingItem = false };
        parser.ParseMsg(TestMessages.X12);

        Assert.Throws<ArgumentException>(() => _ = parser.Segments[999]);
    }

    [Fact]
    public void BatchProcessor_ProcessesCharacterDelimitedSegments()
    {
        var parser = new X12Parser();
        using var stream = new MemoryStream(Encoding.ASCII.GetBytes("ST*850*1~BEG*00*SA*123~SE*3*1~"));
        var processor = new BatchProcessor(parser, stream);
        var names = new List<string>();
        processor.Reading += (ref Segment segment) => names.Add(segment.Name);

        processor.Process();

        Assert.Equal(new[] { "ST", "BEG", "SE" }, names);
        Assert.Equal(BatchProcessor.ProcessingStatuses.ProcessingComplete, processor.ProcessingStatus);
    }

    [Fact]
    public void BatchProcessor_LimitCanLeaveProcessingPending()
    {
        var parser = new X12Parser();
        using var stream = new MemoryStream(Encoding.ASCII.GetBytes("ST*850*1~BEG*00*SA*123~SE*3*1~"));
        var processor = new BatchProcessor(parser, stream);
        processor.Limit(0, 1);
        var names = new List<string>();
        processor.Reading += (ref Segment segment) => names.Add(segment.Name);

        processor.Process();

        Assert.Single(names);
        Assert.Equal(BatchProcessor.ProcessingStatuses.ProcessingPending, processor.ProcessingStatus);
    }
}
