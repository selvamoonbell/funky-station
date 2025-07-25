// SPDX-FileCopyrightText: 2023 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 Kara <lunarautomaton6@gmail.com>
// SPDX-FileCopyrightText: 2023 Nemanja <98561806+EmoGarbage404@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 Ilya246 <57039557+Ilya246@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 Tadeo <td12233a@gmail.com>
// SPDX-FileCopyrightText: 2025 Steve <marlumpy@gmail.com>
// SPDX-FileCopyrightText: 2025 marc-pelletier <113944176+marc-pelletier@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using Content.Server.StationEvents.Events;
using Content.Shared.Atmos;
using Robust.Shared.Map;

namespace Content.Server.StationEvents.Components;

[RegisterComponent, Access(typeof(GasLeakRule))]
public sealed partial class GasLeakRuleComponent : Component
{
    public readonly Gas[] LeakableGases =
    {
        Gas.Ammonia,
        Gas.Plasma,
        Gas.Tritium,
        Gas.Frezon,
        Gas.WaterVapor, // the fog
        Gas.BZ, // Assmos - /tg/ gases
        Gas.Healium, // Assmos - /tg/ gases
        Gas.Nitrium, // Assmos - /tg/ gases
        Gas.Pluoxium, // Assmos - /tg/ gases
        Gas.HyperNoblium, // Assmos - /tg/ gases
        Gas.ProtoNitrate, // Assmos - /tg/ gases
        Gas.Halon, // Assmos - /tg/ gases
        Gas.Helium, // Assmos - /tg/ gases
        Gas.AntiNoblium, // Assmos - /tg/ gases
    };

    /// <summary>
    ///     Running cooldown of how much time until another leak.
    /// </summary>
    public float TimeUntilLeak;

    /// <summary>
    ///     How long between more gas being added to the tile.
    /// </summary>
    public float LeakCooldown = 1.0f;

    // Event variables
    public EntityUid TargetStation;
    public EntityUid TargetGrid;
    public Vector2i TargetTile;
    public EntityCoordinates TargetCoords;
    public bool FoundTile;
    public Gas LeakGas;
    public float MolesPerSecond;
    public readonly int MinimumMolesPerSecond = 80;

    /// <summary>
    ///     Don't want to make it too fast to give people time to flee.
    /// </summary>
    public int MaximumMolesPerSecond = 200;

    public int MinimumGas = 1000;
    public int MaximumGas = 4000;
    public float SparkChance = 0.05f;
}
