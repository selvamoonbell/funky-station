# SPDX-FileCopyrightText: 2022 Rane <60792108+Elijahrane@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 crazybrain23 <44417085+crazybrain23@users.noreply.github.com>
# SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 Guillaume E <262623+quatre@users.noreply.github.com>
# SPDX-FileCopyrightText: 2024 Kara <lunarautomaton6@gmail.com>
# SPDX-FileCopyrightText: 2024 Nemanja <98561806+EmoGarbage404@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
#
# SPDX-License-Identifier: MIT

ammonia-smell = Something smells pungent!

## Perishable

perishable-1 = [color=green]{ CAPITALIZE(POSS-ADJ($target)) } corpse still looks fresh.[/color]
perishable-2 = [color=orangered]{ CAPITALIZE(POSS-ADJ($target)) } corpse looks somewhat fresh.[/color]
perishable-3 = [color=red]{ CAPITALIZE(POSS-ADJ($target)) } corpse doesn't look very fresh.[/color]

perishable-1-nonmob = [color=green]{ CAPITALIZE(SUBJECT($target)) } still looks fresh.[/color]
perishable-2-nonmob = [color=orangered]{ CAPITALIZE(SUBJECT($target)) } looks somewhat fresh.[/color]
perishable-3-nonmob = [color=red]{ CAPITALIZE(SUBJECT($target)) } doesn't look very fresh.[/color]

## Rotting

rotting-rotting = [color=orange]{ CAPITALIZE(POSS-ADJ($target)) } corpse is rotting![/color]
rotting-bloated = [color=orangered]{ CAPITALIZE(POSS-ADJ($target)) } corpse is bloated![/color]
rotting-extremely-bloated = [color=red]{ CAPITALIZE(POSS-ADJ($target)) } corpse is extremely bloated![/color]

rotting-rotting-nonmob = [color=orange]{ CAPITALIZE(SUBJECT($target)) } is rotting![/color]
rotting-bloated-nonmob = [color=orangered]{ CAPITALIZE(SUBJECT($target)) } is bloated![/color]
rotting-extremely-bloated-nonmob = [color=red]{ CAPITALIZE(SUBJECT($target)) } is extremely bloated![/color]
