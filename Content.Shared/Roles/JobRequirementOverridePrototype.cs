// SPDX-FileCopyrightText: 2024 Leon Friedrich <60421075+ElectroJr@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using Robust.Shared.Prototypes;

namespace Content.Shared.Roles;

/// <summary>
/// Collection of job, antag, and ghost-role job requirements for per-server requirement overrides.
/// </summary>
[Prototype]
public sealed partial class JobRequirementOverridePrototype : IPrototype
{
    [ViewVariables]
    [IdDataField]
    public string ID { get; private set; } = default!;

    [DataField]
    public Dictionary<ProtoId<JobPrototype>, HashSet<JobRequirement>> Jobs = new ();

    [DataField]
    public Dictionary<ProtoId<AntagPrototype>, HashSet<JobRequirement>> Antags = new ();
}
