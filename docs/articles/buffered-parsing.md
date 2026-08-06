# Buffered Parsing

Buffered parsing processes one segment at a time instead of eagerly splitting the entire message.

This is useful for large files and event-driven processing.

```csharp
parser.ParsedSegment += (_, args) =>
{
    // Process the segment immediately.
};

parser.ConserveMemory = true;
parser.ParseMsg();
```

## Preserved migration behavior

The .NET 8 migration replaced the original VB `Segment_Jump` label with structured lazy iteration. The converted implementation preserves:

- segment-by-segment processing
- immediate `ParsedSegment` events
- cancellation between segments
- `ConserveMemory` behavior

The original `Continue_Field_Loop` label was replaced with structured loop control while preserving the field counter increment exactly once per field.
