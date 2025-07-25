// SPDX-FileCopyrightText: 2024 Ed <96445749+TheShuEd@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 Tadeo <td12233a@gmail.com>
// SPDX-FileCopyrightText: 2024 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using System.Numerics;
using Robust.Shared.Map;

namespace Content.Shared.Random.Rules;

/// <summary>
/// Returns true if on a grid or in range of one.
/// </summary>
public sealed partial class GridInRangeRule : RulesRule
{
    [DataField]
    public float Range = 10f;

    public override bool Check(EntityManager entManager, EntityUid uid)
    {
        if (!entManager.TryGetComponent(uid, out TransformComponent? xform))
        {
            return false;
        }

        if (xform.GridUid != null)
        {
            return !Inverted;
        }

        var transform = entManager.System<SharedTransformSystem>();
        var mapManager = IoCManager.Resolve<IMapManager>();

        var worldPos = transform.GetWorldPosition(xform);
        var gridRange = new Vector2(Range, Range);

        foreach (var _ in mapManager.FindGridsIntersecting(xform.MapID, new Box2(worldPos - gridRange, worldPos + gridRange)))
        {
            return !Inverted;
        }

        return Inverted;
    }
}
