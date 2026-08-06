# EDIParser

EDIParser is an open-source .NET 8 library for parsing and generating X12, HL7, and EDIFACT messages.

The library exposes a hierarchical message model:

```text
Message
└── Segment
    └── Field
        └── Repetition
            └── Component
                └── SubComponent
```

## Start here

- [Getting Started](articles/getting-started.md)
- [Parser Model](articles/parser-model.md)
- [X12 Guide](articles/x12.md)
- [HL7 Guide](articles/hl7.md)
- [EDIFACT Guide](articles/edifact.md)
- [Sample Projects](articles/samples.md)
- [API Reference](api/toc.yml)

## Supported formats

| Format | Status |
|---|---|
| X12 | Supported |
| HL7 v2 delimiter-based messages | Supported |
| UN/EDIFACT | Supported |
| NCPDP SCRIPT | Not included in the verified .NET 8 Core |

## Indexing convention

The public parser API preserves the original one-based EDI indexing convention. For example, field `3` is addressed as field 3, even though internal CLR collections are zero-based.
