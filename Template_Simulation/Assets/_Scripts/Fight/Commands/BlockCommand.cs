using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCommand : FightCommand
{
    protected static float _priorityBonus;
    protected float _protectionAmount = 5;

    public BlockCommand()
    {
        PossibleTargets = TargetTypes.Self;
        FightCommandType = FightCommandTypes.Block;
    }
    public BlockCommand(Fighter executor, Fighter target, float priority) : base(executor, target, priority)
    {
        _priority += _priorityBonus;
        PossibleTargets = TargetTypes.Self;
        FightCommandType = FightCommandTypes.Block;
    }

    public override void Excecute()
    {
        _target.AddDefense(_target.BaseDefense);
    }

    public override void Undo()
    {
        _target.AddDefense(-_target.BaseDefense);
    }
}
