public class BuffDefenseComand : FightCommand
{
    protected static float _priorityBonus;
    protected float _amount = 1;


    public BuffDefenseComand()
    {
        PossibleTargets = TargetTypes.Self;
        FightCommandType = FightCommandTypes.BuffDefense;
    }
    public BuffDefenseComand(Fighter executor, Fighter target, float priority, float protection) : base(executor, target, priority)
    {
        _priority += _priorityBonus;
        _amount = protection;
        PossibleTargets = TargetTypes.Self;
        FightCommandType = FightCommandTypes.BuffDefense;
    }

    public override void Excecute()
    {
        _target.AddDefense  (_amount);
    }

    public override void Undo()
    {
        _target.AddDefense(-_amount);
    }
}