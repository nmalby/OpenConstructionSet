﻿namespace OpenConstructionSet.Mods;

/// <summary>
/// Represents the data stored within a <see cref="IModFile"/>.
/// </summary>
/// <param name="Header">The <see cref="Header"/> of the <see cref="IModFile"/>.</param>
/// <param name="LastId">The last Id used to generate a new <see cref="IItem"/>.</param>
/// <param name="Items">The collection of <see cref="Item"/>s of the <see cref="IModFile"/>.</param>
/// <param name="Info">Optional data contained within the <see cref="IModFile"/>'s .info file.</param>
public sealed record ModFileData(Header Header, int LastId, List<Item> Items, ModInfoData? Info)
{
    /// <summary>
    /// Creates a new <see cref="ModFileData"/>.
    /// </summary>
    /// <param name="header">The mod's header.</param>
    /// <param name="lastId">The last ID number used when creating new items.</param>
    /// <param name="items">The collection of <see cref="Item"/>s of the <see cref="IModFile"/>.</param>
    /// <param name="info">Optional data contained within the <see cref="IModFile"/>'s .info file.</param>
    public ModFileData(Header header, int lastId, IEnumerable<Item> items, ModInfoData? info) : this(header, lastId, items.ToList(), info)
    {
    }

    /// <summary>
    /// The <see cref="Header"/> of the <see cref="IModFile"/>.
    /// </summary>
    public Header Header { get; init; } = Header;

    /// <summary>
    /// The last Id used to generate a new <see cref="IItem"/>.
    /// </summary>
    public int LastId { get; init; } = LastId;

    /// <summary>
    /// The collection of <see cref="Item"/>s of the <see cref="IModFile"/>.
    /// </summary>
    public List<Item> Items { get; init; } = Items;

    /// <summary>
    /// Optional data contained within the <see cref="IModFile"/>'s .info file.
    /// </summary>
    public ModInfoData? Info { get; init; } = Info;
}
