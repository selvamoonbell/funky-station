// SPDX-FileCopyrightText: 2023 Nemanja <98561806+EmoGarbage404@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Tay <td12233a@gmail.com>
// SPDX-FileCopyrightText: 2025 slarticodefast <161409025+slarticodefast@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using Content.Shared.Chemistry.EntitySystems;
using Content.Shared.Chemistry.Reaction;
using Robust.Shared.Audio;
using Robust.Shared.Audio.Components;
using Robust.Shared.GameStates;
using Robust.Shared.Serialization;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom;

namespace Content.Shared.Chemistry.Components;

/// <summary>
/// This is used for an entity that uses <see cref="ReactionMixerComponent"/> to mix any container with a solution after a period of time.
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
[Access(typeof(SharedSolutionContainerMixerSystem))]
public sealed partial class SolutionContainerMixerComponent : Component
{
    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public string ContainerId = "mixer";

    [DataField, AutoNetworkedField]
    public bool Mixing;

    /// <summary>
    /// How long it takes for mixing to occurs.
    /// </summary>
    [DataField, ViewVariables(VVAccess.ReadWrite), AutoNetworkedField]
    public TimeSpan MixDuration;

    [DataField(customTypeSerializer: typeof(TimeOffsetSerializer)), ViewVariables(VVAccess.ReadWrite), AutoNetworkedField]
    public TimeSpan MixTimeEnd;

    [DataField, AutoNetworkedField]
    public SoundSpecifier? MixingSound;

    [ViewVariables]
    public Entity<AudioComponent>? MixingSoundEntity;
}

[Serializable, NetSerializable]
public enum SolutionContainerMixerVisuals : byte
{
    Mixing
}
