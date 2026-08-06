# EDIFACT

Use `EdiFactParser` for UN/EDIFACT messages.

```csharp
using EDIParser;

var parser = new EdiFactParser
{
    Message = File.ReadAllText("orders.edi")
};

parser.ParseMsg();

var documentNumber = parser.GetValue("BGM.2", 1, 1);
```

## Typical separators

- segment terminator: `'`
- data element separator: `+`
- component separator: `:`

## Example

```text
UNB+UNOC:3+SENDER+RECEIVER+260731:1512+1'
UNH+1+ORDERS:D:96A:UN'
BGM+220+12345+9'
UNT+3+1'
UNZ+1+1'
```
