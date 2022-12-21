using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldComand : FightCommand
{
    protected static float _priorityBonus;
    protected float _protectionAmount=5;

    public ShieldComand()
    {
        PossibleTargets = TargetTypes.FriendNotSelf;
        FightCommandType = FightCommandTypes.Shield;
    }
    public ShieldComand(Fighter executor, Fighter target, float priority, float protection) : base(executor, target, priority)
    {
        _priority += _priorityBonus;
        _protectionAmount = protection;
        PossibleTargets = TargetTypes.FriendNotSelf;
        FightCommandType = FightCommandTypes.Shield;
    }

    public override void Excecute()
    {
        _target.AddDefense(_protectionAmount);
    }

    public override void Undo()
    {
        _target.AddDefense(-_protectionAmount);
    }
}
