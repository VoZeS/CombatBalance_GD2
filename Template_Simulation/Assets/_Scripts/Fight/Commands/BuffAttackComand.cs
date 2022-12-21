using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAttackComand : FightCommand
{
    protected static float _priorityBonus;
    protected float _amount=1;


    public BuffAttackComand()
    {
        PossibleTargets = TargetTypes.Self;
        FightCommandType = FightCommandTypes.BuffAttack;
    }
    public BuffAttackComand(Fighter executor, Fighter target, float priority, float protection) : base(executor, target, priority)
    {
        _priority += _priorityBonus;
        _amount = protection;
        PossibleTargets = TargetTypes.Self;
        FightCommandType = FightCommandTypes.BuffAttack;
    }

    public override void Excecute()
    {
        _target.AddAttack(_amount);
    }

    public override void Undo()
    {
        _target.AddDefense(-_amount);
    }
}
