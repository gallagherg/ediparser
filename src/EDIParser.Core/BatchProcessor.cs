namespace EDIParser;

/// <summary>
/// Processes EDI messages from a stream and raises an event as each segment is read.
/// </summary>
/// <remarks>
/// <para>
/// The processor supports reading an entire stream or processing a limited range
/// through the <see cref="Limit(long, long)"/> method.
/// </para>
/// <para>
/// When the parser's segment separator is a carriage-return and line-feed sequence,
/// the stream is processed one line at a time. For other single-character segment
/// separators, the stream is processed character by character.
/// </para>
/// <para>
/// Segment collection access is one-based, consistent with the original parser API.
/// </para>
/// </remarks>
public sealed class BatchProcessor
{
    /// <summary>
    /// Defines the states reported while batch processing EDI input.
    /// </summary>
    public enum ProcessingStatuses
    {
        /// <summary>
        /// Indicates that the requested input has been completely processed.
        /// </summary>
        ProcessingComplete = 1,

        /// <summary>
        /// Indicates that additional input remains to be processed.
        /// </summary>
        ProcessingPending = 2
    }

    /// <summary>
    /// Represents the method that handles the <see cref="Reading"/> event.
    /// </summary>
    /// <param name="segment">
    /// A reference to the segment that was read from the input stream.
    /// </param>
    /// <remarks>
    /// The segment is passed by reference to preserve the behavior of the original API.
    /// </remarks>
    public delegate void ReadingEventHandler(ref Segment segment);

    /// <summary>
    /// Occurs after a segment has been read and parsed from the input stream.
    /// </summary>
    public event ReadingEventHandler? Reading;

    private readonly Parser _parser;
    private readonly Stream _stream;
    private readonly string _segmentSeparator;
    private long _seekPosition;
    private long _seekCount;
    private StreamReader? _reader;
    private bool _processAll = true;

    /// <summary>
    /// Initializes a new instance of the <see cref="BatchProcessor"/> class.
    /// </summary>
    /// <param name="parser">
    /// The parser used to parse each EDI segment read from the stream.
    /// </param>
    /// <param name="stream">
    /// The readable stream containing the EDI data to process.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="parser"/> or <paramref name="stream"/> is
    /// <see langword="null"/>.
    /// </exception>
    public BatchProcessor(Parser parser, Stream stream)
    {
        _parser = parser ?? throw new ArgumentNullException(nameof(parser));
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _segmentSeparator = parser.SegmentSeparator;
    }

    /// <summary>
    /// Gets the current batch-processing status.
    /// </summary>
    /// <value>
    /// <see cref="ProcessingStatuses.ProcessingComplete"/> when processing has
    /// reached the end of the requested input; otherwise,
    /// <see cref="ProcessingStatuses.ProcessingPending"/>.
    /// </value>
    public ProcessingStatuses ProcessingStatus { get; private set; }

    /// <summary>
    /// Limits processing to a specified starting position and maximum count.
    /// </summary>
    /// <param name="seekPosition">
    /// The zero-based stream or line position at which processing begins.
    /// </param>
    /// <param name="seekCount">
    /// The maximum number of segments or lines to process. A value of zero
    /// indicates that no count limit is applied.
    /// </param>
    /// <remarks>
    /// For carriage-return and line-feed separated input,
    /// <paramref name="seekPosition"/> is interpreted as a line position.
    /// For character-delimited input, it is interpreted as a byte position
    /// within the stream.
    /// </remarks>
    public void Limit(long seekPosition, long seekCount)
    {
        _seekPosition = seekPosition;
        _seekCount = seekCount;
        _processAll = false;
    }

    /// <summary>
    /// Processes EDI data from the configured stream.
    /// </summary>
    /// <remarks>
    /// <para>
    /// When no processing limit has been configured, processing begins at the
    /// current stream position and continues until the end of the stream.
    /// </para>
    /// <para>
    /// When the parser uses a carriage-return and line-feed segment separator,
    /// the input is processed line by line. Otherwise, the input is processed
    /// by searching for the configured segment-separator character.
    /// </para>
    /// <para>
    /// The <see cref="Reading"/> event is raised after each parsed segment.
    /// </para>
    /// </remarks>
    public void Process()
    {
        var firstPass = false;

        if (_processAll)
        {
            _reader = new StreamReader(_stream, leaveOpen: true);
        }
        else if (_reader is null)
        {
            firstPass = true;
            _reader = new StreamReader(_stream, leaveOpen: true);
        }

        ProcessingStatus = ProcessingStatuses.ProcessingComplete;

        if (_segmentSeparator == "\r\n")
        {
            ProcessLines(firstPass);
        }
        else
        {
            ProcessCharacters();
        }
    }

    /// <summary>
    /// Processes input in line mode when segments are separated by
    /// carriage-return and line-feed characters.
    /// </summary>
    /// <param name="firstPass">
    /// <see langword="true"/> when this is the first limited processing pass;
    /// otherwise, <see langword="false"/>.
    /// </param>
    private void ProcessLines(bool firstPass)
    {
        var reader = _reader!;
        long position = 0;

        if (firstPass || _seekPosition > 0)
        {
            while (reader.Peek() >= 0 && position < _seekPosition)
            {
                reader.ReadLine();
                position++;
            }
        }

        _seekPosition = position;
        long count = 1;

        while (reader.Peek() >= 0)
        {
            if (_seekCount > 0 && count > _seekCount)
            {
                ProcessingStatus = ProcessingStatuses.ProcessingPending;
                break;
            }

            count++;

            var line = reader.ReadLine() ?? string.Empty;
            _parser.ParseMsg(line);

            // The original line-mode contract uses one-based segment access.
            var segment = _parser.Segments[1];

            Reading?.Invoke(ref segment);
            _seekPosition++;
        }

        if (reader.Peek() < 0)
        {
            ProcessingStatus = ProcessingStatuses.ProcessingComplete;
        }
    }

    /// <summary>
    /// Processes input by reading characters until the configured segment
    /// separator is encountered.
    /// </summary>
    /// <remarks>
    /// Each completed segment is parsed independently and supplied to subscribers
    /// through the <see cref="Reading"/> event. Any remaining buffered data is
    /// processed when the end of the stream is reached.
    /// </remarks>
    private void ProcessCharacters()
    {
        var fileLength = _stream.Length;

        _stream.Seek(_seekPosition, SeekOrigin.Begin);

        var buffer = new System.Text.StringBuilder();
        long count = 1;

        while (_seekPosition <= fileLength - 1)
        {
            var next = _stream.ReadByte();

            if (next < 0)
            {
                break;
            }

            var ch = (char)next;

            if (ch.ToString() == _segmentSeparator)
            {
                if (_seekCount > 0 && count > _seekCount)
                {
                    ProcessingStatus = ProcessingStatuses.ProcessingPending;
                    buffer.Clear();
                    break;
                }

                count++;

                _parser.ParseMsg(buffer.ToString());

                var segment = _parser.Segments.Count > 0
                    ? _parser.Segments[1]
                    : new Segment();

                Reading?.Invoke(ref segment);
                buffer.Clear();
            }
            else
            {
                buffer.Append(ch);
            }

            _seekPosition++;
        }

        if (_seekPosition >= fileLength)
        {
            ProcessingStatus = ProcessingStatuses.ProcessingComplete;
        }

        if (buffer.Length > 0)
        {
            _parser.ParseMsg(buffer.ToString());

            var segment = _parser.Segments.Count > 0
                ? _parser.Segments[1]
                : new Segment();

            Reading?.Invoke(ref segment);
        }
    }
}