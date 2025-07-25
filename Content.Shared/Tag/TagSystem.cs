// SPDX-FileCopyrightText: 2023 metalgearsloth <comedian_vs_clown@hotmail.com>
// SPDX-FileCopyrightText: 2024 Errant <35878406+Errant-4@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 Leon Friedrich <60421075+ElectroJr@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 Tayrtahn <tayrtahn@gmail.com>
// SPDX-FileCopyrightText: 2024 Tornado Tech <54727692+Tornado-Technology@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Tay <td12233a@gmail.com>
// SPDX-FileCopyrightText: 2025 pa.pecherskij <pa.pecherskij@interfax.ru>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using Robust.Shared.Prototypes;
using Robust.Shared.Utility;

namespace Content.Shared.Tag;

/// <summary>
/// The system that is responsible for working with tags.
/// Checking the existence of the <see cref="TagPrototype"/> only happens in DEBUG builds,
/// to improve performance, so don't forget to check it.
/// </summary>
/// <summary>
/// The methods to add or remove a list of tags have only an implementation with the <see cref="IEnumerable{T}"/> type,
/// it's not much, but it takes away performance,
/// if you need to use them often, it's better to make a proper implementation,
/// you can read more <a href="https://github.com/space-wizards/space-station-14/pull/28272">HERE</a>.
/// </summary>
public sealed class TagSystem : EntitySystem
{
    [Dependency] private readonly IPrototypeManager _proto = default!;

    private EntityQuery<TagComponent> _tagQuery;

    public override void Initialize()
    {
        base.Initialize();

        _tagQuery = GetEntityQuery<TagComponent>();

#if DEBUG
        SubscribeLocalEvent<TagComponent, ComponentInit>(OnTagInit);
#endif
    }

#if DEBUG
    private void OnTagInit(EntityUid uid, TagComponent component, ComponentInit args)
    {
        foreach (var tag in component.Tags)
        {
            AssertValidTag(tag);
        }
    }
#endif

    /// <summary>
    /// Tries to add a tag to an entity if the tag doesn't already exist.
    /// </summary>
    /// <returns>
    /// true if it was added, false otherwise even if it already existed.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if no <see cref="TagPrototype"/> exists with the given id.
    /// </exception>
    public bool AddTag(EntityUid entityUid, [ForbidLiteral] ProtoId<TagPrototype> tag)
    {
        return AddTag((entityUid, EnsureComp<TagComponent>(entityUid)), tag);
    }

    /// <summary>
    /// Tries to add the given tags to an entity if the tags don't already exist.
    /// </summary>
    /// <returns>
    /// true if any tags were added, false otherwise even if they all already existed.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if one of the ids represents an unregistered <see cref="TagPrototype"/>.
    /// </exception>
    public bool AddTags(EntityUid entityUid, [ForbidLiteral] params ProtoId<TagPrototype>[] tags)
    {
        return AddTags(entityUid, (IEnumerable<ProtoId<TagPrototype>>)tags);
    }

    /// <summary>
    /// Tries to add the given tags to an entity if the tags don't already exist.
    /// </summary>
    /// <returns>
    /// true if any tags were added, false otherwise even if they all already existed.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if one of the ids represents an unregistered <see cref="TagPrototype"/>.
    /// </exception>
    public bool AddTags(EntityUid entityUid, [ForbidLiteral] IEnumerable<ProtoId<TagPrototype>> tags)
    {
        return AddTags((entityUid, EnsureComp<TagComponent>(entityUid)), tags);
    }

    /// <summary>
    /// Tries to add a tag to an entity if it has a <see cref="TagComponent"/>
    /// and the tag doesn't already exist.
    /// </summary>
    /// <returns>
    /// true if it was added, false otherwise even if it already existed.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if no <see cref="TagPrototype"/> exists with the given id.
    /// </exception>
    public bool TryAddTag(EntityUid entityUid, [ForbidLiteral] ProtoId<TagPrototype> tag)
    {
        return _tagQuery.TryComp(entityUid, out var component) &&
               AddTag((entityUid, component), tag);
    }

    /// <summary>
    /// Tries to add the given tags to an entity if it has a
    /// <see cref="TagComponent"/> and the tags don't already exist.
    /// </summary>
    /// <returns>
    /// true if any tags were added, false otherwise even if they all already existed.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if one of the ids represents an unregistered <see cref="TagPrototype"/>.
    /// </exception>
    public bool TryAddTags(EntityUid entityUid, [ForbidLiteral] params ProtoId<TagPrototype>[] tags)
    {
        return TryAddTags(entityUid, (IEnumerable<ProtoId<TagPrototype>>)tags);
    }

    /// <summary>
    /// Tries to add the given tags to an entity if it has a
    /// <see cref="TagComponent"/> and the tags don't already exist.
    /// </summary>
    /// <returns>
    /// true if any tags were added, false otherwise even if they all already existed.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if one of the ids represents an unregistered <see cref="TagPrototype"/>.
    /// </exception>
    public bool TryAddTags(EntityUid entityUid, [ForbidLiteral] IEnumerable<ProtoId<TagPrototype>> tags)
    {
        return _tagQuery.TryComp(entityUid, out var component) &&
               AddTags((entityUid, component), tags);
    }

    /// <summary>
    /// Checks if a tag has been added to an entity.
    /// </summary>
    /// <returns>
    /// true if it exists, false otherwise.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if no <see cref="TagPrototype"/> exists with the given id.
    /// </exception>
    public bool HasTag(EntityUid entityUid, [ForbidLiteral] ProtoId<TagPrototype> tag)
    {
        return _tagQuery.TryComp(entityUid, out var component) &&
               HasTag(component, tag);
    }

    /// <summary>
    /// Checks if a tag has been added to an entity.
    /// </summary>
    /// <returns>
    /// true if it exists, false otherwise.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if no <see cref="TagPrototype"/> exists with the given id.
    /// </exception>
    public bool HasAllTags(EntityUid entityUid, ProtoId<TagPrototype> tag) =>
        HasTag(entityUid, tag);

    /// <summary>
    /// Checks if all of the given tags have been added to an entity.
    /// </summary>
    /// <returns>
    /// true if they all exist, false otherwise.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if one of the ids represents an unregistered <see cref="TagPrototype"/>.
    /// </exception>
    public bool HasAllTags(EntityUid entityUid, [ForbidLiteral] params ProtoId<TagPrototype>[] tags)
    {
        return _tagQuery.TryComp(entityUid, out var component) &&
               HasAllTags(component, tags);
    }

    /// <summary>
    /// Checks if all of the given tags have been added to an entity.
    /// </summary>
    /// <returns>
    /// true if they all exist, false otherwise.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if one of the ids represents an unregistered <see cref="TagPrototype"/>.
    /// </exception>
    public bool HasAllTags(EntityUid entityUid, [ForbidLiteral] HashSet<ProtoId<TagPrototype>> tags)
    {
        return _tagQuery.TryComp(entityUid, out var component) &&
               HasAllTags(component, tags);
    }

    /// <summary>
    /// Checks if all of the given tags have been added to an entity.
    /// </summary>
    /// <returns>
    /// true if they all exist, false otherwise.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if one of the ids represents an unregistered <see cref="TagPrototype"/>.
    /// </exception>
    public bool HasAllTags(EntityUid entityUid, [ForbidLiteral] List<ProtoId<TagPrototype>> tags)
    {
        return _tagQuery.TryComp(entityUid, out var component) &&
               HasAllTags(component, tags);
    }

    /// <summary>
    /// Checks if all of the given tags have been added to an entity.
    /// </summary>
    /// <returns>
    /// true if they all exist, false otherwise.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if one of the ids represents an unregistered <see cref="TagPrototype"/>.
    /// </exception>
    public bool HasAllTags(EntityUid entityUid, [ForbidLiteral] IEnumerable<ProtoId<TagPrototype>> tags)
    {
        return _tagQuery.TryComp(entityUid, out var component) &&
               HasAllTags(component, tags);
    }

    /// <summary>
    /// Checks if a tag has been added to an entity.
    /// </summary>
    /// <returns>
    /// true if it exists, false otherwise.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if no <see cref="TagPrototype"/> exists with the given id.
    /// </exception>
    public bool HasAnyTag(EntityUid entityUid, [ForbidLiteral] ProtoId<TagPrototype> tag) =>
        HasTag(entityUid, tag);

    /// <summary>
    /// Checks if any of the given tags have been added to an entity.
    /// </summary>
    /// <returns>
    /// true if any of them exist, false otherwise.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if one of the ids represents an unregistered <see cref="TagPrototype"/>.
    /// </exception>
    public bool HasAnyTag(EntityUid entityUid, [ForbidLiteral] params ProtoId<TagPrototype>[] tags)
    {
        return _tagQuery.TryComp(entityUid, out var component) &&
               HasAnyTag(component, tags);
    }

    /// <summary>
    /// Checks if any of the given tags have been added to an entity.
    /// </summary>
    /// <returns>
    /// true if any of them exist, false otherwise.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if one of the ids represents an unregistered <see cref="TagPrototype"/>.
    /// </exception>
    public bool HasAnyTag(EntityUid entityUid, [ForbidLiteral] HashSet<ProtoId<TagPrototype>> tags)
    {
        return _tagQuery.TryComp(entityUid, out var component) &&
               HasAnyTag(component, tags);
    }

    /// <summary>
    /// Checks if any of the given tags have been added to an entity.
    /// </summary>
    /// <returns>
    /// true if any of them exist, false otherwise.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if one of the ids represents an unregistered <see cref="TagPrototype"/>.
    /// </exception>
    public bool HasAnyTag(EntityUid entityUid, [ForbidLiteral] List<ProtoId<TagPrototype>> tags)
    {
        return _tagQuery.TryComp(entityUid, out var component) &&
               HasAnyTag(component, tags);
    }

    /// <summary>
    /// Checks if any of the given tags have been added to an entity.
    /// </summary>
    /// <returns>
    /// true if any of them exist, false otherwise.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if one of the ids represents an unregistered <see cref="TagPrototype"/>.
    /// </exception>
    public bool HasAnyTag(EntityUid entityUid, [ForbidLiteral] IEnumerable<ProtoId<TagPrototype>> tags)
    {
        return _tagQuery.TryComp(entityUid, out var component) &&
               HasAnyTag(component, tags);
    }

    /// <summary>
    /// Checks if a tag has been added to an component.
    /// </summary>
    /// <returns>
    /// true if it exists, false otherwise.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if no <see cref="TagPrototype"/> exists with the given id.
    /// </exception>
    public bool HasTag(TagComponent component, [ForbidLiteral] ProtoId<TagPrototype> tag)
    {
#if DEBUG
        AssertValidTag(tag);
#endif
        return component.Tags.Contains(tag);
    }

    /// <summary>
    /// Checks if a tag has been added to an component.
    /// </summary>
    /// <returns>
    /// true if it exists, false otherwise.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if no <see cref="TagPrototype"/> exists with the given id.
    /// </exception>
    public bool HasAllTags(TagComponent component, [ForbidLiteral] ProtoId<TagPrototype> tag) =>
        HasTag(component, tag);

    /// <summary>
    /// Checks if all of the given tags have been added to an component.
    /// </summary>
    /// <returns>
    /// true if they all exist, false otherwise.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if one of the ids represents an unregistered <see cref="TagPrototype"/>.
    /// </exception>
    public bool HasAllTags(TagComponent component, [ForbidLiteral] params ProtoId<TagPrototype>[] tags)
    {
        foreach (var tag in tags)
        {
#if DEBUG
            AssertValidTag(tag);
#endif
            if (!component.Tags.Contains(tag))
                return false;
        }

        return true;
    }

    /// <summary>
    /// Checks if all of the given tags have been added to an component.
    /// </summary>
    /// <returns>
    /// true if they all exist, false otherwise.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if one of the ids represents an unregistered <see cref="TagPrototype"/>.
    /// </exception>
    public bool HasAllTagsArray(TagComponent component, [ForbidLiteral] ProtoId<TagPrototype>[] tags)
    {
        foreach (var tag in tags)
        {
#if DEBUG
            AssertValidTag(tag);
#endif
            if (!component.Tags.Contains(tag))
                return false;
        }

        return true;
    }

    /// <summary>
    /// Checks if all of the given tags have been added to an component.
    /// </summary>
    /// <returns>
    /// true if they all exist, false otherwise.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if one of the ids represents an unregistered <see cref="TagPrototype"/>.
    /// </exception>
    public bool HasAllTags(TagComponent component, [ForbidLiteral] List<ProtoId<TagPrototype>> tags)
    {
        foreach (var tag in tags)
        {
#if DEBUG
            AssertValidTag(tag);
#endif
            if (!component.Tags.Contains(tag))
                return false;
        }

        return true;
    }

    /// <summary>
    /// Checks if all of the given tags have been added to an component.
    /// </summary>
    /// <returns>
    /// true if they all exist, false otherwise.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if one of the ids represents an unregistered <see cref="TagPrototype"/>.
    /// </exception>
    public bool HasAllTags(TagComponent component, [ForbidLiteral] HashSet<ProtoId<TagPrototype>> tags)
    {
        foreach (var tag in tags)
        {
#if DEBUG
            AssertValidTag(tag);
#endif
            if (!component.Tags.Contains(tag))
                return false;
        }

        return true;
    }

    /// <summary>
    /// Checks if all of the given tags have been added to an component.
    /// </summary>
    /// <returns>
    /// true if they all exist, false otherwise.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if one of the ids represents an unregistered <see cref="TagPrototype"/>.
    /// </exception>
    public bool HasAllTags(TagComponent component, [ForbidLiteral] IEnumerable<ProtoId<TagPrototype>> tags)
    {
        foreach (var tag in tags)
        {
#if DEBUG
            AssertValidTag(tag);
#endif
            if (!component.Tags.Contains(tag))
                return false;
        }

        return true;
    }

    /// <summary>
    /// Checks if a tag has been added to an component.
    /// </summary>
    /// <returns>
    /// true if it exists, false otherwise.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if no <see cref="TagPrototype"/> exists with the given id.
    /// </exception>
    public bool HasAnyTag(TagComponent component, [ForbidLiteral] ProtoId<TagPrototype> tag) =>
        HasTag(component, tag);

    /// <summary>
    /// Checks if any of the given tags have been added to an component.
    /// </summary>
    /// <returns>
    /// true if any of them exist, false otherwise.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if one of the ids represents an unregistered <see cref="TagPrototype"/>.
    /// </exception>
    public bool HasAnyTag(TagComponent component, [ForbidLiteral] params ProtoId<TagPrototype>[] tags)
    {
        foreach (var tag in tags)
        {
#if DEBUG
            AssertValidTag(tag);
#endif
            if (component.Tags.Contains(tag))
                return true;
        }

        return false;
    }

    /// <summary>
    /// Checks if any of the given tags have been added to an component.
    /// </summary>
    /// <returns>
    /// true if any of them exist, false otherwise.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if one of the ids represents an unregistered <see cref="TagPrototype"/>.
    /// </exception>
    public bool HasAnyTag(TagComponent component, [ForbidLiteral] HashSet<ProtoId<TagPrototype>> tags)
    {
        foreach (var tag in tags)
        {
#if DEBUG
            AssertValidTag(tag);
#endif
            if (component.Tags.Contains(tag))
                return true;
        }

        return false;
    }

    /// <summary>
    /// Checks if any of the given tags have been added to an component.
    /// </summary>
    /// <returns>
    /// true if any of them exist, false otherwise.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if one of the ids represents an unregistered <see cref="TagPrototype"/>.
    /// </exception>
    public bool HasAnyTag(TagComponent component, [ForbidLiteral] List<ProtoId<TagPrototype>> tags)
    {
        foreach (var tag in tags)
        {
#if DEBUG
            AssertValidTag(tag);
#endif
            if (component.Tags.Contains(tag))
                return true;
        }

        return false;
    }

    /// <summary>
    /// Checks if any of the given tags have been added to an component.
    /// </summary>
    /// <returns>
    /// true if any of them exist, false otherwise.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if one of the ids represents an unregistered <see cref="TagPrototype"/>.
    /// </exception>
    public bool HasAnyTag(TagComponent component, [ForbidLiteral] IEnumerable<ProtoId<TagPrototype>> tags)
    {
        foreach (var tag in tags)
        {
#if DEBUG
            AssertValidTag(tag);
#endif
            if (component.Tags.Contains(tag))
                return true;
        }

        return false;
    }

    /// <summary>
    /// Tries to remove a tag from an entity if it exists.
    /// </summary>
    /// <returns>
    /// true if it was removed, false otherwise even if it didn't exist.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if no <see cref="TagPrototype"/> exists with the given id.
    /// </exception>
    public bool RemoveTag(EntityUid entityUid, [ForbidLiteral] ProtoId<TagPrototype> tag)
    {
        return _tagQuery.TryComp(entityUid, out var component) &&
               RemoveTag((entityUid, component), tag);
    }

    /// <summary>
    /// Tries to remove a tag from an entity if it exists.
    /// </summary>
    /// <returns>
    /// true if it was removed, false otherwise even if it didn't exist.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if one of the ids represents an unregistered <see cref="TagPrototype"/>.
    /// </exception>
    public bool RemoveTags(EntityUid entityUid, [ForbidLiteral] params ProtoId<TagPrototype>[] tags)
    {
        return RemoveTags(entityUid, (IEnumerable<ProtoId<TagPrototype>>)tags);
    }

    /// <summary>
    /// Tries to remove a tag from an entity if it exists.
    /// </summary>
    /// <returns>
    /// true if it was removed, false otherwise even if it didn't exist.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if one of the ids represents an unregistered <see cref="TagPrototype"/>.
    /// </exception>
    public bool RemoveTags(EntityUid entityUid, [ForbidLiteral] IEnumerable<ProtoId<TagPrototype>> tags)
    {
        return _tagQuery.TryComp(entityUid, out var component) &&
               RemoveTags((entityUid, component), tags);
    }

    /// <summary>
    /// Tries to add a tag if it doesn't already exist.
    /// </summary>
    /// <returns>
    /// true if it was added, false if it already existed.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if no <see cref="TagPrototype"/> exists with the given id.
    /// </exception>
    public bool AddTag(Entity<TagComponent> entity, [ForbidLiteral] ProtoId<TagPrototype> tag)
    {
#if DEBUG
        AssertValidTag(tag);
#endif
        if (!entity.Comp.Tags.Add(tag))
            return false;

        Dirty(entity);
        return true;
    }

    /// <summary>
    /// Tries to add the given tags if they don't already exist.
    /// </summary>
    /// <returns>
    /// true if any tags were added, false if they all already existed.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if one of the ids represents an unregistered <see cref="TagPrototype"/>.
    /// </exception>
    public bool AddTags(Entity<TagComponent> entity, [ForbidLiteral] params ProtoId<TagPrototype>[] tags)
    {
        return AddTags(entity, (IEnumerable<ProtoId<TagPrototype>>)tags);
    }

    /// <summary>
    /// Tries to add the given tags if they don't already exist.
    /// </summary>
    /// <returns>
    /// true if any tags were added, false if they all already existed.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if one of the ids represents an unregistered <see cref="TagPrototype"/>.
    /// </exception>
    public bool AddTags(Entity<TagComponent> entity, [ForbidLiteral] IEnumerable<ProtoId<TagPrototype>> tags)
    {
        var update = false;
        foreach (var tag in tags)
        {
#if DEBUG
            AssertValidTag(tag);
#endif
            if (entity.Comp.Tags.Add(tag) && !update)
                update = true;
        }

        if (!update)
            return false;

        Dirty(entity);
        return true;
    }

    /// <summary>
    /// Tries to remove a tag if it exists.
    /// </summary>
    /// <returns>
    /// true if it was removed, false otherwise even if it didn't exist.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if no <see cref="TagPrototype"/> exists with the given id.
    /// </exception>
    public bool RemoveTag(Entity<TagComponent> entity, [ForbidLiteral] ProtoId<TagPrototype> tag)
    {
#if DEBUG
        AssertValidTag(tag);
#endif

        if (!entity.Comp.Tags.Remove(tag))
            return false;

        Dirty(entity);
        return true;
    }

    /// <summary>
    /// Tries to remove all of the given tags if they exist.
    /// </summary>
    /// <returns>
    /// true if any tag was removed, false otherwise.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if one of the ids represents an unregistered <see cref="TagPrototype"/>.
    /// </exception>
    public bool RemoveTags(Entity<TagComponent> entity, [ForbidLiteral] params ProtoId<TagPrototype>[] tags)
    {
        return RemoveTags(entity, (IEnumerable<ProtoId<TagPrototype>>)tags);
    }

    /// <summary>
    /// Tries to remove all of the given tags if they exist.
    /// </summary>
    /// <returns>
    /// true if any tag was removed, false otherwise.
    /// </returns>
    /// <exception cref="UnknownPrototypeException">
    /// Thrown if one of the ids represents an unregistered <see cref="TagPrototype"/>.
    /// </exception>
    public bool RemoveTags(Entity<TagComponent> entity, [ForbidLiteral] IEnumerable<ProtoId<TagPrototype>> tags)
    {
        var update = false;
        foreach (var tag in tags)
        {
#if DEBUG
            AssertValidTag(tag);
#endif
            if (entity.Comp.Tags.Remove(tag) && !update)
                update = true;
        }

        if (!update)
            return false;

        Dirty(entity);
        return true;
    }

    private void AssertValidTag(string id)
    {
        DebugTools.Assert(_proto.HasIndex<TagPrototype>(id), $"Unknown tag: {id}");
    }
}
