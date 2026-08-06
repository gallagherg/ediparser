namespace EDIParser;

/// <summary>
/// Provides indexer-based access to values contained within a parser, segment,
/// field, component, or repetition.
/// </summary>
/// <remarks>
/// <para>
/// A <see cref="ValueIndexer"/> is associated with exactly one object level when
/// it is created. The available indexer overloads depend on that object level.
/// </para>
/// <para>
/// Element paths use period-delimited positions such as
/// <c>PID.3.1</c> or <c>REF.2.1.0.1</c>. Segment, field, component,
/// repetition, and subcomponent identifiers are interpreted by
/// <see cref="PathNavigator"/>.
/// </para>
/// </remarks>
public sealed class ValueIndexer
{
    private readonly Parser? _parser;
    private readonly Field? _field;
    private readonly Segment? _segment;
    private readonly Component? _component;
    private readonly Repetition? _repetition;

    /// <summary>
    /// Initializes a value indexer associated with a parser.
    /// </summary>
    /// <param name="parser">The parser used for element-path navigation.</param>
    internal ValueIndexer(Parser parser) => _parser = parser;

    /// <summary>
    /// Initializes a value indexer associated with a field.
    /// </summary>
    /// <param name="field">The field used for element-path navigation.</param>
    internal ValueIndexer(Field field) => _field = field;

    /// <summary>
    /// Initializes a value indexer associated with a segment.
    /// </summary>
    /// <param name="segment">The segment used for element-path navigation.</param>
    internal ValueIndexer(Segment segment) => _segment = segment;

    /// <summary>
    /// Initializes a value indexer associated with a component.
    /// </summary>
    /// <param name="component">
    /// The component used for element-path navigation.
    /// </param>
    internal ValueIndexer(Component component) => _component = component;

    /// <summary>
    /// Initializes a value indexer associated with a repetition.
    /// </summary>
    /// <param name="repetition">
    /// The repetition used for element-path navigation.
    /// </param>
    internal ValueIndexer(Repetition repetition) => _repetition = repetition;

    /// <summary>
    /// Occurs when setting a value causes a new segment to be added to a parser.
    /// </summary>
    /// <remarks>
    /// The event arguments contain the event sender, the one-based position of
    /// the added segment, and the newly created segment.
    /// </remarks>
    public event Action<object, int, Segment>? AddSegment;

    /// <summary>
    /// Gets or sets a value using an EDI element path.
    /// </summary>
    /// <param name="element">
    /// The period-delimited EDI element path identifying the requested value.
    /// </param>
    /// <returns>The value identified by the element path.</returns>
    /// <remarks>
    /// <para>
    /// When associated with a parser, this indexer accesses the first matching
    /// segment and the first field repetition.
    /// </para>
    /// <para>
    /// When associated with a segment, field, component, or repetition, the
    /// element path is resolved relative to that object.
    /// </para>
    /// <para>
    /// Setting a value is supported only when the indexer is associated with a
    /// parser.
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentException">
    /// Thrown when setting a value through an indexer that is not associated with
    /// a parser, or when the element path is empty.
    /// </exception>
    public string this[string element]
    {
        get
        {
            if (_parser is not null)
            {
                return PathNavigator.GetFromParser(
                    _parser,
                    element,
                    1,
                    1);
            }

            if (_field is not null)
            {
                return PathNavigator.GetFromField(
                    _field,
                    element,
                    1);
            }

            if (_segment is not null)
            {
                return PathNavigator.GetFromSegment(
                    _segment,
                    element);
            }

            if (_component is not null)
            {
                return PathNavigator.GetFromComponent(
                    _component,
                    element);
            }

            if (_repetition is not null)
            {
                return PathNavigator.GetFromRepetition(
                    _repetition,
                    element);
            }

            return string.Empty;
        }
        set
        {
            if (_parser is null)
            {
                throw new ArgumentException(
                    "Setting elements at this object level is not supported.");
            }

            PathNavigator.SetOnParser(
                _parser,
                element,
                value ?? string.Empty,
                1,
                1,
                AddSegment);
        }
    }

    /// <summary>
    /// Gets or sets a value in a specified occurrence of a matching segment.
    /// </summary>
    /// <param name="segmentIndex">
    /// The one-based occurrence of the segment identified by the element path.
    /// </param>
    /// <param name="element">
    /// The period-delimited EDI element path identifying the requested value.
    /// </param>
    /// <returns>
    /// The value identified by the segment occurrence and element path.
    /// </returns>
    /// <remarks>
    /// The segment index is relative to segments having the segment name specified
    /// by the first part of <paramref name="element"/>. It is not the absolute
    /// position in the complete segment collection.
    /// </remarks>
    /// <exception cref="ArgumentException">
    /// Thrown when setting a value through an indexer that is not associated with
    /// a parser, or when the element path is empty.
    /// </exception>
    /// <exception cref="IndexOutOfRangeException">
    /// Thrown when <paramref name="segmentIndex"/> is less than one while setting
    /// a value.
    /// </exception>
    public string this[int segmentIndex, string element]
    {
        get =>
            _parser is null
                ? string.Empty
                : PathNavigator.GetFromParser(
                    _parser,
                    element,
                    segmentIndex,
                    1);

        set
        {
            if (_parser is null)
            {
                throw new ArgumentException(
                    "Indexer is only valid for a parser.");
            }

            PathNavigator.SetOnParser(
                _parser,
                element,
                value ?? string.Empty,
                segmentIndex,
                1,
                AddSegment);
        }
    }

    /// <summary>
    /// Gets or sets a value in a specified segment occurrence and field
    /// repetition.
    /// </summary>
    /// <param name="segmentIndex">
    /// The one-based occurrence of the segment identified by the element path.
    /// </param>
    /// <param name="element">
    /// The period-delimited EDI element path identifying the requested value.
    /// </param>
    /// <param name="fieldRepetitionIndex">
    /// The one-based field-repetition position.
    /// </param>
    /// <returns>
    /// The value identified by the segment occurrence, element path, and field
    /// repetition.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when setting a value through an indexer that is not associated with
    /// a parser, or when the element path is empty.
    /// </exception>
    /// <exception cref="IndexOutOfRangeException">
    /// Thrown when either index is less than one while setting a value.
    /// </exception>
    public string this[
        int segmentIndex,
        string element,
        int fieldRepetitionIndex]
    {
        get =>
            _parser is null
                ? string.Empty
                : PathNavigator.GetFromParser(
                    _parser,
                    element,
                    segmentIndex,
                    fieldRepetitionIndex);

        set
        {
            if (_parser is null)
            {
                throw new ArgumentException(
                    "Indexer is only valid for a parser.");
            }

            PathNavigator.SetOnParser(
                _parser,
                element,
                value ?? string.Empty,
                segmentIndex,
                fieldRepetitionIndex,
                AddSegment);
        }
    }

    /// <summary>
    /// Gets or sets the value of a specified field repetition.
    /// </summary>
    /// <param name="fieldRepetitionIndex">
    /// The one-based field-repetition index.
    /// </param>
    /// <returns>The value of the specified field repetition.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when setting a value through an indexer that is not associated with
    /// a field.
    /// </exception>
    /// <exception cref="IndexOutOfRangeException">
    /// Thrown when the repetition index is outside the field's supported range.
    /// </exception>
    public string this[int fieldRepetitionIndex]
    {
        get =>
            _field?.GetValue(fieldRepetitionIndex) ??
            string.Empty;

        set
        {
            if (_field is null)
            {
                throw new ArgumentException(
                    "Indexer is only valid for a field.");
            }

            _field.SetValue(
                value ?? string.Empty,
                fieldRepetitionIndex);
        }
    }

    /// <summary>
    /// Gets a nested value from a specified field repetition or sets the value of
    /// that repetition.
    /// </summary>
    /// <param name="element">
    /// The period-delimited element path identifying the nested value.
    /// </param>
    /// <param name="fieldRepetitionIndex">
    /// The one-based field-repetition index.
    /// </param>
    /// <returns>
    /// The nested value identified by the element path and field repetition.
    /// </returns>
    /// <remarks>
    /// The getter resolves the supplied element path. The setter assigns the
    /// complete value of the specified field repetition.
    /// </remarks>
    /// <exception cref="ArgumentException">
    /// Thrown when setting a value through an indexer that is not associated with
    /// a field.
    /// </exception>
    /// <exception cref="IndexOutOfRangeException">
    /// Thrown when the repetition index is outside the field's supported range.
    /// </exception>
    public string this[
        string element,
        int fieldRepetitionIndex]
    {
        get =>
            _field?.GetValue(
                element,
                fieldRepetitionIndex) ??
            string.Empty;

        set
        {
            if (_field is null)
            {
                throw new ArgumentException(
                    "Indexer is only valid for a field.");
            }

            _field.SetValue(
                value ?? string.Empty,
                fieldRepetitionIndex);
        }
    }
}

/// <summary>
/// Resolves and modifies values within the parser object model using
/// period-delimited EDI element paths.
/// </summary>
/// <remarks>
/// <para>
/// Element paths are interpreted hierarchically as segment, field, component,
/// repetition, and subcomponent identifiers.
/// </para>
/// <para>
/// A repetition identifier of <c>0</c> indicates that subcomponent navigation
/// applies directly to the component rather than to a repeated component value.
/// </para>
/// </remarks>
internal static class PathNavigator
{
    /// <summary>
    /// Gets a value from a specified occurrence of a matching parser segment.
    /// </summary>
    /// <param name="parser">The parser containing the message object model.</param>
    /// <param name="element">
    /// The period-delimited element path identifying the requested value.
    /// </param>
    /// <param name="segmentIndex">
    /// The one-based occurrence of the segment name specified by the element path.
    /// </param>
    /// <param name="fieldRepeatIndex">
    /// The one-based field-repetition index.
    /// </param>
    /// <returns>
    /// The requested value, or an empty string when the segment or nested value is
    /// not found.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="element"/> is empty or consists only of white
    /// space.
    /// </exception>
    internal static string GetFromParser(
        Parser parser,
        string element,
        int segmentIndex,
        int fieldRepeatIndex)
    {
        if (string.IsNullOrWhiteSpace(element))
        {
            throw new ArgumentException(
                "Element path cannot be empty.",
                nameof(element));
        }

        var parts = Parts(element);

        if (parts.Length == 0)
            return string.Empty;

        var match = parser.Segments
            .Cast<Segment>()
            .Where(segment => segment.Name == parts[0])
            .Skip(Math.Max(0, segmentIndex - 1))
            .FirstOrDefault();

        return match is null
            ? string.Empty
            : GetFromSegment(
                match,
                element,
                fieldRepeatIndex);
    }

    /// <summary>
    /// Gets a value from a segment using an EDI element path.
    /// </summary>
    /// <param name="segment">The segment from which to retrieve the value.</param>
    /// <param name="element">
    /// The period-delimited element path identifying the requested value.
    /// </param>
    /// <param name="fieldRepeatIndex">
    /// The one-based field-repetition index.
    /// </param>
    /// <returns>
    /// The requested value, the complete segment value when no field is specified,
    /// or an empty string when the field is not found.
    /// </returns>
    internal static string GetFromSegment(
        Segment segment,
        string element,
        int fieldRepeatIndex = 1)
    {
        var parts = Parts(element);

        if (parts.Length < 2)
            return segment.Value;

        var field = segment.Fields
            .Cast<Field>()
            .FirstOrDefault(
                candidate => candidate.Name == parts[1]);

        if (field is null)
            return string.Empty;

        return GetFromField(
            field,
            element,
            fieldRepeatIndex);
    }

    /// <summary>
    /// Gets a value from a field using an EDI element path.
    /// </summary>
    /// <param name="field">The field from which to retrieve the value.</param>
    /// <param name="element">
    /// The period-delimited element path identifying the requested value.
    /// </param>
    /// <param name="fieldRepeatIndex">
    /// The one-based field-repetition index.
    /// </param>
    /// <returns>
    /// The requested field or component value, or an empty string when it cannot
    /// be resolved.
    /// </returns>
    internal static string GetFromField(
        Field field,
        string element,
        int fieldRepeatIndex)
    {
        var parts = Parts(element);

        if (fieldRepeatIndex < 1 ||
            fieldRepeatIndex >
            field.ValueByRepetitionIndexer.Length)
        {
            return string.Empty;
        }

        if (parts.Length < 3)
            return field.GetValue(fieldRepeatIndex);

        if (fieldRepeatIndex >
            field.ComponentsByRepetitionIndexer.Length)
        {
            return string.Empty;
        }

        var component =
            field.ComponentsByRepetitionIndexer[
                    fieldRepeatIndex]
                .Cast<Component>()
                .FirstOrDefault(
                    candidate =>
                        candidate.Name == parts[2]);

        return component is null
            ? string.Empty
            : GetFromComponent(
                component,
                element);
    }

    /// <summary>
    /// Gets a value from a component using an EDI element path.
    /// </summary>
    /// <param name="component">
    /// The component from which to retrieve the value.
    /// </param>
    /// <param name="element">
    /// The period-delimited element path identifying the requested value.
    /// </param>
    /// <returns>
    /// The component, repetition, or subcomponent value identified by the path,
    /// or an empty string when it cannot be resolved.
    /// </returns>
    internal static string GetFromComponent(
        Component component,
        string element)
    {
        var parts = Parts(element);

        if (parts.Length < 4)
            return component.Value;

        if (parts[3] != "0")
        {
            var repetition = component.Repetitions
                .Cast<Repetition>()
                .FirstOrDefault(
                    candidate =>
                        candidate.Name == parts[3]);

            if (repetition is null)
            {
                if (parts[3] == "1" &&
                    component.Repetitions.Count == 0)
                {
                    return GetComponentSubComponent(
                        component,
                        parts);
                }

                return string.Empty;
            }

            return GetFromRepetition(
                repetition,
                element);
        }

        return GetComponentSubComponent(
            component,
            parts);
    }

    /// <summary>
    /// Gets a subcomponent value directly from a component.
    /// </summary>
    /// <param name="component">
    /// The component containing the requested subcomponent.
    /// </param>
    /// <param name="parts">
    /// The parsed element-path parts.
    /// </param>
    /// <returns>
    /// The requested subcomponent value, the component value for the implicit
    /// first subcomponent, or an empty string when no value is found.
    /// </returns>
    private static string GetComponentSubComponent(
        Component component,
        string[] parts)
    {
        if (parts.Length < 5)
            return component.Value;

        var subComponent = component.SubComponents
            .Cast<SubComponent>()
            .FirstOrDefault(
                candidate =>
                    candidate.Name == parts[4]);

        if (subComponent is null &&
            parts[4] == "1" &&
            component.SubComponents.Count == 0)
        {
            return component.Value;
        }

        return subComponent?.Value ??
               string.Empty;
    }

    /// <summary>
    /// Gets a value from a component repetition using an EDI element path.
    /// </summary>
    /// <param name="repetition">
    /// The repetition from which to retrieve the value.
    /// </param>
    /// <param name="element">
    /// The period-delimited element path identifying the requested value.
    /// </param>
    /// <returns>
    /// The repetition or subcomponent value identified by the path, or an empty
    /// string when it cannot be resolved.
    /// </returns>
    internal static string GetFromRepetition(
        Repetition repetition,
        string element)
    {
        var parts = Parts(element);

        if (parts.Length < 5)
            return repetition.Value;

        var subComponent = repetition.SubComponents
            .Cast<SubComponent>()
            .FirstOrDefault(
                candidate =>
                    candidate.Name == parts[4]);

        if (subComponent is null &&
            parts[4] == "1" &&
            repetition.SubComponents.Count == 0)
        {
            return repetition.Value;
        }

        return subComponent?.Value ??
               string.Empty;
    }

    /// <summary>
    /// Sets a parser value using an EDI element path, creating missing object-model
    /// nodes as required.
    /// </summary>
    /// <param name="parser">The parser containing the object model to update.</param>
    /// <param name="element">
    /// The period-delimited EDI element path identifying the value to set.
    /// </param>
    /// <param name="value">The value to assign.</param>
    /// <param name="segmentIndex">
    /// The one-based occurrence of the segment name specified by the element path.
    /// </param>
    /// <param name="fieldRepeatIndex">
    /// The one-based field-repetition index.
    /// </param>
    /// <param name="added">
    /// An optional callback invoked whenever a new segment is created.
    /// </param>
    /// <remarks>
    /// Missing segments, fields, components, repetitions, and subcomponents are
    /// created as necessary. A repetition path value of <c>0</c> directs
    /// subcomponent creation to the component itself.
    /// </remarks>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="element"/> is empty or consists only of white
    /// space.
    /// </exception>
    /// <exception cref="IndexOutOfRangeException">
    /// Thrown when <paramref name="segmentIndex"/> or
    /// <paramref name="fieldRepeatIndex"/> is less than one.
    /// </exception>
    internal static void SetOnParser(
        Parser parser,
        string element,
        string value,
        int segmentIndex,
        int fieldRepeatIndex,
        Action<object, int, Segment>? added)
    {
        if (string.IsNullOrWhiteSpace(element))
        {
            throw new ArgumentException(
                "Element path cannot be empty.",
                nameof(element));
        }

        var parts = Parts(element);

        if (parts.Length == 0)
        {
            throw new ArgumentException(
                "Element cannot be empty.",
                nameof(element));
        }

        if (segmentIndex < 1 ||
            fieldRepeatIndex < 1)
        {
            throw new IndexOutOfRangeException();
        }

        var matching = parser.Segments
            .Cast<Segment>()
            .Where(segment => segment.Name == parts[0])
            .ToList();

        Segment segment;

        if (segmentIndex <= matching.Count)
        {
            segment = matching[segmentIndex - 1];
        }
        else
        {
            while (matching.Count < segmentIndex)
            {
                segment =
                    new Segment(parser.IgnoreMissingItem)
                    {
                        Name = parts[0]
                    };

                parser.AddSegment(segment);
                matching.Add(segment);

                added?.Invoke(
                    parser.ValueIndexer,
                    parser.Segments.Count,
                    segment);
            }

            segment = matching[segmentIndex - 1];
        }

        if (parts.Length == 1)
        {
            segment.Value = value;
            return;
        }

        var field = segment.Fields
            .Cast<Field>()
            .FirstOrDefault(
                candidate =>
                    candidate.Name == parts[1]);

        if (field is null)
        {
            field =
                new Field(parser.IgnoreMissingItem)
                {
                    Name = parts[1]
                };

            segment.Fields.Add(
                field,
                parts[1]);
        }

        if (parts.Length == 2)
        {
            field.SetValue(
                value,
                fieldRepeatIndex);

            return;
        }

        while (field.ComponentsByRepetitionIndexer.Length <
               fieldRepeatIndex)
        {
            field.ComponentsByRepetitionIndexer[
                    field.ComponentsByRepetitionIndexer.Length +
                    1] =
                new Components
                {
                    IgnoreMissingItem =
                        parser.IgnoreMissingItem
                };
        }

        var components =
            field.ComponentsByRepetitionIndexer[
                fieldRepeatIndex];

        var component = components
            .Cast<Component>()
            .FirstOrDefault(
                candidate =>
                    candidate.Name == parts[2]);

        if (component is null)
        {
            component =
                new Component(parser.IgnoreMissingItem)
                {
                    Name = parts[2]
                };

            components.Add(
                component,
                components.Count.ToString());
        }

        if (parts.Length == 3)
        {
            component.Value = value;
            return;
        }

        if (parts[3] != "0")
        {
            var repetition = component.Repetitions
                .Cast<Repetition>()
                .FirstOrDefault(
                    candidate =>
                        candidate.Name == parts[3]);

            if (repetition is null)
            {
                repetition =
                    new Repetition(
                        parser.IgnoreMissingItem)
                    {
                        Name = parts[3]
                    };

                component.Repetitions.Add(
                    repetition,
                    component.Repetitions.Count
                        .ToString());

                component.HasRepetition = true;
            }

            if (parts.Length == 4)
            {
                repetition.Value = value;
                return;
            }

            var subComponent =
                repetition.SubComponents
                    .Cast<SubComponent>()
                    .FirstOrDefault(
                        candidate =>
                            candidate.Name == parts[4]);

            if (subComponent is null)
            {
                subComponent =
                    new SubComponent(
                        parser.IgnoreMissingItem)
                    {
                        Name = parts[4]
                    };

                repetition.SubComponents.Add(
                    subComponent,
                    repetition.SubComponents.Count
                        .ToString());

                repetition.HasSubComponents = true;
            }

            subComponent.Value = value;
            return;
        }

        if (parts.Length < 5)
        {
            component.Value = value;
            return;
        }

        var componentSub =
            component.SubComponents
                .Cast<SubComponent>()
                .FirstOrDefault(
                    candidate =>
                        candidate.Name == parts[4]);

        if (componentSub is null)
        {
            componentSub =
                new SubComponent(
                    parser.IgnoreMissingItem)
                {
                    Name = parts[4]
                };

            component.SubComponents.Add(
                componentSub,
                component.SubComponents.Count
                    .ToString());

            component.HasSubComponents = true;
        }

        componentSub.Value = value;
    }

    /// <summary>
    /// Splits an EDI element path into its period-delimited parts.
    /// </summary>
    /// <param name="element">The element path to split.</param>
    /// <returns>
    /// The element-path parts, including empty entries when the path contains
    /// consecutive delimiters.
    /// </returns>
    private static string[] Parts(string element) =>
        (element ?? string.Empty).Split(
            '.',
            StringSplitOptions.None);
}