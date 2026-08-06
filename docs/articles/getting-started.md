# Getting Started

## Requirements

- .NET 8 SDK
- Visual Studio 2022 or another .NET 8-compatible IDE

## Reference the project

Add a project reference to `EDIParser.Core`:

```xml
<ItemGroup>
  <ProjectReference Include="..\src\EDIParser.Core\EDIParser.Core.csproj" />
</ItemGroup>
```

## Parse a message

```csharp
using EDIParser;

var parser = new X12Parser
{
    Message = File.ReadAllText("purchase-order.edi")
};

parser.ParseMsg();

Console.WriteLine($"Segments: {parser.SegmentCount}");
```

## Read a value

The parser uses one-based EDI positions:

```csharp
var purchaseOrderNumber = parser.GetValue("BEG.3", 1, 1);
```

## Modify a value

```csharp
parser.SetValue("BEG.3", "PO-10025", 1, 1);
```

## Generate the message

```csharp
var output = parser.GenerateMessage();
File.WriteAllText("purchase-order-updated.edi", output);
```

> [!NOTE]
> Exact parser member names may vary by class. Use the generated [API reference](../api/toc.yml) for the authoritative signatures.
