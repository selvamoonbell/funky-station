// SPDX-FileCopyrightText: 2022 Flipp Syder <76629141+vulppine@users.noreply.github.com>
// SPDX-FileCopyrightText: 2022 Kara <lunarautomaton6@gmail.com>
// SPDX-FileCopyrightText: 2022 Paul Ritter <ritter.paul1@googlemail.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using Content.Server.Damage.Components;
using Content.Server.Explosion.EntitySystems;
using Content.Shared.Damage;
using Content.Shared.StepTrigger;
using Content.Shared.StepTrigger.Systems;

namespace Content.Server.Damage.Systems;

// System for damage that occurs on specific trigger, towards the user..
public sealed class DamageUserOnTriggerSystem : EntitySystem
{
    [Dependency] private readonly DamageableSystem _damageableSystem = default!;

    public override void Initialize()
    {
        SubscribeLocalEvent<DamageUserOnTriggerComponent, TriggerEvent>(OnTrigger);
    }

    private void OnTrigger(EntityUid uid, DamageUserOnTriggerComponent component, TriggerEvent args)
    {
        if (args.User is null)
            return;

        args.Handled |= OnDamageTrigger(uid, args.User.Value, component);
    }

    private bool OnDamageTrigger(EntityUid source, EntityUid target, DamageUserOnTriggerComponent? component = null)
    {
        if (!Resolve(source, ref component))
        {
            return false;
        }

        var damage = new DamageSpecifier(component.Damage);
        var ev = new BeforeDamageUserOnTriggerEvent(damage, target);
        RaiseLocalEvent(source, ev);

        return _damageableSystem.TryChangeDamage(target, ev.Damage, component.IgnoreResistances, origin: source) is not null;
    }
}

public sealed class BeforeDamageUserOnTriggerEvent : EntityEventArgs
{
    public DamageSpecifier Damage { get; set;  }
    public EntityUid Tripper { get; }

    public BeforeDamageUserOnTriggerEvent(DamageSpecifier damage, EntityUid target)
    {
        Damage = damage;
        Tripper = target;
    }
}
