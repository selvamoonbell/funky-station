// SPDX-FileCopyrightText: 2023 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 JoulesBerg <104539820+JoulesBerg@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 slarticodefast <161409025+slarticodefast@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using Content.Server.Explosion.EntitySystems;
using Robust.Shared.Prototypes;

namespace Content.Server.Explosion.Components;

/// <summary>
///     Spawns a protoype when triggered.
/// </summary>
[RegisterComponent, Access(typeof(TriggerSystem))]
public sealed partial class SpawnOnTriggerComponent : Component
{
    /// <summary>
    ///     The prototype to spawn.
    /// </summary>
    [DataField(required: true)]
    public EntProtoId Proto = string.Empty;

    /// <summary>
    ///     Use MapCoordinates for spawning?
    ///     Set to true if you don't want the new entity parented to the spawner.
    /// </summary>
    [DataField]
    public bool mapCoords;
}
