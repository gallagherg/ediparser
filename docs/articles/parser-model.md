# Parser Model

EDIParser represents each message as a hierarchy of segments and nested values.

## Segment

A segment contains a name, its original text value, and a collection of fields.

```csharp
foreach (Segment segment in parser.Segments)
{
    Console.WriteLine(segment.Name);
}
```

## Field

Fields are addressed using one-based positions.

```csharp
Field firstField = segment.Fields[1];
```

## Repetition

A field may contain one or more repetitions. Repetition positions are also one-based.

```csharp
var components = field.ComponentsByRepetitionIndexer[1];
```

## Component and subcomponent

```csharp
foreach (EDIParser.Component component in components)
{
    Console.WriteLine(component.Value);

    foreach (SubComponent subComponent in component.SubComponents)
    {
        Console.WriteLine(subComponent.Value);
    }
}
```

## Why explicit loop types matter

Some compatibility collections expose a non-generic enumerator. Use explicit types in `foreach` statements:

```csharp
foreach (Field field in segment.Fields)
{
    // ...
}
```

Using `var` may cause the compiler to infer `object` for these legacy-compatible collections.
