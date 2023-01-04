using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeComand : FightCommand
{
    public MeleeComand()
    {
        PossibleTargets = TargetTypes.Enemy;
        FightCommandType = FightCommandTypes.Melee;
    }
    public MeleeComand(Fighter executor, Fighter target, float priority) : base(executor, target, priority)
    {
        PossibleTargets = TargetTypes.Enemy;
        FightCommandType = FightCommandTypes.Melee;
    }

    public override void Excecute()
    {
        _target.TakeDamage(_executor.Attack);
        _executor.CurrentAmmo--;

    }

    public override void Undo()
    {
        _target.TakeDamage(-_executor.Attack);
        _executor.CurrentAmmo++;

    }
}
