# EDIParser

EDIParser is an open-source .NET 8 library for parsing, inspecting, modifying, and generating electronic data interchange messages.

This project is a modernized C# migration of the original EDIParser library, Windows Forms Viewer application, and selected sample projects.

## Documentation

Full documentation and API reference:

https://gallagherg.github.io/ediparser/

## Supported formats

EDIParser currently supports:

- ANSI X12
- HL7
- UN/EDIFACT

The migrated parser preserves the original public one-based indexing behavior for segments, fields, field repetitions, components, component repetitions, and subcomponents.

## Repository structure

```text
EDIParser/
├── src/
│   ├── EDIParser.Core/
│   ├── EDIParser.Viewer/
│   └── EDIParser.Samples/
├── tests/
├── docs/
├── EDIParser.sln
├── LICENSE
└── README.md
```

## Requirements

- .NET 8 SDK
- Visual Studio 2022 or another compatible .NET development environment
- Windows for the Windows Forms Viewer
- Windows and a compatible OleDb provider for any retained OleDb-based samples

## Building the solution

Clone the repository and run the following commands from the repository root:

```powershell
dotnet restore .\EDIParser.sln
dotnet build .\EDIParser.sln -c Release
```

To run the automated tests:

```powershell
dotnet test .\EDIParser.sln -c Release
```

## Basic usage

### Parse an X12 message

```csharp
using EDIParser;

var message = File.ReadAllText("purchase-order.edi");

var parser = new X12Parser
{
    CheckISASeparator = true
};

parser.ParseMsg(message);

var purchaseOrderNumber = parser.GetValue(
    "BEG.3",
    segmentIndex: 1,
    fieldRepeatIndex: 1);

Console.WriteLine(purchaseOrderNumber);
```

The `segmentIndex` identifies the occurrence of the segment named in the EDI path. For example, an index of `2` with an `N1` path selects the second `N1` segment, not necessarily the second segment in the complete message.

### Parse an HL7 message

```csharp
using EDIParser;

var message = File.ReadAllText("admission.hl7");

var parser = new HL7Parser();

parser.ParseMsg(message);

var patientIdentifier = parser.GetValue(
    "PID.3.1",
    segmentIndex: 1,
    fieldRepeatIndex: 1);

Console.WriteLine(patientIdentifier);
```

By default, `HL7Parser` reads the field separator and encoding characters from the MSH segment.

### Parse an EDIFACT message

```csharp
using EDIParser;

var message = File.ReadAllText("orders.edi");

var parser = new EdiFactParser();

parser.ParseMsg(message);

var regeneratedMessage = parser.Message();
```

`EdiFactParser` reads the delimiter definition from the UNA service string advice at the beginning of the message.

## Parser object model

EDIParser represents a message using the following primary hierarchy:

```text
Parser
└── Segment
    └── Field
        └── Component
            ├── Repetition
            │   └── SubComponent
            └── SubComponent
```

HL7 field repetitions are represented separately through field repetition indexers:

```text
Field
├── ValueByRepetitionIndexer
└── ComponentsByRepetitionIndexer
```

The exact use of repetitions depends on the message standard:

- HL7 uses repeated fields.
- X12 may use repeated component values.
- Components and repetitions may contain subcomponents.

## Indexing model

EDIParser preserves the original one-based public indexing behavior.

For example:

```csharp
var firstSegment = parser.Segments[1];
var firstField = firstSegment.Fields[1];
var firstComponent = firstField.Components[1];
```

Internally, the migrated implementation uses normal zero-based .NET storage while preserving the original public API contract.

Segment collection keys currently use one-based numeric strings:

```csharp
var firstSegmentByPosition = parser.Segments[1];
var firstSegmentByKey = parser.Segments["1"];
```

Literal segment-name lookup is intentionally not used for the segment collection because EDI messages may contain repeated segment names such as `N1`, `OBX`, or `NTE`.

Use `GetValue` when retrieving values by segment name and EDI path.

## EDI paths

Values are addressed with period-delimited EDI paths.

The complete path structure is:

```text
Segment.Field.Component.Repetition.SubComponent
```

Not every value requires every path level.

Examples:

```text
BEG.3
PID.3.1
REF.2.1
```

A repetition value of `0` is used internally by the path navigation model when a subcomponent belongs directly to a component rather than to a repeated component value.

## Reading values

Read a value from the first occurrence of a segment:

```csharp
var value = parser.GetValue("BEG.3");
```

Read a value from a specific occurrence of a repeated segment:

```csharp
var secondN1Name = parser.GetValue(
    "N1.2",
    segmentIndex: 2);
```

Read a value from a specific field repetition:

```csharp
var alternatePatientIdentifier = parser.GetValue(
    "PID.3.1",
    segmentIndex: 1,
    fieldRepeatIndex: 2);
```

When a requested value cannot be found, the parser normally returns an empty value when `IgnoreMissingItem` is enabled.

## Updating values

Values can be updated through the parser value API:

```csharp
parser.SetValue(
    "BEG.3",
    "NEW-PO-NUMBER",
    segmentIndex: 1,
    fieldRepeatIndex: 1);
```

Missing segments, fields, components, repetitions, and subcomponents are created when required by the supplied path.

After updating the object model, rebuild the message with `Message()`:

```csharp
var updatedMessage = parser.Message();
```

`Message()` reconstructs the message from the current parser object model and includes a trailing segment separator when at least one segment is present.

## HL7 field repetitions

HL7 repeated fields use the configured repetition separator, normally `~`.

For example:

```text
PID|1||12345^^^MRN~67890^^^ALT
```

The two PID-3 repetitions can be inspected through the parsed field:

```csharp
var pid = parser.Segments[2];
var patientIdentifierField = pid.Fields[3];

Console.WriteLine(patientIdentifierField.RepetitionCount);

for (var repetition = 1;
     repetition <= patientIdentifierField.RepetitionCount;
     repetition++)
{
    var components =
        patientIdentifierField.ComponentsByRepetitionIndexer[repetition];

    Console.WriteLine(components[1].Value);
}
```

Normal callers should iterate only through `RepetitionCount`.

## Buffered parsing

EDIParser supports buffered segment processing for large messages.

Buffered parsing enumerates one segment at a time and raises the `ParsedSegment` event after each segment has been parsed.

The event must be subscribed before enabling buffered parsing or memory conservation.

```csharp
using EDIParser;

var message = File.ReadAllText("large-message.edi");

var parser = new X12Parser();

parser.ParsedSegment += HandleParsedSegment;
parser.SegmentParsingOption =
    Parser.SegmentParsingOptions.Buffered;
parser.ConserveMemory = true;

parser.ParseMsg(message);

static void HandleParsedSegment(
    object sender,
    int segmentNumber,
    ref Segment segment,
    ref bool cancel)
{
    Console.WriteLine($"{segmentNumber}: {segment.Name}");

    // Set cancel to true to stop processing.
    cancel = false;
}
```

Buffered parsing supports:

- incremental segment processing
- event-based segment handling
- cancellation
- progress reporting through the event callback
- reduced retained memory through `ConserveMemory`

When `ConserveMemory` is enabled, accumulated segments are cleared after the `ParsedSegment` event is raised.

## Batch processing

`BatchProcessor` can read EDI data from a stream and raise an event for each parsed segment.

```csharp
using EDIParser;

using var stream = File.OpenRead("messages.edi");

var parser = new X12Parser();
var processor = new BatchProcessor(parser, stream);

processor.Reading += HandleSegment;
processor.Process();

static void HandleSegment(ref Segment segment)
{
    Console.WriteLine(segment.Name);
}
```

A processing range can be configured with:

```csharp
processor.Limit(
    seekPosition: 0,
    seekCount: 100);
```

The meaning of `seekPosition` depends on the segment separator:

- For `"\r\n"` input, it represents a line position.
- For character-delimited input, it represents a byte position.

## Viewer application

The repository includes a .NET 8 Windows Forms Viewer under:

```text
src/EDIParser.Viewer/
```

The Viewer can:

- open X12, HL7, and EDIFACT files
- automatically detect supported formats
- display segments, fields, repetitions, components, and subcomponents
- show tree, text, hexadecimal, and report views
- process messages using buffered parsing
- display progress while parsing
- cancel parsing
- open a file supplied through the command line

The original splash screen and application icons are included.

The original registration, licensing, and trial-expiration features were intentionally excluded from the open-source migration.

## Samples

Sample projects are located under:

```text
src/EDIParser.Samples/
```

The retained samples demonstrate selected scenarios such as:

- parsing EDI messages
- reading values
- updating values
- generating messages
- working with X12
- working with HL7
- working with EDIFACT
- using the parser from console and Windows Forms applications

All publicly distributed sample data should be synthetic or sanitized.

Do not commit production EDI messages, protected health information, personally identifiable information, credentials, certificates, signing keys, or confidential customer data.

Obsolete WCF, registration, licensing, and trial-expiration samples are not part of the primary open-source solution.

## Documentation

Developer guides and the DocFX configuration are located under:

```text
docs/
```

The documentation includes:

- getting started
- parser object model
- X12 usage
- HL7 usage
- EDIFACT usage
- buffered parsing
- sample project guidance
- migration notes
- generated API reference

### Install DocFX

```powershell
dotnet tool install -g docfx
```

To update an existing installation:

```powershell
dotnet tool update -g docfx
```

### Build the documentation

```powershell
docfx .\docs\docfx.json
```

### Build and serve locally

```powershell
docfx .\docs\docfx.json --serve
```

The generated site is written to:

```text
docs/_site/
```

The generated `_site` directory is build output and should not normally be committed to the primary source branch.

## XML API documentation

The Core project is configured to generate XML documentation:

```xml
<GenerateDocumentationFile>true</GenerateDocumentationFile>
```

Public classes, methods, properties, events, and indexers use standard C# XML documentation comments:

```csharp
/// <summary>
/// Parses an EDI message and populates the segment object model.
/// </summary>
/// <param name="msg">The EDI message to parse.</param>
public virtual void ParseMsg(string msg)
{
}
```

DocFX uses these comments to generate the API reference.

## Migration validation

The C# Core migration was validated through:

- a clean .NET 8 Release build
- zero compiler warnings
- zero compiler errors
- parser contract tests
- collection and one-based indexing tests
- VB.NET-versus-C# parity comparisons
- matching X12 parser snapshots
- matching HL7 parser snapshots
- matching EDIFACT parser snapshots
- Windows Forms Viewer validation
- sample project migration
- DocFX API generation

The original `GoTo`-based parser logic was replaced with structured C# control flow while preserving behavior.

This includes:

- replacing `Segment_Jump` with incremental segment enumeration
- replacing `Continue_Field_Loop` with structured loop control
- preserving the original field-counter increment behavior
- preserving one-based public indexing
- preserving numeric segment collection keys
- preserving buffered parsing, cancellation, and event behavior
- preserving format-specific repetition behavior

## Modernization decisions

The migration intentionally:

- targets .NET 8
- uses SDK-style project files
- references the Core project directly from the Viewer and samples
- excludes registration and licensing features
- excludes obsolete WCF projects from the primary solution
- replaces legacy dependencies where practical
- retains selected Windows-specific compatibility where required
- uses DocFX instead of NDoc
- expands the original XML documentation
- adds automated tests around legacy behavior
- preserves behavioral parity before introducing broader API redesigns

A future redesign may introduce more descriptive segment keys, such as:

```text
OBX.1
OBX.2
OBX.3
```

That redesign is intentionally deferred so the current migration can preserve the original numeric-key contract.

## Known limitations

- NCPDP is not currently included in the verified Core build.
- The Windows Forms Viewer is Windows-only.
- OleDb-based samples, when retained, are Windows-specific.
- Some retained sample projects may require external providers or additional local configuration.
- The parser preserves several legacy API and indexing behaviors for compatibility.
- Segment-name lookup is performed through EDI paths rather than through the segment collection string indexer.
- Broader API redesigns are deferred until after migration parity is established.

## Security and data handling

EDIParser processes message content supplied by the calling application.

Applications using the library are responsible for:

- validating untrusted input
- limiting message and stream sizes
- protecting healthcare and personal data
- sanitizing logs and exception output
- securing files and database connections
- complying with applicable privacy and retention requirements

Sample data submitted with an issue or pull request must be synthetic or sanitized.

## Contributing

Issues, documentation improvements, additional sanitized samples, and pull requests are welcome.

When reporting a parsing issue, include:

- the message format
- a synthetic or sanitized sample
- the expected value or structure
- the actual result
- any nonstandard delimiters
- the parser configuration used

Do not include:

- protected health information
- personally identifiable information
- credentials or secrets
- certificates or private keys
- proprietary production messages
- confidential customer data

## License

Copyright 2009-2026 Gary Gallagher

EDIParser is licensed under the Apache License, Version 2.0.

See the [`LICENSE`](LICENSE) file for the complete license terms.
