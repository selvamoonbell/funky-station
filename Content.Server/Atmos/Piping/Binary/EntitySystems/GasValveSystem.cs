// SPDX-FileCopyrightText: 2021 E F R <602406+Efruit@users.noreply.github.com>
// SPDX-FileCopyrightText: 2021 Swept <sweptwastaken@protonmail.com>
// SPDX-FileCopyrightText: 2021 Vera Aguilera Puerto <6766154+Zumorica@users.noreply.github.com>
// SPDX-FileCopyrightText: 2021 Vera Aguilera Puerto <gradientvera@outlook.com>
// SPDX-FileCopyrightText: 2022 Moony <moonheart08@users.noreply.github.com>
// SPDX-FileCopyrightText: 2022 keronshb <54602815+keronshb@users.noreply.github.com>
// SPDX-FileCopyrightText: 2022 wrexbe <81056464+wrexbe@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 Pieter-Jan Briers <pieterjan.briers@gmail.com>
// SPDX-FileCopyrightText: 2023 Visne <39844191+Visne@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 faint <46868845+ficcialfaint@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 Leon Friedrich <60421075+ElectroJr@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 Nemanja <98561806+EmoGarbage404@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using Content.Server.Atmos.Piping.Binary.Components;
using Content.Server.NodeContainer;
using Content.Server.NodeContainer.EntitySystems;
using Content.Server.NodeContainer.Nodes;
using Content.Shared.Atmos.Piping;
using Content.Shared.Audio;
using Content.Shared.Examine;
using Content.Shared.Interaction;
using JetBrains.Annotations;
using Robust.Shared.Audio;
using Robust.Shared.Audio.Systems;
using Robust.Shared.Player;

namespace Content.Server.Atmos.Piping.Binary.EntitySystems
{
    [UsedImplicitly]
    public sealed class GasValveSystem : EntitySystem
    {
        [Dependency] private readonly SharedAmbientSoundSystem _ambientSoundSystem = default!;
        [Dependency] private readonly SharedAppearanceSystem _appearance = default!;
        [Dependency] private readonly SharedAudioSystem _audio = default!;
        [Dependency] private readonly NodeContainerSystem _nodeContainer = default!;

        public override void Initialize()
        {
            base.Initialize();

            SubscribeLocalEvent<GasValveComponent, ComponentStartup>(OnStartup);
            SubscribeLocalEvent<GasValveComponent, ActivateInWorldEvent>(OnActivate);
            SubscribeLocalEvent<GasValveComponent, ExaminedEvent>(OnExamined);
        }

        private void OnExamined(Entity<GasValveComponent> ent, ref ExaminedEvent args)
        {
            var valve = ent.Comp;
            if (!Comp<TransformComponent>(ent).Anchored || !args.IsInDetailsRange) // Not anchored? Out of range? No status.
                return;

            if (Loc.TryGetString("gas-valve-system-examined", out var str,
                    ("statusColor", valve.Open ? "green" : "orange"),
                    ("open", valve.Open)))
            {
                args.PushMarkup(str);
            }
        }

        private void OnStartup(EntityUid uid, GasValveComponent component, ComponentStartup args)
        {
            // We call set in startup so it sets the appearance, node state, etc.
            Set(uid, component, component.Open);
        }

        private void OnActivate(EntityUid uid, GasValveComponent component, ActivateInWorldEvent args)
        {
            if (args.Handled || !args.Complex)
                return;

            Toggle(uid, component);
            _audio.PlayPvs(component.ValveSound, uid, AudioParams.Default.WithVariation(0.25f));
            args.Handled = true;
        }

        public void Set(EntityUid uid, GasValveComponent component, bool value)
        {
            component.Open = value;

            if (_nodeContainer.TryGetNodes(uid, component.InletName, component.OutletName, out PipeNode? inlet, out PipeNode? outlet))
            {
                if (TryComp<AppearanceComponent>(uid, out var appearance))
                {
                    _appearance.SetData(uid, FilterVisuals.Enabled, component.Open, appearance);
                }
                if (component.Open)
                {
                    inlet.AddAlwaysReachable(outlet);
                    outlet.AddAlwaysReachable(inlet);
                    _ambientSoundSystem.SetAmbience(uid, true);
                }
                else
                {
                    inlet.RemoveAlwaysReachable(outlet);
                    outlet.RemoveAlwaysReachable(inlet);
                    _ambientSoundSystem.SetAmbience(uid, false);
                }
            }
        }

        public void Toggle(EntityUid uid, GasValveComponent component)
        {
            Set(uid, component, !component.Open);
        }
    }
}
