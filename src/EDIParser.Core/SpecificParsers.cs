namespace EDIParser;

/// <summary>
/// Parses ANSI X12 messages using the delimiters and structural rules defined
/// by the X12 standard.
/// </summary>
/// <remarks>
/// <para>
/// The parser is initialized with the default X12 separators:
/// segment <c>~</c>, field <c>*</c>, component <c>&gt;</c>, and repetition
/// <c>^</c>.
/// </para>
/// <para>
/// When <see cref="CheckISASeparator"/> is enabled, the parser reads separator
/// values from the ISA segment before parsing the remainder of the message.
/// </para>
/// </remarks>
public sealed class X12Parser : Parser
{
    private int _transactionSegmentCount;
    private int _functionalGroupCount;
    private int _interchangeCount;

    /// <summary>
    /// Initializes a new instance of the <see cref="X12Parser"/> class using the
    /// default X12 delimiters.
    /// </summary>
    public X12Parser()
    {
        SegmentSeparator = "~";
        FieldSeparator = "*";
        ComponentSeparator = ">";
        RepetitionSeparator = "^";
        ParserType = ParserTypeEnum.X12;
        ValueIndexer.AddSegment += OnSegmentAdded;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="X12Parser"/> class using the
    /// legacy constructor signature.
    /// </summary>
    /// <param name="value">
    /// An unused value retained for compatibility with the original API.
    /// </param>
    internal X12Parser(object value) : base(value)
    {
        SegmentSeparator = "~";
        FieldSeparator = "*";
        ComponentSeparator = ">";
        RepetitionSeparator = "^";
        ParserType = ParserTypeEnum.X12;
        ValueIndexer.AddSegment += OnSegmentAdded;
    }

    /// <summary>
    /// Gets or sets a value indicating whether separator characters should be
    /// read from the ISA segment before parsing.
    /// </summary>
    /// <value>
    /// <see langword="true"/> to detect separators from the ISA segment;
    /// otherwise, <see langword="false"/> to use the currently configured
    /// separators.
    /// </value>
    public bool CheckISASeparator { get; set; }

    /// <summary>
    /// Gets the number of segments counted in the current X12 transaction set.
    /// </summary>
    /// <remarks>
    /// The count is updated when segments are added through the parser's value
    /// indexer.
    /// </remarks>
    public int TransactionSegmentCount => _transactionSegmentCount;

    /// <summary>
    /// Gets the number of transaction sets counted in the current functional
    /// group.
    /// </summary>
    public int TransactionFunctionalGroupCount => _functionalGroupCount;

    /// <summary>
    /// Gets the number of functional groups counted in the current interchange.
    /// </summary>
    public int TransactionInterchangeCount => _interchangeCount;

    /// <summary>
    /// Parses an ANSI X12 message and populates the parser object model.
    /// </summary>
    /// <param name="msg">The X12 message to parse.</param>
    /// <remarks>
    /// <para>
    /// When <see cref="CheckISASeparator"/> is enabled and the message begins
    /// with an ISA segment, the segment, field, and component separators are
    /// derived from their fixed ISA positions.
    /// </para>
    /// <para>
    /// Carriage-return and line-feed characters that are not configured as the
    /// segment separator are removed before the message is passed to the base
    /// parser.
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="msg"/> is <see langword="null"/> or empty.
    /// </exception>
    public override void ParseMsg(string msg)
    {
        if (string.IsNullOrEmpty(msg))
        {
            throw new ArgumentException(
                "Msg is empty or null",
                nameof(msg));
        }

        if (CheckISASeparator)
        {
            if (msg.Length >= 106 &&
                msg.StartsWith(
                    "ISA",
                    StringComparison.Ordinal))
            {
                SegmentSeparator =
                    msg.Substring(105, 1);

                FieldSeparator =
                    msg.Substring(3, 1);

                ComponentSeparator =
                    msg.Substring(104, 1);
            }

            var segmentEnd = msg.IndexOf(
                SegmentSeparator,
                StringComparison.Ordinal);

            if (segmentEnd >= 0)
            {
                var isaFields =
                    msg[..segmentEnd].Split(
                        [FieldSeparator],
                        StringSplitOptions.None);

                if (isaFields.Length > 16 &&
                    isaFields[0]
                        .Trim()
                        .Equals(
                            "ISA",
                            StringComparison.OrdinalIgnoreCase) &&
                    isaFields[16].Trim().Length > 0)
                {
                    ComponentSeparator =
                        isaFields[16];

                    var position = msg.IndexOf(
                        ComponentSeparator,
                        StringComparison.Ordinal);

                    if (position >= 0)
                    {
                        msg =
                            msg[..position] +
                            "[Component_Delimiter]" +
                            msg[(position + 1)..];
                    }
                }
            }
        }

        if (SegmentSeparator != "\r\n")
        {
            msg = msg.Replace(
                "\r\n",
                string.Empty,
                StringComparison.Ordinal);
        }

        if (SegmentSeparator != "\r")
        {
            msg = msg.Replace(
                "\r",
                string.Empty,
                StringComparison.Ordinal);
        }

        if (SegmentSeparator != "\n")
        {
            msg = msg.Replace(
                "\n",
                string.Empty,
                StringComparison.Ordinal);
        }

        base.ParseMsg(msg);
    }

    /// <summary>
    /// Updates transaction, functional-group, and interchange counts after a
    /// segment is added.
    /// </summary>
    /// <param name="sender">
    /// The object that raised the segment-added event.
    /// </param>
    /// <param name="segmentNbr">
    /// The one-based position assigned to the added segment.
    /// </param>
    /// <param name="segment">The segment that was added.</param>
    private void OnSegmentAdded(
        object sender,
        int segmentNbr,
        Segment segment)
    {
        switch (segment.Name)
        {
            case "ISA":
            case "IEA":
            case "GE":
                return;

            case "ST":
                _functionalGroupCount++;
                _transactionSegmentCount = 1;
                break;

            case "GS":
                if (_functionalGroupCount > 0)
                    _functionalGroupCount = 0;

                _interchangeCount++;
                break;

            case "SE":
                _transactionSegmentCount++;
                break;

            default:
                _transactionSegmentCount++;
                break;
        }
    }
}

/// <summary>
/// Parses HL7 messages using delimiter values defined in the MSH segment.
/// </summary>
/// <remarks>
/// <para>
/// The parser is initialized with standard HL7 delimiters:
/// carriage return for segments, <c>|</c> for fields, <c>^</c> for components,
/// <c>~</c> for repetitions, <c>\</c> for escapes, and <c>&amp;</c> for
/// subcomponents.
/// </para>
/// <para>
/// By default, delimiter values are read from the MSH segment before parsing.
/// </para>
/// </remarks>
public sealed class HL7Parser : Parser
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HL7Parser"/> class using the
    /// standard HL7 delimiters.
    /// </summary>
    public HL7Parser()
    {
        base.SegmentSeparator = "\r";
        FieldSeparator = "|";
        ComponentSeparator = "^";
        SubComponentSeparator = "&";
        RepetitionSeparator = "~";
        EscapeChar = "\\";
        Delimiters =
            FieldSeparator +
            ComponentSeparator +
            RepetitionSeparator +
            EscapeChar +
            SubComponentSeparator;

        ParserType = ParserTypeEnum.HL7;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HL7Parser"/> class using the
    /// legacy constructor signature.
    /// </summary>
    /// <param name="value">
    /// An unused value retained for compatibility with the original API.
    /// </param>
    internal HL7Parser(object value) : base(value)
    {
        base.SegmentSeparator = "\r";
        FieldSeparator = "|";
        ComponentSeparator = "^";
        SubComponentSeparator = "&";
        RepetitionSeparator = "~";
        EscapeChar = "\\";
        Delimiters =
            FieldSeparator +
            ComponentSeparator +
            RepetitionSeparator +
            EscapeChar +
            SubComponentSeparator;

        ParserType = ParserTypeEnum.HL7;
    }

    /// <summary>
    /// Gets or sets the delimiter used to separate HL7 segments.
    /// </summary>
    /// <remarks>
    /// The parser may automatically switch between carriage return and
    /// carriage-return/line-feed separators based on the source message.
    /// </remarks>
    public override string SegmentSeparator
    {
        get => base.SegmentSeparator;
        set => base.SegmentSeparator = value;
    }

    /// <summary>
    /// Gets or sets the HL7 escape character.
    /// </summary>
    public string EscapeChar { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether delimiter characters should be
    /// read from the MSH segment before parsing.
    /// </summary>
    /// <value>
    /// <see langword="true"/> to detect separators from MSH-1 and MSH-2;
    /// otherwise, <see langword="false"/> to use the currently configured
    /// delimiters.
    /// </value>
    public bool CheckMSHSeparator { get; set; } = true;

    /// <summary>
    /// Parses an HL7 message and populates the parser object model.
    /// </summary>
    /// <param name="msg">The HL7 message to parse.</param>
    /// <remarks>
    /// <para>
    /// When <see cref="CheckMSHSeparator"/> is enabled, the field, component,
    /// repetition, escape, and subcomponent delimiters are read from the initial
    /// MSH segment.
    /// </para>
    /// <para>
    /// The MSH field delimiter and encoding characters are temporarily replaced
    /// with parser tokens so that MSH can be represented consistently within
    /// the normal field object model.
    /// </para>
    /// <para>
    /// The parser detects whether the source message uses carriage return or
    /// carriage-return/line-feed segment separators.
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="msg"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown when the message is too short to contain the MSH delimiter
    /// definition.
    /// </exception>
    /// <exception cref="Exception">
    /// Thrown when the configured segment separator cannot be found in the
    /// message.
    /// </exception>
    public override void ParseMsg(string msg)
    {
        ArgumentNullException.ThrowIfNull(msg);

        if (msg.Length < 8)
        {
            throw new ArgumentException(
                "HL7 message is too short to contain MSH delimiters.",
                nameof(msg));
        }

        var startLength = 9;

        if (CheckMSHSeparator)
        {
            FieldSeparator =
                VbMid(msg, 4, 1);

            ComponentSeparator =
                VbMid(msg, 5, 1);

            RepetitionSeparator =
                VbMid(msg, 6, 1);

            EscapeChar =
                VbMid(msg, 7, 1);

            SubComponentSeparator =
                VbMid(msg, 8, 1);
        }

        if (SubComponentSeparator == EscapeChar)
        {
            SubComponentSeparator =
                VbMid(msg, 9, 1);

            startLength = 10;
            Delimiters = VbMid(msg, 5, 5);
        }
        else
        {
            Delimiters = VbMid(msg, 5, 4);
        }

        msg =
            VbMid(msg, 1, 4) +
            "<Field_Delimiter>" +
            FieldSeparator +
            "<Replace_Delimiters>" +
            VbMid(msg, startLength);

        if (SegmentSeparator == "\r" &&
            msg.Contains(
                "\r\n",
                StringComparison.Ordinal))
        {
            SegmentSeparator = "\r\n";
        }
        else if (SegmentSeparator == "\r\n" &&
                 !msg.Contains(
                     "\r\n",
                     StringComparison.Ordinal))
        {
            if (msg.Contains('\r'))
            {
                SegmentSeparator = "\r";
            }
            else
            {
                throw new Exception(
                    "Segment separator not found");
            }
        }

        base.ParseMsg(msg);
    }

    /// <summary>
    /// Returns a substring using VB-compatible one-based start-position
    /// semantics.
    /// </summary>
    /// <param name="value">The source string.</param>
    /// <param name="oneBasedStart">
    /// The one-based position at which the substring begins.
    /// </param>
    /// <param name="length">
    /// The optional maximum number of characters to return. When omitted, the
    /// remainder of the string is returned.
    /// </param>
    /// <returns>
    /// The requested substring, or an empty string when the start position is
    /// beyond the end of the source value.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="oneBasedStart"/> is less than one.
    /// </exception>
    private static string VbMid(
        string value,
        int oneBasedStart,
        int? length = null)
    {
        if (oneBasedStart < 1)
        {
            throw new ArgumentException(
                "Start must be one or greater.");
        }

        var start = oneBasedStart - 1;

        if (start >= value.Length)
            return string.Empty;

        return length is null
            ? value[start..]
            : value.Substring(
                start,
                Math.Min(
                    length.Value,
                    value.Length - start));
    }
}

/// <summary>
/// Parses UN/EDIFACT messages using the delimiter values defined by the UNA
/// service string advice segment.
/// </summary>
/// <remarks>
/// The parser is initialized with the default EDIFACT separators and replaces
/// them with the delimiter values contained in the source UNA segment during
/// parsing.
/// </remarks>
public sealed class EdiFactParser : Parser
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EdiFactParser"/> class using
    /// the default EDIFACT delimiters.
    /// </summary>
    public EdiFactParser()
    {
        SegmentSeparator = "'";
        FieldSeparator = "+";
        ComponentSeparator = ":";
        RepetitionSeparator = "*";
        ReleaseIndicator = "/";
        DecimalNotation = ".";
        ParserType = ParserTypeEnum.EdiFact;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EdiFactParser"/> class using
    /// the legacy constructor signature.
    /// </summary>
    /// <param name="value">
    /// An unused value retained for compatibility with the original API.
    /// </param>
    internal EdiFactParser(object value) : base(value)
    {
        SegmentSeparator = "'";
        FieldSeparator = "+";
        ComponentSeparator = ":";
        RepetitionSeparator = "*";
        ReleaseIndicator = "/";
        DecimalNotation = ".";
        ParserType = ParserTypeEnum.EdiFact;
    }

    /// <summary>
    /// Gets or sets the EDIFACT release indicator used to escape delimiter
    /// characters.
    /// </summary>
    public string ReleaseIndicator { get; set; }

    /// <summary>
    /// Gets or sets the EDIFACT decimal-notation character.
    /// </summary>
    public string DecimalNotation { get; set; }

    /// <summary>
    /// Parses an EDIFACT message and populates the parser object model.
    /// </summary>
    /// <param name="msg">
    /// The EDIFACT message beginning with a UNA service string advice segment.
    /// </param>
    /// <remarks>
    /// The component, field, decimal, release, repetition, and segment separators
    /// are read from their fixed UNA positions before the remainder of the
    /// message is passed to the base parser.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="msg"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown when the message is too short to contain the UNA delimiter
    /// definition.
    /// </exception>
    public override void ParseMsg(string msg)
    {
        ArgumentNullException.ThrowIfNull(msg);

        if (msg.Length < 9)
        {
            throw new ArgumentException(
                "EDIFACT message is too short to contain UNA delimiters.",
                nameof(msg));
        }

        // Preserve the original VB positions exactly.
        // VB Mid uses one-based positions.
        SegmentSeparator =
            msg.Substring(8, 1);

        FieldSeparator =
            msg.Substring(4, 1);

        ComponentSeparator =
            msg.Substring(3, 1);

        RepetitionSeparator =
            msg.Substring(7, 1);

        ReleaseIndicator =
            msg.Substring(6, 1);

        DecimalNotation =
            msg.Substring(3, 1);

        base.ParseMsg(msg[9..]);
    }

    /// <summary>
    /// Rebuilds the current EDIFACT message, including its UNA service string
    /// advice segment.
    /// </summary>
    /// <returns>
    /// The reconstructed UNA segment followed by the generated EDIFACT message.
    /// </returns>
    public override string Message() =>
        "UNA" +
        ComponentSeparator +
        FieldSeparator +
        DecimalNotation +
        ReleaseIndicator +
        RepetitionSeparator +
        SegmentSeparator +
        base.Message();
}