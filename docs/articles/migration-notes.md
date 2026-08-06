# Migration Notes

The .NET 8 version is a behavior-preserving migration of the original VB.NET parser and Viewer.

## Verified Core behavior

- clean Release build with warnings treated as errors
- X12 VB/C# parity
- HL7 VB/C# parity
- EDIFACT VB/C# parity
- one-based public index behavior
- structured replacement of legacy `GoTo` flow

## Viewer migration

The Viewer references `EDIParser.Core` and includes:

- file opening and parser selection
- tree, text, hexadecimal, and report views
- progress and cancellation
- splash screen and application icons

Registration and licensing screens were intentionally excluded.

## Compatibility considerations

- Some public collections retain legacy-compatible enumeration behavior.
- Public parser indexes remain one-based.
- NCPDP is not part of the verified .NET 8 Core.
