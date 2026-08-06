using System.ComponentModel;
using System.Text.RegularExpressions;

namespace EDIParser;

/// <summary>
/// Provides the base implementation for parsing, navigating, modifying, and
/// rebuilding EDI messages.
/// </summary>
/// <remarks>
/// <para>
/// The parser converts an EDI message into a hierarchical object model consisting
/// of segments, fields, components, repetitions, and subcomponents.
/// </para>
/// <para>
/// Derived parser classes configure the delimiters and parser type required for a
/// particular EDI standard, such as X12, HL7, or EDIFACT.
/// </para>
/// <para>
/// Collection positions and segment indexes are one-based to preserve the behavior
/// of the original VB implementation.
/// </para>
/// </remarks>
public class Parser : System.ComponentModel.Component
{
    /// <summary>
    /// Identifies the EDI standard-specific parsing behavior used by the parser.
    /// </summary>
    internal enum ParserTypeEnum
    {
        /// <summary>
        /// Indicates an ANSI X12 message.
        /// </summary>
        X12 = 1,

        /// <summary>
        /// Indicates an HL7 message.
        /// </summary>
        HL7 = 2,

        /// <summary>
        /// Indicates an EDIFACT message.
        /// </summary>
        EdiFact = 3
    }

    /// <summary>
    /// Defines how source messages are divided into segments during parsing.
    /// </summary>
    public enum SegmentParsingOptions
    {
        /// <summary>
        /// Loads and separates all message segments in memory before parsing.
        /// </summary>
        InMemory = 1,

        /// <summary>
        /// Enumerates and parses one segment at a time without allocating an array
        /// containing every segment.
        /// </summary>
        /// <remarks>
        /// Buffered parsing requires a subscriber to the
        /// <see cref="ParsedSegment"/> event.
        /// </remarks>
        Buffered = 2
    }

    /// <summary>
    /// Represents the method that handles the <see cref="ParsedSegment"/> event.
    /// </summary>
    /// <param name="sender">The parser that raised the event.</param>
    /// <param name="segmentNbr">
    /// The one-based position of the segment in the source message.
    /// </param>
    /// <param name="segment">A reference to the parsed segment.</param>
    /// <param name="cancel">
    /// Set to <see langword="true"/> to stop parsing after the current segment.
    /// </param>
    public delegate void ParsedSegmentEventHandler(
        object sender,
        int segmentNbr,
        ref Segment segment,
        ref bool cancel);

    /// <summary>
    /// Occurs after a segment has been parsed and added to the segment collection.
    /// </summary>
    /// <remarks>
    /// The event handler may set the cancellation argument to
    /// <see langword="true"/> to stop parsing the remaining message.
    /// </remarks>
    public event ParsedSegmentEventHandler? ParsedSegment;

    private readonly Segments _segments = new();
    private bool _subComponentSeparatorWasSet;
    private int _segmentCount;
    private ParserTypeEnum _parserType = ParserTypeEnum.X12;
    private SegmentParsingOptions _segmentParsingOption =
        SegmentParsingOptions.InMemory;

    /// <summary>
    /// Initializes a new instance of the <see cref="Parser"/> class.
    /// </summary>
    public Parser()
    {
        ValueIndexer = new ValueIndexer(this);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Parser"/> class using the
    /// legacy constructor signature.
    /// </summary>
    /// <param name="_">
    /// An unused value retained for compatibility with the original API.
    /// </param>
    internal Parser(object _) : this()
    {
    }

    /// <summary>
    /// Gets the indexer used to retrieve or update message values using EDI
    /// element paths.
    /// </summary>
    public ValueIndexer ValueIndexer { get; }

    private bool _ignoreMissingItem = true;

    /// <summary>
    /// Gets or sets a value indicating whether missing collection items return
    /// placeholder objects instead of throwing exceptions.
    /// </summary>
    /// <value>
    /// <see langword="true"/> to return placeholder items for missing collection
    /// entries; otherwise, <see langword="false"/>.
    /// </value>
    /// <remarks>
    /// Changing this property also updates the behavior of the parser's segment
    /// collection. Newly parsed child collections inherit the setting when they
    /// are created.
    /// </remarks>
    public bool IgnoreMissingItem
    {
        get => _ignoreMissingItem;
        set
        {
            _ignoreMissingItem = value;
            _segments.IgnoreMissingItem = value;
        }
    }

    private bool _conserveMemory;

    /// <summary>
    /// Gets or sets a value indicating whether parsed segments are removed from
    /// the parser after the <see cref="ParsedSegment"/> event is raised.
    /// </summary>
    /// <value>
    /// <see langword="true"/> to clear accumulated segments after each event;
    /// otherwise, <see langword="false"/>.
    /// </value>
    /// <remarks>
    /// This option supports stream-oriented processing where the caller handles
    /// each segment through <see cref="ParsedSegment"/> and does not need the
    /// complete message retained in memory.
    /// </remarks>
    /// <exception cref="ApplicationException">
    /// Thrown when the property is set to <see langword="true"/> and no
    /// <see cref="ParsedSegment"/> event handler is registered.
    /// </exception>
    public bool ConserveMemory
    {
        get => _conserveMemory;
        set
        {
            if (value && ParsedSegment is null)
            {
                throw new ApplicationException(
                    "ConserveMemory can only be set to true when used with the ParsedSegment event.");
            }

            _conserveMemory = value;
        }
    }

    /// <summary>
    /// Gets or sets the strategy used to divide a message into segments.
    /// </summary>
    /// <value>
    /// One of the <see cref="SegmentParsingOptions"/> values.
    /// </value>
    /// <exception cref="ApplicationException">
    /// Thrown when the property is set to
    /// <see cref="SegmentParsingOptions.Buffered"/> and no
    /// <see cref="ParsedSegment"/> event handler is registered.
    /// </exception>
    public SegmentParsingOptions SegmentParsingOption
    {
        get => _segmentParsingOption;
        set
        {
            if (value == SegmentParsingOptions.Buffered &&
                ParsedSegment is null)
            {
                throw new ApplicationException(
                    "SegmentParsingOption can only be set to Buffered when used with the ParsedSegment event.");
            }

            _segmentParsingOption = value;
        }
    }

    /// <summary>
    /// Gets or sets the delimiter used to separate segments in a message.
    /// </summary>
    /// <remarks>
    /// Derived parser classes normally configure this property for their
    /// corresponding EDI standard.
    /// </remarks>
    public virtual string SegmentSeparator { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the delimiter used to separate fields within a segment.
    /// </summary>
    public string FieldSeparator { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the complete delimiter definition used by parser-specific
    /// replacement tokens.
    /// </summary>
    internal string Delimiters { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the delimiter used to separate components within a field.
    /// </summary>
    public string ComponentSeparator { get; set; } = string.Empty;

    private string _subComponentSeparator = string.Empty;

    /// <summary>
    /// Gets or sets the delimiter used to separate subcomponents.
    /// </summary>
    /// <remarks>
    /// Setting this property records that subcomponent parsing was explicitly
    /// configured. An empty separator disables subcomponent parsing.
    /// </remarks>
    public string SubComponentSeparator
    {
        get => _subComponentSeparator;
        set
        {
            _subComponentSeparator = value ?? string.Empty;
            _subComponentSeparatorWasSet = true;
        }
    }

    /// <summary>
    /// Gets or sets the delimiter used to separate repeated values.
    /// </summary>
    /// <remarks>
    /// HL7 uses this delimiter for repeated fields. X12 uses it for repeated
    /// component values.
    /// </remarks>
    public string RepetitionSeparator { get; set; } = string.Empty;

    /// <summary>
    /// Gets the segments parsed from the current message.
    /// </summary>
    /// <remarks>
    /// Segment positions are one-based. String keys currently contain the
    /// one-based numeric position assigned during parsing.
    /// </remarks>
    public Segments Segments => _segments;

    /// <summary>
    /// Gets the segment-count value recorded by the current parsing operation.
    /// </summary>
    /// <remarks>
    /// In-memory parsing preserves the original VB upper-bound semantics. Buffered
    /// parsing increments this value as segments are enumerated.
    /// </remarks>
    internal int SegmentCount => _segmentCount;

    /// <summary>
    /// Gets or sets the EDI standard-specific behavior used during parsing.
    /// </summary>
    internal ParserTypeEnum ParserType
    {
        get => _parserType;
        set => _parserType = value;
    }

    /// <summary>
    /// Gets or sets the date and time associated with the beginning of a parsing
    /// operation.
    /// </summary>
    internal DateTime StartDateTime { get; set; }

    /// <summary>
    /// Removes all segments associated with the current message.
    /// </summary>
    private void ClearMsg() => _segments.Clear();

    /// <summary>
    /// Parses an EDI message and populates the segment object model.
    /// </summary>
    /// <param name="msg">The EDI message to parse.</param>
    /// <remarks>
    /// <para>
    /// Null characters are removed before parsing. Existing parsed segments are
    /// cleared before the new message is processed.
    /// </para>
    /// <para>
    /// Segment, field, component, repetition, and subcomponent behavior depends
    /// on the delimiters and parser type configured by the derived parser.
    /// </para>
    /// <para>
    /// When <see cref="SegmentParsingOption"/> is
    /// <see cref="SegmentParsingOptions.Buffered"/>, segments are enumerated
    /// lazily and the <see cref="ParsedSegment"/> event is raised as each segment
    /// is completed.
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="msg"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the segment, field, or component separator has not been
    /// configured.
    /// </exception>
    public virtual void ParseMsg(string msg)
    {
        ArgumentNullException.ThrowIfNull(msg);

        if (string.IsNullOrEmpty(SegmentSeparator))
        {
            throw new InvalidOperationException(
                "SegmentSeparator must be configured before parsing.");
        }

        if (string.IsNullOrEmpty(FieldSeparator))
        {
            throw new InvalidOperationException(
                "FieldSeparator must be configured before parsing.");
        }

        if (string.IsNullOrEmpty(ComponentSeparator))
        {
            throw new InvalidOperationException(
                "ComponentSeparator must be configured before parsing.");
        }

        ClearMsg();
        _segmentCount = 0;

        var normalized = msg.Replace(
            "\0",
            string.Empty,
            StringComparison.Ordinal);

        var invalidSegmentName = new Regex(
            "[^a-zA-Z0-9]",
            RegexOptions.Compiled);

        IEnumerable<string> rawSegments;

        if (_segmentParsingOption ==
            SegmentParsingOptions.InMemory)
        {
            var splitSegments = normalized.Split(
                [SegmentSeparator],
                StringSplitOptions.None);

            // VB records UBound(array), not array length.
            _segmentCount =
                Math.Max(0, splitSegments.Length - 1);

            rawSegments = splitSegments;
        }
        else
        {
            // This is intentionally lazy. It replaces Segment_Jump without
            // eagerly allocating every segment and preserves event and
            // cancellation timing.
            rawSegments = EnumerateBufferedSegments(
                normalized,
                SegmentSeparator);
        }

        var oneBasedSegmentNumber = 0;

        foreach (var rawSegment in rawSegments)
        {
            oneBasedSegmentNumber++;

            if (_segmentParsingOption ==
                SegmentParsingOptions.Buffered)
            {
                _segmentCount++;
            }

            var rawFields = rawSegment.Split(
                [FieldSeparator],
                StringSplitOptions.None);

            if (rawFields.Length == 0 ||
                rawFields[0] == string.Empty)
            {
                // VB: If m_arFields(0) = "" Then Exit For
                break;
            }

            var segment = new Segment(IgnoreMissingItem)
            {
                Name = rawFields[0]
                    .Replace(
                        ComponentSeparator,
                        string.Empty,
                        StringComparison.Ordinal)
                    .TrimStart(),

                Value = rawSegment
            };

            if (invalidSegmentName.IsMatch(segment.Name) ||
                segment.Name.Trim().Length == 0)
            {
                continue;
            }

            _segments.Add(
                segment,
                oneBasedSegmentNumber.ToString());

            if (rawFields.Length > 1)
            {
                for (var fieldNumber = 1;
                     fieldNumber < rawFields.Length;
                     fieldNumber++)
                {
                    var field =
                        new Field(IgnoreMissingItem)
                        {
                            Name = fieldNumber.ToString(),
                            Value = rawFields[fieldNumber]
                        };

                    segment.Fields.Add(
                        field,
                        fieldNumber.ToString());

                    // These branches are the structured equivalents of
                    // GoTo Continue_Field_Loop.
                    if (field.Value ==
                        "<Field_Delimiter>")
                    {
                        field.Value = FieldSeparator;
                        continue;
                    }

                    if (field.Value ==
                        "<Replace_Delimiters>")
                    {
                        field.Value = Delimiters;
                        continue;
                    }

                    if (field.Value ==
                        "[Component_Delimiter]")
                    {
                        field.Value = ComponentSeparator;
                        continue;
                    }

                    var fieldRepetitions =
                        _parserType == ParserTypeEnum.HL7
                            ? rawFields[fieldNumber].Split(
                                [RepetitionSeparator],
                                StringSplitOptions.None)
                            : [rawFields[fieldNumber]];

                    // Continue_Field_Loop is outside the repetition loop in VB.
                    // Therefore, a field with no component separator abandons
                    // all remaining repetitions for this field, not only the
                    // current repetition.
                    var continueOuterFieldLoop = false;

                    for (var zeroBasedFieldRepetition = 0;
                         zeroBasedFieldRepetition <
                         fieldRepetitions.Length;
                         zeroBasedFieldRepetition++)
                    {
                        var oneBasedFieldRepetition =
                            zeroBasedFieldRepetition + 1;

                        field.SetValue(
                            fieldRepetitions[
                                zeroBasedFieldRepetition],
                            oneBasedFieldRepetition);

                        var rawComponents =
                            fieldRepetitions[
                                    zeroBasedFieldRepetition]
                                .Split(
                                    [ComponentSeparator],
                                    StringSplitOptions.None);

                        if (rawComponents.Length == 1)
                        {
                            continueOuterFieldLoop = true;
                            break;
                        }

                        if (oneBasedFieldRepetition > 1)
                        {
                            field.ComponentsByRepetitionIndexer[
                                    oneBasedFieldRepetition] =
                                new Components
                                {
                                    IgnoreMissingItem =
                                        IgnoreMissingItem
                                };
                        }

                        for (var zeroBasedComponent = 0;
                             zeroBasedComponent <
                             rawComponents.Length;
                             zeroBasedComponent++)
                        {
                            var component =
                                new Component(
                                    IgnoreMissingItem)
                                {
                                    Name =
                                        (zeroBasedComponent + 1)
                                        .ToString(),

                                    Value =
                                        rawComponents[
                                            zeroBasedComponent]
                                };

                            // The original VB collection key is zero-based even
                            // though Name and positional access are one-based.
                            field
                                .ComponentsByRepetitionIndexer[
                                    oneBasedFieldRepetition]
                                .Add(
                                    component,
                                    zeroBasedComponent.ToString());

                            var componentRepetitions =
                                _parserType ==
                                ParserTypeEnum.X12
                                    ? rawComponents[
                                            zeroBasedComponent]
                                        .Split(
                                            [RepetitionSeparator],
                                            StringSplitOptions.None)
                                    : [
                                        rawComponents[
                                            zeroBasedComponent]
                                    ];

                            if (componentRepetitions.Length > 1)
                            {
                                component.HasRepetition = true;

                                for (var zeroBasedRepetition = 0;
                                     zeroBasedRepetition <
                                     componentRepetitions.Length;
                                     zeroBasedRepetition++)
                                {
                                    var repetition =
                                        new Repetition(
                                            IgnoreMissingItem)
                                        {
                                            Name =
                                                (zeroBasedRepetition +
                                                 1)
                                                .ToString(),

                                            Value =
                                                componentRepetitions[
                                                    zeroBasedRepetition]
                                        };

                                    component.Repetitions.Add(
                                        repetition,
                                        zeroBasedRepetition
                                            .ToString());

                                    ParseSubComponents(
                                        componentRepetitions[
                                            zeroBasedRepetition],
                                        repetition.SubComponents,
                                        out var
                                            hasSubComponents);

                                    repetition.HasSubComponents =
                                        hasSubComponents;
                                }
                            }
                            else
                            {
                                ParseSubComponents(
                                    rawComponents[
                                        zeroBasedComponent],
                                    component.SubComponents,
                                    out var hasSubComponents);

                                component.HasSubComponents =
                                    hasSubComponents;
                            }
                        }
                    }

                    if (continueOuterFieldLoop)
                        continue;
                }
            }

            var cancel = RaiseParsedSegment(
                oneBasedSegmentNumber,
                segment);

            if (ParsedSegment is not null &&
                ConserveMemory)
            {
                ClearMsg();
            }

            if (cancel)
                break;
        }
    }

    /// <summary>
    /// Lazily enumerates message segments using the specified separator.
    /// </summary>
    /// <param name="message">The complete message to enumerate.</param>
    /// <param name="separator">The segment separator.</param>
    /// <returns>
    /// An enumerable sequence containing each segment without its separator.
    /// </returns>
    /// <remarks>
    /// If the message does not end with a separator, the remaining trailing
    /// content is returned as the final segment.
    /// </remarks>
    private static IEnumerable<string> EnumerateBufferedSegments(
        string message,
        string separator)
    {
        var current = 0;

        while (current < message.Length)
        {
            var tokenIndex = message.IndexOf(
                separator,
                current,
                StringComparison.Ordinal);

            if (tokenIndex >= 0)
            {
                yield return message.Substring(
                    current,
                    tokenIndex - current);

                current =
                    tokenIndex + separator.Length;

                continue;
            }

            // VB emits the remaining tail when no further separator is found.
            if (current < message.Length)
                yield return message[current..];

            yield break;
        }
    }

    /// <summary>
    /// Parses subcomponents from a component or repetition value.
    /// </summary>
    /// <param name="value">
    /// The value that may contain subcomponent delimiters.
    /// </param>
    /// <param name="destination">
    /// The collection that receives the parsed subcomponents.
    /// </param>
    /// <param name="hasSubComponents">
    /// When this method returns, contains <see langword="true"/> when more than
    /// one subcomponent was parsed; otherwise, <see langword="false"/>.
    /// </param>
    private void ParseSubComponents(
        string value,
        SubComponents destination,
        out bool hasSubComponents)
    {
        hasSubComponents = false;

        if (!_subComponentSeparatorWasSet ||
            string.IsNullOrEmpty(SubComponentSeparator))
        {
            return;
        }

        var items = value.Split(
            [SubComponentSeparator],
            StringSplitOptions.None);

        if (items.Length <= 1)
            return;

        hasSubComponents = true;

        for (var i = 0; i < items.Length; i++)
        {
            destination.Add(
                new SubComponent(IgnoreMissingItem)
                {
                    Name = (i + 1).ToString(),
                    Value = items[i]
                },
                i.ToString());
        }
    }

    /// <summary>
    /// Raises the <see cref="ParsedSegment"/> event for a parsed segment.
    /// </summary>
    /// <param name="number">
    /// The one-based position of the segment in the source message.
    /// </param>
    /// <param name="segment">The parsed segment.</param>
    /// <returns>
    /// <see langword="true"/> when an event handler requests cancellation;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    private bool RaiseParsedSegment(
        int number,
        Segment segment)
    {
        if (ParsedSegment is null)
            return false;

        var cancel = false;
        var eventSegment = segment;

        ParsedSegment(
            this,
            number,
            ref eventSegment,
            ref cancel);

        return cancel;
    }

    /// <summary>
    /// Gets a value from the first matching segment using an EDI element path.
    /// </summary>
    /// <param name="element">
    /// The EDI element path identifying the requested value.
    /// </param>
    /// <returns>The value identified by the element path.</returns>
    public string GetValue(string element) =>
        ValueIndexer[element];

    /// <summary>
    /// Gets a value from a specified segment using an EDI element path.
    /// </summary>
    /// <param name="element">
    /// The EDI element path identifying the requested value.
    /// </param>
    /// <param name="segmentIndex">
    /// The one-based position of the segment.
    /// </param>
    /// <returns>The value identified by the path and segment index.</returns>
    public string GetValue(
        string element,
        int segmentIndex) =>
        ValueIndexer[segmentIndex, element];

    /// <summary>
    /// Gets a value from a specified segment and field repetition using an EDI
    /// element path.
    /// </summary>
    /// <param name="element">
    /// The EDI element path identifying the requested value.
    /// </param>
    /// <param name="segmentIndex">
    /// The one-based position of the segment.
    /// </param>
    /// <param name="fieldRepeatIndex">
    /// The one-based field-repetition position.
    /// </param>
    /// <returns>
    /// The value identified by the path, segment index, and repetition index.
    /// </returns>
    public string GetValue(
        string element,
        int segmentIndex,
        int fieldRepeatIndex) =>
        ValueIndexer[
            segmentIndex,
            element,
            fieldRepeatIndex];

    /// <summary>
    /// Sets a value using an EDI element path.
    /// </summary>
    /// <param name="element">
    /// The EDI element path identifying the value to update.
    /// </param>
    /// <param name="value">The value to assign.</param>
    public void SetValue(
        string element,
        string value) =>
        ValueIndexer[element] = value;

    /// <summary>
    /// Sets a value in a specified segment using an EDI element path.
    /// </summary>
    /// <param name="element">
    /// The EDI element path identifying the value to update.
    /// </param>
    /// <param name="value">The value to assign.</param>
    /// <param name="segmentIndex">
    /// The one-based position of the segment.
    /// </param>
    public void SetValue(
        string element,
        string value,
        int segmentIndex) =>
        ValueIndexer[
            segmentIndex,
            element] = value;

    /// <summary>
    /// Sets a value in a specified segment and field repetition using an EDI
    /// element path.
    /// </summary>
    /// <param name="element">
    /// The EDI element path identifying the value to update.
    /// </param>
    /// <param name="value">The value to assign.</param>
    /// <param name="segmentIndex">
    /// The one-based position of the segment.
    /// </param>
    /// <param name="fieldRepeatIndex">
    /// The one-based field-repetition position.
    /// </param>
    public void SetValue(
        string element,
        string value,
        int segmentIndex,
        int fieldRepeatIndex) =>
        ValueIndexer[
            segmentIndex,
            element,
            fieldRepeatIndex] = value;

    /// <summary>
    /// Rebuilds the current EDI message from the parsed object model.
    /// </summary>
    /// <returns>
    /// The reconstructed EDI message, including a trailing segment separator when
    /// at least one segment is present.
    /// </returns>
    /// <remarks>
    /// The message is generated from the current segment, field, component,
    /// repetition, and subcomponent values. Changes made through
    /// <see cref="SetValue(string, string)"/> or the object model are reflected in
    /// the returned message.
    /// </remarks>
    public virtual string Message()
    {
        var output =
            new System.Text.StringBuilder();

        var segmentStarted = false;

        foreach (Segment segment in _segments)
        {
            if (segmentStarted)
                output.Append(SegmentSeparator);

            output.Append(segment.Name);
            segmentStarted = true;

            var fieldDelimiterRequired = true;

            foreach (Field field in segment.Fields)
            {
                var repeatMax =
                    field.HasRepetition
                        ? field.RepetitionCount
                        : 1;

                for (var repeat = 1;
                     repeat <= repeatMax;
                     repeat++)
                {
                    output.Append(
                        repeat > 1
                            ? RepetitionSeparator
                            : fieldDelimiterRequired
                                ? FieldSeparator
                                : string.Empty);

                    var components =
                        field.ComponentsByRepetitionIndexer[
                            repeat];

                    var fieldValue =
                        field.GetValue(repeat);

                    if (components.Count == 0 &&
                        fieldValue != FieldSeparator)
                    {
                        output.Append(fieldValue);
                    }

                    fieldDelimiterRequired =
                        fieldValue != FieldSeparator;

                    var componentStarted = false;

                    foreach (Component component
                             in components)
                    {
                        if (componentStarted)
                            output.Append(
                                ComponentSeparator);

                        if (component.Repetitions.Count ==
                                0 &&
                            component.Value !=
                                FieldSeparator)
                        {
                            output.Append(
                                component.Value);
                        }

                        componentStarted = true;

                        fieldDelimiterRequired =
                            component.Value !=
                            FieldSeparator;

                        if (component.Repetitions.Count >
                            0)
                        {
                            var repetitionStarted = false;

                            foreach (Repetition repetition
                                     in component
                                         .Repetitions)
                            {
                                if (repetitionStarted)
                                {
                                    output.Append(
                                        RepetitionSeparator);
                                }

                                if (repetition
                                        .SubComponents
                                        .Count == 0)
                                {
                                    output.Append(
                                        repetition.Value);
                                }

                                repetitionStarted = true;

                                AppendSubComponents(
                                    output,
                                    repetition
                                        .SubComponents);
                            }
                        }
                        else
                        {
                            AppendSubComponents(
                                output,
                                component.SubComponents);
                        }
                    }
                }
            }
        }

        if (segmentStarted)
            output.Append(SegmentSeparator);

        return output.ToString();
    }

    /// <summary>
    /// Appends a collection of subcomponents to a generated message.
    /// </summary>
    /// <param name="output">
    /// The message builder that receives the subcomponent values.
    /// </param>
    /// <param name="subComponents">
    /// The subcomponents to append.
    /// </param>
    private void AppendSubComponents(
        System.Text.StringBuilder output,
        SubComponents subComponents)
    {
        var started = false;

        foreach (SubComponent subComponent
                 in subComponents)
        {
            if (started)
                output.Append(SubComponentSeparator);

            output.Append(subComponent.Value);
            started = true;
        }
    }

    /// <summary>
    /// Adds a segment to the parser's current segment collection.
    /// </summary>
    /// <param name="segment">The segment to add.</param>
    /// <param name="key">
    /// An optional collection key. When omitted, the next one-based numeric key
    /// is assigned.
    /// </param>
    internal void AddSegment(
        Segment segment,
        string? key = null) =>
        _segments.Add(
            segment,
            key ??
            (_segments.Count + 1).ToString());
}