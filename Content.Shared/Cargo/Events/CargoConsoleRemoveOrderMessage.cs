// SPDX-FileCopyrightText: 2022 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 Eoin Mcloughlin <helloworld@eoinrul.es>
// SPDX-FileCopyrightText: 2023 eoineoineoin <eoin.mcloughlin+gh@gmail.com>
// SPDX-FileCopyrightText: 2023 eoineoineoin <github@eoinrul.es>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using Robust.Shared.Serialization;

namespace Content.Shared.Cargo.Events;

/// <summary>
///     Remove order from database.
/// </summary>
[Serializable, NetSerializable]
public sealed class CargoConsoleRemoveOrderMessage : BoundUserInterfaceMessage
{
    public int OrderId;

    public CargoConsoleRemoveOrderMessage(int orderId)
    {
        OrderId = orderId;
    }
}
