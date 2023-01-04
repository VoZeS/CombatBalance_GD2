using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadCommand : FightCommand
{
    protected static float _priorityBonus;
    protected float _protectionAmount = 5;

    public ReloadCommand()
    {
        PossibleTargets = TargetTypes.Self;
        FightCommandType = FightCommandTypes.Reload;
    }
    public ReloadCommand(Fighter executor, Fighter target, float priority) : base(executor, target, priority)
    {
        _priority += _priorityBonus;
        PossibleTargets = TargetTypes.Self;
        FightCommandType = FightCommandTypes.Reload;
    }

    public override void Excecute()
    {
        _target.CurrentAmmo = _target.MaxAmmo;

        Debug.Log("Ammo: " +  _target.CurrentAmmo);
    }

    public override void Undo()
    {
    }
}
