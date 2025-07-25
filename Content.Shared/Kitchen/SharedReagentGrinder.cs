// SPDX-FileCopyrightText: 2022 0x6273 <0x40@keemail.me>
// SPDX-FileCopyrightText: 2023 Emisse <99158783+Emisse@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 Leon Friedrich <60421075+ElectroJr@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 TemporalOroboros <TemporalOroboros@gmail.com>
// SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 Crotalus <Crotalus@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using Content.Shared.Chemistry.Reagent;
using Robust.Shared.Serialization;

namespace Content.Shared.Kitchen
{
    public sealed class SharedReagentGrinder
    {
        public static string BeakerSlotId = "beakerSlot";

        public static string InputContainerId = "inputContainer";
    }

    [Serializable, NetSerializable]
    public sealed class ReagentGrinderToggleAutoModeMessage : BoundUserInterfaceMessage
    {
        public ReagentGrinderToggleAutoModeMessage() { }
    }

    [Serializable, NetSerializable]
    public sealed class ReagentGrinderStartMessage : BoundUserInterfaceMessage
    {
        public readonly GrinderProgram Program;
        public ReagentGrinderStartMessage(GrinderProgram program)
        {
            Program = program;
        }
    }

    [Serializable, NetSerializable]
    public sealed class ReagentGrinderEjectChamberAllMessage : BoundUserInterfaceMessage
    {
        public ReagentGrinderEjectChamberAllMessage()
        {
        }
    }

    [Serializable, NetSerializable]
    public sealed class ReagentGrinderEjectChamberContentMessage : BoundUserInterfaceMessage
    {
        public NetEntity EntityId;
        public ReagentGrinderEjectChamberContentMessage(NetEntity entityId)
        {
            EntityId = entityId;
        }
    }

    [Serializable, NetSerializable]
    public sealed class ReagentGrinderWorkStartedMessage : BoundUserInterfaceMessage
    {
        public GrinderProgram GrinderProgram;
        public ReagentGrinderWorkStartedMessage(GrinderProgram grinderProgram)
        {
            GrinderProgram = grinderProgram;
        }
    }

    [Serializable, NetSerializable]
    public sealed class ReagentGrinderWorkCompleteMessage : BoundUserInterfaceMessage
    {
        public ReagentGrinderWorkCompleteMessage()
        {
        }
    }

    [Serializable, NetSerializable]
    public enum ReagentGrinderVisualState : byte
    {
        BeakerAttached
    }

    [Serializable, NetSerializable]
    public enum GrinderProgram : byte
    {
        Grind,
        Juice
    }

    [NetSerializable, Serializable]
    public enum ReagentGrinderUiKey : byte
    {
        Key
    }

    public enum GrinderAutoMode : byte
    {
        Off,
        Grind,
        Juice
    }

    [NetSerializable, Serializable]
    public sealed class ReagentGrinderInterfaceState : BoundUserInterfaceState
    {
        public bool IsBusy;
        public bool HasBeakerIn;
        public bool Powered;
        public bool CanJuice;
        public bool CanGrind;
        public NetEntity[] ChamberContents;
        public ReagentQuantity[]? ReagentQuantities;
        public GrinderAutoMode AutoMode;

        public ReagentGrinderInterfaceState(bool isBusy, bool hasBeaker, bool powered, bool canJuice, bool canGrind, GrinderAutoMode autoMode, NetEntity[] chamberContents, ReagentQuantity[]? heldBeakerContents)
        {
            IsBusy = isBusy;
            HasBeakerIn = hasBeaker;
            Powered = powered;
            CanJuice = canJuice;
            CanGrind = canGrind;
            AutoMode = autoMode;
            ChamberContents = chamberContents;
            ReagentQuantities = heldBeakerContents;
        }
    }
}
