# EDIParser.Core.Tests

This .NET 8 xUnit project preserves the verified behavior of `EDIParser.Core`.

## Add it to the solution

From the repository root:

```powershell
dotnet sln EDIParser.sln add tests/EDIParser.Core.Tests/EDIParser.Core.Tests.csproj
```

The project reference assumes this repository layout:

```text
src/EDIParser.Core/EDIParser.Core.csproj
tests/EDIParser.Core.Tests/EDIParser.Core.Tests.csproj
```

## Run the tests

```powershell
dotnet restore
dotnet build -c Release
dotnet test -c Release
```

## Coverage

```powershell
dotnet test -c Release --collect:"XPlat Code Coverage"
```

The initial suite covers:

- X12, HL7, and EDIFACT parsing
- one-based public collection indexes
- repeated segment lookup
- `GetValue` and `SetValue`
- message regeneration and round trips
- MSH, ISA, and UNA delimiter handling
- structured replacement of `Continue_Field_Loop`
- buffered parsing, event order, and cancellation
- `ConserveMemory`
- missing-item behavior
- character-delimited `BatchProcessor` behavior
- malformed short-message validation
