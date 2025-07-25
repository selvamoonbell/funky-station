// SPDX-FileCopyrightText: 2025 ATDoop <bug@bug.bug>
// SPDX-FileCopyrightText: 2025 corresp0nd <46357632+corresp0nd@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later AND MIT

using System.Linq;
using Content.Client.Chat.Managers;
using Content.Client.Message;
using Content.Shared._Impstation.Thaven;
using Robust.Client.AutoGenerated;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.XAML;
using Robust.Shared.Prototypes;
using Robust.Shared.Utility;

namespace Content.Client._Impstation.Thaven;

[GenerateTypedNameReferences]
public sealed partial class MoodDisplay : Control
{
    [Dependency] private readonly IPrototypeManager _prototypeManager = default!;
    [Dependency] private readonly IChatManager _chatManager = default!;
    [Dependency] private readonly EntityManager _entityManager = default!;

    private string GetSharedString()
    {
        return $"[italic][font size=10][color=gray]{Loc.GetString("moods-ui-shared-mood")}[/color][/font][/italic]";
    }

    public MoodDisplay(ThavenMood mood, bool shared)
    {
        RobustXamlLoader.Load(this);
        IoCManager.InjectDependencies(this);

        if (shared)
            MoodNameLabel.SetMarkup($"{mood.GetLocName()} {GetSharedString()}");
        else
            MoodNameLabel.SetMarkup(mood.GetLocName());
        MoodDescLabel.SetMarkup(mood.GetLocDesc());
    }
}
