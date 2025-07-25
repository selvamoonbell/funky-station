// SPDX-FileCopyrightText: 2021 20kdc <asdd2808@gmail.com>
// SPDX-FileCopyrightText: 2021 Vera Aguilera Puerto <6766154+Zumorica@users.noreply.github.com>
// SPDX-FileCopyrightText: 2022 Leon Friedrich <60421075+ElectroJr@users.noreply.github.com>
// SPDX-FileCopyrightText: 2022 mirrorcult <lunarautomaton6@gmail.com>
// SPDX-FileCopyrightText: 2022 wrexbe <81056464+wrexbe@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 Pieter-Jan Briers <pieterjan.briers+git@gmail.com>
// SPDX-FileCopyrightText: 2023 TemporalOroboros <TemporalOroboros@gmail.com>
// SPDX-FileCopyrightText: 2023 Visne <39844191+Visne@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 Mervill <mervills.email@gmail.com>
// SPDX-FileCopyrightText: 2024 Nemanja <98561806+EmoGarbage404@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 Piras314 <p1r4s@proton.me>
// SPDX-FileCopyrightText: 2024 Tadeo <td12233a@gmail.com>
// SPDX-FileCopyrightText: 2024 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using Robust.Client.GameObjects;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.CustomControls;

namespace Content.Client.Computer
{
    /// <summary>
    /// ComputerBoundUserInterface shunts all sorts of responsibilities that are in the BoundUserInterface for architectural reasons into the Window.
    /// NOTE: Despite the name, ComputerBoundUserInterface does not and will not care about things like power.
    /// </summary>
    [Virtual]
    public class ComputerBoundUserInterface<TWindow, TState> : ComputerBoundUserInterfaceBase where TWindow : BaseWindow, IComputerWindow<TState>, new() where TState : BoundUserInterfaceState
    {
        [ViewVariables]
        private TWindow? _window;

        protected override void Open()
        {
            base.Open();

            _window = this.CreateWindow<TWindow>();
            _window.SetupComputerWindow(this);
        }

        // Alas, this constructor has to be copied to the subclass. :(
        public ComputerBoundUserInterface(EntityUid owner, Enum uiKey) : base(owner, uiKey)
        {
        }

        protected override void UpdateState(BoundUserInterfaceState state)
        {
            base.UpdateState(state);

            if (_window == null)
            {
                return;
            }

            _window.UpdateState((TState) state);
        }

        protected override void ReceiveMessage(BoundUserInterfaceMessage message)
        {
            _window?.ReceiveMessage(message);
        }
    }

    /// <summary>
    /// This class is to avoid a lot of &lt;&gt; being written when we just want to refer to SendMessage.
    /// We could instead qualify a lot of generics even further, but that is a waste of time.
    /// </summary>
    [Virtual]
    public class ComputerBoundUserInterfaceBase : BoundUserInterface
    {
        public ComputerBoundUserInterfaceBase(EntityUid owner, Enum uiKey) : base(owner, uiKey)
        {
        }

        public new void SendMessage(BoundUserInterfaceMessage msg)
        {
            base.SendMessage(msg);
        }
    }

    public interface IComputerWindow<TState>
    {
        void SetupComputerWindow(ComputerBoundUserInterfaceBase cb)
        {
        }

        void UpdateState(TState state)
        {
        }

        void ReceiveMessage(BoundUserInterfaceMessage message)
        {
        }
    }
}

