using System.Collections;

namespace EDIParser;

/// <summary>
/// Represents a subcomponent within an EDI component or repetition.
/// </summary>
public sealed class SubComponent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SubComponent"/> class.
    /// </summary>
    /// <param name="ignoreMissingItem">
    /// Indicates whether missing collection items should return an empty placeholder
    /// instead of throwing an exception.
    /// </param>
    internal SubComponent(bool ignoreMissingItem = true) =>
        IgnoreMissingItem = ignoreMissingItem;

    /// <summary>
    /// Gets a value indicating whether missing collection items are ignored.
    /// </summary>
    internal bool IgnoreMissingItem { get; }

    /// <summary>
    /// Gets or sets the one-based name or position of the subcomponent.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the value of the subcomponent.
    /// </summary>
    public string Value { get; set; } = string.Empty;
}

/// <summary>
/// Represents a repeated component value and its optional subcomponents.
/// </summary>
public sealed class Repetition
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Repetition"/> class.
    /// </summary>
    /// <param name="ignoreMissingItem">
    /// Indicates whether missing collection items should return an empty placeholder
    /// instead of throwing an exception.
    /// </param>
    internal Repetition(bool ignoreMissingItem = true)
    {
        SubComponents.IgnoreMissingItem = ignoreMissingItem;
        ValueIndexer = new ValueIndexer(this);
    }

    /// <summary>
    /// Gets or sets the one-based name or position of the repetition.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the value of the repetition.
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether this repetition contains subcomponents.
    /// </summary>
    public bool HasSubComponents { get; set; }

    /// <summary>
    /// Gets the subcomponents contained in this repetition.
    /// </summary>
    public SubComponents SubComponents { get; } = new();

    /// <summary>
    /// Gets the indexer used to retrieve values from this repetition by element path.
    /// </summary>
    public ValueIndexer ValueIndexer { get; }
}

/// <summary>
/// Represents a component within an EDI field.
/// </summary>
/// <remarks>
/// Depending on the EDI standard, a component may contain repeated values,
/// subcomponents, or both.
/// </remarks>
public sealed class Component
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Component"/> class.
    /// </summary>
    /// <param name="ignoreMissingItem">
    /// Indicates whether missing collection items should return an empty placeholder
    /// instead of throwing an exception.
    /// </param>
    internal Component(bool ignoreMissingItem = true)
    {
        Repetitions.IgnoreMissingItem = ignoreMissingItem;
        SubComponents.IgnoreMissingItem = ignoreMissingItem;
        ValueIndexer = new ValueIndexer(this);
    }

    /// <summary>
    /// Gets or sets the one-based name or position of the component.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the value of the component.
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether this component contains repetitions.
    /// </summary>
    public bool HasRepetition { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this component contains subcomponents.
    /// </summary>
    public bool HasSubComponents { get; set; }

    /// <summary>
    /// Gets the repeated values contained in this component.
    /// </summary>
    public Repetitions Repetitions { get; } = new();

    /// <summary>
    /// Gets the subcomponents contained in this component.
    /// </summary>
    public SubComponents SubComponents { get; } = new();

    /// <summary>
    /// Gets the indexer used to retrieve values from this component by element path.
    /// </summary>
    public ValueIndexer ValueIndexer { get; }
}

/// <summary>
/// Represents a field within an EDI segment.
/// </summary>
/// <remarks>
/// A field may contain a simple value, one or more components, or repeated field
/// values depending on the configured EDI standard.
/// </remarks>
public sealed class Field
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Field"/> class.
    /// </summary>
    /// <param name="ignoreMissingItem">
    /// Indicates whether missing collection items should return an empty placeholder
    /// instead of throwing an exception.
    /// </param>
    internal Field(bool ignoreMissingItem = true)
    {
        ValueByRepetitionIndexer = new ValueByRepetitionIndexer(this);
        ComponentsByRepetitionIndexer =
            new ComponentsByRepetitionIndexer(
                ValueByRepetitionIndexer,
                ignoreMissingItem);

        ValueIndexer = new ValueIndexer(this);
    }

    /// <summary>
    /// Gets or sets the one-based name or position of the field.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the value of the first field repetition.
    /// </summary>
    /// <remarks>
    /// For repeated fields, use <see cref="ComponentsByRepetitionIndexer"/> or the
    /// parser's element-path APIs to access repetitions beyond the first.
    /// </remarks>
    public string Value
    {
        get => ValueByRepetitionIndexer[1];
        set => ValueByRepetitionIndexer[1] = value ?? string.Empty;
    }

    /// <summary>
    /// Gets the components associated with the first field repetition.
    /// </summary>
    public Components Components => ComponentsByRepetitionIndexer[1];

    /// <summary>
    /// Gets the component collections associated with each field repetition.
    /// </summary>
    public ComponentsByRepetitionIndexer ComponentsByRepetitionIndexer { get; }

    /// <summary>
    /// Gets the internal value collection associated with each field repetition.
    /// </summary>
    internal ValueByRepetitionIndexer ValueByRepetitionIndexer { get; }

    /// <summary>
    /// Gets the indexer used to retrieve values from this field by element path.
    /// </summary>
    public ValueIndexer ValueIndexer { get; }

    /// <summary>
    /// Gets a value indicating whether this field contains more than one repetition.
    /// </summary>
    public bool HasRepetition =>
        ComponentsByRepetitionIndexer.HasRepetition ||
        ValueByRepetitionIndexer.HasRepetition;

    /// <summary>
    /// Gets the number of repetitions represented by this field.
    /// </summary>
    /// <remarks>
    /// The repetition count is the larger of the stored field-value count and
    /// component-collection count.
    /// </remarks>
    public int RepetitionCount =>
        Math.Max(
            ComponentsByRepetitionIndexer.Length,
            ValueByRepetitionIndexer.Length);

    /// <summary>
    /// Gets the value of a specified field repetition.
    /// </summary>
    /// <param name="repeatIndex">The one-based repetition index.</param>
    /// <returns>The value stored at the specified repetition.</returns>
    internal string GetValue(int repeatIndex) =>
        ValueByRepetitionIndexer[repeatIndex];

    /// <summary>
    /// Sets the value of a specified field repetition.
    /// </summary>
    /// <param name="value">The value to store.</param>
    /// <param name="repeatIndex">The one-based repetition index.</param>
    internal void SetValue(string value, int repeatIndex) =>
        ValueByRepetitionIndexer[repeatIndex] = value ?? string.Empty;

    /// <summary>
    /// Gets a value from this field using an EDI element path.
    /// </summary>
    /// <param name="element">
    /// The element path identifying a component, repetition, or subcomponent.
    /// </param>
    /// <returns>The value identified by the element path.</returns>
    internal string GetValue(string element) =>
        PathNavigator.GetFromField(this, element, 1);

    /// <summary>
    /// Gets a value from a specified field repetition using an EDI element path.
    /// </summary>
    /// <param name="element">
    /// The element path identifying a component, repetition, or subcomponent.
    /// </param>
    /// <param name="repeatIndex">The one-based field-repetition index.</param>
    /// <returns>The value identified by the element path.</returns>
    /// <exception cref="IndexOutOfRangeException">
    /// Thrown when <paramref name="repeatIndex"/> exceeds the number of stored
    /// field repetitions.
    /// </exception>
    internal string GetValue(string element, int repeatIndex)
    {
        if (repeatIndex > ValueByRepetitionIndexer.Length)
            throw new IndexOutOfRangeException();

        return PathNavigator.GetFromField(
            this,
            element,
            repeatIndex);
    }
}

/// <summary>
/// Represents an EDI segment and its collection of fields.
/// </summary>
public sealed class Segment
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Segment"/> class.
    /// </summary>
    /// <param name="ignoreMissingItem">
    /// Indicates whether missing collection items should return an empty placeholder
    /// instead of throwing an exception.
    /// </param>
    internal Segment(bool ignoreMissingItem = true)
    {
        Fields.IgnoreMissingItem = ignoreMissingItem;
        ValueIndexer = new ValueIndexer(this);
    }

    /// <summary>
    /// Gets or sets the segment identifier, such as <c>PID</c>, <c>OBX</c>,
    /// or <c>N1</c>.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the original unparsed value of the segment.
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Gets the fields contained in this segment.
    /// </summary>
    public Fields Fields { get; } = new();

    /// <summary>
    /// Gets the indexer used to retrieve values from this segment by element path.
    /// </summary>
    public ValueIndexer ValueIndexer { get; }
}

/// <summary>
/// Provides the base implementation for the parser's one-based keyed collections.
/// </summary>
/// <remarks>
/// <para>
/// Items are stored in insertion order and may also be associated with string keys.
/// Numeric positional access in derived collections is one-based to preserve the
/// behavior of the original VB implementation.
/// </para>
/// <para>
/// When <see cref="IgnoreMissingItem"/> is enabled, an out-of-range or missing-key
/// lookup returns the collection's configured placeholder item.
/// </para>
/// </remarks>
public class ItemObjectCollection : IEnumerable
{
    private readonly List<object> _items = new();
    private readonly List<string> _keys = new();
    private object? _missingItem;

    /// <summary>
    /// Gets or sets a value indicating whether missing collection items return a
    /// placeholder object instead of throwing an exception.
    /// </summary>
    public bool IgnoreMissingItem { get; set; }

    /// <summary>
    /// Sets the placeholder object returned when a requested item is missing and
    /// <see cref="IgnoreMissingItem"/> is enabled.
    /// </summary>
    internal object? MissingItem
    {
        set => _missingItem = value;
    }

    /// <summary>
    /// Gets the number of items contained in the collection.
    /// </summary>
    public int Count => _items.Count;

    /// <summary>
    /// Adds an item to the collection using the specified key.
    /// </summary>
    /// <param name="item">The item to add.</param>
    /// <param name="key">The key associated with the item.</param>
    /// <param name="before">
    /// An optional existing item before which the new item is inserted.
    /// </param>
    /// <param name="after">
    /// An optional existing item after which the new item is inserted.
    /// </param>
    /// <remarks>
    /// The insertion behavior preserves the original implementation, including
    /// its handling of targets located at index zero.
    /// </remarks>
    internal void Add(
        object item,
        string key,
        object? before = null,
        object? after = null)
    {
        // Preserve original behavior: Before/After only inserts
        // when target index > 0.
        if (after is not null)
        {
            var idx = _items.IndexOf(after);

            if (idx > 0)
            {
                _items.Insert(idx, item);
                _keys.Insert(idx, key);
                return;
            }
        }

        if (before is not null)
        {
            var idx = _items.IndexOf(before);

            if (idx > 0)
            {
                _items.Insert(idx - 1, item);
                _keys.Insert(idx - 1, key);
                return;
            }
        }

        _items.Add(item);
        _keys.Add(key);
    }

    /// <summary>
    /// Adds an item to the collection without assigning a key.
    /// </summary>
    /// <param name="item">The item to add.</param>
    internal void Add(object item) => _items.Add(item);

    /// <summary>
    /// Removes all items and keys from the collection.
    /// </summary>
    internal void Clear()
    {
        _items.Clear();
        _keys.Clear();
    }

    /// <summary>
    /// Removes the item at the specified one-based index.
    /// </summary>
    /// <param name="oneBasedIndex">The one-based index of the item to remove.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the index does not identify an item in the collection.
    /// </exception>
    internal void Remove(int oneBasedIndex)
    {
        _items.RemoveAt(oneBasedIndex - 1);

        if (oneBasedIndex - 1 < _keys.Count)
            _keys.RemoveAt(oneBasedIndex - 1);
    }

    /// <summary>
    /// Removes the item associated with the specified key.
    /// </summary>
    /// <param name="key">The key of the item to remove.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when the key does not exist and <see cref="IgnoreMissingItem"/> is
    /// disabled.
    /// </exception>
    internal void Remove(string key)
    {
        var zeroBasedIndex = _keys.IndexOf(key);

        if (zeroBasedIndex < 0)
        {
            if (IgnoreMissingItem)
                return;

            throw new ArgumentException(
                "Argument 'key' is not a valid value.",
                nameof(key));
        }

        Remove(zeroBasedIndex + 1);
    }

    /// <summary>
    /// Determines whether the collection contains the specified key.
    /// </summary>
    /// <param name="key">The key to locate.</param>
    /// <returns>
    /// <see langword="true"/> when the key exists; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    public bool Contains(string key) => _keys.Contains(key);

    /// <summary>
    /// Gets an item using a one-based positional index.
    /// </summary>
    /// <param name="index">The one-based index of the item.</param>
    /// <returns>The item at the specified position.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="index"/> is less than one.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown when the index exceeds the collection size and
    /// <see cref="IgnoreMissingItem"/> is disabled.
    /// </exception>
    protected object GetByOneBasedIndex(int index)
    {
        if (index < 1)
        {
            throw new ArgumentOutOfRangeException(
                nameof(index),
                "Collection indexes are one-based.");
        }

        if (index <= _items.Count)
            return _items[index - 1];

        if (IgnoreMissingItem)
            return _missingItem!;

        throw new ArgumentException(
            "Argument 'Index' is not a valid value.",
            nameof(index));
    }

    /// <summary>
    /// Gets an item by comparing it with the objects contained in the collection.
    /// </summary>
    /// <param name="index">The object to locate.</param>
    /// <returns>The matching item.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when no matching item is found and
    /// <see cref="IgnoreMissingItem"/> is disabled.
    /// </exception>
    protected object GetByObject(object index)
    {
        foreach (var item in _items)
        {
            if (Equals(item, index))
                return item;
        }

        if (IgnoreMissingItem)
            return _missingItem!;

        throw new ArgumentException(
            "Argument 'Index' is not a valid value.",
            nameof(index));
    }

    /// <summary>
    /// Gets an item using its collection key.
    /// </summary>
    /// <param name="key">The key associated with the requested item.</param>
    /// <returns>The item associated with the specified key.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the key does not exist and <see cref="IgnoreMissingItem"/> is
    /// disabled.
    /// </exception>
    protected object GetByKey(string key)
    {
        var idx = _keys.IndexOf(key);

        if (idx >= 0)
            return _items[idx];

        if (IgnoreMissingItem)
            return _missingItem!;

        throw new ArgumentException(
            "Argument 'Index' is not a valid value.",
            nameof(key));
    }

    /// <summary>
    /// Attempts to retrieve an item using its collection key.
    /// </summary>
    /// <param name="key">The key associated with the requested item.</param>
    /// <param name="item">
    /// When this method returns, contains the matching item or
    /// <see langword="null"/> when the key is not found.
    /// </param>
    /// <returns>
    /// <see langword="true"/> when the key is found; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    protected bool TryGetByKey(string key, out object? item)
    {
        var index = _keys.IndexOf(key);

        if (index >= 0)
        {
            item = _items[index];
            return true;
        }

        item = null;
        return false;
    }

    /// <summary>
    /// Gets a read-only view of the objects contained in the collection.
    /// </summary>
    protected IReadOnlyList<object> RawItems => _items;

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>An enumerator for the collection.</returns>
    public IEnumerator GetEnumerator() => _items.GetEnumerator();
}

/// <summary>
/// Represents an ordered collection of EDI segments.
/// </summary>
/// <remarks>
/// Segments are stored using one-based numeric string keys to support messages
/// containing repeated segment names.
/// </remarks>
public sealed class Segments :
    ItemObjectCollection,
    IEnumerable<Segment>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Segments"/> collection.
    /// </summary>
    public Segments() => MissingItem = new Segment();

    /// <summary>
    /// Gets a segment using its one-based position.
    /// </summary>
    /// <param name="oneBasedIndex">The one-based segment position.</param>
    /// <returns>The segment at the specified position.</returns>
    public Segment this[int oneBasedIndex] =>
        (Segment)GetByOneBasedIndex(oneBasedIndex);

    /// <summary>
    /// Gets a segment by matching an existing segment object.
    /// </summary>
    /// <param name="index">The segment object to locate.</param>
    /// <returns>The matching segment.</returns>
    public Segment this[object index] =>
        (Segment)GetByObject(index);

    /// <summary>
    /// Gets a segment using its collection key.
    /// </summary>
    /// <param name="key">
    /// The one-based numeric string key assigned when the message was parsed.
    /// </param>
    /// <returns>The segment associated with the specified key.</returns>
    /// <remarks>
    /// The current collection keys are numeric strings such as <c>"1"</c>,
    /// <c>"2"</c>, and <c>"3"</c>. Segment-name lookup is not currently supported
    /// because EDI messages may contain repeated segment identifiers.
    /// </remarks>
    public Segment this[string key] =>
        (Segment)GetByKey(key);

    IEnumerator<Segment> IEnumerable<Segment>.GetEnumerator() =>
        RawItems.Cast<Segment>().GetEnumerator();
}

/// <summary>
/// Represents an ordered collection of EDI fields.
/// </summary>
public sealed class Fields :
    ItemObjectCollection,
    IEnumerable<Field>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Fields"/> collection.
    /// </summary>
    public Fields() => MissingItem = new Field();

    /// <summary>
    /// Gets a field using its one-based position.
    /// </summary>
    public Field this[int oneBasedIndex] =>
        (Field)GetByOneBasedIndex(oneBasedIndex);

    /// <summary>
    /// Gets a field by matching an existing field object.
    /// </summary>
    public Field this[object index] =>
        (Field)GetByObject(index);

    /// <summary>
    /// Gets a field using its collection key.
    /// </summary>
    public Field this[string key] =>
        (Field)GetByKey(key);

    IEnumerator<Field> IEnumerable<Field>.GetEnumerator() =>
        RawItems.Cast<Field>().GetEnumerator();
}

/// <summary>
/// Represents an ordered collection of EDI components.
/// </summary>
public sealed class Components :
    ItemObjectCollection,
    IEnumerable<Component>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Components"/> collection.
    /// </summary>
    public Components() => MissingItem = new Component();

    /// <summary>
    /// Gets a component using its one-based position.
    /// </summary>
    public Component this[int oneBasedIndex] =>
        (Component)GetByOneBasedIndex(oneBasedIndex);

    /// <summary>
    /// Gets a component by matching an existing component object.
    /// </summary>
    public Component this[object index] =>
        (Component)GetByObject(index);

    /// <summary>
    /// Gets a component using its collection key.
    /// </summary>
    public Component this[string key] =>
        (Component)GetByKey(key);

    IEnumerator<Component> IEnumerable<Component>.GetEnumerator() =>
        RawItems.Cast<Component>().GetEnumerator();
}

/// <summary>
/// Represents an ordered collection of EDI subcomponents.
/// </summary>
public sealed class SubComponents :
    ItemObjectCollection,
    IEnumerable<SubComponent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SubComponents"/> collection.
    /// </summary>
    public SubComponents() => MissingItem = new SubComponent();

    /// <summary>
    /// Gets a subcomponent using its one-based position.
    /// </summary>
    public SubComponent this[int oneBasedIndex] =>
        (SubComponent)GetByOneBasedIndex(oneBasedIndex);

    /// <summary>
    /// Gets a subcomponent by matching an existing subcomponent object.
    /// </summary>
    public SubComponent this[object index] =>
        (SubComponent)GetByObject(index);

    /// <summary>
    /// Gets a subcomponent using its collection key.
    /// </summary>
    public SubComponent this[string key] =>
        (SubComponent)GetByKey(key);

    IEnumerator<SubComponent>
        IEnumerable<SubComponent>.GetEnumerator() =>
            RawItems.Cast<SubComponent>().GetEnumerator();
}

/// <summary>
/// Represents an ordered collection of repeated EDI values.
/// </summary>
public sealed class Repetitions :
    ItemObjectCollection,
    IEnumerable<Repetition>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Repetitions"/> collection.
    /// </summary>
    public Repetitions() => MissingItem = new Repetition();

    /// <summary>
    /// Gets a repetition using its one-based position.
    /// </summary>
    public Repetition this[int oneBasedIndex] =>
        (Repetition)GetByOneBasedIndex(oneBasedIndex);

    /// <summary>
    /// Gets a repetition by matching an existing repetition object.
    /// </summary>
    public Repetition this[object index] =>
        (Repetition)GetByObject(index);

    /// <summary>
    /// Gets a repetition using its collection key.
    /// </summary>
    public Repetition this[string key] =>
        (Repetition)GetByKey(key);

    IEnumerator<Repetition>
        IEnumerable<Repetition>.GetEnumerator() =>
            RawItems.Cast<Repetition>().GetEnumerator();
}

/// <summary>
/// Provides one-based access to the component collection associated with each
/// field repetition.
/// </summary>
public sealed class ComponentsByRepetitionIndexer
{
    private Components[] _items = [new Components()];
    private readonly ValueByRepetitionIndexer? _values;
    private readonly bool _ignoreMissingItem;

    /// <summary>
    /// Initializes an independent instance of the
    /// <see cref="ComponentsByRepetitionIndexer"/> class.
    /// </summary>
    public ComponentsByRepetitionIndexer()
    {
    }

    /// <summary>
    /// Initializes a new instance linked to a field's repeated values.
    /// </summary>
    /// <param name="values">
    /// The value indexer synchronized with this component indexer.
    /// </param>
    /// <param name="ignoreMissingItem">
    /// Indicates whether missing component collection items should return
    /// placeholders.
    /// </param>
    internal ComponentsByRepetitionIndexer(
        ValueByRepetitionIndexer values,
        bool ignoreMissingItem = true)
    {
        _values = values;
        _ignoreMissingItem = ignoreMissingItem;
        _items[0].IgnoreMissingItem = ignoreMissingItem;
    }

    /// <summary>
    /// Gets or sets the component collection associated with a field repetition.
    /// </summary>
    /// <param name="oneBasedIndex">
    /// The one-based field-repetition index.
    /// </param>
    /// <returns>
    /// The component collection associated with the specified field repetition.
    /// </returns>
    /// <exception cref="IndexOutOfRangeException">
    /// Thrown when the index is less than one or when a requested item exceeds
    /// the current collection length.
    /// </exception>
    public Components this[int oneBasedIndex]
    {
        get
        {
            if (oneBasedIndex < 1 ||
                oneBasedIndex > _items.Length)
            {
                throw new IndexOutOfRangeException();
            }

            return _items[oneBasedIndex - 1];
        }
        set
        {
            if (oneBasedIndex < 1)
                throw new IndexOutOfRangeException();

            if (oneBasedIndex > _items.Length)
            {
                // VB ReDim Preserve upper-bound Index-1
                // maps to C# array length Index.
                Array.Resize(
                    ref _items,
                    oneBasedIndex);

                for (var i = 0; i < _items.Length; i++)
                {
                    if (_items[i] is null)
                    {
                        _items[i] = new Components
                        {
                            IgnoreMissingItem =
                                _ignoreMissingItem
                        };
                    }
                }

                HasRepetition = true;

                if (_values is not null &&
                    oneBasedIndex > _values.Length)
                {
                    _values[oneBasedIndex] =
                        string.Empty;
                }
            }

            _items[oneBasedIndex - 1] =
                value ??
                new Components
                {
                    IgnoreMissingItem =
                        _ignoreMissingItem
                };
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether more than one field repetition
    /// is represented.
    /// </summary>
    public bool HasRepetition { get; set; }

    /// <summary>
    /// Gets the number of component collections represented by this indexer.
    /// </summary>
    public int Length => _items.Length;
}

/// <summary>
/// Provides internal one-based access to the values associated with each field
/// repetition.
/// </summary>
internal sealed class ValueByRepetitionIndexer
{
    private string[] _values = [string.Empty];
    private readonly Field? _field;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValueByRepetitionIndexer"/>
    /// class.
    /// </summary>
    /// <param name="field">
    /// The optional field used to resolve element-path lookups.
    /// </param>
    internal ValueByRepetitionIndexer(Field? field = null) =>
        _field = field;

    /// <summary>
    /// Gets or sets a value using a one-based field-repetition index.
    /// </summary>
    /// <param name="oneBasedIndex">
    /// The one-based field-repetition index.
    /// </param>
    /// <returns>The value stored at the specified repetition.</returns>
    /// <exception cref="IndexOutOfRangeException">
    /// Thrown when the index is less than one or exceeds the current value count.
    /// </exception>
    public string this[int oneBasedIndex]
    {
        get
        {
            if (oneBasedIndex < 1 ||
                oneBasedIndex > _values.Length)
            {
                throw new IndexOutOfRangeException();
            }

            return _values[oneBasedIndex - 1];
        }
        set
        {
            if (oneBasedIndex < 1)
                throw new IndexOutOfRangeException();

            if (oneBasedIndex > _values.Length)
            {
                Array.Resize(
                    ref _values,
                    oneBasedIndex);

                HasRepetition = true;
            }

            _values[oneBasedIndex - 1] =
                value ?? string.Empty;
        }
    }

    /// <summary>
    /// Gets or sets a value using an EDI element path and one-based field
    /// repetition index.
    /// </summary>
    /// <param name="element">
    /// The element path identifying a nested value within the field.
    /// </param>
    /// <param name="oneBasedIndex">
    /// The one-based field-repetition index.
    /// </param>
    /// <returns>The value identified by the path and repetition index.</returns>
    /// <exception cref="IndexOutOfRangeException">
    /// Thrown when the repetition index exceeds the current value count.
    /// </exception>
    public string this[
        string element,
        int oneBasedIndex]
    {
        get
        {
            if (oneBasedIndex > _values.Length)
                throw new IndexOutOfRangeException();

            return _field is null
                ? string.Empty
                : PathNavigator.GetFromField(
                    _field,
                    element,
                    oneBasedIndex);
        }
        set => this[oneBasedIndex] = value;
    }

    /// <summary>
    /// Gets a value from the first field repetition using an EDI element path.
    /// </summary>
    /// <param name="element">
    /// The element path identifying a nested value within the field.
    /// </param>
    /// <returns>The value identified by the element path.</returns>
    public string this[string element] =>
        _field is null
            ? string.Empty
            : PathNavigator.GetFromField(
                _field,
                element,
                1);

    /// <summary>
    /// Gets or sets a value indicating whether more than one field repetition
    /// is represented.
    /// </summary>
    public bool HasRepetition { get; set; }

    /// <summary>
    /// Gets the number of stored field-repetition values.
    /// </summary>
    public int Length => _values.Length;
}