// SPDX-FileCopyrightText: 2024 deltanedas <39013340+deltanedas@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using Content.Server.Antag.Components;

namespace Content.Server.Antag;

/// <summary>
/// Spawns an entity when creating an antag for <see cref="AntagSpawnerComponent"/>.
/// </summary>
public sealed class AntagSpawnerSystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<AntagSpawnerComponent, AntagSelectEntityEvent>(OnSelectEntity);
    }

    private void OnSelectEntity(Entity<AntagSpawnerComponent> ent, ref AntagSelectEntityEvent args)
    {
        args.Entity = Spawn(ent.Comp.Prototype);
    }
}
