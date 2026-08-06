# HL7

Use `HL7Parser` for delimiter-based HL7 v2 messages.

```csharp
using EDIParser;

var parser = new HL7Parser
{
    Message = File.ReadAllText("adt-a01.hl7")
};

parser.ParseMsg();

var patientIdentifier = parser.GetValue("PID.3.1", 1, 1);
var familyName = parser.GetValue("PID.5.1", 1, 1);
```

## HL7 separators

The `MSH` segment defines the encoding characters. Typical values are:

- field separator: `|`
- component separator: `^`
- repetition separator: `~`
- escape character: `\\`
- subcomponent separator: `&`

## Segment terminators

Use the segment terminator expected by the parser and input file. The migrated Viewer and parity tests use CRLF where configured.
