using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealComand : FightCommand
{
    protected static float _priorityBonus;
    protected float _protectionAmount;

    public HealComand()
    {
        PossibleTargets = TargetTypes.Friend;
        FightCommandType = FightCommandTypes.Heal;
    }
    public HealComand(Fighter executor, Fighter target, float priority, float protection) : base(executor, target, priority)
    {
        _priority += _priorityBonus;
        _protectionAmount = protection;
        PossibleTargets = TargetTypes.Friend;
        FightCommandType = FightCommandTypes.Heal;
    }

    public override void Excecute()
    {
        _target.CurrentHealth+=3;
    }

    public override void Undo()
    {
        _target.CurrentHealth -= 3;
    }
}
