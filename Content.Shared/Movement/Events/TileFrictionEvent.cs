// SPDX-FileCopyrightText: 2022 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

namespace Content.Shared.Movement.Events;

/// <summary>
/// Raised to try and get any tile friction modifiers for a particular body.
/// </summary>
[ByRefEvent]
public struct TileFrictionEvent
{
    public float Modifier;

    public TileFrictionEvent(float modifier)
    {
        Modifier = modifier;
    }
}
