// SPDX-FileCopyrightText: 2024 Ed <96445749+TheShuEd@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT


namespace Content.Server.Anomaly.Components;

/// <summary>
/// prohibits the possibility of anomalies appearing in the specified radius around the entity
/// </summary>
[RegisterComponent, Access(typeof(AnomalySystem))]
public sealed partial class AntiAnomalyZoneComponent : Component
{
    /// <summary>
    /// the radius in which anomalies cannot appear
    /// </summary>
    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public float ZoneRadius = 10;
}
