// SPDX-FileCopyrightText: 2023 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 Slava0135 <40753025+Slava0135@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 Pieter-Jan Briers <pieterjan.briers+git@gmail.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using Robust.Shared.GameStates;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom;

namespace Content.Shared.Emp;

/// <summary>
/// While entity has this component it is "disabled" by EMP.
/// Add desired behaviour in other systems
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentPause]
[Access(typeof(SharedEmpSystem))]
public sealed partial class EmpDisabledComponent : Component
{
    /// <summary>
    /// Moment of time when component is removed and entity stops being "disabled"
    /// </summary>
    [DataField("timeLeft", customTypeSerializer: typeof(TimeOffsetSerializer)), ViewVariables(VVAccess.ReadWrite)]
    [AutoPausedField]
    public TimeSpan DisabledUntil;

    [DataField("effectCoolDown"), ViewVariables(VVAccess.ReadWrite)]
    public float EffectCooldown = 3f;

    /// <summary>
    /// When next effect will be spawned
    /// </summary>
    [AutoPausedField]
    public TimeSpan TargetTime = TimeSpan.Zero;
}
