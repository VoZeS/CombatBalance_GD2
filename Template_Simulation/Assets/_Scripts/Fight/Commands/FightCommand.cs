using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public abstract class FightCommand : ICommand
{
    protected Fighter _executor;
    protected Fighter _target;
    protected float _priority;
    public TargetTypes PossibleTargets;
    public FightCommandTypes FightCommandType;

    protected FightCommand() { }
    protected FightCommand(Fighter executor, Fighter target, float priority)
    {
        _executor = executor;
        _target = target;
        _priority = priority;
    }


    public void SetFighters(Fighter executor, Fighter target)
    {
        _executor = executor;
        _target = target;
    }
    public abstract void Excecute();
    public abstract void Undo();

   
}


public enum FightCommandTypes
{
    Melee,
    Shield,
    Heal,
    BuffAttack,
    BuffDefense,
    Block,
    Reload
}

public enum TargetTypes
{
    Enemy,
    Friend,
    Self,
    FriendNotSelf
}